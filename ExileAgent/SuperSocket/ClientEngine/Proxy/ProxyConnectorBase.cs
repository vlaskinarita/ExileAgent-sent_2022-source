using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SuperSocket.ClientEngine.Proxy
{
	public abstract class ProxyConnectorBase : IProxyConnector
	{
		public EndPoint ProxyEndPoint { get; private set; }

		public string TargetHostHame { get; private set; }

		public ProxyConnectorBase(EndPoint proxyEndPoint) : this(proxyEndPoint, null)
		{
		}

		public ProxyConnectorBase(EndPoint proxyEndPoint, string targetHostHame)
		{
			this.ProxyEndPoint = proxyEndPoint;
			this.TargetHostHame = targetHostHame;
		}

		public abstract void Connect(EndPoint remoteEndPoint);

		public event EventHandler<ProxyEventArgs> Completed
		{
			add
			{
				this.m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Combine(this.m_Completed, value);
			}
			remove
			{
				this.m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Remove(this.m_Completed, value);
			}
		}

		protected void OnCompleted(ProxyEventArgs args)
		{
			if (this.m_Completed == null)
			{
				return;
			}
			this.m_Completed(this, args);
		}

		protected void OnException(Exception exception)
		{
			this.OnCompleted(new ProxyEventArgs(exception));
		}

		protected void OnException(string exception)
		{
			this.OnCompleted(new ProxyEventArgs(new Exception(exception)));
		}

		protected bool ValidateAsyncResult(SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)e.SocketError);
				this.OnCompleted(new ProxyEventArgs(new Exception(ex.Message, ex)));
				return false;
			}
			return true;
		}

		protected void AsyncEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.LastOperation == SocketAsyncOperation.Send)
			{
				this.ProcessSend(e);
				return;
			}
			this.ProcessReceive(e);
		}

		protected void StartSend(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.SendAsync(e);
			}
			catch (Exception ex)
			{
				this.OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				this.ProcessSend(e);
			}
		}

		protected virtual void StartReceive(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.ReceiveAsync(e);
			}
			catch (Exception ex)
			{
				this.OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				this.ProcessReceive(e);
			}
		}

		protected abstract void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception);

		protected abstract void ProcessSend(SocketAsyncEventArgs e);

		protected abstract void ProcessReceive(SocketAsyncEventArgs e);

		protected static Encoding ASCIIEncoding = new ASCIIEncoding();

		private EventHandler<ProxyEventArgs> m_Completed;
	}
}
