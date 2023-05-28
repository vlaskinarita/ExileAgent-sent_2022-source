using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns41;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns12
{
	internal sealed class Class270
	{
		public Class287 status { get; set; } = new Class287(Class270.getString_0(107453281));

		public List<string> have { get; set; } = new List<string>();

		public List<string> want { get; set; } = new List<string>();

		public int minimum { get; set; } = 1;

		public Class270()
		{
		}

		public Class270(string string_0, string string_1, int int_1 = 1)
		{
			this.have.Add(string_1);
			this.want.Add(string_0);
			this.minimum = int_1;
		}

		static Class270()
		{
			Strings.CreateGetStringDelegate(typeof(Class270));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class287 class287_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
