using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using OpenCvSharp;
using OpenCvSharp.Dnn;

using System.Runtime.InteropServices;

using static Infokom.ML.Vision.Internal.YOLO.v8.Detector;

namespace Infokom.ML.Vision.Internal.YOLO.v8
{
	
	

	public enum ObjectCategory
	{
		Person = 0,
		Bicycle = 1,
		Car = 2,
		Motorcycle = 3,
		Airplane = 4,
		Bus = 5,
		Train = 6,
		Truck = 7,
		Boat = 8,
		TrafficLight = 9,
		FireHydrant = 10,
		StopSign = 11,
		ParkingMeter = 12,
		Bench = 13,
		Bird = 14,
		Cat = 15,
		Dog = 16,
		Horse = 17,
		Sheep = 18,
		Cow = 19,
		Elephant = 20,
		Bear = 21,
		Zebra = 22,
		Giraffe = 23,
		Backpack = 24,
		Umbrella = 25,
		Handbag = 26,
		Tie = 27,
		Suitcase = 28,
		Frisbee = 29,
		Skis = 30,
		Snowboard = 31,
		SportsBall = 32,
		Kite = 33,
		BaseballBat = 34,
		BaseballGlove = 35,
		Skateboard = 36,
		Surfboard = 37,
		TennisRacket = 38,
		Bottle = 39,
		WineGlass = 40,
		Cup = 41,
		Fork = 42,
		Knife = 43,
		Spoon = 44,
		Bowl = 45,
		Banana = 46,
		Apple = 47,
		Sandwich = 48,
		Orange = 49,
		Broccoli = 50,
		Carrot = 51,
		HotDog = 52,
		Pizza = 53,
		Donut = 54,
		Cake = 55,
		Chair = 56,
		Couch = 57,
		PottedPlant = 58,
		Bed = 59,
		DiningTable = 60,
		Toilet = 61,
		Tv = 62,
		Laptop = 63,
		Mouse = 64,
		Remote = 65,
		Keyboard = 66,
		CellPhone = 67,
		Microwave = 68,
		Oven = 69,
		Toaster = 70,
		Sink = 71,
		Refrigerator = 72,
		Book = 73,
		Clock = 74,
		Vase = 75,
		Scissors = 76,
		TeddyBear = 77,
		HairDrier = 78,
		Toothbrush = 79
	}

	internal readonly record struct ObjectPrediction
	{
		public ObjectCategory Category { get; }
		public float Confidence { get; }
		public Rect BoundingBox { get; }
	}


	public class Frame<TContent>
	{
		public Guid Id { get; set; }

		public DateTimeOffset Instant { get; set; }

		public TContent Content { get; set; }
	}


	public class Image : Mat
	{
		public Image(int size) : base(size, size, MatType.CV_8UC3)
		{

		}

		public Image(int sizeX, int sizeY) : base(sizeX, sizeY, MatType.CV_8UC3)
		{
			
		}
	}

	public class Detection
	{
		public ObjectCategory Category { get; set; }
		public float Confidence { get; set; }
		public Rect BoundingBox { get; set; }


		public class Inference : List<Detection>
		{
			public class Session : InferenceSession
			{
				public Session() : base(@".\yolov8n.onnx")
				{
				}
			}









			
		}
	}

	public class Detector : InferenceSession
	{
		public class Frame
		{
			public struct Entry
			{
				public ObjectCategory Category { get; set; }
				public float Confidence { get; set; }
				public Rect BoundingBox { get; set; }
			}


			public Tensor<float> Input { get; set; }

			public Tensor<float> Output { get; set; }


			public List<Frame.Entry> GetDetections()
			{
				var data = this.Output.ToArray();

				var boxes = new List<Rect>();
				var confidences = new List<float>();
				var classIds = new List<int>();

				int rows = 8400; // Numri i kutive të parashikuara

				// YOLOv8 e nxjerr rezultatin në formatin e transpozuar [84 x 8400]
				// Ne duhet ta lexojmë në mënyrë të saktë kolonë pas kolone
				for (int c = 0; c < rows; c++)
				{
					// 1. Gjejmë klasën me saktësinë më të lartë për këtë kuti
					float maxScore = 0;
					int classId = -1;

					for (int i = 0; i < 80; i++)
					{
						float score = data[(4 + i) * rows + c];
						if (score > maxScore)
						{
							maxScore = score;
							classId = i;
						}
					}

					// Filtrojmë nëse saktësia është më e ulët se pragu (threshold)
					if (maxScore >= CNF_TH)
					{
						// 2. Lexojmë koordinatat e kësaj kutie (janë në formatin: QendërX, QendërY, Gjerësi, Lartësi)
						float cx = data[0 * rows + c];
						float cy = data[1 * rows + c];
						float w = data[2 * rows + c];
						float h = data[3 * rows + c];

						// Kthejmë koordinatat në formatin standard të OpenCV (Top-Left X, Top-Left Y, W, H)
						int x = (int)(cx - w / 2f);
						int y = (int)(cy - h / 2f);

						boxes.Add(new Rect(x, y, (int)w, (int)h));
						confidences.Add(maxScore);
						classIds.Add(classId);
					}
				}

				// 3. Përdorim funksionin e fuqishëm të OpenCV për të hequr kutitë e tepërta që mbivendosen (NMS)
				Cv2.Dnn.NMSBoxes(boxes, confidences, CNF_TH, NMS_TH, out int[] indices);

				var result = new List<Frame.Entry>();

				foreach (int idx in indices)
				{
					result.Add(new()
					{
						Category = (ObjectCategory)classIds[idx],
						Confidence = confidences[idx],
						BoundingBox = boxes[idx]
					});
				}

				return result;
			}

			public void DrawDetection(Mat image)
			{
				var detections = this.GetDetections();
				foreach (var obj in detections)
				{
					// Vizato kutinë rreth objektit
					Cv2.Rectangle(image, obj.BoundingBox, Scalar.LightGreen, 2);

					// Shkruaj klasën dhe saktësinë sipër kutisë
					string label = $"ID: {obj.Category} ({obj.Confidence:P0})";
					Cv2.PutText(
						image, 
						label,
						new Point(obj.BoundingBox.X, obj.BoundingBox.Y - 5),
						HersheyFonts.HersheySimplex, 
						0.5,
						Scalar.Red, 
						1);
					
				}
			}
		}

		private const float CNF_TH = 0.45f;//confidence threshold
		private const float NMS_TH = 0.45f;//non mimima suppression threshold

	


		private const int W = 640, H = 640;
		private const string INPUT = "images";
		private const string OUTPUT = "output0";

		public Detector(string model = null) : base(model ?? @".\Internal\YOLO\v8\yolov8n.onnx")
		{
		}



		

		public Tensor<float> Run(Tensor<float> source)
		{
			var input = NamedOnnxValue.CreateFromTensor(INPUT, source);
			var output = NamedOnnxValue.CreateFromTensor(OUTPUT, new DenseTensor<float>([1, 84, 8400]));

			this.Run([input], [output]);

			return output.AsTensor<float>();
		}

		public Frame Run(Mat source)
		{
			using (var blob = Cv2.Dnn.BlobFromImage(source, 1.0 / 255.0, new(640, 640), new(0, 0, 0), true, false))
			{
				var buffer = new float[1 * 3 * 640 * 640];

				Marshal.Copy(blob.Data, buffer, 0, buffer.Length);

				var inputTensor = new DenseTensor<float>(buffer, [1, 3, 640, 640]);

				var outputTensor = this.Run(inputTensor);

				return new Frame() { Input = inputTensor, Output = outputTensor };
			}
		}

		public void Run(Frame frame)
		{
			
		}
	}




	public class YoloOutputParser
	{
		

		
	}
}
