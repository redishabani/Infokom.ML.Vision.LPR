using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Xml.Linq;

using static Infokom.Numerics.Extensions.RANDOM;

namespace Infokom.Numerics.Extensions
{


	public static class RANDOM
	{
		private static uint SplitMix(ref uint t)
		{
			t ^= t >> 16;
			t *= 0x85EBCA6B;
			t ^= t >> 13;
			t *= 0xC2B2AE35;
			t ^= t >> 16;
			return t;
		}

		private static ulong SplitMix(ref ulong t)
		{
			t ^= t >> 30;
			t *= 0xBF58476D1CE4E5B9;
			t ^= t >> 27;
			t *= 0x94D049BB133111EB;
			t ^= t >> 31;
			return t;
		}

		private static ulong X, Y, Z, W;

		static RANDOM()
		{
			ulong seed = (ulong)Environment.TickCount64;

			X = SplitMix(ref seed);
			Y = SplitMix(ref seed);
			Z = SplitMix(ref seed);
			W = SplitMix(ref seed);

			// xoshiro requires a non-zero state
			if ((X | Y | Z | W) == 0)
				X = 1;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Next()
		{
			ulong result = BitOperations.RotateLeft(Y * 5, 7) * 9;

			ulong t = Y << 17;

			Z ^= X;
			W ^= Y;
			Y ^= Z;
			X ^= W;

			Z ^= t;
			W = BitOperations.RotateLeft(W, 45);

			return result;
		}

		extension(ulong)
		{
			/// <summary>
			/// Generates a random (xorshift) unsigned long integer between 0 (inclusive) and the specified maximum value (exclusive).
			/// </summary>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random unsigned long integer between 0 and <paramref name="max"/>.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ulong Random(ulong max) => Next() % max;

			/// <summary>
			/// Generates a random (xorshift) unsigned long integer between the specified minimum and maximum values.
			/// </summary>
			/// <param name="min">The minimum value (inclusive).</param>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random unsigned long integer between <paramref name="min"/> and <paramref name="max"/>.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ulong Random(ulong min, ulong max) => min + Random(max - min);
		}

		extension(long)
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static long Random(long max) => max >= 0 ? (long)ulong.Random((ulong)max) : throw new ArgumentException($"max ({max}) must be non-negative");

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static long Random(long min, long max) => max >= min ? min + long.Random((max - min)) : throw new ArgumentException($"max ({max}) must be greater than or equal to min ({min})");
		}


		extension(uint)
		{
			/// <summary>
			/// Generates a random (xorshift) unsigned integer between 0 (inclusive) and the specified maximum value (exclusive).
			/// </summary>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random unsigned integer between 0 and <paramref name="max"/>.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static uint Random(uint max) => (uint)ulong.Random(max);

			/// <summary>
			/// Generates a random (xorshift) unsigned integer between the specified minimum and maximum values.
			/// </summary>
			/// <param name="min">The minimum value (inclusive).</param>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random unsigned integer between <paramref name="min"/> and <paramref name="max"/>.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static uint Random(uint min, uint max) => (uint)ulong.Random(min, max);
		}


		extension(int)
		{
			/// <summary>
			/// Generates a random (xorshift) non-negative integer.
			/// </summary>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random integer between 0 (inclusive) and <paramref name="max"/> (exclusive).</returns>
			/// <exception cref="ArgumentException"></exception>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static int Random(int max) => (int)long.Random(max);

			/// <summary>
			/// Generates a random (xorshift) integer between the specified minimum and maximum values.
			/// </summary>
			/// <param name="min">The minimum value (inclusive).</param>
			/// <param name="max">The maximum value (exclusive).</param>
			/// <returns>A random integer between <paramref name="min"/> and <paramref name="max"/>.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static int Random(int min, int max) => (int)long.Random(min, max);
		}



		extension<T>(T) where T : unmanaged, IBinaryInteger<T>
		{
			public static T Random() => T.CreateTruncating(Next());
		}
	}
}
