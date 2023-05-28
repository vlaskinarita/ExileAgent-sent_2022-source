using System;

namespace WebSocket4Net.Command
{
	public sealed class Close : WebSocketCommandBase
	{
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			if (session.StateCode == 2)
			{
				session.CloseWithoutHandshake();
				return;
			}
			short num = commandInfo.CloseStatusCode;
			if (num <= 0)
			{
				num = session.ProtocolProcessor.CloseStatusCode.NoStatusCode;
			}
			session.Close((int)num, commandInfo.Text);
		}

		public override string Name
		{
			get
			{
				return 8.ToString();
			}
		}
	}
}
