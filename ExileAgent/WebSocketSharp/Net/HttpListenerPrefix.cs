using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class HttpListenerPrefix
	{
		internal HttpListenerPrefix(string uriPrefix, HttpListener listener)
		{
			this._original = uriPrefix;
			this._listener = listener;
			this.parse(uriPrefix);
		}

		public string Host
		{
			get
			{
				return this._host;
			}
		}

		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		public HttpListener Listener
		{
			get
			{
				return this._listener;
			}
		}

		public string Original
		{
			get
			{
				return this._original;
			}
		}

		public string Path
		{
			get
			{
				return this._path;
			}
		}

		public string Port
		{
			get
			{
				return this._port;
			}
		}

		private void parse(string uriPrefix)
		{
			if (uriPrefix.StartsWith(HttpListenerPrefix.getString_0(107363870)))
			{
				this._secure = true;
			}
			int length = uriPrefix.Length;
			int num = uriPrefix.IndexOf(':') + 3;
			int num2 = uriPrefix.IndexOf('/', num + 1, length - num - 1);
			int num3 = uriPrefix.LastIndexOf(':', num2 - 1, num2 - num - 1);
			if (uriPrefix[num2 - 1] != ']' && num3 > num)
			{
				this._host = uriPrefix.Substring(num, num3 - num);
				this._port = uriPrefix.Substring(num3 + 1, num2 - num3 - 1);
			}
			else
			{
				this._host = uriPrefix.Substring(num, num2 - num);
				this._port = (this._secure ? HttpListenerPrefix.getString_0(107160724) : HttpListenerPrefix.getString_0(107160729));
			}
			this._path = uriPrefix.Substring(num2);
			this._prefix = string.Format(HttpListenerPrefix.getString_0(107142117), new object[]
			{
				this._secure ? HttpListenerPrefix.getString_0(107363870) : HttpListenerPrefix.getString_0(107140557),
				this._host,
				this._port,
				this._path
			});
		}

		public static void CheckPrefix(string uriPrefix)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException(HttpListenerPrefix.getString_0(107131590));
			}
			int length = uriPrefix.Length;
			if (length == 0)
			{
				string message = HttpListenerPrefix.getString_0(107140410);
				throw new ArgumentException(message, HttpListenerPrefix.getString_0(107131590));
			}
			if (!uriPrefix.StartsWith(HttpListenerPrefix.getString_0(107160751)) && !uriPrefix.StartsWith(HttpListenerPrefix.getString_0(107160706)))
			{
				string message2 = HttpListenerPrefix.getString_0(107160693);
				throw new ArgumentException(message2, HttpListenerPrefix.getString_0(107131590));
			}
			int num = length - 1;
			if (uriPrefix[num] != '/')
			{
				string message3 = HttpListenerPrefix.getString_0(107160676);
				throw new ArgumentException(message3, HttpListenerPrefix.getString_0(107131590));
			}
			int num2 = uriPrefix.IndexOf(':') + 3;
			if (num2 >= num)
			{
				string message4 = HttpListenerPrefix.getString_0(107160647);
				throw new ArgumentException(message4, HttpListenerPrefix.getString_0(107131590));
			}
			if (uriPrefix[num2] == ':')
			{
				string message5 = HttpListenerPrefix.getString_0(107160647);
				throw new ArgumentException(message5, HttpListenerPrefix.getString_0(107131590));
			}
			int num3 = uriPrefix.IndexOf('/', num2, length - num2);
			if (num3 == num2)
			{
				string message6 = HttpListenerPrefix.getString_0(107160647);
				throw new ArgumentException(message6, HttpListenerPrefix.getString_0(107131590));
			}
			if (uriPrefix[num3 - 1] == ':')
			{
				string message7 = HttpListenerPrefix.getString_0(107160618);
				throw new ArgumentException(message7, HttpListenerPrefix.getString_0(107131590));
			}
			if (num3 == num - 1)
			{
				string message8 = HttpListenerPrefix.getString_0(107160589);
				throw new ArgumentException(message8, HttpListenerPrefix.getString_0(107131590));
			}
		}

		public override bool Equals(object obj)
		{
			HttpListenerPrefix httpListenerPrefix = obj as HttpListenerPrefix;
			return httpListenerPrefix != null && this._prefix.Equals(httpListenerPrefix._prefix);
		}

		public override int GetHashCode()
		{
			return this._prefix.GetHashCode();
		}

		public override string ToString()
		{
			return this._prefix;
		}

		static HttpListenerPrefix()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerPrefix));
		}

		private string _host;

		private HttpListener _listener;

		private string _original;

		private string _path;

		private string _port;

		private string _prefix;

		private bool _secure;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
