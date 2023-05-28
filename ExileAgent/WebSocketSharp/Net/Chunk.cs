using System;

namespace WebSocketSharp.Net
{
	internal sealed class Chunk
	{
		public Chunk(byte[] data)
		{
			this._data = data;
		}

		public int ReadLeft
		{
			get
			{
				return this._data.Length - this._offset;
			}
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this._data.Length - this._offset;
			int result;
			if (num == 0)
			{
				result = 0;
			}
			else
			{
				if (count > num)
				{
					count = num;
				}
				Buffer.BlockCopy(this._data, this._offset, buffer, offset, count);
				this._offset += count;
				result = count;
			}
			return result;
		}

		private byte[] _data;

		private int _offset;
	}
}
