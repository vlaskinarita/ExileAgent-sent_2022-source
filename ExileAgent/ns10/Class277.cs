using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ns10
{
	internal class Class277
	{
		public int? min { get; set; }

		public int? max { get; set; }

		public string option { get; set; }

		public Class277()
		{
		}

		public Class277(int int_0, int int_1)
		{
			this.min = new int?(int_0);
			this.max = new int?(int_1);
		}

		public Class277(int int_0)
		{
			this.min = new int?(int_0);
			this.max = new int?(int_0);
		}

		public Class277(string string_1)
		{
			this.option = string_1;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int? nullable_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int? nullable_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;
	}
}
