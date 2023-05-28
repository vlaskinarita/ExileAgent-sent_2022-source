using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using ns0;
using ns8;
using PoEv2.Models;
using PoEv2.PublicModels;

namespace PoEv2.Classes
{
	public sealed class Characters
	{
		public static List<Character> smethod_0(Dictionary<string, string> dictionary_0)
		{
			string string_ = string.Format(Class103.string_9, Class255.class105_0.method_3(ConfigOptions.AccountName));
			string value = Web.smethod_2(string_, Encoding.UTF8, dictionary_0);
			List<Character> result;
			try
			{
				List<Character> list = JsonConvert.DeserializeObject<List<Character>>(value);
				result = list;
			}
			catch
			{
				result = new List<Character>();
			}
			return result;
		}

		public unsafe static string smethod_1(Dictionary<string, string> dictionary_0)
		{
			void* ptr = stackalloc byte[2];
			Characters.Class177 @class = new Characters.Class177();
			List<Character> source = Characters.smethod_0(dictionary_0);
			@class.character_0 = source.FirstOrDefault(new Func<Character, bool>(Characters.<>c.<>9.method_0));
			*(byte*)ptr = ((@class.character_0 == null) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = string.Empty;
			}
			else
			{
				Characters.int_0 = source.Where(new Func<Character, bool>(Characters.<>c.<>9.method_1)).OrderBy(new Func<Character, string>(Characters.<>c.<>9.method_2)).ToList<Character>().FindIndex(new Predicate<Character>(@class.method_0));
				((byte*)ptr)[1] = ((@class.character_0.league != Class255.class105_0.method_3(ConfigOptions.League)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Characters.int_0 = -1;
				}
				result = @class.character_0.name;
			}
			return result;
		}

		public static IEnumerable<JsonItem> smethod_2(Dictionary<string, string> dictionary_0)
		{
			string string_ = string.Format(Class103.string_10, Class255.class105_0.method_3(ConfigOptions.AccountName), Characters.smethod_1(dictionary_0));
			string value = Web.smethod_2(string_, Encoding.UTF8, dictionary_0);
			Characters.Class176 @class = JsonConvert.DeserializeObject<Characters.Class176>(value);
			IEnumerable<JsonItem> result;
			if (@class.list_0 == null)
			{
				IEnumerable<JsonItem> enumerable = new List<JsonItem>();
				result = enumerable;
			}
			else
			{
				result = @class.list_0.Where(new Func<JsonItem, bool>(Characters.<>c.<>9.method_3));
			}
			return result;
		}

		public static int int_0 = 0;

		private sealed class Class176
		{
			public List<JsonItem> list_0;
		}

		[CompilerGenerated]
		private sealed class Class177
		{
			internal bool method_0(Character character_1)
			{
				return character_1 == this.character_0;
			}

			public Character character_0;
		}
	}
}
