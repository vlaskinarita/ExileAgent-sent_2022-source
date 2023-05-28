using System;

namespace WebSocket4Net.Common
{
	public interface ICommand<TSession, TCommandInfo> : ICommand where TCommandInfo : ICommandInfo
	{
		void ExecuteCommand(TSession session, TCommandInfo commandInfo);
	}
}
