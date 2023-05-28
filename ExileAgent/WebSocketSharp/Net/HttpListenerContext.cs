using System;
using System.Security.Principal;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerContext
	{
		internal HttpListenerContext(HttpConnection connection)
		{
			this._connection = connection;
			this._errorStatusCode = 400;
			this._request = new HttpListenerRequest(this);
			this._response = new HttpListenerResponse(this);
		}

		internal HttpConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		internal string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
			set
			{
				this._errorMessage = value;
			}
		}

		internal int ErrorStatusCode
		{
			get
			{
				return this._errorStatusCode;
			}
			set
			{
				this._errorStatusCode = value;
			}
		}

		internal bool HasErrorMessage
		{
			get
			{
				return this._errorMessage != null;
			}
		}

		internal HttpListener Listener
		{
			get
			{
				return this._listener;
			}
			set
			{
				this._listener = value;
			}
		}

		public HttpListenerRequest Request
		{
			get
			{
				return this._request;
			}
		}

		public HttpListenerResponse Response
		{
			get
			{
				return this._response;
			}
		}

		public IPrincipal User
		{
			get
			{
				return this._user;
			}
			internal set
			{
				this._user = value;
			}
		}

		private static string createErrorContent(int statusCode, string statusDescription, string message)
		{
			return (message == null || message.Length <= 0) ? string.Format(HttpListenerContext.getString_0(107131033), statusCode, statusDescription) : string.Format(HttpListenerContext.getString_0(107131008), statusCode, statusDescription, message);
		}

		internal HttpListenerWebSocketContext GetWebSocketContext(string protocol)
		{
			this._websocketContext = new HttpListenerWebSocketContext(this, protocol);
			return this._websocketContext;
		}

		internal void SendAuthenticationChallenge(AuthenticationSchemes scheme, string realm)
		{
			string value = new AuthenticationChallenge(scheme, realm).ToString();
			this._response.StatusCode = 401;
			this._response.Headers.InternalSet(HttpListenerContext.getString_0(107137445), value, true);
			this._response.Close();
		}

		internal void SendError()
		{
			try
			{
				this._response.StatusCode = this._errorStatusCode;
				this._response.ContentType = HttpListenerContext.getString_0(107245531);
				string s = HttpListenerContext.createErrorContent(this._errorStatusCode, this._response.StatusDescription, this._errorMessage);
				Encoding utf = Encoding.UTF8;
				byte[] bytes = utf.GetBytes(s);
				this._response.ContentEncoding = utf;
				this._response.ContentLength64 = (long)bytes.Length;
				this._response.Close(bytes, true);
			}
			catch
			{
				this._connection.Close(true);
			}
		}

		internal void Unregister()
		{
			if (this._listener != null)
			{
				this._listener.UnregisterContext(this);
			}
		}

		public HttpListenerWebSocketContext AcceptWebSocket(string protocol)
		{
			if (this._websocketContext != null)
			{
				string message = HttpListenerContext.getString_0(107130943);
				throw new InvalidOperationException(message);
			}
			if (protocol != null)
			{
				if (protocol.Length == 0)
				{
					string message2 = HttpListenerContext.getString_0(107140209);
					throw new ArgumentException(message2, HttpListenerContext.getString_0(107131402));
				}
				if (!protocol.IsToken())
				{
					string message3 = HttpListenerContext.getString_0(107135478);
					throw new ArgumentException(message3, HttpListenerContext.getString_0(107131402));
				}
			}
			return this.GetWebSocketContext(protocol);
		}

		static HttpListenerContext()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerContext));
		}

		private HttpConnection _connection;

		private string _errorMessage;

		private int _errorStatusCode;

		private HttpListener _listener;

		private HttpListenerRequest _request;

		private HttpListenerResponse _response;

		private IPrincipal _user;

		private HttpListenerWebSocketContext _websocketContext;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
