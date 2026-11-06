using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Infokom.Numerics.Atomics
{




	[StructLayout(LayoutKind.Explicit)]
	public struct BitMatrix16x16 : IEquatable<BitMatrix16x16>, IEqualityOperators<BitMatrix16x16, BitMatrix16x16, bool>
	{
		private const ulong ZEROS_A = ulong.MinValue, ZEROS_B = ulong.MinValue, ZEROS_C = ulong.MinValue, ZEROS_D = ulong.MinValue;
		private const ulong UNITS_A = ulong.MaxValue, UNITS_B = ulong.MaxValue, UNITS_C = ulong.MaxValue, UNITS_D = ulong.MaxValue;

		[FieldOffset(0)] private readonly ulong _00_08;
		[FieldOffset(8)] private readonly ulong _08_16;
		[FieldOffset(16)] private readonly ulong _16_24;
		[FieldOffset(24)] private readonly ulong _24_32;

		[FieldOffset(0)] public InlineArray16<BitVector16> Rows;



		private BitMatrix16x16(ulong a, ulong b, ulong c, ulong d)
		{
			_00_08 = a;
			_08_16 = b;
			_16_24 = c;
			_24_32 = d;
		}

		public BitMatrix16x16(BitVector16 row00 = default, BitVector16 row01 = default, BitVector16 row02 = default, BitVector16 row03 = default, BitVector16 row04 = default, BitVector16 row05 = default, BitVector16 row06 = default, BitVector16 row07 = default, BitVector16 row08 = default, BitVector16 row09 = default, BitVector16 row10 = default, BitVector16 row11 = default, BitVector16 row12 = default, BitVector16 row13 = default, BitVector16 row14 = default, BitVector16 row15 = default)
		{
			var rows = new InlineArray16<BitVector16>();
			rows[00] = row00;
			rows[01] = row01;
			rows[02] = row02;
			rows[03] = row03;
			rows[04] = row04;
			rows[05] = row05;
			rows[06] = row06;
			rows[07] = row07;
			rows[08] = row08;
			rows[09] = row09;
			rows[10] = row10;
			rows[11] = row11;
			rows[12] = row12;
			rows[13] = row13;
			rows[14] = row14;
			rows[15] = row15;
			Rows = rows;
		}

		public BitMatrix16x16(InlineArray16<BitVector16> rows)
		{
			Rows = rows;
		}

		public BitMatrix16x16(ReadOnlySpan<BitVector16> rows)
		{
			if (rows.Length != 16)
				throw new ArgumentException("The input span must have a length of 16.", nameof(rows));

			for (int i = 0; i < 16; i++)
			{
				Rows[i] = rows[i];
			}
		}

		public Bit this[int r, int c]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => (uint)r < 16 && (uint)c < 16 && Rows[r][c];
			set
			{
				if ((uint)r < 16 && (uint)c < 16)
				{
					ref var row = ref Rows[r];

					row = value ? row | BitVector16.Unit(c) : row & ~BitVector16.Unit(c);
				}
			}
		}

		public Bit this[Point<sbyte, sbyte> p]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => this[p.Y, p.X];
			set => this[p.Y, p.X] = value;
		}

		public readonly int NNZ => BitOperations.PopCount(_00_08) + BitOperations.PopCount(_08_16) + BitOperations.PopCount(_16_24) + BitOperations.PopCount(_24_32);

		public override readonly string ToString()
		{
			string HEAD = $"┌{new string(' ', 16 * 3)}┐" + Environment.NewLine;
			string FOOT = $"└{new string(' ', 16 * 3)}┘";


			string result = HEAD;
			for (int i = 0; i < 16; i++)
			{
				result += "|";
				for (int j = 0; j < 16; j++)
				{
					result += this[i, j] ? " 1 " : " 0 ";
				}
				result += "|" + Environment.NewLine;
			}

			result += FOOT;
			return Environment.NewLine + result + Environment.NewLine;
		}

		public readonly override int GetHashCode() => HashCode.Combine(_00_08, _08_16, _16_24, _24_32);
		public readonly bool Equals(BitMatrix16x16 other) => _00_08.Equals(other._00_08) && _08_16.Equals(other._08_16) && _16_24.Equals(other._16_24) && _24_32.Equals(other._24_32);
		public readonly override bool Equals(object obj) => obj is BitMatrix16x16 other && this.Equals(other);


		public static bool operator ==(BitMatrix16x16 x, BitMatrix16x16 y) => x._00_08 == y._00_08 && x._08_16 == y._08_16 && x._16_24 == y._16_24 && x._24_32 == y._24_32;
		public static bool operator !=(BitMatrix16x16 x, BitMatrix16x16 y) => x._00_08 != y._00_08 || x._08_16 != y._08_16 || x._16_24 != y._16_24 || x._24_32 != y._24_32;























		/// <summary>
		/// Transposes this matrix, swapping rows and columns.
		/// </summary>
		/// <returns>The transposed of this matrix.</returns>
		public readonly BitMatrix16x16 Transpose() => Transpose(this);

		/// <summary>
		/// Rotates this matrix 90 degrees counterclockwise.
		/// </summary>
		/// <returns>The rotated matrix.</returns>
		public readonly BitMatrix16x16 Rotate() => Rotate(this);







		public static BitMatrix16x16 operator ~(BitMatrix16x16 x) => new(
			~x._00_08,
			~x._08_16,
			~x._16_24,
			~x._24_32);

		public static BitMatrix16x16 operator &(BitMatrix16x16 x, BitMatrix16x16 y) => new(
			x._00_08 & y._00_08,
			x._08_16 & y._08_16,
			x._16_24 & y._16_24,
			x._24_32 & y._24_32);

		public static BitMatrix16x16 operator |(BitMatrix16x16 x, BitMatrix16x16 y) => new(
			x._00_08 | y._00_08,
			x._08_16 | y._08_16,
			x._16_24 | y._16_24,
			x._24_32 | y._24_32);

		public static BitMatrix16x16 operator ^(BitMatrix16x16 x, BitMatrix16x16 y) => new(
			x._00_08 ^ y._00_08,
			x._08_16 ^ y._08_16,
			x._16_24 ^ y._16_24,
			x._24_32 ^ y._24_32);

		public static BitMatrix16x16 Rand() => new(
			(ulong)Random.Shared.NextInt64(),
			(ulong)Random.Shared.NextInt64(),
			(ulong)Random.Shared.NextInt64(),
			(ulong)Random.Shared.NextInt64());


		/// <summary>
		/// Transpose a matrix, swapping rows and columns.
		/// </summary>
		/// <param name="μ">The matrix to transpose.</param>
		/// <returns>The transposed of <paramref name="μ"/>.</returns>
		public static BitMatrix16x16 Transpose(BitMatrix16x16 μ)
		{
			var result = new BitMatrix16x16();
			for (int r = 0; r < 16; r++)
			{
				for (int c = 0; c < 16; c++)
				{
					result[c, r] = μ[r, c];
				}
			}
			return result;
		}

		/// <summary>
		/// Rotates the matrix 90 degrees counterclockwise.
		/// </summary>
		/// <param name="μ">The matrix to rotate.</param>
		/// <returns>The rotated matrix.</returns>
		public static BitMatrix16x16 Rotate(BitMatrix16x16 μ)
		{
			var result = new BitMatrix16x16();
			for (int r = 0; r < 16; r++)
			{
				for (int c = 0; c < 16; c++)
				{
					result[15 - c, r] = μ[r, c];
				}
			}
			return result;
		}

		public static readonly BitMatrix16x16 Φ;
		public static readonly BitMatrix16x16 Ω = ~Φ;


		/// <summary>
		/// Creates a new BitMatrix16x16 with the specified row set to all ones.
		/// </summary>
		/// <param name="y">The index of the row to set.</param>
		/// <returns>The created BitMatrix16x16 with the specified row set.</returns>
		/// <remarks>
		/// <![CDATA[
		/// ┌                ┐
		/// ╎                ╎
		/// |  1   1   ┈   1 | ← y
		/// ╎                ╎
		/// └                ┘
		/// ]]>
		/// </remarks>
		public static BitMatrix16x16 Row(int y) => OfRow(y, BitVector16.Ω);

		/// <summary>
		/// Creates a new BitMatrix16x16 with the specified row set to the given elements.
		/// </summary>
		/// <param name="y">The index of the row to set.</param>
		/// <param name="v">The elements to set in the specified row.</param>
		/// <returns>The created BitMatrix16x16 with the specified row set.</returns>
		/// <remarks>
		/// <![CDATA[
		/// ┌                   ┐
		/// ╎                   ╎
		/// | v[0] v[1] ┈ v[15] | ← y
		/// ╎                   ╎
		/// └                   ┘
		/// ]]>
		/// </remarks>
		public static BitMatrix16x16 OfRow(int y, BitVector16 v)
		{
			var matrix = new BitMatrix16x16();

			matrix.Rows[y] = v;

			return matrix;
		}



		public static BitMatrix16x16 Row(int y, Func<int, Bit> ν)
		{
			var matrix = new BitMatrix16x16();

			matrix.Rows[y] = BitVector16.Create(ν);

			return matrix;
		}











		/// <summary>
		/// Creates a new BitMatrix16x16 with the specified row set to the given elements.
		/// </summary>
		/// <param name="y">The index of the row to set.</param>
		/// <param name="v">The elements to set in the specified row.</param>
		/// <returns>The created BitMatrix16x16 with the specified row set.</returns>
		/// <remarks>
		/// <![CDATA[
		/// ┌                   ┐
		/// ╎                   ╎
		/// | v[0] v[1] ┈ v[15] | ← y
		/// ╎                   ╎
		/// └                   ┘
		/// ]]>
		/// </remarks>
		public static BitMatrix16x16 Col(int y, BitVector16 v) => Col(y, x => v[x]);

		/// <summary>
		/// Creates a new BitMatrix16x16 with the specified column set to the given elements.
		/// </summary>
		/// <param name="x">The index of the column to set.</param>
		/// <param name="ν">A function that returns the element for each row in the specified column.</param>
		/// <returns>The created BitMatrix16x16 with the specified column set.</returns>
		/// <remarks>
		/// <![CDATA[
		/// ┌                   ┐
		/// ╎                   ╎
		/// | f(0) f(1) ┈ f(15) | ← x
		/// ╎                   ╎
		/// └                   ┘
		/// ]]>
		/// </remarks>
		public static BitMatrix16x16 Col(int x, Func<int, Bit> ν)
		{
			var m = new BitMatrix16x16();

			for (int y = 0; y < 16; y++)
				m[y, x] = ν(y);

			return m;
		}
	}
}