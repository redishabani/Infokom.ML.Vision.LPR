using Infokom.Numerics.Atomics;

using System.Runtime.CompilerServices;

namespace Infokom.Numerics
{
	public interface IOrdinal<T> where T : unmanaged, IOrdinal<T>
	{
		public static abstract int Start { get; }
		public static abstract int Count { get; }
		public static abstract T Forward(int offset);
		public static abstract T Reverse(int offset);


		public static abstract implicit operator int(T source);
		public static abstract explicit operator T(int source);
		public static abstract explicit operator checked T(int source);
	}

	public interface ISymbolic<T> where T : unmanaged, ISymbolic<T>
	{
		public static abstract IReadOnlyDictionary<char, T> Lookup { get; }

		public static abstract implicit operator char(T source);
		public static abstract explicit operator T(char source);
		public static abstract explicit operator checked T(char source);
	}


}