using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns5;
using ns9;
using PoEv2.Models.Query.Filters;

namespace ns8
{
	internal sealed class Class293
	{
		public string type { get; set; }

		public bool disabled { get; set; }

		public List<Class292> filters { get; set; }

		public Class289 value { get; set; }

		public Class293()
		{
		}

		public Class293(StatType statType_0)
		{
			this.type = statType_0.ToString();
			this.filters = new List<Class292>();
		}

		public Class293(StatType statType_0, List<Class292> list_1)
		{
			this.type = statType_0.ToString();
			this.filters = list_1;
		}

		public Class293(StatType statType_0, Class292 class292_0)
		{
			this.type = statType_0.ToString();
			this.filters = new List<Class292>
			{
				class292_0
			};
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Class292> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class289 class289_0;
	}
}
