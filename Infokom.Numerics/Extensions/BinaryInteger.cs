using Infokom.Numerics.Atomics;

using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace Infokom.Numerics.Extensions
{
#pragma warning restore IDE1006 // Naming Styles

	public static partial class BinaryInteger
	{


		#region BIT SCAN RANDOM
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanRandom(this byte source) => BitScanRandom((uint)source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanRandom(this ushort source) => BitScanRandom((uint)source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanRandom(this uint source)
		{
			int offset = -1;

			if (source != default)
			{
				var n = (int)uint.Random(0u, (uint)(BitOperations.PopCount(source) - 1));//skip count
				if (Bmi2.IsSupported)
					offset = BitOperations.TrailingZeroCount(Bmi2.ParallelBitDeposit(1u << n, source));
				else
					for (int i = 0; i <= n; i++) offset = BitOperations.TrailingZeroCount(source &= (source - 1));
			}

			return offset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte BitScanShuffle(this ulong source)
		{
			sbyte offset = -1;

			if (source != default)
			{
				var n = (int)uint.Random(0u, (uint)(BitOperations.PopCount(source) - 1));//skip count
				if (Bmi2.X64.IsSupported)
					offset = (sbyte)BitOperations.TrailingZeroCount(Bmi2.X64.ParallelBitDeposit(1ul << n, source));
				else
					for (int i = 0; i <= n; i++) offset = (sbyte)BitOperations.TrailingZeroCount(source &= (source - 1));
			}

			return offset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanRandom(this ushort source, out int offset, out ushort target)
		{

			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (ushort)(source & ~(1u << (offset = source.BitScanRandom())));
			return true;
		}




		public static bool BitScanRandom(this byte source, out sbyte offset, out byte target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (byte)(source & ~(1u << (offset = (sbyte)source.BitScanRandom())));
			return true;
		}



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanRandom(this uint source, out int offset, out uint target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (source & ~(1u << (offset = source.BitScanRandom())));
			return true;
		}

		/// <summary>
		/// Find a non-zero bit from a binary. No guarantee on which bit is found if multiple bits are set.
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <param name="offset">Index of the non-zero bit found</param>
		/// <param name="target">Binary value with the non-zero bit isolated</param>
		/// <returns>True if a non-zero bit is found; otherwise, false</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanRandom(this ulong source, out sbyte offset, out ulong target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			var k = int.Random(0, BitOperations.PopCount(source));
			if(Bmi2.X64.IsSupported)
			{
				target = Bmi2.X64.ParallelBitDeposit(1ul << k, source);
			}
			else
			{
				while (k-- > 0)
					source &= source - 1;
				target = source & (0UL - source);
			}
			offset = (sbyte)BitOperations.TrailingZeroCount(target);
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong BitIsolateRandom(this ulong source)
		{
			if (source == default)
				return default;
			var k = int.Random(0, BitOperations.PopCount(source));
			if (Bmi2.X64.IsSupported)
			{
				return Bmi2.X64.ParallelBitDeposit(1ul << k, source);
			}
			else
			{
				while (k-- > 0)
					source &= source - 1;
				return source & (0UL - source);
			}
		}

		/// <summary>
		/// Find a specific number of non-zero bits from a binary. No guarantee on which bits are found if multiple bits are set.
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <param name="count">Number of non-zero bits to find</param>
		/// <returns>Binary value with the specified number of non-zero bits isolated</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong BitIsolateRandom(this ulong source, int count)
		{
			if (source == default || count <= 0)
				return default;
			int n = BitOperations.PopCount(source);
			if (count >= n)
				return source;

			ulong result = 0;
			for (int i = 0; i < count; i++)
			{
				var j = int.Random(0, n);
				ulong selected;
				if (Bmi2.X64.IsSupported)
				{
					selected = Bmi2.X64.ParallelBitDeposit(1ul << j, source);
				}
				else
				{
					var tempSource = source;
					while (j-- > 0)
						tempSource &= tempSource - 1;
					selected = tempSource & (0UL - tempSource);
				}

				result |= selected;
				source &= ~selected;
				n--;
			}
			return result;
		}

		#endregion

		#region BIT SCAN FORWARD
		/// <summary>
		/// Find the the least significant non zero bit
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <returns>Index of the least significant non zero bit</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte BitScanForward(this byte source) => (sbyte)BitOperations.TrailingZeroCount(source);


		/// <summary>
		/// Find the least significant non zero bit.
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <param name="offset">Index of the least significant non zero bit</param>
		/// <param name="target">Binary value with the least significant non zero bit cleared</param>
		/// <returns>True if a non zero bit is found; otherwise, false</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanForward(this byte source, out sbyte offset, out byte target)
		{

			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (byte)(source & ~(1u << (offset = source.BitScanForward())));
			return true;
		}



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanForward(this ushort source) => BitOperations.TrailingZeroCount(source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanForward(this uint source) => BitOperations.TrailingZeroCount(source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanForward(this ulong source) => BitOperations.TrailingZeroCount(source);



		





		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanForward(this ushort source, out int offset, out ushort target)
		{

			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (ushort)(source & ~(1u << (offset = source.BitScanForward())));
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanForward(this uint source, out int offset, out uint target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}


			offset = BitOperations.TrailingZeroCount(source);
			target = source & ~(1u << offset);
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanForward(this ulong source, out int offset, out ulong target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}


			offset = BitOperations.TrailingZeroCount(source);
			target = source & ~(1ul << offset);
			return true;
		}
		#endregion

		#region BIT SCAN REVERSE
		/// <summary>
		/// Find the the least significant non zero bit
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <returns>Index of the least significant non zero bit</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte BitScanReverse(this byte source) => (sbyte)BitOperations.Log2(source);


		/// <summary>
		/// Find the least significant non zero bit.
		/// </summary>
		/// <param name="source">Binary value to scan</param>
		/// <param name="offset">Index of the least significant non zero bit</param>
		/// <param name="target">Binary value with the least significant non zero bit cleared</param>
		/// <returns>True if a non zero bit is found; otherwise, false</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BitScanReverse(this byte source, out sbyte offset, out byte target)
		{

			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (byte)(source & ~(1u << (offset = source.BitScanReverse())));
			return true;
		}






		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BitScanReverse(this ushort source) => BitOperations.Log2(source);
		public static int BitScanReverse(this uint source) => BitOperations.Log2(source);
		public static int BitScanReverse(this ulong source) => BitOperations.Log2(source);
		public static bool BitScanReverse(this byte source, out int offset, out byte target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (byte)(source & ~(1u << (offset = source.BitScanReverse())));
			return true;
		}
		public static bool BitScanReverse(this ushort source, out int offset, out ushort target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}

			target = (ushort)(source & ~(1u << (offset = source.BitScanReverse())));
			return true;
		}
		public static bool BitScanReverse(this uint source, out int offset, out uint target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}


			offset = BitOperations.Log2(source);
			target = (source & ~(1u << (offset = source.BitScanReverse())));
			return true;
		}
		public static bool BitScanReverse(this ulong source, out int offset, out ulong target)
		{
			if (source == default)
			{
				offset = -1;
				target = default;
				return false;
			}


			offset = BitOperations.Log2(source);
			target = (source & ~(1ul << (offset = source.BitScanReverse())));
			return true;
		}
		#endregion




		extension(ushort source)
		{
			public int CNT
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.PopCount(source);
			}

			/// <summary>
			/// Find the index of the most significant <c>1</c> bit
			/// </summary>
			/// <returns>Index of the upmost <see cref="INumberBase{TSelf}.One">1</see> bit <see cref="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int UpmostNonZeroBit() => source == 0 ? -1 : BitOperations.Log2(source);

			/// <summary>
			/// Find the most significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/>
			/// </summary>
			/// <param name="target">Value from <paramref name="source"/> by isolating only the most significant <c><see cref="Bit.Unit">1₍₂₎</see></c>.</param>
			/// <returns>Offset of the most significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int UpmostNonZeroBit(out ushort target)
			{
				int offset = source.UpmostNonZeroBit();

				target = (ushort)(1u << offset);

				return offset;
			}


			/// <summary>
			/// Find the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/>
			/// </summary>
			/// <returns>Index of the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int LowestNonZeroBit() => source == 0 ? -1 : BitOperations.Log2(source);


			/// <summary>
			/// Find the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c>
			/// </summary>
			/// <param name="target">Value from <paramref name="source"/> by isolating only the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c>.</param>
			/// <returns>Offset of the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int LowestNonZeroBit(out ushort target)
			{
				int offset = source.LowestNonZeroBit();

				target = (ushort)(1u << offset);

				return offset;
			}

			/// <summary>
			/// Find the most significant <c><see cref="Bit.Unit">1₍₂₎</see></c>
			/// </summary>
			/// <returns>Isolated value of the most signicant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ushort IsolateUpmostNonZeroBit() => (ushort)(1u << source.UpmostNonZeroBit());


			/// <summary>
			/// Find the most significant non zero bits by taking not more than a specific number
			/// </summary>
			/// <param name="n">The max number of bits to take</param>
			/// <returns>Isolated <paramref name="n"/> most significant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			public ushort IsolateUpmostNonZeroBits(int n)
			{
				if (n <= 0 || n >= source.CNT)
					return default;

				var (hi, lo) = (default(ushort), source);

				for (int i = 0; i < n; i++)
				{
					var currentHi = lo.IsolateUpmostNonZeroBit();
					hi |= currentHi;
					lo ^= currentHi;
				}

				return hi;
			}




			/// <summary>
			/// Find the less significant <c><see cref="Bit.Unit">1₍₂₎</see></c>
			/// </summary>
			/// <returns>Isolated value of the less signicant <c><see cref="Bit.Unit">1₍₂₎</see></c> in <paramref name="source"/></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ushort IsolateLowestNonZeroBit() => (ushort)(1u << source.LowestNonZeroBit());



			public ushort IsolateLowestNonZeroBits(int n)
			{
				if (n <= 0 || n >= source.CNT)
					return default;

				var (hi, lo) = (source, default(ushort));

				for (int i = 0; i < n; i++)
				{
					var currentLo = lo.IsolateLowestNonZeroBit();
					hi ^= currentLo;
					lo |= currentLo;
				}

				return hi;
			}







			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ushort BitSet(Range range)
			{
				var (k, n) = range.GetOffsetAndLength(16);

				return n > 0 ? (ushort)((~(ushort.MaxValue << n)) << k) : default;
			}





		}

		extension(uint source)
		{
			/// <inheritdoc cref="IBinaryInteger{TSelf}.PopCount(TSelf)"/>
			public int CNT
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.PopCount(source);
			}

			/// <inheritdoc cref="IBinaryInteger{TSelf}.LeadingZeroCount(TSelf)"/>
			public int LZC
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.LeadingZeroCount(source);
			}

			/// <inheritdoc cref="IBinaryInteger{TSelf}.TrailingZeroCount(TSelf)"/>
			public int TZC
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.TrailingZeroCount(source);
			}


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryWriteTo(Span<byte> target) => MemoryMarshal.TryWrite(target, in source);


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void WriteTo(Span<byte> target) => MemoryMarshal.Write(target, in source);



			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public uint HI() => 1u << BitOperations.Log2(source);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public uint LO() => 1u << BitOperations.TrailingZeroCount(source);



			public uint HI(Index index)
			{
				// Merr vlerën numerike të indeksit (p.sh. ^3 bëhet 3, dhe 3 mbetet 3)
				int n = index.Value;
				int totalBits = source.CNT;

				var (hi, lo) = (0u, source);

				if (n <= 0)
				{
					if (index.IsFromEnd) (hi, _) = (lo, hi);
					return hi;
				}

				if (n >= totalBits)
				{
					if (!index.IsFromEnd) (hi, lo) = (lo, hi);
					return hi;
				}


				if (index.IsFromEnd)//LOHI
				{
					(hi, lo) = (lo, hi);
					for (int i = 0; i < n; i++)
					{
						var currentLo = hi.LO();
						lo |= currentLo;
						hi ^= currentLo;
					}
					hi = source ^ lo;

					return hi;
				}



				//HILO
				hi = 0u;
				lo = source;
				for (int i = 0; i < n; i++)
				{
					var currentHi = lo.HI();
					hi |= currentHi;
					lo ^= currentHi;
				}


				return hi;
			}


			public (uint HI, uint LO) HILO(Index index)
			{
				// Merr vlerën numerike të indeksit (p.sh. ^3 bëhet 3, dhe 3 mbetet 3)
				int n = index.Value;
				int totalBits = source.CNT;

				var (hi, lo) = (0u, source);

				if (n <= 0)
				{
					if (index.IsFromEnd) (hi, lo) = (lo, hi);
					return (hi, lo);
				}

				if (n >= totalBits)
				{
					if (!index.IsFromEnd) (hi, lo) = (lo, hi);
					return (hi, lo);
				}


				if (index.IsFromEnd)//LOHI
				{
					(hi, lo) = (lo, hi);
					for (int i = 0; i < n; i++)
					{
						var currentLo = hi.LO();
						lo |= currentLo;
						hi ^= currentLo;
					}
					hi = source ^ lo;

					return (hi, lo);
				}



				//HILO
				hi = 0u;
				lo = source;
				for (int i = 0; i < n; i++)
				{
					var currentHi = lo.HI();
					hi |= currentHi;
					lo ^= currentHi;
				}
				lo = source ^ hi;

				return (hi, lo);
			}



		}

		extension(ulong source)
		{
			/// <inheritdoc cref = "IBinaryInteger{TSelf}.PopCount(TSelf)" />
			public int CNT
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.PopCount(source);
			}

			public int LZC
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.LeadingZeroCount(source);
			}

			public int TZC
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => BitOperations.TrailingZeroCount(source);
			}


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryWriteTo(Span<byte> target) => MemoryMarshal.TryWrite(target, in source);


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void WriteTo(Span<byte> target) => MemoryMarshal.Write(target, in source);





		}




		extension(ulong source)
		{
			/// <summary>
			/// Bit Test.
			/// </summary>
			/// <param name="index"></param>
			/// <returns></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool BitTest(int index) => (source & (1ul << index)) != 0;

			/// <summary>
			/// Bit Clear.
			/// </summary>
			/// <param name="index"></param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ulong BitClear(int index) => source &= ~(1ul << index);

		}

















		/// <summary>
		/// Gjeneron në mënyrë eficiente të gjitha nën-maskat binare me një peshë specifike Hamming.
		/// </summary>
		/// <param name="mask">Vlera origjinale që shërben si maskë.</param>
		/// <param name="weight">Numri i saktë i bitave '1' që duhet të ketë çdo kombinim.</param>
		/// <returns>Një sekuencë IEnumerable me të gjitha kombinimet e vlefshme.</returns>
		public static IEnumerable<ushort> BitMaskChoose(ushort mask, int weight)
		{
			// Nëse pesha e kërkuar është më e madhe se bitat e ndezur në maskë, nuk ka kombinime
			if (weight > BitOperations.PopCount(mask) || weight < 0)
			{
				yield break;
			}

			ushort submask = mask;

			while (submask > 0)
			{
				// Kontrolli i peshës Hamming përmes instruksionit të shpejtë të CPU-së
				if (BitOperations.PopCount(submask) == weight)
				{
					yield return submask; // Kthen vlerën pa krijuar apo ruajtur një listë në memorie
				}

				// Kalimi direkt te nën-maska tjetër valide (Truku i Knuth)
				submask = (ushort)((submask - 1) & mask);
			}

			// Rasti specifik: nëse kërkohet pesha 0, i vetmi kombinim është numri 0
			if (weight == 0)
			{
				yield return 0;
			}
		}
	}


	//tex: Let $X = \{x \mid x \in \mathbb{N}, 0 \leq x \leq 15 \}$. If ${{\cal{X}}=\cal{P}}(X)$ is the power set of $X$:
	//$${\cal{X}} = \{ \binom{X}{k} \mid X\supseteq  \binom{X}{k}  \in {\cal{X}},  0 \leq k \leq 16 \}$$
	//where $X_{(k)}=\binom{X}{k}$ denotes the family of $k$-element subsets of $X$.
	//We are able to represent an integers in the range $[0 \cdots 2^{16}-1]$ as a bit mask in a 16-bit unsigned integer.
	//In terms of bit vectors, $X_k$ is the set of all bit vectors
	//$$\hat{x} = \pmatrix{x_0 \\ x_1 \\ \vdots \\ x_{n-1}}$$
	//such that
	//$$\hat{x} \in X_k \iff \sum_{i=0}^{n-1} x_i = k$$.
	public static class UInt16BitChoose
	{

		extension(Binary<ushort> X)
		{
			public Iterator Choose(int k) => new(X, k);
		}

		extension(ushort X)
		{
			/// <summary>
			/// Returns an iterator that enumerates all valid submasks of a given binary mask with a specified Hamming weight.
			/// </summary>
			/// <param name="weight">The weight of the mask; the number of elements to pick from the source.</param>
			/// <returns>An iterator that enumerates all valid submasks.</returns>
			public Iterator BitChoose(int k) => new(X, k);
		}

		/// <summary>
		/// Iterator of all valid submasks of a given binary mask with a specified Hamming weight.
		/// </summary>
		public readonly struct Iterator : IReadOnlyCollection<ushort>
		{


			public readonly ushort _x;
			private readonly int _k;

			/// <summary>
			/// Initializes a new instance of the <see cref="Iterator"/> struct.
			/// </summary>
			/// <param name="source">The source mask; a binary acting as a set of integer number, where the offset of each bit represents the presence of the corresponding integer.</param> 
			/// <param name="weight">The weight of the mask; the number of elements to pick from the source.</param>
			public Iterator(ushort source, int weight)
			{
				_x = source;
				_k = weight;
			}

			/// <summary>
			/// Enumerates all valid submasks of a given binary mask with a specified Hamming weight.
			/// </summary>
			public struct Enumerator : IEnumerator<ushort>
			{
				private readonly ushort _x;
				private readonly int _n;
				private readonly int _k;
				private ushort _current;

				// BMI2 state
				private uint _bmiIdx, _bmiMax;

				// Fallback state
				private ushort _submask;
				private bool _zeroCase;

				/// <summary>
				/// Initializes a new instance of the <see cref="Enumerator"/> struct.
				/// </summary>
				/// <param name="source">The source mask; a binary acting as a set of integer number, where the offset of each bit represents the presence of the corresponding integer.</param>
				/// <param name="weight">The weight of the mask; the number of elements to pick from the source.</param>
				public Enumerator(Iterator owner)
				{
					(_x, _k) = (owner._x, owner._k);

					_n = BitOperations.PopCount(owner._x);

					if (owner._k < 0 || owner._k > _n) return;
					_zeroCase = (owner._k == 0);

					if (Bmi2.IsSupported)
					{
						_bmiIdx = (1U << owner._k) - 1;
						_bmiMax = _bmiIdx << (_n - owner._k);
					}
					else _submask = owner._x;
				}

				/// <summary>
				/// Gets the current value of the enumerator.
				/// </summary>
				public readonly ushort Current => _current;

				readonly object IEnumerator.Current => Current;

				/// <summary>
				/// Moves to the next valid submask with the specified Hamming weight.
				/// </summary>
				/// <returns>True if the enumerator was successfully advanced to the next submask; false if there are no more submasks.</returns>
				public bool MoveNext()
				{
					if (Bmi2.IsSupported)
					{
						if (_bmiIdx == 0) return false;
						_current = (ushort)Bmi2.ParallelBitDeposit(_bmiIdx, _x);

						if (_bmiIdx == _bmiMax) _bmiIdx = 0;
						else
						{
							uint c = _bmiIdx & (uint)-(int)_bmiIdx;
							uint r = _bmiIdx + c;
							_bmiIdx = (((r ^ _bmiIdx) >> 2) / c) | r;
						}
						return true;
					}

					while (_submask > 0)
					{
						ushort pot = _submask;
						_submask = (ushort)((_submask - 1) & _x);
						if (BitOperations.PopCount(pot) == _k) { _current = pot; return true; }
					}

					if (_zeroCase) { _zeroCase = false; _current = 0; return true; }
					return false;
				}

				/// <summary>
				/// Resets the enumerator to its initial position, which is before the first element in the collection.
				/// </summary>
				public void Reset() => _current = 0;
				readonly void IDisposable.Dispose() { }
			}

			public Enumerator GetEnumerator() => new(this);
			public int Count => Math.Choose(BitOperations.PopCount(_x), _k);
			IEnumerator<ushort> IEnumerable<ushort>.GetEnumerator() => GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}





	public static class UInt32BitChoose
	{
		extension(Binary<uint> X)
		{
			public Collection Choose(int k) => new(X, k);
		}




		public readonly struct Collection(Binary<uint> X, int k) : IReadOnlyCollection<uint>
		{
			public int Count => Math.Choose(BitOperations.PopCount(X), k);
			public Enumerator GetEnumerator() => new(X, k);
			IEnumerator<uint> IEnumerable<uint>.GetEnumerator() => GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}

		public struct Enumerator : IEnumerator<uint>
		{
			private readonly uint _mask;
			private readonly int _weight;
			private uint _current, _bmiIdx, _bmiMax, _submask;
			private bool _zeroCase;

			public Enumerator(uint mask, int weight)
			{
				(_mask, _weight) = (mask, weight);
				int pop = BitOperations.PopCount(mask);
				if (weight < 0 || weight > pop) return;
				_zeroCase = (weight == 0);

				if (Bmi2.IsSupported)
				{
					_bmiIdx = (1U << weight) - 1;
					_bmiMax = _bmiIdx << (pop - weight);
				}
				else _submask = mask;
			}

			public readonly uint Current => _current;
			readonly object IEnumerator.Current => Current;
			public bool MoveNext()
			{
				if (Bmi2.IsSupported)
				{
					if (_bmiIdx == 0) return false;
					_current = Bmi2.ParallelBitDeposit(_bmiIdx, _mask);
					if (_bmiIdx == _bmiMax) _bmiIdx = 0;
					else
					{
						uint c = _bmiIdx & (uint)-(int)_bmiIdx;
						uint r = _bmiIdx + c;
						_bmiIdx = (((r ^ _bmiIdx) >> 2) / c) | r;
					}
					return true;
				}

				while (_submask > 0)
				{
					uint pot = _submask;
					_submask = (_submask - 1) & _mask;
					if (BitOperations.PopCount(pot) == _weight) { _current = pot; return true; }
				}

				if (_zeroCase) { _zeroCase = false; _current = 0; return true; }
				return false;
			}

			public void Reset() => _current = 0;
			readonly void IDisposable.Dispose() { }
		}
	}


	public static class UInt64BitChoose
	{
		extension(Binary<ulong> X)
		{
			public Collection Choose(int k) => new(X, k);
		}






		public readonly struct Collection : IReadOnlyCollection<ulong>
		{

			private readonly ulong _X;
			private readonly int _k;
			public Collection(ulong X, int k) => (_X, _k) = (X, k);


			public int Count => Math.Choose(BitOperations.PopCount(_X), _k);

			public struct Enumerator : IEnumerator<ulong>
			{
				private readonly ulong _X;
				private readonly int _k;
				private ulong _x, _bmiIdx, _bmiMax, _submask;
				private bool _zeroCase;

				public Enumerator(Collection owner)
				{
					(_X, _k) = (owner._X, owner._k);
					int pop = BitOperations.PopCount(_X);
					if (_k < 0 || _k > pop) return;
					_zeroCase = (_k == 0);

					if (Bmi2.X64.IsSupported)
					{
						_bmiIdx = (1UL << _k) - 1UL;
						_bmiMax = _bmiIdx << (pop - _k);
					}
					else _submask = _X;
				}

				public ulong Current => _x;

				object IEnumerator.Current => this.Current;

				public bool MoveNext()
				{
					if (Bmi2.X64.IsSupported)
					{
						if (_bmiIdx == 0) return false;
						_x = Bmi2.X64.ParallelBitDeposit(_bmiIdx, _X);
						if (_bmiIdx == _bmiMax) _bmiIdx = 0;
						else
						{
							ulong c = _bmiIdx & (ulong)-(long)_bmiIdx;
							ulong r = _bmiIdx + c;
							_bmiIdx = (((r ^ _bmiIdx) >> 2) / c) | r;
						}
						return true;
					}

					while (_submask > 0)
					{
						ulong pot = _submask;
						_submask = (_submask - 1) & _X;
						if (BitOperations.PopCount(pot) == _k) { _x = pot; return true; }
					}

					if (_zeroCase) { _zeroCase = false; _x = 0; return true; }
					return false;
				}

				void IDisposable.Dispose() { }
				public void Reset()
				{
					int pop = BitOperations.PopCount(_X);
					if (_k < 0 || _k > pop) return;
					_zeroCase = (_k == 0);

					if (Bmi2.X64.IsSupported)
					{
						_bmiIdx = (1UL << _k) - 1UL;
						_bmiMax = _bmiIdx << (pop - _k);
					}
					else _submask = _X;
				}
			}
			public Enumerator GetEnumerator() => new(this);
			IEnumerator<ulong> IEnumerable<ulong>.GetEnumerator() => this.GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		}
	}
}