using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine.Proxy
{
	public sealed class HttpConnectProxy : ProxyConnectorBase
	{
		static HttpConnectProxy()
		{
			Strings.CreateGetStringDelegate(typeof(HttpConnectProxy));
			HttpConnectProxy.m_LineSeparator = ProxyConnectorBase.ASCIIEncoding.GetBytes(HttpConnectProxy.getString_0(107309270));
		}

		public HttpConnectProxy(EndPoint proxyEndPoint) : this(proxyEndPoint, 128, null)
		{
		}

		public HttpConnectProxy(EndPoint proxyEndPoint, string targetHostName) : this(proxyEndPoint, 128, targetHostName)
		{
		}

		public HttpConnectProxy(EndPoint proxyEndPoint, int receiveBufferSize, string targetHostName) : base(proxyEndPoint, targetHostName)
		{
			this.m_ReceiveBufferSize = receiveBufferSize;
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException(HttpConnectProxy.getString_0(107310073));
			}
			if (!(remoteEndPoint is IPEndPoint) && !(remoteEndPoint is DnsEndPoint))
			{
				throw new ArgumentException(HttpConnectProxy.getString_0(107309261), HttpConnectProxy.getString_0(107310073));
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), remoteEndPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception(HttpConnectProxy.getString_0(107309676), innerException));
			}
		}

		protected override void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				base.OnException(exception);
				return;
			}
			if (e != null && !base.ValidateAsyncResult(e))
			{
				return;
			}
			if (socket == null)
			{
				base.OnException(new SocketException(10053));
				return;
			}
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
			}
			string s;
			if (targetEndPoint is DnsEndPoint)
			{
				DnsEndPoint dnsEndPoint = (DnsEndPoint)targetEndPoint;
				s = string.Format(HttpConnectProxy.getString_0(107309635), dnsEndPoint.Host, dnsEndPoint.Port);
			}
			else
			{
				IPEndPoint ipendPoint = (IPEndPoint)targetEndPoint;
				s = string.Format(HttpConnectProxy.getString_0(107309635), ipendPoint.Address, ipendPoint.Port);
			}
			byte[] bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(s);
			e.Completed += base.AsyncEventArgsCompleted;
			e.UserToken = new HttpConnectProxy.ConnectContext
			{
				Socket = socket,
				SearchState = new SearchMarkState<byte>(HttpConnectProxy.m_LineSeparator)
			};
			e.SetBuffer(bytes, 0, bytes.Length);
			base.StartSend(socket, e);
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			HttpConnectProxy.ConnectContext connectContext = (HttpConnectProxy.ConnectContext)e.UserToken;
			byte[] array = new byte[this.m_ReceiveBufferSize];
			e.SetBuffer(array, 0, array.Length);
			this.StartReceive(connectContext.Socket, e);
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			HttpConnectProxy.ConnectContext connectContext = (HttpConnectProxy.ConnectContext)e.UserToken;
			int matched = connectContext.SearchState.Matched;
			int num = e.Buffer.SearchMark(e.Offset, e.BytesTransferred, connectContext.SearchState);
			if (num < 0)
			{
				int num2 = e.Offset + e.BytesTransferred;
				if (num2 >= this.m_ReceiveBufferSize)
				{
					base.OnException(HttpConnectProxy.getString_0(107309534));
					return;
				}
				e.SetBuffer(num2, this.m_ReceiveBufferSize - num2);
				this.StartReceive(connectContext.Socket, e);
				return;
			}
			else
			{
				int num3 = (matched > 0) ? (e.Offset - matched) : (e.Offset + num);
				if (e.Offset + e.BytesTransferred > num3 + HttpConnectProxy.m_LineSeparator.Length)
				{
					base.OnException(HttpConnectProxy.getString_0(107309481));
					return;
				}
				string text = new StringReader(ProxyConnectorBase.ASCIIEncoding.GetString(e.Buffer, 0, num3)).ReadLine();
				if (string.IsNullOrEmpty(text))
				{
					base.OnException(HttpConnectProxy.getString_0(107308908));
					return;
				}
				int num4 = text.IndexOf(' ');
				if (num4 <= 0 || text.Length <= num4 + 2)
				{
					base.OnException(HttpConnectProxy.getString_0(107308908));
					return;
				}
				string value = text.Substring(0, num4);
				if (!HttpConnectProxy.getString_0(107308863).Equals(value))
				{
					base.OnException(HttpConnectProxy.getString_0(107308882));
					return;
				}
				int num5 = text.IndexOf(' ', num4 + 1);
				if (num5 < 0)
				{
					base.OnException(HttpConnectProxy.getString_0(107308908));
					return;
				}
				int num6;
				if (int.TryParse(text.Substring(num4 + 1, num5 - num4 - 1), out num6) && num6 <= 299 && num6 >= 200)
				{
					base.OnCompleted(new ProxyEventArgs(connectContext.Socket, base.TargetHostHame));
					return;
				}
				base.OnException(HttpConnectProxy.getString_0(107308805));
				return;
			}
		}

		private const string m_RequestTemplate = "CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n";

		private const string m_ResponsePrefix = "HTTP/1.1";

		private const char m_Space = ' ';

		private static byte[] m_LineSeparator;

		private int m_ReceiveBufferSize;

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class ConnectContext
		{
			public Socket Socket { get; set; }

			public SearchMarkState<byte> SearchState { get; set; }
		}
	}
}
