using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Atomics
{
	public unsafe readonly partial struct Bit
	{
		/// <summary>
		/// Represents a vector of bits backed by a binary integer type.
		/// </summary>
		/// <typeparam name="TBinary">The underlying binary integer type.</typeparam>
		public struct Vector<TBinary> : IReadOnlyList<Bit> where TBinary : unmanaged, IBinaryInteger<TBinary>
		{
			/// <summary>
			/// Gets the number of bits in the vector, which is equal to the number of bits in the underlying binary integer type.
			/// </summary>
			public static readonly sbyte Count = (sbyte)(sizeof(TBinary) * 8);

			private TBinary _value;

			/// <summary>
			/// Initializes a new instance of the <see cref="Vector{TBinary}"/> struct with the specified binary integer value.
			/// </summary>
			/// <param name="value">The binary integer value to initialize the vector with.</param>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private Vector(TBinary value) => _value = value;

			/// <summary>
			/// Gets or sets the bit at the specified index.
			/// </summary>
			/// <param name="index">The zero-based index of the bit to get or set.</param>
			/// <returns>The value of the bit at the specified index.</returns>
			public Bit this[int index]
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get => (_value & (TBinary.One << index)) != TBinary.Zero;

				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set => _value = value ? _value | (TBinary.One << index) : _value & ~(TBinary.One << index);
			}

			public static implicit operator Vector<TBinary>(TBinary value) => new(value);
			public static implicit operator TBinary(Vector<TBinary> vector) => vector._value;












			/// <summary>
			/// Enumerates the bits in a <see cref="Vector{TBinary}"/>.
			/// </summary>
			public struct Enumerator : IEnumerator<Bit>
			{
				private readonly TBinary _bits;
				private int _offset;

				internal Enumerator(TBinary value)
				{
					_bits = value;
					_offset = -1;
				}

				/// <summary>
				/// Gets the current bit in the enumeration.
				/// </summary>
				public readonly Bit Current
				{
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					get => (_bits & (TBinary.One << _offset)) != TBinary.Zero;
				}

				readonly object IEnumerator.Current => Current;

				/// <summary>
				/// Moves to the next bit in the enumeration.
				/// </summary>
				/// <returns><c>true</c> if the enumerator was successfully advanced to the next bit; <c>false</c> if the enumerator has passed the end of the collection.</returns>
				public bool MoveNext() => ++_offset < Count;

				/// <summary>
				/// Resets the enumerator to its initial position, which is before the first bit in the collection.
				/// </summary>
				public void Reset() => _offset = -1;
				readonly void IDisposable.Dispose() { }
			}
		
			/// <summary>
			/// Returns an enumerator that iterates through the bits in the vector.
			/// </summary>
			/// <returns></returns>
			public readonly Enumerator GetEnumerator() => new(_value);
			readonly int IReadOnlyCollection<Bit>.Count => Count;
			readonly IEnumerator<Bit> IEnumerable<Bit>.GetEnumerator() => throw new NotImplementedException();
			readonly IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
			
		}
	}
}