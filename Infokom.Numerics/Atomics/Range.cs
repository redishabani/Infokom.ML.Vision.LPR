using System.Numerics;
using System.Runtime.InteropServices;

namespace Infokom.Numerics.Atomics
{
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Range<Tx> where Tx : unmanaged, INumber<Tx>, IMinMaxValue<Tx>
	{
		private readonly Tx _low;
		private readonly Tx _upp;

		private Range(Point<Tx> low, Point<Tx> upp) => (_low, _upp) = (low, upp);

		public Point<Tx> LowerBound => _low;

		public Point<Tx> UpperBound => _upp;

		/// <summary>
		/// Exclude from this range all values less than a given lower bound.
		/// </summary>
		/// <param name="low"></param>
		/// <returns>{x ∈ <see langword="this"/> | x ⩾ x' }, where x' = <paramref name="low"/></returns>
		public Range<Tx> Above(Point<Tx> low) => new(low, this.UpperBound);

		/// <summary>
		/// Exclude from this range all values greater than a given upper bound.
		/// </summary>
		/// <param name="upp"></param>
		/// <returns>{x ∈ <see langword="this"/> | x ⩽ x' }, where x' = <paramref name="upp"/></returns>
		public Range<Tx> Below(Point<Tx> upp) => new(this.LowerBound, upp);

		public bool IsEmpty => LowerBound == UpperBound;

		/// <summary>
		/// [<see cref="Point{Tx, Ty, Tz}.Zeros">0³</see> ... <see cref="Point{Tx, Ty, Tz}.Units">0³</see>]
		/// </summary>
		public static readonly Range<Tx> Zero;

		/// <summary>
		/// [<see cref="Point{Tx, Ty, Tz}.Zeros">0³</see> - <see cref="Point{Tx, Ty, Tz}.Units">1³</see>]
		/// </summary>
		public static readonly Range<Tx> Unit = new(Point<Tx>.Zero, Point<Tx>.Unit);

		public static readonly Range<Tx> Ω = new(new(Tx.MinValue), new(Tx.MaxValue));
	}

	public readonly struct Range<Tx, Ty>
		where Tx : unmanaged, INumber<Tx>, IMinMaxValue<Tx>
		where Ty : unmanaged, INumber<Ty>, IMinMaxValue<Ty>
	{

		private Range(Point<Tx, Ty> low, Point<Tx, Ty> upp) => (this.LowerBound, this.UpperBound) = (low, upp);

		public Point<Tx, Ty> LowerBound { get; }

		public Point<Tx, Ty> UpperBound { get; }

		public static readonly Range<Tx, Ty> Zero;

		public static readonly Point<Tx, Ty> LOWEST = new(Tx.MinValue, Ty.MinValue);
		public static readonly Point<Tx, Ty> UPMOST = new(Tx.MaxValue, Ty.MaxValue);

		public Range<Tx, Ty> Below(Tx x, Ty y) => Below((x, y));
		public Range<Tx, Ty> Below(Point<Tx, Ty> point)
		{
			var (x0, y0) = this.LowerBound;
			var (x1, y1) = this.UpperBound;
			var (x, y) = point;

			x1 = Tx.Min(x1, x);
			y1 = Ty.Min(y1, y);

			return new(new(x0, y0), new(x1, y1));
		}

		public Range<Tx, Ty> Above(Tx x, Ty y) => Above((x, y));
		public Range<Tx, Ty> Above(Point<Tx, Ty> point) => new(point, UPMOST);


		public static Range<Tx, Ty> operator &(Range<Tx, Ty> a, Range<Tx, Ty> b) => new(
			low: new(Tx.Max(a.LowerBound.X, b.LowerBound.X), Ty.Max(a.LowerBound.Y, b.LowerBound.Y)),
			upp: new(Tx.Min(a.UpperBound.X, b.UpperBound.X), Ty.Min(a.UpperBound.Y, b.UpperBound.Y))
		);

	}

	public readonly struct Range<Tx, Ty, Tz> 
		where Tx : unmanaged, INumber<Tx>
		where Ty : unmanaged, INumber<Ty>
		where Tz : unmanaged, INumber<Tz>
	{

		private Range(Point<Tx, Ty, Tz> low, Point<Tx, Ty, Tz> upp) => (this.LowerBound, this.UpperBound) = (low, upp);

		public Point<Tx, Ty, Tz> LowerBound { get; }

		public Point<Tx, Ty, Tz> UpperBound { get; }

		public bool IsEmpty => this.LowerBound == this.UpperBound;
	}



	public static class Range1
	{
		extension(Range)
		{
			public static Range<int> X(int min, int max) => Range<int>.Ω;
		}
	}
}
