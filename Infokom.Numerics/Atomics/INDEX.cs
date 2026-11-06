using Infokom.Numerics.Operators;

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Atomics
{
	public readonly record struct Index<Tx>(Tx X)
		where Tx : unmanaged, IBinaryInteger<Tx>
	{
		public static Index<Tx> Zero { get; } = new(Tx.Zero);
		public static Index<Tx> Unit { get; } = new(Tx.One);

		public static implicit operator Index<Tx>(Tx source) => new(source);
		public static implicit operator Index<Tx>(ValueTuple<Tx> source) => new(source.Item1);


		public static explicit operator Tx(in Index<Tx> source) => Unsafe.As<Index<Tx>, Tx>(ref Unsafe.AsRef(in source));


		public static Index<Tx> operator +(Index<Tx> a, Index<Tx> b) => new(a.X + b.X);
		public static Index<Tx> operator *(Index<Tx> a, Tx x) => new(a.X * x);
		public static Index<Tx> operator /(Index<Tx> a, Tx x) => new(a.X / x);
		public static Index<Tx> operator %(Index<Tx> a, Tx x) => new(a.X % x);

	}

	public readonly record struct Index<Tx, Ty>(Tx X, Ty Y) 
		where Tx : unmanaged, IBinaryInteger<Tx>
		where Ty : unmanaged, IBinaryInteger<Ty>
	{
		public static Index<Tx, Ty> Zeros { get; } = new(Tx.Zero, Ty.Zero);
		public static Index<Tx, Ty> Units { get; } = new(Tx.One, Ty.One);

		public static implicit operator Index<Tx, Ty>(ValueTuple<Tx, Ty> source) => new(source.Item1, source.Item2);
	}

	public readonly record struct Index<Tx, Ty, Tz>(Tx X, Ty Y, Tz Z)
		where Tx : unmanaged, IBinaryInteger<Tx> 
		where Ty : unmanaged, IBinaryInteger<Ty> 
		where Tz : unmanaged, IBinaryInteger<Tz>
	{
		public static Index<Tx, Ty, Tz> Zeros { get; } = new(Tx.Zero, Ty.Zero, Tz.Zero);
		public static Index<Tx, Ty, Tz> Units { get; } = new(Tx.One, Ty.One, Tz.One);


		public static implicit operator Index<Tx, Ty, Tz>(ValueTuple<Tx, Ty, Tz> source) => new(source.Item1, source.Item2, source.Item3);

	}

	public static class INDEX
	{
		public static Index<T> X<T>(T x) where T : unmanaged, IBinaryInteger<T> => (x);
		
		public static Index<T, T> Y<T>(this Index<T> source, T y) where T : unmanaged, IBinaryInteger<T> => (source.X, y);

		public static Index<T, T, T> Z<T>(this Index<T, T> source, T z) where T : unmanaged, IBinaryInteger<T> => (source.X, source.Y, z);





		extension(System.Index)
		{
			public static Index<int> X(int x) => (x);


		}
	}
}
