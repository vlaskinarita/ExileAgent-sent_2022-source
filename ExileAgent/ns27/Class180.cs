using System;
using System.Collections.Generic;
using ns0;
using ns1;
using ns10;
using ns17;
using ns2;
using ns20;
using ns21;
using ns29;
using ns3;
using ns33;
using ns34;
using ns38;
using ns40;
using ns41;
using ns5;
using ns8;
using ns9;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Models.Query.Filters;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns27
{
	internal static class Class180
	{
		public unsafe static Class274 smethod_0(JsonItem jsonItem_0, string string_0)
		{
			void* ptr = stackalloc byte[7];
			Class273 @class = new Class273
			{
				status = new Class287(Class180.getString_0(107452480)),
				name = (string.IsNullOrEmpty(jsonItem_0.name) ? null : jsonItem_0.name),
				type = jsonItem_0.typeLine
			};
			if (string_0 != Class180.getString_0(107406928) && string_0 != Class180.getString_0(107352056) && !string_0.smethod_25())
			{
				@class.filters = new Class288();
				@class.filters.TradeFilters = new Class295();
				@class.filters.TradeFilters.filters.price = new Class289(API.smethod_7(string_0).Id);
			}
			else
			{
				*(byte*)ptr = (Class255.class105_0.method_4(ConfigOptions.SkipPerandusCoins) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					@class.filters = new Class288();
					@class.filters.TradeFilters = new Class295();
					@class.filters.TradeFilters.filters.price = new Class289
					{
						min = new double?(0.1)
					};
				}
			}
			((byte*)ptr)[1] = (jsonItem_0.IsMap ? 1 : 0);
			Class274 result;
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				result = Class180.smethod_1(jsonItem_0, @class);
			}
			else
			{
				((byte*)ptr)[2] = (jsonItem_0.IsClusterJewel ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					result = Class180.smethod_2(jsonItem_0, @class);
				}
				else
				{
					((byte*)ptr)[3] = ((jsonItem_0.ItemRarity == ItemRarity.Gem) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						result = Class180.smethod_3(jsonItem_0, @class);
					}
					else
					{
						ItemRarity itemRarity = jsonItem_0.ItemRarity;
						ItemRarity itemRarity2 = itemRarity;
						if (itemRarity2 > ItemRarity.Unique)
						{
							((byte*)ptr)[4] = ((jsonItem_0.ItemRarity == ItemRarity.Prophecy) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = Class180.smethod_5(jsonItem_0, @class);
							}
							else
							{
								((byte*)ptr)[5] = (jsonItem_0.IsSeed ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 5) != 0)
								{
									result = Class180.smethod_7(jsonItem_0, @class);
								}
								else
								{
									((byte*)ptr)[6] = ((jsonItem_0.ItemRarity == ItemRarity.Currency) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										result = Class180.smethod_4(jsonItem_0, @class);
									}
									else
									{
										result = new Class274(@class);
									}
								}
							}
						}
						else
						{
							result = Class180.smethod_6(jsonItem_0, @class);
						}
					}
				}
			}
			return result;
		}

		private unsafe static Class274 smethod_1(JsonItem jsonItem_0, Class273 class273_0)
		{
			void* ptr = stackalloc byte[32];
			class273_0.name = null;
			class273_0.type = API.smethod_8(jsonItem_0).Text.Replace(Class180.getString_0(107367726), Class180.getString_0(107397402)).Replace(Class180.getString_0(107452439), Class180.getString_0(107397402));
			((byte*)ptr)[20] = ((class273_0.filters == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) != 0)
			{
				class273_0.filters = new Class288();
			}
			class273_0.filters.MapFilters = new Class282();
			class273_0.filters.MiscFilters = new Class284();
			class273_0.filters.TypeFilters = new Class297();
			class273_0.filters.MapFilters.disabled = false;
			class273_0.filters.MapFilters.filters.MapTier = new Class289((double)jsonItem_0.MapTier);
			((byte*)ptr)[21] = ((!jsonItem_0.IsUnique) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				Class283 filters = class273_0.filters.MiscFilters.filters;
				((byte*)ptr)[22] = (jsonItem_0.corrupted ? 1 : 0);
				filters.corrupted = new Class287(((bool*)((byte*)ptr + 22))->ToString());
			}
			((byte*)ptr)[23] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107452439)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 23) != 0)
			{
				class273_0.type = null;
				class273_0.filters.MapFilters.filters.Blighted = new Class287(Class180.getString_0(107452458));
			}
			((byte*)ptr)[24] = ((jsonItem_0.ItemRarity == ItemRarity.Unique) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 24) != 0)
			{
				class273_0.filters.TypeFilters.filters.rarity = new Class287(Class180.getString_0(107452449));
			}
			else
			{
				class273_0.filters.TypeFilters.filters.rarity = new Class287(Class180.getString_0(107452408));
			}
			((byte*)ptr)[25] = ((jsonItem_0.implicitMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 25) != 0)
			{
				class273_0.stats = new List<Class293>();
				string text = string.Join(Class180.getString_0(107393026), jsonItem_0.implicitMods).ToLower();
				((byte*)ptr)[26] = (text.Contains(Class180.getString_0(107452427)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 26) != 0)
				{
					class273_0.stats.Add(new Class293(StatType.and, new Class292(Class180.getString_0(107452378), new Class289(Class180.getString_0(107369591)), false)));
					if (!class273_0.type.Contains(Class180.getString_0(107452345)) && !class273_0.type.Contains(Class180.getString_0(107452336)) && !class273_0.type.Contains(Class180.getString_0(107452355)) && !class273_0.type.Contains(Class180.getString_0(107452310)))
					{
						class273_0.type = null;
					}
				}
				((byte*)ptr)[27] = (text.Contains(Class180.getString_0(107452329)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 27) != 0)
				{
					class273_0.stats.Add(new Class293(StatType.and, new Class292(Class180.getString_0(107452378), new Class289(Class180.getString_0(107390954)), false)));
				}
				((byte*)ptr)[28] = (text.Contains(Class180.getString_0(107451772)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 28) != 0)
				{
					class273_0.type = null;
					string[] array = new string[]
					{
						Class180.getString_0(107451787),
						Class180.getString_0(107451738),
						Class180.getString_0(107451749),
						Class180.getString_0(107451696)
					};
					*(int*)ptr = 1;
					string[] array2 = array;
					*(int*)((byte*)ptr + 4) = 0;
					while (*(int*)((byte*)ptr + 4) < array2.Length)
					{
						string value = array2[*(int*)((byte*)ptr + 4)];
						((byte*)ptr)[29] = (text.Contains(value) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 29) != 0)
						{
							*(int*)ptr = Array.IndexOf<string>(array, value);
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					List<Class292> filters2 = class273_0.stats[0].filters;
					string string_ = Class180.getString_0(107451711);
					*(int*)((byte*)ptr + 8) = *(int*)ptr + 1;
					filters2.Add(new Class292(string_, new Class289(((int*)((byte*)ptr + 8))->ToString()), false));
				}
				((byte*)ptr)[30] = (text.Contains(Class180.getString_0(107451678)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 30) != 0)
				{
					string[] array3 = new string[]
					{
						Class180.getString_0(107451633),
						Class180.getString_0(107451656),
						Class180.getString_0(107451611),
						Class180.getString_0(107451598)
					};
					*(int*)((byte*)ptr + 12) = 1;
					string[] array4 = array3;
					*(int*)((byte*)ptr + 16) = 0;
					while (*(int*)((byte*)ptr + 16) < array4.Length)
					{
						string value2 = array4[*(int*)((byte*)ptr + 16)];
						((byte*)ptr)[31] = (text.Contains(value2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 31) != 0)
						{
							*(int*)((byte*)ptr + 12) = Array.IndexOf<string>(array3, value2);
						}
						*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
					}
					List<Class292> filters3 = class273_0.stats[0].filters;
					string string_2 = Class180.getString_0(107451621);
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 12) + 1;
					filters3.Add(new Class292(string_2, new Class289(((int*)((byte*)ptr + 8))->ToString()), false));
				}
			}
			return new Class274(class273_0);
		}

		private static Class274 smethod_2(JsonItem jsonItem_0, Class273 class273_0)
		{
			return Class180.smethod_6(jsonItem_0, class273_0);
		}

		private unsafe static Class274 smethod_3(JsonItem jsonItem_0, Class273 class273_0)
		{
			void* ptr = stackalloc byte[19];
			((byte*)ptr)[4] = ((jsonItem_0.hybrid != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				class273_0.type = jsonItem_0.hybrid.baseTypeName;
			}
			((byte*)ptr)[5] = ((class273_0.filters == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				class273_0.filters = new Class288();
			}
			class273_0.filters.MiscFilters = new Class284();
			class273_0.filters.MiscFilters.disabled = false;
			class273_0.filters.MiscFilters.filters.GemLevel = new Class277(jsonItem_0.GemLevel);
			Class283 filters = class273_0.filters.MiscFilters.filters;
			((byte*)ptr)[6] = (jsonItem_0.corrupted ? 1 : 0);
			filters.corrupted = new Class287(((bool*)((byte*)ptr + 6))->ToString());
			class273_0.stats = new List<Class293>();
			class273_0.stats.Add(new Class293(StatType.and));
			((byte*)ptr)[7] = (jsonItem_0.IsAlternateGem ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 7) != 0)
			{
				*(int*)ptr = 0;
				((byte*)ptr)[8] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107451588)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)ptr = 1;
				}
				else
				{
					((byte*)ptr)[9] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107451543)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						*(int*)ptr = 2;
					}
					else
					{
						((byte*)ptr)[10] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107451562)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							*(int*)ptr = 3;
						}
					}
				}
				class273_0.type = jsonItem_0.typeLine.Substring(jsonItem_0.typeLine.IndexOf(' ') + 1);
				class273_0.filters.MiscFilters.filters.GemAlternateQuality = new Class287(((int*)ptr)->ToString());
			}
			((byte*)ptr)[11] = ((!jsonItem_0.IsSpecialGem) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 11) != 0)
			{
				((byte*)ptr)[12] = ((jsonItem_0.GemLevel < 15) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					class273_0.filters.MiscFilters.filters.GemLevel.min = new int?(jsonItem_0.GemLevel);
					if (jsonItem_0.Quality >= 15 && jsonItem_0.Quality < 19)
					{
						class273_0.filters.MiscFilters.filters.quality = new Class277
						{
							min = new int?(jsonItem_0.Quality)
						};
					}
					else
					{
						((byte*)ptr)[13] = ((jsonItem_0.Quality == 20) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							class273_0.filters.MiscFilters.filters.quality = new Class277
							{
								min = new int?(jsonItem_0.Quality)
							};
						}
					}
				}
				else if (jsonItem_0.GemLevel >= 15 && jsonItem_0.GemLevel < 19)
				{
					class273_0.filters.MiscFilters.filters.GemLevel.min = new int?(jsonItem_0.GemLevel);
					if (jsonItem_0.Quality >= 15 && jsonItem_0.Quality < 19)
					{
						class273_0.filters.MiscFilters.filters.quality = new Class277
						{
							min = new int?(jsonItem_0.Quality)
						};
					}
					else
					{
						((byte*)ptr)[14] = ((jsonItem_0.Quality == 20) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 14) != 0)
						{
							class273_0.filters.MiscFilters.filters.quality = new Class277
							{
								min = new int?(jsonItem_0.Quality)
							};
						}
					}
				}
				else
				{
					((byte*)ptr)[15] = ((jsonItem_0.GemLevel >= 20) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						class273_0.filters.MiscFilters.filters.GemLevel.min = new int?(jsonItem_0.GemLevel);
						if (jsonItem_0.Quality >= 15 && jsonItem_0.Quality < 19)
						{
							class273_0.filters.MiscFilters.filters.quality = new Class277
							{
								min = new int?(jsonItem_0.Quality)
							};
						}
						else
						{
							((byte*)ptr)[16] = ((jsonItem_0.Quality >= 20) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								class273_0.filters.MiscFilters.filters.quality = new Class277
								{
									min = new int?(jsonItem_0.Quality)
								};
							}
						}
					}
				}
			}
			else
			{
				((byte*)ptr)[17] = ((jsonItem_0.GemLevel < 3) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					((byte*)ptr)[18] = ((!jsonItem_0.corrupted) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						class273_0.filters.MiscFilters.filters.GemLevel.min = new int?(jsonItem_0.GemLevel);
						class273_0.filters.MiscFilters.filters.quality = new Class277
						{
							min = new int?(jsonItem_0.Quality)
						};
					}
					else
					{
						class273_0.filters.MiscFilters.filters.GemLevel.min = new int?(jsonItem_0.GemLevel);
					}
				}
			}
			return new Class274(class273_0);
		}

		private static Class274 smethod_4(JsonItem jsonItem_0, Class273 class273_0)
		{
			return new Class274(class273_0);
		}

		private unsafe static Class274 smethod_5(JsonItem jsonItem_0, Class273 class273_0)
		{
			void* ptr = stackalloc byte[6];
			class273_0.name = jsonItem_0.typeLine;
			class273_0.type = Class180.getString_0(107352631);
			*(byte*)ptr = ((jsonItem_0.typeLine == Class180.getString_0(107370103)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				class273_0.name = null;
				class273_0.type = null;
				string string_ = string.Empty;
				((byte*)ptr)[1] = (jsonItem_0.prophecyText.Contains(Class180.getString_0(107365871)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					string_ = Class180.getString_0(107452025);
				}
				((byte*)ptr)[2] = (jsonItem_0.prophecyText.Contains(Class180.getString_0(107365953)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					string_ = Class180.getString_0(107452016);
				}
				((byte*)ptr)[3] = (jsonItem_0.prophecyText.Contains(Class180.getString_0(107365912)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					string_ = Class180.getString_0(107452039);
				}
				((byte*)ptr)[4] = (jsonItem_0.prophecyText.Contains(Class180.getString_0(107365926)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					string_ = Class180.getString_0(107452030);
				}
				((byte*)ptr)[5] = (jsonItem_0.prophecyText.Contains(Class180.getString_0(107365880)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					string_ = Class180.getString_0(107451993);
				}
				class273_0.discriminatorName = new Class269(string_, Class180.getString_0(107370103));
				class273_0.discriminatorType = new Class269(string_, Class180.getString_0(107352631));
			}
			return new Class274(class273_0);
		}

		private unsafe static Class274 smethod_6(JsonItem jsonItem_0, Class273 class273_0)
		{
			void* ptr = stackalloc byte[16];
			*(byte*)ptr = ((class273_0.filters == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				class273_0.filters = new Class288();
			}
			class273_0.filters.TypeFilters = new Class297();
			class273_0.filters.MiscFilters = new Class284();
			((byte*)ptr)[1] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107451984)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				class273_0.type = jsonItem_0.typeLine.Replace(Class180.getString_0(107451999), Class180.getString_0(107397402));
				class273_0.filters.MiscFilters.filters.SynthesisedItem = new Class287(Class180.getString_0(107452458));
			}
			((byte*)ptr)[2] = (jsonItem_0.typeLine.Contains(Class180.getString_0(107451950)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				class273_0.type = class273_0.type.Replace(Class180.getString_0(107367726), Class180.getString_0(107397402));
			}
			class273_0.filters.TypeFilters = new Class297();
			((byte*)ptr)[3] = ((jsonItem_0.ItemRarity != ItemRarity.Unique) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				class273_0.name = null;
				class273_0.filters.TypeFilters.filters.rarity = new Class287(Class180.getString_0(107452408));
				((byte*)ptr)[4] = ((jsonItem_0.ItemRarity == ItemRarity.Magic) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					class273_0.type = API.smethod_15(jsonItem_0);
				}
				((byte*)ptr)[5] = (jsonItem_0.UsesItemLevel ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					class273_0.filters.MiscFilters.filters.itemLevel = new Class277(Math.Min(jsonItem_0.ilvl, 86), 100);
				}
				((byte*)ptr)[6] = (jsonItem_0.IsHeist ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					class273_0.filters.MapFilters = new Class282();
					class273_0.filters.MapFilters.filters.AreaLevel = new Class277(jsonItem_0.AreaLevel, 100);
				}
				((byte*)ptr)[7] = ((jsonItem_0.influences != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					((byte*)ptr)[8] = (jsonItem_0.influences.Crusader ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						class273_0.filters.MiscFilters.filters.CrusaderItem = new Class287(Class180.getString_0(107452458));
					}
					((byte*)ptr)[9] = (jsonItem_0.influences.Elder ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						class273_0.filters.MiscFilters.filters.ElderItem = new Class287(Class180.getString_0(107452458));
					}
					((byte*)ptr)[10] = (jsonItem_0.influences.Hunter ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						class273_0.filters.MiscFilters.filters.HunterItem = new Class287(Class180.getString_0(107452458));
					}
					((byte*)ptr)[11] = (jsonItem_0.influences.Redeemer ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						class273_0.filters.MiscFilters.filters.RedeemerItem = new Class287(Class180.getString_0(107452458));
					}
					((byte*)ptr)[12] = (jsonItem_0.influences.Shaper ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						class273_0.filters.MiscFilters.filters.ShaperItem = new Class287(Class180.getString_0(107452458));
					}
					((byte*)ptr)[13] = (jsonItem_0.influences.Warlord ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						class273_0.filters.MiscFilters.filters.WarlordItem = new Class287(Class180.getString_0(107452458));
					}
				}
			}
			else
			{
				class273_0.filters.TypeFilters.filters.rarity = new Class287(Class180.getString_0(107452449));
				Class283 filters = class273_0.filters.MiscFilters.filters;
				((byte*)ptr)[14] = (jsonItem_0.corrupted ? 1 : 0);
				filters.corrupted = new Class287(((bool*)((byte*)ptr + 14))->ToString().ToLower());
			}
			((byte*)ptr)[15] = ((jsonItem_0.Links == 6) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 15) != 0)
			{
				class273_0.filters.SocketFilters = new Class291();
				class273_0.filters.SocketFilters.disabled = false;
				class273_0.filters.SocketFilters.filters.links = new Class278(6);
			}
			return new Class274(class273_0);
		}

		private static Class274 smethod_7(JsonItem jsonItem_0, Class273 class273_0)
		{
			class273_0.filters.MiscFilters = new Class284();
			class273_0.filters.MiscFilters.disabled = false;
			if (jsonItem_0.SeedLevel >= 76)
			{
				class273_0.filters.MiscFilters.filters.itemLevel = new Class277(76, 100);
			}
			else
			{
				class273_0.filters.MiscFilters.filters.itemLevel = new Class277(1, 75);
			}
			return new Class274(class273_0);
		}

		static Class180()
		{
			Strings.CreateGetStringDelegate(typeof(Class180));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
