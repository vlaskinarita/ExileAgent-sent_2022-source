using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public sealed class WebSocketServer
	{
		static WebSocketServer()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocketServer));
			WebSocketServer._defaultRealm = WebSocketServer.getString_0(107132266);
		}

		public WebSocketServer()
		{
			IPAddress any = IPAddress.Any;
			this.init(any.ToString(), any, 80, false);
		}

		public WebSocketServer(int port) : this(port, port == 443)
		{
		}

		public WebSocketServer(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(WebSocketServer.getString_0(107404308));
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(WebSocketServer.getString_0(107140577), WebSocketServer.getString_0(107404308));
			}
			Uri uri;
			string message;
			if (!WebSocketServer.tryCreateUri(url, out uri, out message))
			{
				throw new ArgumentException(message, WebSocketServer.getString_0(107404308));
			}
			string dnsSafeHost = uri.DnsSafeHost;
			IPAddress ipaddress = dnsSafeHost.ToIPAddress();
			if (ipaddress == null)
			{
				message = WebSocketServer.getString_0(107160177);
				throw new ArgumentException(message, WebSocketServer.getString_0(107404308));
			}
			if (!ipaddress.IsLocal())
			{
				message = WebSocketServer.getString_0(107160072);
				throw new ArgumentException(message, WebSocketServer.getString_0(107404308));
			}
			this.init(dnsSafeHost, ipaddress, uri.Port, uri.Scheme == WebSocketServer.getString_0(107364028));
		}

		public WebSocketServer(int port, bool secure)
		{
			if (!port.IsPortNumber())
			{
				string message = WebSocketServer.getString_0(107159999);
				throw new ArgumentOutOfRangeException(WebSocketServer.getString_0(107132453), message);
			}
			IPAddress any = IPAddress.Any;
			this.init(any.ToString(), any, port, secure);
		}

		public WebSocketServer(IPAddress address, int port) : this(address, port, port == 443)
		{
		}

		public WebSocketServer(IPAddress address, int port, bool secure)
		{
			if (address == null)
			{
				throw new ArgumentNullException(WebSocketServer.getString_0(107251956));
			}
			if (!address.IsLocal())
			{
				string message = WebSocketServer.getString_0(107159942);
				throw new ArgumentException(message, WebSocketServer.getString_0(107251956));
			}
			if (!port.IsPortNumber())
			{
				string message2 = WebSocketServer.getString_0(107159999);
				throw new ArgumentOutOfRangeException(WebSocketServer.getString_0(107132453), message2);
			}
			this.init(address.ToString(), address, port, secure);
		}

		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		public bool AllowForwardedRequest
		{
			get
			{
				return this._allowForwardedRequest;
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
						this._allowForwardedRequest = value;
					}
				}
			}
		}

		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this._authSchemes;
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
						this._authSchemes = value;
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
				return this._realm;
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
						this._realm = value;
					}
				}
			}
		}

		public bool ReuseAddress
		{
			get
			{
				return this._reuseAddress;
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
						this._reuseAddress = value;
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
					string message = WebSocketServer.getString_0(107160413);
					throw new InvalidOperationException(message);
				}
				return this.getSslConfiguration();
			}
		}

		public Func<IIdentity, NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return this._userCredFinder;
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
						this._userCredFinder = value;
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
					this._listener.Stop();
				}
				finally
				{
					this._services.Stop(1006, string.Empty);
				}
			}
			catch
			{
			}
			this._state = ServerState.Stop;
		}

		private bool authenticateClient(TcpListenerWebSocketContext context)
		{
			return this._authSchemes == AuthenticationSchemes.Anonymous || (this._authSchemes != AuthenticationSchemes.None && context.Authenticate(this._authSchemes, this._realmInUse, this._userCredFinder));
		}

		private bool canSet(out string message)
		{
			message = null;
			bool result;
			if (this._state == ServerState.Start)
			{
				message = WebSocketServer.getString_0(107160348);
				result = false;
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				message = WebSocketServer.getString_0(107160335);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private bool checkHostNameForRequest(string name)
		{
			return !this._dnsStyle || Uri.CheckHostName(name) != UriHostNameType.Dns || name == this._hostname;
		}

		private string getRealm()
		{
			string realm = this._realm;
			return (realm == null || realm.Length <= 0) ? WebSocketServer._defaultRealm : realm;
		}

		private ServerSslConfiguration getSslConfiguration()
		{
			if (this._sslConfig == null)
			{
				this._sslConfig = new ServerSslConfiguration();
			}
			return this._sslConfig;
		}

		private void init(string hostname, IPAddress address, int port, bool secure)
		{
			this._hostname = hostname;
			this._address = address;
			this._port = port;
			this._secure = secure;
			this._authSchemes = AuthenticationSchemes.Anonymous;
			this._dnsStyle = (Uri.CheckHostName(hostname) == UriHostNameType.Dns);
			this._listener = new TcpListener(address, port);
			this._log = new Logger();
			this._services = new WebSocketServiceManager(this._log);
			this._sync = new object();
		}

		private void processRequest(TcpListenerWebSocketContext context)
		{
			if (!this.authenticateClient(context))
			{
				context.Close(HttpStatusCode.Forbidden);
			}
			else
			{
				Uri requestUri = context.RequestUri;
				if (requestUri == null)
				{
					context.Close(HttpStatusCode.BadRequest);
				}
				else
				{
					if (!this._allowForwardedRequest)
					{
						if (requestUri.Port != this._port)
						{
							context.Close(HttpStatusCode.BadRequest);
							return;
						}
						if (!this.checkHostNameForRequest(requestUri.DnsSafeHost))
						{
							context.Close(HttpStatusCode.NotFound);
							return;
						}
					}
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
		}

		private void receiveRequest()
		{
			for (;;)
			{
				TcpClient cl = null;
				try
				{
					cl = this._listener.AcceptTcpClient();
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						try
						{
							TcpListenerWebSocketContext context = new TcpListenerWebSocketContext(cl, null, this._secure, this._sslConfigInUse, this._log);
							this.processRequest(context);
						}
						catch (Exception ex3)
						{
							this._log.Error(ex3.Message);
							this._log.Debug(ex3.ToString());
							cl.Close();
						}
					});
				}
				catch (SocketException ex)
				{
					if (this._state == ServerState.ShuttingDown)
					{
						this._log.Info(WebSocketServer.getString_0(107160262));
						break;
					}
					this._log.Fatal(ex.Message);
					this._log.Debug(ex.ToString());
					break;
				}
				catch (Exception ex2)
				{
					this._log.Fatal(ex2.Message);
					this._log.Debug(ex2.ToString());
					if (cl != null)
					{
						cl.Close();
					}
					break;
				}
			}
			if (this._state != ServerState.ShuttingDown)
			{
				this.abort();
			}
		}

		private void start(ServerSslConfiguration sslConfig)
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._state == ServerState.Start)
				{
					this._log.Info(WebSocketServer.getString_0(107160348));
				}
				else if (this._state == ServerState.ShuttingDown)
				{
					this._log.Warn(WebSocketServer.getString_0(107160335));
				}
				else
				{
					this._sslConfigInUse = sslConfig;
					this._realmInUse = this.getRealm();
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
			if (this._reuseAddress)
			{
				this._listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			try
			{
				this._listener.Start();
			}
			catch (Exception innerException)
			{
				string message = WebSocketServer.getString_0(107160245);
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
					this._log.Info(WebSocketServer.getString_0(107160335));
					return;
				}
				if (this._state == ServerState.Stop)
				{
					this._log.Info(WebSocketServer.getString_0(107159672));
					return;
				}
				this._state = ServerState.ShuttingDown;
			}
			try
			{
				bool flag = false;
				try
				{
					this.stopReceiving(5000);
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
						this._services.Stop(code, reason);
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
			try
			{
				this._listener.Stop();
			}
			catch (Exception innerException)
			{
				string message = WebSocketServer.getString_0(107159627);
				throw new InvalidOperationException(message, innerException);
			}
			this._receiveThread.Join(millisecondsTimeout);
		}

		private static bool tryCreateUri(string uriString, out Uri result, out string message)
		{
			bool result2;
			if (!uriString.TryCreateWebSocketUri(out result, out message))
			{
				result2 = false;
			}
			else if (result.PathAndQuery != WebSocketServer.getString_0(107381103))
			{
				result = null;
				message = WebSocketServer.getString_0(107159566);
				result2 = false;
			}
			else
			{
				result2 = true;
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
			ServerSslConfiguration serverSslConfiguration = null;
			if (this._secure)
			{
				ServerSslConfiguration sslConfiguration = this.getSslConfiguration();
				serverSslConfiguration = new ServerSslConfiguration(sslConfiguration);
				if (serverSslConfiguration.ServerCertificate == null)
				{
					string message = WebSocketServer.getString_0(107159461);
					throw new InvalidOperationException(message);
				}
			}
			if (this._state == ServerState.Start)
			{
				this._log.Info(WebSocketServer.getString_0(107160348));
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				this._log.Warn(WebSocketServer.getString_0(107160335));
			}
			else
			{
				this.start(serverSslConfiguration);
			}
		}

		public void Stop()
		{
			if (this._state == ServerState.Ready)
			{
				this._log.Info(WebSocketServer.getString_0(107159900));
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				this._log.Info(WebSocketServer.getString_0(107160335));
			}
			else if (this._state == ServerState.Stop)
			{
				this._log.Info(WebSocketServer.getString_0(107159672));
			}
			else
			{
				this.stop(1001, string.Empty);
			}
		}

		private IPAddress _address;

		private bool _allowForwardedRequest;

		private AuthenticationSchemes _authSchemes;

		private static readonly string _defaultRealm;

		private bool _dnsStyle;

		private string _hostname;

		private TcpListener _listener;

		private Logger _log;

		private int _port;

		private string _realm;

		private string _realmInUse;

		private Thread _receiveThread;

		private bool _reuseAddress;

		private bool _secure;

		private WebSocketServiceManager _services;

		private ServerSslConfiguration _sslConfig;

		private ServerSslConfiguration _sslConfigInUse;

		private volatile ServerState _state;

		private object _sync;

		private Func<IIdentity, NetworkCredential> _userCredFinder;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
