using System;
using System.Collections.Specialized;
using System.IO;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public abstract class WebSocketBehavior : IWebSocketSession
	{
		protected WebSocketBehavior()
		{
			this._startTime = DateTime.MaxValue;
		}

		protected NameValueCollection Headers
		{
			get
			{
				return (this._context != null) ? this._context.Headers : null;
			}
		}

		protected NameValueCollection QueryString
		{
			get
			{
				return (this._context != null) ? this._context.QueryString : null;
			}
		}

		protected WebSocketSessionManager Sessions
		{
			get
			{
				return this._sessions;
			}
		}

		public WebSocketState ConnectionState
		{
			get
			{
				return (this._websocket != null) ? this._websocket.ReadyState : WebSocketState.Connecting;
			}
		}

		public WebSocketContext Context
		{
			get
			{
				return this._context;
			}
		}

		public Func<CookieCollection, CookieCollection, bool> CookiesValidator
		{
			get
			{
				return this._cookiesValidator;
			}
			set
			{
				this._cookiesValidator = value;
			}
		}

		public bool EmitOnPing
		{
			get
			{
				return (this._websocket != null) ? this._websocket.EmitOnPing : this._emitOnPing;
			}
			set
			{
				if (this._websocket != null)
				{
					this._websocket.EmitOnPing = value;
				}
				else
				{
					this._emitOnPing = value;
				}
			}
		}

		public string ID
		{
			get
			{
				return this._id;
			}
		}

		public bool IgnoreExtensions
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

		public Func<string, bool> OriginValidator
		{
			get
			{
				return this._originValidator;
			}
			set
			{
				this._originValidator = value;
			}
		}

		public string Protocol
		{
			get
			{
				return (this._websocket != null) ? this._websocket.Protocol : (this._protocol ?? string.Empty);
			}
			set
			{
				if (this._websocket != null)
				{
					string message = WebSocketBehavior.getString_0(107159286);
					throw new InvalidOperationException(message);
				}
				if (value == null || value.Length == 0)
				{
					this._protocol = null;
				}
				else
				{
					if (!value.IsToken())
					{
						string message2 = WebSocketBehavior.getString_0(107159273);
						throw new ArgumentException(message2, WebSocketBehavior.getString_0(107458253));
					}
					this._protocol = value;
				}
			}
		}

		public DateTime StartTime
		{
			get
			{
				return this._startTime;
			}
		}

		private string checkHandshakeRequest(WebSocketContext context)
		{
			string result;
			if (this._originValidator != null && !this._originValidator(context.Origin))
			{
				result = WebSocketBehavior.getString_0(107158704);
			}
			else
			{
				if (this._cookiesValidator != null)
				{
					CookieCollection cookieCollection = context.CookieCollection;
					CookieCollection cookieCollection2 = context.WebSocket.CookieCollection;
					if (!this._cookiesValidator(cookieCollection, cookieCollection2))
					{
						return WebSocketBehavior.getString_0(107158671);
					}
				}
				result = null;
			}
			return result;
		}

		private void onClose(object sender, CloseEventArgs e)
		{
			if (this._id != null)
			{
				this._sessions.Remove(this._id);
				this.OnClose(e);
			}
		}

		private void onError(object sender, ErrorEventArgs e)
		{
			this.OnError(e);
		}

		private void onMessage(object sender, MessageEventArgs e)
		{
			this.OnMessage(e);
		}

		private void onOpen(object sender, EventArgs e)
		{
			this._id = this._sessions.Add(this);
			if (this._id == null)
			{
				this._websocket.Close(CloseStatusCode.Away);
			}
			else
			{
				this._startTime = DateTime.Now;
				this.OnOpen();
			}
		}

		internal void Start(WebSocketContext context, WebSocketSessionManager sessions)
		{
			this._context = context;
			this._sessions = sessions;
			this._websocket = context.WebSocket;
			this._websocket.CustomHandshakeRequestChecker = new Func<WebSocketContext, string>(this.checkHandshakeRequest);
			this._websocket.EmitOnPing = this._emitOnPing;
			this._websocket.IgnoreExtensions = this._ignoreExtensions;
			this._websocket.Protocol = this._protocol;
			TimeSpan waitTime = sessions.WaitTime;
			if (waitTime != this._websocket.WaitTime)
			{
				this._websocket.WaitTime = waitTime;
			}
			this._websocket.OnOpen += this.onOpen;
			this._websocket.OnMessage += this.onMessage;
			this._websocket.OnError += this.onError;
			this._websocket.OnClose += this.onClose;
			this._websocket.InternalAccept();
		}

		protected void Close()
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.Close();
		}

		protected void Close(ushort code, string reason)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.Close(code, reason);
		}

		protected void Close(CloseStatusCode code, string reason)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.Close(code, reason);
		}

		protected void CloseAsync()
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync();
		}

		protected void CloseAsync(ushort code, string reason)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync(code, reason);
		}

		protected void CloseAsync(CloseStatusCode code, string reason)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync(code, reason);
		}

		protected virtual void OnClose(CloseEventArgs e)
		{
		}

		protected virtual void OnError(ErrorEventArgs e)
		{
		}

		protected virtual void OnMessage(MessageEventArgs e)
		{
		}

		protected virtual void OnOpen()
		{
		}

		protected bool Ping()
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message);
			}
			return this._websocket.Ping();
		}

		protected bool Ping(string message)
		{
			if (this._websocket == null)
			{
				string message2 = WebSocketBehavior.getString_0(107158582);
				throw new InvalidOperationException(message2);
			}
			return this._websocket.Ping(message);
		}

		protected void Send(byte[] data)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(data);
		}

		protected void Send(FileInfo fileInfo)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(fileInfo);
		}

		protected void Send(string data)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(data);
		}

		protected void Send(Stream stream, int length)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(stream, length);
		}

		protected void SendAsync(byte[] data, Action<bool> completed)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(data, completed);
		}

		protected void SendAsync(FileInfo fileInfo, Action<bool> completed)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(fileInfo, completed);
		}

		protected void SendAsync(string data, Action<bool> completed)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(data, completed);
		}

		protected void SendAsync(Stream stream, int length, Action<bool> completed)
		{
			if (this._websocket == null)
			{
				string message = WebSocketBehavior.getString_0(107136252);
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(stream, length, completed);
		}

		static WebSocketBehavior()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocketBehavior));
		}

		private WebSocketContext _context;

		private Func<CookieCollection, CookieCollection, bool> _cookiesValidator;

		private bool _emitOnPing;

		private string _id;

		private bool _ignoreExtensions;

		private Func<string, bool> _originValidator;

		private string _protocol;

		private WebSocketSessionManager _sessions;

		private DateTime _startTime;

		private WebSocket _websocket;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
