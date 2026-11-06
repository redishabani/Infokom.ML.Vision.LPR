using System.Numerics;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics.Extensions
{
	public static partial class MultiplyOperators
	{
		extension<T>(ValueTuple<T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T> operator *(ValueTuple<T> a, T b) => ValueTuple.Create(a.Item1 * b);
		}

		extension<T>(ValueTuple<T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T> operator *(ValueTuple<T, T> a, T b) => ValueTuple.Create(a.Item1 * b, a.Item2 * b);
		}

		extension<T>(ValueTuple<T, T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T> Multiply(ValueTuple<T, T, T> source, T factor) => ValueTuple.Create(source.Item1 * factor, source.Item2 * factor, source.Item3 * factor);


			public static ValueTuple<T, T, T> operator *(in ValueTuple<T, T, T> a, T b) => Multiply(a, b);
			public static ValueTuple<T, T, T> operator *(T a, ValueTuple<T, T, T> b) => Multiply(b, a);
		}

		extension<T>(ref ValueTuple<T, T, T> source) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			public void operator *=(T factor)
			{
				ref T x = ref source.Item1;
				ref T y = ref source.Item2;
				ref T z = ref source.Item3;

				x *= factor;
				y *= factor;
				z *= factor;
			}
		}

		extension<T>(ValueTuple<T, T, T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T> operator *(ValueTuple<T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 * b, a.Item2 * b, a.Item3 * b, a.Item4 * b);
		}

		extension<T>(ValueTuple<T, T, T, T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T> operator *(ValueTuple<T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 * b, a.Item2 * b, a.Item3 * b, a.Item4 * b, a.Item5 * b);
		}

		extension<T>(ValueTuple<T, T, T, T, T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T, T> operator *(ValueTuple<T, T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 * b, a.Item2 * b, a.Item3 * b, a.Item4 * b, a.Item5 * b, a.Item6 * b);
		}

		extension<T>(ValueTuple<T, T, T, T, T, T, T>) where T : unmanaged, IMultiplyOperators<T, T, T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ValueTuple<T, T, T, T, T, T, T> operator *(ValueTuple<T, T, T, T, T, T, T> a, T b) => ValueTuple.Create(a.Item1 * b, a.Item2 * b, a.Item3 * b, a.Item4 * b, a.Item5 * b, a.Item6 * b, a.Item7 * b);
		}
	}
}
