using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns41;
using ns9;

namespace ns5
{
	internal sealed class Class292
	{
		public string id { get; set; }

		public Class289 value { get; set; }

		public Class287 option { get; set; }

		public bool disabled { get; set; }

		public Class292()
		{
		}

		public Class292(string string_1, Class289 class289_1, bool bool_1)
		{
			this.id = string_1;
			this.value = class289_1;
			this.disabled = bool_1;
		}

		public Class292(string string_1, Class287 class287_1, bool bool_1)
		{
			this.id = string_1;
			this.option = class287_1;
			this.disabled = bool_1;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class289 class289_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class287 class287_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;
	}
}
