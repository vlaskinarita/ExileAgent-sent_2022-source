using System;

namespace SuperSocket.ClientEngine
{
	public sealed class ErrorEventArgs : EventArgs
	{
		public Exception Exception { get; private set; }

		public ErrorEventArgs(Exception exception)
		{
			this.Exception = exception;
		}
	}
}
