using System;
using WebSocket4Net.Common;

namespace WebSocket4Net.Command
{
	public abstract class WebSocketCommandBase : ICommand<WebSocket, WebSocketCommandInfo>, ICommand
	{
		public abstract void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo);

		public abstract string Name { get; }
	}
}
