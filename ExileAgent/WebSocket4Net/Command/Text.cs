using System;

namespace WebSocket4Net.Command
{
	public sealed class Text : WebSocketCommandBase
	{
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.FireMessageReceived(commandInfo.Text);
		}

		public override string Name
		{
			get
			{
				return 1.ToString();
			}
		}
	}
}
