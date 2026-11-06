using System.Numerics;

namespace Infokom.Numerics.Extensions
{
	public static class EnumExtensions
	{
		extension<TSource>(TSource source) where TSource : struct, Enum
		{
			/// <summary>
			/// Returns a <see cref="bool"/> telling whether a given value is declared in <typeparamref name="TSource"/> enumeration.</summary>
			/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
			/// <param name="value">The value of <typeparamref name="TSource"/> to be checked.</param>
			/// <returns><see langword="true"/> if the value of <paramref name="source"/> is declared in <typeparamref name="TSource"/>; <see langword="false"/> otherwise.</returns>
			public bool IsKnown => Enum.IsDefined(source);

			public static TSource[] GetValues() => Enum.GetValues<TSource>();


			public TTarget ToBinary<TTarget>() where TTarget : unmanaged, IBinaryInteger<TTarget> => (TTarget)Enum.ToObject(typeof(TTarget), source);
		}
	}
}
