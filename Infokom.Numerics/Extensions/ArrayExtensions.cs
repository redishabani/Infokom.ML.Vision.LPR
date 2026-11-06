using System.Collections;


namespace Infokom.Numerics.Extensions
{


	public static class ArrayExtensions
	{
		public static string Stringify<T>(this T[] source, string elementSeparator = ", ", string elementFormat = null, IFormatProvider elementFormatProvider = null) where T : IFormattable
		{
			ArgumentNullException.ThrowIfNull(source, nameof(source));

			var cells = Array.ConvertAll(source, x => x.ToString(elementFormat, elementFormatProvider));

			var row = string.Join(elementSeparator, cells);

			return row;
		}

		public static string Stringify<T>(this ReadOnlySpan<T> source, string elementSeparator = ", ", string elementFormat = null, IFormatProvider elementFormatProvider = null) where T : IFormattable
		{
			Span<string> tokens = new string[source.Length];

			int i = 0;
			foreach (var element in source)
				tokens[i++] = element.ToString(elementFormat, elementFormatProvider);

			return string.Join(elementSeparator, tokens);
		}

		public static void Foo(int[,] source)
		{
			
		}
	}


}
