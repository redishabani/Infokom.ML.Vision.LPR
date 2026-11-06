namespace Infokom.Numerics.Attributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class IndexAttribute(int index) : IndexAttribute<int>(index);
}
