using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns22
{
	internal sealed class Class246
	{
		public Order Order { get; set; }

		public string Message { get; set; }

		public Class246(Order order_1, string string_1)
		{
			this.Order = order_1;
			this.Message = string_1;
		}

		public string PlayerMessage
		{
			get
			{
				Regex regex = new Regex(Class246.getString_0(107364060));
				Match match = regex.Match(this.Message);
				return match.Groups[2].Value;
			}
		}

		public unsafe int AdditionalItemCount
		{
			get
			{
				void* ptr = stackalloc byte[10];
				Regex regex = new Regex(Class246.getString_0(107449772));
				MatchCollection matchCollection = regex.Matches(this.PlayerMessage);
				((byte*)ptr)[8] = ((matchCollection.Count != 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					((byte*)ptr)[9] = ((!int.TryParse(matchCollection[0].Groups[1].Value, out *(int*)ptr)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						*(int*)((byte*)ptr + 4) = 0;
					}
					else
					{
						*(int*)((byte*)ptr + 4) = *(int*)ptr - this.Order.my_item_amount_floor;
					}
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		static Class246()
		{
			Strings.CreateGetStringDelegate(typeof(Class246));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Order order_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
