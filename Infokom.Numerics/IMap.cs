namespace Infokom.Numerics
{
	public interface IMap<TSource, TTarget>
	{
		public TTarget this[TSource source] { get; }
	}
}
