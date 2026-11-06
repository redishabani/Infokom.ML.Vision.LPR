namespace Infokom.Numerics.Atomics
{
	public readonly partial struct Bit
	{
		private const bool ZERO = false;
		private const bool UNIT = true;

		private readonly bool _value;

		public Bit(bool value) => _value = value;

		/// <summary>
		/// 0₍₂₎
		/// </summary>
		public static readonly Bit Zero = new(ZERO);

		/// <summary>
		/// 1₍₂₎
		/// </summary>
		public static readonly Bit Unit = new(UNIT);

		public static readonly Bit Φ = Zero;

		public static readonly Bit Ω = Unit;

		public static bool operator true(Bit bit) => bit._value;
		public static bool operator false(Bit bit) => !bit._value;

		public static implicit operator bool(Bit source) => source._value;
		public static implicit operator Bit(bool source) => new(source);
		public static implicit operator int(Bit source) => source ? 1 : 0;
		//public static explicit operator Bit(int source) => new(source != 0);
		//public static explicit operator checked Bit(int value) => value is 0 or 1 ? new(value != 0) : throw new OverflowException();
		public static implicit operator Bit(int value) => value is 0 or 1 ? new(value != 0) : throw new OverflowException();


		public static int operator +(Bit x) => x ? +1 : 0;
		public static int operator -(Bit x) => x ? -1 : 0;
		public static int operator -(Bit a, int b) => a ? 1 - b : -b;
		public static int operator +(Bit a, int b) => a ? 1 + b :  b;
		public static int operator +(Bit x, Bit y) => x ? 1 : 0 + (y ? 1 : 0);
		public static int operator -(Bit x, Bit y) => x ? 1 - (y ? 1 : 0) : 0 - (y ? 1 : 0);
	}
}