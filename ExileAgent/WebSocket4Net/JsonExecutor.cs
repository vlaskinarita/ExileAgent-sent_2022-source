using System;

namespace WebSocket4Net
{
	internal sealed class JsonExecutor<T> : JsonExecutorBase<T>
	{
		public JsonExecutor(Action<T> action)
		{
			this.m_ExecutorAction = action;
		}

		public override void Execute(JsonWebSocket websocket, string token, object param)
		{
			this.m_ExecutorAction.Method.Invoke(this.m_ExecutorAction.Target, new object[]
			{
				param
			});
		}

		private Action<T> m_ExecutorAction;
	}
}
