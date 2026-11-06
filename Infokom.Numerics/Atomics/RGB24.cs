//using static Infokom.Numerics.Atomics.FloatingPointConstant;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Infokom.Numerics.Extensions;

namespace Infokom.Numerics.Atomics
{
	/// <summary>
	/// Binary data for ARGB32 color format.
	/// </summary>
	/// <remarks>
	/// 0xRR_GG_BB
	/// </remarks>
	[StructLayout(LayoutKind.Explicit, Size = 3)]
	public readonly struct RGB24 : IColor
	{
		private const uint B = 0x00_00_FF;
		private const uint G = 0x00_FF_00;
		private const uint R = 0xFF_00_00;

		[FieldOffset(0)] private readonly uint _data;
		[FieldOffset(0)] private readonly byte _b;
		[FieldOffset(1)] private readonly byte _g;
		[FieldOffset(2)] private readonly byte _r;

		[FieldOffset(0)] private readonly InlineArray3<BitVector8> _channels;

		private RGB24(uint data) => _data = data;

		private RGB24(byte r, byte g, byte b) => (_r, _g, _b) = (r, g, b);

		public float Red => _r / 255f;
		public float Green => _g / 255f;
		public float Blue => _b / 255f;
		float IColor.Alpha => 1;



		public override string ToString() => $"{_r:X2}{_g:X2}{_b:X2}";


		/// <summary>
		/// Creates a new RGB24 color from normalized float values for red, green, and blue channels.
		/// </summary>
		/// <param name="r">The red component (0.0 to 1.0).</param>
		/// <param name="g">The green component (0.0 to 1.0).</param>
		/// <param name="b">The blue component (0.0 to 1.0).</param>
		/// <returns>A new RGB24 color.</returns>
		/// <remarks>
		/// (r,g,b) is assumed to be normalized so that their sum equals 1.0. If not, a normalization step is performed to ensure the resulting color is valid.
		/// </remarks>
		public static RGB24 Create(float r, float g, float b) 
		{
			(r, g, b) = 255f * (r, g, b) / (r + g + b);

			return new((byte)r, (byte)g, (byte)b);
		}




		public static explicit operator RGB24(uint data) => new(data);
		public static implicit operator uint(RGB24 color) => color._data;


		public static RGB24 operator ~(RGB24 x) => new(~x._data);
		public static RGB24 operator &(RGB24 x, RGB24 y) => new(x._data & y._data);
		public static RGB24 operator |(RGB24 x, RGB24 y) => new(x._data | y._data);
		public static RGB24 operator ^(RGB24 x, RGB24 y) => new(x._data ^ y._data);
	}
}