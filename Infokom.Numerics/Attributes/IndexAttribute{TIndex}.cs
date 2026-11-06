using System.Numerics;

namespace Infokom.Numerics.Attributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public class IndexAttribute<TIndex>(TIndex index) : Attribute where TIndex : unmanaged, IBinaryInteger<TIndex>, ISignedNumber<TIndex>
	{
		public TIndex Index { get; } = index;
	}
}
