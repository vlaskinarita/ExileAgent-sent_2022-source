using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns17;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns29
{
	internal sealed class Class143
	{
		[JsonProperty("ok")]
		public bool Success { get; set; }

		[JsonProperty("code")]
		public Enum4 Reason { get; set; }

		[JsonProperty("features")]
		public ushort Features { get; set; }

		public static string smethod_0(Enum4 enum4_1)
		{
			string result;
			if (enum4_1 != Enum4.const_1)
			{
				if (enum4_1 != Enum4.const_2)
				{
					result = string.Empty;
				}
				else
				{
					result = Class143.getString_0(107370919);
				}
			}
			else
			{
				result = Class143.getString_0(107370822);
			}
			return result;
		}

		static Class143()
		{
			Strings.CreateGetStringDelegate(typeof(Class143));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum4 enum4_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ushort ushort_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
