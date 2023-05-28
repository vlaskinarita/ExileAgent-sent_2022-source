using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns33;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns36
{
	internal sealed class Class247
	{
		public string WebData { get; set; }

		public Enum7 Response { get; set; }

		public Class247(Enum7 enum7_1)
		{
			this.Response = enum7_1;
			this.WebData = null;
		}

		public Class247(Enum7 enum7_1, string string_1)
		{
			this.Response = enum7_1;
			this.WebData = string_1;
		}

		public string Reason
		{
			get
			{
				string result;
				switch (this.Response)
				{
				case Enum7.const_0:
					result = Class247.getString_0(107441883);
					break;
				case Enum7.const_1:
					result = Class247.getString_0(107441902);
					break;
				case Enum7.const_2:
					result = Class247.getString_0(107441861);
					break;
				default:
					result = string.Empty;
					break;
				}
				return result;
			}
		}

		static Class247()
		{
			Strings.CreateGetStringDelegate(typeof(Class247));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum7 enum7_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
