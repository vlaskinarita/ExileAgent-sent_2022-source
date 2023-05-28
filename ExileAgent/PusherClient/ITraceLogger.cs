using System;

namespace PusherClient
{
	public interface ITraceLogger
	{
		void TraceInformation(string message);

		void TraceWarning(string message);

		void TraceError(string message);
	}
}
