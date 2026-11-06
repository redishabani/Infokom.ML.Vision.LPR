using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Extensions
{
	public static partial class IncrementOperators
	{
		extension<T1>(ValueTuple<T1>) where T1 : unmanaged, IIncrementOperators<T1>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1> operator --(ValueTuple<T1> a) => ValueTuple.Create(++a.Item1);
		}

		extension<T1, T2>(ValueTuple<T1, T2>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2> operator --(ValueTuple<T1, T2> a) => ValueTuple.Create(++a.Item1, ++a.Item2);
		}

		extension<T1, T2, T3>(ValueTuple<T1, T2, T3>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2> where T3 : unmanaged, IIncrementOperators<T3>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3> operator --(ValueTuple<T1, T2, T3> a) => ValueTuple.Create(++a.Item1, ++a.Item2, ++a.Item3);
		}

		extension<T1, T2, T3, T4>(ValueTuple<T1, T2, T3, T4>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2> where T3 : unmanaged, IIncrementOperators<T3> where T4 : unmanaged, IIncrementOperators<T4>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4> operator --(ValueTuple<T1, T2, T3, T4> a) => ValueTuple.Create(++a.Item1, ++a.Item2, ++a.Item3, ++a.Item4);
		}

		extension<T1, T2, T3, T4, T5>(ValueTuple<T1, T2, T3, T4, T5>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2> where T3 : unmanaged, IIncrementOperators<T3> where T4 : unmanaged, IIncrementOperators<T4> where T5 : unmanaged, IIncrementOperators<T5>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5> operator --(ValueTuple<T1, T2, T3, T4, T5> a) => ValueTuple.Create(++a.Item1, ++a.Item2, ++a.Item3, ++a.Item4, ++a.Item5);
		}

		extension<T1, T2, T3, T4, T5, T6>(ValueTuple<T1, T2, T3, T4, T5, T6>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2> where T3 : unmanaged, IIncrementOperators<T3> where T4 : unmanaged, IIncrementOperators<T4> where T5 : unmanaged, IIncrementOperators<T5> where T6 : unmanaged, IIncrementOperators<T6>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5, T6> operator --(ValueTuple<T1, T2, T3, T4, T5, T6> a) => ValueTuple.Create(++a.Item1, ++a.Item2, ++a.Item3, ++a.Item4, ++a.Item5, ++a.Item6);
		}

		extension<T1, T2, T3, T4, T5, T6, T7>(ValueTuple<T1, T2, T3, T4, T5, T6, T7>) where T1 : unmanaged, IIncrementOperators<T1> where T2 : unmanaged, IIncrementOperators<T2> where T3 : unmanaged, IIncrementOperators<T3> where T4 : unmanaged, IIncrementOperators<T4> where T5 : unmanaged, IIncrementOperators<T5> where T6 : unmanaged, IIncrementOperators<T6> where T7 : unmanaged, IIncrementOperators<T7>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> operator --(ValueTuple<T1, T2, T3, T4, T5, T6, T7> a) => ValueTuple.Create(++a.Item1, ++a.Item2, ++a.Item3, ++a.Item4, ++a.Item5, ++a.Item6, ++a.Item7);
		}
	}
}
