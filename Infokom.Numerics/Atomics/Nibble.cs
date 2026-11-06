using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Infokom.Numerics.Atomics
{
	public readonly partial struct Nibble
	{
		private const byte MASK = 0XFF;
		private const byte MIN_VALUE = 0x0;
		private const byte MAX_VALUE = 0xF;

		private readonly byte _data;

		private Nibble(byte data) => _data = data;

		public static implicit operator uint(Nibble source) => source._data;
		public static explicit operator Nibble(uint source) => new((byte)(source & MASK));
		public static explicit operator checked Nibble(uint source)
		{
			if(source is > MAX_VALUE)
				throw new OverflowException();
			return new((byte)source);
		}




		public static readonly Nibble Zero;
		public static readonly Nibble Unit = (Nibble)0x01u;

		public static Nibble MinValue { get; } = (Nibble)MIN_VALUE;
		public static Nibble MaxValue { get; } = (Nibble)MAX_VALUE;
	}


}
