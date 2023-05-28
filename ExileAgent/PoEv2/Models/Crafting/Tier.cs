using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns17;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Crafting
{
	public sealed class Tier
	{
		public int Rank { get; set; }

		public Class303 Min { get; set; }

		public Class303 Max { get; set; }

		public string ToString()
		{
			string result;
			if (this.Min.Min == this.Max.Min)
			{
				result = string.Format(Tier.getString_0(107370167), this.Min.Min, this.Rank);
			}
			else
			{
				result = string.Format(Tier.getString_0(107284388), this.Min, this.Max, this.Rank);
			}
			return result;
		}

		public Tier()
		{
			this.Min = new Class303(0);
			this.Max = new Class303(0);
		}

		static Tier()
		{
			Strings.CreateGetStringDelegate(typeof(Tier));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class303 class303_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class303 class303_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
