
using Infokom.Numerics.Atomics;

using System.Numerics;

namespace Infokom.Numerics
{
	/// <summary>
	/// Represents an emission matrix or a bounded space of existence (a spectrum) 
	/// where the edge values of a contiguous bounding box circumscribe the absolute limits 
	/// outside of which no entity emission can occur.
	/// </summary>
	/// <typeparam name="Tx">The unmanaged, numeric primitive type used as a selector.</typeparam>
	/// <typeparam name="T">The type of the emitted entity existing within this spectrum.</typeparam>
	public interface ISpectrum<Tx, T> where Tx : unmanaged, INumber<Tx>, IMinMaxValue<Tx>
	{
		/// <summary>
		/// Gets the contiguous bounding box (containing no internal gaps) outside of which there is no emission.
		/// </summary>
		/// <value>
		/// A <see cref="Range{Tx}"/> instance defining the absolute spatial bounds of the spectrum.
		/// </value>
		/// <remarks>
		/// <b>Mathematical Necessity vs. Sufficiency:</b>
		/// This range being non-empty is a strict prerequisite (necessity) for the spectrum to contain emissions. 
		/// Conversely, if this range is empty (<see cref="Range{Tx}.IsEmpty"/>), then the spectrum is 
		/// mathematically guaranteed to be empty. However, a non-empty range does not inherently guarantee 
		/// that an emission exists at every single discrete coordinate within it.
		/// </remarks>
		public Range<Tx> Range { get; }

		/// <summary>
		/// Selects, resolves, or reconstitutes the unique entity instance of <typeparamref name="T"/> 
		/// associated with the specified property descriptor profile.
		/// </summary>
		/// <param name="profile">
		/// An open sequence of scalar values acting as the conceptual descriptor profile of the entity.
		/// </param>
		/// <returns>The universally unique, pre-existing instance of the entity <typeparamref name="T"/>.</returns>
		/// <remarks>
		/// <b>Design Note on Intentional Abstraction Pathways:</b><br/>
		/// At this conceptual phase, this indexer deliberately avoids low-level optimization primitives 
		/// (such as memory spans) to keep the API focused entirely on pure domain semantics. 
		/// <para/>
		/// By expressing the profile as an open array sequence (<c>params Tx[]</c>), we maintain high 
		/// developer ergonomics at the call site while leaving the concrete backing strategy fully open. 
		/// A concrete implementation of this interface is free to process this collection as a standard array, 
		/// wrap it inside a <see cref="ReadOnlySpan{T}"/> internally for execution, or leverage compiler-level 
		/// pattern matching to optimize the sequence translation down the road without modifying this core abstraction.
		/// </remarks>
		/// <exception cref="ArgumentException">Thrown when the provided profile descriptor is empty or malformed.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the resolved key falls outside the defined <see cref="Range"/>.</exception>
		public T this[params Tx[] profile] { get; }
	}
}
