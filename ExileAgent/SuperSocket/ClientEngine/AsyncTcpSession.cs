using System;
using System.Net.Sockets;

namespace SuperSocket.ClientEngine
{
	public sealed class AsyncTcpSession : TcpClientSession
	{
		protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.LastOperation == SocketAsyncOperation.Connect)
			{
				base.ProcessConnect(sender as Socket, null, e, null);
				return;
			}
			this.ProcessReceive(e);
		}

		protected override void SetBuffer(ArraySegment<byte> bufferSegment)
		{
			base.SetBuffer(bufferSegment);
			if (this.m_SocketEventArgs != null)
			{
				this.m_SocketEventArgs.SetBuffer(bufferSegment.Array, bufferSegment.Offset, bufferSegment.Count);
			}
		}

		protected override void OnGetSocket(SocketAsyncEventArgs e)
		{
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
			e.SetBuffer(base.Buffer.Array, base.Buffer.Offset, base.Buffer.Count);
			this.m_SocketEventArgs = e;
			this.OnConnected();
			this.StartReceive();
		}

		private void BeginReceive()
		{
			if (!base.Client.ReceiveAsync(this.m_SocketEventArgs))
			{
				this.ProcessReceive(this.m_SocketEventArgs);
			}
		}

		private void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success)
			{
				if (base.EnsureSocketClosed())
				{
					this.OnClosed();
				}
				if (!base.IsIgnorableSocketError((int)e.SocketError))
				{
					this.OnError(new SocketException((int)e.SocketError));
				}
				return;
			}
			if (e.BytesTransferred == 0)
			{
				if (base.EnsureSocketClosed())
				{
					this.OnClosed();
				}
				return;
			}
			this.OnDataReceived(e.Buffer, e.Offset, e.BytesTransferred);
			this.StartReceive();
		}

		private void StartReceive()
		{
			Socket client = base.Client;
			if (client == null)
			{
				return;
			}
			bool flag;
			try
			{
				flag = client.ReceiveAsync(this.m_SocketEventArgs);
			}
			catch (SocketException ex)
			{
				int errorCode = ex.ErrorCode;
				if (!base.IsIgnorableSocketError(errorCode))
				{
					this.OnError(ex);
				}
				if (base.EnsureSocketClosed(client))
				{
					this.OnClosed();
				}
				return;
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
				return;
			}
			if (!flag)
			{
				this.ProcessReceive(this.m_SocketEventArgs);
			}
		}

		protected override void SendInternal(PosList<ArraySegment<byte>> items)
		{
			if (this.m_SocketEventArgsSend == null)
			{
				this.m_SocketEventArgsSend = new SocketAsyncEventArgs();
				this.m_SocketEventArgsSend.Completed += this.Sending_Completed;
			}
			bool flag;
			try
			{
				if (items.Count > 1)
				{
					if (this.m_SocketEventArgsSend.Buffer != null)
					{
						this.m_SocketEventArgsSend.SetBuffer(null, 0, 0);
					}
					this.m_SocketEventArgsSend.BufferList = items;
				}
				else
				{
					ArraySegment<byte> arraySegment = items[0];
					try
					{
						if (this.m_SocketEventArgsSend.BufferList != null)
						{
							this.m_SocketEventArgsSend.BufferList = null;
						}
					}
					catch
					{
					}
					this.m_SocketEventArgsSend.SetBuffer(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				flag = base.Client.SendAsync(this.m_SocketEventArgsSend);
			}
			catch (SocketException ex)
			{
				int errorCode = ex.ErrorCode;
				if (base.EnsureSocketClosed() && !base.IsIgnorableSocketError(errorCode))
				{
					this.OnError(ex);
				}
				return;
			}
			catch (Exception e)
			{
				if (base.EnsureSocketClosed() && this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				return;
			}
			if (!flag)
			{
				this.Sending_Completed(base.Client, this.m_SocketEventArgsSend);
			}
		}

		private void Sending_Completed(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success && e.BytesTransferred != 0)
			{
				base.OnSendingCompleted();
				return;
			}
			if (base.EnsureSocketClosed())
			{
				this.OnClosed();
			}
			if (e.SocketError != SocketError.Success && !base.IsIgnorableSocketError((int)e.SocketError))
			{
				this.OnError(new SocketException((int)e.SocketError));
			}
		}

		protected override void OnClosed()
		{
			if (this.m_SocketEventArgsSend != null)
			{
				this.m_SocketEventArgsSend.Dispose();
				this.m_SocketEventArgsSend = null;
			}
			if (this.m_SocketEventArgs != null)
			{
				this.m_SocketEventArgs.Dispose();
				this.m_SocketEventArgs = null;
			}
			base.OnClosed();
		}

		private SocketAsyncEventArgs m_SocketEventArgs;

		private SocketAsyncEventArgs m_SocketEventArgsSend;
	}
}
