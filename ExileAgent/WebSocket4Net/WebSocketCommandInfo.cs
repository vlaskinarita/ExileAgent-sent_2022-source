using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net.Common;
using WebSocket4Net.Protocol;

namespace WebSocket4Net
{
	public sealed class WebSocketCommandInfo : ICommandInfo
	{
		public WebSocketCommandInfo()
		{
		}

		public WebSocketCommandInfo(string key)
		{
			this.Key = key;
		}

		public WebSocketCommandInfo(string key, string text)
		{
			this.Key = key;
			this.Text = text;
		}

		public WebSocketCommandInfo(IList<WebSocketDataFrame> frames)
		{
			sbyte opCode = frames[0].OpCode;
			this.Key = opCode.ToString();
			if (opCode == 8)
			{
				WebSocketDataFrame webSocketDataFrame = frames[0];
				int num = (int)webSocketDataFrame.ActualPayloadLength;
				int num2 = webSocketDataFrame.InnerData.Count - num;
				StringBuilder stringBuilder = new StringBuilder();
				if (num >= 2)
				{
					num2 = webSocketDataFrame.InnerData.Count - num;
					byte[] array = webSocketDataFrame.InnerData.ToArrayData(num2, 2);
					this.CloseStatusCode = (short)((int)array[0] * 256 + (int)array[1]);
					if (num > 2)
					{
						stringBuilder.Append(webSocketDataFrame.InnerData.Decode(Encoding.UTF8, num2 + 2, num - 2));
					}
				}
				else if (num > 0)
				{
					stringBuilder.Append(webSocketDataFrame.InnerData.Decode(Encoding.UTF8, num2, num));
				}
				if (frames.Count > 1)
				{
					for (int i = 1; i < frames.Count; i++)
					{
						WebSocketDataFrame webSocketDataFrame2 = frames[i];
						num2 = webSocketDataFrame2.InnerData.Count - (int)webSocketDataFrame2.ActualPayloadLength;
						num = (int)webSocketDataFrame2.ActualPayloadLength;
						if (webSocketDataFrame2.HasMask)
						{
							webSocketDataFrame2.InnerData.DecodeMask(webSocketDataFrame2.MaskKey, num2, num);
						}
						stringBuilder.Append(webSocketDataFrame2.InnerData.Decode(Encoding.UTF8, num2, num));
					}
				}
				this.Text = stringBuilder.ToString();
				return;
			}
			if (opCode != 2)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int j = 0; j < frames.Count; j++)
				{
					WebSocketDataFrame webSocketDataFrame3 = frames[j];
					int num2 = webSocketDataFrame3.InnerData.Count - (int)webSocketDataFrame3.ActualPayloadLength;
					int num = (int)webSocketDataFrame3.ActualPayloadLength;
					if (webSocketDataFrame3.HasMask)
					{
						webSocketDataFrame3.InnerData.DecodeMask(webSocketDataFrame3.MaskKey, num2, num);
					}
					stringBuilder2.Append(webSocketDataFrame3.InnerData.Decode(Encoding.UTF8, num2, num));
				}
				this.Text = stringBuilder2.ToString();
				return;
			}
			byte[] array2 = new byte[frames.Sum((WebSocketDataFrame f) => (int)f.ActualPayloadLength)];
			int num3 = 0;
			for (int k = 0; k < frames.Count; k++)
			{
				WebSocketDataFrame webSocketDataFrame4 = frames[k];
				int num2 = webSocketDataFrame4.InnerData.Count - (int)webSocketDataFrame4.ActualPayloadLength;
				int num = (int)webSocketDataFrame4.ActualPayloadLength;
				if (webSocketDataFrame4.HasMask)
				{
					webSocketDataFrame4.InnerData.DecodeMask(webSocketDataFrame4.MaskKey, num2, num);
				}
				webSocketDataFrame4.InnerData.CopyTo(array2, num2, num3, num);
				num3 += num;
			}
			this.Data = array2;
		}

		public WebSocketCommandInfo(WebSocketDataFrame frame)
		{
			this.Key = frame.OpCode.ToString();
			int num = (int)frame.ActualPayloadLength;
			int num2 = frame.InnerData.Count - (int)frame.ActualPayloadLength;
			if (frame.HasMask && num > 0)
			{
				frame.InnerData.DecodeMask(frame.MaskKey, num2, num);
			}
			if (frame.OpCode == 8 && num >= 2)
			{
				byte[] array = frame.InnerData.ToArrayData(num2, 2);
				this.CloseStatusCode = (short)((int)array[0] * 256 + (int)array[1]);
				if (num > 2)
				{
					this.Text = frame.InnerData.Decode(Encoding.UTF8, num2 + 2, num - 2);
					return;
				}
				this.Text = string.Empty;
				return;
			}
			else if (frame.OpCode != 2)
			{
				if (num > 0)
				{
					this.Text = frame.InnerData.Decode(Encoding.UTF8, num2, num);
					return;
				}
				this.Text = string.Empty;
				return;
			}
			else
			{
				if (num > 0)
				{
					this.Data = frame.InnerData.ToArrayData(num2, num);
					return;
				}
				this.Data = new byte[0];
				return;
			}
		}

		public string Key { get; set; }

		public byte[] Data { get; set; }

		public string Text { get; set; }

		public short CloseStatusCode { get; private set; }
	}
}
