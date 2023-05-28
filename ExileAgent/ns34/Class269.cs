using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ns34
{
	internal sealed class Class269
	{
		public string discriminator { get; set; }

		public string option { get; set; }

		public Class269()
		{
		}

		public Class269(string string_2, string string_3)
		{
			this.discriminator = string_2;
			this.option = string_3;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;
	}
}
