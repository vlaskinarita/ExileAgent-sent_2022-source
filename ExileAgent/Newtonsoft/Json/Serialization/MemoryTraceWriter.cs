using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class MemoryTraceWriter : ITraceWriter
	{
		public TraceLevel LevelFilter { get; set; }

		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		public void Trace(TraceLevel level, string message, Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString(MemoryTraceWriter.getString_0(107296672), CultureInfo.InvariantCulture));
			stringBuilder.Append(MemoryTraceWriter.getString_0(107401727));
			stringBuilder.Append(level.ToString(MemoryTraceWriter.getString_0(107296591)));
			stringBuilder.Append(MemoryTraceWriter.getString_0(107401727));
			stringBuilder.Append(message);
			string item = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(item);
			}
		}

		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		public override string ToString()
		{
			object @lock = this._lock;
			string result;
			lock (@lock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this._traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		static MemoryTraceWriter()
		{
			Strings.CreateGetStringDelegate(typeof(MemoryTraceWriter));
		}

		private readonly Queue<string> _traceMessages;

		private readonly object _lock;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
