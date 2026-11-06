namespace Infokom.Numerics
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TSelection"></typeparam>
	public interface ISelection<TSelection> where TSelection : ISelection<TSelection>
	{

		public static abstract TSelection Empty { get; }

		/// <summary>
		/// Complement
		/// </summary>
		/// <param name="s1"></param>
		/// <returns></returns>
		public static abstract TSelection operator ~(TSelection s1);

		/// <summary>
		/// Intersection
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns><paramref name="a"/> ∩ <paramref name="b"/></returns>
		public static abstract TSelection operator &(TSelection a, TSelection b);

		/// <summary>
		/// Union
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns><paramref name="a"/> ∪ <paramref name="b"/></returns>
		public static abstract TSelection operator |(TSelection a, TSelection b);

		/// <summary>
		/// Symmetric difference
		/// </summary>
		/// <param name="s1"></param>
		/// <param name="s2"></param>
		/// <returns><paramref name="a"/> △ <paramref name="b"/></returns>
		public static abstract TSelection operator ^(TSelection a, TSelection b);
	}
}
