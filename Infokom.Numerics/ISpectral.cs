using Infokom.Numerics.Atomics;

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics
{

	public interface INominable<TEntity, TNominal> : IEqualityOperators<TEntity, TEntity, bool> where TEntity : INominable<TEntity, TNominal>
	{
		public TNominal Group { get; }
	}

	public interface IOrdinable<TEntity, TOrdinal> : IComparisonOperators<TEntity, TEntity, bool> where TEntity : IOrdinable<TEntity, TOrdinal>
	{
		public TOrdinal Rank { get; }
	}



	


	public interface ISpectral<T> where T : ISpectral<T> 
	{
		/// <summary>
		/// The empty set
		/// </summary>
		/// <remarks>
		/// A is <typeparamref name="T"/> => A ∪ Φ = A
		/// </remarks>
		public static abstract T Φ { get; }

		/// <summary>
		/// The universe set
		/// </summary>
		/// <remarks>
		/// A is <typeparamref name="T"/> => A ∩ Ω = A
		/// </remarks>
		public static abstract T Ω { get; }


		


		/// <returns><see cref="ISpectral{TSet}.Ω">ω</see> \ <paramref name="a"/></returns>
		public static abstract T operator ~(T a);

		/// <returns><paramref name="a"/> ∩ <paramref name="b"/></returns>
		public static abstract T operator &(T a, T b);

		/// <returns><paramref name="a"/> ∪ <paramref name="b"/></returns>
		public static abstract T operator |(T a, T b);
	}


	public interface ISpectral<T, Tx> : ISpectral<T> where T : ISpectral<T, Tx> where Tx : unmanaged, INumber<Tx>
	{
		public Point<Tx> LowerBound { get; }
		public Point<Tx> UpperBound { get; }

		public static abstract Size<Tx> Size { get; }		
	}

	public interface ISpectral<T, Tx, Ty> : ISpectral<T> where T : ISpectral<T, Tx, Ty> where Tx : unmanaged, INumber<Tx> where Ty : unmanaged, INumber<Ty>
	{
		public bool this[Ty y, Tx x] { get; }

		public Point<Tx, Ty> LowerBound { get; }
		public Point<Tx, Ty> UpperBound { get; }

		public static abstract Size<Tx> Size { get; }

	}


}