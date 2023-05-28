using System;

namespace System.Buffers
{
	public abstract class MemoryPool<T> : IDisposable
	{
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		public abstract int MaxBufferSize { get; }

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected abstract void Dispose(bool disposing);

		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
