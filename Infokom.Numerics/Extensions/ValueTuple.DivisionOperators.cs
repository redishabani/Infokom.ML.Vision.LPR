using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Extensions
{
	public static partial class DivisionOperators
	{
		extension<T>(ValueTuple<T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T> operator /(ValueTuple<T> a, T b) => ValueTuple.Create(a.Item1 / b);
		}

		extension<T>(ValueTuple<T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T> operator /(ValueTuple<T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b);
		}

		extension<T>(ValueTuple<T, T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T> operator /(in ValueTuple<T, T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b, a.Item3 / b);
		}

		extension<T>(ValueTuple<T, T, T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T> operator /(ValueTuple<T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b, a.Item3 / b, a.Item4 / b);
		}

		extension<T>(ValueTuple<T, T, T, T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T> operator /(ValueTuple<T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b, a.Item3 / b, a.Item4 / b, a.Item5 / b);
		}

		extension<T>(ValueTuple<T, T, T, T, T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T, T> operator /(ValueTuple<T, T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b, a.Item3 / b, a.Item4 / b, a.Item5 / b, a.Item6 / b);
		}

		extension<T>(ValueTuple<T, T, T, T, T, T, T>) where T : unmanaged, IDivisionOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T, T, T> operator /(ValueTuple<T, T, T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 / b, a.Item2 / b, a.Item3 / b, a.Item4 / b, a.Item5 / b, a.Item6 / b, a.Item7 / b);
		}
	}
}
