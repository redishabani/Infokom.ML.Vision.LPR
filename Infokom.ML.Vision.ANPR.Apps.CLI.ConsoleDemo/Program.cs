using OpenCvSharp;

using YOLO = Infokom.ML.Vision.Internal.YOLO;

namespace Infokom.ML.Vision.LPR.Apps.CLI.ConsoleDemo
{
	internal static class Program
	{

		private const string DATASETS = @"~\.datasets";

		public static void Main(string[] args)
		{
			string modelPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Local\\Infokom\\ML\\Models\\yolov8n.onnx");
			
			using(var capture = new VideoCapture("E:\\datasets\\LPR\\skela.mp4"))
			{
				using (var detector = new YOLO.v8.Detector())
				{
					using(var videoFrame = new Mat())
					{
						while (capture.Read(videoFrame))
						{
							if (videoFrame.Empty()) break;

							using (var image = videoFrame.Letterbox(out var _, out var _, out var _))
							{

								var detectorOutput = detector.Run(image);

								detectorOutput.DrawDetection(image);

								Cv2.ImShow("YOLOv8 Detektimi Live", image);

								if (Cv2.WaitKey(1) == 'q') break;
							}
						}
					}
				}
			}

			

			
		}


		static Mat Letterbox(this Mat image, out float scale, out int padX, out int padY)
		{
			float scaleX = (float)640 / image.Width;
			float scaleY = (float)640 / image.Height;

			scale = Math.Min(scaleX, scaleY);

			int width = (int)Math.Round(image.Width * scale);
			int height = (int)Math.Round(image.Height * scale);

			padX = (640 - width) / 2;
			padY = (640 - height) / 2;

			var resized = new Mat();

			Cv2.Resize(image,resized,new Size(width, height));

			var output = new Mat(640, 640, image.Type(), new Scalar(114, 114, 114));

			resized.CopyTo(new Mat(output,new Rect(padX, padY, width, height)));

			resized.Dispose();

			return output;
		}
	}


	



	
}
