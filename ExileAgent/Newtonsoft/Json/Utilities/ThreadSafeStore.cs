using System;
using System.Collections.Concurrent;
using ns20;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class ThreadSafeStore<TKey, TValue>
	{
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, Class401.smethod_0(107334234));
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		private readonly Func<TKey, TValue> _creator;
	}
}
