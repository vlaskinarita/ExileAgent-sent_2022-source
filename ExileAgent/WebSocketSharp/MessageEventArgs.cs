using System;

namespace WebSocketSharp
{
	public sealed class MessageEventArgs : EventArgs
	{
		internal MessageEventArgs(WebSocketFrame frame)
		{
			this._opcode = frame.Opcode;
			this._rawData = frame.PayloadData.ApplicationData;
		}

		internal MessageEventArgs(Opcode opcode, byte[] rawData)
		{
			if ((long)rawData.Length > (long)PayloadData.MaxLength)
			{
				throw new WebSocketException(CloseStatusCode.TooBig);
			}
			this._opcode = opcode;
			this._rawData = rawData;
		}

		internal Opcode Opcode
		{
			get
			{
				return this._opcode;
			}
		}

		public string Data
		{
			get
			{
				this.setData();
				return this._data;
			}
		}

		public bool IsBinary
		{
			get
			{
				return this._opcode == Opcode.Binary;
			}
		}

		public bool IsPing
		{
			get
			{
				return this._opcode == Opcode.Ping;
			}
		}

		public bool IsText
		{
			get
			{
				return this._opcode == Opcode.Text;
			}
		}

		public byte[] RawData
		{
			get
			{
				this.setData();
				return this._rawData;
			}
		}

		private void setData()
		{
			if (!this._dataSet)
			{
				if (this._opcode == Opcode.Binary)
				{
					this._dataSet = true;
				}
				else
				{
					string data;
					if (this._rawData.TryGetUTF8DecodedString(out data))
					{
						this._data = data;
					}
					this._dataSet = true;
				}
			}
		}

		private string _data;

		private bool _dataSet;

		private Opcode _opcode;

		private byte[] _rawData;
	}
}
