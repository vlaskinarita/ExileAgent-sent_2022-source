using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Principal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net.WebSockets
{
	public sealed class HttpListenerWebSocketContext : WebSocketContext
	{
		internal HttpListenerWebSocketContext(HttpListenerContext context, string protocol)
		{
			this._context = context;
			this._websocket = new WebSocket(this, protocol);
		}

		internal Logger Log
		{
			get
			{
				return this._context.Listener.Log;
			}
		}

		internal Stream Stream
		{
			get
			{
				return this._context.Connection.Stream;
			}
		}

		public override CookieCollection CookieCollection
		{
			get
			{
				return this._context.Request.Cookies;
			}
		}

		public override NameValueCollection Headers
		{
			get
			{
				return this._context.Request.Headers;
			}
		}

		public override string Host
		{
			get
			{
				return this._context.Request.UserHostName;
			}
		}

		public override bool IsAuthenticated
		{
			get
			{
				return this._context.Request.IsAuthenticated;
			}
		}

		public override bool IsLocal
		{
			get
			{
				return this._context.Request.IsLocal;
			}
		}

		public override bool IsSecureConnection
		{
			get
			{
				return this._context.Request.IsSecureConnection;
			}
		}

		public override bool IsWebSocketRequest
		{
			get
			{
				return this._context.Request.IsWebSocketRequest;
			}
		}

		public override string Origin
		{
			get
			{
				return this._context.Request.Headers[HttpListenerWebSocketContext.getString_0(107143422)];
			}
		}

		public override NameValueCollection QueryString
		{
			get
			{
				return this._context.Request.QueryString;
			}
		}

		public override Uri RequestUri
		{
			get
			{
				return this._context.Request.Url;
			}
		}

		public override string SecWebSocketKey
		{
			get
			{
				return this._context.Request.Headers[HttpListenerWebSocketContext.getString_0(107140358)];
			}
		}

		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				HttpListenerWebSocketContext.<get_SecWebSocketProtocols>d__30 <get_SecWebSocketProtocols>d__ = new HttpListenerWebSocketContext.<get_SecWebSocketProtocols>d__30(-2);
				<get_SecWebSocketProtocols>d__.<>4__this = this;
				return <get_SecWebSocketProtocols>d__;
			}
		}

		public override string SecWebSocketVersion
		{
			get
			{
				return this._context.Request.Headers[HttpListenerWebSocketContext.getString_0(107143445)];
			}
		}

		public override IPEndPoint ServerEndPoint
		{
			get
			{
				return this._context.Request.LocalEndPoint;
			}
		}

		public override IPrincipal User
		{
			get
			{
				return this._context.User;
			}
		}

		public override IPEndPoint UserEndPoint
		{
			get
			{
				return this._context.Request.RemoteEndPoint;
			}
		}

		public override WebSocket WebSocket
		{
			get
			{
				return this._websocket;
			}
		}

		internal void Close()
		{
			this._context.Connection.Close(true);
		}

		internal void Close(HttpStatusCode code)
		{
			this._context.Response.StatusCode = (int)code;
			this._context.Response.Close();
		}

		public override string ToString()
		{
			return this._context.Request.ToString();
		}

		static HttpListenerWebSocketContext()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerWebSocketContext));
		}

		private HttpListenerContext _context;

		private WebSocket _websocket;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
