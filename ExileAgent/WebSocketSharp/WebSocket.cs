using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp
{
	public sealed class WebSocket : IDisposable
	{
		static WebSocket()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocket));
			WebSocket._maxRetryCountForConnect = 10;
			WebSocket.EmptyBytes = new byte[0];
			WebSocket.FragmentLength = 1016;
			WebSocket.RandomNumber = new RNGCryptoServiceProvider();
		}

		internal WebSocket(HttpListenerWebSocketContext context, string protocol)
		{
			this._context = context;
			this._protocol = protocol;
			this._closeContext = new Action(context.Close);
			this._logger = context.Log;
			this._message = new Action<MessageEventArgs>(this.messages);
			this._secure = context.IsSecureConnection;
			this._stream = context.Stream;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.init();
		}

		internal WebSocket(TcpListenerWebSocketContext context, string protocol)
		{
			this._context = context;
			this._protocol = protocol;
			this._closeContext = new Action(context.Close);
			this._logger = context.Log;
			this._message = new Action<MessageEventArgs>(this.messages);
			this._secure = context.IsSecureConnection;
			this._stream = context.Stream;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.init();
		}

		public WebSocket(string url, params string[] protocols)
		{
			if (url == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107403684));
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(WebSocket.getString_0(107139953), WebSocket.getString_0(107403684));
			}
			string message;
			if (!url.TryCreateWebSocketUri(out this._uri, out message))
			{
				throw new ArgumentException(message, WebSocket.getString_0(107403684));
			}
			if (protocols != null && protocols.Length != 0)
			{
				if (!WebSocket.checkProtocols(protocols, out message))
				{
					throw new ArgumentException(message, WebSocket.getString_0(107139928));
				}
				this._protocols = protocols;
			}
			this._base64Key = WebSocket.CreateBase64Key();
			this._client = true;
			this._logger = new Logger();
			this._message = new Action<MessageEventArgs>(this.messagec);
			this._secure = (this._uri.Scheme == WebSocket.getString_0(107363404));
			this._waitTime = TimeSpan.FromSeconds(5.0);
			this.init();
		}

		internal CookieCollection CookieCollection
		{
			get
			{
				return this._cookies;
			}
		}

		internal Func<WebSocketContext, string> CustomHandshakeRequestChecker
		{
			get
			{
				return this._handshakeRequestChecker;
			}
			set
			{
				this._handshakeRequestChecker = value;
			}
		}

		internal bool HasMessage
		{
			get
			{
				object forMessageEventQueue = this._forMessageEventQueue;
				bool result;
				lock (forMessageEventQueue)
				{
					result = (this._messageEventQueue.Count > 0);
				}
				return result;
			}
		}

		internal bool IgnoreExtensions
		{
			get
			{
				return this._ignoreExtensions;
			}
			set
			{
				this._ignoreExtensions = value;
			}
		}

		internal bool IsConnected
		{
			get
			{
				return this._readyState == WebSocketState.Open || this._readyState == WebSocketState.Closing;
			}
		}

		public CompressionMethod Compression
		{
			get
			{
				return this._compression;
			}
			set
			{
				string message = null;
				if (!this._client)
				{
					message = WebSocket.getString_0(107139915);
					throw new InvalidOperationException(message);
				}
				if (!this.canSet(out message))
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						if (!this.canSet(out message))
						{
							this._logger.Warn(message);
						}
						else
						{
							this._compression = value;
						}
					}
				}
			}
		}

		public IEnumerable<Cookie> Cookies
		{
			get
			{
				WebSocket.<get_Cookies>d__71 <get_Cookies>d__ = new WebSocket.<get_Cookies>d__71(-2);
				<get_Cookies>d__.<>4__this = this;
				return <get_Cookies>d__;
			}
		}

		public NetworkCredential Credentials
		{
			get
			{
				return this._credentials;
			}
		}

		public bool EmitOnPing
		{
			get
			{
				return this._emitOnPing;
			}
			set
			{
				this._emitOnPing = value;
			}
		}

		public bool EnableRedirection
		{
			get
			{
				return this._enableRedirection;
			}
			set
			{
				string message = null;
				if (!this._client)
				{
					message = WebSocket.getString_0(107139915);
					throw new InvalidOperationException(message);
				}
				if (!this.canSet(out message))
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						if (!this.canSet(out message))
						{
							this._logger.Warn(message);
						}
						else
						{
							this._enableRedirection = value;
						}
					}
				}
			}
		}

		public string Extensions
		{
			get
			{
				return this._extensions ?? string.Empty;
			}
		}

		public bool IsAlive
		{
			get
			{
				return this.ping(WebSocket.EmptyBytes);
			}
		}

		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		public Logger Log
		{
			get
			{
				return this._logger;
			}
			internal set
			{
				this._logger = value;
			}
		}

		public string Origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				string message = null;
				if (!this._client)
				{
					message = WebSocket.getString_0(107139915);
					throw new InvalidOperationException(message);
				}
				if (!value.IsNullOrEmpty())
				{
					Uri uri;
					if (!Uri.TryCreate(value, UriKind.Absolute, out uri))
					{
						message = WebSocket.getString_0(107139906);
						throw new ArgumentException(message, WebSocket.getString_0(107457544));
					}
					if (uri.Segments.Length > 1)
					{
						message = WebSocket.getString_0(107139869);
						throw new ArgumentException(message, WebSocket.getString_0(107457544));
					}
				}
				if (!this.canSet(out message))
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						if (!this.canSet(out message))
						{
							this._logger.Warn(message);
						}
						else
						{
							this._origin = ((!value.IsNullOrEmpty()) ? value.TrimEnd(new char[]
							{
								'/'
							}) : value);
						}
					}
				}
			}
		}

		public string UserAgent
		{
			get
			{
				return this._userAgent;
			}
			set
			{
				this._userAgent = value;
			}
		}

		public string Protocol
		{
			get
			{
				return this._protocol ?? string.Empty;
			}
			internal set
			{
				this._protocol = value;
			}
		}

		public WebSocketState ReadyState
		{
			get
			{
				return this._readyState;
			}
		}

		public ClientSslConfiguration SslConfiguration
		{
			get
			{
				if (!this._client)
				{
					string message = WebSocket.getString_0(107139915);
					throw new InvalidOperationException(message);
				}
				if (!this._secure)
				{
					string message2 = WebSocket.getString_0(107140308);
					throw new InvalidOperationException(message2);
				}
				return this.getSslConfiguration();
			}
		}

		public Uri Url
		{
			get
			{
				return this._client ? this._uri : this._context.RequestUri;
			}
		}

		public TimeSpan WaitTime
		{
			get
			{
				return this._waitTime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107457544), WebSocket.getString_0(107140243));
				}
				string message;
				if (!this.canSet(out message))
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						if (!this.canSet(out message))
						{
							this._logger.Warn(message);
						}
						else
						{
							this._waitTime = value;
						}
					}
				}
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<CloseEventArgs> OnClose;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ErrorEventArgs> OnError;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<MessageEventArgs> OnMessage;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler OnOpen;

		private bool accept()
		{
			bool result;
			if (this._readyState == WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107140254);
				this._logger.Warn(message);
				result = false;
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					if (this._readyState == WebSocketState.Open)
					{
						string message2 = WebSocket.getString_0(107140254);
						this._logger.Warn(message2);
						result = false;
					}
					else if (this._readyState == WebSocketState.Closing)
					{
						string message3 = WebSocket.getString_0(107140189);
						this._logger.Error(message3);
						message3 = WebSocket.getString_0(107140116);
						this.error(message3, null);
						result = false;
					}
					else if (this._readyState == WebSocketState.Closed)
					{
						string message4 = WebSocket.getString_0(107139559);
						this._logger.Error(message4);
						message4 = WebSocket.getString_0(107140116);
						this.error(message4, null);
						result = false;
					}
					else
					{
						try
						{
							if (!this.acceptHandshake())
							{
								return false;
							}
						}
						catch (Exception ex)
						{
							this._logger.Fatal(ex.Message);
							this._logger.Debug(ex.ToString());
							string message5 = WebSocket.getString_0(107139482);
							this.fatal(message5, ex);
							return false;
						}
						this._readyState = WebSocketState.Open;
						result = true;
					}
				}
			}
			return result;
		}

		private bool acceptHandshake()
		{
			this._logger.Debug(string.Format(WebSocket.getString_0(107139409), this._context.UserEndPoint, this._context));
			string message;
			bool result;
			if (!this.checkHandshakeRequest(this._context, out message))
			{
				this._logger.Error(message);
				this.refuseHandshake(CloseStatusCode.ProtocolError, WebSocket.getString_0(107139396));
				result = false;
			}
			else if (!this.customCheckHandshakeRequest(this._context, out message))
			{
				this._logger.Error(message);
				this.refuseHandshake(CloseStatusCode.PolicyViolation, WebSocket.getString_0(107139396));
				result = false;
			}
			else
			{
				this._base64Key = this._context.Headers[WebSocket.getString_0(107139795)];
				if (this._protocol != null)
				{
					IEnumerable<string> secWebSocketProtocols = this._context.SecWebSocketProtocols;
					this.processSecWebSocketProtocolClientHeader(secWebSocketProtocols);
				}
				if (!this._ignoreExtensions)
				{
					string value = this._context.Headers[WebSocket.getString_0(107139770)];
					this.processSecWebSocketExtensionsClientHeader(value);
				}
				result = this.sendHttpResponse(this.createHandshakeResponse());
			}
			return result;
		}

		private bool canSet(out string message)
		{
			message = null;
			bool result;
			if (this._readyState == WebSocketState.Open)
			{
				message = WebSocket.getString_0(107139737);
				result = false;
			}
			else if (this._readyState == WebSocketState.Closing)
			{
				message = WebSocket.getString_0(107139708);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private bool checkHandshakeRequest(WebSocketContext context, out string message)
		{
			message = null;
			bool result;
			if (!context.IsWebSocketRequest)
			{
				message = WebSocket.getString_0(107139639);
				result = false;
			}
			else if (context.RequestUri == null)
			{
				message = WebSocket.getString_0(107139606);
				result = false;
			}
			else
			{
				NameValueCollection headers = context.Headers;
				string text = headers[WebSocket.getString_0(107139795)];
				if (text == null)
				{
					message = WebSocket.getString_0(107139077);
					result = false;
				}
				else if (text.Length == 0)
				{
					message = WebSocket.getString_0(107138988);
					result = false;
				}
				else
				{
					string text2 = headers[WebSocket.getString_0(107142882)];
					if (text2 == null)
					{
						message = WebSocket.getString_0(107138923);
						result = false;
					}
					else if (text2 != WebSocket.getString_0(107138862))
					{
						message = WebSocket.getString_0(107138889);
						result = false;
					}
					else
					{
						string text3 = headers[WebSocket.getString_0(107142509)];
						if (text3 != null && text3.Length == 0)
						{
							message = WebSocket.getString_0(107139328);
							result = false;
						}
						else
						{
							if (!this._ignoreExtensions)
							{
								string text4 = headers[WebSocket.getString_0(107139770)];
								if (text4 != null && text4.Length == 0)
								{
									message = WebSocket.getString_0(107139223);
									return false;
								}
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		private bool checkHandshakeResponse(HttpResponse response, out string message)
		{
			message = null;
			bool result;
			if (response.IsRedirect)
			{
				message = WebSocket.getString_0(107139178);
				result = false;
			}
			else if (response.IsUnauthorized)
			{
				message = WebSocket.getString_0(107139141);
				result = false;
			}
			else if (!response.IsWebSocketResponse)
			{
				message = WebSocket.getString_0(107139100);
				result = false;
			}
			else
			{
				NameValueCollection headers = response.Headers;
				if (!this.validateSecWebSocketAcceptHeader(headers[WebSocket.getString_0(107142471)]))
				{
					message = WebSocket.getString_0(107138507);
					result = false;
				}
				else if (!this.validateSecWebSocketProtocolServerHeader(headers[WebSocket.getString_0(107142509)]))
				{
					message = WebSocket.getString_0(107138414);
					result = false;
				}
				else if (!this.validateSecWebSocketExtensionsServerHeader(headers[WebSocket.getString_0(107139770)]))
				{
					message = WebSocket.getString_0(107138317);
					result = false;
				}
				else if (!this.validateSecWebSocketVersionServerHeader(headers[WebSocket.getString_0(107142882)]))
				{
					message = WebSocket.getString_0(107138788);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		private static bool checkProtocols(string[] protocols, out string message)
		{
			message = null;
			Func<string, bool> condition = (string protocol) => protocol.IsNullOrEmpty() || !protocol.IsToken();
			bool result;
			if (protocols.Contains(condition))
			{
				message = WebSocket.getString_0(107138719);
				result = false;
			}
			else if (protocols.ContainsTwice())
			{
				message = WebSocket.getString_0(107138662);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private bool checkReceivedFrame(WebSocketFrame frame, out string message)
		{
			message = null;
			bool isMasked = frame.IsMasked;
			bool result;
			if (this._client && isMasked)
			{
				message = WebSocket.getString_0(107138625);
				result = false;
			}
			else if (!this._client && !isMasked)
			{
				message = WebSocket.getString_0(107138032);
				result = false;
			}
			else if (this._inContinuation && frame.IsData)
			{
				message = WebSocket.getString_0(107138015);
				result = false;
			}
			else if (frame.IsCompressed && this._compression == CompressionMethod.None)
			{
				message = WebSocket.getString_0(107137922);
				result = false;
			}
			else if (frame.Rsv2 == Rsv.On)
			{
				message = WebSocket.getString_0(107137833);
				result = false;
			}
			else if (frame.Rsv3 == Rsv.On)
			{
				message = WebSocket.getString_0(107138228);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private void close(ushort code, string reason)
		{
			if (this._readyState == WebSocketState.Closing)
			{
				this._logger.Info(WebSocket.getString_0(107138175));
			}
			else if (this._readyState == WebSocketState.Closed)
			{
				this._logger.Info(WebSocket.getString_0(107138094));
			}
			else if (code == 1005)
			{
				this.close(PayloadData.Empty, true, true, false);
			}
			else
			{
				bool flag = !code.IsReserved();
				this.close(new PayloadData(code, reason), flag, flag, false);
			}
		}

		private void close(PayloadData payloadData, bool send, bool receive, bool received)
		{
			object forState = this._forState;
			lock (forState)
			{
				if (this._readyState == WebSocketState.Closing)
				{
					this._logger.Info(WebSocket.getString_0(107138175));
					return;
				}
				if (this._readyState == WebSocketState.Closed)
				{
					this._logger.Info(WebSocket.getString_0(107138094));
					return;
				}
				send = (send && this._readyState == WebSocketState.Open);
				receive = (send && receive);
				this._readyState = WebSocketState.Closing;
			}
			this._logger.Trace(WebSocket.getString_0(107137529));
			bool clean = this.closeHandshake(payloadData, send, receive, received);
			this.releaseResources();
			this._logger.Trace(WebSocket.getString_0(107137488));
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs(payloadData, clean);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		private void closeAsync(ushort code, string reason)
		{
			if (this._readyState == WebSocketState.Closing)
			{
				this._logger.Info(WebSocket.getString_0(107138175));
			}
			else if (this._readyState == WebSocketState.Closed)
			{
				this._logger.Info(WebSocket.getString_0(107138094));
			}
			else if (code == 1005)
			{
				this.closeAsync(PayloadData.Empty, true, true, false);
			}
			else
			{
				bool flag = !code.IsReserved();
				this.closeAsync(new PayloadData(code, reason), flag, flag, false);
			}
		}

		private void closeAsync(PayloadData payloadData, bool send, bool receive, bool received)
		{
			Action<PayloadData, bool, bool, bool> closer = new Action<PayloadData, bool, bool, bool>(this.close);
			closer.BeginInvoke(payloadData, send, receive, received, delegate(IAsyncResult ar)
			{
				closer.EndInvoke(ar);
			}, null);
		}

		private bool closeHandshake(byte[] frameAsBytes, bool receive, bool received)
		{
			bool flag = frameAsBytes != null && this.sendBytes(frameAsBytes);
			if (!received && flag && receive && this._receivingExited != null)
			{
				received = this._receivingExited.WaitOne(this._waitTime);
			}
			bool flag2 = flag && received;
			this._logger.Debug(string.Format(WebSocket.getString_0(107137451), flag2, flag, received));
			return flag2;
		}

		private bool closeHandshake(PayloadData payloadData, bool send, bool receive, bool received)
		{
			bool flag = false;
			if (send)
			{
				WebSocketFrame webSocketFrame = WebSocketFrame.CreateCloseFrame(payloadData, this._client);
				flag = this.sendBytes(webSocketFrame.ToArray());
				if (this._client)
				{
					webSocketFrame.Unmask();
				}
			}
			if (!received && flag && receive && this._receivingExited != null)
			{
				received = this._receivingExited.WaitOne(this._waitTime);
			}
			bool flag2 = flag && received;
			this._logger.Debug(string.Format(WebSocket.getString_0(107137451), flag2, flag, received));
			return flag2;
		}

		private bool connect()
		{
			bool result;
			if (this._readyState == WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107139737);
				this._logger.Warn(message);
				result = false;
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					if (this._readyState == WebSocketState.Open)
					{
						string message2 = WebSocket.getString_0(107139737);
						this._logger.Warn(message2);
						result = false;
					}
					else if (this._readyState == WebSocketState.Closing)
					{
						string message3 = WebSocket.getString_0(107140189);
						this._logger.Error(message3);
						message3 = WebSocket.getString_0(107137390);
						this.error(message3, null);
						result = false;
					}
					else if (this._retryCountForConnect > WebSocket._maxRetryCountForConnect)
					{
						string message4 = WebSocket.getString_0(107137345);
						this._logger.Error(message4);
						message4 = WebSocket.getString_0(107137390);
						this.error(message4, null);
						result = false;
					}
					else
					{
						this._readyState = WebSocketState.Connecting;
						try
						{
							this.doHandshake();
						}
						catch (Exception ex)
						{
							this._retryCountForConnect++;
							this._logger.Fatal(ex.Message);
							this._logger.Debug(ex.ToString());
							string message5 = WebSocket.getString_0(107137792);
							this.fatal(message5, ex);
							return false;
						}
						this._retryCountForConnect = 1;
						this._readyState = WebSocketState.Open;
						result = true;
					}
				}
			}
			return result;
		}

		private string createExtensions()
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			if (this._compression > CompressionMethod.None)
			{
				string arg = this._compression.ToExtensionString(new string[]
				{
					WebSocket.getString_0(107137687),
					WebSocket.getString_0(107137650)
				});
				stringBuilder.AppendFormat(WebSocket.getString_0(107137613), arg);
			}
			int length = stringBuilder.Length;
			string result;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				result = stringBuilder.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		private HttpResponse createHandshakeFailureResponse(HttpStatusCode code)
		{
			HttpResponse httpResponse = HttpResponse.CreateCloseResponse(code);
			httpResponse.Headers[WebSocket.getString_0(107142882)] = WebSocket.getString_0(107138862);
			return httpResponse;
		}

		private HttpRequest createHandshakeRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateWebSocketRequest(this._uri);
			NameValueCollection headers = httpRequest.Headers;
			if (!this._origin.IsNullOrEmpty())
			{
				headers[WebSocket.getString_0(107142859)] = this._origin;
			}
			if (!this._userAgent.IsNullOrEmpty())
			{
				headers[WebSocket.getString_0(107325810)] = this._userAgent;
			}
			headers[WebSocket.getString_0(107139795)] = this._base64Key;
			this._protocolsRequested = (this._protocols != null);
			if (this._protocolsRequested)
			{
				headers[WebSocket.getString_0(107142509)] = this._protocols.ToString(WebSocket.getString_0(107405290));
			}
			this._extensionsRequested = (this._compression > CompressionMethod.None);
			if (this._extensionsRequested)
			{
				headers[WebSocket.getString_0(107139770)] = this.createExtensions();
			}
			headers[WebSocket.getString_0(107142882)] = WebSocket.getString_0(107138862);
			AuthenticationResponse authenticationResponse = null;
			if (this._authChallenge != null && this._credentials != null)
			{
				authenticationResponse = new AuthenticationResponse(this._authChallenge, this._credentials, this._nonceCount);
				this._nonceCount = authenticationResponse.NonceCount;
			}
			else if (this._preAuth)
			{
				authenticationResponse = new AuthenticationResponse(this._credentials);
			}
			if (authenticationResponse != null)
			{
				headers[WebSocket.getString_0(107137636)] = authenticationResponse.ToString();
			}
			if (this._cookies.Count > 0)
			{
				httpRequest.SetCookies(this._cookies);
			}
			return httpRequest;
		}

		private HttpResponse createHandshakeResponse()
		{
			HttpResponse httpResponse = HttpResponse.CreateWebSocketResponse();
			NameValueCollection headers = httpResponse.Headers;
			headers[WebSocket.getString_0(107142471)] = WebSocket.CreateResponseKey(this._base64Key);
			if (this._protocol != null)
			{
				headers[WebSocket.getString_0(107142509)] = this._protocol;
			}
			if (this._extensions != null)
			{
				headers[WebSocket.getString_0(107139770)] = this._extensions;
			}
			if (this._cookies.Count > 0)
			{
				httpResponse.SetCookies(this._cookies);
			}
			return httpResponse;
		}

		private bool customCheckHandshakeRequest(WebSocketContext context, out string message)
		{
			message = null;
			bool result;
			if (this._handshakeRequestChecker == null)
			{
				result = true;
			}
			else
			{
				message = this._handshakeRequestChecker(context);
				result = (message == null);
			}
			return result;
		}

		private MessageEventArgs dequeueFromMessageEventQueue()
		{
			object forMessageEventQueue = this._forMessageEventQueue;
			MessageEventArgs result;
			lock (forMessageEventQueue)
			{
				result = ((this._messageEventQueue.Count > 0) ? this._messageEventQueue.Dequeue() : null);
			}
			return result;
		}

		private void doHandshake()
		{
			this.setClientStream();
			HttpResponse httpResponse = this.sendHandshakeRequest();
			string message;
			if (!this.checkHandshakeResponse(httpResponse, out message))
			{
				throw new WebSocketException(CloseStatusCode.ProtocolError, message);
			}
			if (this._protocolsRequested)
			{
				this._protocol = httpResponse.Headers[WebSocket.getString_0(107142509)];
			}
			if (this._extensionsRequested)
			{
				this.processSecWebSocketExtensionsServerHeader(httpResponse.Headers[WebSocket.getString_0(107139770)]);
			}
			this.processCookies(httpResponse.Cookies);
		}

		private void enqueueToMessageEventQueue(MessageEventArgs e)
		{
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				this._messageEventQueue.Enqueue(e);
			}
		}

		private void error(string message, Exception exception)
		{
			try
			{
				this.OnError.Emit(this, new ErrorEventArgs(message, exception));
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		private void fatal(string message, Exception exception)
		{
			CloseStatusCode code = (exception is WebSocketException) ? ((WebSocketException)exception).Code : CloseStatusCode.Abnormal;
			this.fatal(message, (ushort)code);
		}

		private void fatal(string message, ushort code)
		{
			PayloadData payloadData = new PayloadData(code, message);
			this.close(payloadData, !code.IsReserved(), false, false);
		}

		private void fatal(string message, CloseStatusCode code)
		{
			this.fatal(message, (ushort)code);
		}

		private ClientSslConfiguration getSslConfiguration()
		{
			if (this._sslConfig == null)
			{
				this._sslConfig = new ClientSslConfiguration(this._uri.DnsSafeHost);
			}
			return this._sslConfig;
		}

		private void init()
		{
			this._compression = CompressionMethod.None;
			this._cookies = new CookieCollection();
			this._forPing = new object();
			this._forSend = new object();
			this._forState = new object();
			this._messageEventQueue = new Queue<MessageEventArgs>();
			this._forMessageEventQueue = ((ICollection)this._messageEventQueue).SyncRoot;
			this._readyState = WebSocketState.Connecting;
		}

		private void message()
		{
			MessageEventArgs obj = null;
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				if (this._inMessage || this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open)
				{
					return;
				}
				this._inMessage = true;
				obj = this._messageEventQueue.Dequeue();
			}
			this._message(obj);
		}

		private void messagec(MessageEventArgs e)
		{
			for (;;)
			{
				try
				{
					this.OnMessage.Emit(this, e);
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error(WebSocket.getString_0(107137583), ex);
				}
				object forMessageEventQueue = this._forMessageEventQueue;
				lock (forMessageEventQueue)
				{
					if (this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open)
					{
						this._inMessage = false;
						break;
					}
					e = this._messageEventQueue.Dequeue();
				}
			}
		}

		private void messages(MessageEventArgs e)
		{
			try
			{
				this.OnMessage.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.ToString());
				this.error(WebSocket.getString_0(107137583), ex);
			}
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				if (this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open)
				{
					this._inMessage = false;
					return;
				}
				e = this._messageEventQueue.Dequeue();
			}
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.messages(e);
			});
		}

		private void open()
		{
			this._inMessage = true;
			this.startReceiving();
			try
			{
				this.OnOpen.Emit(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.ToString());
				this.error(WebSocket.getString_0(107137006), ex);
			}
			MessageEventArgs obj = null;
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				if (this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open)
				{
					this._inMessage = false;
					return;
				}
				obj = this._messageEventQueue.Dequeue();
			}
			this._message.BeginInvoke(obj, delegate(IAsyncResult ar)
			{
				this._message.EndInvoke(ar);
			}, null);
		}

		private bool ping(byte[] data)
		{
			bool result;
			if (this._readyState != WebSocketState.Open)
			{
				result = false;
			}
			else
			{
				ManualResetEvent pongReceived = this._pongReceived;
				if (pongReceived == null)
				{
					result = false;
				}
				else
				{
					object forPing = this._forPing;
					lock (forPing)
					{
						try
						{
							pongReceived.Reset();
							if (!this.send(Fin.Final, Opcode.Ping, data, false))
							{
								result = false;
							}
							else
							{
								result = pongReceived.WaitOne(this._waitTime);
							}
						}
						catch (ObjectDisposedException)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		private bool processCloseFrame(WebSocketFrame frame)
		{
			PayloadData payloadData = frame.PayloadData;
			this.close(payloadData, !payloadData.HasReservedCode, false, true);
			return false;
		}

		private void processCookies(CookieCollection cookies)
		{
			if (cookies.Count != 0)
			{
				this._cookies.SetOrRemove(cookies);
			}
		}

		private bool processDataFrame(WebSocketFrame frame)
		{
			this.enqueueToMessageEventQueue(frame.IsCompressed ? new MessageEventArgs(frame.Opcode, frame.PayloadData.ApplicationData.Decompress(this._compression)) : new MessageEventArgs(frame));
			return true;
		}

		private bool processFragmentFrame(WebSocketFrame frame)
		{
			if (!this._inContinuation)
			{
				if (frame.IsContinuation)
				{
					return true;
				}
				this._fragmentsOpcode = frame.Opcode;
				this._fragmentsCompressed = frame.IsCompressed;
				this._fragmentsBuffer = new MemoryStream();
				this._inContinuation = true;
			}
			this._fragmentsBuffer.WriteBytes(frame.PayloadData.ApplicationData, 1024);
			if (frame.IsFinal)
			{
				using (this._fragmentsBuffer)
				{
					byte[] rawData = this._fragmentsCompressed ? this._fragmentsBuffer.DecompressToArray(this._compression) : this._fragmentsBuffer.ToArray();
					this.enqueueToMessageEventQueue(new MessageEventArgs(this._fragmentsOpcode, rawData));
				}
				this._fragmentsBuffer = null;
				this._inContinuation = false;
			}
			return true;
		}

		private bool processPingFrame(WebSocketFrame frame)
		{
			this._logger.Trace(WebSocket.getString_0(107136941));
			WebSocketFrame webSocketFrame = WebSocketFrame.CreatePongFrame(frame.PayloadData, this._client);
			object forState = this._forState;
			lock (forState)
			{
				if (this._readyState != WebSocketState.Open)
				{
					this._logger.Error(WebSocket.getString_0(107139708));
					return true;
				}
				if (!this.sendBytes(webSocketFrame.ToArray()))
				{
					return false;
				}
			}
			this._logger.Trace(WebSocket.getString_0(107136912));
			if (this._emitOnPing)
			{
				if (this._client)
				{
					webSocketFrame.Unmask();
				}
				this.enqueueToMessageEventQueue(new MessageEventArgs(frame));
			}
			return true;
		}

		private bool processPongFrame(WebSocketFrame frame)
		{
			this._logger.Trace(WebSocket.getString_0(107136895));
			try
			{
				this._pongReceived.Set();
			}
			catch (NullReferenceException ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
				return false;
			}
			catch (ObjectDisposedException ex2)
			{
				this._logger.Error(ex2.Message);
				this._logger.Debug(ex2.ToString());
				return false;
			}
			this._logger.Trace(WebSocket.getString_0(107136866));
			return true;
		}

		private bool processReceivedFrame(WebSocketFrame frame)
		{
			string message;
			if (!this.checkReceivedFrame(frame, out message))
			{
				throw new WebSocketException(CloseStatusCode.ProtocolError, message);
			}
			frame.Unmask();
			return frame.IsFragment ? this.processFragmentFrame(frame) : (frame.IsData ? this.processDataFrame(frame) : (frame.IsPing ? this.processPingFrame(frame) : (frame.IsPong ? this.processPongFrame(frame) : (frame.IsClose ? this.processCloseFrame(frame) : this.processUnsupportedFrame(frame)))));
		}

		private void processSecWebSocketExtensionsClientHeader(string value)
		{
			if (value != null)
			{
				StringBuilder stringBuilder = new StringBuilder(80);
				bool flag = false;
				foreach (string text in value.SplitHeaderValue(new char[]
				{
					','
				}))
				{
					string text2 = text.Trim();
					if (text2.Length != 0 && !flag && text2.IsCompressionExtension(CompressionMethod.Deflate))
					{
						this._compression = CompressionMethod.Deflate;
						stringBuilder.AppendFormat(WebSocket.getString_0(107137613), this._compression.ToExtensionString(new string[]
						{
							WebSocket.getString_0(107137650),
							WebSocket.getString_0(107137687)
						}));
						flag = true;
					}
				}
				int length = stringBuilder.Length;
				if (length > 2)
				{
					stringBuilder.Length = length - 2;
					this._extensions = stringBuilder.ToString();
				}
			}
		}

		private void processSecWebSocketExtensionsServerHeader(string value)
		{
			if (value == null)
			{
				this._compression = CompressionMethod.None;
			}
			else
			{
				this._extensions = value;
			}
		}

		private void processSecWebSocketProtocolClientHeader(IEnumerable<string> values)
		{
			if (!values.Contains((string val) => val == this._protocol))
			{
				this._protocol = null;
			}
		}

		private bool processUnsupportedFrame(WebSocketFrame frame)
		{
			this._logger.Fatal(WebSocket.getString_0(107136837) + frame.PrintToString(false));
			this.fatal(WebSocket.getString_0(107136808), CloseStatusCode.PolicyViolation);
			return false;
		}

		private void refuseHandshake(CloseStatusCode code, string reason)
		{
			this._readyState = WebSocketState.Closing;
			HttpResponse response = this.createHandshakeFailureResponse(HttpStatusCode.BadRequest);
			this.sendHttpResponse(response);
			this.releaseServerResources();
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs((ushort)code, reason, false);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		private void releaseClientResources()
		{
			if (this._stream != null)
			{
				this._stream.Dispose();
				this._stream = null;
			}
			if (this._tcpClient != null)
			{
				this._tcpClient.Close();
				this._tcpClient = null;
			}
		}

		private void releaseCommonResources()
		{
			if (this._fragmentsBuffer != null)
			{
				this._fragmentsBuffer.Dispose();
				this._fragmentsBuffer = null;
				this._inContinuation = false;
			}
			if (this._pongReceived != null)
			{
				this._pongReceived.Close();
				this._pongReceived = null;
			}
			if (this._receivingExited != null)
			{
				this._receivingExited.Close();
				this._receivingExited = null;
			}
		}

		private void releaseResources()
		{
			if (this._client)
			{
				this.releaseClientResources();
			}
			else
			{
				this.releaseServerResources();
			}
			this.releaseCommonResources();
		}

		private void releaseServerResources()
		{
			if (this._closeContext != null)
			{
				this._closeContext();
				this._closeContext = null;
				this._stream = null;
				this._context = null;
			}
		}

		private bool send(Opcode opcode, Stream stream)
		{
			object forSend = this._forSend;
			bool result;
			lock (forSend)
			{
				Stream stream2 = stream;
				bool flag = false;
				bool flag2 = false;
				try
				{
					if (this._compression > CompressionMethod.None)
					{
						stream = stream.Compress(this._compression);
						flag = true;
					}
					if (!(flag2 = this.send(opcode, stream, flag)))
					{
						this.error(WebSocket.getString_0(107137279), null);
					}
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error(WebSocket.getString_0(107137206), ex);
				}
				finally
				{
					if (flag)
					{
						stream.Dispose();
					}
					stream2.Dispose();
				}
				result = flag2;
			}
			return result;
		}

		private bool send(Opcode opcode, Stream stream, bool compressed)
		{
			long length = stream.Length;
			bool result;
			if (length == 0L)
			{
				result = this.send(Fin.Final, opcode, WebSocket.EmptyBytes, false);
			}
			else
			{
				long num = length / (long)WebSocket.FragmentLength;
				int num2 = (int)(length % (long)WebSocket.FragmentLength);
				if (num == 0L)
				{
					byte[] array = new byte[num2];
					result = (stream.Read(array, 0, num2) == num2 && this.send(Fin.Final, opcode, array, compressed));
				}
				else if (num == 1L && num2 == 0)
				{
					byte[] array = new byte[WebSocket.FragmentLength];
					result = (stream.Read(array, 0, WebSocket.FragmentLength) == WebSocket.FragmentLength && this.send(Fin.Final, opcode, array, compressed));
				}
				else
				{
					byte[] array = new byte[WebSocket.FragmentLength];
					if (stream.Read(array, 0, WebSocket.FragmentLength) != WebSocket.FragmentLength || !this.send(Fin.More, opcode, array, compressed))
					{
						result = false;
					}
					else
					{
						long num3 = (num2 == 0) ? (num - 2L) : (num - 1L);
						for (long num4 = 0L; num4 < num3; num4 += 1L)
						{
							if (stream.Read(array, 0, WebSocket.FragmentLength) != WebSocket.FragmentLength || !this.send(Fin.More, Opcode.Cont, array, false))
							{
								return false;
							}
						}
						if (num2 == 0)
						{
							num2 = WebSocket.FragmentLength;
						}
						else
						{
							array = new byte[num2];
						}
						result = (stream.Read(array, 0, num2) == num2 && this.send(Fin.Final, Opcode.Cont, array, false));
					}
				}
			}
			return result;
		}

		private bool send(Fin fin, Opcode opcode, byte[] data, bool compressed)
		{
			object forState = this._forState;
			bool result;
			lock (forState)
			{
				if (this._readyState != WebSocketState.Open)
				{
					this._logger.Error(WebSocket.getString_0(107139708));
					result = false;
				}
				else
				{
					WebSocketFrame webSocketFrame = new WebSocketFrame(fin, opcode, data, compressed, this._client);
					result = this.sendBytes(webSocketFrame.ToArray());
				}
			}
			return result;
		}

		private void sendAsync(Opcode opcode, Stream stream, Action<bool> completed)
		{
			Func<Opcode, Stream, bool> sender = new Func<Opcode, Stream, bool>(this.send);
			sender.BeginInvoke(opcode, stream, delegate(IAsyncResult ar)
			{
				try
				{
					bool obj = sender.EndInvoke(ar);
					if (completed != null)
					{
						completed(obj);
					}
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error(WebSocket.<>c__DisplayClass171_0.getString_0(107157876), ex);
				}
			}, null);
		}

		private bool sendBytes(byte[] bytes)
		{
			try
			{
				this._stream.Write(bytes, 0, bytes.Length);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
				return false;
			}
			return true;
		}

		private HttpResponse sendHandshakeRequest()
		{
			HttpRequest httpRequest = this.createHandshakeRequest();
			HttpResponse httpResponse = this.sendHttpRequest(httpRequest, 90000);
			if (httpResponse.IsUnauthorized)
			{
				string text = httpResponse.Headers[WebSocket.getString_0(107137189)];
				this._logger.Warn(string.Format(WebSocket.getString_0(107137132), text));
				if (text.IsNullOrEmpty())
				{
					this._logger.Error(WebSocket.getString_0(107137095));
					return httpResponse;
				}
				this._authChallenge = AuthenticationChallenge.Parse(text);
				if (this._authChallenge == null)
				{
					this._logger.Error(WebSocket.getString_0(107136494));
					return httpResponse;
				}
				if (this._credentials != null && (!this._preAuth || this._authChallenge.Scheme == AuthenticationSchemes.Digest))
				{
					if (httpResponse.HasConnectionClose)
					{
						this.releaseClientResources();
						this.setClientStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(this._authChallenge, this._credentials, this._nonceCount);
					this._nonceCount = authenticationResponse.NonceCount;
					httpRequest.Headers[WebSocket.getString_0(107137636)] = authenticationResponse.ToString();
					httpResponse = this.sendHttpRequest(httpRequest, 15000);
				}
			}
			if (httpResponse.IsRedirect)
			{
				string text2 = httpResponse.Headers[WebSocket.getString_0(107244940)];
				this._logger.Warn(string.Format(WebSocket.getString_0(107136457), text2));
				if (this._enableRedirection)
				{
					if (text2.IsNullOrEmpty())
					{
						this._logger.Error(WebSocket.getString_0(107136412));
						return httpResponse;
					}
					Uri uri;
					string str;
					if (!text2.TryCreateWebSocketUri(out uri, out str))
					{
						this._logger.Error(WebSocket.getString_0(107136339) + str);
						return httpResponse;
					}
					this.releaseClientResources();
					this._uri = uri;
					this._secure = (uri.Scheme == WebSocket.getString_0(107363404));
					this.setClientStream();
					return this.sendHandshakeRequest();
				}
			}
			return httpResponse;
		}

		private HttpResponse sendHttpRequest(HttpRequest request, int millisecondsTimeout)
		{
			this._logger.Debug(WebSocket.getString_0(107136318) + request.ToString());
			HttpResponse response = request.GetResponse(this._stream, millisecondsTimeout);
			this._logger.Debug(WebSocket.getString_0(107136761) + response.ToString());
			return response;
		}

		private bool sendHttpResponse(HttpResponse response)
		{
			this._logger.Debug(string.Format(WebSocket.getString_0(107136720), this._context.UserEndPoint, response));
			return this.sendBytes(response.ToByteArray());
		}

		private void sendProxyConnectRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateConnectRequest(this._uri);
			HttpResponse httpResponse = this.sendHttpRequest(httpRequest, 90000);
			if (httpResponse.IsProxyAuthenticationRequired)
			{
				string text = httpResponse.Headers[WebSocket.getString_0(107136687)];
				this._logger.Warn(string.Format(WebSocket.getString_0(107136662), text));
				if (text.IsNullOrEmpty())
				{
					throw new WebSocketException(WebSocket.getString_0(107136589));
				}
				AuthenticationChallenge authenticationChallenge = AuthenticationChallenge.Parse(text);
				if (authenticationChallenge == null)
				{
					throw new WebSocketException(WebSocket.getString_0(107136524));
				}
				if (this._proxyCredentials != null)
				{
					if (httpResponse.HasConnectionClose)
					{
						this.releaseClientResources();
						this._tcpClient = new TcpClient(this._proxyUri.DnsSafeHost, this._proxyUri.Port);
						this._stream = this._tcpClient.GetStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(authenticationChallenge, this._proxyCredentials, 0U);
					httpRequest.Headers[WebSocket.getString_0(107135967)] = authenticationResponse.ToString();
					httpResponse = this.sendHttpRequest(httpRequest, 15000);
				}
				if (httpResponse.IsProxyAuthenticationRequired)
				{
					throw new WebSocketException(WebSocket.getString_0(107135938));
				}
			}
			if (httpResponse.StatusCode[0] != '2')
			{
				throw new WebSocketException(WebSocket.getString_0(107135857));
			}
		}

		private void setClientStream()
		{
			if (this._proxyUri != null)
			{
				this._tcpClient = new TcpClient(this._proxyUri.DnsSafeHost, this._proxyUri.Port);
				this._stream = this._tcpClient.GetStream();
				this.sendProxyConnectRequest();
			}
			else
			{
				this._tcpClient = new TcpClient(this._uri.DnsSafeHost, this._uri.Port);
				this._stream = this._tcpClient.GetStream();
			}
			if (this._secure)
			{
				ClientSslConfiguration sslConfiguration = this.getSslConfiguration();
				string targetHost = sslConfiguration.TargetHost;
				if (targetHost != this._uri.DnsSafeHost)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, WebSocket.getString_0(107135768));
				}
				try
				{
					SslStream sslStream = new SslStream(this._stream, false, sslConfiguration.ServerCertificateValidationCallback, sslConfiguration.ClientCertificateSelectionCallback);
					sslStream.AuthenticateAsClient(targetHost, sslConfiguration.ClientCertificates, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
					this._stream = sslStream;
				}
				catch (Exception innerException)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, innerException);
				}
			}
		}

		private void startReceiving()
		{
			if (this._messageEventQueue.Count > 0)
			{
				this._messageEventQueue.Clear();
			}
			this._pongReceived = new ManualResetEvent(false);
			this._receivingExited = new ManualResetEvent(false);
			Action receive = null;
			Action<WebSocketFrame> <>9__1;
			Action<Exception> <>9__2;
			receive = delegate()
			{
				Stream stream = this._stream;
				bool unmask = false;
				Action<WebSocketFrame> completed;
				if ((completed = <>9__1) == null)
				{
					completed = (<>9__1 = delegate(WebSocketFrame frame)
					{
						if (!this.processReceivedFrame(frame) || this._readyState == WebSocketState.Closed)
						{
							ManualResetEvent receivingExited = this._receivingExited;
							if (receivingExited != null)
							{
								receivingExited.Set();
							}
						}
						else
						{
							receive();
							if (!this._inMessage && this.HasMessage && this._readyState == WebSocketState.Open)
							{
								this.message();
							}
						}
					});
				}
				Action<Exception> error;
				if ((error = <>9__2) == null)
				{
					error = (<>9__2 = delegate(Exception ex)
					{
						this._logger.Fatal(ex.ToString());
						this.fatal(WebSocket.<>c__DisplayClass178_0.getString_0(107158280), ex);
					});
				}
				WebSocketFrame.ReadFrameAsync(stream, unmask, completed, error);
			};
			receive();
		}

		private bool validateSecWebSocketAcceptHeader(string value)
		{
			return value != null && value == WebSocket.CreateResponseKey(this._base64Key);
		}

		private bool validateSecWebSocketExtensionsServerHeader(string value)
		{
			bool result;
			if (value == null)
			{
				result = true;
			}
			else if (value.Length == 0)
			{
				result = false;
			}
			else if (!this._extensionsRequested)
			{
				result = false;
			}
			else
			{
				bool flag = this._compression > CompressionMethod.None;
				foreach (string text in value.SplitHeaderValue(new char[]
				{
					','
				}))
				{
					string text2 = text.Trim();
					if (!flag || !text2.IsCompressionExtension(this._compression))
					{
						return false;
					}
					if (!text2.Contains(WebSocket.getString_0(107137687)))
					{
						this._logger.Error(WebSocket.getString_0(107136263));
						return false;
					}
					if (!text2.Contains(WebSocket.getString_0(107137650)))
					{
						this._logger.Warn(WebSocket.getString_0(107136154));
					}
					string method = this._compression.ToExtensionString(new string[0]);
					if (text2.SplitHeaderValue(new char[]
					{
						';'
					}).Contains(delegate(string t)
					{
						t = t.Trim();
						return t != method && t != WebSocket.<>c__DisplayClass180_0.getString_0(107137710) && t != WebSocket.<>c__DisplayClass180_0.getString_0(107137673);
					}))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		private bool validateSecWebSocketProtocolServerHeader(string value)
		{
			bool result;
			if (value == null)
			{
				result = !this._protocolsRequested;
			}
			else
			{
				result = (value.Length != 0 && this._protocolsRequested && this._protocols.Contains((string p) => p == value));
			}
			return result;
		}

		private bool validateSecWebSocketVersionServerHeader(string value)
		{
			return value == null || value == WebSocket.getString_0(107138862);
		}

		internal void Close(HttpResponse response)
		{
			this._readyState = WebSocketState.Closing;
			this.sendHttpResponse(response);
			this.releaseServerResources();
			this._readyState = WebSocketState.Closed;
		}

		internal void Close(HttpStatusCode code)
		{
			this.Close(this.createHandshakeFailureResponse(code));
		}

		internal void Close(PayloadData payloadData, byte[] frameAsBytes)
		{
			object forState = this._forState;
			lock (forState)
			{
				if (this._readyState == WebSocketState.Closing)
				{
					this._logger.Info(WebSocket.getString_0(107138175));
					return;
				}
				if (this._readyState == WebSocketState.Closed)
				{
					this._logger.Info(WebSocket.getString_0(107138094));
					return;
				}
				this._readyState = WebSocketState.Closing;
			}
			this._logger.Trace(WebSocket.getString_0(107137529));
			bool flag2;
			bool flag = (flag2 = (frameAsBytes != null && this.sendBytes(frameAsBytes))) && this._receivingExited != null && this._receivingExited.WaitOne(this._waitTime);
			bool flag3 = flag2 && flag;
			this._logger.Debug(string.Format(WebSocket.getString_0(107137451), flag3, flag2, flag));
			this.releaseServerResources();
			this.releaseCommonResources();
			this._logger.Trace(WebSocket.getString_0(107137488));
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs(payloadData, flag3);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		internal static string CreateBase64Key()
		{
			byte[] array = new byte[16];
			WebSocket.RandomNumber.GetBytes(array);
			return Convert.ToBase64String(array);
		}

		internal static string CreateResponseKey(string base64Key)
		{
			StringBuilder stringBuilder = new StringBuilder(base64Key, 64);
			stringBuilder.Append(WebSocket.getString_0(107143198));
			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] inArray = sha.ComputeHash(stringBuilder.ToString().GetUTF8EncodedBytes());
			return Convert.ToBase64String(inArray);
		}

		internal void InternalAccept()
		{
			try
			{
				if (!this.acceptHandshake())
				{
					return;
				}
			}
			catch (Exception ex)
			{
				this._logger.Fatal(ex.Message);
				this._logger.Debug(ex.ToString());
				string message = WebSocket.getString_0(107139482);
				this.fatal(message, ex);
				return;
			}
			this._readyState = WebSocketState.Open;
			this.open();
		}

		internal bool Ping(byte[] frameAsBytes, TimeSpan timeout)
		{
			bool result;
			if (this._readyState != WebSocketState.Open)
			{
				result = false;
			}
			else
			{
				ManualResetEvent pongReceived = this._pongReceived;
				if (pongReceived == null)
				{
					result = false;
				}
				else
				{
					object forPing = this._forPing;
					lock (forPing)
					{
						try
						{
							pongReceived.Reset();
							object forState = this._forState;
							lock (forState)
							{
								if (this._readyState != WebSocketState.Open)
								{
									return false;
								}
								if (!this.sendBytes(frameAsBytes))
								{
									return false;
								}
							}
							result = pongReceived.WaitOne(timeout);
						}
						catch (ObjectDisposedException)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		internal void Send(Opcode opcode, byte[] data, Dictionary<CompressionMethod, byte[]> cache)
		{
			object forSend = this._forSend;
			lock (forSend)
			{
				object forState = this._forState;
				lock (forState)
				{
					if (this._readyState != WebSocketState.Open)
					{
						this._logger.Error(WebSocket.getString_0(107139708));
					}
					else
					{
						byte[] array;
						if (!cache.TryGetValue(this._compression, out array))
						{
							array = new WebSocketFrame(Fin.Final, opcode, data.Compress(this._compression), this._compression > CompressionMethod.None, false).ToArray();
							cache.Add(this._compression, array);
						}
						this.sendBytes(array);
					}
				}
			}
		}

		internal void Send(Opcode opcode, Stream stream, Dictionary<CompressionMethod, Stream> cache)
		{
			object forSend = this._forSend;
			lock (forSend)
			{
				Stream stream2;
				if (!cache.TryGetValue(this._compression, out stream2))
				{
					stream2 = stream.Compress(this._compression);
					cache.Add(this._compression, stream2);
				}
				else
				{
					stream2.Position = 0L;
				}
				this.send(opcode, stream2, this._compression > CompressionMethod.None);
			}
		}

		public void Accept()
		{
			if (this._client)
			{
				string message = WebSocket.getString_0(107136077);
				throw new InvalidOperationException(message);
			}
			if (this._readyState == WebSocketState.Closing)
			{
				string message2 = WebSocket.getString_0(107136072);
				throw new InvalidOperationException(message2);
			}
			if (this._readyState == WebSocketState.Closed)
			{
				string message3 = WebSocket.getString_0(107138094);
				throw new InvalidOperationException(message3);
			}
			if (this.accept())
			{
				this.open();
			}
		}

		public void AcceptAsync()
		{
			if (this._client)
			{
				string message = WebSocket.getString_0(107136077);
				throw new InvalidOperationException(message);
			}
			if (this._readyState == WebSocketState.Closing)
			{
				string message2 = WebSocket.getString_0(107136072);
				throw new InvalidOperationException(message2);
			}
			if (this._readyState == WebSocketState.Closed)
			{
				string message3 = WebSocket.getString_0(107138094);
				throw new InvalidOperationException(message3);
			}
			Func<bool> acceptor = new Func<bool>(this.accept);
			acceptor.BeginInvoke(delegate(IAsyncResult ar)
			{
				if (acceptor.EndInvoke(ar))
				{
					this.open();
				}
			}, null);
		}

		public void Close()
		{
			this.close(1005, string.Empty);
		}

		public void Close(ushort code)
		{
			if (!code.IsCloseStatusCode())
			{
				string message = WebSocket.getString_0(107136027);
				throw new ArgumentOutOfRangeException(WebSocket.getString_0(107313720), message);
			}
			if (this._client && code == 1011)
			{
				string message2 = WebSocket.getString_0(107135466);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == 1010)
			{
				string message3 = WebSocket.getString_0(107135405);
				throw new ArgumentException(message3, WebSocket.getString_0(107313720));
			}
			this.close(code, string.Empty);
		}

		public void Close(CloseStatusCode code)
		{
			if (this._client && code == CloseStatusCode.ServerError)
			{
				string message = WebSocket.getString_0(107135376);
				throw new ArgumentException(message, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == CloseStatusCode.MandatoryExtension)
			{
				string message2 = WebSocket.getString_0(107135339);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			this.close((ushort)code, string.Empty);
		}

		public void Close(ushort code, string reason)
		{
			if (!code.IsCloseStatusCode())
			{
				string message = WebSocket.getString_0(107136027);
				throw new ArgumentOutOfRangeException(WebSocket.getString_0(107313720), message);
			}
			if (this._client && code == 1011)
			{
				string message2 = WebSocket.getString_0(107135466);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == 1010)
			{
				string message3 = WebSocket.getString_0(107135405);
				throw new ArgumentException(message3, WebSocket.getString_0(107313720));
			}
			if (reason.IsNullOrEmpty())
			{
				this.close(code, string.Empty);
			}
			else
			{
				if (code == 1005)
				{
					string message4 = WebSocket.getString_0(107135290);
					throw new ArgumentException(message4, WebSocket.getString_0(107313720));
				}
				byte[] array;
				if (!reason.TryGetUTF8EncodedBytes(out array))
				{
					string message5 = WebSocket.getString_0(107135293);
					throw new ArgumentException(message5, WebSocket.getString_0(107135732));
				}
				if (array.Length > 123)
				{
					string message6 = WebSocket.getString_0(107135723);
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107135732), message6);
				}
				this.close(code, reason);
			}
		}

		public void Close(CloseStatusCode code, string reason)
		{
			if (this._client && code == CloseStatusCode.ServerError)
			{
				string message = WebSocket.getString_0(107135376);
				throw new ArgumentException(message, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == CloseStatusCode.MandatoryExtension)
			{
				string message2 = WebSocket.getString_0(107135339);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (reason.IsNullOrEmpty())
			{
				this.close((ushort)code, string.Empty);
			}
			else
			{
				if (code == CloseStatusCode.NoStatus)
				{
					string message3 = WebSocket.getString_0(107135674);
					throw new ArgumentException(message3, WebSocket.getString_0(107313720));
				}
				byte[] array;
				if (!reason.TryGetUTF8EncodedBytes(out array))
				{
					string message4 = WebSocket.getString_0(107135293);
					throw new ArgumentException(message4, WebSocket.getString_0(107135732));
				}
				if (array.Length > 123)
				{
					string message5 = WebSocket.getString_0(107135723);
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107135732), message5);
				}
				this.close((ushort)code, reason);
			}
		}

		public void CloseAsync()
		{
			this.closeAsync(1005, string.Empty);
		}

		public void CloseAsync(ushort code)
		{
			if (!code.IsCloseStatusCode())
			{
				string message = WebSocket.getString_0(107136027);
				throw new ArgumentOutOfRangeException(WebSocket.getString_0(107313720), message);
			}
			if (this._client && code == 1011)
			{
				string message2 = WebSocket.getString_0(107135466);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == 1010)
			{
				string message3 = WebSocket.getString_0(107135405);
				throw new ArgumentException(message3, WebSocket.getString_0(107313720));
			}
			this.closeAsync(code, string.Empty);
		}

		public void CloseAsync(CloseStatusCode code)
		{
			if (this._client && code == CloseStatusCode.ServerError)
			{
				string message = WebSocket.getString_0(107135376);
				throw new ArgumentException(message, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == CloseStatusCode.MandatoryExtension)
			{
				string message2 = WebSocket.getString_0(107135339);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			this.closeAsync((ushort)code, string.Empty);
		}

		public void CloseAsync(ushort code, string reason)
		{
			if (!code.IsCloseStatusCode())
			{
				string message = WebSocket.getString_0(107136027);
				throw new ArgumentOutOfRangeException(WebSocket.getString_0(107313720), message);
			}
			if (this._client && code == 1011)
			{
				string message2 = WebSocket.getString_0(107135466);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == 1010)
			{
				string message3 = WebSocket.getString_0(107135405);
				throw new ArgumentException(message3, WebSocket.getString_0(107313720));
			}
			if (reason.IsNullOrEmpty())
			{
				this.closeAsync(code, string.Empty);
			}
			else
			{
				if (code == 1005)
				{
					string message4 = WebSocket.getString_0(107135290);
					throw new ArgumentException(message4, WebSocket.getString_0(107313720));
				}
				byte[] array;
				if (!reason.TryGetUTF8EncodedBytes(out array))
				{
					string message5 = WebSocket.getString_0(107135293);
					throw new ArgumentException(message5, WebSocket.getString_0(107135732));
				}
				if (array.Length > 123)
				{
					string message6 = WebSocket.getString_0(107135723);
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107135732), message6);
				}
				this.closeAsync(code, reason);
			}
		}

		public void CloseAsync(CloseStatusCode code, string reason)
		{
			if (this._client && code == CloseStatusCode.ServerError)
			{
				string message = WebSocket.getString_0(107135376);
				throw new ArgumentException(message, WebSocket.getString_0(107313720));
			}
			if (!this._client && code == CloseStatusCode.MandatoryExtension)
			{
				string message2 = WebSocket.getString_0(107135339);
				throw new ArgumentException(message2, WebSocket.getString_0(107313720));
			}
			if (reason.IsNullOrEmpty())
			{
				this.closeAsync((ushort)code, string.Empty);
			}
			else
			{
				if (code == CloseStatusCode.NoStatus)
				{
					string message3 = WebSocket.getString_0(107135674);
					throw new ArgumentException(message3, WebSocket.getString_0(107313720));
				}
				byte[] array;
				if (!reason.TryGetUTF8EncodedBytes(out array))
				{
					string message4 = WebSocket.getString_0(107135293);
					throw new ArgumentException(message4, WebSocket.getString_0(107135732));
				}
				if (array.Length > 123)
				{
					string message5 = WebSocket.getString_0(107135723);
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107135732), message5);
				}
				this.closeAsync((ushort)code, reason);
			}
		}

		public void Connect()
		{
			if (!this._client)
			{
				string message = WebSocket.getString_0(107139915);
				throw new InvalidOperationException(message);
			}
			if (this._readyState == WebSocketState.Closing)
			{
				string message2 = WebSocket.getString_0(107136072);
				throw new InvalidOperationException(message2);
			}
			if (this._retryCountForConnect > WebSocket._maxRetryCountForConnect)
			{
				string message3 = WebSocket.getString_0(107135641);
				throw new InvalidOperationException(message3);
			}
			if (this.connect())
			{
				this.open();
			}
		}

		public void ConnectAsync()
		{
			if (!this._client)
			{
				string message = WebSocket.getString_0(107139915);
				throw new InvalidOperationException(message);
			}
			if (this._readyState == WebSocketState.Closing)
			{
				string message2 = WebSocket.getString_0(107136072);
				throw new InvalidOperationException(message2);
			}
			if (this._retryCountForConnect > WebSocket._maxRetryCountForConnect)
			{
				string message3 = WebSocket.getString_0(107135641);
				throw new InvalidOperationException(message3);
			}
			Func<bool> connector = new Func<bool>(this.connect);
			connector.BeginInvoke(delegate(IAsyncResult ar)
			{
				if (connector.EndInvoke(ar))
				{
					this.open();
				}
			}, null);
		}

		public bool Ping()
		{
			return this.ping(WebSocket.EmptyBytes);
		}

		public bool Ping(string message)
		{
			bool result;
			if (message.IsNullOrEmpty())
			{
				result = this.ping(WebSocket.EmptyBytes);
			}
			else
			{
				byte[] array;
				if (!message.TryGetUTF8EncodedBytes(out array))
				{
					string message2 = WebSocket.getString_0(107135293);
					throw new ArgumentException(message2, WebSocket.getString_0(107257014));
				}
				if (array.Length > 125)
				{
					string message3 = WebSocket.getString_0(107135624);
					throw new ArgumentOutOfRangeException(WebSocket.getString_0(107257014), message3);
				}
				result = this.ping(array);
			}
			return result;
		}

		public void Send(byte[] data)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107403916));
			}
			this.send(Opcode.Binary, new MemoryStream(data));
		}

		public void Send(FileInfo fileInfo)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (fileInfo == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107134966));
			}
			if (!fileInfo.Exists)
			{
				string message2 = WebSocket.getString_0(107134985);
				throw new ArgumentException(message2, WebSocket.getString_0(107134966));
			}
			FileStream stream;
			if (!fileInfo.TryOpenRead(out stream))
			{
				string message3 = WebSocket.getString_0(107134952);
				throw new ArgumentException(message3, WebSocket.getString_0(107134966));
			}
			this.send(Opcode.Binary, stream);
		}

		public void Send(string data)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107403916));
			}
			byte[] buffer;
			if (!data.TryGetUTF8EncodedBytes(out buffer))
			{
				string message2 = WebSocket.getString_0(107135293);
				throw new ArgumentException(message2, WebSocket.getString_0(107403916));
			}
			this.send(Opcode.Text, new MemoryStream(buffer));
		}

		public void Send(Stream stream, int length)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107251035));
			}
			if (!stream.CanRead)
			{
				string message2 = WebSocket.getString_0(107134911);
				throw new ArgumentException(message2, WebSocket.getString_0(107251035));
			}
			if (length < 1)
			{
				string message3 = WebSocket.getString_0(107134886);
				throw new ArgumentException(message3, WebSocket.getString_0(107344206));
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = WebSocket.getString_0(107134837);
				throw new ArgumentException(message4, WebSocket.getString_0(107251035));
			}
			if (num < length)
			{
				this._logger.Warn(string.Format(WebSocket.getString_0(107134796), num));
			}
			this.send(Opcode.Binary, new MemoryStream(array));
		}

		public void SendAsync(byte[] data, Action<bool> completed)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107403916));
			}
			this.sendAsync(Opcode.Binary, new MemoryStream(data), completed);
		}

		public void SendAsync(FileInfo fileInfo, Action<bool> completed)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (fileInfo == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107134966));
			}
			if (!fileInfo.Exists)
			{
				string message2 = WebSocket.getString_0(107134985);
				throw new ArgumentException(message2, WebSocket.getString_0(107134966));
			}
			FileStream stream;
			if (!fileInfo.TryOpenRead(out stream))
			{
				string message3 = WebSocket.getString_0(107134952);
				throw new ArgumentException(message3, WebSocket.getString_0(107134966));
			}
			this.sendAsync(Opcode.Binary, stream, completed);
		}

		public void SendAsync(string data, Action<bool> completed)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107403916));
			}
			byte[] buffer;
			if (!data.TryGetUTF8EncodedBytes(out buffer))
			{
				string message2 = WebSocket.getString_0(107135293);
				throw new ArgumentException(message2, WebSocket.getString_0(107403916));
			}
			this.sendAsync(Opcode.Text, new MemoryStream(buffer), completed);
		}

		public void SendAsync(Stream stream, int length, Action<bool> completed)
		{
			if (this._readyState != WebSocketState.Open)
			{
				string message = WebSocket.getString_0(107135543);
				throw new InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107251035));
			}
			if (!stream.CanRead)
			{
				string message2 = WebSocket.getString_0(107134911);
				throw new ArgumentException(message2, WebSocket.getString_0(107251035));
			}
			if (length < 1)
			{
				string message3 = WebSocket.getString_0(107134886);
				throw new ArgumentException(message3, WebSocket.getString_0(107344206));
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = WebSocket.getString_0(107134837);
				throw new ArgumentException(message4, WebSocket.getString_0(107251035));
			}
			if (num < length)
			{
				this._logger.Warn(string.Format(WebSocket.getString_0(107134796), num));
			}
			this.sendAsync(Opcode.Binary, new MemoryStream(array), completed);
		}

		public void SetCookie(Cookie cookie)
		{
			string message = null;
			if (!this._client)
			{
				message = WebSocket.getString_0(107139915);
				throw new InvalidOperationException(message);
			}
			if (cookie == null)
			{
				throw new ArgumentNullException(WebSocket.getString_0(107134751));
			}
			if (!this.canSet(out message))
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					if (!this.canSet(out message))
					{
						this._logger.Warn(message);
					}
					else
					{
						object syncRoot = this._cookies.SyncRoot;
						lock (syncRoot)
						{
							this._cookies.SetOrRemove(cookie);
						}
					}
				}
			}
		}

		public void SetCredentials(string username, string password, bool preAuth)
		{
			string message = null;
			if (!this._client)
			{
				message = WebSocket.getString_0(107139915);
				throw new InvalidOperationException(message);
			}
			if (!username.IsNullOrEmpty() && (username.Contains(new char[]
			{
				':'
			}) || !username.IsText()))
			{
				message = WebSocket.getString_0(107135222);
				throw new ArgumentException(message, WebSocket.getString_0(107471845));
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				message = WebSocket.getString_0(107135222);
				throw new ArgumentException(message, WebSocket.getString_0(107309289));
			}
			if (!this.canSet(out message))
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					if (!this.canSet(out message))
					{
						this._logger.Warn(message);
					}
					else if (username.IsNullOrEmpty())
					{
						this._credentials = null;
						this._preAuth = false;
					}
					else
					{
						this._credentials = new NetworkCredential(username, password, this._uri.PathAndQuery, new string[0]);
						this._preAuth = preAuth;
					}
				}
			}
		}

		public void SetProxy(string url, string username, string password)
		{
			string message = null;
			if (!this._client)
			{
				message = WebSocket.getString_0(107139915);
				throw new InvalidOperationException(message);
			}
			Uri uri = null;
			if (!url.IsNullOrEmpty())
			{
				if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
				{
					message = WebSocket.getString_0(107139906);
					throw new ArgumentException(message, WebSocket.getString_0(107403684));
				}
				if (uri.Scheme != WebSocket.getString_0(107140100))
				{
					message = WebSocket.getString_0(107135209);
					throw new ArgumentException(message, WebSocket.getString_0(107403684));
				}
				if (uri.Segments.Length > 1)
				{
					message = WebSocket.getString_0(107139869);
					throw new ArgumentException(message, WebSocket.getString_0(107403684));
				}
			}
			if (!username.IsNullOrEmpty() && (username.Contains(new char[]
			{
				':'
			}) || !username.IsText()))
			{
				message = WebSocket.getString_0(107135222);
				throw new ArgumentException(message, WebSocket.getString_0(107471845));
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				message = WebSocket.getString_0(107135222);
				throw new ArgumentException(message, WebSocket.getString_0(107309289));
			}
			if (!this.canSet(out message))
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					if (!this.canSet(out message))
					{
						this._logger.Warn(message);
					}
					else if (url.IsNullOrEmpty())
					{
						this._proxyUri = null;
						this._proxyCredentials = null;
					}
					else
					{
						this._proxyUri = uri;
						this._proxyCredentials = ((!username.IsNullOrEmpty()) ? new NetworkCredential(username, password, string.Format(WebSocket.getString_0(107135168), this._uri.DnsSafeHost, this._uri.Port), new string[0]) : null);
					}
				}
			}
		}

		void IDisposable.Dispose()
		{
			this.close(1001, string.Empty);
		}

		private AuthenticationChallenge _authChallenge;

		private string _base64Key;

		private bool _client;

		private Action _closeContext;

		private CompressionMethod _compression;

		private WebSocketContext _context;

		private CookieCollection _cookies;

		private NetworkCredential _credentials;

		private bool _emitOnPing;

		private bool _enableRedirection;

		private string _extensions;

		private bool _extensionsRequested;

		private object _forMessageEventQueue;

		private object _forPing;

		private object _forSend;

		private object _forState;

		private MemoryStream _fragmentsBuffer;

		private bool _fragmentsCompressed;

		private Opcode _fragmentsOpcode;

		private const string _guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private Func<WebSocketContext, string> _handshakeRequestChecker;

		private bool _ignoreExtensions;

		private bool _inContinuation;

		private volatile bool _inMessage;

		private volatile Logger _logger;

		private static readonly int _maxRetryCountForConnect;

		private Action<MessageEventArgs> _message;

		private Queue<MessageEventArgs> _messageEventQueue;

		private uint _nonceCount;

		private string _origin;

		private string _userAgent;

		private ManualResetEvent _pongReceived;

		private bool _preAuth;

		private string _protocol;

		private string[] _protocols;

		private bool _protocolsRequested;

		private NetworkCredential _proxyCredentials;

		private Uri _proxyUri;

		private volatile WebSocketState _readyState;

		private ManualResetEvent _receivingExited;

		private int _retryCountForConnect;

		private bool _secure;

		private ClientSslConfiguration _sslConfig;

		private Stream _stream;

		private TcpClient _tcpClient;

		private Uri _uri;

		private const string _version = "13";

		private TimeSpan _waitTime;

		internal static readonly byte[] EmptyBytes;

		internal static readonly int FragmentLength;

		internal static readonly RandomNumberGenerator RandomNumber;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
