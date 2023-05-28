using System;
using System.Collections;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerPrefixCollection : IEnumerable<string>, IEnumerable, ICollection<string>
	{
		internal HttpListenerPrefixCollection(HttpListener listener)
		{
			this._listener = listener;
			this._prefixes = new List<string>();
		}

		public int Count
		{
			get
			{
				return this._prefixes.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public void Add(string uriPrefix)
		{
			this._listener.CheckDisposed();
			HttpListenerPrefix.CheckPrefix(uriPrefix);
			if (!this._prefixes.Contains(uriPrefix))
			{
				if (this._listener.IsListening)
				{
					EndPointManager.AddPrefix(uriPrefix, this._listener);
				}
				this._prefixes.Add(uriPrefix);
			}
		}

		public void Clear()
		{
			this._listener.CheckDisposed();
			if (this._listener.IsListening)
			{
				EndPointManager.RemoveListener(this._listener);
			}
			this._prefixes.Clear();
		}

		public bool Contains(string uriPrefix)
		{
			this._listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new ArgumentNullException(HttpListenerPrefixCollection.getString_0(107131392));
			}
			return this._prefixes.Contains(uriPrefix);
		}

		public void CopyTo(string[] array, int offset)
		{
			this._listener.CheckDisposed();
			this._prefixes.CopyTo(array, offset);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return this._prefixes.GetEnumerator();
		}

		public bool Remove(string uriPrefix)
		{
			this._listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new ArgumentNullException(HttpListenerPrefixCollection.getString_0(107131392));
			}
			bool result;
			if (!this._prefixes.Contains(uriPrefix))
			{
				result = false;
			}
			else
			{
				if (this._listener.IsListening)
				{
					EndPointManager.RemovePrefix(uriPrefix, this._listener);
				}
				result = this._prefixes.Remove(uriPrefix);
			}
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._prefixes.GetEnumerator();
		}

		static HttpListenerPrefixCollection()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerPrefixCollection));
		}

		private HttpListener _listener;

		private List<string> _prefixes;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
