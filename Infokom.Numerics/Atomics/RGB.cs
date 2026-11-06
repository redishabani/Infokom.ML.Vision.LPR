//using static Infokom.Numerics.Atomics.FloatingPointConstant;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.XPath;

using static Infokom.Numerics.Extensions.Functions;

namespace Infokom.Numerics.Atomics
{
	public interface IColor
	{
		public float Red { get; }
		public float Green { get; }
		public float Blue { get; }
		public float Alpha { get; }
	}

	

	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct ByteVector4
	{
		[FieldOffset(0)] private readonly uint _data;
		[FieldOffset(0)] private fixed byte _elements[4];

		[FieldOffset(0)] public byte X;
		[FieldOffset(1)] public byte Y;
		[FieldOffset(2)] public byte Z;
		[FieldOffset(3)] public byte W;

		private ByteVector4(uint data) => _data = data;

		private ByteVector4(byte x, byte y, byte z, byte w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		public byte this[int index]
		{
			readonly get => (uint)index < 4 ? _elements[index] : throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be in the range [0, 3].");
			set => _elements[index] = (uint)index < 4 ? value : throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be in the range [0, 3].");
		}




		public static implicit operator ByteVector4(uint data) => new(data);

		public static implicit operator uint(ByteVector4 vector) => vector._data;


		public static readonly ByteVector4 Zeros = 0u;
		public static readonly ByteVector4 UnitX = new(1, 0, 0, 0);
		public static readonly ByteVector4 UnitY = new(0, 1, 0, 0);
		public static readonly ByteVector4 UnitZ = new(0, 0, 1, 0);
		public static readonly ByteVector4 UnitW = new(0, 0, 0, 1);
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct BitMatrix4x8
	{
		[FieldOffset(0)] private readonly uint _data;
		[FieldOffset(0)] private ByteVector4 _bytes;
		[FieldOffset(0)] private BitVector32 _bits;
		[FieldOffset(0)] public InlineArray4<BitVector8> Rows;




		/// <summary>
		/// Access a bit in this matrix by row and column index.
		/// </summary>
		/// <param name="i">The row index (0-3).</param>
		/// <param name="j">The column index (0-7).</param>
		/// <returns>The bit at the specified row and column.</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public Bit this[int i, int j]
		{
			readonly get => i switch
			{
				0 => Rows[0][j],
				1 => Rows[1][j],
				2 => Rows[2][j],
				3 => Rows[3][j],
				_ => throw new ArgumentOutOfRangeException($"(R:{i}, C:{j})")
			};

			set
			{
				switch (i)
				{
					case 0: Rows[0][j] = value; break;
					case 1: Rows[1][j] = value; break;
					case 2: Rows[2][j] = value; break;
					case 3: Rows[3][j] = value; break;
					default: throw new ArgumentOutOfRangeException($"(R:{i}, C:{j})");
				}
			}
		}
	}
}