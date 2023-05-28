using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns17
{
	internal sealed class Class303
	{
		public decimal Min { get; set; }

		public decimal Max { get; set; }

		public Class303()
		{
		}

		public Class303(int int_0, int int_1)
		{
			this.Min = int_0;
			this.Max = int_1;
		}

		public Class303(int int_0)
		{
			this.Min = int_0;
			this.Max = int_0;
		}

		public Class303(decimal decimal_2)
		{
			this.Min = decimal_2;
			this.Max = decimal_2;
		}

		public string ToString()
		{
			string result;
			if (this.Min == this.Max)
			{
				result = this.Min.ToString();
			}
			else
			{
				result = string.Format(Class303.getString_0(107284954), this.Min, this.Max);
			}
			return result;
		}

		static Class303()
		{
			Strings.CreateGetStringDelegate(typeof(Class303));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
