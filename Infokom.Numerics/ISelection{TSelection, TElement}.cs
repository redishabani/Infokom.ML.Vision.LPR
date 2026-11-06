namespace Infokom.Numerics
{
	public interface ISelection<TSelection, TOption> : ISelection<TSelection>, IReadOnlyCollection<TOption> where TSelection : ISelection<TSelection, TOption>
	{
		public static abstract TSelection Select(TOption option);
	}
}
