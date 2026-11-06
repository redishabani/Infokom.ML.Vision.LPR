namespace Infokom.Numerics.Attributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class IDAttribute(ulong id) : IDAttribute<ulong>(id);
}
