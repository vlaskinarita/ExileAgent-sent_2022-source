using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ns27
{
	internal sealed class Class140
	{
		[JsonProperty("connected")]
		public bool Connected { get; set; }

		[JsonProperty("botStarted")]
		public bool BotStarted { get; set; }

		[JsonProperty("tradesRequested")]
		public int TradesRequested { get; set; }

		[JsonProperty("tradesFailed")]
		public int TradesFailed { get; set; }

		[JsonProperty("tradesCompleted")]
		public int TradesCompleted { get; set; }

		[JsonProperty("botStartedTime")]
		public int BotStartedTime { get; set; }

		[JsonProperty("botPaused")]
		public bool BotPaused { get; set; }

		[JsonProperty("botStarting")]
		public bool BotStarting { get; set; }

		[JsonProperty("accountName")]
		public string AccountName { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;
	}
}
