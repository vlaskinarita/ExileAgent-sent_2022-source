using System;
using System.Diagnostics;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class DiagnosticsTraceWriter : ITraceWriter
	{
		public TraceLevel LevelFilter { get; set; }

		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				throw new ArgumentOutOfRangeException(DiagnosticsTraceWriter.getString_0(107339258));
			}
		}

		public void Trace(TraceLevel level, string message, Exception ex)
		{
			if (level == TraceLevel.Off)
			{
				return;
			}
			TraceEventCache eventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener obj2 = traceListener;
					lock (obj2)
					{
						traceListener.TraceEvent(eventCache, DiagnosticsTraceWriter.getString_0(107339217), traceEventType, 0, message);
						goto IL_83;
					}
					goto IL_6A;
				}
				goto IL_6A;
				IL_83:
				if (System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_6A:
				traceListener.TraceEvent(eventCache, DiagnosticsTraceWriter.getString_0(107339217), traceEventType, 0, message);
				goto IL_83;
			}
		}

		static DiagnosticsTraceWriter()
		{
			Strings.CreateGetStringDelegate(typeof(DiagnosticsTraceWriter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
