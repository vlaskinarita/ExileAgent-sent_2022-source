using System;
using System.IO;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class ChunkedRequestStream : RequestStream
	{
		static ChunkedRequestStream()
		{
			Strings.CreateGetStringDelegate(typeof(ChunkedRequestStream));
			ChunkedRequestStream._bufferLength = 8192;
		}

		internal ChunkedRequestStream(Stream stream, byte[] buffer, int offset, int count, HttpListenerContext context) : base(stream, buffer, offset, count, -1L)
		{
			this._context = context;
			this._decoder = new ChunkStream((WebHeaderCollection)context.Request.Headers);
		}

		private void onRead(IAsyncResult asyncResult)
		{
			ReadBufferState readBufferState = (ReadBufferState)asyncResult.AsyncState;
			HttpStreamAsyncResult asyncResult2 = readBufferState.AsyncResult;
			try
			{
				int num = base.EndRead(asyncResult);
				this._decoder.Write(asyncResult2.Buffer, asyncResult2.Offset, num);
				num = this._decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += num;
				readBufferState.Count -= num;
				if (readBufferState.Count == 0 || !this._decoder.WantsMore || num == 0)
				{
					this._noMoreData = (!this._decoder.WantsMore && num == 0);
					asyncResult2.Count = readBufferState.InitialCount - readBufferState.Count;
					asyncResult2.Complete();
				}
				else
				{
					base.BeginRead(asyncResult2.Buffer, asyncResult2.Offset, asyncResult2.Count, new AsyncCallback(this.onRead), readBufferState);
				}
			}
			catch (Exception exception)
			{
				this._context.ErrorMessage = ChunkedRequestStream.getString_1(107160797);
				this._context.SendError();
				asyncResult2.Complete(exception);
			}
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
				throw new ArgumentNullException(ChunkedRequestStream.getString_1(107350623));
			}
			if (offset < 0)
			{
				string message = ChunkedRequestStream.getString_1(107130500);
				throw new ArgumentOutOfRangeException(ChunkedRequestStream.getString_1(107145209), message);
			}
			if (count < 0)
			{
				string message2 = ChunkedRequestStream.getString_1(107130500);
				throw new ArgumentOutOfRangeException(ChunkedRequestStream.getString_1(107350573), message2);
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				string message3 = ChunkedRequestStream.getString_1(107130475);
				throw new ArgumentException(message3);
			}
			HttpStreamAsyncResult httpStreamAsyncResult = new HttpStreamAsyncResult(callback, state);
			IAsyncResult result;
			if (this._noMoreData)
			{
				httpStreamAsyncResult.Complete();
				result = httpStreamAsyncResult;
			}
			else
			{
				int num2 = this._decoder.Read(buffer, offset, count);
				offset += num2;
				count -= num2;
				if (count == 0)
				{
					httpStreamAsyncResult.Count = num2;
					httpStreamAsyncResult.Complete();
					result = httpStreamAsyncResult;
				}
				else if (!this._decoder.WantsMore)
				{
					this._noMoreData = (num2 == 0);
					httpStreamAsyncResult.Count = num2;
					httpStreamAsyncResult.Complete();
					result = httpStreamAsyncResult;
				}
				else
				{
					httpStreamAsyncResult.Buffer = new byte[ChunkedRequestStream._bufferLength];
					httpStreamAsyncResult.Offset = 0;
					httpStreamAsyncResult.Count = ChunkedRequestStream._bufferLength;
					ReadBufferState readBufferState = new ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
					readBufferState.InitialCount += num2;
					base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, new AsyncCallback(this.onRead), readBufferState);
					result = httpStreamAsyncResult;
				}
			}
			return result;
		}

		public override void Close()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				base.Close();
			}
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
				throw new ArgumentNullException(ChunkedRequestStream.getString_1(107131847));
			}
			HttpStreamAsyncResult httpStreamAsyncResult = asyncResult as HttpStreamAsyncResult;
			if (httpStreamAsyncResult == null)
			{
				string message = ChunkedRequestStream.getString_1(107131862);
				throw new ArgumentException(message, ChunkedRequestStream.getString_1(107131847));
			}
			if (!httpStreamAsyncResult.IsCompleted)
			{
				httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.HasException)
			{
				string message2 = ChunkedRequestStream.getString_1(107161280);
				throw new HttpListenerException(995, message2);
			}
			return httpStreamAsyncResult.Count;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, count, null, null);
			return this.EndRead(asyncResult);
		}

		private static readonly int _bufferLength;

		private HttpListenerContext _context;

		private ChunkStream _decoder;

		private bool _disposed;

		private bool _noMoreData;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
