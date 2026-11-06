using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Infokom.Numerics.Extensions
{
	public static class SpanExtensions
	{
		extension<T>(in ReadOnlySpan<T> source) where T : unmanaged
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryGet(int offset, out T element)
			{
				if((uint)offset >= source.Length)
				{
					element = default;
					return false;
				}

				element = source[offset];
				return true;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryGetFirst(out T element) => source.TryGet(0, out element);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryGetLast(out T element) => source.TryGet(source.Length - 1, out element);

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryGetRandom(out T element) => source.TryGet(int.Random(source.Length), out element);




		}
	}
}
