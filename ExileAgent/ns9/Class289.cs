using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ns9
{
	internal sealed class Class289
	{
		public double? min { get; set; }

		public double? max { get; set; }

		public string option { get; set; }

		public double? weight { get; set; }

		public Class289()
		{
		}

		public Class289(double double_0, double double_1)
		{
			this.min = new double?(double_0);
			this.max = new double?(double_1);
		}

		public Class289(double double_0)
		{
			this.min = new double?(double_0);
			this.max = new double?(double_0);
		}

		public Class289(string string_1)
		{
			this.option = string_1;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double? nullable_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double? nullable_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double? nullable_2;
	}
}
