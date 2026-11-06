using Infokom.Numerics.Atomics;

using System.Runtime.InteropServices;

using static Infokom.Numerics.Extensions.Functions;

namespace Infokom.Numerics
{
	public class Heatmap
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct Cell
		{
			private const float σ = 0.21233045f;
			private const float Rσ = 0.2f, Gσ = 0.2f, Bσ = 0.2f;
			private const float Rμ = 1.0f, Gμ = 0.5f, Bμ = 0.0f;

			private const float TwoSigmaSquared = 0.090168439994405f;


			[FieldOffset(0)] private readonly uint _data;
			[FieldOffset(0)] private byte _x;

			[FieldOffset(1)] private RGB24 _color;
			[FieldOffset(1)] private byte _b;
			[FieldOffset(2)] private byte _g;
			[FieldOffset(3)] private byte _r;

			public Cell(byte x)
			{
				//if (TryGetColor(x / 255f, out var b, out var g, out var r))
				//{
				//	_b = (byte)round(b * 255f);
				//	_g = (byte)round(g * 255f);
				//	_r = (byte)round(r * 255f);
				//}

				_color = GetColor(x/255f);

				_x = x;
			}


			public readonly byte B => _b;

			public readonly byte G => _g;

			public readonly byte R => _r;

			public float X
			{
				readonly get => _x / 255f;
				set
				{
					var x = value;
					if (TryGetColor(x, out var b, out var g, out var r))
					{
						_b = (byte)(round(b * 255f));
						_g = (byte)(round(g * 255f));
						_r = (byte)(round(r * 255f));
					}
					else
					{
						_b = 0;
						_g = 0;
						_r = 0;
					}
					_x = (byte)(round(x * 255f));
				}
			}

			public readonly RGB24 Color => _color;


			public static implicit operator Cell(byte x) => new(x);



			private static bool TryGetColor(double x, out double b, out double g, out double r)
			{
				
				if (x is >= 0.0 and <= 1.0)
				{
					(b, g, r) = x <= 0.5 ? (1 - 2 * x, 2 * x, 0.0) : (0.0, 1.0 - 2 * (x - 0.5), 2 * (x - 0.5));
					return true;
				}

				r = g = b = 0.0;
				return false;
			}


			private static RGB24 GetColor(float x)
			{
				var (b, g, r) = (0.0f, 0.0f, 0.0f);

				if(x is >= 0 and <= 1)
				{
					b = gauss(x, Bμ, Bσ);
					g = gauss(x, Gμ, Gσ);
					r = gauss(x, Rμ, Rσ);
				}

				return RGB24.Create(r, g, b);
			}

			private static byte FromRgb(byte r, byte g, byte b)
			{
				float x;

				if (b >= r)
				{
					// Blue-Green half: G / B
					x = 0.25f + 2 * σ * σ * ln((float)g / b);
				}
				else
				{
					// Green-Red half: G / R
					x = 0.75f - 2 * σ * σ * ln((float)g / r);
				}

				x = clamp(x, 0.0f, 1.0f);

				return (byte)round(x * byte.MaxValue);
			}
		}

		private readonly Heatmap.Cell[,] _cells;

		public int Rows { get; }

		public int Columns { get; }

		public Heatmap.Cell this[int row, int column]
		{
			get => _cells[row, column];
			set => _cells[row, column] = value;
		}

		public Heatmap(int rows, int columns)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(rows);
			ArgumentOutOfRangeException.ThrowIfNegative(columns);

			Rows = rows;
			Columns = columns;

			_cells = new Heatmap.Cell[rows, columns];
		}
	}
}
