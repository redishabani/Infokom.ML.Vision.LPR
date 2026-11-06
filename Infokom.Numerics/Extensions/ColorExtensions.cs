using System.Drawing;

namespace Infokom.Numerics.Extensions
{
	public static class ColorExtensions
	{
		/// <summary>
		/// Deconstructs a <see cref="Color">color</see> into its individual byte components: (<see cref="Color.B">b</see>, <see cref="Color.G">g</see>, <see cref="Color.R">r</see>, <see cref="Color.A">α</see>)
		/// </summary>
		/// <param name="source">The source Color to deconstruct.</param>
		/// <param name="b">The Blue component.</param>
		/// <param name="g">The Green component.</param>
		/// <param name="r">The Red component.</param>
		/// <param name="α">The Alpha component.</param>
		/// <remarks>
		/// The order of the components is <see cref="Color.B">b</see>, <see cref="Color.G">g</see>, <see cref="Color.R">r</see>, and <see cref="Color.A">α</see>, which is consistent with the internal byte representation of the Color structure in .NET.
		/// </remarks>
		public static void Deconstruct(this Color source, out byte b, out byte g, out byte r, out byte α) => (b, g, r, α) = (source.B, source.G, source.R, source.A);

		/// <summary>
		/// Deconstructs a <see cref="Color">color</see> into its individual byte components: (<see cref="Color.B">b</see>, <see cref="Color.G">g</see>, <see cref="Color.R">r</see>), ignoring the <see cref="Color.A">α</see> component.
		/// </summary>
		/// <param name="source">The source Color to deconstruct.</param>
		/// <param name="b">The Blue component.</param>
		/// <param name="g">The Green component.</param>
		/// <param name="r">The Red component.</param>
		/// <remarks>
		/// The <see cref="Color.A">α</see> component is ignored in this deconstruction. The order of the components is <see cref="Color.B">b</see>, <see cref="Color.G">g</see>, and <see cref="Color.R">r</see>, which is consistent with the internal byte representation of the Color structure in .NET.
		/// </remarks>
		public static void Deconstruct(this Color source, out byte b, out byte g, out byte r) => (b, g, r, _) = source;
	}
}
