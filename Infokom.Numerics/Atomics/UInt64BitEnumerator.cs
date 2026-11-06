using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Atomics
{
	public ref struct UInt64BitEnumerator
	{
		private ulong _bits;
		private int _current;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UInt64BitEnumerator(ulong source)
		{
			_bits = source;
			_current = -1;
		}

		// Duhet të kthejë vetveten për të qenë e vlefshme për foreach
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly UInt64BitEnumerator GetEnumerator() => this;

		// Metoda kryesore e lëvizjes që përdor BitOperations
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool MoveNext()
		{
			if (_bits == 0) return false;

			_current = BitOperations.TrailingZeroCount(_bits);
			_bits &= _bits - 1; // Heq bitin më pak të rëndësishëm (Brian Kernighan's algorithm)
			return true;
		}

		// Kthen bitin aktual
		public readonly int Current
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _current;
		}
	}
}
