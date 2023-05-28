using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns24
{
	internal sealed class Class252
	{
		public decimal Amount { get; set; }

		public string CurrencyId { get; set; }

		public Class252()
		{
			this.Amount = 0m;
			this.CurrencyId = string.Empty;
		}

		public Class252(decimal decimal_1, string string_1)
		{
			this.Amount = decimal_1;
			this.CurrencyId = string_1;
		}

		public string ToString()
		{
			string result;
			if (this.Amount >= 1m)
			{
				result = string.Format(Class252.getString_0(107464521), this.Amount, this.CurrencyId);
			}
			else
			{
				result = string.Format(Class252.getString_0(107442198), 1m / this.Amount, this.CurrencyId);
			}
			return result;
		}

		public unsafe static Class252 smethod_0(string string_1, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[5];
			Regex regex = new Regex(Class252.getString_0(107442141));
			*(byte*)ptr = (string.IsNullOrEmpty(string_1) ? 1 : 0);
			Class252 result;
			if (*(sbyte*)ptr != 0)
			{
				result = new Class252(0m, string.Empty);
			}
			else
			{
				Match match = regex.Match(string_1);
				((byte*)ptr)[1] = ((!match.Success) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = new Class252(0m, string.Empty);
				}
				else
				{
					((byte*)ptr)[2] = (string.IsNullOrEmpty(match.Groups[1].Value) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = new Class252(0m, string.Empty);
					}
					else
					{
						decimal decimal_;
						try
						{
							((byte*)ptr)[3] = (match.Groups[1].Value.Contains(Class252.getString_0(107373105)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								((byte*)ptr)[4] = (bool_0 ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 4) != 0)
								{
									decimal_ = Util.smethod_6(match.Groups[1].Value.Split(new char[]
									{
										'/'
									})[1]) / Util.smethod_6(match.Groups[1].Value.Split(new char[]
									{
										'/'
									})[0]);
								}
								else
								{
									decimal_ = Util.smethod_6(match.Groups[1].Value.Split(new char[]
									{
										'/'
									})[0]) / Util.smethod_6(match.Groups[1].Value.Split(new char[]
									{
										'/'
									})[1]);
								}
							}
							else
							{
								decimal_ = Util.smethod_6(match.Groups[1].Value);
							}
						}
						catch (SystemException)
						{
							return new Class252(0m, string.Empty);
						}
						string id = API.smethod_7(match.Groups[2].Value).Id;
						result = new Class252(decimal_, id);
					}
				}
			}
			return result;
		}

		public static string smethod_1(JsonItem jsonItem_0, JsonTab jsonTab_0)
		{
			return Class252.smethod_2(jsonItem_0.note, jsonTab_0.n);
		}

		public static string smethod_2(string string_1, string string_2)
		{
			string result = null;
			if (!string.IsNullOrEmpty(string_1))
			{
				result = string_1;
			}
			else if (string_2.Contains(Class252.getString_0(107442132)) || string_2.Contains(Class252.getString_0(107442091)))
			{
				result = string_2;
			}
			return result;
		}

		public static bool smethod_3(string string_1, string string_2)
		{
			Class252 @class = Class252.smethod_0(string_1, false);
			Class252 class2 = Class252.smethod_0(string_2, false);
			return !(@class.Amount == 0m) && !(class2.Amount == 0m) && Math.Round(@class.Amount, 1) == Math.Round(class2.Amount, 1) && @class.CurrencyId == class2.CurrencyId;
		}

		public static string smethod_4(Order order_0)
		{
			string id = API.smethod_7(order_0.player_item_name).Id;
			decimal player_item_amount = order_0.player_item_amount;
			decimal my_item_amount = order_0.my_item_amount;
			string result;
			if (my_item_amount == 1m)
			{
				result = string.Format(Class252.getString_0(107464521), player_item_amount, id);
			}
			else
			{
				result = string.Format(Class252.getString_0(107442082), player_item_amount, my_item_amount, id);
			}
			return result;
		}

		static Class252()
		{
			Strings.CreateGetStringDelegate(typeof(Class252));
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
