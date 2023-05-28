using System;
using System.Net;
using System.Net.Sockets;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine
{
	public static class ConnectAsyncExtension
	{
		private static void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
		{
			e.Completed -= ConnectAsyncExtension.SocketAsyncEventCompleted;
			ConnectAsyncExtension.ConnectToken connectToken = (ConnectAsyncExtension.ConnectToken)e.UserToken;
			e.UserToken = null;
			connectToken.Callback(sender as Socket, connectToken.State, e, null);
		}

		private static SocketAsyncEventArgs CreateSocketAsyncEventArgs(EndPoint remoteEndPoint, ConnectedCallback callback, object state)
		{
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.UserToken = new ConnectAsyncExtension.ConnectToken
			{
				State = state,
				Callback = callback
			};
			socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
			socketAsyncEventArgs.Completed += ConnectAsyncExtension.SocketAsyncEventCompleted;
			return socketAsyncEventArgs;
		}

		internal static bool PreferIPv4Stack()
		{
			return Environment.GetEnvironmentVariable(ConnectAsyncExtension.getString_0(107309834)) != null;
		}

		public static void ConnectAsync(this EndPoint remoteEndPoint, EndPoint localEndPoint, ConnectedCallback callback, object state)
		{
			SocketAsyncEventArgs e = ConnectAsyncExtension.CreateSocketAsyncEventArgs(remoteEndPoint, callback, state);
			Socket socket = ConnectAsyncExtension.PreferIPv4Stack() ? new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) : new Socket(SocketType.Stream, ProtocolType.Tcp);
			if (localEndPoint != null)
			{
				try
				{
					socket.ExclusiveAddressUse = false;
					socket.Bind(localEndPoint);
				}
				catch (Exception exception)
				{
					callback(null, state, null, exception);
					return;
				}
			}
			socket.ConnectAsync(e);
		}

		static ConnectAsyncExtension()
		{
			Strings.CreateGetStringDelegate(typeof(ConnectAsyncExtension));
		}

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class ConnectToken
		{
			public object State { get; set; }

			public ConnectedCallback Callback { get; set; }
		}
	}
}
