using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine
{
	public abstract class TcpClientSession : ClientSession
	{
		private protected string HostName { protected get; private set; }

		public TcpClientSession()
		{
		}

		public override EndPoint LocalEndPoint
		{
			get
			{
				return base.LocalEndPoint;
			}
			set
			{
				if (this.m_InConnecting || base.IsConnected)
				{
					throw new Exception(TcpClientSession.getString_0(107309664));
				}
				base.LocalEndPoint = value;
			}
		}

		public override int ReceiveBufferSize
		{
			get
			{
				return base.ReceiveBufferSize;
			}
			set
			{
				if (base.Buffer.Array != null)
				{
					throw new Exception(TcpClientSession.getString_0(107310127));
				}
				base.ReceiveBufferSize = value;
			}
		}

		protected virtual bool IsIgnorableException(Exception e)
		{
			return e is ObjectDisposedException || e is NullReferenceException;
		}

		protected bool IsIgnorableSocketError(int errorCode)
		{
			if (errorCode != 10058 && errorCode != 10053 && errorCode != 10054)
			{
				if (errorCode != 995)
				{
					return false;
				}
			}
			return true;
		}

		protected abstract void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e);

		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException(TcpClientSession.getString_0(107310034));
			}
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			if (dnsEndPoint != null)
			{
				string host = dnsEndPoint.Host;
				if (!string.IsNullOrEmpty(host))
				{
					this.HostName = host;
				}
			}
			if (this.m_InConnecting)
			{
				throw new Exception(TcpClientSession.getString_0(107309981));
			}
			if (base.Client != null)
			{
				throw new Exception(TcpClientSession.getString_0(107309916));
			}
			if (base.Proxy != null)
			{
				base.Proxy.Completed += this.Proxy_Completed;
				base.Proxy.Connect(remoteEndPoint);
				this.m_InConnecting = true;
				return;
			}
			this.m_InConnecting = true;
			remoteEndPoint.ConnectAsync(this.LocalEndPoint, new ConnectedCallback(this.ProcessConnect), null);
		}

		private void Proxy_Completed(object sender, ProxyEventArgs e)
		{
			base.Proxy.Completed -= this.Proxy_Completed;
			if (e.Connected)
			{
				SocketAsyncEventArgs socketAsyncEventArgs = null;
				if (e.TargetHostName != null)
				{
					socketAsyncEventArgs = new SocketAsyncEventArgs();
					socketAsyncEventArgs.RemoteEndPoint = new DnsEndPoint(e.TargetHostName, 0);
				}
				this.ProcessConnect(e.Socket, null, socketAsyncEventArgs, null);
				return;
			}
			this.OnError(new Exception(TcpClientSession.getString_0(107309335), e.Exception));
			this.m_InConnecting = false;
		}

		protected void ProcessConnect(Socket socket, object state, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				this.m_InConnecting = false;
				this.OnError(exception);
				if (e != null)
				{
					e.Dispose();
				}
				return;
			}
			if (e != null && e.SocketError != SocketError.Success)
			{
				this.m_InConnecting = false;
				this.OnError(new SocketException((int)e.SocketError));
				e.Dispose();
				return;
			}
			if (socket == null)
			{
				this.m_InConnecting = false;
				this.OnError(new SocketException(10053));
				return;
			}
			if (!socket.Connected)
			{
				this.m_InConnecting = false;
				SocketError errorCode = SocketError.HostUnreachable;
				try
				{
					errorCode = (SocketError)socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
				}
				catch (Exception)
				{
					errorCode = SocketError.HostUnreachable;
				}
				this.OnError(new SocketException((int)errorCode));
				return;
			}
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
			}
			e.Completed += this.SocketEventArgsCompleted;
			base.Client = socket;
			this.m_InConnecting = false;
			try
			{
				this.LocalEndPoint = socket.LocalEndPoint;
			}
			catch
			{
			}
			EndPoint endPoint = (e.RemoteEndPoint != null) ? e.RemoteEndPoint : socket.RemoteEndPoint;
			if (string.IsNullOrEmpty(this.HostName))
			{
				this.HostName = this.GetHostOfEndPoint(endPoint);
			}
			else
			{
				DnsEndPoint dnsEndPoint = endPoint as DnsEndPoint;
				if (dnsEndPoint != null)
				{
					string host = dnsEndPoint.Host;
					if (!string.IsNullOrEmpty(host) && !this.HostName.Equals(host, StringComparison.OrdinalIgnoreCase))
					{
						this.HostName = host;
					}
				}
			}
			try
			{
				base.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
			}
			catch
			{
			}
			this.OnGetSocket(e);
		}

		private string GetHostOfEndPoint(EndPoint endPoint)
		{
			DnsEndPoint dnsEndPoint = endPoint as DnsEndPoint;
			if (dnsEndPoint != null)
			{
				return dnsEndPoint.Host;
			}
			IPEndPoint ipendPoint = endPoint as IPEndPoint;
			if (ipendPoint != null && ipendPoint.Address != null)
			{
				return ipendPoint.Address.ToString();
			}
			return string.Empty;
		}

		protected abstract void OnGetSocket(SocketAsyncEventArgs e);

		protected bool EnsureSocketClosed()
		{
			return this.EnsureSocketClosed(null);
		}

		protected bool EnsureSocketClosed(Socket prevClient)
		{
			Socket socket = base.Client;
			if (socket == null)
			{
				return false;
			}
			bool result = true;
			if (prevClient != null && prevClient != socket)
			{
				socket = prevClient;
				result = false;
			}
			else
			{
				base.Client = null;
				this.m_IsSending = 0;
			}
			try
			{
				socket.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			finally
			{
				try
				{
					socket.Close();
				}
				catch
				{
				}
			}
			return result;
		}

		private bool DetectConnected()
		{
			if (base.Client != null)
			{
				return true;
			}
			this.OnError(new SocketException(10057));
			return false;
		}

		private IBatchQueue<ArraySegment<byte>> GetSendingQueue()
		{
			if (this.m_SendingQueue != null)
			{
				return this.m_SendingQueue;
			}
			IBatchQueue<ArraySegment<byte>> sendingQueue;
			lock (this)
			{
				if (this.m_SendingQueue != null)
				{
					sendingQueue = this.m_SendingQueue;
				}
				else
				{
					this.m_SendingQueue = new ConcurrentBatchQueue<ArraySegment<byte>>(Math.Max(base.SendingQueueSize, 1024), (ArraySegment<byte> t) => t.Array == null || t.Count == 0);
					sendingQueue = this.m_SendingQueue;
				}
			}
			return sendingQueue;
		}

		private PosList<ArraySegment<byte>> GetSendingItems()
		{
			if (this.m_SendingItems == null)
			{
				this.m_SendingItems = new PosList<ArraySegment<byte>>();
			}
			return this.m_SendingItems;
		}

		protected bool IsSending
		{
			get
			{
				return this.m_IsSending == 1;
			}
		}

		public override bool TrySend(ArraySegment<byte> segment)
		{
			if (segment.Array == null || segment.Count == 0)
			{
				throw new Exception(TcpClientSession.getString_0(107309350));
			}
			if (!this.DetectConnected())
			{
				return true;
			}
			bool result = this.GetSendingQueue().Enqueue(segment);
			if (Interlocked.CompareExchange(ref this.m_IsSending, 1, 0) != 0)
			{
				return result;
			}
			this.DequeueSend();
			return result;
		}

		public override bool TrySend(IList<ArraySegment<byte>> segments)
		{
			if (segments == null || segments.Count == 0)
			{
				throw new ArgumentNullException(TcpClientSession.getString_0(107309301));
			}
			for (int i = 0; i < segments.Count; i++)
			{
				if (segments[i].Count == 0)
				{
					throw new Exception(TcpClientSession.getString_0(107309288));
				}
			}
			if (!this.DetectConnected())
			{
				return true;
			}
			bool result = this.GetSendingQueue().Enqueue(segments);
			if (Interlocked.CompareExchange(ref this.m_IsSending, 1, 0) != 0)
			{
				return result;
			}
			this.DequeueSend();
			return result;
		}

		private void DequeueSend()
		{
			PosList<ArraySegment<byte>> sendingItems = this.GetSendingItems();
			if (!this.m_SendingQueue.TryDequeue(sendingItems))
			{
				this.m_IsSending = 0;
				return;
			}
			this.SendInternal(sendingItems);
		}

		protected abstract void SendInternal(PosList<ArraySegment<byte>> items);

		protected void OnSendingCompleted()
		{
			PosList<ArraySegment<byte>> sendingItems = this.GetSendingItems();
			sendingItems.Clear();
			sendingItems.Position = 0;
			if (!this.m_SendingQueue.TryDequeue(sendingItems))
			{
				this.m_IsSending = 0;
				return;
			}
			this.SendInternal(sendingItems);
		}

		public override void Close()
		{
			if (this.EnsureSocketClosed())
			{
				this.OnClosed();
			}
		}

		static TcpClientSession()
		{
			Strings.CreateGetStringDelegate(typeof(TcpClientSession));
		}

		private bool m_InConnecting;

		private IBatchQueue<ArraySegment<byte>> m_SendingQueue;

		private PosList<ArraySegment<byte>> m_SendingItems;

		private int m_IsSending;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
