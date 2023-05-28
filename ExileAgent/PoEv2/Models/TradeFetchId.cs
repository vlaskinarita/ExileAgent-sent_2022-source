using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PoEv2.Models
{
	public sealed class TradeFetchId
	{
		public string Query { get; set; }

		public string Id { get; set; }

		public TradeFetchId(string query, string id)
		{
			this.Query = query;
			this.Id = id;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;
	}
}
