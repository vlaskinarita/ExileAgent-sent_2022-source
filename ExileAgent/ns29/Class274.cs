using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns3;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns29
{
	internal sealed class Class274
	{
		public Class274()
		{
		}

		public Class274(Class273 class273_1)
		{
			this.query = class273_1;
		}

		public Class273 query { get; set; } = new Class273();

		public Dictionary<string, string> sort { get; set; } = new Dictionary<string, string>
		{
			{
				Class274.getString_0(107439848),
				Class274.getString_0(107439871)
			}
		};

		static Class274()
		{
			Strings.CreateGetStringDelegate(typeof(Class274));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class273 class273_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, string> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
