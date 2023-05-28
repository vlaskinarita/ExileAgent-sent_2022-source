using System;

namespace WebSocketSharp
{
	public sealed class ErrorEventArgs : EventArgs
	{
		internal ErrorEventArgs(string message) : this(message, null)
		{
		}

		internal ErrorEventArgs(string message, Exception exception)
		{
			this._message = message;
			this._exception = exception;
		}

		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		public string Message
		{
			get
			{
				return this._message;
			}
		}

		private Exception _exception;

		private string _message;
	}
}
