using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns10;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns4
{
	internal sealed class Class268
	{
		[JsonProperty("state")]
		public Class267 Query { get; set; }

		public Dictionary<string, string> sort { get; set; } = new Dictionary<string, string>
		{
			{
				Class268.getString_0(107439823),
				Class268.getString_0(107439846)
			}
		};

		static Class268()
		{
			Strings.CreateGetStringDelegate(typeof(Class268));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class267 class267_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, string> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
