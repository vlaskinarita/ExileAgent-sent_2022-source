using System;
using System.IO;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal class RequestStream : Stream
	{
		internal RequestStream(Stream stream, byte[] buffer, int offset, int count, long contentLength)
		{
			this._stream = stream;
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
			this._bodyLeft = contentLength;
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		private int fillFromBuffer(byte[] buffer, int offset, int count)
		{
			int result;
			if (this._bodyLeft == 0L)
			{
				result = -1;
			}
			else if (this._count == 0)
			{
				result = 0;
			}
			else
			{
				if (count > this._count)
				{
					count = this._count;
				}
				if (this._bodyLeft > 0L && this._bodyLeft < (long)count)
				{
					count = (int)this._bodyLeft;
				}
				Buffer.BlockCopy(this._buffer, this._offset, buffer, offset, count);
				this._offset += count;
				this._count -= count;
				if (this._bodyLeft > 0L)
				{
					this._bodyLeft -= (long)count;
				}
				result = count;
			}
			return result;
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this._disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException(RequestStream.getString_0(107350512));
			}
			if (offset < 0)
			{
				string message = RequestStream.getString_0(107130389);
				throw new ArgumentOutOfRangeException(RequestStream.getString_0(107145098), message);
			}
			if (count < 0)
			{
				string message2 = RequestStream.getString_0(107130389);
				throw new ArgumentOutOfRangeException(RequestStream.getString_0(107350462), message2);
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				string message3 = RequestStream.getString_0(107130364);
				throw new ArgumentException(message3);
			}
			IAsyncResult result;
			if (count == 0)
			{
				result = this._stream.BeginRead(buffer, offset, 0, callback, state);
			}
			else
			{
				int num2 = this.fillFromBuffer(buffer, offset, count);
				if (num2 != 0)
				{
					HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
					httpStreamAsyncResult.Buffer = buffer;
					httpStreamAsyncResult.Offset = offset;
					httpStreamAsyncResult.Count = count;
					httpStreamAsyncResult.SyncRead = ((num2 > 0) ? num2 : 0);
					httpStreamAsyncResult.Complete();
					result = httpStreamAsyncResult;
				}
				else
				{
					if (this._bodyLeft >= 0L && this._bodyLeft < (long)count)
					{
						count = (int)this._bodyLeft;
					}
					result = this._stream.BeginRead(buffer, offset, count, callback, state);
				}
			}
			return result;
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		public override void Close()
		{
			this._disposed = true;
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this._disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException(RequestStream.getString_0(107131736));
			}
			int result;
			if (asyncResult is HttpStreamAsyncResult)
			{
				HttpStreamAsyncResult httpStreamAsyncResult = (HttpStreamAsyncResult)asyncResult;
				if (!httpStreamAsyncResult.IsCompleted)
				{
					httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
				}
				result = httpStreamAsyncResult.SyncRead;
			}
			else
			{
				int num = this._stream.EndRead(asyncResult);
				if (num > 0 && this._bodyLeft > 0L)
				{
					this._bodyLeft -= (long)num;
				}
				result = num;
			}
			return result;
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException(RequestStream.getString_0(107350512));
			}
			if (offset < 0)
			{
				string message = RequestStream.getString_0(107130389);
				throw new ArgumentOutOfRangeException(RequestStream.getString_0(107145098), message);
			}
			if (count < 0)
			{
				string message2 = RequestStream.getString_0(107130389);
				throw new ArgumentOutOfRangeException(RequestStream.getString_0(107350462), message2);
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				string message3 = RequestStream.getString_0(107130364);
				throw new ArgumentException(message3);
			}
			int result;
			if (count == 0)
			{
				result = 0;
			}
			else
			{
				int num2 = this.fillFromBuffer(buffer, offset, count);
				if (num2 == -1)
				{
					result = 0;
				}
				else if (num2 > 0)
				{
					result = num2;
				}
				else
				{
					num2 = this._stream.Read(buffer, offset, count);
					if (num2 > 0 && this._bodyLeft > 0L)
					{
						this._bodyLeft -= (long)num2;
					}
					result = num2;
				}
			}
			return result;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		static RequestStream()
		{
			Strings.CreateGetStringDelegate(typeof(RequestStream));
		}

		private long _bodyLeft;

		private byte[] _buffer;

		private int _count;

		private bool _disposed;

		private int _offset;

		private Stream _stream;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
