using System;

namespace WebSocket4Net
{
	internal sealed class JsonExecutorWithToken<T> : JsonExecutorBase<T>
	{
		public JsonExecutorWithToken(Action<string, T> action)
		{
			this.m_ExecutorAction = action;
		}

		public override void Execute(JsonWebSocket websocket, string token, object param)
		{
			this.m_ExecutorAction.Method.Invoke(this.m_ExecutorAction.Target, new object[]
			{
				token,
				param
			});
		}

		private Action<string, T> m_ExecutorAction;
	}
}
