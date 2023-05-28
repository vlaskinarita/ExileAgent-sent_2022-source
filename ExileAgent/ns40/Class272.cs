using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns12;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns40
{
	internal sealed class Class272
	{
		public Class272()
		{
		}

		public Class272(Class270 class270_1)
		{
			this.query = class270_1;
		}

		public Class270 query { get; set; } = new Class270();

		public Dictionary<string, string> sort { get; set; } = new Dictionary<string, string>
		{
			{
				Class272.getString_0(107439599),
				Class272.getString_0(107439860)
			}
		};

		public string engine { get; } = Class272.getString_0(107439855);

		static Class272()
		{
			Strings.CreateGetStringDelegate(typeof(Class272));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class270 class270_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, string> dictionary_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
