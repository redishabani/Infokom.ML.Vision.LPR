using System.Runtime.CompilerServices;
using System.Numerics;

namespace Infokom.Numerics.Atomics
{
	public static class OEIS<TTerm> where TTerm : unmanaged, IBinaryInteger<TTerm>
	{
		private static readonly TTerm LSB_FLAG_VALUE = ~(~TTerm.Zero << 1);
		private static readonly TTerm MSB_FLAG_VALUE = ~(~TTerm.Zero >> 1);

		private static readonly int LSB_FLAG_INDEX = LSB_FLAG_VALUE.GetShortestBitLength() - 1;
		private static readonly int MSB_FLAG_INDEX = MSB_FLAG_VALUE.GetShortestBitLength() - 1;

		public static class A257548
		{
			private static readonly TTerm TERM_MASK = ~(~TTerm.Zero << 5);//0b11111;
			private static readonly TTerm LAST_TERM_VALUE = ~(~TTerm.Zero >> 5);//000001...1 -> 111110..0;
			private static readonly int LAST_TERM_INDEX = LAST_TERM_VALUE.GetShortestBitLength();

			/// <summary>
			/// 
			/// </summary>
			/// <param name="index"></param>
			/// <param name="value"></param>
			/// <returns></returns>
			public static bool TryGet(int index, out TTerm value)
			{
				value = TERM_MASK << (index - 6);
			
				return (uint)index <= LAST_TERM_INDEX;
			}
		}
	}
	public static class OEIS
	{
		public static class A257548
		{
			public static class UINT16
			{
				public readonly struct Provider
				{
					private const uint BASE_TERM = 0x1Fu;

					public ushort this[int n]
					{
						[MethodImpl(MethodImplOptions.AggressiveInlining)]
						get => checked((ushort)unchecked((BASE_TERM << n) >> 5));
					}

					public ushort[] this[Range range]
					{
						[MethodImpl(MethodImplOptions.AggressiveInlining)]
						get
						{
							var (offset, length) = range.GetOffsetAndLength(17);
							var result = new ushort[length]; 
							for (int i = 0; i < length; i++)
								result[i] = (ushort)unchecked((BASE_TERM << (i + offset)) >> 5);
							return result;
						}
					}
				}
			}
		}

		public static class UINT16
		{
			public readonly struct Provider
			{
				/// <summary>
				/// <see href="https://oeis.org/A257548">A257548</see>
				/// </summary>
				public readonly A257548.UINT16.Provider A257548;
			}
		}

	}

	public static class OEIS_UINT16
	{
		public static readonly OEIS.UINT16.Provider OEIS;

		extension(ushort)
		{
			/// <summary>
			/// <see href="https://oeis.org/">The On-Line Encyclopedia of Integer Sequences</see>
			/// </summary>
			public static OEIS.UINT16.Provider OEIS => OEIS;
		}

		public static void Foo()
		{
			var ff = ushort.OEIS.A257548[1];
		}
	}
}
