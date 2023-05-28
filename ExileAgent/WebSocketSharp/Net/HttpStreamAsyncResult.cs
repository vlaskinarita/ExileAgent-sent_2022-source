using System;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal sealed class HttpStreamAsyncResult : IAsyncResult
	{
		internal HttpStreamAsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
			this._sync = new object();
		}

		internal byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
			set
			{
				this._buffer = value;
			}
		}

		internal int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		internal Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		internal bool HasException
		{
			get
			{
				return this._exception != null;
			}
		}

		internal int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		internal int SyncRead
		{
			get
			{
				return this._syncRead;
			}
			set
			{
				this._syncRead = value;
			}
		}

		public object AsyncState
		{
			get
			{
				return this._state;
			}
		}

		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object sync = this._sync;
				WaitHandle waitHandle;
				lock (sync)
				{
					if (this._waitHandle == null)
					{
						this._waitHandle = new ManualResetEvent(this._completed);
					}
					waitHandle = this._waitHandle;
				}
				return waitHandle;
			}
		}

		public bool CompletedSynchronously
		{
			get
			{
				return this._syncRead == this._count;
			}
		}

		public bool IsCompleted
		{
			get
			{
				object sync = this._sync;
				bool completed;
				lock (sync)
				{
					completed = this._completed;
				}
				return completed;
			}
		}

		internal void Complete()
		{
			object sync = this._sync;
			lock (sync)
			{
				if (!this._completed)
				{
					this._completed = true;
					if (this._waitHandle != null)
					{
						this._waitHandle.Set();
					}
					if (this._callback != null)
					{
						this._callback.BeginInvoke(this, delegate(IAsyncResult ar)
						{
							this._callback.EndInvoke(ar);
						}, null);
					}
				}
			}
		}

		internal void Complete(Exception exception)
		{
			object sync = this._sync;
			lock (sync)
			{
				if (!this._completed)
				{
					this._completed = true;
					this._exception = exception;
					if (this._waitHandle != null)
					{
						this._waitHandle.Set();
					}
					if (this._callback != null)
					{
						this._callback.BeginInvoke(this, delegate(IAsyncResult ar)
						{
							this._callback.EndInvoke(ar);
						}, null);
					}
				}
			}
		}

		private byte[] _buffer;

		private AsyncCallback _callback;

		private bool _completed;

		private int _count;

		private Exception _exception;

		private int _offset;

		private object _state;

		private object _sync;

		private int _syncRead;

		private ManualResetEvent _waitHandle;
	}
}
