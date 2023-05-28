using System;
using System.Collections;
using System.Collections.Generic;

namespace WebSocketSharp
{
	internal sealed class PayloadData : IEnumerable, IEnumerable<byte>
	{
		internal PayloadData(byte[] data) : this(data, (long)data.Length)
		{
		}

		internal PayloadData(byte[] data, long length)
		{
			this._data = data;
			this._length = length;
		}

		internal PayloadData(ushort code, string reason)
		{
			this._data = code.Append(reason);
			this._length = (long)this._data.Length;
		}

		internal ushort Code
		{
			get
			{
				return (this._length >= 2L) ? this._data.SubArray(0, 2).ToUInt16(ByteOrder.Big) : 1005;
			}
		}

		internal long ExtensionDataLength
		{
			get
			{
				return this._extDataLength;
			}
			set
			{
				this._extDataLength = value;
			}
		}

		internal bool HasReservedCode
		{
			get
			{
				return this._length >= 2L && this.Code.IsReserved();
			}
		}

		internal string Reason
		{
			get
			{
				string result;
				if (this._length <= 2L)
				{
					result = string.Empty;
				}
				else
				{
					byte[] bytes = this._data.SubArray(2L, this._length - 2L);
					string text;
					result = (bytes.TryGetUTF8DecodedString(out text) ? text : string.Empty);
				}
				return result;
			}
		}

		public byte[] ApplicationData
		{
			get
			{
				return (this._extDataLength > 0L) ? this._data.SubArray(this._extDataLength, this._length - this._extDataLength) : this._data;
			}
		}

		public byte[] ExtensionData
		{
			get
			{
				return (this._extDataLength > 0L) ? this._data.SubArray(0L, this._extDataLength) : WebSocket.EmptyBytes;
			}
		}

		public ulong Length
		{
			get
			{
				return (ulong)this._length;
			}
		}

		internal void Mask(byte[] key)
		{
			for (long num = 0L; num < this._length; num += 1L)
			{
				checked
				{
					this._data[(int)((IntPtr)num)] = (this._data[(int)((IntPtr)num)] ^ key[(int)((IntPtr)(num % 4L))]);
				}
			}
		}

		public IEnumerator<byte> GetEnumerator()
		{
			PayloadData.<GetEnumerator>d__25 <GetEnumerator>d__ = new PayloadData.<GetEnumerator>d__25(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		public byte[] ToArray()
		{
			return this._data;
		}

		public override string ToString()
		{
			return BitConverter.ToString(this._data);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private byte[] _data;

		private long _extDataLength;

		private long _length;

		public static readonly PayloadData Empty = new PayloadData(WebSocket.EmptyBytes, 0L);

		public static readonly ulong MaxLength = 9223372036854775807UL;
	}
}
