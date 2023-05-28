using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class AccountInfo
	{
		[JsonProperty("name")]
		public string AccountName { get; set; }

		public bool IsPrivate
		{
			get
			{
				return this.AccountName == null;
			}
		}

		public unsafe int AccountAge
		{
			get
			{
				void* ptr = stackalloc byte[14];
				Match match = Regex.Match(this._foundDate, AccountInfo.getString_0(107284356));
				((byte*)ptr)[12] = ((!match.Success) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					*(int*)((byte*)ptr + 8) = 0;
				}
				else
				{
					string value = match.Groups[1].Value;
					DateTime value2;
					((byte*)ptr)[13] = ((!DateTime.TryParse(value, out value2)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						*(int*)((byte*)ptr + 8) = 0;
					}
					else
					{
						*(double*)ptr = DateTime.Now.Subtract(value2).TotalDays;
						*(int*)((byte*)ptr + 8) = (int)(*(double*)ptr);
					}
				}
				return *(int*)((byte*)ptr + 8);
			}
		}

		static AccountInfo()
		{
			Strings.CreateGetStringDelegate(typeof(AccountInfo));
		}

		[JsonProperty("found")]
		private string _foundDate;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
