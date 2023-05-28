using System;
using System.Net;
using System.Net.Sockets;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine.Proxy
{
	public class Socks4Connector : ProxyConnectorBase
	{
		public string UserID { get; private set; }

		public Socks4Connector(EndPoint proxyEndPoint, string userID) : base(proxyEndPoint)
		{
			this.UserID = userID;
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			IPEndPoint ipendPoint = remoteEndPoint as IPEndPoint;
			if (ipendPoint == null)
			{
				base.OnCompleted(new ProxyEventArgs(new Exception(Socks4Connector.getString_0(107308370))));
				return;
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), ipendPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception(Socks4Connector.getString_0(107309687), innerException));
			}
		}

		protected virtual byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			IPEndPoint ipendPoint = targetEndPoint as IPEndPoint;
			byte[] addressBytes = ipendPoint.Address.GetAddressBytes();
			byte[] array = new byte[Math.Max(8, (string.IsNullOrEmpty(this.UserID) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(this.UserID.Length)) + 5 + addressBytes.Length)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(ipendPoint.Port / 256);
			array[3] = (byte)(ipendPoint.Port % 256);
			Buffer.BlockCopy(addressBytes, 0, array, 4, addressBytes.Length);
			actualLength = 4 + addressBytes.Length;
			if (!string.IsNullOrEmpty(this.UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(this.UserID, 0, this.UserID.Length, array, actualLength);
			}
			byte[] array2 = array;
			int num = actualLength;
			actualLength = num + 1;
			array2[num] = 0;
			return array;
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
			int count;
			byte[] sendingBuffer = this.GetSendingBuffer((EndPoint)targetEndPoint, out count);
			e.SetBuffer(sendingBuffer, 0, count);
			e.UserToken = socket;
			e.Completed += base.AsyncEventArgsCompleted;
			base.StartSend(socket, e);
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			e.SetBuffer(0, 8);
			this.StartReceive((Socket)e.UserToken, e);
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			int num = e.Offset + e.BytesTransferred;
			if (num < 8)
			{
				e.SetBuffer(num, 8 - num);
				this.StartReceive((Socket)e.UserToken, e);
				return;
			}
			if (num != 8)
			{
				base.OnException(Socks4Connector.getString_0(107308301));
				return;
			}
			byte b = e.Buffer[1];
			if (b == 90)
			{
				base.OnCompleted(new ProxyEventArgs((Socket)e.UserToken));
				return;
			}
			this.HandleFaultStatus(b);
		}

		protected virtual void HandleFaultStatus(byte status)
		{
			string exception = string.Empty;
			switch (status)
			{
			case 91:
				exception = Socks4Connector.getString_0(107308694);
				break;
			case 92:
				exception = Socks4Connector.getString_0(107309169);
				break;
			case 93:
				exception = Socks4Connector.getString_0(107309084);
				break;
			default:
				exception = Socks4Connector.getString_0(107308963);
				break;
			}
			base.OnException(exception);
		}

		static Socks4Connector()
		{
			Strings.CreateGetStringDelegate(typeof(Socks4Connector));
		}

		private const int m_ValidResponseSize = 8;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
