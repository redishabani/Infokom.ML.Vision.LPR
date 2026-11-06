namespace Infokom.Numerics
{
	public interface IParser<TTarget>
	{
		TTarget Parse(string source);
		bool TryParse(string source, out TTarget result);
	}

}