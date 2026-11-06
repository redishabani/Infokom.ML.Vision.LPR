using Infokom.Numerics.Atomics;

using System.Drawing;

namespace Infokom.ML.Vision.ANPR
{
	public interface IVehicleDetector<TFrame, TPixel> where TFrame : class, IFrame<TFrame, TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		Task<VehicleDetectionResult> DetectVehicleAsync(string imagePath);
	}






	public interface IFrame<TFrame, TPixel> where TFrame : class, IFrame<TFrame, TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public ulong Id { get; }

		public Size<int, int> Size { get; }

		public TPixel this[Point<int, int> point] { get; }


		
		public static abstract TFrame Create(Size<int, int> size, Func<Point<int, int>, TPixel> source);
	}



	public class VehicleDetectorResult
	{
		public Rectangle BoundingBox { get; set; }

		public float Confidence { get; set; }
	}
}