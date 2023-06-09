﻿using System;

namespace WebSocket4Net.Command
{
	public sealed class Pong : WebSocketCommandBase
	{
		public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
		{
			session.LastActiveTime = DateTime.Now;
			session.LastPongResponse = commandInfo.Text;
		}

		public override string Name
		{
			get
			{
				return 10.ToString();
			}
		}
	}
}
