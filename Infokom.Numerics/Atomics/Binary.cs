using Infokom.Numerics.Extensions;

using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

using static Infokom.Numerics.Extensions.Functions;

namespace Infokom.Numerics.Atomics
{
	public readonly partial struct Binary<TData> where TData : unmanaged, IBinaryInteger<TData>, IUnsignedNumber<TData>
	{
		private readonly TData _bits;

		private Binary(TData bits) => _bits = bits;

		public bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => TData.IsZero(_bits);
		}

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => int.CreateChecked(TData.PopCount(_bits));
		}


		public static readonly int SIZE = Unsafe.SizeOf<TData>() * 8;

		public static readonly Binary<TData> Φ = default;
		public static readonly Binary<TData> Ω = new(TData.AllBitsSet);





		public static implicit operator TData(Binary<TData> source) => source._bits;



	}

	public static partial class Binary
	{
		
		
		


		public static Iterator Combinations(ulong source, int k) =>new(source, k);


		public readonly struct Iterator : IReadOnlyCollection<ulong>
		{
			private readonly ulong _bits;
			private readonly int _k;


			public Iterator(ulong bits, int k)
			{
				_bits = bits;
				_k = k;
			}


			public static Iterator Create(ulong bits, int k) => new(bits, k);





			public Enumerator GetEnumerator() => new(_bits, _k);

			public int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => Math.Choose(BitOperations.PopCount(_bits), _k);
			}

			IEnumerator<ulong> IEnumerable<ulong>.GetEnumerator() => this.GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

			public struct Enumerator : IEnumerator<ulong>
			{
				private readonly ulong _source;
				private readonly ulong _limit;
				private readonly int _n;
				private ulong _mask;
				private ulong _current;
				private bool _isFirst;

				public Enumerator(ulong source, int k)
				{
					if (k is <= 0 or > 64)
					{
						this = default;
						return;
					}

					_n = BitOperations.PopCount(source);
					if (_n < k)
					{
						this = default;
						return;
					}

					if (!Bmi2.X64.IsSupported)
					{
						throw new PlatformNotSupportedException("BMI2 instruction set (X64) is required for this operation.");
					}

					_source = source;
					_mask = (1UL << k) - 1;
					_limit = _n == 64 ? ulong.MaxValue : (1UL << _n);
					_current = 0;
					_isFirst = true; // Flag to ensure we don't skip the very first combination
				}

				// Required pattern matching for duck-typed foreach loops
				public readonly ulong Current => _current;

				readonly object IEnumerator.Current => this.Current;


				public bool MoveNext()
				{
					// Guard check for invalid state or completed sequences
					if (_mask == 0 || (_n < 64 && _mask >= _limit))
					{
						return false;
					}

					if (_isFirst)
					{
						_isFirst = false;
						_current = Bmi2.X64.ParallelBitDeposit(_mask, _source);
						return true;
					}

					// Advance Gosper's Hack
					ulong c = _mask & (ulong)-(long)_mask;
					ulong r = _mask + c;

					if (r == 0) // Upper boundary overflow limit hit
					{
						_mask = 0;
						return false;
					}

					_mask = (((r ^ _mask) >> 2) / c) | r;

					// Verify bounds after advancement
					if (_n < 64 && _mask >= _limit)
					{
						return false;
					}

					_current = Bmi2.X64.ParallelBitDeposit(_mask, _source);
					return true;
				}

				public void Reset() => throw new NotSupportedException();

				readonly void IDisposable.Dispose() { }
			}
		}


		public static Iterator BitCombo(this ulong source, int k) => Iterator.Create(source, k);
	}
}