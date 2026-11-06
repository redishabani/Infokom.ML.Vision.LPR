//using OpenCvSharp;

//using Sdcb.OpenVINO;
//using Sdcb.OpenVINO.PaddleOCR;
//using Sdcb.OpenVINO.PaddleOCR.Models.Online;

//using System.Drawing;

//namespace Infokom.ML.Vision.LPR.Apps.CLI.ConsoleDemo
//{
//	internal static class PaddleOcrDemo
//	{
//		public static async Task RunAsync(string file = ".\\data\\img\\ocr_test.png")
//		{
//			var model = await OnlineFullModels.EnglishV4.DownloadAsync();

//			using (PaddleOcrAll ocr = new(model))
//			{
//				// Run OCR on local image
//				using (Mat source = Cv2.ImRead(file))
//				{
//					Cv2.ImShow("Source Image", source);

//					_ = Cv2.WaitKey();

//					PaddleOcrResult result = ocr.Run(source);

//					Console.WriteLine("=== Full Extracted Text ===");
//					Console.WriteLine(result.Text);

//					Console.WriteLine("\n=== Line-by-Line Breakdown ===");

//					using (var target = source.Clone())
//					{
//						foreach (PaddleOcrResultRegion region in result.Regions)
//						{
//							Console.WriteLine($"[{region.Score:P1}] {region.Text}");

//							var vertices = region.Rect.Points();

//							Cv2.Line(target, (int)vertices[0].X, (int)vertices[0].Y, (int)vertices[1].X, (int)vertices[1].Y, Scalar.Red);
//							Cv2.Line(target, (int)vertices[1].X, (int)vertices[1].Y, (int)vertices[2].X, (int)vertices[2].Y, Scalar.Red);
//							Cv2.Line(target, (int)vertices[2].X, (int)vertices[2].Y, (int)vertices[3].X, (int)vertices[3].Y, Scalar.Red);
//							Cv2.Line(target, (int)vertices[3].X, (int)vertices[3].Y, (int)vertices[0].X, (int)vertices[0].Y, Scalar.Red);
//						}

//						Cv2.ImShow("OCR Results", target);

//						_ = Cv2.WaitKey();
//					}
//				}
//			}
//		}
//	}
//}
