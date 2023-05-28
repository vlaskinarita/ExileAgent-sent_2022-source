using System;

namespace WebSocket4Net
{
	internal sealed class JsonExecutorWithSenderAndState<T> : JsonExecutorBase<T>
	{
		public JsonExecutorWithSenderAndState(Action<JsonWebSocket, T, object> action, object state)
		{
			this.m_ExecutorAction = action;
			this.m_State = state;
		}

		public override void Execute(JsonWebSocket websocket, string token, object param)
		{
			this.m_ExecutorAction.Method.Invoke(this.m_ExecutorAction.Target, new object[]
			{
				websocket,
				param,
				this.m_State
			});
		}

		private Action<JsonWebSocket, T, object> m_ExecutorAction;

		private object m_State;
	}
}
