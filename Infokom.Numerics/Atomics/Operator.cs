using System;
using System.Collections.Generic;
using System.Text;

namespace Infokom.Numerics.Atomics
{
	public interface IUnaryOperator<TOperator, Tx, Ty> where TOperator : unmanaged, IUnaryOperator<TOperator, Tx, Ty>
	{
		public static abstract Ty Invoke(Tx X);
		public static abstract bool TryInvoke(in Tx X, out Ty Y);
	}

	public interface IBinaryOperator<TOperator, Tx, Ty, Tz> where TOperator : unmanaged, IBinaryOperator<TOperator, Tx, Ty, Tz>
	{

		public static abstract Tz Invoke(Tx X, Ty Y);

		public static abstract bool TryInvoke(in Tx X, in Ty Y, out Tz Z);
	}


	public static class Operator
	{
		public static Ty Invoke<TOperator, Tx, Ty>(Tx X) where TOperator : unmanaged, IUnaryOperator<TOperator, Tx, Ty> => TOperator.Invoke(X);

		public static bool TryInvoke<TOperator, Tx, Ty>(in Tx X, out Ty Y) where TOperator : unmanaged, IUnaryOperator<TOperator, Tx, Ty> => TOperator.TryInvoke(in X, out Y);

		public static Tz Invoke<TOperator, Tx, Ty, Tz>(Tx X, Ty Y) where TOperator : unmanaged, IBinaryOperator<TOperator, Tx, Ty, Tz> => TOperator.Invoke(X, Y);

		public static bool TryInvoke<TOperator, Tx, Ty, Tz>(in Tx X, in Ty Y, out Tz Z) where TOperator : unmanaged, IBinaryOperator<TOperator, Tx, Ty, Tz> => TOperator.TryInvoke(in X, in Y, out Z);
	}

}
