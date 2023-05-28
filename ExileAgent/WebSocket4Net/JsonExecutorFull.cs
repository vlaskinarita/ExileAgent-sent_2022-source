using System;

namespace WebSocket4Net
{
	internal sealed class JsonExecutorFull<T> : JsonExecutorBase<T>
	{
		public JsonExecutorFull(Action<JsonWebSocket, string, T> action)
		{
			this.m_ExecutorAction = action;
		}

		public override void Execute(JsonWebSocket websocket, string token, object param)
		{
			this.m_ExecutorAction.Method.Invoke(this.m_ExecutorAction.Target, new object[]
			{
				websocket,
				token,
				param
			});
		}

		private Action<JsonWebSocket, string, T> m_ExecutorAction;
	}
}
