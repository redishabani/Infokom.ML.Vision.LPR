namespace Infokom.Numerics.Extensions
{
	public static class AggregationOperations
	{
		extension<T>(ValueTuple<T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1) = source;

				x = aggregator(x, x1);

				return x;
			}
		}

		extension<T>(ValueTuple<T, T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1, x2) = source;

				x = aggregator(x, x1);
				x = aggregator(x, x2);

				return x;
			}
		}

		extension<T>(ValueTuple<T, T, T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1, x2, x3) = source;

				x = aggregator(x, x1);
				x = aggregator(x, x2);
				x = aggregator(x, x3);

				return x;
			}
		}

		extension<T>(ValueTuple<T, T, T, T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1, x2, x3, x4) = source;

				x = aggregator(x, x1);
				x = aggregator(x, x2);
				x = aggregator(x, x3);
				x = aggregator(x, x4);

				return x;
			}
		}

		extension<T>(ValueTuple<T, T, T, T, T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1, x2, x3, x4, x5) = source;

				x = aggregator(x, x1);
				x = aggregator(x, x2);
				x = aggregator(x, x3);
				x = aggregator(x, x4);
				x = aggregator(x, x5);

				return x;
			}
		}

		extension<T>(ValueTuple<T, T, T, T, T, T, T> source)
		{
			public T Aggregate(Func<T, T, T> aggregator)
			{
				var (x, x1, x2, x3, x4, x5, x6) = source;

				x = aggregator(x, x1);
				x = aggregator(x, x2);
				x = aggregator(x, x3);
				x = aggregator(x, x4);
				x = aggregator(x, x5);
				x = aggregator(x, x6);

				return x;
			}
		}

		extension<TSource, TTarget>(ValueTuple<TSource, TSource, TSource, TSource, TSource, TSource, TSource> source)
		{
			public TTarget Aggregate(TTarget seed, Func<TTarget, TSource, TTarget> aggregator)
			{
				var (x0, x1, x2, x3, x4, x5, x6) = source;

				var x = seed;

				x = aggregator(x, x0);
				x = aggregator(x, x1);
				x = aggregator(x, x2);
				x = aggregator(x, x3);
				x = aggregator(x, x4);
				x = aggregator(x, x5);
				x = aggregator(x, x6);

				return x;
			}
		}


		extension(ValueTuple<ulong, ulong, ulong, ulong, ulong, ulong, ulong> source)
		{

		}
	}
}
