using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class HttpConnection
	{
		static HttpConnection()
		{
			Strings.CreateGetStringDelegate(typeof(HttpConnection));
			HttpConnection._bufferLength = 8192;
			HttpConnection._maxInputLength = 32768;
		}

		internal HttpConnection(Socket socket, EndPointListener listener)
		{
			this._socket = socket;
			this._listener = listener;
			NetworkStream networkStream = new NetworkStream(socket, false);
			if (listener.IsSecure)
			{
				ServerSslConfiguration sslConfiguration = listener.SslConfiguration;
				SslStream sslStream = new SslStream(networkStream, false, sslConfiguration.ClientCertificateValidationCallback);
				sslStream.AuthenticateAsServer(sslConfiguration.ServerCertificate, sslConfiguration.ClientCertificateRequired, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
				this._secure = true;
				this._stream = sslStream;
			}
			else
			{
				this._stream = networkStream;
			}
			this._buffer = new byte[HttpConnection._bufferLength];
			this._localEndPoint = socket.LocalEndPoint;
			this._remoteEndPoint = socket.RemoteEndPoint;
			this._sync = new object();
			this._timeoutCanceled = new Dictionary<int, bool>();
			this._timer = new Timer(new TimerCallback(HttpConnection.onTimeout), this, -1, -1);
			this.init(90000);
		}

		public bool IsClosed
		{
			get
			{
				return this._socket == null;
			}
		}

		public bool IsLocal
		{
			get
			{
				return ((IPEndPoint)this._remoteEndPoint).Address.IsLocal();
			}
		}

		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		public IPEndPoint LocalEndPoint
		{
			get
			{
				return (IPEndPoint)this._localEndPoint;
			}
		}

		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return (IPEndPoint)this._remoteEndPoint;
			}
		}

		public int Reuses
		{
			get
			{
				return this._reuses;
			}
		}

		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		private void close()
		{
			object sync = this._sync;
			lock (sync)
			{
				if (this._socket == null)
				{
					return;
				}
				this.disposeTimer();
				this.disposeRequestBuffer();
				this.disposeStream();
				this.closeSocket();
			}
			this._context.Unregister();
			this._listener.RemoveConnection(this);
		}

		private void closeSocket()
		{
			try
			{
				this._socket.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			this._socket.Close();
			this._socket = null;
		}

		private void disposeRequestBuffer()
		{
			if (this._requestBuffer != null)
			{
				this._requestBuffer.Dispose();
				this._requestBuffer = null;
			}
		}

		private void disposeStream()
		{
			if (this._stream != null)
			{
				this._stream.Dispose();
				this._stream = null;
			}
		}

		private void disposeTimer()
		{
			if (this._timer != null)
			{
				try
				{
					this._timer.Change(-1, -1);
				}
				catch
				{
				}
				this._timer.Dispose();
				this._timer = null;
			}
		}

		private void init(int timeout)
		{
			this._timeout = timeout;
			this._context = new HttpListenerContext(this);
			this._currentLine = new StringBuilder(64);
			this._inputState = InputState.RequestLine;
			this._inputStream = null;
			this._lineState = LineState.None;
			this._outputStream = null;
			this._position = 0;
			this._requestBuffer = new MemoryStream();
		}

		private static void onRead(IAsyncResult asyncResult)
		{
			HttpConnection httpConnection = (HttpConnection)asyncResult.AsyncState;
			int attempts = httpConnection._attempts;
			if (httpConnection._socket != null)
			{
				object sync = httpConnection._sync;
				lock (sync)
				{
					if (httpConnection._socket != null)
					{
						httpConnection._timer.Change(-1, -1);
						httpConnection._timeoutCanceled[attempts] = true;
						int num = 0;
						try
						{
							num = httpConnection._stream.EndRead(asyncResult);
						}
						catch (Exception)
						{
							httpConnection.close();
							return;
						}
						if (num <= 0)
						{
							httpConnection.close();
						}
						else
						{
							httpConnection._requestBuffer.Write(httpConnection._buffer, 0, num);
							int length = (int)httpConnection._requestBuffer.Length;
							if (httpConnection.processInput(httpConnection._requestBuffer.GetBuffer(), length))
							{
								if (!httpConnection._context.HasErrorMessage)
								{
									httpConnection._context.Request.FinishInitialization();
								}
								if (httpConnection._context.HasErrorMessage)
								{
									httpConnection._context.SendError();
								}
								else
								{
									Uri url = httpConnection._context.Request.Url;
									HttpListener httpListener;
									if (httpConnection._listener.TrySearchHttpListener(url, out httpListener))
									{
										if (httpListener.AuthenticateContext(httpConnection._context))
										{
											if (!httpListener.RegisterContext(httpConnection._context))
											{
												httpConnection._context.ErrorStatusCode = 503;
												httpConnection._context.SendError();
											}
										}
									}
									else
									{
										httpConnection._context.ErrorStatusCode = 404;
										httpConnection._context.SendError();
									}
								}
							}
							else
							{
								httpConnection.BeginReadRequest();
							}
						}
					}
				}
			}
		}

		private static void onTimeout(object state)
		{
			HttpConnection httpConnection = (HttpConnection)state;
			int attempts = httpConnection._attempts;
			if (httpConnection._socket != null)
			{
				object sync = httpConnection._sync;
				lock (sync)
				{
					if (httpConnection._socket != null)
					{
						if (!httpConnection._timeoutCanceled[attempts])
						{
							httpConnection._context.ErrorStatusCode = 408;
							httpConnection._context.SendError();
						}
					}
				}
			}
		}

		private bool processInput(byte[] data, int length)
		{
			try
			{
				for (;;)
				{
					int num;
					string text = this.readLineFrom(data, this._position, length, out num);
					this._position += num;
					if (text == null)
					{
						break;
					}
					if (text.Length == 0)
					{
						if (this._inputState != InputState.RequestLine)
						{
							goto IL_86;
						}
					}
					else
					{
						if (this._inputState == InputState.RequestLine)
						{
							this._context.Request.SetRequestLine(text);
							this._inputState = InputState.Headers;
						}
						else
						{
							this._context.Request.AddHeader(text);
						}
						if (this._context.HasErrorMessage)
						{
							goto IL_B3;
						}
					}
				}
				goto IL_CD;
				IL_86:
				if (this._position > HttpConnection._maxInputLength)
				{
					this._context.ErrorMessage = HttpConnection.getString_0(107131383);
				}
				return true;
				IL_B3:
				return true;
			}
			catch (Exception ex)
			{
				this._context.ErrorMessage = ex.Message;
				return true;
			}
			IL_CD:
			bool result;
			if (this._position >= HttpConnection._maxInputLength)
			{
				this._context.ErrorMessage = HttpConnection.getString_0(107131383);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		private string readLineFrom(byte[] buffer, int offset, int length, out int nread)
		{
			nread = 0;
			for (int i = offset; i < length; i++)
			{
				nread++;
				byte b = buffer[i];
				if (b == 13)
				{
					this._lineState = LineState.Cr;
				}
				else
				{
					if (b == 10)
					{
						this._lineState = LineState.Lf;
						IL_4B:
						string result;
						if (this._lineState != LineState.Lf)
						{
							result = null;
						}
						else
						{
							string text = this._currentLine.ToString();
							this._currentLine.Length = 0;
							this._lineState = LineState.None;
							result = text;
						}
						return result;
					}
					this._currentLine.Append((char)b);
				}
			}
			goto IL_4B;
		}

		internal void BeginReadRequest()
		{
			this._attempts++;
			this._timeoutCanceled.Add(this._attempts, false);
			this._timer.Change(this._timeout, -1);
			try
			{
				this._stream.BeginRead(this._buffer, 0, HttpConnection._bufferLength, new AsyncCallback(HttpConnection.onRead), this);
			}
			catch (Exception)
			{
				this.close();
			}
		}

		internal void Close(bool force)
		{
			if (this._socket != null)
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._socket != null)
					{
						if (force)
						{
							if (this._outputStream != null)
							{
								this._outputStream.Close(true);
							}
							this.close();
						}
						else
						{
							this.GetResponseStream().Close(false);
							if (this._context.Response.CloseConnection)
							{
								this.close();
							}
							else if (!this._context.Request.FlushInput())
							{
								this.close();
							}
							else
							{
								this.disposeRequestBuffer();
								this._context.Unregister();
								this._reuses++;
								this.init(15000);
								this.BeginReadRequest();
							}
						}
					}
				}
			}
		}

		public void Close()
		{
			this.Close(false);
		}

		public RequestStream GetRequestStream(long contentLength, bool chunked)
		{
			object sync = this._sync;
			RequestStream result;
			lock (sync)
			{
				if (this._socket == null)
				{
					result = null;
				}
				else if (this._inputStream != null)
				{
					result = this._inputStream;
				}
				else
				{
					byte[] buffer = this._requestBuffer.GetBuffer();
					int num = (int)this._requestBuffer.Length;
					int count = num - this._position;
					this.disposeRequestBuffer();
					this._inputStream = (chunked ? new ChunkedRequestStream(this._stream, buffer, this._position, count, this._context) : new RequestStream(this._stream, buffer, this._position, count, contentLength));
					result = this._inputStream;
				}
			}
			return result;
		}

		public ResponseStream GetResponseStream()
		{
			object sync = this._sync;
			ResponseStream result;
			lock (sync)
			{
				if (this._socket == null)
				{
					result = null;
				}
				else if (this._outputStream != null)
				{
					result = this._outputStream;
				}
				else
				{
					HttpListener listener = this._context.Listener;
					bool ignoreWriteExceptions = listener == null || listener.IgnoreWriteExceptions;
					this._outputStream = new ResponseStream(this._stream, this._context.Response, ignoreWriteExceptions);
					result = this._outputStream;
				}
			}
			return result;
		}

		private int _attempts;

		private byte[] _buffer;

		private static readonly int _bufferLength;

		private HttpListenerContext _context;

		private StringBuilder _currentLine;

		private InputState _inputState;

		private RequestStream _inputStream;

		private LineState _lineState;

		private EndPointListener _listener;

		private EndPoint _localEndPoint;

		private static readonly int _maxInputLength;

		private ResponseStream _outputStream;

		private int _position;

		private EndPoint _remoteEndPoint;

		private MemoryStream _requestBuffer;

		private int _reuses;

		private bool _secure;

		private Socket _socket;

		private Stream _stream;

		private object _sync;

		private int _timeout;

		private Dictionary<int, bool> _timeoutCanceled;

		private Timer _timer;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
