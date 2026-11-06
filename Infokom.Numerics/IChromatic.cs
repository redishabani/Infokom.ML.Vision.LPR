//using static Infokom.Numerics.Atomics.FloatingPointConstant;
using System.Drawing;
using System.Numerics;

namespace Infokom.Numerics
{
	/// <summary>
	/// χρωματικός (khrōmatikós; "colored")
	/// </summary>
	/// <typeparam name="TChromatic"></typeparam>
	public interface IChromatic<TChromatic> : IEquatable<TChromatic>, IEqualityOperators<TChromatic, TChromatic, bool> where TChromatic : IChromatic<TChromatic>
	{
		public static abstract explicit operator Color(TChromatic source);
		public static abstract explicit operator TChromatic(Color source);
	}
}