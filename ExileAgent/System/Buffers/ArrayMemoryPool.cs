using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		protected sealed override void Dispose(bool disposing)
		{
		}

		private const int s_maxBufferSize = 2147483647;

		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			private T[] _array;
		}
	}
}
