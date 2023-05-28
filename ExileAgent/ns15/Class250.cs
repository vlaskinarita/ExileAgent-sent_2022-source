using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ns15
{
	internal sealed class Class250
	{
		[JsonProperty("min")]
		public double Min { get; set; }

		[JsonProperty("max")]
		public double Max { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		public double Median
		{
			get
			{
				return Math.Round((this.Min + this.Max) / 2.0, 1);
			}
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;
	}
}
