using System;
using WebSocket4Net.Common;

namespace WebSocket4Net.Protocol
{
	public sealed class WebSocketDataFrame
	{
		public ArraySegmentList InnerData
		{
			get
			{
				return this.m_InnerData;
			}
		}

		public WebSocketDataFrame(ArraySegmentList data)
		{
			this.m_InnerData = data;
			this.m_InnerData.ClearSegements();
		}

		public bool IsControlFrame
		{
			get
			{
				switch (this.OpCode)
				{
				case 8:
				case 9:
				case 10:
					return true;
				default:
					return false;
				}
			}
		}

		public bool FIN
		{
			get
			{
				return (this.m_InnerData[0] & 128) == 128;
			}
		}

		public bool RSV1
		{
			get
			{
				return (this.m_InnerData[0] & 64) == 64;
			}
		}

		public bool RSV2
		{
			get
			{
				return (this.m_InnerData[0] & 32) == 32;
			}
		}

		public bool RSV3
		{
			get
			{
				return (this.m_InnerData[0] & 16) == 16;
			}
		}

		public sbyte OpCode
		{
			get
			{
				return (sbyte)(this.m_InnerData[0] & 15);
			}
		}

		public bool HasMask
		{
			get
			{
				return (this.m_InnerData[1] & 128) == 128;
			}
		}

		public sbyte PayloadLenght
		{
			get
			{
				return (sbyte)(this.m_InnerData[1] & 127);
			}
		}

		public long ActualPayloadLength
		{
			get
			{
				if (this.m_ActualPayloadLength >= 0L)
				{
					return this.m_ActualPayloadLength;
				}
				sbyte payloadLenght = this.PayloadLenght;
				if (payloadLenght < 126)
				{
					this.m_ActualPayloadLength = (long)payloadLenght;
				}
				else if (payloadLenght == 126)
				{
					this.m_ActualPayloadLength = (long)((int)this.m_InnerData[2] * 256 + (int)this.m_InnerData[3]);
				}
				else
				{
					long num = 0L;
					int num2 = 1;
					for (int i = 7; i >= 0; i--)
					{
						num += (long)((int)this.m_InnerData[i + 2] * num2);
						num2 *= 256;
					}
					this.m_ActualPayloadLength = num;
				}
				return this.m_ActualPayloadLength;
			}
		}

		public byte[] MaskKey { get; set; }

		public byte[] ExtensionData { get; set; }

		public byte[] ApplicationData { get; set; }

		public int Length
		{
			get
			{
				return this.m_InnerData.Count;
			}
		}

		public void Clear()
		{
			this.m_InnerData.ClearSegements();
			this.ExtensionData = new byte[0];
			this.ApplicationData = new byte[0];
			this.m_ActualPayloadLength = -1L;
		}

		private ArraySegmentList m_InnerData;

		private long m_ActualPayloadLength = -1L;
	}
}
