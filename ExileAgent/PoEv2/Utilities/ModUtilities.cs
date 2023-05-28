using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ns17;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Utilities
{
	public static class ModUtilities
	{
		public static string smethod_0(string string_1)
		{
			string_1 = string_1.Replace(ModUtilities.getString_0(107369718), ModUtilities.getString_0(107397228)).Replace(ModUtilities.getString_0(107369173), ModUtilities.getString_0(107397228)).Replace(ModUtilities.getString_0(107369128), ModUtilities.getString_0(107369147));
			string_1 = Regex.Replace(string_1, ModUtilities.getString_0(107369142), ModUtilities.getString_0(107369097));
			return string_1;
		}

		public static string smethod_1(string string_1)
		{
			string input = Regex.Replace(string_1, ModUtilities.getString_0(107369092), ModUtilities.getString_0(107398421));
			return Regex.Replace(input, ModUtilities.getString_0(107369035), ModUtilities.getString_0(107398421));
		}

		public static string smethod_2(string string_1)
		{
			MatchCollection matchCollection = Regex.Matches(ModUtilities.smethod_0(string_1), ModUtilities.getString_0(107368994));
			string result;
			if (matchCollection.Count == 0)
			{
				result = ModUtilities.getString_0(107369417);
			}
			else
			{
				result = string.Join(ModUtilities.getString_0(107393953), matchCollection.Cast<Match>().Select(new Func<Match, string>(ModUtilities.<>c.<>9.method_0)));
			}
			return result;
		}

		public static IEnumerable<Tier> smethod_3(Mods mods_0, Mod mod_0)
		{
			ModUtilities.Class147 @class = new ModUtilities.Class147();
			@class.mod_0 = mod_0;
			List<Tier> list = new List<Tier>();
			int num = 0;
			IEnumerable<Mod> enumerable = mods_0.ModList.Where(new Func<Mod, bool>(@class.method_0));
			foreach (Mod mod_ in enumerable)
			{
				Tier tier = ModUtilities.smethod_4(mod_);
				tier.Rank = enumerable.Count<Mod>() - num;
				num++;
				if (!(tier.Max.Min == 0m) || !(tier.Max.Max == 0m))
				{
					list.Add(tier);
				}
			}
			return list.OrderByDescending(new Func<Tier, int>(ModUtilities.<>c.<>9.method_1));
		}

		private unsafe static Tier smethod_4(Mod mod_0)
		{
			void* ptr = stackalloc byte[6];
			string input = ModUtilities.smethod_0(mod_0.InGameText).smethod_18(ModUtilities.getString_0(107369412))[0];
			MatchCollection matchCollection = Regex.Matches(input, ModUtilities.getString_0(107368994));
			*(byte*)ptr = ((matchCollection.Count == 0) ? 1 : 0);
			Tier result;
			if (*(sbyte*)ptr != 0)
			{
				result = new Tier();
			}
			else
			{
				Tier tier = new Tier();
				((byte*)ptr)[1] = ((matchCollection.Count == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = (matchCollection[0].Value.Contains(ModUtilities.getString_0(107369147)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						string[] array = matchCollection[0].Value.Split(new char[]
						{
							'-'
						});
						tier.Min = new Class303(Util.smethod_6(array[0]));
						tier.Max = new Class303(Util.smethod_6(array[1]));
					}
					else
					{
						tier.Min = new Class303(Util.smethod_6(matchCollection[0].Value));
						tier.Max = new Class303(Util.smethod_6(matchCollection[0].Value));
					}
				}
				((byte*)ptr)[3] = ((matchCollection.Count == 2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((byte*)ptr)[4] = (matchCollection[0].Value.Contains(ModUtilities.getString_0(107369147)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						string[] array2 = matchCollection[0].Value.Split(new char[]
						{
							'-'
						});
						tier.Min = new Class303(int.Parse(array2[0]), int.Parse(array2[1]));
					}
					else
					{
						tier.Min = new Class303(int.Parse(matchCollection[0].Value));
					}
					((byte*)ptr)[5] = (matchCollection[1].Value.Contains(ModUtilities.getString_0(107369147)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						string[] array3 = matchCollection[1].Value.Split(new char[]
						{
							'-'
						});
						tier.Max = new Class303(int.Parse(array3[0]), int.Parse(array3[1]));
					}
					else
					{
						tier.Max = new Class303(int.Parse(matchCollection[1].Value));
					}
				}
				result = tier;
			}
			return result;
		}

		public unsafe static bool smethod_5(Mod mod_0, string string_1)
		{
			void* ptr = stackalloc byte[9];
			((byte*)ptr)[4] = ((mod_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = 1;
			}
			else
			{
				Item item = new Item(string_1);
				((byte*)ptr)[6] = ((!mod_0.MultiLine) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) == 0)
				{
					((byte*)ptr)[7] = 1;
					string[] regexSplit = mod_0.RegexSplit;
					*(int*)ptr = 0;
					while (*(int*)ptr < regexSplit.Length)
					{
						string pattern = regexSplit[*(int*)ptr];
						((byte*)ptr)[8] = ((!Regex.Match(string_1, pattern).Success) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							((byte*)ptr)[7] = 0;
							IL_97:
							((byte*)ptr)[5] = (byte)(*(sbyte*)((byte*)ptr + 7));
							goto IL_9F;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					goto IL_97;
				}
				((byte*)ptr)[5] = (Regex.Match(item.ExplicitMods, mod_0.Regex).Success ? 1 : 0);
			}
			IL_9F:
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public unsafe static bool smethod_6(Mod mod_0, Tier tier_0, string string_1)
		{
			void* ptr = stackalloc byte[13];
			List<string> list = new List<string>
			{
				ModUtilities.getString_0(107369439)
			};
			((byte*)ptr)[5] = ((mod_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				((byte*)ptr)[6] = 1;
			}
			else
			{
				Item item = new Item(string_1);
				Match match = Regex.Match(item.ExplicitMods, mod_0.RegexSplit[0]);
				((byte*)ptr)[7] = ((!match.Success) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					((byte*)ptr)[6] = 0;
				}
				else
				{
					((byte*)ptr)[8] = ((tier_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						((byte*)ptr)[6] = 1;
					}
					else
					{
						((byte*)ptr)[4] = 0;
						string[] regexSplit = mod_0.RegexSplit;
						*(int*)ptr = 0;
						while (*(int*)ptr < regexSplit.Length)
						{
							string text = regexSplit[*(int*)ptr];
							foreach (string value in list)
							{
								((byte*)ptr)[9] = (text.EndsWith(value) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									((byte*)ptr)[4] = 1;
								}
							}
							*(int*)ptr = *(int*)ptr + 1;
						}
						((byte*)ptr)[10] = ((tier_0.Max.Min == tier_0.Max.Max) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							((byte*)ptr)[11] = (byte)(*(sbyte*)((byte*)ptr + 4));
							if (*(sbyte*)((byte*)ptr + 11) != 0)
							{
								((byte*)ptr)[6] = ((tier_0.Max.Max >= Util.smethod_6(match.Groups[1].Value)) ? 1 : 0);
							}
							else
							{
								((byte*)ptr)[6] = ((tier_0.Min.Min <= Util.smethod_6(match.Groups[1].Value)) ? 1 : 0);
							}
						}
						else
						{
							((byte*)ptr)[12] = (byte)(*(sbyte*)((byte*)ptr + 4));
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								((byte*)ptr)[6] = ((tier_0.Max.Max >= Util.smethod_6(match.Groups[2].Value)) ? 1 : 0);
							}
							else
							{
								((byte*)ptr)[6] = ((tier_0.Max.Min <= Util.smethod_6(match.Groups[2].Value)) ? 1 : 0);
							}
						}
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 6) != 0;
		}

		public static string smethod_7(string string_1)
		{
			List<string> list = new List<string>
			{
				ModUtilities.getString_0(107369382),
				ModUtilities.getString_0(107369345)
			};
			foreach (string value in list)
			{
				if (string_1.Contains(value))
				{
					string[] array = string_1.smethod_18(ModUtilities.getString_0(107369328));
					return array[0];
				}
			}
			return string_1;
		}

		static ModUtilities()
		{
			Strings.CreateGetStringDelegate(typeof(ModUtilities));
		}

		private const string string_0 = "(?!1 Added Passive Skill)(\\d+(?:.\\d+)?(?:-\\d+(?:.\\d+)?)?)(?! tier)";

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class147
		{
			internal bool method_0(Mod mod_1)
			{
				return mod_1.ModGroup == this.mod_0.ModGroup;
			}

			public Mod mod_0;
		}
	}
}
