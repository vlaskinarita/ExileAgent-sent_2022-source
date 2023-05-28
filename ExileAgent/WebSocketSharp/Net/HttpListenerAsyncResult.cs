using System;
using System.Threading;

namespace WebSocketSharp.Net
{
	internal sealed class HttpListenerAsyncResult : IAsyncResult
	{
		internal HttpListenerAsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
			this._sync = new object();
		}

		internal HttpListenerContext Context
		{
			get
			{
				if (this._exception != null)
				{
					throw this._exception;
				}
				return this._context;
			}
		}

		internal bool EndCalled
		{
			get
			{
				return this._endCalled;
			}
			set
			{
				this._endCalled = value;
			}
		}

		internal object SyncRoot
		{
			get
			{
				return this._sync;
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
				return this._completedSynchronously;
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

		private void complete()
		{
			object sync = this._sync;
			lock (sync)
			{
				this._completed = true;
				if (this._waitHandle != null)
				{
					this._waitHandle.Set();
				}
			}
			if (this._callback != null)
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					try
					{
						this._callback(this);
					}
					catch
					{
					}
				}, null);
			}
		}

		internal void Complete(Exception exception)
		{
			this._exception = exception;
			this.complete();
		}

		internal void Complete(HttpListenerContext context, bool completedSynchronously)
		{
			this._context = context;
			this._completedSynchronously = completedSynchronously;
			this.complete();
		}

		private AsyncCallback _callback;

		private bool _completed;

		private bool _completedSynchronously;

		private HttpListenerContext _context;

		private bool _endCalled;

		private Exception _exception;

		private object _state;

		private object _sync;

		private ManualResetEvent _waitHandle;
	}
}
