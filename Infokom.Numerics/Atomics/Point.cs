using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Infokom.Numerics.Atomics
{
	


	public interface IPoint<Tx, Ty> where Tx : unmanaged where Ty : unmanaged
	{
		public Tx X { get; }

		public Ty Y { get; }
	}

	public interface IPunctiform<TPoint> where TPoint : IPunctiform<TPoint>
	{
		public static abstract TPoint Midpoint(TPoint a, TPoint b);
	}

	public interface IPunctiform<TPoint, Tx, Ty> where TPoint : IPunctiform<TPoint, Tx, Ty> where Tx : unmanaged where Ty : unmanaged
	{
		public Tx X { get; }
		public Ty Y { get; }



		public static abstract implicit operator TPoint(Point<Tx, Ty> source);
		public static abstract implicit operator Point<Tx, Ty>(TPoint source);
	}





	#region p = ⟨x⟩
	[StructLayout(LayoutKind.Sequential)]
	public readonly record struct Point<Tx>(Tx X) where Tx : unmanaged, INumber<Tx>
	{

		public static readonly Point<Tx> Zero = new(Tx.Zero);
		public static readonly Point<Tx> Unit = new(Tx.One);


		public static implicit operator Point<Tx>(Tx source) => new(source);
		public static implicit operator Tx(Point<Tx> source) => source.X;
	}

	public static class Point1
	{
		extension<T>(Point<T>) where T : unmanaged, INumber<T>
		{
			public static Point<T> Zero => new(T.Zero);
			public static Point<T> Unit => new(T.One);
		}
	}
	#endregion

	#region p = ⟨x, y⟩
	[StructLayout(LayoutKind.Sequential)]
	public readonly record struct Point<Tx, Ty>(Tx X, Ty Y) where Tx : unmanaged where Ty : unmanaged
	{


		public static implicit operator Point<Tx, Ty>((Tx X, Ty Y) source) => new(source.X, source.Y);
	}

	public static class Point2
	{

		extension<T>(Point<T, T>) where T : unmanaged, INumber<T>
		{
			public static Point<T, T> Zeros => new(T.Zero, T.Zero);
			public static Point<T, T> Units => new(T.One, T.One);
			public static Point<T, T> UnitX => new(T.One, T.Zero);
			public static Point<T, T> UnitY => new(T.Zero, T.One);
		}
	}
	#endregion

	#region p = ⟨x, y, z⟩
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="Tx"></typeparam>
	/// <typeparam name="Ty"></typeparam>
	/// <typeparam name="Tz"></typeparam>
	/// <param name="X"></param>
	/// <param name="Y"></param>
	/// <param name="Z"></param>
	[StructLayout(LayoutKind.Sequential)]
	public record struct Point<Tx, Ty, Tz>(Tx X, Ty Y, Tz Z) where Tx : unmanaged, INumber<Tx> where Ty : unmanaged, INumber<Ty> where Tz : unmanaged, INumber<Tz>
	{
		public static implicit operator Point<Tx, Ty, Tz>(ValueTuple<Tx, Ty, Tz> source) => new(source.Item1, source.Item2, source.Item3);
	}

	public static class Point3
	{
		
		extension<T>(Point<T, T, T>) where T : unmanaged, INumber<T>
		{
			public static Point<T, T, T> Zeros => new(T.Zero, T.Zero, T.Zero);
			public static Point<T, T, T> Units => new(T.One, T.One, T.One);
			public static Point<T, T, T> UnitX => new(T.One, T.Zero, T.Zero);
			public static Point<T, T, T> UnitY => new(T.Zero, T.One, T.Zero);
			public static Point<T, T, T> UnitZ => new(T.Zero, T.Zero, T.One);
		}
	}
	#endregion

	#region p = ⟨x, y, z, w⟩
	//tex:
	//$p = (x, y, z, w) \in \mathit{R}^4$
	[StructLayout(LayoutKind.Sequential)]
	public readonly record struct Point<Tx, Ty, Tz, Tw>(Tx X, Ty Y, Tz Z, Tw W) where Tx : unmanaged where Ty : unmanaged where Tz : unmanaged where Tw : unmanaged
	{
		public static implicit operator Point<Tx, Ty, Tz, Tw>(ValueTuple<Tx, Ty, Tz, Tw> source) => new(source.Item1, source.Item2, source.Item3, source.Item4);
	}

	public static class Point4
	{
		
		extension<T>(Point<T, T, T, T>) where T : unmanaged, INumber<T>
		{
			public static Point<T, T, T, T> Zeros => new(T.Zero, T.Zero, T.Zero, T.Zero);
			public static Point<T, T, T, T> Units => new(T.One, T.One, T.One, T.One);
			public static Point<T, T, T, T> UnitX => new(T.One, T.Zero, T.Zero, T.Zero);
			public static Point<T, T, T, T> UnitY => new(T.Zero, T.One, T.Zero, T.Zero);
			public static Point<T, T, T, T> UnitZ => new(T.Zero, T.Zero, T.One, T.Zero);
			public static Point<T, T, T, T> UnitW => new(T.Zero, T.Zero, T.Zero, T.One);


			public static Point<T, T, T, T> operator *(Point<T, T, T, T> p, T scalar) => new(p.X * scalar, p.Y * scalar, p.Z * scalar, p.W * scalar);
			public static Point<T, T, T, T> operator /(Point<T, T, T, T> p, T scalar) => new(p.X / scalar, p.Y / scalar, p.Z / scalar, p.W / scalar);


			public static Point<T, T, T, T> operator +(Point<T, T, T, T> p1, Point<T, T, T, T> p2) => new(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z, p1.W + p2.W);

			public static Point<T, T, T, T> operator -(Point<T, T, T, T> p1, Point<T, T, T, T> p2) => new(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z, p1.W - p2.W);
		}
	}
	#endregion	

	public static class Point
	{

		public static Point<T> X<T>(T x) where T : unmanaged, INumber<T> => new(x);

		public static Point<T, T> Y<T>(this Point<T> source, T y) where T : unmanaged, INumber<T> => new(source.X, y);

		public static Point<T, T, T> Z<T>(this Point<T, T> source, T z) where T : unmanaged, INumber<T> => new(source.X, source.Y, z);

		public static Point<T, T, T, T> W<T>(this Point<T, T, T> source, T w) where T : unmanaged, INumber<T> => new(source.X, source.Y, source.Z, w);


		public static Point<sbyte, sbyte> ToSByte(this Point<int, int> source)
		{
			var (x, y) = source;

			return new(checked((sbyte)x), checked((sbyte)y));
		}

		public static Point<short, short> ToInt16(this Point<int, int> source)
		{
			var (x, y) = source;

			return new(checked((short)x), checked((short)y));
		}

		public static Point<long, long> ToInt64(this Point<int, int> source)
		{
			var (x, y) = source;

			return new(x, y);
		}


	}
}
