using System;
using SuperSocket.ClientEngine;

namespace WebSocket4Net.Common
{
	internal sealed class DelegateCommand<TClientSession, TCommandInfo> : ICommand<TClientSession, TCommandInfo>, ICommand where TClientSession : IClientSession where TCommandInfo : ICommandInfo
	{
		public DelegateCommand(string name, CommandDelegate<TClientSession, TCommandInfo> execution)
		{
			this.Name = name;
			this.m_Execution = execution;
		}

		public void ExecuteCommand(TClientSession session, TCommandInfo commandInfo)
		{
			this.m_Execution(session, commandInfo);
		}

		public string Name { get; private set; }

		private CommandDelegate<TClientSession, TCommandInfo> m_Execution;
	}
}
