using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine.Proxy
{
	public sealed class Socks5Connector : ProxyConnectorBase
	{
		public Socks5Connector(EndPoint proxyEndPoint) : base(proxyEndPoint)
		{
		}

		public Socks5Connector(EndPoint proxyEndPoint, string username, string password) : base(proxyEndPoint)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException(Socks5Connector.getString_0(107471207));
			}
			byte[] array = new byte[3 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(username.Length) + (string.IsNullOrEmpty(password) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(password.Length))];
			array[0] = 5;
			int bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(username, 0, username.Length, array, 2);
			if (bytes > 255)
			{
				throw new ArgumentException(Socks5Connector.getString_0(107308253), Socks5Connector.getString_0(107471207));
			}
			array[1] = (byte)bytes;
			int num = bytes + 2;
			if (!string.IsNullOrEmpty(password))
			{
				bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(password, 0, password.Length, array, num + 1);
				if (bytes > 255)
				{
					throw new ArgumentException(Socks5Connector.getString_0(107308196), Socks5Connector.getString_0(107308651));
				}
				array[num] = (byte)bytes;
				num += bytes + 1;
			}
			else
			{
				array[num] = 0;
				num++;
			}
			this.m_UserNameAuthenRequest = new ArraySegment<byte>(array, 0, num);
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException(Socks5Connector.getString_0(107310089));
			}
			if (!(remoteEndPoint is IPEndPoint) && !(remoteEndPoint is DnsEndPoint))
			{
				throw new ArgumentException(Socks5Connector.getString_0(107309277), Socks5Connector.getString_0(107310089));
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), remoteEndPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception(Socks5Connector.getString_0(107309692), innerException));
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
			e.UserToken = new Socks5Connector.SocksContext
			{
				TargetEndPoint = (EndPoint)targetEndPoint,
				Socket = socket,
				State = Socks5Connector.SocksState.NotAuthenticated
			};
			e.Completed += base.AsyncEventArgsCompleted;
			e.SetBuffer(Socks5Connector.m_AuthenHandshake, 0, Socks5Connector.m_AuthenHandshake.Length);
			base.StartSend(socket, e);
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			Socks5Connector.SocksContext socksContext = e.UserToken as Socks5Connector.SocksContext;
			if (socksContext.State == Socks5Connector.SocksState.NotAuthenticated)
			{
				e.SetBuffer(0, 2);
				this.StartReceive(socksContext.Socket, e);
				return;
			}
			if (socksContext.State == Socks5Connector.SocksState.Authenticating)
			{
				e.SetBuffer(0, 2);
				this.StartReceive(socksContext.Socket, e);
				return;
			}
			e.SetBuffer(0, e.Buffer.Length);
			this.StartReceive(socksContext.Socket, e);
		}

		private bool ProcessAuthenticationResponse(Socket socket, SocketAsyncEventArgs e)
		{
			int num = e.BytesTransferred + e.Offset;
			if (num < 2)
			{
				e.SetBuffer(num, 2 - num);
				this.StartReceive(socket, e);
				return false;
			}
			if (num > 2)
			{
				base.OnException(Socks5Connector.getString_0(107308638));
				return false;
			}
			if (e.Buffer[0] != 5)
			{
				base.OnException(Socks5Connector.getString_0(107308605));
				return false;
			}
			return true;
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			Socks5Connector.SocksContext socksContext = (Socks5Connector.SocksContext)e.UserToken;
			if (socksContext.State == Socks5Connector.SocksState.NotAuthenticated)
			{
				if (!this.ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					return;
				}
				byte b = e.Buffer[1];
				if (b == 0)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticated;
					this.SendHandshake(e);
					return;
				}
				if (b == 2)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticating;
					this.AutheticateWithUserNamePassword(e);
					return;
				}
				if (b == 255)
				{
					base.OnException(Socks5Connector.getString_0(107308540));
					return;
				}
				base.OnException(Socks5Connector.getString_0(107308523));
				return;
			}
			else if (socksContext.State == Socks5Connector.SocksState.Authenticating)
			{
				if (!this.ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					return;
				}
				if (e.Buffer[1] == 0)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticated;
					this.SendHandshake(e);
					return;
				}
				base.OnException(Socks5Connector.getString_0(107308470));
				return;
			}
			else
			{
				byte[] array = new byte[e.BytesTransferred];
				Buffer.BlockCopy(e.Buffer, e.Offset, array, 0, e.BytesTransferred);
				socksContext.ReceivedData.AddRange(array);
				if (socksContext.ExpectedLength > socksContext.ReceivedData.Count)
				{
					this.StartReceive(socksContext.Socket, e);
					return;
				}
				if (socksContext.State != Socks5Connector.SocksState.FoundLength)
				{
					byte b2 = socksContext.ReceivedData[3];
					int num;
					if (b2 == 1)
					{
						num = 10;
					}
					else if (b2 == 3)
					{
						num = (int)(7 + socksContext.ReceivedData[4]);
					}
					else
					{
						num = 22;
					}
					if (socksContext.ReceivedData.Count < num)
					{
						socksContext.ExpectedLength = num;
						this.StartReceive(socksContext.Socket, e);
						return;
					}
					if (socksContext.ReceivedData.Count > num)
					{
						base.OnException(Socks5Connector.getString_0(107308437));
						return;
					}
					this.OnGetFullResponse(socksContext);
					return;
				}
				else
				{
					if (socksContext.ReceivedData.Count > socksContext.ExpectedLength)
					{
						base.OnException(Socks5Connector.getString_0(107308437));
						return;
					}
					this.OnGetFullResponse(socksContext);
					return;
				}
			}
		}

		private void OnGetFullResponse(Socks5Connector.SocksContext context)
		{
			List<byte> receivedData = context.ReceivedData;
			if (receivedData[0] != 5)
			{
				base.OnException(Socks5Connector.getString_0(107308605));
				return;
			}
			byte b = receivedData[1];
			if (b == 0)
			{
				base.OnCompleted(new ProxyEventArgs(context.Socket));
				return;
			}
			string exception = string.Empty;
			switch (b)
			{
			case 2:
				exception = Socks5Connector.getString_0(107144052);
				break;
			case 3:
				exception = Socks5Connector.getString_0(107144039);
				break;
			case 4:
				exception = Socks5Connector.getString_0(107144010);
				break;
			case 5:
				exception = Socks5Connector.getString_0(107143953);
				break;
			case 6:
				exception = Socks5Connector.getString_0(107143900);
				break;
			case 7:
				exception = Socks5Connector.getString_0(107143915);
				break;
			case 8:
				exception = Socks5Connector.getString_0(107143830);
				break;
			default:
				exception = Socks5Connector.getString_0(107144305);
				break;
			}
			base.OnException(exception);
		}

		private void SendHandshake(SocketAsyncEventArgs e)
		{
			Socks5Connector.SocksContext socksContext = e.UserToken as Socks5Connector.SocksContext;
			EndPoint targetEndPoint = socksContext.TargetEndPoint;
			int port;
			byte[] array;
			int num;
			if (targetEndPoint is IPEndPoint)
			{
				IPEndPoint ipendPoint = targetEndPoint as IPEndPoint;
				port = ipendPoint.Port;
				if (ipendPoint.AddressFamily == AddressFamily.InterNetwork)
				{
					array = new byte[10];
					array[3] = 1;
					Buffer.BlockCopy(ipendPoint.Address.GetAddressBytes(), 0, array, 4, 4);
				}
				else
				{
					if (ipendPoint.AddressFamily != AddressFamily.InterNetworkV6)
					{
						base.OnException(Socks5Connector.getString_0(107144284));
						return;
					}
					array = new byte[22];
					array[3] = 4;
					Buffer.BlockCopy(ipendPoint.Address.GetAddressBytes(), 0, array, 4, 16);
				}
				num = array.Length;
			}
			else
			{
				DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
				port = dnsEndPoint.Port;
				array = new byte[7 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length)];
				array[3] = 3;
				num = 5 + ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, 5);
				num += 2;
			}
			array[0] = 5;
			array[1] = 1;
			array[2] = 0;
			array[num - 2] = (byte)(port / 256);
			array[num - 1] = (byte)(port % 256);
			e.SetBuffer(array, 0, num);
			socksContext.ReceivedData = new List<byte>(num + 5);
			socksContext.ExpectedLength = 5;
			base.StartSend(socksContext.Socket, e);
		}

		private void AutheticateWithUserNamePassword(SocketAsyncEventArgs e)
		{
			Socket socket = ((Socks5Connector.SocksContext)e.UserToken).Socket;
			e.SetBuffer(this.m_UserNameAuthenRequest.Array, this.m_UserNameAuthenRequest.Offset, this.m_UserNameAuthenRequest.Count);
			base.StartSend(socket, e);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Socks5Connector()
		{
			Strings.CreateGetStringDelegate(typeof(Socks5Connector));
			Socks5Connector.m_AuthenHandshake = new byte[]
			{
				5,
				2,
				0,
				2
			};
		}

		private ArraySegment<byte> m_UserNameAuthenRequest;

		private static byte[] m_AuthenHandshake;

		[NonSerialized]
		internal static GetString getString_0;

		private enum SocksState
		{
			NotAuthenticated,
			Authenticating,
			Authenticated,
			FoundLength,
			Connected
		}

		private sealed class SocksContext
		{
			public Socket Socket { get; set; }

			public Socks5Connector.SocksState State { get; set; }

			public EndPoint TargetEndPoint { get; set; }

			public List<byte> ReceivedData { get; set; }

			public int ExpectedLength { get; set; }
		}
	}
}
