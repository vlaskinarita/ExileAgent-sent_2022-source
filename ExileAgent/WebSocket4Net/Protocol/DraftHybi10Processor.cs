using System;
using System.Collections.Generic;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;

namespace WebSocket4Net.Protocol
{
	internal class DraftHybi10Processor : ProtocolProcessorBase
	{
		public DraftHybi10Processor() : base(WebSocketVersion.DraftHybi10, new CloseStatusCodeHybi10())
		{
		}

		protected DraftHybi10Processor(WebSocketVersion version, ICloseStatusCode closeStatusCode, string originHeaderName) : base(version, closeStatusCode)
		{
			this.m_OriginHeaderName = originHeaderName;
		}

		public override void SendHandshake(WebSocket websocket)
		{
			string text = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Substring(0, 16)));
			string value = (text + DraftHybi10Processor.getString_1(107143042)).CalculateChallenge();
			websocket.Items[this.m_ExpectedAcceptKey] = value;
			StringBuilder stringBuilder = new StringBuilder();
			if (websocket.HttpConnectProxy == null)
			{
				stringBuilder.AppendFormatWithCrCf(DraftHybi10Processor.getString_1(107142794), websocket.TargetUri.PathAndQuery);
			}
			else
			{
				stringBuilder.AppendFormatWithCrCf(DraftHybi10Processor.getString_1(107142794), websocket.TargetUri.ToString());
			}
			stringBuilder.Append(DraftHybi10Processor.getString_1(107143249));
			stringBuilder.AppendWithCrCf(websocket.HandshakeHost);
			stringBuilder.AppendWithCrCf(DraftHybi10Processor.getString_1(107142449));
			stringBuilder.AppendWithCrCf(DraftHybi10Processor.getString_1(107143215));
			stringBuilder.Append(DraftHybi10Processor.getString_1(107142424));
			stringBuilder.AppendWithCrCf(base.VersionTag);
			stringBuilder.Append(DraftHybi10Processor.getString_1(107142391));
			stringBuilder.AppendWithCrCf(text);
			stringBuilder.Append(string.Format(DraftHybi10Processor.getString_1(107142362), this.m_OriginHeaderName));
			stringBuilder.AppendWithCrCf(websocket.Origin);
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				stringBuilder.Append(DraftHybi10Processor.getString_1(107143147));
				stringBuilder.AppendWithCrCf(websocket.SubProtocol);
			}
			List<KeyValuePair<string, string>> cookies = websocket.Cookies;
			if (cookies != null && cookies.Count > 0)
			{
				string[] array = new string[cookies.Count];
				for (int i = 0; i < cookies.Count; i++)
				{
					KeyValuePair<string, string> keyValuePair = cookies[i];
					array[i] = keyValuePair.Key + DraftHybi10Processor.getString_1(107231361) + Uri.EscapeUriString(keyValuePair.Value);
				}
				stringBuilder.Append(DraftHybi10Processor.getString_1(107143114));
				stringBuilder.AppendWithCrCf(string.Join(DraftHybi10Processor.getString_1(107247973), array));
			}
			if (websocket.CustomHeaderItems != null)
			{
				for (int j = 0; j < websocket.CustomHeaderItems.Count; j++)
				{
					KeyValuePair<string, string> keyValuePair2 = websocket.CustomHeaderItems[j];
					stringBuilder.AppendFormatWithCrCf(DraftHybi10Processor.getString_1(107370601), new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
			stringBuilder.AppendWithCrCf();
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi10HandshakeReader(websocket);
		}

		private void SendMessage(WebSocket websocket, int opCode, string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			this.SendDataFragment(websocket, opCode, bytes, 0, bytes.Length);
		}

		private byte[] EncodeDataFrame(int opCode, byte[] playloadData, int offset, int length)
		{
			return this.EncodeDataFrame(opCode, true, playloadData, offset, length);
		}

		private byte[] EncodeDataFrame(int opCode, bool isFinal, byte[] playloadData, int offset, int length)
		{
			int num = 4;
			byte[] array;
			if (length < 126)
			{
				array = new byte[2 + num + length];
				array[1] = (byte)length;
			}
			else if (length < 65536)
			{
				array = new byte[4 + num + length];
				array[1] = 126;
				array[2] = (byte)(length / 256);
				array[3] = (byte)(length % 256);
			}
			else
			{
				array = new byte[10 + num + length];
				array[1] = 127;
				int num2 = length;
				int num3 = 256;
				for (int i = 9; i > 1; i--)
				{
					array[i] = (byte)(num2 % num3);
					num2 /= num3;
					if (num2 == 0)
					{
						break;
					}
				}
			}
			if (isFinal)
			{
				array[0] = (byte)(opCode | 128);
			}
			else
			{
				array[0] = (byte)opCode;
			}
			array[1] = (array[1] | 128);
			byte[] array2 = array;
			this.GenerateMask(array2, array2.Length - num - length);
			if (length > 0)
			{
				byte[] array3 = array;
				int outputOffset = array3.Length - length;
				byte[] array4 = array;
				this.MaskData(playloadData, offset, length, array3, outputOffset, array4, array4.Length - num - length);
			}
			return array;
		}

		private void SendDataFragment(WebSocket websocket, int opCode, byte[] playloadData, int offset, int length)
		{
			byte[] array = this.EncodeDataFrame(opCode, playloadData, offset, length);
			TcpClientSession client = websocket.Client;
			if (client != null)
			{
				client.Send(array, 0, array.Length);
			}
		}

		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			this.SendDataFragment(websocket, 2, data, offset, length);
		}

		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>(segments.Count);
			int num = segments.Count - 1;
			for (int i = 0; i < segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = segments[i];
				list.Add(new ArraySegment<byte>(this.EncodeDataFrame((i == 0) ? 2 : 0, i == num, arraySegment.Array, arraySegment.Offset, arraySegment.Count)));
			}
			TcpClientSession client = websocket.Client;
			if (client != null)
			{
				client.Send(list);
			}
		}

		public override void SendMessage(WebSocket websocket, string message)
		{
			this.SendMessage(websocket, 1, message);
		}

		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			byte[] array = new byte[(string.IsNullOrEmpty(closeReason) ? 0 : Encoding.UTF8.GetMaxByteCount(closeReason.Length)) + 2];
			int num = statusCode / 256;
			int num2 = statusCode % 256;
			array[0] = (byte)num;
			array[1] = (byte)num2;
			if (websocket != null)
			{
				if (websocket.State != WebSocketState.Closed)
				{
					if (!string.IsNullOrEmpty(closeReason))
					{
						int bytes = Encoding.UTF8.GetBytes(closeReason, 0, closeReason.Length, array, 2);
						this.SendDataFragment(websocket, 8, array, 0, bytes + 2);
						return;
					}
					this.SendDataFragment(websocket, 8, array, 0, array.Length);
					return;
				}
			}
		}

		public override void SendPing(WebSocket websocket, string ping)
		{
			this.SendMessage(websocket, 9, ping);
		}

		public override void SendPong(WebSocket websocket, string pong)
		{
			this.SendMessage(websocket, 10, pong);
		}

		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			if (string.IsNullOrEmpty(handshakeInfo.Text))
			{
				description = DraftHybi10Processor.getString_1(107142819);
				return false;
			}
			string empty = string.Empty;
			if (!handshakeInfo.Text.ParseMimeHeader(websocket.Items, out empty))
			{
				description = DraftHybi10Processor.getString_1(107142819);
				return false;
			}
			if (!this.ValidateVerbLine(empty))
			{
				description = empty;
				return false;
			}
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				string value = websocket.Items.GetValue(DraftHybi10Processor.getString_1(107142353), string.Empty);
				if (!websocket.SubProtocol.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					description = DraftHybi10Processor.getString_1(107142320);
					return false;
				}
			}
			string value2 = websocket.Items.GetValue(DraftHybi10Processor.getString_1(107142315), string.Empty);
			if (!websocket.Items.GetValue(this.m_ExpectedAcceptKey, string.Empty).Equals(value2, StringComparison.OrdinalIgnoreCase))
			{
				description = DraftHybi10Processor.getString_1(107142286);
				return false;
			}
			description = string.Empty;
			return true;
		}

		public override bool SupportBinary
		{
			get
			{
				return true;
			}
		}

		public override bool SupportPingPong
		{
			get
			{
				return true;
			}
		}

		private void GenerateMask(byte[] mask, int offset)
		{
			int num = Math.Min(offset + 4, mask.Length);
			for (int i = offset; i < num; i++)
			{
				mask[i] = (byte)DraftHybi10Processor.m_Random.Next(0, 255);
			}
		}

		private void MaskData(byte[] rawData, int offset, int length, byte[] outputData, int outputOffset, byte[] mask, int maskOffset)
		{
			for (int i = 0; i < length; i++)
			{
				int num = offset + i;
				outputData[outputOffset++] = (rawData[num] ^ mask[maskOffset + i % 4]);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DraftHybi10Processor()
		{
			Strings.CreateGetStringDelegate(typeof(DraftHybi10Processor));
			DraftHybi10Processor.m_Random = new Random();
		}

		private const string m_Magic = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private string m_ExpectedAcceptKey = DraftHybi10Processor.getString_1(107143060);

		private readonly string m_OriginHeaderName = DraftHybi10Processor.getString_1(107143071);

		private static Random m_Random;

		private const string m_Error_InvalidHandshake = "invalid handshake";

		private const string m_Error_SubProtocolNotMatch = "subprotocol doesn't match";

		private const string m_Error_AcceptKeyNotMatch = "accept key doesn't match";

		[NonSerialized]
		internal static GetString getString_1;
	}
}
