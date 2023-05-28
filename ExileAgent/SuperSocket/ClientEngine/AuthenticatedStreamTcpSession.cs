using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine
{
	public abstract class AuthenticatedStreamTcpSession : TcpClientSession
	{
		public AuthenticatedStreamTcpSession()
		{
		}

		public SecurityOption Security { get; set; }

		protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			base.ProcessConnect(sender as Socket, null, e, null);
		}

		protected abstract void StartAuthenticatedStream(Socket client);

		protected override void OnGetSocket(SocketAsyncEventArgs e)
		{
			try
			{
				this.StartAuthenticatedStream(base.Client);
			}
			catch (Exception e2)
			{
				if (!this.IsIgnorableException(e2))
				{
					this.OnError(e2);
				}
			}
		}

		protected void OnAuthenticatedStreamConnected(AuthenticatedStream stream)
		{
			this.m_Stream = stream;
			this.OnConnected();
			if (base.Buffer.Array == null)
			{
				int num = this.ReceiveBufferSize;
				if (num <= 0)
				{
					num = 4096;
				}
				this.ReceiveBufferSize = num;
				base.Buffer = new ArraySegment<byte>(new byte[num]);
			}
			this.BeginRead();
		}

		private void OnDataRead(IAsyncResult result)
		{
			AuthenticatedStreamTcpSession.StreamAsyncState streamAsyncState = result.AsyncState as AuthenticatedStreamTcpSession.StreamAsyncState;
			if (streamAsyncState != null && streamAsyncState.Stream != null)
			{
				AuthenticatedStream stream = streamAsyncState.Stream;
				int num = 0;
				try
				{
					num = stream.EndRead(result);
				}
				catch (Exception e)
				{
					if (!this.IsIgnorableException(e))
					{
						this.OnError(e);
					}
					if (base.EnsureSocketClosed(streamAsyncState.Client))
					{
						this.OnClosed();
					}
					return;
				}
				if (num == 0)
				{
					if (base.EnsureSocketClosed(streamAsyncState.Client))
					{
						this.OnClosed();
					}
					return;
				}
				this.OnDataReceived(base.Buffer.Array, base.Buffer.Offset, num);
				this.BeginRead();
				return;
			}
			this.OnError(new NullReferenceException(AuthenticatedStreamTcpSession.getString_1(107309847)));
		}

		private void BeginRead()
		{
			this.StartRead();
		}

		private void StartRead()
		{
			Socket client = base.Client;
			if (client != null && this.m_Stream != null)
			{
				try
				{
					ArraySegment<byte> buffer = base.Buffer;
					this.m_Stream.BeginRead(buffer.Array, buffer.Offset, buffer.Count, new AsyncCallback(this.OnDataRead), new AuthenticatedStreamTcpSession.StreamAsyncState
					{
						Stream = this.m_Stream,
						Client = client
					});
				}
				catch (Exception e)
				{
					if (!this.IsIgnorableException(e))
					{
						this.OnError(e);
					}
					if (base.EnsureSocketClosed(client))
					{
						this.OnClosed();
					}
				}
				return;
			}
		}

		protected override bool IsIgnorableException(Exception e)
		{
			if (base.IsIgnorableException(e))
			{
				return true;
			}
			if (e is IOException)
			{
				if (e.InnerException is ObjectDisposedException)
				{
					return true;
				}
				if (e.InnerException is IOException && e.InnerException.InnerException is ObjectDisposedException)
				{
					return true;
				}
			}
			return false;
		}

		protected override void SendInternal(PosList<ArraySegment<byte>> items)
		{
			Socket client = base.Client;
			try
			{
				ArraySegment<byte> arraySegment = items[items.Position];
				this.m_Stream.BeginWrite(arraySegment.Array, arraySegment.Offset, arraySegment.Count, new AsyncCallback(this.OnWriteComplete), new AuthenticatedStreamTcpSession.StreamAsyncState
				{
					Stream = this.m_Stream,
					Client = client,
					SendingItems = items
				});
			}
			catch (Exception e)
			{
				if (!this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				if (base.EnsureSocketClosed(client))
				{
					this.OnClosed();
				}
			}
		}

		private void OnWriteComplete(IAsyncResult result)
		{
			AuthenticatedStreamTcpSession.StreamAsyncState streamAsyncState = result.AsyncState as AuthenticatedStreamTcpSession.StreamAsyncState;
			if (streamAsyncState != null && streamAsyncState.Stream != null)
			{
				AuthenticatedStream stream = streamAsyncState.Stream;
				try
				{
					stream.EndWrite(result);
				}
				catch (Exception e)
				{
					if (!this.IsIgnorableException(e))
					{
						this.OnError(e);
					}
					if (base.EnsureSocketClosed(streamAsyncState.Client))
					{
						this.OnClosed();
					}
					return;
				}
				PosList<ArraySegment<byte>> sendingItems = streamAsyncState.SendingItems;
				int num = sendingItems.Position + 1;
				if (num < sendingItems.Count)
				{
					sendingItems.Position = num;
					this.SendInternal(sendingItems);
					return;
				}
				try
				{
					this.m_Stream.Flush();
				}
				catch (Exception e2)
				{
					if (!this.IsIgnorableException(e2))
					{
						this.OnError(e2);
					}
					if (base.EnsureSocketClosed(streamAsyncState.Client))
					{
						this.OnClosed();
					}
					return;
				}
				base.OnSendingCompleted();
				return;
			}
			this.OnError(new NullReferenceException(AuthenticatedStreamTcpSession.getString_1(107309850)));
		}

		public override void Close()
		{
			AuthenticatedStream stream = this.m_Stream;
			if (stream != null)
			{
				stream.Close();
				stream.Dispose();
				this.m_Stream = null;
			}
			base.Close();
		}

		static AuthenticatedStreamTcpSession()
		{
			Strings.CreateGetStringDelegate(typeof(AuthenticatedStreamTcpSession));
		}

		private AuthenticatedStream m_Stream;

		[NonSerialized]
		internal static GetString getString_1;

		private sealed class StreamAsyncState
		{
			public AuthenticatedStream Stream { get; set; }

			public Socket Client { get; set; }

			public PosList<ArraySegment<byte>> SendingItems { get; set; }
		}
	}
}
