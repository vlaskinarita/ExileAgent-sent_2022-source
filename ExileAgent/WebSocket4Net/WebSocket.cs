using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;
using WebSocket4Net.Command;
using WebSocket4Net.Common;
using WebSocket4Net.Protocol;

namespace WebSocket4Net
{
	public sealed class WebSocket : IDisposable
	{
		internal TcpClientSession Client { get; private set; }

		public WebSocketVersion Version { get; private set; }

		public DateTime LastActiveTime { get; internal set; }

		public bool EnableAutoSendPing { get; set; }

		public int AutoSendPingInterval { get; set; }

		internal IProtocolProcessor ProtocolProcessor { get; private set; }

		public bool SupportBinary
		{
			get
			{
				return this.ProtocolProcessor.SupportBinary;
			}
		}

		internal Uri TargetUri { get; private set; }

		internal string SubProtocol { get; private set; }

		internal IDictionary<string, object> Items { get; private set; }

		internal List<KeyValuePair<string, string>> Cookies { get; private set; }

		internal List<KeyValuePair<string, string>> CustomHeaderItems { get; private set; }

		internal int StateCode
		{
			get
			{
				return this.m_StateCode;
			}
		}

		public WebSocketState State
		{
			get
			{
				return (WebSocketState)this.m_StateCode;
			}
		}

		public bool Handshaked { get; private set; }

		public IProxyConnector Proxy { get; set; }

		internal EndPoint HttpConnectProxy
		{
			get
			{
				return this.m_HttpConnectProxy;
			}
		}

		private protected IClientCommandReader<WebSocketCommandInfo> CommandReader { protected get; private set; }

		internal bool NotSpecifiedVersion { get; private set; }

		internal string LastPongResponse { get; set; }

		internal string HandshakeHost { get; private set; }

		internal string Origin { get; private set; }

		public bool NoDelay { get; set; }

		public EndPoint LocalEndPoint
		{
			get
			{
				if (this.Client == null)
				{
					return null;
				}
				return this.Client.LocalEndPoint;
			}
			set
			{
				if (this.Client == null)
				{
					throw new Exception(WebSocket.getString_0(107143496));
				}
				this.Client.LocalEndPoint = value;
			}
		}

		public SecurityOption Security
		{
			get
			{
				if (this.m_Security != null)
				{
					return this.m_Security;
				}
				SslStreamTcpSession sslStreamTcpSession = this.Client as SslStreamTcpSession;
				if (sslStreamTcpSession == null)
				{
					return this.m_Security = new SecurityOption();
				}
				return this.m_Security = sslStreamTcpSession.Security;
			}
		}

		static WebSocket()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocket));
			WebSocket.m_ProtocolProcessorFactory = new ProtocolProcessorFactory(new IProtocolProcessor[]
			{
				new Rfc6455Processor(),
				new DraftHybi10Processor(),
				new DraftHybi00Processor()
			});
		}

		private EndPoint ResolveUri(string uri, int defaultPort, out int port)
		{
			this.TargetUri = new Uri(uri);
			if (string.IsNullOrEmpty(this.Origin))
			{
				this.Origin = this.TargetUri.GetOrigin();
			}
			port = this.TargetUri.Port;
			if (port <= 0)
			{
				port = defaultPort;
			}
			IPAddress address;
			EndPoint result;
			if (IPAddress.TryParse(this.TargetUri.Host, out address))
			{
				result = new IPEndPoint(address, port);
			}
			else
			{
				result = new DnsEndPoint(this.TargetUri.Host, port);
			}
			return result;
		}

		private TcpClientSession CreateClient(string uri)
		{
			int num;
			this.m_RemoteEndPoint = this.ResolveUri(uri, 80, out num);
			if (num == 80)
			{
				this.HandshakeHost = this.TargetUri.Host;
			}
			else
			{
				this.HandshakeHost = this.TargetUri.Host + WebSocket.getString_0(107404957) + num;
			}
			return new AsyncTcpSession();
		}

		private TcpClientSession CreateSecureClient(string uri)
		{
			int num = uri.IndexOf('/', WebSocket.getString_0(107311849).Length);
			if (num < 0)
			{
				num = uri.IndexOf(':', WebSocket.getString_0(107311849).Length, uri.Length - WebSocket.getString_0(107311849).Length);
				if (num < 0)
				{
					uri = string.Concat(new object[]
					{
						uri,
						WebSocket.getString_0(107404957),
						443,
						WebSocket.getString_0(107380230)
					});
				}
				else
				{
					uri += WebSocket.getString_0(107380230);
				}
			}
			else
			{
				if (num == WebSocket.getString_0(107311849).Length)
				{
					throw new ArgumentException(WebSocket.getString_0(107142903), WebSocket.getString_0(107244804));
				}
				if (uri.IndexOf(':', WebSocket.getString_0(107311849).Length, num - WebSocket.getString_0(107311849).Length) < 0)
				{
					uri = string.Concat(new object[]
					{
						uri.Substring(0, num),
						WebSocket.getString_0(107404957),
						443,
						uri.Substring(num)
					});
				}
			}
			int num2;
			this.m_RemoteEndPoint = this.ResolveUri(uri, 443, out num2);
			if (this.m_HttpConnectProxy != null)
			{
				this.m_RemoteEndPoint = this.m_HttpConnectProxy;
			}
			if (num2 == 443)
			{
				this.HandshakeHost = this.TargetUri.Host;
			}
			else
			{
				this.HandshakeHost = this.TargetUri.Host + WebSocket.getString_0(107404957) + num2;
			}
			return this.CreateSecureTcpSession();
		}

		private void Initialize(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version, EndPoint httpConnectProxy, int receiveBufferSize)
		{
			if (version == WebSocketVersion.None)
			{
				this.NotSpecifiedVersion = true;
				version = WebSocketVersion.Rfc6455;
			}
			this.Version = version;
			this.ProtocolProcessor = WebSocket.GetProtocolProcessor(version);
			this.Cookies = cookies;
			this.Origin = origin;
			if (!string.IsNullOrEmpty(userAgent))
			{
				if (customHeaderItems == null)
				{
					customHeaderItems = new List<KeyValuePair<string, string>>();
				}
				customHeaderItems.Add(new KeyValuePair<string, string>(WebSocket.getString_0(107325561), userAgent));
			}
			if (customHeaderItems != null && customHeaderItems.Count > 0)
			{
				this.CustomHeaderItems = customHeaderItems;
			}
			Handshake handshake = new Handshake();
			this.m_CommandDict.Add(handshake.Name, handshake);
			Text text = new Text();
			this.m_CommandDict.Add(text.Name, text);
			Binary binary = new Binary();
			this.m_CommandDict.Add(binary.Name, binary);
			Close close = new Close();
			this.m_CommandDict.Add(close.Name, close);
			Ping ping = new Ping();
			this.m_CommandDict.Add(ping.Name, ping);
			Pong pong = new Pong();
			this.m_CommandDict.Add(pong.Name, pong);
			BadRequest badRequest = new BadRequest();
			this.m_CommandDict.Add(badRequest.Name, badRequest);
			this.m_StateCode = -1;
			this.SubProtocol = subProtocol;
			this.Items = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.m_HttpConnectProxy = httpConnectProxy;
			TcpClientSession tcpClientSession;
			if (uri.StartsWith(WebSocket.getString_0(107311826), StringComparison.OrdinalIgnoreCase))
			{
				tcpClientSession = this.CreateClient(uri);
			}
			else
			{
				if (!uri.StartsWith(WebSocket.getString_0(107311849), StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(WebSocket.getString_0(107142903), WebSocket.getString_0(107244804));
				}
				tcpClientSession = this.CreateSecureClient(uri);
			}
			tcpClientSession.ReceiveBufferSize = ((receiveBufferSize > 0) ? receiveBufferSize : 4096);
			tcpClientSession.Connected += this.client_Connected;
			tcpClientSession.Closed += this.client_Closed;
			tcpClientSession.Error += this.client_Error;
			tcpClientSession.DataReceived += this.client_DataReceived;
			this.Client = tcpClientSession;
			this.EnableAutoSendPing = true;
		}

		private void client_DataReceived(object sender, DataEventArgs e)
		{
			this.OnDataReceived(e.Data, e.Offset, e.Length);
		}

		private void client_Error(object sender, ErrorEventArgs e)
		{
			this.OnError(e);
			this.OnClosed();
		}

		private void client_Closed(object sender, EventArgs e)
		{
			this.OnClosed();
		}

		private void client_Connected(object sender, EventArgs e)
		{
			this.OnConnected();
		}

		internal bool GetAvailableProcessor(int[] availableVersions)
		{
			IProtocolProcessor preferedProcessorFromAvialable = WebSocket.m_ProtocolProcessorFactory.GetPreferedProcessorFromAvialable(availableVersions);
			if (preferedProcessorFromAvialable == null)
			{
				return false;
			}
			this.ProtocolProcessor = preferedProcessorFromAvialable;
			return true;
		}

		public int ReceiveBufferSize
		{
			get
			{
				return this.Client.ReceiveBufferSize;
			}
			set
			{
				this.Client.ReceiveBufferSize = value;
			}
		}

		public void Open()
		{
			this.m_StateCode = 0;
			if (this.Proxy != null)
			{
				this.Client.Proxy = this.Proxy;
			}
			this.Client.NoDelay = this.NoDelay;
			this.Client.Connect(this.m_RemoteEndPoint);
		}

		private static IProtocolProcessor GetProtocolProcessor(WebSocketVersion version)
		{
			IProtocolProcessor processorByVersion = WebSocket.m_ProtocolProcessorFactory.GetProcessorByVersion(version);
			if (processorByVersion == null)
			{
				throw new ArgumentException(WebSocket.getString_0(107142918));
			}
			return processorByVersion;
		}

		private void OnConnected()
		{
			this.CommandReader = this.ProtocolProcessor.CreateHandshakeReader(this);
			if (this.Items.Count > 0)
			{
				this.Items.Clear();
			}
			this.ProtocolProcessor.SendHandshake(this);
		}

		protected internal void OnHandshaked()
		{
			this.m_StateCode = 1;
			this.Handshaked = true;
			if (this.EnableAutoSendPing && this.ProtocolProcessor.SupportPingPong)
			{
				if (this.AutoSendPingInterval <= 0)
				{
					this.AutoSendPingInterval = 60;
				}
				this.m_WebSocketTimer = new Timer(new TimerCallback(this.OnPingTimerCallback), this.ProtocolProcessor, this.AutoSendPingInterval * 1000, this.AutoSendPingInterval * 1000);
			}
			EventHandler opened = this.m_Opened;
			if (opened != null)
			{
				opened(this, EventArgs.Empty);
			}
		}

		private void OnPingTimerCallback(object state)
		{
			IProtocolProcessor protocolProcessor = state as IProtocolProcessor;
			if (!string.IsNullOrEmpty(this.m_LastPingRequest) && !this.m_LastPingRequest.Equals(this.LastPongResponse))
			{
				try
				{
					protocolProcessor.SendPong(this, WebSocket.getString_0(107404958));
				}
				catch (Exception e)
				{
					this.OnError(e);
					return;
				}
			}
			this.m_LastPingRequest = DateTime.Now.ToString();
			try
			{
				protocolProcessor.SendPing(this, this.m_LastPingRequest);
			}
			catch (Exception e2)
			{
				this.OnError(e2);
			}
		}

		public event EventHandler Opened
		{
			add
			{
				this.m_Opened = (EventHandler)Delegate.Combine(this.m_Opened, value);
			}
			remove
			{
				this.m_Opened = (EventHandler)Delegate.Remove(this.m_Opened, value);
			}
		}

		public event EventHandler<MessageReceivedEventArgs> MessageReceived
		{
			add
			{
				this.m_MessageReceived = (EventHandler<MessageReceivedEventArgs>)Delegate.Combine(this.m_MessageReceived, value);
			}
			remove
			{
				this.m_MessageReceived = (EventHandler<MessageReceivedEventArgs>)Delegate.Remove(this.m_MessageReceived, value);
			}
		}

		internal void FireMessageReceived(string message)
		{
			if (this.m_MessageReceived == null)
			{
				return;
			}
			this.m_MessageReceived(this, new MessageReceivedEventArgs(message));
		}

		public event EventHandler<DataReceivedEventArgs> DataReceived
		{
			add
			{
				this.m_DataReceived = (EventHandler<DataReceivedEventArgs>)Delegate.Combine(this.m_DataReceived, value);
			}
			remove
			{
				this.m_DataReceived = (EventHandler<DataReceivedEventArgs>)Delegate.Remove(this.m_DataReceived, value);
			}
		}

		internal void FireDataReceived(byte[] data)
		{
			if (this.m_DataReceived == null)
			{
				return;
			}
			this.m_DataReceived(this, new DataReceivedEventArgs(data));
		}

		private bool EnsureWebSocketOpen()
		{
			if (!this.Handshaked)
			{
				this.OnError(new Exception(WebSocket.getString_0(107142849)));
				return false;
			}
			return true;
		}

		public void Send(string message)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendMessage(this, message);
		}

		public void Send(byte[] data, int offset, int length)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendData(this, data, offset, length);
		}

		public void Send(IList<ArraySegment<byte>> segments)
		{
			if (!this.EnsureWebSocketOpen())
			{
				return;
			}
			this.ProtocolProcessor.SendData(this, segments);
		}

		private void OnClosed()
		{
			bool flag = false;
			if (this.m_StateCode == 2 || this.m_StateCode == 1 || this.m_StateCode == 0)
			{
				flag = true;
			}
			this.m_StateCode = 3;
			if (flag)
			{
				this.FireClosed();
			}
		}

		public void Close()
		{
			this.Close(string.Empty);
		}

		public void Close(string reason)
		{
			this.Close((int)this.ProtocolProcessor.CloseStatusCode.NormalClosure, reason);
		}

		public void Close(int statusCode, string reason)
		{
			this.m_ClosedArgs = new ClosedEventArgs((short)statusCode, reason);
			if (Interlocked.CompareExchange(ref this.m_StateCode, 3, -1) == -1)
			{
				this.OnClosed();
				return;
			}
			if (Interlocked.CompareExchange(ref this.m_StateCode, 2, 0) != 0)
			{
				this.m_StateCode = 2;
				this.ClearTimer();
				this.m_WebSocketTimer = new Timer(new TimerCallback(this.CheckCloseHandshake), null, 5000, -1);
				try
				{
					this.ProtocolProcessor.SendCloseHandshake(this, statusCode, reason);
				}
				catch (Exception e)
				{
					if (this.Client != null)
					{
						this.OnError(e);
					}
				}
				return;
			}
			TcpClientSession client = this.Client;
			if (client != null && client.IsConnected)
			{
				client.Close();
				return;
			}
			this.OnClosed();
		}

		private void CheckCloseHandshake(object state)
		{
			if (this.m_StateCode == 3)
			{
				return;
			}
			try
			{
				this.CloseWithoutHandshake();
			}
			catch (Exception e)
			{
				this.OnError(e);
			}
		}

		internal void CloseWithoutHandshake()
		{
			TcpClientSession client = this.Client;
			if (client != null)
			{
				client.Close();
			}
		}

		protected void ExecuteCommand(WebSocketCommandInfo commandInfo)
		{
			ICommand<WebSocket, WebSocketCommandInfo> command;
			if (this.m_CommandDict.TryGetValue(commandInfo.Key, out command))
			{
				command.ExecuteCommand(this, commandInfo);
			}
		}

		private void OnDataReceived(byte[] data, int offset, int length)
		{
			for (;;)
			{
				int num;
				WebSocketCommandInfo commandInfo = this.CommandReader.GetCommandInfo(data, offset, length, out num);
				if (this.CommandReader.NextCommandReader != null)
				{
					this.CommandReader = this.CommandReader.NextCommandReader;
				}
				if (commandInfo != null)
				{
					this.ExecuteCommand(commandInfo);
				}
				if (num <= 0)
				{
					break;
				}
				offset = offset + length - num;
				length = num;
			}
		}

		internal void FireError(Exception error)
		{
			this.OnError(error);
		}

		public event EventHandler Closed
		{
			add
			{
				this.m_Closed = (EventHandler)Delegate.Combine(this.m_Closed, value);
			}
			remove
			{
				this.m_Closed = (EventHandler)Delegate.Remove(this.m_Closed, value);
			}
		}

		private void ClearTimer()
		{
			Timer webSocketTimer = this.m_WebSocketTimer;
			if (webSocketTimer == null)
			{
				return;
			}
			lock (this)
			{
				if (this.m_WebSocketTimer != null)
				{
					webSocketTimer.Change(-1, -1);
					webSocketTimer.Dispose();
					this.m_WebSocketTimer = null;
				}
			}
		}

		private void FireClosed()
		{
			this.ClearTimer();
			EventHandler closed = this.m_Closed;
			if (closed != null)
			{
				closed(this, this.m_ClosedArgs ?? EventArgs.Empty);
			}
		}

		public event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(this.m_Error, value);
			}
			remove
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Remove(this.m_Error, value);
			}
		}

		private void OnError(ErrorEventArgs e)
		{
			EventHandler<ErrorEventArgs> error = this.m_Error;
			if (error == null)
			{
				return;
			}
			error(this, e);
		}

		private void OnError(Exception e)
		{
			this.OnError(new ErrorEventArgs(e));
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing)
			{
				TcpClientSession client = this.Client;
				if (client != null)
				{
					client.Connected -= this.client_Connected;
					client.Closed -= this.client_Closed;
					client.Error -= this.client_Error;
					client.DataReceived -= this.client_DataReceived;
					if (client.IsConnected)
					{
						client.Close();
					}
					this.Client = null;
				}
				this.ClearTimer();
			}
			this.m_Disposed = true;
		}

		~WebSocket()
		{
			this.Dispose(false);
		}

		public WebSocket(string uri, string subProtocol, WebSocketVersion version) : this(uri, subProtocol, WebSocket.EmptyCookies, null, string.Empty, string.Empty, version, null, SslProtocols.None, 0)
		{
		}

		public WebSocket(string uri, string subProtocol = "", List<KeyValuePair<string, string>> cookies = null, List<KeyValuePair<string, string>> customHeaderItems = null, string userAgent = "", string origin = "", WebSocketVersion version = WebSocketVersion.None, EndPoint httpConnectProxy = null, SslProtocols sslProtocols = SslProtocols.None, int receiveBufferSize = 0)
		{
			if (sslProtocols != SslProtocols.None)
			{
				this.m_SecureProtocols = sslProtocols;
			}
			this.Initialize(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, httpConnectProxy, receiveBufferSize);
		}

		private TcpClientSession CreateSecureTcpSession()
		{
			SslStreamTcpSession sslStreamTcpSession = new SslStreamTcpSession();
			(sslStreamTcpSession.Security = new SecurityOption()).EnabledSslProtocols = this.m_SecureProtocols;
			return sslStreamTcpSession;
		}

		private EndPoint m_RemoteEndPoint;

		protected const string UserAgentKey = "User-Agent";

		public const int DefaultReceiveBufferSize = 4096;

		private int m_StateCode;

		private EndPoint m_HttpConnectProxy;

		private Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>> m_CommandDict = new Dictionary<string, ICommand<WebSocket, WebSocketCommandInfo>>(StringComparer.OrdinalIgnoreCase);

		private static ProtocolProcessorFactory m_ProtocolProcessorFactory;

		private Timer m_WebSocketTimer;

		private string m_LastPingRequest;

		private const string m_UriScheme = "ws";

		private const string m_UriPrefix = "ws://";

		private const string m_SecureUriScheme = "wss";

		private const int m_SecurePort = 443;

		private const string m_SecureUriPrefix = "wss://";

		private SecurityOption m_Security;

		private bool m_Disposed;

		private EventHandler m_Opened;

		private EventHandler<MessageReceivedEventArgs> m_MessageReceived;

		private EventHandler<DataReceivedEventArgs> m_DataReceived;

		private const string m_NotOpenSendingMessage = "You must send data by websocket after websocket is opened!";

		private ClosedEventArgs m_ClosedArgs;

		private EventHandler m_Closed;

		private EventHandler<ErrorEventArgs> m_Error;

		private static List<KeyValuePair<string, string>> EmptyCookies;

		private SslProtocols m_SecureProtocols = SslProtocols.Default;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
