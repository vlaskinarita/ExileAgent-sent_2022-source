using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public sealed class HttpServer
	{
		public HttpServer()
		{
			this.init(HttpServer.getString_0(107450200), IPAddress.Any, 80, false);
		}

		public HttpServer(int port) : this(port, port == 443)
		{
		}

		public HttpServer(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(HttpServer.getString_0(107404330));
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(HttpServer.getString_0(107140599), HttpServer.getString_0(107404330));
			}
			Uri uri;
			string message;
			if (!HttpServer.tryCreateUri(url, out uri, out message))
			{
				throw new ArgumentException(message, HttpServer.getString_0(107404330));
			}
			string dnsSafeHost = uri.GetDnsSafeHost(true);
			IPAddress ipaddress = dnsSafeHost.ToIPAddress();
			if (ipaddress == null)
			{
				message = HttpServer.getString_0(107160199);
				throw new ArgumentException(message, HttpServer.getString_0(107404330));
			}
			if (!ipaddress.IsLocal())
			{
				message = HttpServer.getString_0(107160094);
				throw new ArgumentException(message, HttpServer.getString_0(107404330));
			}
			this.init(dnsSafeHost, ipaddress, uri.Port, uri.Scheme == HttpServer.getString_0(107364059));
		}

		public HttpServer(int port, bool secure)
		{
			if (!port.IsPortNumber())
			{
				string message = HttpServer.getString_0(107160021);
				throw new ArgumentOutOfRangeException(HttpServer.getString_0(107132475), message);
			}
			this.init(HttpServer.getString_0(107450200), IPAddress.Any, port, secure);
		}

		public HttpServer(IPAddress address, int port) : this(address, port, port == 443)
		{
		}

		public HttpServer(IPAddress address, int port, bool secure)
		{
			if (address == null)
			{
				throw new ArgumentNullException(HttpServer.getString_0(107251978));
			}
			if (!address.IsLocal())
			{
				string message = HttpServer.getString_0(107159964);
				throw new ArgumentException(message, HttpServer.getString_0(107251978));
			}
			if (!port.IsPortNumber())
			{
				string message2 = HttpServer.getString_0(107160021);
				throw new ArgumentOutOfRangeException(HttpServer.getString_0(107132475), message2);
			}
			this.init(address.ToString(true), address, port, secure);
		}

		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this._listener.AuthenticationSchemes;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						this._listener.AuthenticationSchemes = value;
					}
				}
			}
		}

		public string DocumentRootPath
		{
			get
			{
				return this._docRootPath;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(HttpServer.getString_0(107458190));
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(HttpServer.getString_0(107140599), HttpServer.getString_0(107458190));
				}
				value = value.TrimSlashOrBackslashFromEnd();
				if (value == HttpServer.getString_0(107381125))
				{
					throw new ArgumentException(HttpServer.getString_0(107159917), HttpServer.getString_0(107458190));
				}
				if (value == HttpServer.getString_0(107400882))
				{
					throw new ArgumentException(HttpServer.getString_0(107159917), HttpServer.getString_0(107458190));
				}
				if (value.Length == 2 && value[1] == ':')
				{
					throw new ArgumentException(HttpServer.getString_0(107159917), HttpServer.getString_0(107458190));
				}
				string text = null;
				try
				{
					text = Path.GetFullPath(value);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(HttpServer.getString_0(107159860), HttpServer.getString_0(107458190), innerException);
				}
				if (text == HttpServer.getString_0(107381125))
				{
					throw new ArgumentException(HttpServer.getString_0(107159917), HttpServer.getString_0(107458190));
				}
				text = text.TrimSlashOrBackslashFromEnd();
				if (text.Length == 2 && text[1] == ':')
				{
					throw new ArgumentException(HttpServer.getString_0(107159917), HttpServer.getString_0(107458190));
				}
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						this._docRootPath = value;
					}
				}
			}
		}

		public bool IsListening
		{
			get
			{
				return this._state == ServerState.Start;
			}
		}

		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		public bool KeepClean
		{
			get
			{
				return this._services.KeepClean;
			}
			set
			{
				this._services.KeepClean = value;
			}
		}

		public Logger Log
		{
			get
			{
				return this._log;
			}
		}

		public int Port
		{
			get
			{
				return this._port;
			}
		}

		public string Realm
		{
			get
			{
				return this._listener.Realm;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						this._listener.Realm = value;
					}
				}
			}
		}

		public bool ReuseAddress
		{
			get
			{
				return this._listener.ReuseAddress;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						this._listener.ReuseAddress = value;
					}
				}
			}
		}

		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (!this._secure)
				{
					string message = HttpServer.getString_0(107160435);
					throw new InvalidOperationException(message);
				}
				return this._listener.SslConfiguration;
			}
		}

		public Func<IIdentity, NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return this._listener.UserCredentialsFinder;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						this._listener.UserCredentialsFinder = value;
					}
				}
			}
		}

		public TimeSpan WaitTime
		{
			get
			{
				return this._services.WaitTime;
			}
			set
			{
				this._services.WaitTime = value;
			}
		}

		public WebSocketServiceManager WebSocketServices
		{
			get
			{
				return this._services;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnConnect;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnDelete;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnGet;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnHead;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnOptions;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnPost;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnPut;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<HttpRequestEventArgs> OnTrace;

		private void abort()
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._state != ServerState.Start)
				{
					return;
				}
				this._state = ServerState.ShuttingDown;
			}
			try
			{
				try
				{
					this._services.Stop(1006, string.Empty);
				}
				finally
				{
					this._listener.Abort();
				}
			}
			catch
			{
			}
			this._state = ServerState.Stop;
		}

		private bool canSet(out string message)
		{
			message = null;
			bool result;
			if (this._state == ServerState.Start)
			{
				message = HttpServer.getString_0(107160370);
				result = false;
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				message = HttpServer.getString_0(107160357);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private bool checkCertificate(out string message)
		{
			message = null;
			bool flag = this._listener.SslConfiguration.ServerCertificate != null;
			string certificateFolderPath = this._listener.CertificateFolderPath;
			bool flag2 = EndPointListener.CertificateExists(this._port, certificateFolderPath);
			bool result;
			if (!flag && !flag2)
			{
				message = HttpServer.getString_0(107159483);
				result = false;
			}
			else
			{
				if (flag && flag2)
				{
					string message2 = HttpServer.getString_0(107159827);
					this._log.Warn(message2);
				}
				result = true;
			}
			return result;
		}

		private static HttpListener createListener(string hostname, int port, bool secure)
		{
			HttpListener httpListener = new HttpListener();
			string arg = secure ? HttpServer.getString_0(107364059) : HttpServer.getString_0(107140746);
			string uriPrefix = string.Format(HttpServer.getString_0(107159782), arg, hostname, port);
			httpListener.Prefixes.Add(uriPrefix);
			return httpListener;
		}

		private void init(string hostname, IPAddress address, int port, bool secure)
		{
			this._hostname = hostname;
			this._address = address;
			this._port = port;
			this._secure = secure;
			this._docRootPath = HttpServer.getString_0(107159729);
			this._listener = HttpServer.createListener(this._hostname, this._port, this._secure);
			this._log = this._listener.Log;
			this._services = new WebSocketServiceManager(this._log);
			this._sync = new object();
		}

		private void processRequest(HttpListenerContext context)
		{
			string httpMethod = context.Request.HttpMethod;
			EventHandler<HttpRequestEventArgs> eventHandler = (httpMethod == HttpServer.getString_0(107458181)) ? this.OnGet : ((httpMethod == HttpServer.getString_0(107142671)) ? this.OnHead : ((httpMethod == HttpServer.getString_0(107380455)) ? this.OnPost : ((httpMethod == HttpServer.getString_0(107142662)) ? this.OnPut : ((httpMethod == HttpServer.getString_0(107142657)) ? this.OnDelete : ((httpMethod == HttpServer.getString_0(107142616)) ? this.OnConnect : ((httpMethod == HttpServer.getString_0(107142635)) ? this.OnOptions : ((httpMethod == HttpServer.getString_0(107142590)) ? this.OnTrace : null)))))));
			if (eventHandler == null)
			{
				context.ErrorStatusCode = 501;
				context.SendError();
			}
			else
			{
				HttpRequestEventArgs e = new HttpRequestEventArgs(context, this._docRootPath);
				eventHandler(this, e);
				context.Response.Close();
			}
		}

		private void processRequest(HttpListenerWebSocketContext context)
		{
			Uri requestUri = context.RequestUri;
			if (requestUri == null)
			{
				context.Close(HttpStatusCode.BadRequest);
			}
			else
			{
				string text = requestUri.AbsolutePath;
				if (text.IndexOfAny(new char[]
				{
					'%',
					'+'
				}) > -1)
				{
					text = HttpUtility.UrlDecode(text, Encoding.UTF8);
				}
				WebSocketServiceHost webSocketServiceHost;
				if (!this._services.InternalTryGetServiceHost(text, out webSocketServiceHost))
				{
					context.Close(HttpStatusCode.NotImplemented);
				}
				else
				{
					webSocketServiceHost.StartSession(context);
				}
			}
		}

		private void receiveRequest()
		{
			for (;;)
			{
				HttpListenerContext ctx = null;
				try
				{
					ctx = this._listener.GetContext();
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						try
						{
							if (ctx.Request.IsUpgradeRequest(HttpServer.<>c__DisplayClass83_0.getString_0(107144559)))
							{
								this.processRequest(ctx.GetWebSocketContext(null));
							}
							else
							{
								this.processRequest(ctx);
							}
						}
						catch (Exception ex2)
						{
							this._log.Fatal(ex2.Message);
							this._log.Debug(ex2.ToString());
							ctx.Connection.Close(true);
						}
					});
				}
				catch (HttpListenerException)
				{
					this._log.Info(HttpServer.getString_0(107160284));
					break;
				}
				catch (InvalidOperationException)
				{
					this._log.Info(HttpServer.getString_0(107160284));
					break;
				}
				catch (Exception ex)
				{
					this._log.Fatal(ex.Message);
					this._log.Debug(ex.ToString());
					if (ctx != null)
					{
						ctx.Connection.Close(true);
					}
					break;
				}
			}
			if (this._state != ServerState.ShuttingDown)
			{
				this.abort();
			}
		}

		private void start()
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._state == ServerState.Start)
				{
					this._log.Info(HttpServer.getString_0(107160370));
				}
				else if (this._state == ServerState.ShuttingDown)
				{
					this._log.Warn(HttpServer.getString_0(107160357));
				}
				else
				{
					this._services.Start();
					try
					{
						this.startReceiving();
					}
					catch
					{
						this._services.Stop(1011, string.Empty);
						throw;
					}
					this._state = ServerState.Start;
				}
			}
		}

		private void startReceiving()
		{
			try
			{
				this._listener.Start();
			}
			catch (Exception innerException)
			{
				string message = HttpServer.getString_0(107160267);
				throw new InvalidOperationException(message, innerException);
			}
			this._receiveThread = new Thread(new ThreadStart(this.receiveRequest));
			this._receiveThread.IsBackground = true;
			this._receiveThread.Start();
		}

		private void stop(ushort code, string reason)
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._state == ServerState.ShuttingDown)
				{
					this._log.Info(HttpServer.getString_0(107160357));
					return;
				}
				if (this._state == ServerState.Stop)
				{
					this._log.Info(HttpServer.getString_0(107159694));
					return;
				}
				this._state = ServerState.ShuttingDown;
			}
			try
			{
				bool flag = false;
				try
				{
					this._services.Stop(code, reason);
				}
				catch
				{
					flag = true;
					throw;
				}
				finally
				{
					try
					{
						this.stopReceiving(5000);
					}
					catch
					{
						if (!flag)
						{
							throw;
						}
					}
				}
			}
			finally
			{
				this._state = ServerState.Stop;
			}
		}

		private void stopReceiving(int millisecondsTimeout)
		{
			this._listener.Stop();
			this._receiveThread.Join(millisecondsTimeout);
		}

		private static bool tryCreateUri(string uriString, out Uri result, out string message)
		{
			result = null;
			message = null;
			Uri uri = uriString.ToUri();
			bool result2;
			if (uri == null)
			{
				message = HttpServer.getString_0(107142500);
				result2 = false;
			}
			else if (!uri.IsAbsoluteUri)
			{
				message = HttpServer.getString_0(107142467);
				result2 = false;
			}
			else
			{
				string scheme = uri.Scheme;
				if (!(scheme == HttpServer.getString_0(107140746)) && !(scheme == HttpServer.getString_0(107364059)))
				{
					message = HttpServer.getString_0(107159748);
					result2 = false;
				}
				else if (uri.PathAndQuery != HttpServer.getString_0(107381125))
				{
					message = HttpServer.getString_0(107159588);
					result2 = false;
				}
				else if (uri.Fragment.Length > 0)
				{
					message = HttpServer.getString_0(107142323);
					result2 = false;
				}
				else if (uri.Port == 0)
				{
					message = HttpServer.getString_0(107142356);
					result2 = false;
				}
				else
				{
					result = uri;
					result2 = true;
				}
			}
			return result2;
		}

		public void AddWebSocketService<TBehavior>(string path) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, null);
		}

		public void AddWebSocketService<TBehavior>(string path, Action<TBehavior> initializer) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, initializer);
		}

		public bool RemoveWebSocketService(string path)
		{
			return this._services.RemoveService(path);
		}

		public void Start()
		{
			string message;
			if (this._secure && !this.checkCertificate(out message))
			{
				throw new InvalidOperationException(message);
			}
			if (this._state == ServerState.Start)
			{
				this._log.Info(HttpServer.getString_0(107160370));
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				this._log.Warn(HttpServer.getString_0(107160357));
			}
			else
			{
				this.start();
			}
		}

		public void Stop()
		{
			if (this._state == ServerState.Ready)
			{
				this._log.Info(HttpServer.getString_0(107159922));
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				this._log.Info(HttpServer.getString_0(107160357));
			}
			else if (this._state == ServerState.Stop)
			{
				this._log.Info(HttpServer.getString_0(107159694));
			}
			else
			{
				this.stop(1001, string.Empty);
			}
		}

		static HttpServer()
		{
			Strings.CreateGetStringDelegate(typeof(HttpServer));
		}

		private IPAddress _address;

		private string _docRootPath;

		private string _hostname;

		private HttpListener _listener;

		private Logger _log;

		private int _port;

		private Thread _receiveThread;

		private bool _secure;

		private WebSocketServiceManager _services;

		private volatile ServerState _state;

		private object _sync;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
