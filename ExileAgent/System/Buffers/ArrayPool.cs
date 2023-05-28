using System;
using System.Threading;

namespace System.Buffers
{
	public abstract class ArrayPool<T>
	{
		public static ArrayPool<T> Shared
		{
			get
			{
				return Volatile.Read<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance) ?? ArrayPool<T>.EnsureSharedCreated();
			}
		}

		private static ArrayPool<T> EnsureSharedCreated()
		{
			Interlocked.CompareExchange<ArrayPool<T>>(ref ArrayPool<T>.s_sharedInstance, ArrayPool<T>.Create(), null);
			return ArrayPool<T>.s_sharedInstance;
		}

		public static ArrayPool<T> Create()
		{
			return new DefaultArrayPool<T>();
		}

		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new DefaultArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		public abstract T[] Rent(int minimumLength);

		public abstract void Return(T[] array, bool clearArray = false);

		private static ArrayPool<T> s_sharedInstance;
	}
}
