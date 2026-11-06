//using static Infokom.Numerics.Atomics.FloatingPointConstant;
using System.Drawing;
using System.Numerics;

namespace Infokom.Numerics.Atomics
{
	public readonly struct RYB : IEquatable<RYB>
	{
		private readonly byte _value;

		public RYB(byte value) => _value = value;

		public decimal R => _value <= 85 ? (85m - _value) / 85m : _value < 170 ? 0 : (_value - 170m) / 86m;

		public decimal Y => _value <= 85 ? _value / 85.0m : _value < 170 ? (170m - _value) / 85m : 0m;

		public decimal B => _value >= 170 ? (256m - _value) / 86m : 0m;

		public decimal Normalized => _value / 256m;

		public static RYB FromNormalized(decimal value)
		{
			value = Math.Clamp(value, 0, 1);

			if (value == 1m)
				return new RYB(0);

			return new RYB((byte)(value * 256m));
		}

		public Color ToColor()
		{
			int r = (int)((R + Y) * 255m);
			int g = (int)(Y * 255m);
			int b = (int)(B * 255m);

			return Color.FromArgb(r, g, b);
		}

		public bool Equals(RYB other)
			=> _value == other._value;

		public override bool Equals(object obj)
			=> obj is RYB other && Equals(other);

		public override int GetHashCode()
			=> _value;

		public static bool operator ==(RYB left, RYB right)
			=> left._value == right._value;

		public static bool operator !=(RYB left, RYB right)
			=> left._value != right._value;
	}

}
