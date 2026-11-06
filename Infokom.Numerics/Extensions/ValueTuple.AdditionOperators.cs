using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Extensions
{

	public static partial class AdditionOperatorsExtensions
	{
		extension<T1>(ValueTuple<T1>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1> operator +(ValueTuple<T1> a, ValueTuple<T1> b) => ValueTuple.Create(a.Item1 + b.Item1);
		}

		extension<T1, T2>(ValueTuple<T1, T2>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2> operator +(ValueTuple<T1, T2> a, ValueTuple<T1, T2> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2);
		}

		extension<T1, T2, T3>(ValueTuple<T1, T2, T3>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2> where T3 : unmanaged, IAdditionOperators<T3, T3, T3>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3> operator +(ValueTuple<T1, T2, T3> a, ValueTuple<T1, T2, T3> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3);
		}

		extension<T1, T2, T3, T4>(ValueTuple<T1, T2, T3, T4>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2> where T3 : unmanaged, IAdditionOperators<T3, T3, T3> where T4 : unmanaged, IAdditionOperators<T4, T4, T4>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4> operator +(ValueTuple<T1, T2, T3, T4> a, ValueTuple<T1, T2, T3, T4> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3, a.Item4 + b.Item4);
		}

		extension<T1, T2, T3, T4, T5>(ValueTuple<T1, T2, T3, T4, T5>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2> where T3 : unmanaged, IAdditionOperators<T3, T3, T3> where T4 : unmanaged, IAdditionOperators<T4, T4, T4> where T5 : unmanaged, IAdditionOperators<T5, T5, T5>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5> operator +(ValueTuple<T1, T2, T3, T4, T5> a, ValueTuple<T1, T2, T3, T4, T5> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3, a.Item4 + b.Item4, a.Item5 + b.Item5);
		}

		extension<T1, T2, T3, T4, T5, T6>(ValueTuple<T1, T2, T3, T4, T5, T6>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2> where T3 : unmanaged, IAdditionOperators<T3, T3, T3> where T4 : unmanaged, IAdditionOperators<T4, T4, T4> where T5 : unmanaged, IAdditionOperators<T5, T5, T5> where T6 : unmanaged, IAdditionOperators<T6, T6, T6>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5, T6> operator +(ValueTuple<T1, T2, T3, T4, T5, T6> a, ValueTuple<T1, T2, T3, T4, T5, T6> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3, a.Item4 + b.Item4, a.Item5 + b.Item5, a.Item6 + b.Item6);
		}

		extension<T1, T2, T3, T4, T5, T6, T7>(ValueTuple<T1, T2, T3, T4, T5, T6, T7>) where T1 : unmanaged, IAdditionOperators<T1, T1, T1> where T2 : unmanaged, IAdditionOperators<T2, T2, T2> where T3 : unmanaged, IAdditionOperators<T3, T3, T3> where T4 : unmanaged, IAdditionOperators<T4, T4, T4> where T5 : unmanaged, IAdditionOperators<T5, T5, T5> where T6 : unmanaged, IAdditionOperators<T6, T6, T6> where T7 : unmanaged, IAdditionOperators<T7, T7, T7>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> operator +(ValueTuple<T1, T2, T3, T4, T5, T6, T7> a, ValueTuple<T1, T2, T3, T4, T5, T6, T7> b) => ValueTuple.Create(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3, a.Item4 + b.Item4, a.Item5 + b.Item5, a.Item6 + b.Item6, a.Item7 + b.Item7);
		}
	}



	public static partial class AdditionOperatorsExtensions
	{
		extension<T>(T[]) where T : IAdditionOperators<T, T, T>
		{
			public static T[] operator +(T[] a, T[] b)
			{
				var n = a.Length == b.Length ? a.Length : throw new ArgumentOutOfRangeException("Array dimension mismatch");
				var c = new T[a.Length];

				for (int i = 0; i < n; i++)
				{
					c[i] = a[i] + b[i];
				}

				return c;
			}
		}
	}

	public static partial class SubtractionOperators
	{
		extension<T>(T[]) where T : ISubtractionOperators<T, T, T>
		{
			public static T[] operator -(T[] a, T[] b)
			{
				var n = a.Length == b.Length ? a.Length : throw new ArgumentOutOfRangeException("Array dimension mismatch");
				var c = new T[a.Length];

				for (int i = 0; i < n; i++)
				{
					c[i] = a[i] - b[i];
				}

				return c;
			}
		}
	}

	public static partial class MultiplyOperators
	{
		extension<T>(T[]) where T : IMultiplyOperators<T, T, T>
		{
			public static T[] operator *(T[] a, T b)
			{
				var n = a.Length;

				var c = new T[n];
				for (int i = 0; i < n; i++)
				{
					c[i] = a[i] * b;
				}

				return c;
			}

			public static T[] operator *(T a, T[] b) => b * a;
		}
	}

	public static partial class DivisionOperators
	{
		extension<T>(T[]) where T : IDivisionOperators<T, T, T>
		{
			public static T[] operator /(T[] a, T b)
			{
				var n = a.Length;

				var c = new T[n];
				for (int i = 0; i < n; i++)
				{
					c[i] = a[i] / b;
				}

				return c;
			}
		}
	}
}
