using System;

namespace WebSocket4Net
{
	internal sealed class JsonExecutorWithSender<T> : JsonExecutorBase<T>
	{
		public JsonExecutorWithSender(Action<JsonWebSocket, T> action)
		{
			this.m_ExecutorAction = action;
		}

		public override void Execute(JsonWebSocket websocket, string token, object param)
		{
			this.m_ExecutorAction.Method.Invoke(this.m_ExecutorAction.Target, new object[]
			{
				websocket,
				param
			});
		}

		private Action<JsonWebSocket, T> m_ExecutorAction;
	}
}
