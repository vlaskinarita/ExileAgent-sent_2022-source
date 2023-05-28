using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns34
{
	internal sealed class Class249
	{
		public decimal Amount { get; set; } = 0m;

		public string Currency { get; set; } = Class249.getString_0(107407299);

		public Class249()
		{
		}

		public Class249(decimal decimal_1)
		{
			this.Amount = decimal_1;
		}

		public Class249(decimal decimal_1, string string_1)
		{
			this.Amount = decimal_1;
			this.Currency = string_1;
		}

		public string ToString()
		{
			return string.Format(Class249.getString_0(107396187), this.Amount, this.Currency);
		}

		public bool Equals(object obj)
		{
			bool result;
			if (!(obj is Class249))
			{
				result = false;
			}
			else
			{
				Class249 @class = (Class249)obj;
				result = (this.Amount == @class.Amount && this.Currency == @class.Currency);
			}
			return result;
		}

		public int GetHashCode()
		{
			return this.Amount.GetHashCode() + this.Currency.GetHashCode();
		}

		static Class249()
		{
			Strings.CreateGetStringDelegate(typeof(Class249));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
