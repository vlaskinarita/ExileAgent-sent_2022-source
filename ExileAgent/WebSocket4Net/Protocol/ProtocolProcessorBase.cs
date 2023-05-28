using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocket4Net.Protocol
{
	internal abstract class ProtocolProcessorBase : IProtocolProcessor
	{
		public ProtocolProcessorBase(WebSocketVersion version, ICloseStatusCode closeStatusCode)
		{
			this.CloseStatusCode = closeStatusCode;
			this.Version = version;
			int num = (int)version;
			this.VersionTag = num.ToString();
		}

		public abstract void SendHandshake(WebSocket websocket);

		public abstract ReaderBase CreateHandshakeReader(WebSocket websocket);

		public abstract bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description);

		public abstract void SendMessage(WebSocket websocket, string message);

		public abstract void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason);

		public abstract void SendPing(WebSocket websocket, string ping);

		public abstract void SendPong(WebSocket websocket, string pong);

		public abstract void SendData(WebSocket websocket, byte[] data, int offset, int length);

		public abstract void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments);

		public abstract bool SupportBinary { get; }

		public abstract bool SupportPingPong { get; }

		public ICloseStatusCode CloseStatusCode { get; private set; }

		public WebSocketVersion Version { get; private set; }

		private protected string VersionTag { protected get; private set; }

		protected virtual bool ValidateVerbLine(string verbLine)
		{
			string[] array = verbLine.Split(ProtocolProcessorBase.s_SpaceSpliter, 3, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return false;
			}
			if (!array[0].StartsWith(ProtocolProcessorBase.getString_0(107142700)))
			{
				return false;
			}
			int num = 0;
			return int.TryParse(array[1], out num) && num == 101;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ProtocolProcessorBase()
		{
			Strings.CreateGetStringDelegate(typeof(ProtocolProcessorBase));
			ProtocolProcessorBase.s_SpaceSpliter = new char[]
			{
				' '
			};
		}

		protected const string HeaderItemFormat = "{0}: {1}";

		private static char[] s_SpaceSpliter;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
