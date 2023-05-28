using System;

namespace WebSocket4Net.Common
{
	public abstract class CommandInfo<TCommandData> : ICommandInfo<TCommandData>, ICommandInfo
	{
		public CommandInfo(string key, TCommandData data)
		{
			this.Key = key;
			this.Data = data;
		}

		public TCommandData Data { get; private set; }

		public string Key { get; private set; }
	}
}
