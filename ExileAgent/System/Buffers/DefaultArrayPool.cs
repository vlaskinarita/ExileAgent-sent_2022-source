using System;
using System.Diagnostics;
using System.Threading;
using ns20;

namespace System.Buffers
{
	internal sealed class DefaultArrayPool<T> : ArrayPool<T>
	{
		internal DefaultArrayPool() : this(1048576, 50)
		{
		}

		internal DefaultArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException(Class401.smethod_0(107134877));
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException(Class401.smethod_0(107134824));
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			int num = Utilities.SelectBucketIndex(maxArrayLength);
			DefaultArrayPool<T>.Bucket[] array = new DefaultArrayPool<T>.Bucket[num + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DefaultArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException(Class401.smethod_0(107134799));
			}
			if (minimumLength == 0)
			{
				T[] result;
				if ((result = DefaultArrayPool<T>.s_emptyArray) == null)
				{
					result = (DefaultArrayPool<T>.s_emptyArray = new T[0]);
				}
				return result;
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				do
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						goto Block_7;
					}
					if (++num2 >= this._buckets.Length)
					{
						break;
					}
				}
				while (num2 != num + 2);
				goto IL_9F;
				Block_7:
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_9F:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				log.BufferRented(hashCode, array.Length, this.Id, -1);
				log.BufferAllocated(hashCode, array.Length, this.Id, -1, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException(Class401.smethod_0(107289482));
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		private const int DefaultMaxArrayLength = 1048576;

		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		private static T[] s_emptyArray;

		private readonly DefaultArrayPool<T>.Bucket[] _buckets;

		private sealed class Bucket
		{
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = (array == null);
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException(SR.ArgumentException_BufferNotFromPool, Class401.smethod_0(107289482));
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			internal readonly int _bufferLength;

			private readonly T[][] _buffers;

			private readonly int _poolId;

			private SpinLock _lock;

			private int _index;
		}
	}
}
