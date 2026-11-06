namespace Infokom.Numerics.Attributes
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class SymbolAttribute(char symbol) : Attribute
	{
		public char Symbol { get; } = symbol;
	}


	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LabelAttribute(string label) : Attribute
	{
		public string Label { get; } = label;
	}
}
