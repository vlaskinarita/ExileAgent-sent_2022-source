using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class ChunkStream
	{
		public ChunkStream(WebHeaderCollection headers)
		{
			this._headers = headers;
			this._chunkSize = -1;
			this._chunks = new List<Chunk>();
			this._saved = new StringBuilder();
		}

		public WebHeaderCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		public bool WantsMore
		{
			get
			{
				return this._state < InputChunkState.End;
			}
		}

		private int read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int count2 = this._chunks.Count;
			for (int i = 0; i < count2; i++)
			{
				Chunk chunk = this._chunks[i];
				if (chunk != null)
				{
					if (chunk.ReadLeft == 0)
					{
						this._chunks[i] = null;
					}
					else
					{
						num += chunk.Read(buffer, offset + num, count - num);
						if (num == count)
						{
							break;
						}
					}
				}
			}
			return num;
		}

		private InputChunkState seekCrLf(byte[] buffer, ref int offset, int length)
		{
			int num;
			if (!this._sawCr)
			{
				num = offset;
				offset = num + 1;
				if (buffer[num] != 13)
				{
					ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133339));
				}
				this._sawCr = true;
				if (offset == length)
				{
					return InputChunkState.DataEnded;
				}
			}
			num = offset;
			offset = num + 1;
			if (buffer[num] != 10)
			{
				ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133286));
			}
			return InputChunkState.None;
		}

		private InputChunkState setChunkSize(byte[] buffer, ref int offset, int length)
		{
			byte b = 0;
			while (offset < length)
			{
				int num = offset;
				offset = num + 1;
				b = buffer[num];
				if (this._sawCr)
				{
					if (b != 10)
					{
						ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133286));
					}
					IL_97:
					if (this._saved.Length > 20)
					{
						ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133272));
					}
					InputChunkState result;
					if (b != 10)
					{
						result = InputChunkState.None;
					}
					else
					{
						string s = this._saved.ToString();
						try
						{
							this._chunkSize = int.Parse(s, NumberStyles.HexNumber);
						}
						catch
						{
							ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133203));
						}
						this._chunkRead = 0;
						if (this._chunkSize == 0)
						{
							this._trailerState = 2;
							result = InputChunkState.Trailer;
						}
						else
						{
							result = InputChunkState.Data;
						}
					}
					return result;
				}
				if (b == 13)
				{
					this._sawCr = true;
				}
				else
				{
					if (b == 10)
					{
						ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133265));
					}
					if (!this._gotIt)
					{
						if (b == 32 || b == 59)
						{
							this._gotIt = true;
						}
						else
						{
							this._saved.Append((char)b);
						}
					}
				}
			}
			goto IL_97;
		}

		private InputChunkState setTrailer(byte[] buffer, ref int offset, int length)
		{
			while (offset < length && this._trailerState != 4)
			{
				int num = offset;
				offset = num + 1;
				byte b = buffer[num];
				this._saved.Append((char)b);
				if (this._trailerState == 1 || this._trailerState == 3)
				{
					if (b != 10)
					{
						ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133286));
					}
					this._trailerState++;
				}
				else if (b == 13)
				{
					this._trailerState++;
				}
				else
				{
					if (b == 10)
					{
						ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133265));
					}
					this._trailerState = 0;
				}
			}
			int length2 = this._saved.Length;
			if (length2 > 4196)
			{
				ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133158));
			}
			InputChunkState result;
			if (this._trailerState < 4)
			{
				result = InputChunkState.Trailer;
			}
			else if (length2 == 2)
			{
				result = InputChunkState.End;
			}
			else
			{
				this._saved.Length = length2 - 2;
				string s = this._saved.ToString();
				StringReader stringReader = new StringReader(s);
				for (;;)
				{
					string text = stringReader.ReadLine();
					if (text == null || text.Length == 0)
					{
						break;
					}
					this._headers.Add(text);
				}
				result = InputChunkState.End;
			}
			return result;
		}

		private static void throwProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		private void write(byte[] buffer, int offset, int length)
		{
			if (this._state == InputChunkState.End)
			{
				ChunkStream.throwProtocolViolation(ChunkStream.getString_0(107133157));
			}
			if (this._state == InputChunkState.None)
			{
				this._state = this.setChunkSize(buffer, ref offset, length);
				if (this._state == InputChunkState.None)
				{
					return;
				}
				this._saved.Length = 0;
				this._sawCr = false;
				this._gotIt = false;
			}
			if (this._state == InputChunkState.Data)
			{
				if (offset >= length)
				{
					return;
				}
				this._state = this.writeData(buffer, ref offset, length);
				if (this._state == InputChunkState.Data)
				{
					return;
				}
			}
			if (this._state == InputChunkState.DataEnded)
			{
				if (offset >= length)
				{
					return;
				}
				this._state = this.seekCrLf(buffer, ref offset, length);
				if (this._state == InputChunkState.DataEnded)
				{
					return;
				}
				this._sawCr = false;
			}
			if (this._state == InputChunkState.Trailer)
			{
				if (offset >= length)
				{
					return;
				}
				this._state = this.setTrailer(buffer, ref offset, length);
				if (this._state == InputChunkState.Trailer)
				{
					return;
				}
				this._saved.Length = 0;
			}
			if (offset < length)
			{
				this.write(buffer, offset, length);
			}
		}

		private InputChunkState writeData(byte[] buffer, ref int offset, int length)
		{
			int num = length - offset;
			int num2 = this._chunkSize - this._chunkRead;
			if (num > num2)
			{
				num = num2;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			Chunk item = new Chunk(array);
			this._chunks.Add(item);
			offset += num;
			this._chunkRead += num;
			return (this._chunkRead == this._chunkSize) ? InputChunkState.DataEnded : InputChunkState.Data;
		}

		internal void ResetBuffer()
		{
			this._chunkRead = 0;
			this._chunkSize = -1;
			this._chunks.Clear();
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			int result;
			if (count <= 0)
			{
				result = 0;
			}
			else
			{
				result = this.read(buffer, offset, count);
			}
			return result;
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this.write(buffer, offset, offset + count);
			}
		}

		static ChunkStream()
		{
			Strings.CreateGetStringDelegate(typeof(ChunkStream));
		}

		private int _chunkRead;

		private int _chunkSize;

		private List<Chunk> _chunks;

		private bool _gotIt;

		private WebHeaderCollection _headers;

		private StringBuilder _saved;

		private bool _sawCr;

		private InputChunkState _state;

		private int _trailerState;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
