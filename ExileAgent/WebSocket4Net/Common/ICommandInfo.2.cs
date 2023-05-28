using System;

namespace WebSocket4Net.Common
{
	public interface ICommandInfo<TCommandData> : ICommandInfo
	{
		TCommandData Data { get; }
	}
}
