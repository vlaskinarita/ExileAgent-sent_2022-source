using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebSocketSharp.Net
{
	[Serializable]
	public sealed class HttpListenerException : Win32Exception
	{
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		public HttpListenerException()
		{
		}

		public HttpListenerException(int errorCode) : base(errorCode)
		{
		}

		public HttpListenerException(int errorCode, string message) : base(errorCode, message)
		{
		}

		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
