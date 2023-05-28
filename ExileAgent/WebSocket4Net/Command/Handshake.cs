using System;

namespace WebSocket4Net.Command
{
	public sealed class Handshake : WebSocketCommandBase
	{
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			string text;
			if (!session.ProtocolProcessor.VerifyHandshake(session, commandInfo, out text))
			{
				session.FireError(new Exception(text));
				session.Close((int)session.ProtocolProcessor.CloseStatusCode.ProtocolError, text);
				return;
			}
			session.OnHandshaked();
		}

		public override string Name
		{
			get
			{
				return -1.ToString();
			}
		}
	}
}
