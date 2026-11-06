using Infokom.Numerics.Atomics;

using System.Collections;
using System.Drawing;
using System.Numerics;
using Infokom.Numerics.Extensions;
using System.Runtime.CompilerServices;

namespace Infokom.Numerics
{

	public interface IMatrixial<TMatrix, TElement> : IMatrix<TElement> where TMatrix : IMatrixial<TMatrix, TElement>
	{
		Size<int, int> IMatrix<TElement>.Size => TMatrix.Size;

		public static new abstract Size<int, int> Size { get; }
	}





	public interface IMatrix<TElement> : IMap<Point<int, int>, TElement>
	{
		/// <summary>
		/// Acces an element at the specified row and column indices in the matrix.
		/// </summary>
		/// <param name="i">The row index.</param>
		/// <param name="j">The column index.</param>
		/// <returns>The element at the specified row and column indices.</returns>
		public TElement this[int i, int j] { get; }

		TElement IMap<Point<int, int>, TElement>.this[Point<int, int> source] => this[source.Y, source.X];

		public Size<int, int> Size { get; }
	}

	

	public class Matrix<T> : IMatrix<T>, IEnumerable<T> where T : unmanaged, IEquatable<T>, IEqualityOperators<T, T , bool>
	{
		private readonly int _ni, _nj;
		private readonly T[,] _data;

		protected Matrix(T[,] data)
		{
			ArgumentNullException.ThrowIfNull(data, nameof(data));

			this._data = data;
			(_ni, _nj) = (data.GetLength(0), data.GetLength(1));
		}

		public Size<int, int> Size => (_nj, _ni);

		public T this[int i, int j] => _data[i, j];

		public T this[Point<int, int> index] => _data[index.Y, index.X];


		public IEnumerator<T> GetEnumerator() => this._data.Cast<T>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this._data.GetEnumerator();



		public static Matrix<T> Create(int rows, int cols) => Create(rows, cols);

		public static Matrix<T> Create(int rows, int cols, T value)
		{
			var v = Matrix<T>.Create(rows, cols);

			if (value == default(T))
			{
				_ = Parallel.For(0, rows, row =>
				{
					for (int col = 0; col < cols; col++)
						v._data[row, col] = value;
				});
			}

			return v;
		}

		public static Matrix<T> Create(int rows, int cols, Func<int, int, T> source)
		{
			var v = Matrix<T>.Create(rows, cols);

			_ = Parallel.For(0, cols, col =>
			{
				for (int row = 0; row < rows; row++)
					v._data[row, col] = source(row, col);
			});

			return v;
		}
	}


	public static class Matrix
	{
		extension(Point<int, int> source)
		{
			/// <summary>
			/// Row index of a cell in a grid like structure.
			/// </summary>
			public int Row => source.Y;

			/// <summary>
			/// Column index of a cell in a grid like structure.
			/// </summary>
			public int Col => source.X;
		}

		extension(Size<int, int> source)
		{
			/// <summary>
			/// Number of cells on each row in a grid like structure.
			/// </summary>
			public int Rows => source.Y;

			/// <summary>
			/// Number of cells on each column in a grid like structure.
			/// </summary>
			public int Cols => source.X;
		}


		extension<T>(T[,] source)
		{
			public (int i, int j) LowerBound => (source.GetLowerBound(0), source.GetLowerBound(1));
			public (int i, int j) UpperBound => (source.GetUpperBound(0), source.GetUpperBound(1));
			public (int m, int n) Size => (source.GetLength(0), source.GetLength(1));
		}

		public static Matrix<T> Create<T>(int ni, int nj) where T : unmanaged, IEquatable<T>, IEqualityOperators<T, T, bool> => Matrix<T>.Create(ni, nj);
		public static Matrix<T> Create<T>(int ni, int nj, T value) where T : unmanaged, IEquatable<T>, IEqualityOperators<T, T, bool> => Matrix<T>.Create(ni, nj, value);
		public static Matrix<T> Create<T>(int ni, int nj, Func<int, int, T> source) where T : unmanaged, IEquatable<T>, IEqualityOperators<T, T, bool> => Matrix<T>.Create(ni, nj, source);




		public static T[,] ToArray<T>(this IMatrix<T> source)
		{
			ArgumentNullException.ThrowIfNull(source, nameof(source));

			var (ni, nj) = source.Size;

			var result = new T[ni, nj];

			_ = Parallel.For(0, ni, i =>
			{
				for (int j = 0; j < nj; j++)
				{
					result[i, j] = source[i, j];
				}
			});

			return result;
		}


		public static IEnumerable<(int i, int j, T value)> Cells<T>(this IMatrix<T> source)
		{
			ArgumentNullException.ThrowIfNull(source, nameof(source));
			var (ni, nj) = source.Size;
			for (int i = 0; i < ni; i++)
			{
				for (int j = 0; j < nj; j++)
				{
					yield return (i, j, source[i, j]);
				}
			}
		}
	}
}
