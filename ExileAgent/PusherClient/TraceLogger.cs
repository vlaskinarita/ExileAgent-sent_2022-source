using System;
using System.Diagnostics;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class TraceLogger : ITraceLogger
	{
		private static TraceSource Trace { get; }

		public void TraceError(string message)
		{
			if (message != null)
			{
				TraceLogger.Trace.TraceEvent(TraceEventType.Error, -1, message);
			}
		}

		public void TraceInformation(string message)
		{
			if (message != null)
			{
				TraceLogger.Trace.TraceEvent(TraceEventType.Information, 0, message);
			}
		}

		public void TraceWarning(string message)
		{
			if (message != null)
			{
				TraceLogger.Trace.TraceEvent(TraceEventType.Warning, 1, message);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static TraceLogger()
		{
			Strings.CreateGetStringDelegate(typeof(TraceLogger));
			TraceLogger.Trace = new TraceSource(TraceLogger.getString_0(107309700));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
