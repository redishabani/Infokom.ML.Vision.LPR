namespace Infokom.ML.Vision.ANPR
{
	public interface IPixel<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public byte A { get; }
		
		public byte R { get; }
		
		public byte G { get; }

		public byte B { get; }

		public static abstract TPixel FromARGB(byte a, byte r, byte g, byte b);
	}
}
