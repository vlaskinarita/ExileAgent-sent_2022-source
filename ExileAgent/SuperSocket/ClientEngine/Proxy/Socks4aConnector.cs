using System;
using System.Net;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine.Proxy
{
	public sealed class Socks4aConnector : Socks4Connector
	{
		public Socks4aConnector(EndPoint proxyEndPoint, string userID) : base(proxyEndPoint, userID)
		{
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			if (dnsEndPoint == null)
			{
				base.OnCompleted(new ProxyEventArgs(new Exception(Socks4aConnector.getString_1(107308792))));
				return;
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), dnsEndPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception(Socks4aConnector.getString_1(107309684), innerException));
			}
		}

		protected override byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
			byte[] array = new byte[Math.Max(8, (string.IsNullOrEmpty(base.UserID) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(base.UserID.Length)) + 5 + 4 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length) + 1)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(dnsEndPoint.Port / 256);
			array[3] = (byte)(dnsEndPoint.Port % 256);
			array[4] = 0;
			array[5] = 0;
			array[6] = 0;
			array[7] = (byte)Socks4aConnector.m_Random.Next(1, 255);
			actualLength = 8;
			if (!string.IsNullOrEmpty(base.UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(base.UserID, 0, base.UserID.Length, array, actualLength);
			}
			byte[] array2 = array;
			int num = actualLength;
			actualLength = num + 1;
			array2[num] = 0;
			actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, actualLength);
			byte[] array3 = array;
			num = actualLength;
			actualLength = num + 1;
			array3[num] = 0;
			return array;
		}

		protected override void HandleFaultStatus(byte status)
		{
			string exception = string.Empty;
			switch (status)
			{
			case 91:
				exception = Socks4aConnector.getString_1(107308691);
				break;
			case 92:
				exception = Socks4aConnector.getString_1(107309166);
				break;
			case 93:
				exception = Socks4aConnector.getString_1(107309081);
				break;
			default:
				exception = Socks4aConnector.getString_1(107308960);
				break;
			}
			base.OnException(exception);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Socks4aConnector()
		{
			Strings.CreateGetStringDelegate(typeof(Socks4aConnector));
			Socks4aConnector.m_Random = new Random();
		}

		private static Random m_Random;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
