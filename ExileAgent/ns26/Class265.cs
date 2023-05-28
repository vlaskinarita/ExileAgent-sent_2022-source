using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using PoEv2.Models.Flipping;

namespace ns26
{
	internal sealed class Class265
	{
		[JsonProperty("error")]
		public bool Error { get; set; }

		[JsonProperty("bad-proxies")]
		public int BadProxies { get; set; }

		[JsonProperty("proxies-used")]
		public int ProxiesUsed { get; set; }

		[JsonProperty("render")]
		public string TotalTime { get; set; }

		[JsonProperty("bytes-used")]
		public int BytesUsed { get; set; }

		[JsonProperty("results")]
		public List<FlippingListJsonItem> FlippingList { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<FlippingListJsonItem> list_0;
	}
}
