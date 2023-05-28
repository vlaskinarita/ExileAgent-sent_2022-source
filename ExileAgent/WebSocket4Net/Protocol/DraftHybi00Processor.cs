using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;

namespace WebSocket4Net.Protocol
{
	internal sealed class DraftHybi00Processor : ProtocolProcessorBase
	{
		public DraftHybi00Processor() : base(WebSocketVersion.DraftHybi00, new CloseStatusCodeHybi10())
		{
		}

		static DraftHybi00Processor()
		{
			Strings.CreateGetStringDelegate(typeof(DraftHybi00Processor));
			DraftHybi00Processor.m_CharLib = new List<char>();
			DraftHybi00Processor.m_DigLib = new List<char>();
			DraftHybi00Processor.m_Random = new Random();
			byte[] array = new byte[2];
			array[0] = byte.MaxValue;
			DraftHybi00Processor.CloseHandshake = array;
			for (int i = 33; i <= 126; i++)
			{
				char c = (char)i;
				if (char.IsLetter(c))
				{
					DraftHybi00Processor.m_CharLib.Add(c);
				}
				else if (char.IsDigit(c))
				{
					DraftHybi00Processor.m_DigLib.Add(c);
				}
			}
		}

		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi00HandshakeReader(websocket);
		}

		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			byte[] data = handshakeInfo.Data;
			if (data.Length != data.Length)
			{
				description = DraftHybi00Processor.getString_1(107142872);
				return false;
			}
			for (int i = 0; i < this.m_ExpectedChallenge.Length; i++)
			{
				if (data[i] != this.m_ExpectedChallenge[i])
				{
					description = DraftHybi00Processor.getString_1(107142831);
					return false;
				}
			}
			string empty = string.Empty;
			if (!handshakeInfo.Text.ParseMimeHeader(websocket.Items, out empty))
			{
				description = DraftHybi00Processor.getString_1(107142798);
				return false;
			}
			if (!this.ValidateVerbLine(empty))
			{
				description = empty;
				return false;
			}
			description = string.Empty;
			return true;
		}

		public override void SendMessage(WebSocket websocket, string message)
		{
			byte[] array = new byte[Encoding.UTF8.GetMaxByteCount(message.Length) + 2];
			array[0] = 0;
			int bytes = Encoding.UTF8.GetBytes(message, 0, message.Length, array, 1);
			array[1 + bytes] = byte.MaxValue;
			websocket.Client.Send(array, 0, bytes + 2);
		}

		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			throw new NotSupportedException();
		}

		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			throw new NotSupportedException();
		}

		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			if (websocket != null)
			{
				if (websocket.State != WebSocketState.Closed)
				{
					websocket.Client.Send(DraftHybi00Processor.CloseHandshake, 0, DraftHybi00Processor.CloseHandshake.Length);
					return;
				}
			}
		}

		public override void SendPing(WebSocket websocket, string ping)
		{
			throw new NotSupportedException();
		}

		public override void SendPong(WebSocket websocket, string pong)
		{
			throw new NotSupportedException();
		}

		public override void SendHandshake(WebSocket websocket)
		{
			string @string = Encoding.UTF8.GetString(this.GenerateSecKey());
			string string2 = Encoding.UTF8.GetString(this.GenerateSecKey());
			byte[] array = this.GenerateSecKey(8);
			this.m_ExpectedChallenge = this.GetResponseSecurityKey(@string, string2, array);
			StringBuilder stringBuilder = new StringBuilder();
			if (websocket.HttpConnectProxy == null)
			{
				stringBuilder.AppendFormatWithCrCf(DraftHybi00Processor.getString_1(107142773), websocket.TargetUri.PathAndQuery);
			}
			else
			{
				stringBuilder.AppendFormatWithCrCf(DraftHybi00Processor.getString_1(107142773), websocket.TargetUri.ToString());
			}
			stringBuilder.Append(DraftHybi00Processor.getString_1(107143228));
			stringBuilder.AppendWithCrCf(websocket.TargetUri.Host);
			stringBuilder.AppendWithCrCf(DraftHybi00Processor.getString_1(107143251));
			stringBuilder.AppendWithCrCf(DraftHybi00Processor.getString_1(107143194));
			stringBuilder.Append(DraftHybi00Processor.getString_1(107143165));
			stringBuilder.AppendWithCrCf(@string);
			stringBuilder.Append(DraftHybi00Processor.getString_1(107143136));
			stringBuilder.AppendWithCrCf(string2);
			stringBuilder.Append(DraftHybi00Processor.getString_1(107143107));
			stringBuilder.AppendWithCrCf(string.IsNullOrEmpty(websocket.Origin) ? websocket.TargetUri.Host : websocket.Origin);
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				stringBuilder.Append(DraftHybi00Processor.getString_1(107143126));
				stringBuilder.AppendWithCrCf(websocket.SubProtocol);
			}
			List<KeyValuePair<string, string>> cookies = websocket.Cookies;
			if (cookies != null && cookies.Count > 0)
			{
				string[] array2 = new string[cookies.Count];
				for (int i = 0; i < cookies.Count; i++)
				{
					KeyValuePair<string, string> keyValuePair = cookies[i];
					array2[i] = keyValuePair.Key + DraftHybi00Processor.getString_1(107231340) + Uri.EscapeUriString(keyValuePair.Value);
				}
				stringBuilder.Append(DraftHybi00Processor.getString_1(107143093));
				stringBuilder.AppendWithCrCf(string.Join(DraftHybi00Processor.getString_1(107247952), array2));
			}
			if (websocket.CustomHeaderItems != null)
			{
				for (int j = 0; j < websocket.CustomHeaderItems.Count; j++)
				{
					KeyValuePair<string, string> keyValuePair2 = websocket.CustomHeaderItems[j];
					stringBuilder.AppendFormatWithCrCf(DraftHybi00Processor.getString_1(107370580), new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
			stringBuilder.AppendWithCrCf();
			stringBuilder.Append(Encoding.UTF8.GetString(array, 0, array.Length));
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		private byte[] GetResponseSecurityKey(string secKey1, string secKey2, byte[] secKey3)
		{
			string s = Regex.Replace(secKey1, DraftHybi00Processor.getString_1(107143048), string.Empty);
			string s2 = Regex.Replace(secKey2, DraftHybi00Processor.getString_1(107143048), string.Empty);
			long num = long.Parse(s);
			long num2 = long.Parse(s2);
			int num3 = secKey1.Count((char c) => c == ' ');
			int num4 = secKey2.Count((char c) => c == ' ');
			int value = (int)(num / (long)num3);
			int value2 = (int)(num2 / (long)num4);
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			Array.Reverse(bytes2);
			byte[] array = new byte[bytes.Length + bytes2.Length + secKey3.Length];
			Array.Copy(bytes, 0, array, 0, bytes.Length);
			Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
			Array.Copy(secKey3, 0, array, bytes.Length + bytes2.Length, secKey3.Length);
			return array.ComputeMD5Hash();
		}

		private byte[] GenerateSecKey()
		{
			int totalLen = DraftHybi00Processor.m_Random.Next(10, 20);
			return this.GenerateSecKey(totalLen);
		}

		private byte[] GenerateSecKey(int totalLen)
		{
			int num = DraftHybi00Processor.m_Random.Next(1, totalLen / 2 + 1);
			int num2 = DraftHybi00Processor.m_Random.Next(3, totalLen - 1 - num);
			int num3 = totalLen - num - num2;
			byte[] array = new byte[totalLen];
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				array[num4++] = 32;
			}
			for (int j = 0; j < num2; j++)
			{
				array[num4++] = (byte)DraftHybi00Processor.m_CharLib[DraftHybi00Processor.m_Random.Next(0, DraftHybi00Processor.m_CharLib.Count - 1)];
			}
			for (int k = 0; k < num3; k++)
			{
				array[num4++] = (byte)DraftHybi00Processor.m_DigLib[DraftHybi00Processor.m_Random.Next(0, DraftHybi00Processor.m_DigLib.Count - 1)];
			}
			return array.RandomOrder<byte>();
		}

		public override bool SupportBinary
		{
			get
			{
				return false;
			}
		}

		public override bool SupportPingPong
		{
			get
			{
				return false;
			}
		}

		private static List<char> m_CharLib;

		private static List<char> m_DigLib;

		private static Random m_Random;

		public const byte StartByte = 0;

		public const byte EndByte = 255;

		public static byte[] CloseHandshake;

		private byte[] m_ExpectedChallenge;

		private const string m_Error_ChallengeLengthNotMatch = "challenge length doesn't match";

		private const string m_Error_ChallengeNotMatch = "challenge doesn't match";

		private const string m_Error_InvalidHandshake = "invalid handshake";

		[NonSerialized]
		internal static GetString getString_1;
	}
}
