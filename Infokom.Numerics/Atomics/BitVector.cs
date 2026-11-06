using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Infokom.Numerics.Atomics
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BitVector8 : IEquatable<BitVector8>, IEqualityOperators<BitVector8, BitVector8, bool>, IShiftOperators<BitVector8, int, BitVector8>
	{
		private const byte ZEROS = byte.MinValue;
		private const byte UNITS = byte.MaxValue;
		private byte _bits;	

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private BitVector8(byte bits) => _bits = bits;

		/// <summary>
		/// Gets or sets the value of the bit at the specified index in the <see cref="BitVector"/> instance.
		/// </summary>
		/// <param name="offset">The zero-based index of the bit to get or set.</param>
		/// <returns>The value of the bit at the specified index.</returns>
		/// <remarks>
		/// It is expected that <paramref name="offset"/> is in [0..8] range, where 0 represents the least significant bit (LSB) and 7 represents the most significant bit (MSB), otherwise unexpected behavior may occur.
		/// </remarks>
		public Bit this[int offset]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => (_bits & (1 << offset)) != 0;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _bits = value ? (byte)(_bits | (1 << offset)) : (byte)(_bits & ~(1 << offset));
		}


		public Bit this[Index index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => this[index.GetOffset(8)];

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => this[index.GetOffset(8)] = value;
		}

		public readonly override string ToString() => _bits.ToString("B8");
		public readonly override int GetHashCode() => _bits.GetHashCode();
		public readonly bool Equals(BitVector8 other) => _bits.Equals(other._bits);
		public readonly override bool Equals(object obj) => obj is BitVector8 other && Equals(other);
		public static bool operator ==(BitVector8 v1, BitVector8 v2) => v1._bits == v2._bits;
		public static bool operator !=(BitVector8 v1, BitVector8 v2) => v1._bits != v2._bits;

		/// <summary>
		/// A <see cref="BitVector"/> instance with all bits <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector8 Zeros = new(ZEROS);

		/// <summary>
		/// A <see cref="BitVector"/> instance with all bits <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector8 Units = new(UNITS);

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector"/> instance with the bits shifted to the left by the specified offset.</returns>
		public static BitVector8 operator <<(BitVector8 source, int offset) => new((byte)(source._bits << offset));

		/// <summary>
		/// Performs a bitwise right shift operation on a <see cref="BitVector"/> instance by certain number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector"/> instance with the bits shifted to the right by the specified offset.</returns>
		public static BitVector8 operator >>(BitVector8 source, int offset) => new((byte)(source._bits >> offset));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector"/> instance with the bits shifted to the right by the specified offset.</returns>
		static BitVector8 IShiftOperators<BitVector8, int, BitVector8>.operator >>>(BitVector8 source, int offset) => source >> offset;



		public static implicit operator ushort(BitVector8 vector) => vector._bits;
		public static implicit operator BitVector8(byte value) => new(value);

		public static explicit operator BitVector8(ushort value) => new((byte)value);
		public static explicit operator checked BitVector8(ushort value) => new(checked((byte)value));

		public static explicit operator BitVector8(uint value) => new((byte)value);
		public static explicit operator checked BitVector8(uint value) => new(checked((byte)value));

		public static explicit operator BitVector8(ulong value) => new((byte)value);
		public static explicit operator checked BitVector8(ulong value) => new(checked((byte)value));
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct BitVector16 : IEquatable<BitVector16>, IEqualityOperators<BitVector16, BitVector16, bool>, IShiftOperators<BitVector16, int, BitVector16>
	{
		private const ushort ZEROS = ushort.MinValue;
		private const ushort UNITS = ushort.MaxValue;

		[FieldOffset(0)] private ushort _bits;

		[FieldOffset(0)] public BitVector8 Lower;
		[FieldOffset(1)] public BitVector8 Upper;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private BitVector16(ushort bits) => _bits = bits;

		/// <summary>
		/// Gets or sets the value of the bit at the specified offset.
		/// </summary>
		/// <param name="offset">The offset of the target bit.</param>
		/// <returns>The value of the bit at the specified offset.</returns>
		/// <remarks>
		/// It is expected an <paramref name="offset"/> from 0 (LSB) to 15 (MSB); no guarantee about the behavior out of these bounds.
		/// </remarks>
		public Bit this[int offset]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => (uint)offset switch
			{
				<  8 => Lower[offset    ],
				< 16 => Upper[offset - 8],
				_ => Bit.Zero,
			};

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch((uint)offset)
				{
					case <  8: Lower[offset    ] = value; break;
					case < 16: Upper[offset - 8] = value; break;
					default: break;
				}
			}
		}


		public readonly override string ToString()
		{
			var source = _bits.ToString("B16").AsSpan();
			return string.Create(16 * 3, source, (target, source) =>
			{
				target.Fill(' ');
				target[0] = '[';
				for (int i = 0; i < source.Length; i++)
				{
					target[3 * i + 1] = source[^(i + 1)];
				}
				target[^1] = ']';
			});
		}

		public readonly override int GetHashCode() => _bits.GetHashCode();
		public readonly bool Equals(BitVector16 other) => _bits.Equals(other._bits);
		public readonly override bool Equals(object obj) => obj is BitVector16 other && Equals(other);
		public static bool operator ==(BitVector16 v1, BitVector16 v2) => v1._bits == v2._bits;
		public static bool operator !=(BitVector16 v1, BitVector16 v2) => v1._bits != v2._bits;

		/// <summary>
		/// A <see cref="BitVector16">16-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector16 Zeros = new(ZEROS);

		/// <summary>
		/// A <see cref="BitVector16">16-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector16 Units = new(UNITS);

		/// <summary>
		/// A <see cref="BitVector16">16-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector16 Φ = Zeros;


		/// <summary>
		/// A <see cref="BitVector16">16-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector16 Ω = ~Φ;


		public static BitVector16 Create(Func<int, Bit> source)
		{
			var result = new BitVector16();
			for (int i = 0; i < 16; i++)
				if (source(i))
					result._bits |= (ushort)(1 << i);
			return result;
		}

		public static BitVector16 Unit(int index = 0) => new((ushort)(1 << index));

		public static BitVector16 operator ~(BitVector16 vector) => new((ushort)(~vector._bits));
		public static BitVector16 operator &(BitVector16 v1, BitVector16 v2) => new((ushort)(v1._bits & v2._bits));
		public static BitVector16 operator |(BitVector16 v1, BitVector16 v2) => new((ushort)(v1._bits | v2._bits));
		public static BitVector16 operator ^(BitVector16 v1, BitVector16 v2) => new((ushort)(v1._bits ^ v2._bits));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector16"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector16"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector16"/> instance with the bits shifted to the left by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≪ k = [xxxxxxxxxxx00000]</c>
		/// </remarks>
		public static BitVector16 operator <<(BitVector16 source, int offset) => new((ushort)(source._bits << offset));

		/// <summary>
		/// Performs a bitwise right shift operation on a <see cref="BitVector16"/> instance by certain number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector16"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector16"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≫ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		public static BitVector16 operator >>(BitVector16 source, int offset) => new((ushort)(source._bits >> offset));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector16"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector16"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector16"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ⋙ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		static BitVector16 IShiftOperators<BitVector16, int, BitVector16>.operator >>>(BitVector16 source, int offset) => source >> offset;


		public static implicit operator BitVector16(bool source) => source ? Units : Zeros;


		public static implicit operator ushort(BitVector16 vector) => vector._bits;
		public static implicit operator BitVector16(ushort value) => new(value);
		public static explicit operator BitVector16(uint value) => new((ushort)value);
		public static explicit operator checked BitVector16(uint value) => new(checked((ushort)value));
		public static explicit operator BitVector16(ulong value) => new((ushort)value);
		public static explicit operator checked BitVector16(ulong value) => new(checked((ushort)value));


		/// <summary>
		/// True operator for <see cref="BitVector16">16-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in <paramref name="vector"/> is exactly 16, and false otherwise.</returns>
		public static bool operator true(BitVector16 vector) => vector._bits == 0b__1111_1111_1111_1111;

		/// <summary>
		/// False operator for <see cref="BitVector16">16-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in the <paramref name="vector"/> is exactly 0, and false otherwise.</returns>
		public static bool operator false(BitVector16 vector) => vector._bits == 0b__0000_0000_0000_0000;
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct BitVector32 : IEquatable<BitVector32>, IEqualityOperators<BitVector32, BitVector32, bool>, IShiftOperators<BitVector32, int, BitVector32>
	{
		private const uint ZEROS = uint.MinValue;
		private const uint UNITS = uint.MaxValue;

		[FieldOffset(0)] private uint _bits;

		[FieldOffset(0)] public BitVector16 Lower;
		[FieldOffset(2)] public BitVector16 Upper;



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private BitVector32(uint bits) => _bits = bits;

		/// <summary>
		/// Gets or sets the value of the bit at the specified offset.
		/// </summary>
		/// <param name="offset">The offset of the target bit.</param>
		/// <returns>The value of the bit at the specified offset.</returns>
		/// <remarks>
		/// It is expected an <paramref name="offset"/> from 0 (LSB) to 15 (MSB); no guarantee about the behavior out of these bounds.
		/// </remarks>
		public Bit this[int offset]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			//readonly get => (_bits & (1u << offset)) != 0;
			readonly get => (uint)offset switch
			{
				< 16 => Lower[offset      ],
				< 32 => Upper[offset - 16],
				_ => Bit.Zero,
			};

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch ((uint)offset)
				{
					case < 16: Lower[offset] = value; break;
					case < 32: Upper[offset - 16] = value; break;
					default: break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value of the bit at the specified index in the <see cref="BitVector32"/> instance.
		/// </summary>
		/// <param name="index">The zero-based index of the bit to get or set.</param>
		/// <returns>The value of the bit at the specified index.</returns>
		/// <remarks>
		/// It is expected an <paramref name="index"/> from 0 (LSB) to 15 (MSB); no guarantee about the behavior out of these bounds.
		/// </remarks>
		public Bit this[Index index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => this[index.GetOffset(32)];

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => this[index.GetOffset(32)] = value;
		}


		public readonly override string ToString()
		{
			var source = _bits.ToString("B32").AsSpan();
			return string.Create(32 * 3, source, (target, source) =>
			{
				target.Fill(' ');
				target[0] = '[';
				for (int i = 0; i < source.Length; i++)
				{
					target[3 * i + 1] = source[^(i + 1)];
				}
				target[^1] = ']';
			});
		}

		public readonly override int GetHashCode() => _bits.GetHashCode();
		public readonly bool Equals(BitVector32 other) => _bits.Equals(other._bits);
		public readonly override bool Equals(object obj) => obj is BitVector32 other && Equals(other);
		public static bool operator ==(BitVector32 v1, BitVector32 v2) => v1._bits == v2._bits;
		public static bool operator !=(BitVector32 v1, BitVector32 v2) => v1._bits != v2._bits;

		/// <summary>
		/// A <see cref="BitVector32">32-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector32 Zeros = new(ZEROS);

		/// <summary>
		/// A <see cref="BitVector32">32-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector32 Units = new(UNITS);

		/// <summary>
		/// A <see cref="BitVector32">32-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector32 Φ = Zeros;


		/// <summary>
		/// A <see cref="BitVector32">32-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector32 Ω = ~Φ;


		public static BitVector32 Create(Func<int, Bit> source)
		{
			var result = new BitVector32();
			for (int i = 0; i < 32; i++)
				if (source(i))
					result._bits |= (uint)(1 << i);
			return result;
		}

		public static BitVector32 Unit(int index = 0) => new((uint)(1 << index));

		public static BitVector32 operator ~(BitVector32 vector) => new((uint)(~vector._bits));
		public static BitVector32 operator &(BitVector32 v1, BitVector32 v2) => new((uint)(v1._bits & v2._bits));
		public static BitVector32 operator |(BitVector32 v1, BitVector32 v2) => new((uint)(v1._bits | v2._bits));
		public static BitVector32 operator ^(BitVector32 v1, BitVector32 v2) => new((uint)(v1._bits ^ v2._bits));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector32"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector32"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector32"/> instance with the bits shifted to the left by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≪ k = [xxxxxxxxxxx00000]</c>
		/// </remarks>
		public static BitVector32 operator <<(BitVector32 source, int offset) => new((uint)(source._bits << offset));

		/// <summary>
		/// Performs a bitwise right shift operation on a <see cref="BitVector32"/> instance by certain number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector32"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector32"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≫ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		public static BitVector32 operator >>(BitVector32 source, int offset) => new((uint)(source._bits >> offset));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector32"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector32"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector32"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ⋙ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		static BitVector32 IShiftOperators<BitVector32, int, BitVector32>.operator >>>(BitVector32 source, int offset) => source >> offset;


		public static implicit operator BitVector32(bool source) => source ? Units : Zeros;


		public static implicit operator uint(BitVector32 vector) => vector._bits;
		public static implicit operator BitVector32(uint value) => new(value);


		/// <summary>
		/// True operator for <see cref="BitVector32">32-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in <paramref name="vector"/> is exactly 32, and false otherwise.</returns>
		public static bool operator true(BitVector32 vector) => vector._bits == 0b__1111_1111_1111_1111;

		/// <summary>
		/// False operator for <see cref="BitVector32">32-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in the <paramref name="vector"/> is exactly 0, and false otherwise.</returns>
		public static bool operator false(BitVector32 vector) => vector._bits == 0b__0000_0000_0000_0000;
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct BitVector64 : IEquatable<BitVector64>, IEqualityOperators<BitVector64, BitVector64, bool>, IShiftOperators<BitVector64, int, BitVector64>
	{
		private const ulong ZEROS = ulong.MinValue;
		private const ulong UNITS = ulong.MaxValue;

		[FieldOffset(0)] private ulong _bits;

		[FieldOffset(0)] public BitVector32 Lower;
		[FieldOffset(4)] public BitVector32 Upper;



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private BitVector64(ulong bits) => _bits = bits;

		/// <summary>
		/// Gets or sets the value of the bit at the specified offset.
		/// </summary>
		/// <param name="offset">The offset of the target bit.</param>
		/// <returns>The value of the bit at the specified offset.</returns>
		/// <remarks>
		/// It is expected an <paramref name="offset"/> from 0 (LSB) to 15 (MSB); no guarantee about the behavior out of these bounds.
		/// </remarks>
		public Bit this[int offset]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			//readonly get => (_bits & (1u << offset)) != 0;
			readonly get => (ulong)offset switch
			{
				< 32 => (Bit)Lower.Upper.Upper[offset     ],
				< 64 => (Bit)Upper.Upper.Upper[offset - 32],
				_ => Bit.Zero,
			};

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch ((ulong)offset)
				{
					case < 32: Lower.Upper.Upper[offset     ] = value; break;
					case < 64: Upper.Upper.Upper[offset - 32] = value; break;
					default: break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value of the bit at the specified index in the <see cref="BitVector64"/> instance.
		/// </summary>
		/// <param name="index">The zero-based index of the bit to get or set.</param>
		/// <returns>The value of the bit at the specified index.</returns>
		/// <remarks>
		/// It is expected an <paramref name="index"/> from 0 (LSB) to 15 (MSB); no guarantee about the behavior out of these bounds.
		/// </remarks>
		public Bit this[Index index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get => this[index.GetOffset(64)];

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => this[index.GetOffset(64)] = value;
		}


		public readonly override string ToString()
		{
			var source = _bits.ToString("B64").AsSpan();
			return string.Create(64 * 3, source, (target, source) =>
			{
				target.Fill(' ');
				target[0] = '[';
				for (int i = 0; i < source.Length; i++)
				{
					target[3 * i + 1] = source[^(i + 1)];
				}
				target[^1] = ']';
			});
		}

		public readonly override int GetHashCode() => _bits.GetHashCode();
		public readonly bool Equals(BitVector64 other) => _bits.Equals(other._bits);
		public readonly override bool Equals(object obj) => obj is BitVector64 other && Equals(other);
		public static bool operator ==(BitVector64 v1, BitVector64 v2) => v1._bits == v2._bits;
		public static bool operator !=(BitVector64 v1, BitVector64 v2) => v1._bits != v2._bits;

		/// <summary>
		/// A <see cref="BitVector64">64-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector64 Zeros = new(ZEROS);

		/// <summary>
		/// A <see cref="BitVector64">64-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector64 Units = new(UNITS);

		/// <summary>
		/// A <see cref="BitVector64">64-bit vector</see> with all elements set to <see cref="Bit.Zero">0</see>.
		/// </summary>
		public static readonly BitVector64 Φ = Zeros;


		/// <summary>
		/// A <see cref="BitVector64">64-bit vector</see> instance with all elements set to <see cref="Bit.Unit">1</see>.
		/// </summary>
		public static readonly BitVector64 Ω = ~Φ;


		public static BitVector64 Create(Func<int, Bit> source)
		{
			var result = new BitVector64();
			for (int i = 0; i < 64; i++)
				if (source(i))
					result._bits |= 1ul << i;
			return result;
		}

		public static BitVector64 Unit(int index = 0) => new((1ul << index));

		public static BitVector64 operator ~(BitVector64 vector) => new((~vector._bits));
		public static BitVector64 operator &(BitVector64 v1, BitVector64 v2) => new((v1._bits & v2._bits));
		public static BitVector64 operator |(BitVector64 v1, BitVector64 v2) => new((v1._bits | v2._bits));
		public static BitVector64 operator ^(BitVector64 v1, BitVector64 v2) => new((v1._bits ^ v2._bits));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector64"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector64"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector64"/> instance with the bits shifted to the left by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≪ k = [xxxxxxxxxxx00000]</c>
		/// </remarks>
		public static BitVector64 operator <<(BitVector64 source, int offset) => new((ulong)(source._bits << offset));

		/// <summary>
		/// Performs a bitwise right shift operation on a <see cref="BitVector64"/> instance by certain number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector64"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector64"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ≫ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		public static BitVector64 operator >>(BitVector64 source, int offset) => new((ulong)(source._bits >> offset));

		/// <summary>
		/// Performs a bitwise left shift operation on the specified <see cref="BitVector64"/> instance by the specified number of bits.
		/// </summary>
		/// <param name="source">The <see cref="BitVector64"/> instance to shift.</param>
		/// <param name="offset">The number of bits to shift.</param>
		/// <returns>A new <see cref="BitVector64"/> instance with the bits shifted to the right by the specified offset.</returns>
		/// <remarks>
		/// <c>[xxxxxxxxxxxxxxxx] ⋙ k = [00000xxxxxxxxxxx]</c>
		/// </remarks>
		static BitVector64 IShiftOperators<BitVector64, int, BitVector64>.operator >>>(BitVector64 source, int offset) => source >> offset;


		public static implicit operator BitVector64(bool source) => source ? Units : Zeros;


		public static implicit operator ulong(BitVector64 vector) => vector._bits;
		public static implicit operator BitVector64(ulong value) => new(value);


		/// <summary>
		/// True operator for <see cref="BitVector64">64-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in <paramref name="vector"/> is exactly 64, and false otherwise.</returns>
		public static bool operator true(BitVector64 vector) => vector._bits == 0b__1111_1111_1111_1111;

		/// <summary>
		/// False operator for <see cref="BitVector64">64-bit vectors</see>
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>true if the sum of all elements in the <paramref name="vector"/> is exactly 0, and false otherwise.</returns>
		public static bool operator false(BitVector64 vector) => vector._bits == 0b__0000_0000_0000_0000;
	}

	public static class BitVector
	{
		extension(in byte source)
		{
			public ref BitVector8 Bits => ref Unsafe.As<byte, BitVector8>(ref Unsafe.AsRef(in source));
		}

		extension(in ushort source)
		{
			public ref BitVector16 Bits => ref Unsafe.As<ushort, BitVector16>(ref Unsafe.AsRef(in source));
		}

		extension(in uint source)
		{
			public ref BitVector32 Bits => ref Unsafe.As<uint, BitVector32>(ref Unsafe.AsRef(in source));
		}

		extension(in ulong source)
		{
			public ref BitVector64 Bits => ref Unsafe.As<ulong, BitVector64>(ref Unsafe.AsRef(in source));
		}
	}
}