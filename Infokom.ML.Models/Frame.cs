using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Infokom.ML.Models
{
	public interface IFrame
	{
		public Guid Id { get; }

		public DateTimeOffset Instant { get; }

		public void CopyTo(Span<byte> target);
	}



	public abstract class Frame : IFrame
	{
		protected Frame()
		{
			this.Id = Guid.NewGuid();
			this.Instant = DateTimeOffset.Now;
		}

		public Guid Id { get; }

		public DateTimeOffset Instant { get; }

		public abstract void CopyTo(Span<byte> target);
	}

	public class ByteArrayFrame : Frame
	{
		private readonly byte[] _data;

		public ByteArrayFrame(int length) : base() => _data = new byte[length];

		public override void CopyTo(Span<byte> target) => _data.CopyTo(target);
	}
}
