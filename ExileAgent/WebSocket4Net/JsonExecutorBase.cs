using System;

namespace WebSocket4Net
{
	internal abstract class JsonExecutorBase<T> : IJsonExecutor
	{
		public Type Type
		{
			get
			{
				return typeof(T);
			}
		}

		public abstract void Execute(JsonWebSocket websocket, string token, object param);
	}
}
