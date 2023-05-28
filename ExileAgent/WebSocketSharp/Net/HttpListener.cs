using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpListener : IDisposable
	{
		static HttpListener()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListener));
			HttpListener._defaultRealm = HttpListener.getString_0(107131889);
		}

		public HttpListener()
		{
			this._authSchemes = AuthenticationSchemes.Anonymous;
			this._contextQueue = new Queue<HttpListenerContext>();
			this._contextRegistry = new LinkedList<HttpListenerContext>();
			this._contextRegistrySync = ((ICollection)this._contextRegistry).SyncRoot;
			this._log = new Logger();
			this._objectName = base.GetType().ToString();
			this._prefixes = new HttpListenerPrefixCollection(this);
			this._waitQueue = new Queue<HttpListenerAsyncResult>();
		}

		internal bool ReuseAddress
		{
			get
			{
				return this._reuseAddress;
			}
			set
			{
				this._reuseAddress = value;
			}
		}

		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._authSchemes;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._authSchemes = value;
			}
		}

		public Func<HttpListenerRequest, AuthenticationSchemes> AuthenticationSchemeSelector
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._authSchemeSelector;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._authSchemeSelector = value;
			}
		}

		public string CertificateFolderPath
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._certFolderPath;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._certFolderPath = value;
			}
		}

		public bool IgnoreWriteExceptions
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._ignoreWriteExceptions;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._ignoreWriteExceptions = value;
			}
		}

		public bool IsListening
		{
			get
			{
				return this._listening;
			}
		}

		public static bool IsSupported
		{
			get
			{
				return true;
			}
		}

		public Logger Log
		{
			get
			{
				return this._log;
			}
		}

		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._prefixes;
			}
		}

		public string Realm
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._realm;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._realm = value;
			}
		}

		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				if (this._sslConfig == null)
				{
					this._sslConfig = new ServerSslConfiguration();
				}
				return this._sslConfig;
			}
		}

		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public Func<IIdentity, NetworkCredential> UserCredentialsFinder
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._userCredFinder;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._userCredFinder = value;
			}
		}

		private HttpListenerAsyncResult beginGetContext(AsyncCallback callback, object state)
		{
			object contextRegistrySync = this._contextRegistrySync;
			HttpListenerAsyncResult result;
			lock (contextRegistrySync)
			{
				if (!this._listening)
				{
					string message = this._disposed ? HttpListener.getString_0(107131871) : HttpListener.getString_0(107131904);
					throw new HttpListenerException(995, message);
				}
				HttpListenerAsyncResult httpListenerAsyncResult = new HttpListenerAsyncResult(callback, state);
				if (this._contextQueue.Count == 0)
				{
					this._waitQueue.Enqueue(httpListenerAsyncResult);
				}
				else
				{
					HttpListenerContext context = this._contextQueue.Dequeue();
					httpListenerAsyncResult.Complete(context, true);
				}
				result = httpListenerAsyncResult;
			}
			return result;
		}

		private void cleanupContextQueue(bool force)
		{
			if (this._contextQueue.Count != 0)
			{
				if (force)
				{
					this._contextQueue.Clear();
				}
				else
				{
					HttpListenerContext[] array = this._contextQueue.ToArray();
					this._contextQueue.Clear();
					foreach (HttpListenerContext httpListenerContext in array)
					{
						httpListenerContext.ErrorStatusCode = 503;
						httpListenerContext.SendError();
					}
				}
			}
		}

		private void cleanupContextRegistry()
		{
			int count = this._contextRegistry.Count;
			if (count != 0)
			{
				HttpListenerContext[] array = new HttpListenerContext[count];
				this._contextRegistry.CopyTo(array, 0);
				this._contextRegistry.Clear();
				foreach (HttpListenerContext httpListenerContext in array)
				{
					httpListenerContext.Connection.Close(true);
				}
			}
		}

		private void cleanupWaitQueue(string message)
		{
			if (this._waitQueue.Count != 0)
			{
				HttpListenerAsyncResult[] array = this._waitQueue.ToArray();
				this._waitQueue.Clear();
				foreach (HttpListenerAsyncResult httpListenerAsyncResult in array)
				{
					HttpListenerException exception = new HttpListenerException(995, message);
					httpListenerAsyncResult.Complete(exception);
				}
			}
		}

		private void close(bool force)
		{
			if (!this._listening)
			{
				this._disposed = true;
			}
			else
			{
				this._listening = false;
				this.cleanupContextQueue(force);
				this.cleanupContextRegistry();
				string message = HttpListener.getString_0(107131871);
				this.cleanupWaitQueue(message);
				EndPointManager.RemoveListener(this);
				this._disposed = true;
			}
		}

		private string getRealm()
		{
			string realm = this._realm;
			return (realm == null || realm.Length <= 0) ? HttpListener._defaultRealm : realm;
		}

		private AuthenticationSchemes selectAuthenticationScheme(HttpListenerRequest request)
		{
			Func<HttpListenerRequest, AuthenticationSchemes> authSchemeSelector = this._authSchemeSelector;
			AuthenticationSchemes result;
			if (authSchemeSelector == null)
			{
				result = this._authSchemes;
			}
			else
			{
				try
				{
					result = authSchemeSelector(request);
				}
				catch
				{
					result = AuthenticationSchemes.None;
				}
			}
			return result;
		}

		internal bool AuthenticateContext(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			AuthenticationSchemes authenticationSchemes = this.selectAuthenticationScheme(request);
			bool result;
			if (authenticationSchemes == AuthenticationSchemes.Anonymous)
			{
				result = true;
			}
			else if (authenticationSchemes == AuthenticationSchemes.None)
			{
				context.ErrorStatusCode = 403;
				context.ErrorMessage = HttpListener.getString_0(107131838);
				context.SendError();
				result = false;
			}
			else
			{
				string realm = this.getRealm();
				IPrincipal principal = HttpUtility.CreateUser(request.Headers[HttpListener.getString_0(107137883)], authenticationSchemes, realm, request.HttpMethod, this._userCredFinder);
				if (principal == null || !principal.Identity.IsAuthenticated)
				{
					context.SendAuthenticationChallenge(authenticationSchemes, realm);
					result = false;
				}
				else
				{
					context.User = principal;
					result = true;
				}
			}
			return result;
		}

		internal void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
		}

		internal bool RegisterContext(HttpListenerContext context)
		{
			bool result;
			if (!this._listening)
			{
				result = false;
			}
			else
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					if (!this._listening)
					{
						result = false;
					}
					else
					{
						context.Listener = this;
						this._contextRegistry.AddLast(context);
						if (this._waitQueue.Count == 0)
						{
							this._contextQueue.Enqueue(context);
						}
						else
						{
							HttpListenerAsyncResult httpListenerAsyncResult = this._waitQueue.Dequeue();
							httpListenerAsyncResult.Complete(context, false);
						}
						result = true;
					}
				}
			}
			return result;
		}

		internal void UnregisterContext(HttpListenerContext context)
		{
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				this._contextRegistry.Remove(context);
			}
		}

		public void Abort()
		{
			if (!this._disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					if (!this._disposed)
					{
						this.close(true);
					}
				}
			}
		}

		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			if (this._prefixes.Count == 0)
			{
				string message = HttpListener.getString_0(107131801);
				throw new InvalidOperationException(message);
			}
			if (!this._listening)
			{
				string message2 = HttpListener.getString_0(107131736);
				throw new InvalidOperationException(message2);
			}
			return this.beginGetContext(callback, state);
		}

		public void Close()
		{
			if (!this._disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					if (!this._disposed)
					{
						this.close(false);
					}
				}
			}
		}

		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException(HttpListener.getString_0(107131655));
			}
			HttpListenerAsyncResult httpListenerAsyncResult = asyncResult as HttpListenerAsyncResult;
			if (httpListenerAsyncResult == null)
			{
				string message = HttpListener.getString_0(107131670);
				throw new ArgumentException(message, HttpListener.getString_0(107131655));
			}
			object syncRoot = httpListenerAsyncResult.SyncRoot;
			lock (syncRoot)
			{
				if (httpListenerAsyncResult.EndCalled)
				{
					string message2 = HttpListener.getString_0(107131085);
					throw new InvalidOperationException(message2);
				}
				httpListenerAsyncResult.EndCalled = true;
			}
			if (!httpListenerAsyncResult.IsCompleted)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		public HttpListenerContext GetContext()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			if (this._prefixes.Count == 0)
			{
				string message = HttpListener.getString_0(107131801);
				throw new InvalidOperationException(message);
			}
			if (!this._listening)
			{
				string message2 = HttpListener.getString_0(107131736);
				throw new InvalidOperationException(message2);
			}
			HttpListenerAsyncResult httpListenerAsyncResult = this.beginGetContext(null, null);
			httpListenerAsyncResult.EndCalled = true;
			if (!httpListenerAsyncResult.IsCompleted)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		public void Start()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				if (!this._listening)
				{
					EndPointManager.AddListener(this);
					this._listening = true;
				}
			}
		}

		public void Stop()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				if (this._listening)
				{
					this._listening = false;
					this.cleanupContextQueue(false);
					this.cleanupContextRegistry();
					string message = HttpListener.getString_0(107131904);
					this.cleanupWaitQueue(message);
					EndPointManager.RemoveListener(this);
				}
			}
		}

		void IDisposable.Dispose()
		{
			if (!this._disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					if (!this._disposed)
					{
						this.close(true);
					}
				}
			}
		}

		private AuthenticationSchemes _authSchemes;

		private Func<HttpListenerRequest, AuthenticationSchemes> _authSchemeSelector;

		private string _certFolderPath;

		private Queue<HttpListenerContext> _contextQueue;

		private LinkedList<HttpListenerContext> _contextRegistry;

		private object _contextRegistrySync;

		private static readonly string _defaultRealm;

		private bool _disposed;

		private bool _ignoreWriteExceptions;

		private volatile bool _listening;

		private Logger _log;

		private string _objectName;

		private HttpListenerPrefixCollection _prefixes;

		private string _realm;

		private bool _reuseAddress;

		private ServerSslConfiguration _sslConfig;

		private Func<IIdentity, NetworkCredential> _userCredFinder;

		private Queue<HttpListenerAsyncResult> _waitQueue;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
