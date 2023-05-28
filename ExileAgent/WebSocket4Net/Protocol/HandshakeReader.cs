using System;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;

namespace WebSocket4Net.Protocol
{
	internal class HandshakeReader : ReaderBase
	{
		static HandshakeReader()
		{
			Strings.CreateGetStringDelegate(typeof(HandshakeReader));
			HandshakeReader.BadRequestCode = 400.ToString();
			HandshakeReader.HeaderTerminator = Encoding.UTF8.GetBytes(HandshakeReader.getString_0(107309736));
		}

		public HandshakeReader(WebSocket websocket) : base(websocket)
		{
			this.m_HeadSeachState = new SearchMarkState<byte>(HandshakeReader.HeaderTerminator);
		}

		private protected static WebSocketCommandInfo DefaultHandshakeCommandInfo { protected get; private set; }

		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			left = 0;
			int num = this.m_HeadSeachState.Matched;
			int num2 = readBuffer.SearchMark(offset, length, this.m_HeadSeachState);
			if (num2 < 0)
			{
				base.AddArraySegment(readBuffer, offset, length);
				return null;
			}
			int num3 = num2 - offset;
			string text = string.Empty;
			if (base.BufferSegments.Count > 0)
			{
				if (num3 > 0)
				{
					base.AddArraySegment(readBuffer, offset, num3);
					text = base.BufferSegments.Decode(Encoding.UTF8);
					num = 0;
				}
				else
				{
					text = base.BufferSegments.Decode(Encoding.UTF8, 0, base.BufferSegments.Count - num);
				}
			}
			else
			{
				text = Encoding.UTF8.GetString(readBuffer, offset, num3);
				num = 0;
			}
			left = length - num3 - (HandshakeReader.HeaderTerminator.Length - num);
			base.BufferSegments.ClearSegements();
			this.m_HeadSeachState.Matched = 0;
			if (!text.StartsWith(HandshakeReader.getString_0(107142733), StringComparison.OrdinalIgnoreCase))
			{
				return new WebSocketCommandInfo
				{
					Key = -1.ToString(),
					Text = text
				};
			}
			return new WebSocketCommandInfo
			{
				Key = 400.ToString(),
				Text = text
			};
		}

		private const string m_BadRequestPrefix = "HTTP/1.1 400 ";

		protected static readonly string BadRequestCode;

		protected static readonly byte[] HeaderTerminator;

		private SearchMarkState<byte> m_HeadSeachState;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
