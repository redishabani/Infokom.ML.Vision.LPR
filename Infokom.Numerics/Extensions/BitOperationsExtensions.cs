using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace Infokom.Numerics.Extensions
{
	public static class BitOperationsExtensions
	{
		extension(BitOperations)
		{
			public static uint IsolateSegment(uint x, Range k)
			{
				var (k0, n) = k.GetOffsetAndLength(32);
				return Bmi1.BitFieldExtract(x, (byte)k0, (byte)n);
			}




			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static int UpmostNonZero(uint source) => BitOperations.Log2(source);






			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static uint IsolateUpmostNonZero(uint source) => 1u << BitOperations.UpmostNonZero(source);


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ushort IsolateUpmostNonZero(ushort source) => (ushort)(1u << BitOperations.UpmostNonZero(source));









			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static uint IsolateUpmost(uint x, int n)
			{
				n = Math.Clamp(n, 0, BitOperations.PopCount(x));

				var y = 0u;
				while (n-- is > 0) y |= 1u << BitOperations.Log2(x ^ y);
				return y;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static uint IsolateLowest(uint x, int n)
			{
				n = Math.Clamp(n, 0, BitOperations.PopCount(x));

				var y = 0u;
				while (n-- is > 0) y |= 1u << BitOperations.TrailingZeroCount(x ^ y);
				return y;
			}
		}
	}
}
