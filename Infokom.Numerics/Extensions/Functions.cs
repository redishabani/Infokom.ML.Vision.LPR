using Infokom.Numerics.Atomics;

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

using static Infokom.Numerics.Extensions.Constants;

namespace Infokom.Numerics.Extensions
{


	public static class Functions
	{
#pragma warning disable IDE1006 // Naming Styles

		extension<T>(T) where T : INumber<T>
		{

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T Σ(params T[] x)
			{
				T sum = T.Zero;
				foreach (var value in x)
					sum += value;
				return sum;
			}




			/// <summary>
			/// Returns the absolute value of a number.
			/// </summary>
			/// <param name="x">The number to get the absolute value of.</param>
			/// <returns>|<paramref name="x"/>|</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T abs(T x) => T.IsNegative(x) ? -x : x;


			/// <summary>
			/// Returns the minimum of two values.
			/// </summary>
			/// <param name="x">The first value to compare.</param>
			/// <param name="y">The second value to compare.</param>
			/// <returns>x if x &lt; y, otherwise y.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T min(T x, T y) => x < y ? x : y;

			/// <summary>
			/// Returns the minimum of three values.
			/// </summary>
			/// <param name="x">The first value to compare.</param>
			/// <param name="y">The second value to compare.</param>
			/// <param name="z">The third value to compare.</param>
			/// <returns>x if x &lt; y &amp;&amp; x &lt; z, y if y &lt; z, otherwise z.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T min(T x, T y, T z) => min(min(x, y), z);

			/// <summary>
			/// Returns the maximum of two values.
			/// </summary>
			/// <param name="x">The first value to compare.</param>
			/// <param name="y">The second value to compare.</param>
			/// <returns>x if x &gt; y, otherwise y.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T max(T x, T y) => x > y ? x : y;

			/// <summary>
			/// Returns the maximum of three values.
			/// </summary>
			/// <param name="x">The first value to compare.</param>
			/// <param name="y">The second value to compare.</param>
			/// <param name="z">The third value to compare.</param>
			/// <returns>x if x &gt; y &amp;&amp; x &gt; z, y if y &gt; z, otherwise z.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T max(T x, T y, T z) => max(max(x, y), z);


			/// <summary>
			/// Clamp a value between a minimum and maximum value.
			/// </summary>
			/// <param name="x">The value to clamp.</param>
			/// <param name="a">The minimum value.</param>
			/// <param name="b">The maximum value.</param>
			/// <returns>x if a ⩽ x ⩽ b, a if x &lt; a, b if x &gt; b</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T clamp(T x, T a, T b) => T.Clamp(x, a, b);


			/// <returns><c><paramref name="x"/> ⋅ <paramref name="x"/></c></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T sq(T x) => x * x;

			/// <returns><c><paramref name="x"/> ⋅ <paramref name="x"/> ⋅ <paramref name="x"/></c></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T cb(T x) => x * x * x;
		}


		extension(float)
		{
			/// <returns><c><paramref name="x1"/> &lt; <paramref name="x2"/> ? <paramref name="x1"/> : <paramref name="x2"/></c></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float min(float x1, float x2) => x1 < x2 ? x1 : x2;


			/// <returns><c>min(<paramref name="x1"/>, min(<paramref name="x2"/>, <paramref name="x3"/>))</c></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float min(float x1, float x2, float x3) => min(min(x1, x2), x3);


			/// <returns><c><see cref="min(float, float)">min</see>(<see cref="min(float, float)">min</see>(<paramref name="x1"/>, <paramref name="x2"/>), <see cref="min(float, float)">min</see>(<paramref name="x3"/>, <paramref name="x4"/>))</c></returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float min(float x1, float x2, float x3, float x4) => min(min(x1, x2), min(x3, x4));

			/// <summary>
			/// Find the smallest among some scalars.
			/// </summary>
			/// <param name="scalars">Collection of scalars to be searched.</param>
			/// <returns>The smallest value among the provided scalars.</returns>
			/// <remarks>
			/// If any of the scalars is NaN or negative infinity then such scalar is returned by terminating the search early.
			/// </remarks>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float min(params ReadOnlySpan<float> scalars)
			{
				var y = float.NaN;
				
				foreach (var x in scalars)
					if ((y = min(y, x)) is float.NaN or float.NegativeInfinity)
						break;

				return y;
			}



			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float max(float x1, float x2) => x1 is float.NaN || x2 is float.NaN ? float.NaN : x1 > x2 ? x1 : x2;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float max(float x1, float x2, float x3) => max(max(x1, x2), x3);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float max(float x1, float x2, float x3, float x4) => max(max(x1, x2), max(x3, x4));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float max(params ReadOnlySpan<float> scalars)
			{
				var y = float.NaN;
				foreach (var x in scalars)
					if ((y = max(y, x)) is float.NaN or float.PositiveInfinity)
						break;
				return y;
			}




			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float clamp(float source, float min, float max) => min > max ? float.NaN : source < min ? min : source > max ? max : source;


			public static float hypot(float x, float y) 
			{
				if (float.IsInfinity(x) || float.IsInfinity(y))
					return float.PositiveInfinity;

				if(float.IsNaN(x) || float.IsNaN(y))
					return float.NaN;

				if(x is 0)
					return y < 0 ? -y : y;

				if(y is 0)
					return x < 0 ? -x : x;

				return sqrt(sq(x) + sq(y));
			}

			//tex: $$ \mathcal{G}(x) = \frac{1}{\sqrt{2\pi}} e^{-\frac{x^2}{2}}$$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float gauss(float x) => (float)(exp(-x * x / 2) / sqrt(2f * π));

			//tex: $$ \mathcal{G}(x | \mu) = \mathcal{G}(x - \mu) = \frac{1}{\sqrt{2\pi}} e^{-\frac{(x - \mu)^2}{2}} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float gauss(float x, float μ) => gauss(x - μ);

			//tex: $$ \mathcal{G}(x | \mu, \sigma) = \frac{1}{\sigma}\mathcal{G}(\frac{x}{\sigma}|\frac{\mu}{\sigma}) = \frac{1}{\sigma} \mathcal{G}(\frac{x - \mu}{\sigma}) = \frac{1}{\sigma \sqrt{2\pi}} e^{-\frac{(x - \mu)^2}{2\sigma^2}} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static float gauss(float x, float μ, float σ) => gauss((x - μ) / σ) / σ;
		}

		extension(double)
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double min(double x1, double x2) => x1 < x2 ? x1 : x2;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double min(double x1, double x2, double x3) => min(min(x1, x2), x3);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double min(double x1, double x2, double x3, double x4) => min(min(x1, x2), min(x3, x4));
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double max(double x1, double x2) => x1 > x2 ? x1 : x2;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double max(double x1, double x2, double x3) => max(max(x1, x2), x3);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double max(double x1, double x2, double x3, double x4) => max(max(x1, x2), max(x3, x4));
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double clamp(double x, double a, double b) => a > b ? throw new ArgumentException("Minimum value cannot be greater than maximum value.") : (x < a ? a : (x > b ? b : x));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double sq(double x) => x * x;
			
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double cb(double x) => x * x * x;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double sqrt(double x) => Math.Sqrt(x);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double cbrt(double x) => Math.Cbrt(x);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double hypot(double x, double y) => double.Hypot(x, y);

			//tex: $$ e^{x} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double exp(double x) => Math.Exp(x);




			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double gauss(double x) => exp(-x * x / 2) / sqrt(2.0 * π);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double gauss(double x, double μ) => gauss(x - μ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static double gauss(double x, double μ, double σ) => gauss((x - μ) / σ) / σ;





		}






		extension<T>(T) where T : IRootFunctions<T>
		{
			/// <summary>
			/// Returns the square root of a number.
			/// </summary>
			/// <param name="x">The value to calculate the square root of.</param>
			/// <returns>√x</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T sqrt(T x) => T.Sqrt(x);

			/// <summary>
			/// Returns the cube root of a number.
			/// </summary>
			/// <param name="x">The value to calculate the cube root of.</param>
			/// <returns>∛x</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T cbrt(T x) => T.Cbrt(x);

			/// <summary>∜<paramref name="x"/></summary>½
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T qbrt(T x) => T.Sqrt(T.Sqrt(x));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T hypot(T x, T y) => T.Hypot(x, y);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T rootn(T x, int n) => T.RootN(x, n);
		}

		extension<T>(T) where T : ITrigonometricFunctions<T>
		{
			/// <summary>
			/// Sine trigonometric function.
			/// </summary>
			/// <param name="x">The value to calculate the sine of.</param>
			/// <returns>sin(x)</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T sin(T ϑ) => T.Sin(ϑ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T cos(T ϑ) => T.Cos(ϑ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T tan(T ϑ) => T.Tan(ϑ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T asin(T ϑ) => T.Asin(ϑ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T acos(T ϑ) => T.Acos(ϑ);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T atan(T x) => T.Atan(x);

			/// <summary>
			/// Computes the arctangent of a number and returns the angle in radians.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="θ"></param>
			public static void atan(in T x, out T θ) => θ = T.Atan(x);
		}


		extension<T>(T) where T : IFloatingPoint<T>
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T round(T x) => T.Round(x);

			

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T floor(T x) => T.Floor(x);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T ceil(T x) => T.Ceiling(x);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T truncate(T x) => T.Truncate(x);
		}

		extension<T>(T) where T : IFloatingPointIeee754<T>
		{
			
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T exp(T x) => T.Exp(x);

			//tex: $$ \ln{x} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T ln(T x) => T.Log(x);

			//tex: $$ \log_{2}{x} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T log2(T x) => T.Log2(x);

			//tex: $$ \log_{10}{x} $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T log10(T x) => T.Log10(x);

			//tex: $$ \mathrm{atan2}(y, x) = \mathrm{atan}\left(\frac{y}{x}\right) $$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T atan2(T y, T x) => T.Atan2(y, x);
		}

		extension<T>(T) where T : unmanaged, INumber<T>
		{
			//tex: If $X$ is a set of $n$ elements, and ${\cal{P}}(X)$ the power set of $X$, then:
			//$$\binom{X}{k} = \{ C_{k} | C_k \in {\cal{P}}(X), |C_{k}| = k \}$$
			//is the family of $k$-element subsets of $X$.
			//The the number of $k$-element subsets of an $n$-element set is given by the binomial coefficient:
			//$$|\binom{X}{k}| = \binom{|X|}{k} =  \frac{|X|!}{k!(|X|-k)!}$$
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static T choose(T n, T k)
			{
				if(!T.IsInteger(n) || !T.IsPositive(n) || !T.IsInteger(k))
				{
					throw new ArgumentException("Both n and k must be integers.");
				}

				var Cnk = T.One;

				if (n > T.Zero)
				{
					k = T.Clamp(k, T.Zero, n);
					if (n - k > T.Zero)
					{
						if (k > n - k) k = n - k;

						for (var i = T.One; i <= k; i++)
						{
							Cnk = (Cnk * (n + T.One - i)) / i;//NOTE: is not equivalent to C *= (n + T.One - i) / i, because of integer division truncation.
						}
					}
				}

				return Cnk;
			}
		}
	}
#pragma warning restore IDE1006 // Naming Styles
}