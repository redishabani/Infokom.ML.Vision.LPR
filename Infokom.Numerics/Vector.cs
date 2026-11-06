using Infokom.Numerics.Atomics;

using System.Collections;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Infokom.Numerics
{

	public interface IVectorial<TVector> where TVector : IVectorial<TVector>
	{
		public static abstract Size<sbyte> Size { get; }
	}

	public interface IVectorial<TVector, TElement> : IVectorial<TVector>, IReadOnlyList<TElement> where TVector : IVectorial<TVector, TElement>
	{
		public struct Enumerator : IEnumerator<TElement>
		{
			private readonly TVector _owner;
			private sbyte _offset;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Enumerator(TVector owner)
			{
				_owner = owner;
				_offset = -1;
			}

			public readonly TElement Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get => TVector.Extract(_owner, _offset);
			}

			readonly object IEnumerator.Current => this.Current;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext() => (++_offset) < TVector.Size;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset() => _offset = -1;

			readonly void IDisposable.Dispose() => _ = false;

		}


		public new virtual Enumerator GetEnumerator() => new((TVector)this);
		int IReadOnlyCollection<TElement>.Count => TVector.Size.X;
		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator() => this.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


		public static abstract bool TryExtract(TVector source, int offset, out TElement target);
		public static abstract TElement Extract(TVector source, int offset);

		public static abstract bool TryIsolate(TVector source, int offset, out TVector target);
		public static abstract TVector Isolate(TVector source, int offset);
	}














	public interface IVector<T>
	{
		public int Size { get; }

		public T this[int index] { get; }
	}

	public class Vector<T>
	{
		private readonly Array _data;

		private Vector(Array data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));

			this._data = data;
		}

		public int Size => _data.Length;

		public T this[int i] => (T)_data.GetValue(i);


		public static Vector<T> Create(int size) => Create(size, null);

		public static Vector<T> Create(int size, T value) => Create(size, i => value);

		public static Vector<T> Create(int size, Func<int, T> source)
		{
			var data = Array.CreateInstance(typeof(T), size);

			if (source != null)
			{
				for (int i = 0; i < size; i++)
				{
					data.SetValue(source(i), i);
				}
			}

			return new Vector<T>(data);
		}
	}


	public static class Vector
	{
		public static Vector<T> Create<T>(int size) => Vector<T>.Create(size);

		public static Vector<T> Create<T>(int size, T value) => Vector<T>.Create(size, value);

		public static Vector<T> Create<T>(int size, Func<int, T> source) => Vector<T>.Create(size, source);



		#region addition
		extension<TScalar>(ReadOnlySpan<TScalar>) where TScalar : unmanaged, INumber<TScalar>
		{
			public static TScalar[] operator +(ReadOnlySpan<TScalar> a)
			{
				var n = a.Length;

				var b = new TScalar[n];

				unsafe
				{
					fixed (TScalar* aPtr = &MemoryMarshal.GetReference(a), bPtr = &MemoryMarshal.GetReference(b))
					{
						for (int i = 0; i < n; i++)
						{
							bPtr[i] = +aPtr[i];
						}
					}
				}

				return b;
			}


			public static TScalar[] operator +(ReadOnlySpan<TScalar> v1, ReadOnlySpan<TScalar> v2)
			{
				ArgumentOutOfRangeException.ThrowIfNotEqual(v1.Length, v2.Length, "Array dimension mismatch.");

				var n = v1.Length;

				var v = new TScalar[n];

				unsafe
				{
					fixed (TScalar* ptr1 = &MemoryMarshal.GetReference(v1), ptr2 = &MemoryMarshal.GetReference(v2), ptr = &MemoryMarshal.GetReference(v))
					{
						for (int i = 0; i < n; i++)
						{
							ptr[i] = ptr1[i] + ptr2[i];
						}
					}
				}

				return v;
			}



			public static TScalar[] operator -(ReadOnlySpan<TScalar> a)
			{
				var n = a.Length;

				var b = new TScalar[n];

				unsafe
				{
					fixed (TScalar* aPtr = &MemoryMarshal.GetReference(a), bPtr = &MemoryMarshal.GetReference(b))
					{
						for (int i = 0; i < n; i++)
						{
							bPtr[i] = -aPtr[i];
						}
					}
				}

				return b;
			}



			public static TScalar[] operator -(ReadOnlySpan<TScalar> a, ReadOnlySpan<TScalar> b)
			{
				ArgumentOutOfRangeException.ThrowIfNotEqual(a.Length, b.Length, "Array dimension mismatch.");

				var n = a.Length;

				var c = new TScalar[n];

				unsafe
				{
					fixed (TScalar* aPtr = &MemoryMarshal.GetReference(a), bPtr = &MemoryMarshal.GetReference(b), cPtr = &MemoryMarshal.GetReference(c))
					{
						for (int i = 0; i < n; i++)
						{
							cPtr[i] = aPtr[i] - bPtr[i];
						}
					}
				}


				return c;
			}


			public static TScalar[] operator *(ReadOnlySpan<TScalar> a, TScalar b)
			{
				var n = a.Length;

				var c = new TScalar[n];



				unsafe
				{
					fixed (TScalar* aPtr = &MemoryMarshal.GetReference(a), cPtr = &MemoryMarshal.GetReference(c))
					{
						for (int i = 0; i < n; i++)
						{
							cPtr[i] = aPtr[i] * b;
						}
					}
				}

				return c;
			}

			public static TScalar[] operator *(TScalar a, ReadOnlySpan<TScalar> b) => b * a;

			public static TScalar[] operator /(ReadOnlySpan<TScalar> a, TScalar b) => a * (TScalar.One / b);
		}

		#endregion



		#region uint[]

		/// <summary>
		/// Returns the maximum value in the vector.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Max(this uint[] vector)
		{
			uint max = uint.MinValue;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					uint* pn = p0 + vector.Length;


					uint* pi = p0;
					while (pi < pn)
					{
						if (*pi > max)
							max = *pi;
						pi++;
					}
				}
			}

			return max;
		}

		/// <summary>
		/// Returns the minimum value in the vector.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Min(this uint[] vector)
		{
			uint min = uint.MaxValue;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					uint* pn = p0 + vector.Length;


					uint* pi = p0;
					while (pi < pn)
					{
						if (*pi < min)
							min = *pi;
						pi++;
					}
				}
			}

			return min;
		}

		/// <summary>
		/// Returns the average value in the vector.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Avg(this uint[] vector)
		{
			int n = vector.Length;
			uint avg = 0U;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					for (int i = 0; i < n; i++)
					{
						avg += *(p0 + i);
					}
				}
			}

			return avg / (uint)n;
		}

		/// <summary>
		/// Executes the specified action for each element in the vector.
		/// </summary>
		/// <param name="vector"> the vector to iterate over </param>
		/// <param name="action"> the action to execute for each element </param>
		public static void ForEach(this uint[] vector, Action<uint> action)
		{
			ArgumentNullException.ThrowIfNull(vector, nameof(vector));
			ArgumentNullException.ThrowIfNull(action, nameof(action));

			int n = vector.Length;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					for (int i = 0; i < n; i++)
					{
						action(*(p0 + i));
					}
				}
			}
		}

		/// <summary>
		/// Executes the specified action for each element in the vector that satisfies the given predicate.
		/// </summary>
		/// <param name="vector"> the vector to iterate over </param>
		/// <param name="predicate"> the predicate to test each element against </param>
		/// <param name="action"> the action to execute for each element that satisfies the predicate	</param>
		public static void ForEach(this uint[] vector, Func<uint, bool> predicate, Action<uint> action)
		{
			ArgumentNullException.ThrowIfNull(vector, nameof(vector));
			ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
			ArgumentNullException.ThrowIfNull(action, nameof(action));

			int n = vector.Length;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					for (int i = 0; i < n; i++)
					{
						if (predicate(*(p0 + i)))
						{
							action(*(p0 + i));
						}
					}
				}
			}
		}

		/// <summary>
		/// Executes the specified action for each element in the vector, providing the index and value of each element.
		/// </summary>
		/// <param name="vector"> the vector to iterate over </param>
		/// <param name="action"> the action to execute for each element </param>
		public static void ForEach(this uint[] vector, Action<int, uint> action)
		{
			ArgumentNullException.ThrowIfNull(vector, nameof(vector));
			ArgumentNullException.ThrowIfNull(action, nameof(action));

			int n = vector.Length;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					for (int i = 0; i < n; i++)
					{
						action(i, *(p0 + i));
					}
				}
			}
		}

		/// <summary>
		/// Executes the specified action for each element in the vector that satisfies the given predicate.
		/// </summary>
		/// <param name="vector"> the vector to iterate over </param>
		/// <param name="predicate"> the predicate to test each element against </param>
		/// <param name="action"> the action to execute for each element that satisfies the predicate </param>
		public static void ForEach(this uint[] vector, Func<int, uint, bool> predicate, Action<int, uint> action)
		{
			ArgumentNullException.ThrowIfNull(vector, nameof(vector));
			ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
			ArgumentNullException.ThrowIfNull(action, nameof(action));

			int n = vector.Length;

			unsafe
			{
				fixed (uint* p0 = vector)
				{
					for (int i = 0; i < n; i++)
					{
						if (predicate(i, *(p0 + i)))
						{
							action(i, *(p0 + i));
						}
					}
				}
			}
		}

		extension(uint[])
		{
			public static BitArray operator ==(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi == k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}

			public static BitArray operator !=(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi != k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}


			public static BitArray operator <(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi < k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}

			public static BitArray operator <=(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi <= k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}

			public static BitArray operator >(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi > k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}

			public static BitArray operator >=(uint[] v, uint k)
			{
				var mask = new BitArray(v.Length);

				var n = v.Length;

				unsafe
				{
					fixed (uint* p0 = v)
					{
						for (int i = 0; i < n; i++)
						{
							var pi = p0 + i;
							if (*pi >= k)
								mask[i] = true;
						}
					}
				}

				return mask;
			}
		}
		#endregion



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(this BitArray source)
		{
			int setBits = 0;
			foreach (bool bit in source)
			{
				if (bit) setBits++;
			}
			return setBits;
		}


		public static void ForEach(this BitArray vector, Action<int> action)
		{
			ArgumentNullException.ThrowIfNull(vector, nameof(vector));
			ArgumentNullException.ThrowIfNull(action, nameof(action));

			int n = vector.Length;

			for (int i = 0; i < n; i++)
			{
				action(i);
			}
		}
	}
}
