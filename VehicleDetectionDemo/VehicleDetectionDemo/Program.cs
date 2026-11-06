using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

const int ImageSize = 640;

if (args.Length == 0)
{
	PrintUsage();
	return;
}

try
{
	switch (args[0].ToLowerInvariant())
	{
		case "prepare" when args.Length >= 3:
			DatasetBuilder.Prepare(args[1], args[2], seed: args.Length >= 4 ? int.Parse(args[3]) : 1234);
			break;

		case "train" when args.Length >= 2:
			var dataset = Path.GetFullPath(args[1]);
			var epochs = args.Length >= 3 ? int.Parse(args[2]) : 50;
			await RunTrainerAsync("all", Path.Combine(dataset, "data.yaml"),
			    epochs.ToString(CultureInfo.InvariantCulture),
			    Path.Combine(dataset, "artifacts", "yolov8m.optimized.onnx"));
			break;

		case "infer" when args.Length >= 3:
			using (var detector = new VehicleDetector(args[1]))
			{
				var detections = detector.Detect(args[2], confidenceThreshold: 0.25f);
				foreach (var d in detections)
				{
					Console.WriteLine(
					    $"vehicle  confidence={d.Confidence:P1}  " +
					    $"x={d.X:F1}, y={d.Y:F1}, w={d.Width:F1}, h={d.Height:F1}");
				}

				Console.WriteLine($"{detections.Count} vehicle(s) detected.");
			}
			break;

		default:
			PrintUsage();
			break;
	}
}
catch (Exception ex)
{
	Console.Error.WriteLine(ex);
	Environment.ExitCode = 1;
}

static async Task RunTrainerAsync(params string[] arguments)
{
	var script = Path.Combine(AppContext.BaseDirectory, "trainer.py");
	if (!File.Exists(script))
		throw new FileNotFoundException("trainer.py was not copied to the application output directory.", script);

	var start = new ProcessStartInfo
	{
		FileName = Environment.GetEnvironmentVariable("PYTHON") ?? "python",
		WorkingDirectory = AppContext.BaseDirectory,
		UseShellExecute = false
	};
	start.ArgumentList.Add(script);
	foreach (var argument in arguments)
		start.ArgumentList.Add(argument);

	using var process = Process.Start(start)
	    ?? throw new InvalidOperationException("Could not start Python.");

	await process.WaitForExitAsync();
	if (process.ExitCode != 0)
		throw new InvalidOperationException($"Training script failed with exit code {process.ExitCode}.");
}

static void PrintUsage()
{
	Console.WriteLine("""
    VehicleDetectionDemo

      prepare <annotations.json> <output-dataset-dir> [seed]
      train   <output-dataset-dir> [epochs]
      infer   <model.onnx> <image-file>

    Example:
      dotnet run -- prepare data/annotations.json data/vehicle-dataset
      dotnet run -- train data/vehicle-dataset 50
      dotnet run -- infer data/vehicle-dataset/artifacts/vehicle.onnx data/test.jpg
    """);
}

public sealed record Detection(float X, float Y, float Width, float Height, float Confidence);

public sealed class VehicleDetector : IDisposable
{
	private readonly InferenceSession _session;
	private readonly string _inputName;
	private const int Size = 640;

	public VehicleDetector(string modelPath)
	{
		var options = new SessionOptions
		{
			IntraOpNumThreads = Math.Max(1, Environment.ProcessorCount - 1),
			GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL
		};

		_session = new InferenceSession(modelPath, options);
		_inputName = _session.InputMetadata.Keys.First();
	}

	public List<Detection> Detect(string imagePath, float confidenceThreshold = 0.25f)
	{
		using var original = Image.Load<Rgb24>(imagePath);
		int originalWidth = original.Width;
		int originalHeight = original.Height;

		float scale = Math.Min((float)Size / originalWidth, (float)Size / originalHeight);
		int resizedWidth = (int)Math.Round(originalWidth * scale);
		int resizedHeight = (int)Math.Round(originalHeight * scale);
		int padX = (Size - resizedWidth) / 2;
		int padY = (Size - resizedHeight) / 2;

		using var resized = original.Clone(ctx => ctx.Resize(new ResizeOptions
		{
			Size = new Size(resizedWidth, resizedHeight),
			Mode = ResizeMode.Stretch,
			Sampler = KnownResamplers.Bicubic
		}));

		// YOLO letterbox padding is RGB 114; tensor layout is NCHW, RGB, [0,1].
		var inputData = new float[1 * 3 * Size * Size];
		Array.Fill(inputData, 114f / 255f);

		resized.ProcessPixelRows(accessor =>
		{
			for (int y = 0; y < resizedHeight; y++)
			{
				Span<Rgb24> row = accessor.GetRowSpan(y);
				for (int x = 0; x < resizedWidth; x++)
				{
					var pixel = row[x];
					int targetX = x + padX;
					int targetY = y + padY;
					int plane = Size * Size;
					int offset = targetY * Size + targetX;

					inputData[offset] = pixel.R / 255f;
					inputData[plane + offset] = pixel.G / 255f;
					inputData[2 * plane + offset] = pixel.B / 255f;
				}
			}
		});

		var tensor = new DenseTensor<float>(inputData, new[] { 1, 3, Size, Size });
		using var outputs = _session.Run(new[]
		{
		  NamedOnnxValue.CreateFromTensor(_inputName, tensor)
	   });

		var output = outputs.First().AsTensor<float>();
		var dims = output.Dimensions.ToArray();

		// This demo exports YOLO11 with embedded NMS. Expected output: [1, max_det, 6].
		// Each detection row is [x1, y1, x2, y2, confidence, class_id].
		if (dims.Length != 3 || dims[0] != 1 || dims[2] != 6)
		{
			throw new NotSupportedException(
			    $"Unexpected ONNX output shape [{string.Join(",", dims)}]. " +
			    "This decoder expects an NMS-enabled YOLO export with shape [1, N, 6].");
		}

		float[] values = output.ToArray();
		int count = dims[1];
		var detections = new List<Detection>();

		for (int i = 0; i < count; i++)
		{
			int offset = i * 6;
			float x1 = values[offset];
			float y1 = values[offset + 1];
			float x2 = values[offset + 2];
			float y2 = values[offset + 3];
			float confidence = values[offset + 4];
			int classId = (int)Math.Round(values[offset + 5]);

			if (confidence < confidenceThreshold || classId != 0)
				continue;

			// Remove letterbox padding and map coordinates back to original image pixels.
			x1 = Math.Clamp((x1 - padX) / scale, 0, originalWidth);
			y1 = Math.Clamp((y1 - padY) / scale, 0, originalHeight);
			x2 = Math.Clamp((x2 - padX) / scale, 0, originalWidth);
			y2 = Math.Clamp((y2 - padY) / scale, 0, originalHeight);

			if (x2 > x1 && y2 > y1)
				detections.Add(new Detection(x1, y1, x2 - x1, y2 - y1, confidence));
		}

		return detections;
	}

	public void Dispose() => _session.Dispose();
}

public static class DatasetBuilder
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	public static void Prepare(string annotationsPath, string outputDirectory, int seed)
	{
		string annotationFullPath = Path.GetFullPath(annotationsPath);
		string sourceRoot = Path.GetDirectoryName(annotationFullPath)!;
		var manifest = JsonSerializer.Deserialize<DatasetManifest>(
		    File.ReadAllText(annotationFullPath), JsonOptions)
		    ?? throw new InvalidDataException("Could not parse annotations JSON.");

		if (manifest.Images.Count < 10)
			throw new InvalidDataException("Use at least 10 images so train/validation/test splits are meaningful.");

		var random = new Random(seed);
		var shuffled = manifest.Images.OrderBy(_ => random.Next()).ToList();

		int testCount = Math.Max(1, shuffled.Count / 10);
		int validationCount = Math.Max(1, shuffled.Count / 10);
		int trainCount = shuffled.Count - validationCount - testCount;
		if (trainCount < 1)
			throw new InvalidDataException("Not enough images for the requested split.");

		var splits = new Dictionary<string, List<DatasetImage>>
		{
			["train"] = shuffled.Take(trainCount).ToList(),
			["val"] = shuffled.Skip(trainCount).Take(validationCount).ToList(),
			["test"] = shuffled.Skip(trainCount + validationCount).ToList()
		};

		string outputRoot = Path.GetFullPath(outputDirectory);
		if (Directory.Exists(outputRoot))
			Directory.Delete(outputRoot, recursive: true);

		foreach (string split in splits.Keys)
		{
			Directory.CreateDirectory(Path.Combine(outputRoot, "images", split));
			Directory.CreateDirectory(Path.Combine(outputRoot, "labels", split));
		}

		int imageIndex = 0;
		foreach (var (split, images) in splits)
		{
			foreach (var item in images)
			{
				string sourceImage = Path.GetFullPath(Path.Combine(sourceRoot, item.File));
				if (!File.Exists(sourceImage))
					throw new FileNotFoundException($"Image not found: {sourceImage}");

				using var image = Image.Load<Rgb24>(sourceImage);
				int width = image.Width;
				int height = image.Height;

				string outputName = $"{imageIndex++:D7}{Path.GetExtension(sourceImage).ToLowerInvariant()}";
				string destinationImage = Path.Combine(outputRoot, "images", split, outputName);
				File.Copy(sourceImage, destinationImage, overwrite: true);

				var lines = new List<string>();
				foreach (var box in item.Objects)
				{
					float x1 = Math.Clamp(box.X, 0, width);
					float y1 = Math.Clamp(box.Y, 0, height);
					float x2 = Math.Clamp(box.X + box.Width, 0, width);
					float y2 = Math.Clamp(box.Y + box.Height, 0, height);

					if (x2 <= x1 || y2 <= y1)
						throw new InvalidDataException($"Invalid bounding box in {item.File}.");

					float cx = ((x1 + x2) / 2f) / width;
					float cy = ((y1 + y2) / 2f) / height;
					float bw = (x2 - x1) / width;
					float bh = (y2 - y1) / height;

					lines.Add(string.Join(" ",
					    "0",
					    cx.ToString("F6", CultureInfo.InvariantCulture),
					    cy.ToString("F6", CultureInfo.InvariantCulture),
					    bw.ToString("F6", CultureInfo.InvariantCulture),
					    bh.ToString("F6", CultureInfo.InvariantCulture)));
				}

				string labelName = Path.GetFileNameWithoutExtension(outputName) + ".txt";
				File.WriteAllLines(Path.Combine(outputRoot, "labels", split, labelName), lines);
			}
		}

		string yaml = $"""
        path: "{outputRoot.Replace("\\", "/")}"
        train: images/train
        val: images/val
        test: images/test
        names:
          0: vehicle
        """;
		File.WriteAllText(Path.Combine(outputRoot, "data.yaml"), yaml);

		Console.WriteLine($"Dataset prepared: {outputRoot}");
		Console.WriteLine($"Train: {splits["train"].Count}, validation: {splits["val"].Count}, test: {splits["test"].Count}");
	}
}

public sealed class DatasetManifest
{
	public List<DatasetImage> Images { get; set; } = [];
}

public sealed class DatasetImage
{
	public string File { get; set; } = "";
	public List<BoundingBox> Objects { get; set; } = [];
}

public sealed class BoundingBox
{
	public float X { get; set; }
	public float Y { get; set; }
	public float Width { get; set; }
	public float Height { get; set; }
}
