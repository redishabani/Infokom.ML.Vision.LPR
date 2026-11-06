//using static Infokom.Numerics.Atomics.FloatingPointConstant;
using System.Runtime.InteropServices;

namespace Infokom.Numerics.Atomics
{
	/// <summary>
	/// Binary data for ARGB32 color format.
	/// </summary>
	/// <remarks>
	/// 0xAA_RR_GG_BB
	/// </remarks>
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct ARGB32 : IColor
	{
		private const uint B = 0x00_00_00_FF;
		private const uint G = 0x00_00_FF_00;
		private const uint R = 0x00_FF_00_00;
		private const uint A = 0xFF_00_00_00;

		[FieldOffset(0)] private readonly uint _data;
		[FieldOffset(0)] private readonly byte _r;
		[FieldOffset(1)] private readonly byte _g;
		[FieldOffset(2)] private readonly byte _b;
		[FieldOffset(3)] private readonly byte _a;

		[FieldOffset(0)] public readonly ByteVector4 Channels;		

		private ARGB32(uint data) => _data = data;

		public float Red => _r / 255f;
		public float Green => _g / 255f;
		public float Blue => _b / 255f;
		public float Alpha => _a / 255f;

		public static implicit operator ARGB32(uint data) => new(data);
		public static implicit operator uint(ARGB32 color) => color._data;

		public static ARGB32 operator ~(ARGB32 x) => new (~x._data);
		public static ARGB32 operator &(ARGB32 x, ARGB32 y) => new(x._data & y._data);
		public static ARGB32 operator |(ARGB32 x, ARGB32 y) => new(x._data | y._data);
		public static ARGB32 operator ^(ARGB32 x, ARGB32 y) => new(x._data ^ y._data);
	}
}