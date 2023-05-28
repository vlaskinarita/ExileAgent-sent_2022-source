using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using ns22;
using ns29;
using ns38;
using ns6;
using PoEv2.Classes;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Items
{
	public sealed class ItemData
	{
		public unsafe static string smethod_0(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = (jsonItem_0.typeLine.Contains(ItemData.getString_0(107440001)) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = ItemData.smethod_7(jsonItem_0);
			}
			else
			{
				((byte*)ptr)[1] = (jsonItem_0.typeLine.Contains(ItemData.getString_0(107439980)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = ItemData.smethod_8(jsonItem_0);
				}
				else
				{
					((byte*)ptr)[2] = (jsonItem_0.typeLine.Contains(ItemData.getString_0(107439407)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = ItemData.smethod_9(jsonItem_0);
					}
					else
					{
						((byte*)ptr)[3] = (jsonItem_0.typeLine.Contains(ItemData.getString_0(107439378)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = ItemData.smethod_10(jsonItem_0);
						}
						else
						{
							string string_ = API.smethod_15(jsonItem_0);
							Enum16 @enum = ItemData.smethod_6(string_);
							result = ((@enum == Enum16.const_0) ? null : ItemData.dictionary_1[@enum]);
						}
					}
				}
			}
			return result;
		}

		public static bool smethod_1(string string_0)
		{
			return ItemData.smethod_5(string_0) != null;
		}

		public static Size smethod_2(string string_0)
		{
			string string_ = API.smethod_16(string_0);
			ItemDatum itemDatum = ItemData.smethod_5(string_);
			return (itemDatum == null) ? new Size(1, 1) : itemDatum.Dimensions;
		}

		public static List<string> Amulets
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_0);
			}
		}

		public static List<string> Belts
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_1);
			}
		}

		public static List<string> Helmets
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_5);
			}
		}

		public static List<string> BodyArmour
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_2);
			}
		}

		public static List<string> Boots
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_3);
			}
		}

		public static List<string> Gloves
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_4);
			}
		}

		public static List<string> Rings
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_6);
			}
		}

		public static List<string> Shields
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_7);
			}
		}

		public static List<string> Weapons1H
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_8);
			}
		}

		public static List<string> Weapons2H
		{
			get
			{
				return ItemData.smethod_4(Enum15.const_9);
			}
		}

		public static IEnumerable<string> smethod_3()
		{
			return ItemData.dictionary_0.Values.Where(new Func<ItemDatum, bool>(ItemData.<>c.<>9.method_0)).Select(new Func<ItemDatum, string>(ItemData.<>c.<>9.method_1));
		}

		private static List<string> smethod_4(Enum15 enum15_0)
		{
			ItemData.Class298 @class = new ItemData.Class298();
			@class.enum15_0 = enum15_0;
			return ItemData.dictionary_0.Values.Where(new Func<ItemDatum, bool>(@class.method_0)).Select(new Func<ItemDatum, string>(ItemData.<>c.<>9.method_2)).ToList<string>();
		}

		public static ItemDatum smethod_5(string string_0)
		{
			ItemDatum result;
			if (!ItemData.dictionary_0.ContainsKey(string_0))
			{
				result = null;
			}
			else
			{
				result = ItemData.dictionary_0[string_0];
			}
			return result;
		}

		private static Enum16 smethod_6(string string_0)
		{
			ItemDatum itemDatum = ItemData.smethod_5(string_0);
			return (itemDatum == null) ? Enum16.const_0 : itemDatum.CraftingClass;
		}

		private unsafe static string smethod_7(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((jsonItem_0.implicitMods != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = (jsonItem_0.implicitMods[0].Contains(ItemData.getString_0(107439349)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return ItemData.dictionary_1[Enum16.const_44];
				}
				((byte*)ptr)[2] = (jsonItem_0.implicitMods[0].Contains(ItemData.getString_0(107439324)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return ItemData.dictionary_1[Enum16.const_43];
				}
				((byte*)ptr)[3] = (jsonItem_0.implicitMods[0].Contains(ItemData.getString_0(107439335)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					return ItemData.dictionary_1[Enum16.const_45];
				}
			}
			return null;
		}

		private unsafe static string smethod_8(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[12];
			foreach (string text in jsonItem_0.enchantMods)
			{
				*(byte*)ptr = (text.Contains(ItemData.getString_0(107439278)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					return Class237.SmallClusterJewel_MaximumLife;
				}
				((byte*)ptr)[1] = (text.Contains(ItemData.getString_0(107439245)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return Class237.SmallClusterJewel_MaximumEnergyShield;
				}
				((byte*)ptr)[2] = (text.Contains(ItemData.getString_0(107439232)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return Class237.SmallClusterJewel_IncreasedArmor;
				}
				((byte*)ptr)[3] = (text.Contains(ItemData.getString_0(107439207)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					return Class237.SmallClusterJewel_EvasionRating;
				}
				((byte*)ptr)[4] = (text.Contains(ItemData.getString_0(107439686)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					return Class237.SmallClusterJewel_Block;
				}
				((byte*)ptr)[5] = (text.Contains(ItemData.getString_0(107439657)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					return Class237.SmallClusterJewel_Block;
				}
				((byte*)ptr)[6] = (text.Contains(ItemData.getString_0(107439600)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					return Class237.SmallClusterJewel_FireResistance;
				}
				((byte*)ptr)[7] = (text.Contains(ItemData.getString_0(107439575)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					return Class237.SmallClusterJewel_ColdResistance;
				}
				((byte*)ptr)[8] = (text.Contains(ItemData.getString_0(107439582)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					return Class237.SmallClusterJewel_LightningResistance;
				}
				((byte*)ptr)[9] = (text.Contains(ItemData.getString_0(107439549)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					return Class237.SmallClusterJewel_ChaosResistance;
				}
				if (text.Contains(ItemData.getString_0(107439520)) || text.Contains(ItemData.getString_0(107439495)))
				{
					return Class237.SmallClusterJewel_DodgeAttacks;
				}
				((byte*)ptr)[10] = (text.Contains(ItemData.getString_0(107439466)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					return Class237.SmallClusterJewel_ManaReservationSmall;
				}
				((byte*)ptr)[11] = (text.Contains(ItemData.getString_0(107438889)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					return Class237.SmallClusterJewel_CurseEffectSmall;
				}
			}
			return null;
		}

		private unsafe static string smethod_9(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[20];
			foreach (string text in jsonItem_0.enchantMods)
			{
				if (text.Contains(ItemData.getString_0(107438812)) || text.Contains(ItemData.getString_0(107438815)))
				{
					return Class237.MediumClusterJewel_FireDamageOverTimeMultiplier;
				}
				*(byte*)ptr = (text.Contains(ItemData.getString_0(107438782)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					return Class237.MediumClusterJewel_ChaosDamageOverTimeMultiplier;
				}
				((byte*)ptr)[1] = (text.Contains(ItemData.getString_0(107438749)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return Class237.MediumClusterJewel_PhysicalDamageOverTimeMultiplier;
				}
				((byte*)ptr)[2] = (text.Contains(ItemData.getString_0(107438680)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return Class237.MediumClusterJewel_ColdDamageOverTimeMultiplier;
				}
				((byte*)ptr)[3] = (text.Contains(ItemData.getString_0(107439163)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					return Class237.MediumClusterJewel_DamageOverTimeMultiplier;
				}
				((byte*)ptr)[4] = (text.Contains(ItemData.getString_0(107439170)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					return Class237.MediumClusterJewel_EffectOfNonDamagingAilments;
				}
				((byte*)ptr)[5] = (text.Contains(ItemData.getString_0(107439093)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					return Class237.MediumClusterJewel_AuraEffect;
				}
				((byte*)ptr)[6] = (text.Contains(ItemData.getString_0(107439056)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					return Class237.MediumClusterJewel_CurseEffect;
				}
				((byte*)ptr)[7] = (text.Contains(ItemData.getString_0(107439027)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					return Class237.MediumClusterJewel_DamageWhileYouHaveAHerald;
				}
				((byte*)ptr)[8] = (text.Contains(ItemData.getString_0(107439014)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					return Class237.MediumClusterJewel_MinionDamageWhileYouHaveAHerald;
				}
				((byte*)ptr)[9] = (text.Contains(ItemData.getString_0(107438925)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					return Class237.MediumClusterJewel_WarcryBuffEffect;
				}
				((byte*)ptr)[10] = (text.Contains(ItemData.getString_0(107438384)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					return Class237.MediumClusterJewel_CriticalChance;
				}
				((byte*)ptr)[11] = (text.Contains(ItemData.getString_0(107439278)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					return Class237.MediumClusterJewel_MinionLife;
				}
				((byte*)ptr)[12] = (text.Contains(ItemData.getString_0(107438371)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					return Class237.MediumClusterJewel_AreaDamage;
				}
				((byte*)ptr)[13] = (text.Contains(ItemData.getString_0(107438342)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					return Class237.MediumClusterJewel_ProjectileDamage;
				}
				((byte*)ptr)[14] = (text.Contains(ItemData.getString_0(107438305)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					return Class237.MediumClusterJewel_TrapAndMineDamage;
				}
				((byte*)ptr)[15] = (text.Contains(ItemData.getString_0(107438276)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					return Class237.MediumClusterJewel_TotemDamage;
				}
				((byte*)ptr)[16] = (text.Contains(ItemData.getString_0(107438243)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					return Class237.MediumClusterJewel_BrandDamage;
				}
				((byte*)ptr)[17] = (text.Contains(ItemData.getString_0(107438210)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					return Class237.MediumClusterJewel_ChannellingSkillDamage;
				}
				((byte*)ptr)[18] = (text.Contains(ItemData.getString_0(107438177)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					return Class237.MediumClusterJewel_FlaskDuration;
				}
				((byte*)ptr)[19] = (text.Contains(ItemData.getString_0(107438612)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 19) != 0)
				{
					return Class237.MediumClusterJewel_LifeAndManaRecoveryFromFlasks;
				}
			}
			return null;
		}

		private unsafe static string smethod_10(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[17];
			foreach (string text in jsonItem_0.enchantMods)
			{
				*(byte*)ptr = (text.Contains(ItemData.getString_0(107438575)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					return Class237.LargeClusterJewel_AxeAndSwordDamage;
				}
				((byte*)ptr)[1] = (text.Contains(ItemData.getString_0(107438550)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return Class237.LargeClusterJewel_MaceAndStaffDamage;
				}
				((byte*)ptr)[2] = (text.Contains(ItemData.getString_0(107438509)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return Class237.LargeClusterJewel_DaggerAndClawDamage;
				}
				((byte*)ptr)[3] = (text.Contains(ItemData.getString_0(107438480)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					return Class237.LargeClusterJewel_BowDamage;
				}
				((byte*)ptr)[4] = (text.Contains(ItemData.getString_0(107438475)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					return Class237.LargeClusterJewel_WandDamage;
				}
				((byte*)ptr)[5] = (text.Contains(ItemData.getString_0(107438418)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					return Class237.LargeClusterJewel_DamageWithTwoHandedMeleeWeapons;
				}
				((byte*)ptr)[6] = (text.Contains(ItemData.getString_0(107437849)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					return Class237.LargeClusterJewel_AttackDamageWhileDualWielding;
				}
				((byte*)ptr)[7] = (text.Contains(ItemData.getString_0(107437788)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					return Class237.LargeClusterJewel_AttackDamageWhileHoldingAShield;
				}
				((byte*)ptr)[8] = (text.EndsWith(ItemData.getString_0(107437723)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					return Class237.LargeClusterJewel_AttackDamage;
				}
				((byte*)ptr)[9] = (text.EndsWith(ItemData.getString_0(107437690)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					return Class237.LargeClusterJewel_SpellDamage;
				}
				((byte*)ptr)[10] = (text.EndsWith(ItemData.getString_0(107437657)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					return Class237.LargeClusterJewel_ElementalDamage;
				}
				((byte*)ptr)[11] = (text.EndsWith(ItemData.getString_0(107438132)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					return Class237.LargeClusterJewel_PhysicalDamage;
				}
				((byte*)ptr)[12] = (text.EndsWith(ItemData.getString_0(107438095)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					return Class237.LargeClusterJewel_FireDamage;
				}
				((byte*)ptr)[13] = (text.EndsWith(ItemData.getString_0(107438066)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					return Class237.LargeClusterJewel_LightningDamage;
				}
				((byte*)ptr)[14] = (text.EndsWith(ItemData.getString_0(107438029)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					return Class237.LargeClusterJewel_ColdDamage;
				}
				((byte*)ptr)[15] = (text.EndsWith(ItemData.getString_0(107438000)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					return Class237.LargeClusterJewel_ChaosDamage;
				}
				((byte*)ptr)[16] = (text.Contains(ItemData.getString_0(107437967)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					return Class237.LargeClusterJewel_MinionDamage;
				}
			}
			return null;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ItemData()
		{
			Strings.CreateGetStringDelegate(typeof(ItemData));
			ItemData.dictionary_0 = new Dictionary<string, ItemDatum>
			{
				{
					ItemData.getString_0(107437982),
					new ItemDatum(ItemData.getString_0(107437982), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Agate_Amulet)
				},
				{
					ItemData.getString_0(107437933),
					new ItemDatum(ItemData.getString_0(107437933), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Amber_Amulet)
				},
				{
					ItemData.getString_0(107437916),
					new ItemDatum(ItemData.getString_0(107437916), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Ashscale_Talisman)
				},
				{
					ItemData.getString_0(107437923),
					new ItemDatum(ItemData.getString_0(107437923), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Avian_Twins_Talisman)
				},
				{
					ItemData.getString_0(107437382),
					new ItemDatum(ItemData.getString_0(107437382), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Black_Maw_Talisman)
				},
				{
					ItemData.getString_0(107437325),
					new ItemDatum(ItemData.getString_0(107437325), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Blue_Pearl_Amulet)
				},
				{
					ItemData.getString_0(107437300),
					new ItemDatum(ItemData.getString_0(107437300), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Bonespire_Talisman)
				},
				{
					ItemData.getString_0(107437275),
					new ItemDatum(ItemData.getString_0(107437275), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Breakrib_Talisman)
				},
				{
					ItemData.getString_0(107437282),
					new ItemDatum(ItemData.getString_0(107437282), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Chrysalis_Talisman)
				},
				{
					ItemData.getString_0(107437257),
					new ItemDatum(ItemData.getString_0(107437257), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Citrine_Amulet)
				},
				{
					ItemData.getString_0(107437204),
					new ItemDatum(ItemData.getString_0(107437204), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Clutching_Talisman)
				},
				{
					ItemData.getString_0(107437179),
					new ItemDatum(ItemData.getString_0(107437179), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Coral_Amulet)
				},
				{
					ItemData.getString_0(107437194),
					new ItemDatum(ItemData.getString_0(107437194), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Deadhand_Talisman)
				},
				{
					ItemData.getString_0(107437137),
					new ItemDatum(ItemData.getString_0(107437137), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Deep_One_Talisman)
				},
				{
					ItemData.getString_0(107437624),
					new ItemDatum(ItemData.getString_0(107437624), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Fangjaw_Talisman)
				},
				{
					ItemData.getString_0(107437631),
					new ItemDatum(ItemData.getString_0(107437631), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Gold_Amulet)
				},
				{
					ItemData.getString_0(107437582),
					new ItemDatum(ItemData.getString_0(107437582), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Greatwolf_Talisman)
				},
				{
					ItemData.getString_0(107437557),
					new ItemDatum(ItemData.getString_0(107437557), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Hexclaw_Talisman)
				},
				{
					ItemData.getString_0(107437532),
					new ItemDatum(ItemData.getString_0(107437532), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Horned_Talisman)
				},
				{
					ItemData.getString_0(107437543),
					new ItemDatum(ItemData.getString_0(107437543), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Jade_Amulet)
				},
				{
					ItemData.getString_0(107437494),
					new ItemDatum(ItemData.getString_0(107437494), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Jet_Amulet)
				},
				{
					ItemData.getString_0(107437509),
					new ItemDatum(ItemData.getString_0(107437509), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Lapis_Amulet)
				},
				{
					ItemData.getString_0(107437460),
					new ItemDatum(ItemData.getString_0(107437460), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Lone_Antler_Talisman)
				},
				{
					ItemData.getString_0(107437431),
					new ItemDatum(ItemData.getString_0(107437431), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Longtooth_Talisman)
				},
				{
					ItemData.getString_0(107437438),
					new ItemDatum(ItemData.getString_0(107437438), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Mandible_Talisman)
				},
				{
					ItemData.getString_0(107437413),
					new ItemDatum(ItemData.getString_0(107437413), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Marble_Amulet)
				},
				{
					ItemData.getString_0(107436848),
					new ItemDatum(ItemData.getString_0(107436848), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Monkey_Paw_Talisman)
				},
				{
					ItemData.getString_0(107436819),
					new ItemDatum(ItemData.getString_0(107436819), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Monkey_Twins_Talisman)
				},
				{
					ItemData.getString_0(107436790),
					new ItemDatum(ItemData.getString_0(107436790), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Onyx_Amulet)
				},
				{
					ItemData.getString_0(107436805),
					new ItemDatum(ItemData.getString_0(107436805), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Paua_Amulet)
				},
				{
					ItemData.getString_0(107436756),
					new ItemDatum(ItemData.getString_0(107436756), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Primal_Skull_Talisman)
				},
				{
					ItemData.getString_0(107436727),
					new ItemDatum(ItemData.getString_0(107436727), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Rot_Head_Talisman)
				},
				{
					ItemData.getString_0(107436734),
					new ItemDatum(ItemData.getString_0(107436734), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Rotfeather_Talisman)
				},
				{
					ItemData.getString_0(107436705),
					new ItemDatum(ItemData.getString_0(107436705), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Ruby_Amulet)
				},
				{
					ItemData.getString_0(107436656),
					new ItemDatum(ItemData.getString_0(107436656), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Seaglass_Amulet)
				},
				{
					ItemData.getString_0(107436635),
					new ItemDatum(ItemData.getString_0(107436635), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Simplex_Amulet)
				},
				{
					ItemData.getString_0(107436646),
					new ItemDatum(ItemData.getString_0(107436646), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Spinefuse_Talisman)
				},
				{
					ItemData.getString_0(107437101),
					new ItemDatum(ItemData.getString_0(107437101), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Splitnewt_Talisman)
				},
				{
					ItemData.getString_0(107437076),
					new ItemDatum(ItemData.getString_0(107437076), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Three_Hands_Talisman)
				},
				{
					ItemData.getString_0(107437047),
					new ItemDatum(ItemData.getString_0(107437047), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Three_Rat_Talisman)
				},
				{
					ItemData.getString_0(107437054),
					new ItemDatum(ItemData.getString_0(107437054), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Turquoise_Amulet)
				},
				{
					ItemData.getString_0(107437029),
					new ItemDatum(ItemData.getString_0(107437029), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Undying_Flesh_Talisman)
				},
				{
					ItemData.getString_0(107436996),
					new ItemDatum(ItemData.getString_0(107436996), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Wereclaw_Talisman)
				},
				{
					ItemData.getString_0(107436971),
					new ItemDatum(ItemData.getString_0(107436971), Enum16.const_29, Enum15.const_0, 1, 1, Class217.Writhing_Talisman)
				},
				{
					ItemData.getString_0(107436914),
					new ItemDatum(ItemData.getString_0(107436914), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Chain_Belt)
				},
				{
					ItemData.getString_0(107436929),
					new ItemDatum(ItemData.getString_0(107436929), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Cloth_Belt)
				},
				{
					ItemData.getString_0(107436880),
					new ItemDatum(ItemData.getString_0(107436880), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Crystal_Belt)
				},
				{
					ItemData.getString_0(107436895),
					new ItemDatum(ItemData.getString_0(107436895), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Heavy_Belt)
				},
				{
					ItemData.getString_0(107436334),
					new ItemDatum(ItemData.getString_0(107436334), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Leather_Belt)
				},
				{
					ItemData.getString_0(107436349),
					new ItemDatum(ItemData.getString_0(107436349), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Micro_Distillery_Belt)
				},
				{
					ItemData.getString_0(107436320),
					new ItemDatum(ItemData.getString_0(107436320), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Rustic_Sash)
				},
				{
					ItemData.getString_0(107436271),
					new ItemDatum(ItemData.getString_0(107436271), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Studded_Belt)
				},
				{
					ItemData.getString_0(107436286),
					new ItemDatum(ItemData.getString_0(107436286), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Stygian_Vise)
				},
				{
					ItemData.getString_0(107436237),
					new ItemDatum(ItemData.getString_0(107436237), Enum16.const_32, Enum15.const_1, 2, 1, Class217.Vanguard_Belt)
				},
				{
					ItemData.getString_0(107436216),
					new ItemDatum(ItemData.getString_0(107436216), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Arena_Plate)
				},
				{
					ItemData.getString_0(107436231),
					new ItemDatum(ItemData.getString_0(107436231), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Assassin_s_Garb)
				},
				{
					ItemData.getString_0(107436178),
					new ItemDatum(ItemData.getString_0(107436178), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Astral_Plate)
				},
				{
					ItemData.getString_0(107436193),
					new ItemDatum(ItemData.getString_0(107436193), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Battle_Lamellar)
				},
				{
					ItemData.getString_0(107436172),
					new ItemDatum(ItemData.getString_0(107436172), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Battle_Plate)
				},
				{
					ItemData.getString_0(107436123),
					new ItemDatum(ItemData.getString_0(107436123), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Blood_Raiment)
				},
				{
					ItemData.getString_0(107436134),
					new ItemDatum(ItemData.getString_0(107436134), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Bone_Armour)
				},
				{
					ItemData.getString_0(107436597),
					new ItemDatum(ItemData.getString_0(107436597), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Bronze_Plate)
				},
				{
					ItemData.getString_0(107436612),
					new ItemDatum(ItemData.getString_0(107436612), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Buckskin_Tunic)
				},
				{
					ItemData.getString_0(107436559),
					new ItemDatum(ItemData.getString_0(107436559), Enum16.const_52, Enum15.const_2, 2, 3, Class217.Cabalist_Regalia)
				},
				{
					ItemData.getString_0(107436534),
					new ItemDatum(ItemData.getString_0(107436534), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Carnal_Armour)
				},
				{
					ItemData.getString_0(107436545),
					new ItemDatum(ItemData.getString_0(107436545), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Chain_Hauberk)
				},
				{
					ItemData.getString_0(107436524),
					new ItemDatum(ItemData.getString_0(107436524), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Chainmail_Doublet)
				},
				{
					ItemData.getString_0(107436467),
					new ItemDatum(ItemData.getString_0(107436467), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Chainmail_Tunic)
				},
				{
					ItemData.getString_0(107436478),
					new ItemDatum(ItemData.getString_0(107436478), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Chainmail_Vest)
				},
				{
					ItemData.getString_0(107479394),
					new ItemDatum(ItemData.getString_0(107479394), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Chestplate)
				},
				{
					ItemData.getString_0(107436457),
					new ItemDatum(ItemData.getString_0(107436457), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Colosseum_Plate)
				},
				{
					ItemData.getString_0(107436404),
					new ItemDatum(ItemData.getString_0(107436404), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Commander_s_Brigandine)
				},
				{
					ItemData.getString_0(107436371),
					new ItemDatum(ItemData.getString_0(107436371), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Conjurer_s_Vestment)
				},
				{
					ItemData.getString_0(107435830),
					new ItemDatum(ItemData.getString_0(107435830), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Conquest_Chainmail)
				},
				{
					ItemData.getString_0(107435837),
					new ItemDatum(ItemData.getString_0(107435837), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Copper_Plate)
				},
				{
					ItemData.getString_0(107435820),
					new ItemDatum(ItemData.getString_0(107435820), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Coronal_Leather)
				},
				{
					ItemData.getString_0(107435767),
					new ItemDatum(ItemData.getString_0(107435767), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Crimson_Raiment)
				},
				{
					ItemData.getString_0(107435778),
					new ItemDatum(ItemData.getString_0(107435778), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Crusader_Chainmail)
				},
				{
					ItemData.getString_0(107435753),
					new ItemDatum(ItemData.getString_0(107435753), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Crusader_Plate)
				},
				{
					ItemData.getString_0(107435700),
					new ItemDatum(ItemData.getString_0(107435700), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Crypt_Armour)
				},
				{
					ItemData.getString_0(107435715),
					new ItemDatum(ItemData.getString_0(107435715), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Cutthroat_s_Garb)
				},
				{
					ItemData.getString_0(107435690),
					new ItemDatum(ItemData.getString_0(107435690), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Desert_Brigandine)
				},
				{
					ItemData.getString_0(107435633),
					new ItemDatum(ItemData.getString_0(107435633), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Destiny_Leather)
				},
				{
					ItemData.getString_0(107435612),
					new ItemDatum(ItemData.getString_0(107435612), Enum16.const_52, Enum15.const_2, 2, 3, Class217.Destroyer_Regalia)
				},
				{
					ItemData.getString_0(107435619),
					new ItemDatum(ItemData.getString_0(107435619), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Devout_Chainmail)
				},
				{
					ItemData.getString_0(107436106),
					new ItemDatum(ItemData.getString_0(107436106), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Dragonscale_Doublet)
				},
				{
					ItemData.getString_0(107436045),
					new ItemDatum(ItemData.getString_0(107436045), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Eelskin_Tunic)
				},
				{
					ItemData.getString_0(107436024),
					new ItemDatum(ItemData.getString_0(107436024), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Elegant_Ringmail)
				},
				{
					ItemData.getString_0(107436031),
					new ItemDatum(ItemData.getString_0(107436031), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Exquisite_Leather)
				},
				{
					ItemData.getString_0(107436006),
					new ItemDatum(ItemData.getString_0(107436006), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Field_Lamellar)
				},
				{
					ItemData.getString_0(107435953),
					new ItemDatum(ItemData.getString_0(107435953), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Frontier_Leather)
				},
				{
					ItemData.getString_0(107435928),
					new ItemDatum(ItemData.getString_0(107435928), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Full_Chainmail)
				},
				{
					ItemData.getString_0(107435939),
					new ItemDatum(ItemData.getString_0(107435939), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Full_Dragonscale)
				},
				{
					ItemData.getString_0(107435914),
					new ItemDatum(ItemData.getString_0(107435914), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Full_Leather)
				},
				{
					ItemData.getString_0(107435865),
					new ItemDatum(ItemData.getString_0(107435865), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Full_Plate)
				},
				{
					ItemData.getString_0(107435880),
					new ItemDatum(ItemData.getString_0(107435880), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Full_Ringmail)
				},
				{
					ItemData.getString_0(107435315),
					new ItemDatum(ItemData.getString_0(107435315), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Full_Scale_Armour)
				},
				{
					ItemData.getString_0(107435290),
					new ItemDatum(ItemData.getString_0(107435290), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Full_Wyrmscale)
				},
				{
					ItemData.getString_0(107435301),
					new ItemDatum(ItemData.getString_0(107435301), Enum16.const_49, Enum15.const_2, 2, 3, Class217.General_s_Brigandine)
				},
				{
					ItemData.getString_0(107435272),
					new ItemDatum(ItemData.getString_0(107435272), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Gladiator_Plate)
				},
				{
					ItemData.getString_0(107435219),
					new ItemDatum(ItemData.getString_0(107435219), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Glorious_Leather)
				},
				{
					ItemData.getString_0(107435194),
					new ItemDatum(ItemData.getString_0(107435194), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Glorious_Plate)
				},
				{
					ItemData.getString_0(107435205),
					new ItemDatum(ItemData.getString_0(107435205), Enum16.const_0, Enum15.const_2, 2, 3, Class217.Golden_Mantle)
				},
				{
					ItemData.getString_0(107435152),
					new ItemDatum(ItemData.getString_0(107435152), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Golden_Plate)
				},
				{
					ItemData.getString_0(107435167),
					new ItemDatum(ItemData.getString_0(107435167), Enum16.const_52, Enum15.const_2, 2, 3, Class217.Grasping_Mail)
				},
				{
					ItemData.getString_0(107435146),
					new ItemDatum(ItemData.getString_0(107435146), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Holy_Chainmail)
				},
				{
					ItemData.getString_0(107435093),
					new ItemDatum(ItemData.getString_0(107435093), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Hussar_Brigandine)
				},
				{
					ItemData.getString_0(107435580),
					new ItemDatum(ItemData.getString_0(107435580), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Infantry_Brigandine)
				},
				{
					ItemData.getString_0(107435583),
					new ItemDatum(ItemData.getString_0(107435583), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Lacquered_Garb)
				},
				{
					ItemData.getString_0(107435562),
					new ItemDatum(ItemData.getString_0(107435562), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Latticed_Ringmail)
				},
				{
					ItemData.getString_0(107435505),
					new ItemDatum(ItemData.getString_0(107435505), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Light_Brigandine)
				},
				{
					ItemData.getString_0(107435480),
					new ItemDatum(ItemData.getString_0(107435480), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Lordly_Plate)
				},
				{
					ItemData.getString_0(107435495),
					new ItemDatum(ItemData.getString_0(107435495), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Loricated_Ringmail)
				},
				{
					ItemData.getString_0(107435438),
					new ItemDatum(ItemData.getString_0(107435438), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Mage_s_Vestment)
				},
				{
					ItemData.getString_0(107435417),
					new ItemDatum(ItemData.getString_0(107435417), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Majestic_Plate)
				},
				{
					ItemData.getString_0(107435428),
					new ItemDatum(ItemData.getString_0(107435428), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Necromancer_Silks)
				},
				{
					ItemData.getString_0(107435403),
					new ItemDatum(ItemData.getString_0(107435403), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Occultist_s_Vestment)
				},
				{
					ItemData.getString_0(107435342),
					new ItemDatum(ItemData.getString_0(107435342), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Oiled_Coat)
				},
				{
					ItemData.getString_0(107435357),
					new ItemDatum(ItemData.getString_0(107435357), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Oiled_Vest)
				},
				{
					ItemData.getString_0(107434828),
					new ItemDatum(ItemData.getString_0(107434828), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Ornate_Ringmail)
				},
				{
					ItemData.getString_0(107434775),
					new ItemDatum(ItemData.getString_0(107434775), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Padded_Jacket)
				},
				{
					ItemData.getString_0(107434786),
					new ItemDatum(ItemData.getString_0(107434786), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Padded_Vest)
				},
				{
					ItemData.getString_0(107434737),
					new ItemDatum(ItemData.getString_0(107434737), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Plate_Vest)
				},
				{
					ItemData.getString_0(107434752),
					new ItemDatum(ItemData.getString_0(107434752), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Quilted_Jacket)
				},
				{
					ItemData.getString_0(107434731),
					new ItemDatum(ItemData.getString_0(107434731), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Ringmail_Coat)
				},
				{
					ItemData.getString_0(107434678),
					new ItemDatum(ItemData.getString_0(107434678), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Sacrificial_Garb)
				},
				{
					ItemData.getString_0(107434685),
					new ItemDatum(ItemData.getString_0(107434685), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Sadist_Garb)
				},
				{
					ItemData.getString_0(107434668),
					new ItemDatum(ItemData.getString_0(107434668), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Sage_s_Robe)
				},
				{
					ItemData.getString_0(107434619),
					new ItemDatum(ItemData.getString_0(107434619), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Saintly_Chainmail)
				},
				{
					ItemData.getString_0(107434626),
					new ItemDatum(ItemData.getString_0(107434626), Enum16.const_50, Enum15.const_2, 2, 3, Class217.Saint_s_Hauberk)
				},
				{
					ItemData.getString_0(107434573),
					new ItemDatum(ItemData.getString_0(107434573), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Savant_s_Robe)
				},
				{
					ItemData.getString_0(107435064),
					new ItemDatum(ItemData.getString_0(107435064), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Scale_Doublet)
				},
				{
					ItemData.getString_0(107435075),
					new ItemDatum(ItemData.getString_0(107435075), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Scale_Vest)
				},
				{
					ItemData.getString_0(107435026),
					new ItemDatum(ItemData.getString_0(107435026), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Scarlet_Raiment)
				},
				{
					ItemData.getString_0(107435037),
					new ItemDatum(ItemData.getString_0(107435037), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Scholar_s_Robe)
				},
				{
					ItemData.getString_0(107435016),
					new ItemDatum(ItemData.getString_0(107435016), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Sentinel_Jacket)
				},
				{
					ItemData.getString_0(107434963),
					new ItemDatum(ItemData.getString_0(107434963), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Shabby_Jerkin)
				},
				{
					ItemData.getString_0(107434974),
					new ItemDatum(ItemData.getString_0(107434974), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Sharkskin_Tunic)
				},
				{
					ItemData.getString_0(107434953),
					new ItemDatum(ItemData.getString_0(107434953), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Silk_Robe)
				},
				{
					ItemData.getString_0(107434908),
					new ItemDatum(ItemData.getString_0(107434908), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Silken_Garb)
				},
				{
					ItemData.getString_0(107434923),
					new ItemDatum(ItemData.getString_0(107434923), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Silken_Vest)
				},
				{
					ItemData.getString_0(107434874),
					new ItemDatum(ItemData.getString_0(107434874), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Silken_Wrap)
				},
				{
					ItemData.getString_0(107434889),
					new ItemDatum(ItemData.getString_0(107434889), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Simple_Robe)
				},
				{
					ItemData.getString_0(107434840),
					new ItemDatum(ItemData.getString_0(107434840), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Sleek_Coat)
				},
				{
					ItemData.getString_0(107434855),
					new ItemDatum(ItemData.getString_0(107434855), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Soldier_s_Brigandine)
				},
				{
					ItemData.getString_0(107434314),
					new ItemDatum(ItemData.getString_0(107434314), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Spidersilk_Robe)
				},
				{
					ItemData.getString_0(107434261),
					new ItemDatum(ItemData.getString_0(107434261), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Strapped_Leather)
				},
				{
					ItemData.getString_0(107434236),
					new ItemDatum(ItemData.getString_0(107434236), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Sun_Leather)
				},
				{
					ItemData.getString_0(107434251),
					new ItemDatum(ItemData.getString_0(107434251), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Sun_Plate)
				},
				{
					ItemData.getString_0(107434238),
					new ItemDatum(ItemData.getString_0(107434238), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Thief_s_Garb)
				},
				{
					ItemData.getString_0(107434189),
					new ItemDatum(ItemData.getString_0(107434189), Enum16.const_46, Enum15.const_2, 2, 3, Class217.Triumphant_Lamellar)
				},
				{
					ItemData.getString_0(107434160),
					new ItemDatum(ItemData.getString_0(107434160), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Vaal_Regalia)
				},
				{
					ItemData.getString_0(107434175),
					new ItemDatum(ItemData.getString_0(107434175), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Varnished_Coat)
				},
				{
					ItemData.getString_0(107434154),
					new ItemDatum(ItemData.getString_0(107434154), Enum16.const_46, Enum15.const_2, 2, 3, Class217.War_Plate)
				},
				{
					ItemData.getString_0(107434141),
					new ItemDatum(ItemData.getString_0(107434141), Enum16.const_51, Enum15.const_2, 2, 3, Class217.Waxed_Garb)
				},
				{
					ItemData.getString_0(107434124),
					new ItemDatum(ItemData.getString_0(107434124), Enum16.const_48, Enum15.const_2, 2, 3, Class217.Widowsilk_Robe)
				},
				{
					ItemData.getString_0(107434071),
					new ItemDatum(ItemData.getString_0(107434071), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Wild_Leather)
				},
				{
					ItemData.getString_0(107434086),
					new ItemDatum(ItemData.getString_0(107434086), Enum16.const_49, Enum15.const_2, 2, 3, Class217.Wyrmscale_Doublet)
				},
				{
					ItemData.getString_0(107434541),
					new ItemDatum(ItemData.getString_0(107434541), Enum16.const_47, Enum15.const_2, 2, 3, Class217.Zodiac_Leather)
				},
				{
					ItemData.getString_0(107434520),
					new ItemDatum(ItemData.getString_0(107434520), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Ambush_Boots)
				},
				{
					ItemData.getString_0(107434535),
					new ItemDatum(ItemData.getString_0(107434535), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Ancient_Greaves)
				},
				{
					ItemData.getString_0(107434482),
					new ItemDatum(ItemData.getString_0(107434482), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Antique_Greaves)
				},
				{
					ItemData.getString_0(107434493),
					new ItemDatum(ItemData.getString_0(107434493), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Arcanist_Slippers)
				},
				{
					ItemData.getString_0(107434468),
					new ItemDatum(ItemData.getString_0(107434468), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Assassin_s_Boots)
				},
				{
					ItemData.getString_0(107434443),
					new ItemDatum(ItemData.getString_0(107434443), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Avian_Slippers)
				},
				{
					ItemData.getString_0(107434390),
					new ItemDatum(ItemData.getString_0(107434390), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Basemetal_Treads)
				},
				{
					ItemData.getString_0(107434397),
					new ItemDatum(ItemData.getString_0(107434397), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Brimstone_Treads)
				},
				{
					ItemData.getString_0(107434372),
					new ItemDatum(ItemData.getString_0(107434372), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Bronzescale_Boots)
				},
				{
					ItemData.getString_0(107434347),
					new ItemDatum(ItemData.getString_0(107434347), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Carnal_Boots)
				},
				{
					ItemData.getString_0(107433786),
					new ItemDatum(ItemData.getString_0(107433786), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Chain_Boots)
				},
				{
					ItemData.getString_0(107433801),
					new ItemDatum(ItemData.getString_0(107433801), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Clasped_Boots)
				},
				{
					ItemData.getString_0(107433748),
					new ItemDatum(ItemData.getString_0(107433748), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Cloudwhisper_Boots)
				},
				{
					ItemData.getString_0(107433723),
					new ItemDatum(ItemData.getString_0(107433723), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Conjurer_Boots)
				},
				{
					ItemData.getString_0(107433734),
					new ItemDatum(ItemData.getString_0(107433734), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Crusader_Boots)
				},
				{
					ItemData.getString_0(107433681),
					new ItemDatum(ItemData.getString_0(107433681), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Darksteel_Treads)
				},
				{
					ItemData.getString_0(107433656),
					new ItemDatum(ItemData.getString_0(107433656), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Deerskin_Boots)
				},
				{
					ItemData.getString_0(107433667),
					new ItemDatum(ItemData.getString_0(107433667), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Dragonscale_Boots)
				},
				{
					ItemData.getString_0(107433642),
					new ItemDatum(ItemData.getString_0(107433642), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Dreamquest_Slippers)
				},
				{
					ItemData.getString_0(107433581),
					new ItemDatum(ItemData.getString_0(107433581), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Duskwalk_Slippers)
				},
				{
					ItemData.getString_0(107433556),
					new ItemDatum(ItemData.getString_0(107433556), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Eelskin_Boots)
				},
				{
					ItemData.getString_0(107433567),
					new ItemDatum(ItemData.getString_0(107433567), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Fugitive_Boots)
				},
				{
					ItemData.getString_0(107434058),
					new ItemDatum(ItemData.getString_0(107434058), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Goathide_Boots)
				},
				{
					ItemData.getString_0(107434005),
					new ItemDatum(ItemData.getString_0(107434005), Enum16.const_0, Enum15.const_3, 2, 2, Class217.Golden_Caligae)
				},
				{
					ItemData.getString_0(107434016),
					new ItemDatum(ItemData.getString_0(107434016), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Goliath_Greaves)
				},
				{
					ItemData.getString_0(107433995),
					new ItemDatum(ItemData.getString_0(107433995), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Hydrascale_Boots)
				},
				{
					ItemData.getString_0(107433938),
					new ItemDatum(ItemData.getString_0(107433938), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Iron_Greaves)
				},
				{
					ItemData.getString_0(107433953),
					new ItemDatum(ItemData.getString_0(107433953), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Ironscale_Boots)
				},
				{
					ItemData.getString_0(107433932),
					new ItemDatum(ItemData.getString_0(107433932), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Kaom_s_Greaves)
				},
				{
					ItemData.getString_0(107433879),
					new ItemDatum(ItemData.getString_0(107433879), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Leatherscale_Boots)
				},
				{
					ItemData.getString_0(107433886),
					new ItemDatum(ItemData.getString_0(107433886), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Legion_Boots)
				},
				{
					ItemData.getString_0(107433837),
					new ItemDatum(ItemData.getString_0(107433837), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Mesh_Boots)
				},
				{
					ItemData.getString_0(107433820),
					new ItemDatum(ItemData.getString_0(107433820), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Murder_Boots)
				},
				{
					ItemData.getString_0(107433835),
					new ItemDatum(ItemData.getString_0(107433835), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Nightwind_Slippers)
				},
				{
					ItemData.getString_0(107433266),
					new ItemDatum(ItemData.getString_0(107433266), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Nubuck_Boots)
				},
				{
					ItemData.getString_0(107433281),
					new ItemDatum(ItemData.getString_0(107433281), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Plated_Greaves)
				},
				{
					ItemData.getString_0(107433260),
					new ItemDatum(ItemData.getString_0(107433260), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Rawhide_Boots)
				},
				{
					ItemData.getString_0(107433207),
					new ItemDatum(ItemData.getString_0(107433207), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Reinforced_Greaves)
				},
				{
					ItemData.getString_0(107433214),
					new ItemDatum(ItemData.getString_0(107433214), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Ringmail_Boots)
				},
				{
					ItemData.getString_0(107433193),
					new ItemDatum(ItemData.getString_0(107433193), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Riveted_Boots)
				},
				{
					ItemData.getString_0(107433140),
					new ItemDatum(ItemData.getString_0(107433140), Enum16.const_0, Enum15.const_3, 2, 2, Class217.Runic_Greaves)
				},
				{
					ItemData.getString_0(107433151),
					new ItemDatum(ItemData.getString_0(107433151), Enum16.const_0, Enum15.const_3, 2, 2, Class217.Runic_Sabatons)
				},
				{
					ItemData.getString_0(107433130),
					new ItemDatum(ItemData.getString_0(107433130), Enum16.const_0, Enum15.const_3, 2, 2, Class217.Runic_Sollerets)
				},
				{
					ItemData.getString_0(107433077),
					new ItemDatum(ItemData.getString_0(107433077), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Samite_Slippers)
				},
				{
					ItemData.getString_0(107433088),
					new ItemDatum(ItemData.getString_0(107433088), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Satin_Slippers)
				},
				{
					ItemData.getString_0(107433067),
					new ItemDatum(ItemData.getString_0(107433067), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Scholar_Boots)
				},
				{
					ItemData.getString_0(107433526),
					new ItemDatum(ItemData.getString_0(107433526), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Serpentscale_Boots)
				},
				{
					ItemData.getString_0(107433533),
					new ItemDatum(ItemData.getString_0(107433533), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Shackled_Boots)
				},
				{
					ItemData.getString_0(107433512),
					new ItemDatum(ItemData.getString_0(107433512), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Shagreen_Boots)
				},
				{
					ItemData.getString_0(107433459),
					new ItemDatum(ItemData.getString_0(107433459), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Sharkskin_Boots)
				},
				{
					ItemData.getString_0(107433470),
					new ItemDatum(ItemData.getString_0(107433470), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Silk_Slippers)
				},
				{
					ItemData.getString_0(107433449),
					new ItemDatum(ItemData.getString_0(107433449), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Slink_Boots)
				},
				{
					ItemData.getString_0(107433400),
					new ItemDatum(ItemData.getString_0(107433400), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Soldier_Boots)
				},
				{
					ItemData.getString_0(107433411),
					new ItemDatum(ItemData.getString_0(107433411), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Sorcerer_Boots)
				},
				{
					ItemData.getString_0(107433358),
					new ItemDatum(ItemData.getString_0(107433358), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Stealth_Boots)
				},
				{
					ItemData.getString_0(107433337),
					new ItemDatum(ItemData.getString_0(107433337), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Steel_Greaves)
				},
				{
					ItemData.getString_0(107433348),
					new ItemDatum(ItemData.getString_0(107433348), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Steelscale_Boots)
				},
				{
					ItemData.getString_0(107433323),
					new ItemDatum(ItemData.getString_0(107433323), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Stormrider_Boots)
				},
				{
					ItemData.getString_0(107268914),
					new ItemDatum(ItemData.getString_0(107268914), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Strapped_Boots)
				},
				{
					ItemData.getString_0(107268925),
					new ItemDatum(ItemData.getString_0(107268925), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Titan_Greaves)
				},
				{
					ItemData.getString_0(107268904),
					new ItemDatum(ItemData.getString_0(107268904), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Trapper_Boots)
				},
				{
					ItemData.getString_0(107440001),
					new ItemDatum(ItemData.getString_0(107440001), Enum16.const_0, Enum15.const_3, 2, 2, Class217.Two_Toned_Boots)
				},
				{
					ItemData.getString_0(107268851),
					new ItemDatum(ItemData.getString_0(107268851), Enum16.const_40, Enum15.const_3, 2, 2, Class217.Vaal_Greaves)
				},
				{
					ItemData.getString_0(107268866),
					new ItemDatum(ItemData.getString_0(107268866), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Velvet_Slippers)
				},
				{
					ItemData.getString_0(107268813),
					new ItemDatum(ItemData.getString_0(107268813), Enum16.const_41, Enum15.const_3, 2, 2, Class217.Windbreak_Boots)
				},
				{
					ItemData.getString_0(107268792),
					new ItemDatum(ItemData.getString_0(107268792), Enum16.const_42, Enum15.const_3, 2, 2, Class217.Wool_Shoes)
				},
				{
					ItemData.getString_0(107268807),
					new ItemDatum(ItemData.getString_0(107268807), Enum16.const_45, Enum15.const_3, 2, 2, Class217.Wrapped_Boots)
				},
				{
					ItemData.getString_0(107268754),
					new ItemDatum(ItemData.getString_0(107268754), Enum16.const_43, Enum15.const_3, 2, 2, Class217.Wyrmscale_Boots)
				},
				{
					ItemData.getString_0(107268765),
					new ItemDatum(ItemData.getString_0(107268765), Enum16.const_44, Enum15.const_3, 2, 2, Class217.Zealot_Boots)
				},
				{
					ItemData.getString_0(107268748),
					new ItemDatum(ItemData.getString_0(107268748), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Aetherwind_Gloves)
				},
				{
					ItemData.getString_0(107268691),
					new ItemDatum(ItemData.getString_0(107268691), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Ambush_Mitts)
				},
				{
					ItemData.getString_0(107268706),
					new ItemDatum(ItemData.getString_0(107268706), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Ancient_Gauntlets)
				},
				{
					ItemData.getString_0(107269193),
					new ItemDatum(ItemData.getString_0(107269193), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Antique_Gauntlets)
				},
				{
					ItemData.getString_0(107269136),
					new ItemDatum(ItemData.getString_0(107269136), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Apothecary_s_Gloves)
				},
				{
					ItemData.getString_0(107269107),
					new ItemDatum(ItemData.getString_0(107269107), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Arcanist_Gloves)
				},
				{
					ItemData.getString_0(107269118),
					new ItemDatum(ItemData.getString_0(107269118), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Assassin_s_Mitts)
				},
				{
					ItemData.getString_0(107269093),
					new ItemDatum(ItemData.getString_0(107269093), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Bronze_Gauntlets)
				},
				{
					ItemData.getString_0(107269068),
					new ItemDatum(ItemData.getString_0(107269068), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Bronzescale_Gauntlets)
				},
				{
					ItemData.getString_0(107269007),
					new ItemDatum(ItemData.getString_0(107269007), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Carnal_Mitts)
				},
				{
					ItemData.getString_0(107269022),
					new ItemDatum(ItemData.getString_0(107269022), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Chain_Gloves)
				},
				{
					ItemData.getString_0(107268973),
					new ItemDatum(ItemData.getString_0(107268973), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Clasped_Mitts)
				},
				{
					ItemData.getString_0(107268952),
					new ItemDatum(ItemData.getString_0(107268952), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Conjurer_Gloves)
				},
				{
					ItemData.getString_0(107268963),
					new ItemDatum(ItemData.getString_0(107268963), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Crusader_Gloves)
				},
				{
					ItemData.getString_0(107268398),
					new ItemDatum(ItemData.getString_0(107268398), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Debilitation_Gauntlets)
				},
				{
					ItemData.getString_0(107268365),
					new ItemDatum(ItemData.getString_0(107268365), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Deerskin_Gloves)
				},
				{
					ItemData.getString_0(107268344),
					new ItemDatum(ItemData.getString_0(107268344), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Dragonscale_Gauntlets)
				},
				{
					ItemData.getString_0(107268315),
					new ItemDatum(ItemData.getString_0(107268315), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Eelskin_Gloves)
				},
				{
					ItemData.getString_0(107268326),
					new ItemDatum(ItemData.getString_0(107268326), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Embroidered_Gloves)
				},
				{
					ItemData.getString_0(107268269),
					new ItemDatum(ItemData.getString_0(107268269), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Fingerless_Silk_Gloves)
				},
				{
					ItemData.getString_0(107268268),
					new ItemDatum(ItemData.getString_0(107268268), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Fishscale_Gauntlets)
				},
				{
					ItemData.getString_0(107268207),
					new ItemDatum(ItemData.getString_0(107268207), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Gauche_Gloves)
				},
				{
					ItemData.getString_0(107268186),
					new ItemDatum(ItemData.getString_0(107268186), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Goathide_Gloves)
				},
				{
					ItemData.getString_0(107268197),
					new ItemDatum(ItemData.getString_0(107268197), Enum16.const_0, Enum15.const_4, 2, 2, Class217.Golden_Bracers)
				},
				{
					ItemData.getString_0(107268656),
					new ItemDatum(ItemData.getString_0(107268656), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Goliath_Gauntlets)
				},
				{
					ItemData.getString_0(107268631),
					new ItemDatum(ItemData.getString_0(107268631), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Gripped_Gloves)
				},
				{
					ItemData.getString_0(107268642),
					new ItemDatum(ItemData.getString_0(107268642), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Gruelling_Gauntlets)
				},
				{
					ItemData.getString_0(107268613),
					new ItemDatum(ItemData.getString_0(107268613), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Hydrascale_Gauntlets)
				},
				{
					ItemData.getString_0(107268584),
					new ItemDatum(ItemData.getString_0(107268584), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Iron_Gauntlets)
				},
				{
					ItemData.getString_0(107268531),
					new ItemDatum(ItemData.getString_0(107268531), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Ironscale_Gauntlets)
				},
				{
					ItemData.getString_0(107268502),
					new ItemDatum(ItemData.getString_0(107268502), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Legion_Gloves)
				},
				{
					ItemData.getString_0(107268513),
					new ItemDatum(ItemData.getString_0(107268513), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Leyline_Gloves)
				},
				{
					ItemData.getString_0(107268492),
					new ItemDatum(ItemData.getString_0(107268492), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Mesh_Gloves)
				},
				{
					ItemData.getString_0(107268443),
					new ItemDatum(ItemData.getString_0(107268443), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Murder_Mitts)
				},
				{
					ItemData.getString_0(107268458),
					new ItemDatum(ItemData.getString_0(107268458), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Nexus_Gloves)
				},
				{
					ItemData.getString_0(107267897),
					new ItemDatum(ItemData.getString_0(107267897), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Nubuck_Gloves)
				},
				{
					ItemData.getString_0(107267908),
					new ItemDatum(ItemData.getString_0(107267908), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Plated_Gauntlets)
				},
				{
					ItemData.getString_0(107267883),
					new ItemDatum(ItemData.getString_0(107267883), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Rawhide_Gloves)
				},
				{
					ItemData.getString_0(107267830),
					new ItemDatum(ItemData.getString_0(107267830), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Ringmail_Gloves)
				},
				{
					ItemData.getString_0(107267841),
					new ItemDatum(ItemData.getString_0(107267841), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Riveted_Gloves)
				},
				{
					ItemData.getString_0(107267820),
					new ItemDatum(ItemData.getString_0(107267820), Enum16.const_0, Enum15.const_4, 2, 2, Class217.Runic_Gages)
				},
				{
					ItemData.getString_0(107267771),
					new ItemDatum(ItemData.getString_0(107267771), Enum16.const_0, Enum15.const_4, 2, 2, Class217.Runic_Gauntlets)
				},
				{
					ItemData.getString_0(107267782),
					new ItemDatum(ItemData.getString_0(107267782), Enum16.const_0, Enum15.const_4, 2, 2, Class217.Runic_Gloves)
				},
				{
					ItemData.getString_0(107267733),
					new ItemDatum(ItemData.getString_0(107267733), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Samite_Gloves)
				},
				{
					ItemData.getString_0(107267744),
					new ItemDatum(ItemData.getString_0(107267744), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Satin_Gloves)
				},
				{
					ItemData.getString_0(107267695),
					new ItemDatum(ItemData.getString_0(107267695), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Serpentscale_Gauntlets)
				},
				{
					ItemData.getString_0(107267662),
					new ItemDatum(ItemData.getString_0(107267662), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Shagreen_Gloves)
				},
				{
					ItemData.getString_0(107268153),
					new ItemDatum(ItemData.getString_0(107268153), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Sharkskin_Gloves)
				},
				{
					ItemData.getString_0(107268160),
					new ItemDatum(ItemData.getString_0(107268160), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Silk_Gloves)
				},
				{
					ItemData.getString_0(107268111),
					new ItemDatum(ItemData.getString_0(107268111), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Sinistral_Gloves)
				},
				{
					ItemData.getString_0(107268086),
					new ItemDatum(ItemData.getString_0(107268086), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Slink_Gloves)
				},
				{
					ItemData.getString_0(107268101),
					new ItemDatum(ItemData.getString_0(107268101), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Soldier_Gloves)
				},
				{
					ItemData.getString_0(107268048),
					new ItemDatum(ItemData.getString_0(107268048), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Sorcerer_Gloves)
				},
				{
					ItemData.getString_0(107268027),
					new ItemDatum(ItemData.getString_0(107268027), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Southswing_Gloves)
				},
				{
					ItemData.getString_0(107268034),
					new ItemDatum(ItemData.getString_0(107268034), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Spiked_Gloves)
				},
				{
					ItemData.getString_0(107267981),
					new ItemDatum(ItemData.getString_0(107267981), Enum16.const_35, Enum15.const_4, 2, 2, Class217.Stealth_Gloves)
				},
				{
					ItemData.getString_0(107267960),
					new ItemDatum(ItemData.getString_0(107267960), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Steel_Gauntlets)
				},
				{
					ItemData.getString_0(107267971),
					new ItemDatum(ItemData.getString_0(107267971), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Steelscale_Gauntlets)
				},
				{
					ItemData.getString_0(107267942),
					new ItemDatum(ItemData.getString_0(107267942), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Strapped_Mitts)
				},
				{
					ItemData.getString_0(107267377),
					new ItemDatum(ItemData.getString_0(107267377), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Taxing_Gauntlets)
				},
				{
					ItemData.getString_0(107267352),
					new ItemDatum(ItemData.getString_0(107267352), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Titan_Gauntlets)
				},
				{
					ItemData.getString_0(107267363),
					new ItemDatum(ItemData.getString_0(107267363), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Trapper_Mitts)
				},
				{
					ItemData.getString_0(107267310),
					new ItemDatum(ItemData.getString_0(107267310), Enum16.const_34, Enum15.const_4, 2, 2, Class217.Vaal_Gauntlets)
				},
				{
					ItemData.getString_0(107267289),
					new ItemDatum(ItemData.getString_0(107267289), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Velvet_Gloves)
				},
				{
					ItemData.getString_0(107267300),
					new ItemDatum(ItemData.getString_0(107267300), Enum16.const_36, Enum15.const_4, 2, 2, Class217.Wool_Gloves)
				},
				{
					ItemData.getString_0(107267251),
					new ItemDatum(ItemData.getString_0(107267251), Enum16.const_39, Enum15.const_4, 2, 2, Class217.Wrapped_Mitts)
				},
				{
					ItemData.getString_0(107267262),
					new ItemDatum(ItemData.getString_0(107267262), Enum16.const_37, Enum15.const_4, 2, 2, Class217.Wyrmscale_Gauntlets)
				},
				{
					ItemData.getString_0(107267233),
					new ItemDatum(ItemData.getString_0(107267233), Enum16.const_38, Enum15.const_4, 2, 2, Class217.Zealot_Gloves)
				},
				{
					ItemData.getString_0(107267212),
					new ItemDatum(ItemData.getString_0(107267212), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Archdemon_Crown)
				},
				{
					ItemData.getString_0(107267159),
					new ItemDatum(ItemData.getString_0(107267159), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Atonement_Mask)
				},
				{
					ItemData.getString_0(107267170),
					new ItemDatum(ItemData.getString_0(107267170), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Aventail_Helmet)
				},
				{
					ItemData.getString_0(107267629),
					new ItemDatum(ItemData.getString_0(107267629), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Barbute_Helmet)
				},
				{
					ItemData.getString_0(107267608),
					new ItemDatum(ItemData.getString_0(107267608), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Battered_Helm)
				},
				{
					ItemData.getString_0(107267619),
					new ItemDatum(ItemData.getString_0(107267619), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Blizzard_Crown)
				},
				{
					ItemData.getString_0(107267566),
					new ItemDatum(ItemData.getString_0(107267566), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Bone_Circlet)
				},
				{
					ItemData.getString_0(107267581),
					new ItemDatum(ItemData.getString_0(107267581), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Bone_Helmet)
				},
				{
					ItemData.getString_0(107267564),
					new ItemDatum(ItemData.getString_0(107267564), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Callous_Mask)
				},
				{
					ItemData.getString_0(107267515),
					new ItemDatum(ItemData.getString_0(107267515), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Close_Helmet)
				},
				{
					ItemData.getString_0(107267530),
					new ItemDatum(ItemData.getString_0(107267530), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Cone_Helmet)
				},
				{
					ItemData.getString_0(107267481),
					new ItemDatum(ItemData.getString_0(107267481), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Crusader_Helmet)
				},
				{
					ItemData.getString_0(107267492),
					new ItemDatum(ItemData.getString_0(107267492), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Deicide_Mask)
				},
				{
					ItemData.getString_0(107267443),
					new ItemDatum(ItemData.getString_0(107267443), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Demon_Crown)
				},
				{
					ItemData.getString_0(107267458),
					new ItemDatum(ItemData.getString_0(107267458), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Eternal_Burgonet)
				},
				{
					ItemData.getString_0(107267433),
					new ItemDatum(ItemData.getString_0(107267433), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Ezomyte_Burgonet)
				},
				{
					ItemData.getString_0(107266864),
					new ItemDatum(ItemData.getString_0(107266864), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Fencer_Helm)
				},
				{
					ItemData.getString_0(107266879),
					new ItemDatum(ItemData.getString_0(107266879), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Festival_Mask)
				},
				{
					ItemData.getString_0(107266858),
					new ItemDatum(ItemData.getString_0(107266858), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Fluted_Bascinet)
				},
				{
					ItemData.getString_0(107266805),
					new ItemDatum(ItemData.getString_0(107266805), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Gale_Crown)
				},
				{
					ItemData.getString_0(107266820),
					new ItemDatum(ItemData.getString_0(107266820), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Gilded_Sallet)
				},
				{
					ItemData.getString_0(107266767),
					new ItemDatum(ItemData.getString_0(107266767), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Gladiator_Helmet)
				},
				{
					ItemData.getString_0(107266742),
					new ItemDatum(ItemData.getString_0(107266742), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Golden_Mask)
				},
				{
					ItemData.getString_0(107266757),
					new ItemDatum(ItemData.getString_0(107266757), Enum16.const_0, Enum15.const_5, 2, 2, Class217.Golden_Visage)
				},
				{
					ItemData.getString_0(107266704),
					new ItemDatum(ItemData.getString_0(107266704), Enum16.const_0, Enum15.const_5, 2, 2, Class217.Golden_Wreath)
				},
				{
					ItemData.getString_0(107266683),
					new ItemDatum(ItemData.getString_0(107266683), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Great_Crown)
				},
				{
					ItemData.getString_0(107266698),
					new ItemDatum(ItemData.getString_0(107266698), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Great_Helmet)
				},
				{
					ItemData.getString_0(107266649),
					new ItemDatum(ItemData.getString_0(107266649), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Harlequin_Mask)
				},
				{
					ItemData.getString_0(107451564),
					new ItemDatum(ItemData.getString_0(107451564), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Hubris_Circlet)
				},
				{
					ItemData.getString_0(107266660),
					new ItemDatum(ItemData.getString_0(107266660), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Hunter_Hood)
				},
				{
					ItemData.getString_0(107267123),
					new ItemDatum(ItemData.getString_0(107267123), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Imp_Crown)
				},
				{
					ItemData.getString_0(107267142),
					new ItemDatum(ItemData.getString_0(107267142), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Iron_Circlet)
				},
				{
					ItemData.getString_0(107267093),
					new ItemDatum(ItemData.getString_0(107267093), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Iron_Hat)
				},
				{
					ItemData.getString_0(107267112),
					new ItemDatum(ItemData.getString_0(107267112), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Iron_Mask)
				},
				{
					ItemData.getString_0(107267067),
					new ItemDatum(ItemData.getString_0(107267067), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Lacquered_Helmet)
				},
				{
					ItemData.getString_0(107267074),
					new ItemDatum(ItemData.getString_0(107267074), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Leather_Cap)
				},
				{
					ItemData.getString_0(107267025),
					new ItemDatum(ItemData.getString_0(107267025), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Leather_Hood)
				},
				{
					ItemData.getString_0(107267040),
					new ItemDatum(ItemData.getString_0(107267040), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Lion_Pelt)
				},
				{
					ItemData.getString_0(107266995),
					new ItemDatum(ItemData.getString_0(107266995), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Lunaris_Circlet)
				},
				{
					ItemData.getString_0(107267006),
					new ItemDatum(ItemData.getString_0(107267006), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Magistrate_Crown)
				},
				{
					ItemData.getString_0(107266981),
					new ItemDatum(ItemData.getString_0(107266981), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Mind_Cage)
				},
				{
					ItemData.getString_0(107266936),
					new ItemDatum(ItemData.getString_0(107266936), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Necromancer_Circlet)
				},
				{
					ItemData.getString_0(107266907),
					new ItemDatum(ItemData.getString_0(107266907), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Nightmare_Bascinet)
				},
				{
					ItemData.getString_0(107266914),
					new ItemDatum(ItemData.getString_0(107266914), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Noble_Tricorne)
				},
				{
					ItemData.getString_0(107266349),
					new ItemDatum(ItemData.getString_0(107266349), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Penitent_Mask)
				},
				{
					ItemData.getString_0(107266328),
					new ItemDatum(ItemData.getString_0(107266328), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Pig_Faced_Bascinet)
				},
				{
					ItemData.getString_0(107266335),
					new ItemDatum(ItemData.getString_0(107266335), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Plague_Mask)
				},
				{
					ItemData.getString_0(107266286),
					new ItemDatum(ItemData.getString_0(107266286), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Praetor_Crown)
				},
				{
					ItemData.getString_0(107266265),
					new ItemDatum(ItemData.getString_0(107266265), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Prophet_Crown)
				},
				{
					ItemData.getString_0(107266276),
					new ItemDatum(ItemData.getString_0(107266276), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Raven_Mask)
				},
				{
					ItemData.getString_0(107266227),
					new ItemDatum(ItemData.getString_0(107266227), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Reaver_Helmet)
				},
				{
					ItemData.getString_0(107266238),
					new ItemDatum(ItemData.getString_0(107266238), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Regicide_Mask)
				},
				{
					ItemData.getString_0(107266217),
					new ItemDatum(ItemData.getString_0(107266217), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Royal_Burgonet)
				},
				{
					ItemData.getString_0(107266164),
					new ItemDatum(ItemData.getString_0(107266164), Enum16.const_0, Enum15.const_5, 2, 2, Class217.Runic_Crest)
				},
				{
					ItemData.getString_0(107266179),
					new ItemDatum(ItemData.getString_0(107266179), Enum16.const_0, Enum15.const_5, 2, 2, Class217.Runic_Crown)
				},
				{
					ItemData.getString_0(107266130),
					new ItemDatum(ItemData.getString_0(107266130), Enum16.const_0, Enum15.const_5, 2, 2, Class217.Runic_Helm)
				},
				{
					ItemData.getString_0(107266145),
					new ItemDatum(ItemData.getString_0(107266145), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Rusted_Coif)
				},
				{
					ItemData.getString_0(107467969),
					new ItemDatum(ItemData.getString_0(107467969), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Sallet)
				},
				{
					ItemData.getString_0(107266608),
					new ItemDatum(ItemData.getString_0(107266608), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Samnite_Helmet)
				},
				{
					ItemData.getString_0(107266587),
					new ItemDatum(ItemData.getString_0(107266587), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Scare_Mask)
				},
				{
					ItemData.getString_0(107266602),
					new ItemDatum(ItemData.getString_0(107266602), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Secutor_Helm)
				},
				{
					ItemData.getString_0(107266553),
					new ItemDatum(ItemData.getString_0(107266553), Enum16.const_53, Enum15.const_5, 2, 2, Class217.Siege_Helmet)
				},
				{
					ItemData.getString_0(107266568),
					new ItemDatum(ItemData.getString_0(107266568), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Silken_Hood)
				},
				{
					ItemData.getString_0(107266519),
					new ItemDatum(ItemData.getString_0(107266519), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Sinner_Tricorne)
				},
				{
					ItemData.getString_0(107266530),
					new ItemDatum(ItemData.getString_0(107266530), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Solaris_Circlet)
				},
				{
					ItemData.getString_0(107266477),
					new ItemDatum(ItemData.getString_0(107266477), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Soldier_Helmet)
				},
				{
					ItemData.getString_0(107266456),
					new ItemDatum(ItemData.getString_0(107266456), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Sorrow_Mask)
				},
				{
					ItemData.getString_0(107266471),
					new ItemDatum(ItemData.getString_0(107266471), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Steel_Circlet)
				},
				{
					ItemData.getString_0(107266418),
					new ItemDatum(ItemData.getString_0(107266418), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Torture_Cage)
				},
				{
					ItemData.getString_0(107266433),
					new ItemDatum(ItemData.getString_0(107266433), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Tribal_Circlet)
				},
				{
					ItemData.getString_0(107432408),
					new ItemDatum(ItemData.getString_0(107432408), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Tricorne)
				},
				{
					ItemData.getString_0(107266412),
					new ItemDatum(ItemData.getString_0(107266412), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Ursine_Pelt)
				},
				{
					ItemData.getString_0(107265851),
					new ItemDatum(ItemData.getString_0(107265851), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Vaal_Mask)
				},
				{
					ItemData.getString_0(107265838),
					new ItemDatum(ItemData.getString_0(107265838), Enum16.const_55, Enum15.const_5, 2, 2, Class217.Vine_Circlet)
				},
				{
					ItemData.getString_0(107265853),
					new ItemDatum(ItemData.getString_0(107265853), Enum16.const_56, Enum15.const_5, 2, 2, Class217.Visored_Sallet)
				},
				{
					ItemData.getString_0(107265832),
					new ItemDatum(ItemData.getString_0(107265832), Enum16.const_58, Enum15.const_5, 2, 2, Class217.Winter_Crown)
				},
				{
					ItemData.getString_0(107265783),
					new ItemDatum(ItemData.getString_0(107265783), Enum16.const_54, Enum15.const_5, 2, 2, Class217.Wolf_Pelt)
				},
				{
					ItemData.getString_0(107265802),
					new ItemDatum(ItemData.getString_0(107265802), Enum16.const_57, Enum15.const_5, 2, 2, Class217.Zealot_Helmet)
				},
				{
					ItemData.getString_0(107265749),
					new ItemDatum(ItemData.getString_0(107265749), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Amethyst_Ring)
				},
				{
					ItemData.getString_0(107265760),
					new ItemDatum(ItemData.getString_0(107265760), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Breach_Ring)
				},
				{
					ItemData.getString_0(107265711),
					new ItemDatum(ItemData.getString_0(107265711), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Cerulean_Ring)
				},
				{
					ItemData.getString_0(107265690),
					new ItemDatum(ItemData.getString_0(107265690), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Coral_Ring)
				},
				{
					ItemData.getString_0(107265705),
					new ItemDatum(ItemData.getString_0(107265705), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Diamond_Ring)
				},
				{
					ItemData.getString_0(107265656),
					new ItemDatum(ItemData.getString_0(107265656), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Gold_Ring)
				},
				{
					ItemData.getString_0(107265675),
					new ItemDatum(ItemData.getString_0(107265675), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Golden_Hoop)
				},
				{
					ItemData.getString_0(107265626),
					new ItemDatum(ItemData.getString_0(107265626), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Iolite_Ring)
				},
				{
					ItemData.getString_0(107265641),
					new ItemDatum(ItemData.getString_0(107265641), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Iron_Ring)
				},
				{
					ItemData.getString_0(107266108),
					new ItemDatum(ItemData.getString_0(107266108), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Jet_Ring)
				},
				{
					ItemData.getString_0(107266095),
					new ItemDatum(ItemData.getString_0(107266095), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Moonstone_Ring)
				},
				{
					ItemData.getString_0(107266074),
					new ItemDatum(ItemData.getString_0(107266074), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Opal_Ring)
				},
				{
					ItemData.getString_0(107266061),
					new ItemDatum(ItemData.getString_0(107266061), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Paua_Ring)
				},
				{
					ItemData.getString_0(107266080),
					new ItemDatum(ItemData.getString_0(107266080), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Prismatic_Ring)
				},
				{
					ItemData.getString_0(107266059),
					new ItemDatum(ItemData.getString_0(107266059), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Ruby_Ring)
				},
				{
					ItemData.getString_0(107266046),
					new ItemDatum(ItemData.getString_0(107266046), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Sapphire_Ring)
				},
				{
					ItemData.getString_0(107266025),
					new ItemDatum(ItemData.getString_0(107266025), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Steel_Ring)
				},
				{
					ItemData.getString_0(107265976),
					new ItemDatum(ItemData.getString_0(107265976), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Topaz_Ring)
				},
				{
					ItemData.getString_0(107265991),
					new ItemDatum(ItemData.getString_0(107265991), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Two_Stone_Ring)
				},
				{
					ItemData.getString_0(107265938),
					new ItemDatum(ItemData.getString_0(107265938), Enum16.const_31, Enum15.const_6, 1, 1, Class217.Unset_Ring)
				},
				{
					ItemData.getString_0(107265953),
					new ItemDatum(ItemData.getString_0(107265953), Enum16.const_30, Enum15.const_6, 1, 1, Class217.Vermillion_Ring)
				},
				{
					ItemData.getString_0(107265932),
					new ItemDatum(ItemData.getString_0(107265932), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Alder_Spiked_Shield)
				},
				{
					ItemData.getString_0(107265871),
					new ItemDatum(ItemData.getString_0(107265871), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Alloyed_Spiked_Shield)
				},
				{
					ItemData.getString_0(107265330),
					new ItemDatum(ItemData.getString_0(107265330), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Ancient_Spirit_Shield)
				},
				{
					ItemData.getString_0(107265301),
					new ItemDatum(ItemData.getString_0(107265301), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Angelic_Kite_Shield)
				},
				{
					ItemData.getString_0(107265272),
					new ItemDatum(ItemData.getString_0(107265272), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Archon_Kite_Shield)
				},
				{
					ItemData.getString_0(107265279),
					new ItemDatum(ItemData.getString_0(107265279), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Baroque_Round_Shield)
				},
				{
					ItemData.getString_0(107265250),
					new ItemDatum(ItemData.getString_0(107265250), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Battle_Buckler)
				},
				{
					ItemData.getString_0(107265197),
					new ItemDatum(ItemData.getString_0(107265197), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Bone_Spirit_Shield)
				},
				{
					ItemData.getString_0(107265172),
					new ItemDatum(ItemData.getString_0(107265172), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Branded_Kite_Shield)
				},
				{
					ItemData.getString_0(107265143),
					new ItemDatum(ItemData.getString_0(107265143), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Brass_Spirit_Shield)
				},
				{
					ItemData.getString_0(107265114),
					new ItemDatum(ItemData.getString_0(107265114), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Bronze_Tower_Shield)
				},
				{
					ItemData.getString_0(107265117),
					new ItemDatum(ItemData.getString_0(107265117), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Buckskin_Tower_Shield)
				},
				{
					ItemData.getString_0(107265600),
					new ItemDatum(ItemData.getString_0(107265600), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Burnished_Spiked_Shield)
				},
				{
					ItemData.getString_0(107265567),
					new ItemDatum(ItemData.getString_0(107265567), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Cardinal_Round_Shield)
				},
				{
					ItemData.getString_0(107265538),
					new ItemDatum(ItemData.getString_0(107265538), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Cedar_Tower_Shield)
				},
				{
					ItemData.getString_0(107265513),
					new ItemDatum(ItemData.getString_0(107265513), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Ceremonial_Kite_Shield)
				},
				{
					ItemData.getString_0(107265480),
					new ItemDatum(ItemData.getString_0(107265480), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Champion_Kite_Shield)
				},
				{
					ItemData.getString_0(107265451),
					new ItemDatum(ItemData.getString_0(107265451), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Chiming_Spirit_Shield)
				},
				{
					ItemData.getString_0(107265390),
					new ItemDatum(ItemData.getString_0(107265390), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Cold_attuned_Buckler)
				},
				{
					ItemData.getString_0(107265361),
					new ItemDatum(ItemData.getString_0(107265361), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Colossal_Tower_Shield)
				},
				{
					ItemData.getString_0(107264820),
					new ItemDatum(ItemData.getString_0(107264820), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Compound_Spiked_Shield)
				},
				{
					ItemData.getString_0(107264787),
					new ItemDatum(ItemData.getString_0(107264787), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Copper_Tower_Shield)
				},
				{
					ItemData.getString_0(107264758),
					new ItemDatum(ItemData.getString_0(107264758), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Corroded_Tower_Shield)
				},
				{
					ItemData.getString_0(107264729),
					new ItemDatum(ItemData.getString_0(107264729), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Corrugated_Buckler)
				},
				{
					ItemData.getString_0(107264736),
					new ItemDatum(ItemData.getString_0(107264736), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Crested_Tower_Shield)
				},
				{
					ItemData.getString_0(107264707),
					new ItemDatum(ItemData.getString_0(107264707), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Crimson_Round_Shield)
				},
				{
					ItemData.getString_0(107264678),
					new ItemDatum(ItemData.getString_0(107264678), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Crusader_Buckler)
				},
				{
					ItemData.getString_0(107264621),
					new ItemDatum(ItemData.getString_0(107264621), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Driftwood_Spiked_Shield)
				},
				{
					ItemData.getString_0(107264620),
					new ItemDatum(ItemData.getString_0(107264620), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Ebony_Tower_Shield)
				},
				{
					ItemData.getString_0(107265075),
					new ItemDatum(ItemData.getString_0(107265075), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Elegant_Round_Shield)
				},
				{
					ItemData.getString_0(107265046),
					new ItemDatum(ItemData.getString_0(107265046), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Enameled_Buckler)
				},
				{
					ItemData.getString_0(107265053),
					new ItemDatum(ItemData.getString_0(107265053), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Endothermic_Buckler)
				},
				{
					ItemData.getString_0(107265024),
					new ItemDatum(ItemData.getString_0(107265024), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Etched_Kite_Shield)
				},
				{
					ItemData.getString_0(107264999),
					new ItemDatum(ItemData.getString_0(107264999), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Exhausting_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264966),
					new ItemDatum(ItemData.getString_0(107264966), Enum16.const_61, Enum15.const_7, 2, 3, Class217.Exothermic_Tower_Shield)
				},
				{
					ItemData.getString_0(107264933),
					new ItemDatum(ItemData.getString_0(107264933), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Ezomyte_Spiked_Shield)
				},
				{
					ItemData.getString_0(107264904),
					new ItemDatum(ItemData.getString_0(107264904), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Ezomyte_Tower_Shield)
				},
				{
					ItemData.getString_0(107264875),
					new ItemDatum(ItemData.getString_0(107264875), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Fir_Round_Shield)
				},
				{
					ItemData.getString_0(107264306),
					new ItemDatum(ItemData.getString_0(107264306), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Fossilised_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264273),
					new ItemDatum(ItemData.getString_0(107264273), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Gilded_Buckler)
				},
				{
					ItemData.getString_0(107264252),
					new ItemDatum(ItemData.getString_0(107264252), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Girded_Tower_Shield)
				},
				{
					ItemData.getString_0(107264255),
					new ItemDatum(ItemData.getString_0(107264255), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Goathide_Buckler)
				},
				{
					ItemData.getString_0(107264230),
					new ItemDatum(ItemData.getString_0(107264230), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Golden_Buckler)
				},
				{
					ItemData.getString_0(107264177),
					new ItemDatum(ItemData.getString_0(107264177), Enum16.const_0, Enum15.const_7, 2, 2, Class217.Golden_Flame)
				},
				{
					ItemData.getString_0(107264192),
					new ItemDatum(ItemData.getString_0(107264192), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Hammered_Buckler)
				},
				{
					ItemData.getString_0(107264167),
					new ItemDatum(ItemData.getString_0(107264167), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Harmonic_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264134),
					new ItemDatum(ItemData.getString_0(107264134), Enum16.const_60, Enum15.const_7, 2, 3, Class217.Heat_attuned_Tower_Shield)
				},
				{
					ItemData.getString_0(107264097),
					new ItemDatum(ItemData.getString_0(107264097), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Imperial_Buckler)
				},
				{
					ItemData.getString_0(107264584),
					new ItemDatum(ItemData.getString_0(107264584), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Ironwood_Buckler)
				},
				{
					ItemData.getString_0(107264527),
					new ItemDatum(ItemData.getString_0(107264527), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Ivory_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264498),
					new ItemDatum(ItemData.getString_0(107264498), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Jingling_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264465),
					new ItemDatum(ItemData.getString_0(107264465), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Lacewood_Spirit_Shield)
				},
				{
					ItemData.getString_0(107264432),
					new ItemDatum(ItemData.getString_0(107264432), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Lacquered_Buckler)
				},
				{
					ItemData.getString_0(107264407),
					new ItemDatum(ItemData.getString_0(107264407), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Laminated_Kite_Shield)
				},
				{
					ItemData.getString_0(107264378),
					new ItemDatum(ItemData.getString_0(107264378), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Layered_Kite_Shield)
				},
				{
					ItemData.getString_0(107264381),
					new ItemDatum(ItemData.getString_0(107264381), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Linden_Kite_Shield)
				},
				{
					ItemData.getString_0(107264356),
					new ItemDatum(ItemData.getString_0(107264356), Enum16.const_60, Enum15.const_7, 2, 3, Class217.Magmatic_Tower_Shield)
				},
				{
					ItemData.getString_0(107263815),
					new ItemDatum(ItemData.getString_0(107263815), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Mahogany_Tower_Shield)
				},
				{
					ItemData.getString_0(107263786),
					new ItemDatum(ItemData.getString_0(107263786), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Maple_Round_Shield)
				},
				{
					ItemData.getString_0(107263729),
					new ItemDatum(ItemData.getString_0(107263729), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Mirrored_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263696),
					new ItemDatum(ItemData.getString_0(107263696), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Mosaic_Kite_Shield)
				},
				{
					ItemData.getString_0(107263671),
					new ItemDatum(ItemData.getString_0(107263671), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Oak_Buckler)
				},
				{
					ItemData.getString_0(107263686),
					new ItemDatum(ItemData.getString_0(107263686), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Ornate_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263657),
					new ItemDatum(ItemData.getString_0(107263657), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Painted_Buckler)
				},
				{
					ItemData.getString_0(107263604),
					new ItemDatum(ItemData.getString_0(107263604), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Painted_Tower_Shield)
				},
				{
					ItemData.getString_0(107263575),
					new ItemDatum(ItemData.getString_0(107263575), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Pine_Buckler)
				},
				{
					ItemData.getString_0(107263590),
					new ItemDatum(ItemData.getString_0(107263590), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Pinnacle_Tower_Shield)
				},
				{
					ItemData.getString_0(107264073),
					new ItemDatum(ItemData.getString_0(107264073), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Plank_Kite_Shield)
				},
				{
					ItemData.getString_0(107264016),
					new ItemDatum(ItemData.getString_0(107264016), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Polar_Buckler)
				},
				{
					ItemData.getString_0(107263995),
					new ItemDatum(ItemData.getString_0(107263995), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Polished_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263962),
					new ItemDatum(ItemData.getString_0(107263962), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Rawhide_Tower_Shield)
				},
				{
					ItemData.getString_0(107263965),
					new ItemDatum(ItemData.getString_0(107263965), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Redwood_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263936),
					new ItemDatum(ItemData.getString_0(107263936), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Reinforced_Kite_Shield)
				},
				{
					ItemData.getString_0(107263903),
					new ItemDatum(ItemData.getString_0(107263903), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Reinforced_Tower_Shield)
				},
				{
					ItemData.getString_0(107263870),
					new ItemDatum(ItemData.getString_0(107263870), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Rotted_Round_Shield)
				},
				{
					ItemData.getString_0(107263841),
					new ItemDatum(ItemData.getString_0(107263841), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Scarlet_Round_Shield)
				},
				{
					ItemData.getString_0(107263300),
					new ItemDatum(ItemData.getString_0(107263300), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Shagreen_Tower_Shield)
				},
				{
					ItemData.getString_0(107263271),
					new ItemDatum(ItemData.getString_0(107263271), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Sovereign_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263238),
					new ItemDatum(ItemData.getString_0(107263238), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Spiked_Bundle)
				},
				{
					ItemData.getString_0(107263185),
					new ItemDatum(ItemData.getString_0(107263185), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Spiked_Round_Shield)
				},
				{
					ItemData.getString_0(107263156),
					new ItemDatum(ItemData.getString_0(107263156), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Spiny_Round_Shield)
				},
				{
					ItemData.getString_0(107263131),
					new ItemDatum(ItemData.getString_0(107263131), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Splendid_Round_Shield)
				},
				{
					ItemData.getString_0(107263134),
					new ItemDatum(ItemData.getString_0(107263134), Enum16.const_60, Enum15.const_7, 2, 4, Class217.Splintered_Tower_Shield)
				},
				{
					ItemData.getString_0(107263101),
					new ItemDatum(ItemData.getString_0(107263101), Enum16.const_64, Enum15.const_7, 2, 3, Class217.Steel_Kite_Shield)
				},
				{
					ItemData.getString_0(107263076),
					new ItemDatum(ItemData.getString_0(107263076), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Studded_Round_Shield)
				},
				{
					ItemData.getString_0(107263559),
					new ItemDatum(ItemData.getString_0(107263559), Enum16.const_62, Enum15.const_7, 2, 3, Class217.Subsuming_Spirit_Shield)
				},
				{
					ItemData.getString_0(107263526),
					new ItemDatum(ItemData.getString_0(107263526), Enum16.const_65, Enum15.const_7, 2, 2, Class217.Supreme_Spiked_Shield)
				},
				{
					ItemData.getString_0(107263497),
					new ItemDatum(ItemData.getString_0(107263497), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Tarnished_Spirit_Shield)
				},
				{
					ItemData.getString_0(107263464),
					new ItemDatum(ItemData.getString_0(107263464), Enum16.const_63, Enum15.const_7, 2, 3, Class217.Teak_Round_Shield)
				},
				{
					ItemData.getString_0(107263407),
					new ItemDatum(ItemData.getString_0(107263407), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Thorium_Spirit_Shield)
				},
				{
					ItemData.getString_0(107263378),
					new ItemDatum(ItemData.getString_0(107263378), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Titanium_Spirit_Shield)
				},
				{
					ItemData.getString_0(107263345),
					new ItemDatum(ItemData.getString_0(107263345), Enum16.const_62, Enum15.const_7, 2, 3, Class217.Transfer_attuned_Spirit_Shield)
				},
				{
					ItemData.getString_0(107263336),
					new ItemDatum(ItemData.getString_0(107263336), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Twig_Spirit_Shield)
				},
				{
					ItemData.getString_0(107262767),
					new ItemDatum(ItemData.getString_0(107262767), Enum16.const_61, Enum15.const_7, 2, 2, Class217.Vaal_Buckler)
				},
				{
					ItemData.getString_0(107262782),
					new ItemDatum(ItemData.getString_0(107262782), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Vaal_Spirit_Shield)
				},
				{
					ItemData.getString_0(107262757),
					new ItemDatum(ItemData.getString_0(107262757), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Walnut_Spirit_Shield)
				},
				{
					ItemData.getString_0(107262728),
					new ItemDatum(ItemData.getString_0(107262728), Enum16.const_61, Enum15.const_7, 2, 2, Class217.War_Buckler)
				},
				{
					ItemData.getString_0(107262679),
					new ItemDatum(ItemData.getString_0(107262679), Enum16.const_62, Enum15.const_7, 2, 2, Class217.Yew_Spirit_Shield)
				},
				{
					ItemData.getString_0(107262686),
					new ItemDatum(ItemData.getString_0(107262686), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Abyssal_Sceptre)
				},
				{
					ItemData.getString_0(107262665),
					new ItemDatum(ItemData.getString_0(107262665), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Accumulator_Wand)
				},
				{
					ItemData.getString_0(107262608),
					new ItemDatum(ItemData.getString_0(107262608), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Alternating_Sceptre)
				},
				{
					ItemData.getString_0(107481980),
					new ItemDatum(ItemData.getString_0(107481980), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Ambusher)
				},
				{
					ItemData.getString_0(107262579),
					new ItemDatum(ItemData.getString_0(107262579), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Anarchic_Spiritblade)
				},
				{
					ItemData.getString_0(107262550),
					new ItemDatum(ItemData.getString_0(107262550), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Ancestral_Club)
				},
				{
					ItemData.getString_0(107262561),
					new ItemDatum(ItemData.getString_0(107262561), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Ancient_Sword)
				},
				{
					ItemData.getString_0(107263052),
					new ItemDatum(ItemData.getString_0(107263052), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Antique_Rapier)
				},
				{
					ItemData.getString_0(107262999),
					new ItemDatum(ItemData.getString_0(107262999), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Apex_Rapier)
				},
				{
					ItemData.getString_0(107263014),
					new ItemDatum(ItemData.getString_0(107263014), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Arming_Axe)
				},
				{
					ItemData.getString_0(107262965),
					new ItemDatum(ItemData.getString_0(107262965), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Assembler_Wand)
				},
				{
					ItemData.getString_0(107262976),
					new ItemDatum(ItemData.getString_0(107262976), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Auric_Mace)
				},
				{
					ItemData.getString_0(107480693),
					new ItemDatum(ItemData.getString_0(107480693), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Awl)
				},
				{
					ItemData.getString_0(107262927),
					new ItemDatum(ItemData.getString_0(107262927), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Barbed_Club)
				},
				{
					ItemData.getString_0(107481112),
					new ItemDatum(ItemData.getString_0(107481112), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Baselard)
				},
				{
					ItemData.getString_0(107262942),
					new ItemDatum(ItemData.getString_0(107262942), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Basket_Rapier)
				},
				{
					ItemData.getString_0(107262921),
					new ItemDatum(ItemData.getString_0(107262921), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Battered_Foil)
				},
				{
					ItemData.getString_0(107262868),
					new ItemDatum(ItemData.getString_0(107262868), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Battle_Hammer)
				},
				{
					ItemData.getString_0(107262879),
					new ItemDatum(ItemData.getString_0(107262879), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Battle_Sword)
				},
				{
					ItemData.getString_0(107262830),
					new ItemDatum(ItemData.getString_0(107262830), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Behemoth_Mace)
				},
				{
					ItemData.getString_0(107262809),
					new ItemDatum(ItemData.getString_0(107262809), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Bladed_Mace)
				},
				{
					ItemData.getString_0(107480276),
					new ItemDatum(ItemData.getString_0(107480276), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Blinder)
				},
				{
					ItemData.getString_0(107262824),
					new ItemDatum(ItemData.getString_0(107262824), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Blood_Sceptre)
				},
				{
					ItemData.getString_0(107262259),
					new ItemDatum(ItemData.getString_0(107262259), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Boarding_Axe)
				},
				{
					ItemData.getString_0(107262274),
					new ItemDatum(ItemData.getString_0(107262274), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Boom_Mace)
				},
				{
					ItemData.getString_0(107262229),
					new ItemDatum(ItemData.getString_0(107262229), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Boot_Blade)
				},
				{
					ItemData.getString_0(107262244),
					new ItemDatum(ItemData.getString_0(107262244), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Boot_Knife)
				},
				{
					ItemData.getString_0(107262195),
					new ItemDatum(ItemData.getString_0(107262195), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Broad_Axe)
				},
				{
					ItemData.getString_0(107262214),
					new ItemDatum(ItemData.getString_0(107262214), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Broad_Sword)
				},
				{
					ItemData.getString_0(107262165),
					new ItemDatum(ItemData.getString_0(107262165), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Bronze_Sceptre)
				},
				{
					ItemData.getString_0(107262176),
					new ItemDatum(ItemData.getString_0(107262176), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Burnished_Foil)
				},
				{
					ItemData.getString_0(107262155),
					new ItemDatum(ItemData.getString_0(107262155), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Butcher_Axe)
				},
				{
					ItemData.getString_0(107262106),
					new ItemDatum(ItemData.getString_0(107262106), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Butcher_Knife)
				},
				{
					ItemData.getString_0(107262117),
					new ItemDatum(ItemData.getString_0(107262117), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Capricious_Spiritblade)
				},
				{
					ItemData.getString_0(107262084),
					new ItemDatum(ItemData.getString_0(107262084), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Carnal_Sceptre)
				},
				{
					ItemData.getString_0(107262031),
					new ItemDatum(ItemData.getString_0(107262031), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Carved_Wand)
				},
				{
					ItemData.getString_0(107262046),
					new ItemDatum(ItemData.getString_0(107262046), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Carving_Knife)
				},
				{
					ItemData.getString_0(107262537),
					new ItemDatum(ItemData.getString_0(107262537), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Cat_s_Paw)
				},
				{
					ItemData.getString_0(107262492),
					new ItemDatum(ItemData.getString_0(107262492), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Ceremonial_Axe)
				},
				{
					ItemData.getString_0(107262503),
					new ItemDatum(ItemData.getString_0(107262503), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Ceremonial_Mace)
				},
				{
					ItemData.getString_0(107262450),
					new ItemDatum(ItemData.getString_0(107262450), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Charan_s_Sword)
				},
				{
					ItemData.getString_0(107262461),
					new ItemDatum(ItemData.getString_0(107262461), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Chest_Splitter)
				},
				{
					ItemData.getString_0(107478731),
					new ItemDatum(ItemData.getString_0(107478731), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Cleaver)
				},
				{
					ItemData.getString_0(107262440),
					new ItemDatum(ItemData.getString_0(107262440), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Congregator_Wand)
				},
				{
					ItemData.getString_0(107262383),
					new ItemDatum(ItemData.getString_0(107262383), Enum16.const_10, Enum15.const_8, 1, 3, Class217.Convoking_Wand)
				},
				{
					ItemData.getString_0(107262362),
					new ItemDatum(ItemData.getString_0(107262362), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Copper_Kris)
				},
				{
					ItemData.getString_0(107262377),
					new ItemDatum(ItemData.getString_0(107262377), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Copper_Sword)
				},
				{
					ItemData.getString_0(107262328),
					new ItemDatum(ItemData.getString_0(107262328), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Corsair_Sword)
				},
				{
					ItemData.getString_0(107262339),
					new ItemDatum(ItemData.getString_0(107262339), Enum16.const_5, Enum15.const_8, 2, 3, Class217.Courtesan_Sword)
				},
				{
					ItemData.getString_0(107262286),
					new ItemDatum(ItemData.getString_0(107262286), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Crack_Mace)
				},
				{
					ItemData.getString_0(107262301),
					new ItemDatum(ItemData.getString_0(107262301), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Crystal_Sceptre)
				},
				{
					ItemData.getString_0(107261768),
					new ItemDatum(ItemData.getString_0(107261768), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Crystal_Wand)
				},
				{
					ItemData.getString_0(107478014),
					new ItemDatum(ItemData.getString_0(107478014), Enum16.const_4, Enum15.const_8, 1, 3, Class217.Cutlass)
				},
				{
					ItemData.getString_0(107261719),
					new ItemDatum(ItemData.getString_0(107261719), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Darkwood_Sceptre)
				},
				{
					ItemData.getString_0(107261726),
					new ItemDatum(ItemData.getString_0(107261726), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Decorative_Axe)
				},
				{
					ItemData.getString_0(107261705),
					new ItemDatum(ItemData.getString_0(107261705), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Demon_Dagger)
				},
				{
					ItemData.getString_0(107261656),
					new ItemDatum(ItemData.getString_0(107261656), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Demon_s_Horn)
				},
				{
					ItemData.getString_0(107261671),
					new ItemDatum(ItemData.getString_0(107261671), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Disapprobation_Axe)
				},
				{
					ItemData.getString_0(107261614),
					new ItemDatum(ItemData.getString_0(107261614), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Double_Claw)
				},
				{
					ItemData.getString_0(107261629),
					new ItemDatum(ItemData.getString_0(107261629), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Dragon_Mace)
				},
				{
					ItemData.getString_0(107261612),
					new ItemDatum(ItemData.getString_0(107261612), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Dragonbone_Rapier)
				},
				{
					ItemData.getString_0(107261555),
					new ItemDatum(ItemData.getString_0(107261555), Enum16.const_5, Enum15.const_8, 2, 3, Class217.Dragoon_Sword)
				},
				{
					ItemData.getString_0(107261566),
					new ItemDatum(ItemData.getString_0(107261566), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Dream_Mace)
				},
				{
					ItemData.getString_0(107261517),
					new ItemDatum(ItemData.getString_0(107261517), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Driftwood_Club)
				},
				{
					ItemData.getString_0(107262008),
					new ItemDatum(ItemData.getString_0(107262008), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Driftwood_Sceptre)
				},
				{
					ItemData.getString_0(107262015),
					new ItemDatum(ItemData.getString_0(107262015), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Driftwood_Wand)
				},
				{
					ItemData.getString_0(107261994),
					new ItemDatum(ItemData.getString_0(107261994), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Dusk_Blade)
				},
				{
					ItemData.getString_0(107261945),
					new ItemDatum(ItemData.getString_0(107261945), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Eagle_Claw)
				},
				{
					ItemData.getString_0(107261960),
					new ItemDatum(ItemData.getString_0(107261960), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Elder_Sword)
				},
				{
					ItemData.getString_0(107261911),
					new ItemDatum(ItemData.getString_0(107261911), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Elegant_Foil)
				},
				{
					ItemData.getString_0(107261926),
					new ItemDatum(ItemData.getString_0(107261926), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Elegant_Sword)
				},
				{
					ItemData.getString_0(107261873),
					new ItemDatum(ItemData.getString_0(107261873), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Engraved_Hatchet)
				},
				{
					ItemData.getString_0(107261848),
					new ItemDatum(ItemData.getString_0(107261848), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Engraved_Wand)
				},
				{
					ItemData.getString_0(107476165),
					new ItemDatum(ItemData.getString_0(107476165), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Estoc)
				},
				{
					ItemData.getString_0(107261859),
					new ItemDatum(ItemData.getString_0(107261859), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Etched_Hatchet)
				},
				{
					ItemData.getString_0(107261806),
					new ItemDatum(ItemData.getString_0(107261806), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Eternal_Sword)
				},
				{
					ItemData.getString_0(107261785),
					new ItemDatum(ItemData.getString_0(107261785), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Eye_Gouger)
				},
				{
					ItemData.getString_0(107261800),
					new ItemDatum(ItemData.getString_0(107261800), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Ezomyte_Dagger)
				},
				{
					ItemData.getString_0(107261235),
					new ItemDatum(ItemData.getString_0(107261235), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Fancy_Foil)
				},
				{
					ItemData.getString_0(107261250),
					new ItemDatum(ItemData.getString_0(107261250), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Faun_s_Horn)
				},
				{
					ItemData.getString_0(107261201),
					new ItemDatum(ItemData.getString_0(107261201), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Fickle_Spiritblade)
				},
				{
					ItemData.getString_0(107261176),
					new ItemDatum(ItemData.getString_0(107261176), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Fiend_Dagger)
				},
				{
					ItemData.getString_0(107261191),
					new ItemDatum(ItemData.getString_0(107261191), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Flanged_Mace)
				},
				{
					ItemData.getString_0(107261142),
					new ItemDatum(ItemData.getString_0(107261142), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Flare_Mace)
				},
				{
					ItemData.getString_0(107261157),
					new ItemDatum(ItemData.getString_0(107261157), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Flashfire_Blade)
				},
				{
					ItemData.getString_0(107261104),
					new ItemDatum(ItemData.getString_0(107261104), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Flaying_Knife)
				},
				{
					ItemData.getString_0(107261083),
					new ItemDatum(ItemData.getString_0(107261083), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Flickerflame_Blade)
				},
				{
					ItemData.getString_0(107261090),
					new ItemDatum(ItemData.getString_0(107261090), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Fright_Claw)
				},
				{
					ItemData.getString_0(107475436),
					new ItemDatum(ItemData.getString_0(107475436), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Gavel)
				},
				{
					ItemData.getString_0(107261041),
					new ItemDatum(ItemData.getString_0(107261041), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Gemini_Claw)
				},
				{
					ItemData.getString_0(107261056),
					new ItemDatum(ItemData.getString_0(107261056), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Gemstone_Sword)
				},
				{
					ItemData.getString_0(107474668),
					new ItemDatum(ItemData.getString_0(107474668), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Gladius)
				},
				{
					ItemData.getString_0(107261035),
					new ItemDatum(ItemData.getString_0(107261035), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Glass_Shank)
				},
				{
					ItemData.getString_0(107261498),
					new ItemDatum(ItemData.getString_0(107261498), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Goat_s_Horn)
				},
				{
					ItemData.getString_0(107261513),
					new ItemDatum(ItemData.getString_0(107261513), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Golden_Kris)
				},
				{
					ItemData.getString_0(107474134),
					new ItemDatum(ItemData.getString_0(107474134), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Gouger)
				},
				{
					ItemData.getString_0(107261464),
					new ItemDatum(ItemData.getString_0(107261464), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Graceful_Sword)
				},
				{
					ItemData.getString_0(107474104),
					new ItemDatum(ItemData.getString_0(107474104), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Grappler)
				},
				{
					ItemData.getString_0(107261475),
					new ItemDatum(ItemData.getString_0(107261475), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Great_White_Claw)
				},
				{
					ItemData.getString_0(107261450),
					new ItemDatum(ItemData.getString_0(107261450), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Grinning_Fetish)
				},
				{
					ItemData.getString_0(107261397),
					new ItemDatum(ItemData.getString_0(107261397), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Gut_Ripper)
				},
				{
					ItemData.getString_0(107261412),
					new ItemDatum(ItemData.getString_0(107261412), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Gutting_Knife)
				},
				{
					ItemData.getString_0(107261359),
					new ItemDatum(ItemData.getString_0(107261359), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Harpy_Rapier)
				},
				{
					ItemData.getString_0(107261374),
					new ItemDatum(ItemData.getString_0(107261374), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Heathen_Wand)
				},
				{
					ItemData.getString_0(107261325),
					new ItemDatum(ItemData.getString_0(107261325), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Hellion_s_Paw)
				},
				{
					ItemData.getString_0(107261304),
					new ItemDatum(ItemData.getString_0(107261304), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Hollowpoint_Dagger)
				},
				{
					ItemData.getString_0(107261311),
					new ItemDatum(ItemData.getString_0(107261311), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Hook_Sword)
				},
				{
					ItemData.getString_0(107261262),
					new ItemDatum(ItemData.getString_0(107261262), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Horned_Sceptre)
				},
				{
					ItemData.getString_0(107260729),
					new ItemDatum(ItemData.getString_0(107260729), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Imbued_Wand)
				},
				{
					ItemData.getString_0(107260744),
					new ItemDatum(ItemData.getString_0(107260744), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Imp_Dagger)
				},
				{
					ItemData.getString_0(107260695),
					new ItemDatum(ItemData.getString_0(107260695), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Imperial_Claw)
				},
				{
					ItemData.getString_0(107260706),
					new ItemDatum(ItemData.getString_0(107260706), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Imperial_Skean)
				},
				{
					ItemData.getString_0(107260653),
					new ItemDatum(ItemData.getString_0(107260653), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Infernal_Axe)
				},
				{
					ItemData.getString_0(107260636),
					new ItemDatum(ItemData.getString_0(107260636), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Infernal_Blade)
				},
				{
					ItemData.getString_0(107260647),
					new ItemDatum(ItemData.getString_0(107260647), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Iron_Sceptre)
				},
				{
					ItemData.getString_0(107260598),
					new ItemDatum(ItemData.getString_0(107260598), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Jade_Hatchet)
				},
				{
					ItemData.getString_0(107260613),
					new ItemDatum(ItemData.getString_0(107260613), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Jagged_Foil)
				},
				{
					ItemData.getString_0(107260564),
					new ItemDatum(ItemData.getString_0(107260564), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Jasper_Axe)
				},
				{
					ItemData.getString_0(107260579),
					new ItemDatum(ItemData.getString_0(107260579), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Jewelled_Foil)
				},
				{
					ItemData.getString_0(107260526),
					new ItemDatum(ItemData.getString_0(107260526), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Karui_Axe)
				},
				{
					ItemData.getString_0(107260545),
					new ItemDatum(ItemData.getString_0(107260545), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Karui_Sceptre)
				},
				{
					ItemData.getString_0(107260524),
					new ItemDatum(ItemData.getString_0(107260524), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Lead_Sceptre)
				},
				{
					ItemData.getString_0(107260987),
					new ItemDatum(ItemData.getString_0(107260987), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Legion_Hammer)
				},
				{
					ItemData.getString_0(107260998),
					new ItemDatum(ItemData.getString_0(107260998), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Legion_Sword)
				},
				{
					ItemData.getString_0(107260949),
					new ItemDatum(ItemData.getString_0(107260949), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Malign_Fangs)
				},
				{
					ItemData.getString_0(107260964),
					new ItemDatum(ItemData.getString_0(107260964), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Maltreatment_Axe)
				},
				{
					ItemData.getString_0(107260939),
					new ItemDatum(ItemData.getString_0(107260939), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Midnight_Blade)
				},
				{
					ItemData.getString_0(107260886),
					new ItemDatum(ItemData.getString_0(107260886), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Nailed_Fist)
				},
				{
					ItemData.getString_0(107260901),
					new ItemDatum(ItemData.getString_0(107260901), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Nightmare_Mace)
				},
				{
					ItemData.getString_0(107260848),
					new ItemDatum(ItemData.getString_0(107260848), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Noble_Claw)
				},
				{
					ItemData.getString_0(107260863),
					new ItemDatum(ItemData.getString_0(107260863), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Ochre_Sceptre)
				},
				{
					ItemData.getString_0(107260842),
					new ItemDatum(ItemData.getString_0(107260842), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Omen_Wand)
				},
				{
					ItemData.getString_0(107260829),
					new ItemDatum(ItemData.getString_0(107260829), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Opal_Sceptre)
				},
				{
					ItemData.getString_0(107260812),
					new ItemDatum(ItemData.getString_0(107260812), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Opal_Wand)
				},
				{
					ItemData.getString_0(107260799),
					new ItemDatum(ItemData.getString_0(107260799), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Oscillating_Sceptre)
				},
				{
					ItemData.getString_0(107260770),
					new ItemDatum(ItemData.getString_0(107260770), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Ornate_Mace)
				},
				{
					ItemData.getString_0(107260209),
					new ItemDatum(ItemData.getString_0(107260209), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Pagan_Wand)
				},
				{
					ItemData.getString_0(107470776),
					new ItemDatum(ItemData.getString_0(107470776), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Pecoraro)
				},
				{
					ItemData.getString_0(107260224),
					new ItemDatum(ItemData.getString_0(107260224), Enum16.const_7, Enum15.const_8, 2, 3)
				},
				{
					ItemData.getString_0(107260179),
					new ItemDatum(ItemData.getString_0(107260179), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Petrified_Club)
				},
				{
					ItemData.getString_0(107260190),
					new ItemDatum(ItemData.getString_0(107260190), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Phantom_Mace)
				},
				{
					ItemData.getString_0(107260141),
					new ItemDatum(ItemData.getString_0(107260141), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Platinum_Kris)
				},
				{
					ItemData.getString_0(107260120),
					new ItemDatum(ItemData.getString_0(107260120), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Platinum_Sceptre)
				},
				{
					ItemData.getString_0(107260127),
					new ItemDatum(ItemData.getString_0(107260127), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Pneumatic_Dagger)
				},
				{
					ItemData.getString_0(107470409),
					new ItemDatum(ItemData.getString_0(107470409), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Poignard)
				},
				{
					ItemData.getString_0(107260102),
					new ItemDatum(ItemData.getString_0(107260102), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Prehistoric_Claw)
				},
				{
					ItemData.getString_0(107260045),
					new ItemDatum(ItemData.getString_0(107260045), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Pressurised_Dagger)
				},
				{
					ItemData.getString_0(107260020),
					new ItemDatum(ItemData.getString_0(107260020), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Primeval_Rapier)
				},
				{
					ItemData.getString_0(107260031),
					new ItemDatum(ItemData.getString_0(107260031), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Profane_Wand)
				},
				{
					ItemData.getString_0(107259982),
					new ItemDatum(ItemData.getString_0(107259982), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Prong_Dagger)
				},
				{
					ItemData.getString_0(107259997),
					new ItemDatum(ItemData.getString_0(107259997), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Prophecy_Wand)
				},
				{
					ItemData.getString_0(107260488),
					new ItemDatum(ItemData.getString_0(107260488), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Psychotic_Axe)
				},
				{
					ItemData.getString_0(107260435),
					new ItemDatum(ItemData.getString_0(107260435), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Quartz_Sceptre)
				},
				{
					ItemData.getString_0(107260446),
					new ItemDatum(ItemData.getString_0(107260446), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Quartz_Wand)
				},
				{
					ItemData.getString_0(107260397),
					new ItemDatum(ItemData.getString_0(107260397), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Reaver_Axe)
				},
				{
					ItemData.getString_0(107260380),
					new ItemDatum(ItemData.getString_0(107260380), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Ritual_Sceptre)
				},
				{
					ItemData.getString_0(107260391),
					new ItemDatum(ItemData.getString_0(107260391), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Rock_Breaker)
				},
				{
					ItemData.getString_0(107260342),
					new ItemDatum(ItemData.getString_0(107260342), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Royal_Axe)
				},
				{
					ItemData.getString_0(107260361),
					new ItemDatum(ItemData.getString_0(107260361), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Royal_Sceptre)
				},
				{
					ItemData.getString_0(107260308),
					new ItemDatum(ItemData.getString_0(107260308), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Royal_Skean)
				},
				{
					ItemData.getString_0(107260323),
					new ItemDatum(ItemData.getString_0(107260323), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Runic_Hatchet)
				},
				{
					ItemData.getString_0(107260270),
					new ItemDatum(ItemData.getString_0(107260270), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Rusted_Hatchet)
				},
				{
					ItemData.getString_0(107260249),
					new ItemDatum(ItemData.getString_0(107260249), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Rusted_Spike)
				},
				{
					ItemData.getString_0(107260264),
					new ItemDatum(ItemData.getString_0(107260264), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Rusted_Sword)
				},
				{
					ItemData.getString_0(107468101),
					new ItemDatum(ItemData.getString_0(107468101), Enum16.const_4, Enum15.const_8, 1, 3, Class217.Sabre)
				},
				{
					ItemData.getString_0(107259703),
					new ItemDatum(ItemData.getString_0(107259703), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Sage_Wand)
				},
				{
					ItemData.getString_0(107467988),
					new ItemDatum(ItemData.getString_0(107467988), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Sai)
				},
				{
					ItemData.getString_0(107259722),
					new ItemDatum(ItemData.getString_0(107259722), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Sambar_Sceptre)
				},
				{
					ItemData.getString_0(107467582),
					new ItemDatum(ItemData.getString_0(107467582), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Sekhem)
				},
				{
					ItemData.getString_0(107259669),
					new ItemDatum(ItemData.getString_0(107259669), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Serpent_Wand)
				},
				{
					ItemData.getString_0(107259684),
					new ItemDatum(ItemData.getString_0(107259684), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Serrated_Foil)
				},
				{
					ItemData.getString_0(107259631),
					new ItemDatum(ItemData.getString_0(107259631), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Shadow_Fangs)
				},
				{
					ItemData.getString_0(107259646),
					new ItemDatum(ItemData.getString_0(107259646), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Shadow_Sceptre)
				},
				{
					ItemData.getString_0(107259625),
					new ItemDatum(ItemData.getString_0(107259625), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Sharktooth_Claw)
				},
				{
					ItemData.getString_0(107259572),
					new ItemDatum(ItemData.getString_0(107259572), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Siege_Axe)
				},
				{
					ItemData.getString_0(107466835),
					new ItemDatum(ItemData.getString_0(107466835), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Skean)
				},
				{
					ItemData.getString_0(107259591),
					new ItemDatum(ItemData.getString_0(107259591), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Skinning_Knife)
				},
				{
					ItemData.getString_0(107259538),
					new ItemDatum(ItemData.getString_0(107259538), Enum16.const_9, Enum15.const_8, 1, 3, Class217.Slaughter_Knife)
				},
				{
					ItemData.getString_0(107467228),
					new ItemDatum(ItemData.getString_0(107467228), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Smallsword)
				},
				{
					ItemData.getString_0(107259549),
					new ItemDatum(ItemData.getString_0(107259549), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Sparkling_Claw)
				},
				{
					ItemData.getString_0(107259528),
					new ItemDatum(ItemData.getString_0(107259528), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Spectral_Axe)
				},
				{
					ItemData.getString_0(107259479),
					new ItemDatum(ItemData.getString_0(107259479), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Spiked_Club)
				},
				{
					ItemData.getString_0(107259494),
					new ItemDatum(ItemData.getString_0(107259494), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Spiraled_Foil)
				},
				{
					ItemData.getString_0(107259953),
					new ItemDatum(ItemData.getString_0(107259953), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Spiraled_Wand)
				},
				{
					ItemData.getString_0(107259932),
					new ItemDatum(ItemData.getString_0(107259932), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Stabilising_Sceptre)
				},
				{
					ItemData.getString_0(107259935),
					new ItemDatum(ItemData.getString_0(107259935), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Stag_Sceptre)
				},
				{
					ItemData.getString_0(107466271),
					new ItemDatum(ItemData.getString_0(107466271), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Stiletto)
				},
				{
					ItemData.getString_0(107259886),
					new ItemDatum(ItemData.getString_0(107259886), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Stone_Hammer)
				},
				{
					ItemData.getString_0(107259901),
					new ItemDatum(ItemData.getString_0(107259901), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Tempered_Foil)
				},
				{
					ItemData.getString_0(107432531),
					new ItemDatum(ItemData.getString_0(107432531), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Tenderizer)
				},
				{
					ItemData.getString_0(107259880),
					new ItemDatum(ItemData.getString_0(107259880), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Terror_Claw)
				},
				{
					ItemData.getString_0(107259831),
					new ItemDatum(ItemData.getString_0(107259831), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Thorn_Rapier)
				},
				{
					ItemData.getString_0(107259846),
					new ItemDatum(ItemData.getString_0(107259846), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Thresher_Claw)
				},
				{
					ItemData.getString_0(107259793),
					new ItemDatum(ItemData.getString_0(107259793), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Throat_Stabber)
				},
				{
					ItemData.getString_0(107259772),
					new ItemDatum(ItemData.getString_0(107259772), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Tiger_Hook)
				},
				{
					ItemData.getString_0(107259787),
					new ItemDatum(ItemData.getString_0(107259787), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Tiger_s_Paw)
				},
				{
					ItemData.getString_0(107259738),
					new ItemDatum(ItemData.getString_0(107259738), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Timeworn_Claw)
				},
				{
					ItemData.getString_0(107432172),
					new ItemDatum(ItemData.getString_0(107432172), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Tomahawk)
				},
				{
					ItemData.getString_0(107259749),
					new ItemDatum(ItemData.getString_0(107259749), Enum16.const_3, Enum15.const_8, 1, 3, Class217.Tornado_Wand)
				},
				{
					ItemData.getString_0(107259188),
					new ItemDatum(ItemData.getString_0(107259188), Enum16.const_7, Enum15.const_8, 1, 3, Class217.Tribal_Club)
				},
				{
					ItemData.getString_0(107432427),
					new ItemDatum(ItemData.getString_0(107432427), Enum16.const_2, Enum15.const_8, 1, 3, Class217.Trisula)
				},
				{
					ItemData.getString_0(107259203),
					new ItemDatum(ItemData.getString_0(107259203), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Twilight_Blade)
				},
				{
					ItemData.getString_0(107259150),
					new ItemDatum(ItemData.getString_0(107259150), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Twin_Claw)
				},
				{
					ItemData.getString_0(107259169),
					new ItemDatum(ItemData.getString_0(107259169), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Tyrant_s_Sekhem)
				},
				{
					ItemData.getString_0(107259148),
					new ItemDatum(ItemData.getString_0(107259148), Enum16.const_4, Enum15.const_8, 2, 3, Class217.Vaal_Blade)
				},
				{
					ItemData.getString_0(107259099),
					new ItemDatum(ItemData.getString_0(107259099), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Vaal_Claw)
				},
				{
					ItemData.getString_0(107259086),
					new ItemDatum(ItemData.getString_0(107259086), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Vaal_Hatchet)
				},
				{
					ItemData.getString_0(107259101),
					new ItemDatum(ItemData.getString_0(107259101), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Vaal_Rapier)
				},
				{
					ItemData.getString_0(107259084),
					new ItemDatum(ItemData.getString_0(107259084), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Vaal_Sceptre)
				},
				{
					ItemData.getString_0(107259035),
					new ItemDatum(ItemData.getString_0(107259035), Enum16.const_4, Enum15.const_8, 1, 3, Class217.Variscite_Blade)
				},
				{
					ItemData.getString_0(107259046),
					new ItemDatum(ItemData.getString_0(107259046), Enum16.const_1, Enum15.const_8, 2, 2, Class217.Void_Fangs)
				},
				{
					ItemData.getString_0(107258997),
					new ItemDatum(ItemData.getString_0(107258997), Enum16.const_8, Enum15.const_8, 2, 3, Class217.Void_Sceptre)
				},
				{
					ItemData.getString_0(107259012),
					new ItemDatum(ItemData.getString_0(107259012), Enum16.const_6, Enum15.const_8, 2, 3, Class217.War_Axe)
				},
				{
					ItemData.getString_0(107258967),
					new ItemDatum(ItemData.getString_0(107258967), Enum16.const_7, Enum15.const_8, 2, 3, Class217.War_Hammer)
				},
				{
					ItemData.getString_0(107258982),
					new ItemDatum(ItemData.getString_0(107258982), Enum16.const_4, Enum15.const_8, 2, 3, Class217.War_Sword)
				},
				{
					ItemData.getString_0(107259449),
					new ItemDatum(ItemData.getString_0(107259449), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Whalebone_Rapier)
				},
				{
					ItemData.getString_0(107259456),
					new ItemDatum(ItemData.getString_0(107259456), Enum16.const_6, Enum15.const_8, 2, 3, Class217.Wraith_Axe)
				},
				{
					ItemData.getString_0(107259407),
					new ItemDatum(ItemData.getString_0(107259407), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Wrist_Chopper)
				},
				{
					ItemData.getString_0(107259386),
					new ItemDatum(ItemData.getString_0(107259386), Enum16.const_7, Enum15.const_8, 2, 3, Class217.Wyrm_Mace)
				},
				{
					ItemData.getString_0(107259373),
					new ItemDatum(ItemData.getString_0(107259373), Enum16.const_5, Enum15.const_8, 1, 4, Class217.Wyrmbone_Rapier)
				},
				{
					ItemData.getString_0(107259352),
					new ItemDatum(ItemData.getString_0(107259352), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Abyssal_Axe)
				},
				{
					ItemData.getString_0(107259367),
					new ItemDatum(ItemData.getString_0(107259367), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Assassin_Bow)
				},
				{
					ItemData.getString_0(107259318),
					new ItemDatum(ItemData.getString_0(107259318), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Banishing_Blade)
				},
				{
					ItemData.getString_0(107259329),
					new ItemDatum(ItemData.getString_0(107259329), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Bastard_Sword)
				},
				{
					ItemData.getString_0(107259308),
					new ItemDatum(ItemData.getString_0(107259308), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Battery_Staff)
				},
				{
					ItemData.getString_0(107259255),
					new ItemDatum(ItemData.getString_0(107259255), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Blasting_Blade)
				},
				{
					ItemData.getString_0(107259266),
					new ItemDatum(ItemData.getString_0(107259266), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Bone_Bow)
				},
				{
					ItemData.getString_0(107259221),
					new ItemDatum(ItemData.getString_0(107259221), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Brass_Maul)
				},
				{
					ItemData.getString_0(107259236),
					new ItemDatum(ItemData.getString_0(107259236), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Butcher_Sword)
				},
				{
					ItemData.getString_0(107258671),
					new ItemDatum(ItemData.getString_0(107258671), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Capacity_Rod)
				},
				{
					ItemData.getString_0(107258686),
					new ItemDatum(ItemData.getString_0(107258686), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Citadel_Bow)
				},
				{
					ItemData.getString_0(107258637),
					new ItemDatum(ItemData.getString_0(107258637), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Coiled_Staff)
				},
				{
					ItemData.getString_0(107258620),
					new ItemDatum(ItemData.getString_0(107258620), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Colossus_Mallet)
				},
				{
					ItemData.getString_0(107258631),
					new ItemDatum(ItemData.getString_0(107258631), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Composite_Bow)
				},
				{
					ItemData.getString_0(107258578),
					new ItemDatum(ItemData.getString_0(107258578), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Compound_Bow)
				},
				{
					ItemData.getString_0(107258593),
					new ItemDatum(ItemData.getString_0(107258593), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Coronal_Maul)
				},
				{
					ItemData.getString_0(107258544),
					new ItemDatum(ItemData.getString_0(107258544), Enum16.const_13, Enum15.const_9, 1, 4, Class217.Corroded_Blade)
				},
				{
					ItemData.getString_0(107258523),
					new ItemDatum(ItemData.getString_0(107258523), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Crescent_Staff)
				},
				{
					ItemData.getString_0(107258534),
					new ItemDatum(ItemData.getString_0(107258534), Enum16.const_11, Enum15.const_9, 2, 3, Class217.Crude_Bow)
				},
				{
					ItemData.getString_0(107258489),
					new ItemDatum(ItemData.getString_0(107258489), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Curved_Blade)
				},
				{
					ItemData.getString_0(107258504),
					new ItemDatum(ItemData.getString_0(107258504), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Dagger_Axe)
				},
				{
					ItemData.getString_0(107258455),
					new ItemDatum(ItemData.getString_0(107258455), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Death_Bow)
				},
				{
					ItemData.getString_0(107258474),
					new ItemDatum(ItemData.getString_0(107258474), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Decimation_Bow)
				},
				{
					ItemData.getString_0(107258933),
					new ItemDatum(ItemData.getString_0(107258933), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Decurve_Bow)
				},
				{
					ItemData.getString_0(107258948),
					new ItemDatum(ItemData.getString_0(107258948), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Despot_Axe)
				},
				{
					ItemData.getString_0(107258899),
					new ItemDatum(ItemData.getString_0(107258899), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Double_Axe)
				},
				{
					ItemData.getString_0(107258914),
					new ItemDatum(ItemData.getString_0(107258914), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Dread_Maul)
				},
				{
					ItemData.getString_0(107258865),
					new ItemDatum(ItemData.getString_0(107258865), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Driftwood_Maul)
				},
				{
					ItemData.getString_0(107258844),
					new ItemDatum(ItemData.getString_0(107258844), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Eclipse_Staff)
				},
				{
					ItemData.getString_0(107258855),
					new ItemDatum(ItemData.getString_0(107258855), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Engraved_Greatsword)
				},
				{
					ItemData.getString_0(107258826),
					new ItemDatum(ItemData.getString_0(107258826), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Etched_Greatsword)
				},
				{
					ItemData.getString_0(107258769),
					new ItemDatum(ItemData.getString_0(107258769), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Exquisite_Blade)
				},
				{
					ItemData.getString_0(107258748),
					new ItemDatum(ItemData.getString_0(107258748), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Eventuality_Rod)
				},
				{
					ItemData.getString_0(107258759),
					new ItemDatum(ItemData.getString_0(107258759), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Ezomyte_Axe)
				},
				{
					ItemData.getString_0(107258710),
					new ItemDatum(ItemData.getString_0(107258710), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Ezomyte_Blade)
				},
				{
					ItemData.getString_0(107258721),
					new ItemDatum(ItemData.getString_0(107258721), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Ezomyte_Staff)
				},
				{
					ItemData.getString_0(107258188),
					new ItemDatum(ItemData.getString_0(107258188), Enum16.const_0, Enum15.const_9, 1, 4, Class217.Fishing_Rod)
				},
				{
					ItemData.getString_0(107475852),
					new ItemDatum(ItemData.getString_0(107475852), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Fleshripper)
				},
				{
					ItemData.getString_0(107258139),
					new ItemDatum(ItemData.getString_0(107258139), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Footman_Sword)
				},
				{
					ItemData.getString_0(107258150),
					new ItemDatum(ItemData.getString_0(107258150), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Foul_Staff)
				},
				{
					ItemData.getString_0(107258101),
					new ItemDatum(ItemData.getString_0(107258101), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Fright_Maul)
				},
				{
					ItemData.getString_0(107258116),
					new ItemDatum(ItemData.getString_0(107258116), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Gilded_Axe)
				},
				{
					ItemData.getString_0(107258067),
					new ItemDatum(ItemData.getString_0(107258067), Enum16.const_12, Enum15.const_9, 1, 4, Class217.Gnarled_Branch)
				},
				{
					ItemData.getString_0(107258078),
					new ItemDatum(ItemData.getString_0(107258078), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Great_Mallet)
				},
				{
					ItemData.getString_0(107258029),
					new ItemDatum(ItemData.getString_0(107258029), Enum16.const_11, Enum15.const_9, 2, 2, Class217.Grove_Bow)
				},
				{
					ItemData.getString_0(107258048),
					new ItemDatum(ItemData.getString_0(107258048), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Harbinger_Bow)
				},
				{
					ItemData.getString_0(107258027),
					new ItemDatum(ItemData.getString_0(107258027), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Headman_s_Sword)
				},
				{
					ItemData.getString_0(107257974),
					new ItemDatum(ItemData.getString_0(107257974), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Headsman_Axe)
				},
				{
					ItemData.getString_0(107257989),
					new ItemDatum(ItemData.getString_0(107257989), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Highborn_Bow)
				},
				{
					ItemData.getString_0(107257940),
					new ItemDatum(ItemData.getString_0(107257940), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Highborn_Staff)
				},
				{
					ItemData.getString_0(107257951),
					new ItemDatum(ItemData.getString_0(107257951), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Highland_Blade)
				},
				{
					ItemData.getString_0(107258442),
					new ItemDatum(ItemData.getString_0(107258442), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Imperial_Bow)
				},
				{
					ItemData.getString_0(107258393),
					new ItemDatum(ItemData.getString_0(107258393), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Imperial_Maul)
				},
				{
					ItemData.getString_0(107258404),
					new ItemDatum(ItemData.getString_0(107258404), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Imperial_Staff)
				},
				{
					ItemData.getString_0(107258351),
					new ItemDatum(ItemData.getString_0(107258351), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Infernal_Sword)
				},
				{
					ItemData.getString_0(107258330),
					new ItemDatum(ItemData.getString_0(107258330), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Iron_Staff)
				},
				{
					ItemData.getString_0(107258345),
					new ItemDatum(ItemData.getString_0(107258345), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Ivory_Bow)
				},
				{
					ItemData.getString_0(107258300),
					new ItemDatum(ItemData.getString_0(107258300), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Jade_Chopper)
				},
				{
					ItemData.getString_0(107258315),
					new ItemDatum(ItemData.getString_0(107258315), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Jagged_Maul)
				},
				{
					ItemData.getString_0(107258266),
					new ItemDatum(ItemData.getString_0(107258266), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Jasper_Chopper)
				},
				{
					ItemData.getString_0(107258277),
					new ItemDatum(ItemData.getString_0(107258277), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Judgement_Staff)
				},
				{
					ItemData.getString_0(107258224),
					new ItemDatum(ItemData.getString_0(107258224), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Karui_Chopper)
				},
				{
					ItemData.getString_0(107258203),
					new ItemDatum(ItemData.getString_0(107258203), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Karui_Maul)
				},
				{
					ItemData.getString_0(107258218),
					new ItemDatum(ItemData.getString_0(107258218), Enum16.const_13, Enum15.const_9, 2, 4)
				},
				{
					ItemData.getString_0(107472864),
					new ItemDatum(ItemData.getString_0(107472864), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Labrys)
				},
				{
					ItemData.getString_0(107472193),
					new ItemDatum(ItemData.getString_0(107472193), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Lathi)
				},
				{
					ItemData.getString_0(107258205),
					new ItemDatum(ItemData.getString_0(107258205), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Lion_Sword)
				},
				{
					ItemData.getString_0(107257676),
					new ItemDatum(ItemData.getString_0(107257676), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Lithe_Blade)
				},
				{
					ItemData.getString_0(107257627),
					new ItemDatum(ItemData.getString_0(107257627), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Long_Bow)
				},
				{
					ItemData.getString_0(107257614),
					new ItemDatum(ItemData.getString_0(107257614), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Long_Staff)
				},
				{
					ItemData.getString_0(107472247),
					new ItemDatum(ItemData.getString_0(107472247), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Longsword)
				},
				{
					ItemData.getString_0(107257629),
					new ItemDatum(ItemData.getString_0(107257629), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Bitmap_0)
				},
				{
					ItemData.getString_0(107471499),
					new ItemDatum(ItemData.getString_0(107471499), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Mallet)
				},
				{
					ItemData.getString_0(107257604),
					new ItemDatum(ItemData.getString_0(107257604), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Maraketh_Bow)
				},
				{
					ItemData.getString_0(107471857),
					new ItemDatum(ItemData.getString_0(107471857), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Meatgrinder)
				},
				{
					ItemData.getString_0(107257555),
					new ItemDatum(ItemData.getString_0(107257555), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Military_Staff)
				},
				{
					ItemData.getString_0(107257566),
					new ItemDatum(ItemData.getString_0(107257566), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Moon_Staff)
				},
				{
					ItemData.getString_0(107257517),
					new ItemDatum(ItemData.getString_0(107257517), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Morning_Star)
				},
				{
					ItemData.getString_0(107257500),
					new ItemDatum(ItemData.getString_0(107257500), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Noble_Axe)
				},
				{
					ItemData.getString_0(107257487),
					new ItemDatum(ItemData.getString_0(107257487), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Ornate_Sword)
				},
				{
					ItemData.getString_0(107470153),
					new ItemDatum(ItemData.getString_0(107470153), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Piledriver)
				},
				{
					ItemData.getString_0(107257502),
					new ItemDatum(ItemData.getString_0(107257502), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Plated_Maul)
				},
				{
					ItemData.getString_0(107470375),
					new ItemDatum(ItemData.getString_0(107470375), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Poleaxe)
				},
				{
					ItemData.getString_0(107257453),
					new ItemDatum(ItemData.getString_0(107257453), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Potentiality_Rod)
				},
				{
					ItemData.getString_0(107257428),
					new ItemDatum(ItemData.getString_0(107257428), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Primitive_Staff)
				},
				{
					ItemData.getString_0(107257439),
					new ItemDatum(ItemData.getString_0(107257439), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Primordial_Staff)
				},
				{
					ItemData.getString_0(107469454),
					new ItemDatum(ItemData.getString_0(107469454), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Quarterstaff)
				},
				{
					ItemData.getString_0(107257926),
					new ItemDatum(ItemData.getString_0(107257926), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Ranger_Bow)
				},
				{
					ItemData.getString_0(107257877),
					new ItemDatum(ItemData.getString_0(107257877), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Reaver_Sword)
				},
				{
					ItemData.getString_0(107257892),
					new ItemDatum(ItemData.getString_0(107257892), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Rebuking_Blade)
				},
				{
					ItemData.getString_0(107257839),
					new ItemDatum(ItemData.getString_0(107257839), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Reciprocation_Staff)
				},
				{
					ItemData.getString_0(107257810),
					new ItemDatum(ItemData.getString_0(107257810), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Recurve_Bow)
				},
				{
					ItemData.getString_0(107257825),
					new ItemDatum(ItemData.getString_0(107257825), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Reflex_Bow)
				},
				{
					ItemData.getString_0(107257776),
					new ItemDatum(ItemData.getString_0(107257776), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Royal_Bow)
				},
				{
					ItemData.getString_0(107257795),
					new ItemDatum(ItemData.getString_0(107257795), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Royal_Staff)
				},
				{
					ItemData.getString_0(107257746),
					new ItemDatum(ItemData.getString_0(107257746), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Serpentine_Staff)
				},
				{
					ItemData.getString_0(107257721),
					new ItemDatum(ItemData.getString_0(107257721), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Shadow_Axe)
				},
				{
					ItemData.getString_0(107257736),
					new ItemDatum(ItemData.getString_0(107257736), Enum16.const_11, Enum15.const_9, 2, 3, Class217.Short_Bow)
				},
				{
					ItemData.getString_0(107467328),
					new ItemDatum(ItemData.getString_0(107467328), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Sledgehammer)
				},
				{
					ItemData.getString_0(107257691),
					new ItemDatum(ItemData.getString_0(107257691), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Sniper_Bow)
				},
				{
					ItemData.getString_0(107257706),
					new ItemDatum(ItemData.getString_0(107257706), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Solar_Maul)
				},
				{
					ItemData.getString_0(107257145),
					new ItemDatum(ItemData.getString_0(107257145), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Spectral_Sword)
				},
				{
					ItemData.getString_0(107257156),
					new ItemDatum(ItemData.getString_0(107257156), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Spine_Bow)
				},
				{
					ItemData.getString_0(107257111),
					new ItemDatum(ItemData.getString_0(107257111), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Spiny_Maul)
				},
				{
					ItemData.getString_0(107465815),
					new ItemDatum(ItemData.getString_0(107465815), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Steelhead)
				},
				{
					ItemData.getString_0(107257126),
					new ItemDatum(ItemData.getString_0(107257126), Enum16.const_11, Enum15.const_9, 2, 4, Class217.Steelwood_Bow)
				},
				{
					ItemData.getString_0(107257073),
					new ItemDatum(ItemData.getString_0(107257073), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Stone_Axe)
				},
				{
					ItemData.getString_0(107257092),
					new ItemDatum(ItemData.getString_0(107257092), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Sundering_Axe)
				},
				{
					ItemData.getString_0(107257039),
					new ItemDatum(ItemData.getString_0(107257039), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Talon_Axe)
				},
				{
					ItemData.getString_0(107257058),
					new ItemDatum(ItemData.getString_0(107257058), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Terror_Maul)
				},
				{
					ItemData.getString_0(107257009),
					new ItemDatum(ItemData.getString_0(107257009), Enum16.const_11, Enum15.const_9, 2, 3, Class217.Thicket_Bow)
				},
				{
					ItemData.getString_0(107257024),
					new ItemDatum(ItemData.getString_0(107257024), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Tiger_Sword)
				},
				{
					ItemData.getString_0(107256975),
					new ItemDatum(ItemData.getString_0(107256975), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Timber_Axe)
				},
				{
					ItemData.getString_0(107256990),
					new ItemDatum(ItemData.getString_0(107256990), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Totemic_Maul)
				},
				{
					ItemData.getString_0(107256941),
					new ItemDatum(ItemData.getString_0(107256941), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Transformer_Staff)
				},
				{
					ItemData.getString_0(107256916),
					new ItemDatum(ItemData.getString_0(107256916), Enum16.const_15, Enum15.const_9, 2, 4, Class217.Tribal_Maul)
				},
				{
					ItemData.getString_0(107256931),
					new ItemDatum(ItemData.getString_0(107256931), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Two_Handed_Sword)
				},
				{
					ItemData.getString_0(107257418),
					new ItemDatum(ItemData.getString_0(107257418), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Vaal_Axe)
				},
				{
					ItemData.getString_0(107257405),
					new ItemDatum(ItemData.getString_0(107257405), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Vaal_Greatsword)
				},
				{
					ItemData.getString_0(107257384),
					new ItemDatum(ItemData.getString_0(107257384), Enum16.const_16, Enum15.const_9, 2, 4, Class217.Vile_Staff)
				},
				{
					ItemData.getString_0(107257335),
					new ItemDatum(ItemData.getString_0(107257335), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Void_Axe)
				},
				{
					ItemData.getString_0(107257354),
					new ItemDatum(ItemData.getString_0(107257354), Enum16.const_12, Enum15.const_9, 2, 4, Class217.Woodful_Staff)
				},
				{
					ItemData.getString_0(107431311),
					new ItemDatum(ItemData.getString_0(107431311), Enum16.const_14, Enum15.const_9, 2, 4, Class217.Woodsplitter)
				},
				{
					ItemData.getString_0(107257301),
					new ItemDatum(ItemData.getString_0(107257301), Enum16.const_13, Enum15.const_9, 2, 4, Class217.Wraith_Sword)
				},
				{
					ItemData.getString_0(107257316),
					new ItemDatum(ItemData.getString_0(107257316), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Artillery_Quiver)
				},
				{
					ItemData.getString_0(107257291),
					new ItemDatum(ItemData.getString_0(107257291), Enum16.const_59, Enum15.const_10, 2, 3)
				},
				{
					ItemData.getString_0(107257230),
					new ItemDatum(ItemData.getString_0(107257230), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Blunt_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107257205),
					new ItemDatum(ItemData.getString_0(107257205), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Broadhead_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107257172),
					new ItemDatum(ItemData.getString_0(107257172), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Conductive_Quiver)
				},
				{
					ItemData.getString_0(107256635),
					new ItemDatum(ItemData.getString_0(107256635), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Cured_Quiver)
				},
				{
					ItemData.getString_0(107256650),
					new ItemDatum(ItemData.getString_0(107256650), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Fire_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256593),
					new ItemDatum(ItemData.getString_0(107256593), Enum16.const_59, Enum15.const_10, 2, 3)
				},
				{
					ItemData.getString_0(107256560),
					new ItemDatum(ItemData.getString_0(107256560), Enum16.const_59, Enum15.const_10, 2, 3)
				},
				{
					ItemData.getString_0(107256535),
					new ItemDatum(ItemData.getString_0(107256535), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Heavy_Quiver)
				},
				{
					ItemData.getString_0(107256550),
					new ItemDatum(ItemData.getString_0(107256550), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Light_Quiver)
				},
				{
					ItemData.getString_0(107256501),
					new ItemDatum(ItemData.getString_0(107256501), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Ornate_Quiver)
				},
				{
					ItemData.getString_0(107256512),
					new ItemDatum(ItemData.getString_0(107256512), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Penetrating_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256479),
					new ItemDatum(ItemData.getString_0(107256479), Enum16.const_59, Enum15.const_10, 2, 3)
				},
				{
					ItemData.getString_0(107256450),
					new ItemDatum(ItemData.getString_0(107256450), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Rugged_Quiver)
				},
				{
					ItemData.getString_0(107256397),
					new ItemDatum(ItemData.getString_0(107256397), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Serrated_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256880),
					new ItemDatum(ItemData.getString_0(107256880), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Sharktooth_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256847),
					new ItemDatum(ItemData.getString_0(107256847), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Two_Point_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256814),
					new ItemDatum(ItemData.getString_0(107256814), Enum16.const_59, Enum15.const_10, 2, 3, Class217.Spike_Point_Arrow_Quiver)
				},
				{
					ItemData.getString_0(107256781),
					new ItemDatum(ItemData.getString_0(107256781), Enum16.const_59, Enum15.const_10, 2, 3)
				},
				{
					ItemData.getString_0(107256756),
					new ItemDatum(ItemData.getString_0(107256756), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256767),
					new ItemDatum(ItemData.getString_0(107256767), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256742),
					new ItemDatum(ItemData.getString_0(107256742), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256693),
					new ItemDatum(ItemData.getString_0(107256693), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256704),
					new ItemDatum(ItemData.getString_0(107256704), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256675),
					new ItemDatum(ItemData.getString_0(107256675), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256134),
					new ItemDatum(ItemData.getString_0(107256134), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256105),
					new ItemDatum(ItemData.getString_0(107256105), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256052),
					new ItemDatum(ItemData.getString_0(107256052), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256063),
					new ItemDatum(ItemData.getString_0(107256063), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256038),
					new ItemDatum(ItemData.getString_0(107256038), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255981),
					new ItemDatum(ItemData.getString_0(107255981), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255956),
					new ItemDatum(ItemData.getString_0(107255956), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255931),
					new ItemDatum(ItemData.getString_0(107255931), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255938),
					new ItemDatum(ItemData.getString_0(107255938), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255913),
					new ItemDatum(ItemData.getString_0(107255913), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256376),
					new ItemDatum(ItemData.getString_0(107256376), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256383),
					new ItemDatum(ItemData.getString_0(107256383), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256358),
					new ItemDatum(ItemData.getString_0(107256358), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256305),
					new ItemDatum(ItemData.getString_0(107256305), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256280),
					new ItemDatum(ItemData.getString_0(107256280), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256287),
					new ItemDatum(ItemData.getString_0(107256287), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256258),
					new ItemDatum(ItemData.getString_0(107256258), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256229),
					new ItemDatum(ItemData.getString_0(107256229), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256200),
					new ItemDatum(ItemData.getString_0(107256200), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256151),
					new ItemDatum(ItemData.getString_0(107256151), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107256166),
					new ItemDatum(ItemData.getString_0(107256166), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255597),
					new ItemDatum(ItemData.getString_0(107255597), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255572),
					new ItemDatum(ItemData.getString_0(107255572), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255547),
					new ItemDatum(ItemData.getString_0(107255547), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255550),
					new ItemDatum(ItemData.getString_0(107255550), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255525),
					new ItemDatum(ItemData.getString_0(107255525), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255500),
					new ItemDatum(ItemData.getString_0(107255500), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255451),
					new ItemDatum(ItemData.getString_0(107255451), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255458),
					new ItemDatum(ItemData.getString_0(107255458), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255409),
					new ItemDatum(ItemData.getString_0(107255409), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255380),
					new ItemDatum(ItemData.getString_0(107255380), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255867),
					new ItemDatum(ItemData.getString_0(107255867), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255874),
					new ItemDatum(ItemData.getString_0(107255874), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255845),
					new ItemDatum(ItemData.getString_0(107255845), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255816),
					new ItemDatum(ItemData.getString_0(107255816), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255763),
					new ItemDatum(ItemData.getString_0(107255763), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255778),
					new ItemDatum(ItemData.getString_0(107255778), Enum16.const_68, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255753),
					new ItemDatum(ItemData.getString_0(107255753), Enum16.const_66, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255696),
					new ItemDatum(ItemData.getString_0(107255696), Enum16.const_67, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255671),
					new ItemDatum(ItemData.getString_0(107255671), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255682),
					new ItemDatum(ItemData.getString_0(107255682), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255629),
					new ItemDatum(ItemData.getString_0(107255629), Enum16.const_69, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107255100),
					new ItemDatum(ItemData.getString_0(107255100), Enum16.const_19, Enum15.const_10, 1, 1, Class217.Cobalt_Jewel)
				},
				{
					ItemData.getString_0(107255115),
					new ItemDatum(ItemData.getString_0(107255115), Enum16.const_17, Enum15.const_10, 1, 1, Class217.Crimson_Jewel)
				},
				{
					ItemData.getString_0(107255062),
					new ItemDatum(ItemData.getString_0(107255062), Enum16.const_24, Enum15.const_10, 1, 1, Class217.Ghastly_Eye_Jewel)
				},
				{
					ItemData.getString_0(107255069),
					new ItemDatum(ItemData.getString_0(107255069), Enum16.const_23, Enum15.const_10, 1, 1, Class217.Hypnotic_Eye_Jewel)
				},
				{
					ItemData.getString_0(107439378),
					new ItemDatum(ItemData.getString_0(107439378), Enum16.const_26, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107439407),
					new ItemDatum(ItemData.getString_0(107439407), Enum16.const_27, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255044),
					new ItemDatum(ItemData.getString_0(107255044), Enum16.const_21, Enum15.const_10, 1, 1, Class217.Murderous_Eye_Jewel)
				},
				{
					ItemData.getString_0(107255015),
					new ItemDatum(ItemData.getString_0(107255015), Enum16.const_20, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254962),
					new ItemDatum(ItemData.getString_0(107254962), Enum16.const_22, Enum15.const_10, 1, 1, Class217.Searching_Eye_Jewel)
				},
				{
					ItemData.getString_0(107439980),
					new ItemDatum(ItemData.getString_0(107439980), Enum16.const_28, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254933),
					new ItemDatum(ItemData.getString_0(107254933), Enum16.const_18, Enum15.const_10, 1, 1, Class217.Viridian_Jewel)
				},
				{
					ItemData.getString_0(107254944),
					new ItemDatum(ItemData.getString_0(107254944), Enum16.const_25, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254923),
					new ItemDatum(ItemData.getString_0(107254923), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254866),
					new ItemDatum(ItemData.getString_0(107254866), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255345),
					new ItemDatum(ItemData.getString_0(107255345), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255316),
					new ItemDatum(ItemData.getString_0(107255316), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255287),
					new ItemDatum(ItemData.getString_0(107255287), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255246),
					new ItemDatum(ItemData.getString_0(107255246), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255221),
					new ItemDatum(ItemData.getString_0(107255221), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255192),
					new ItemDatum(ItemData.getString_0(107255192), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255159),
					new ItemDatum(ItemData.getString_0(107255159), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255166),
					new ItemDatum(ItemData.getString_0(107255166), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107255141),
					new ItemDatum(ItemData.getString_0(107255141), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254600),
					new ItemDatum(ItemData.getString_0(107254600), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254543),
					new ItemDatum(ItemData.getString_0(107254543), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254518),
					new ItemDatum(ItemData.getString_0(107254518), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254529),
					new ItemDatum(ItemData.getString_0(107254529), Enum16.const_70, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254504),
					new ItemDatum(ItemData.getString_0(107254504), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254447),
					new ItemDatum(ItemData.getString_0(107254447), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254442),
					new ItemDatum(ItemData.getString_0(107254442), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254381),
					new ItemDatum(ItemData.getString_0(107254381), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254352),
					new ItemDatum(ItemData.getString_0(107254352), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254855),
					new ItemDatum(ItemData.getString_0(107254855), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254798),
					new ItemDatum(ItemData.getString_0(107254798), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254769),
					new ItemDatum(ItemData.getString_0(107254769), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254764),
					new ItemDatum(ItemData.getString_0(107254764), Enum16.const_71, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254707),
					new ItemDatum(ItemData.getString_0(107254707), Enum16.const_72, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254674),
					new ItemDatum(ItemData.getString_0(107254674), Enum16.const_72, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254637),
					new ItemDatum(ItemData.getString_0(107254637), Enum16.const_72, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254608),
					new ItemDatum(ItemData.getString_0(107254608), Enum16.const_72, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254091),
					new ItemDatum(ItemData.getString_0(107254091), Enum16.const_73, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254034),
					new ItemDatum(ItemData.getString_0(107254034), Enum16.const_73, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254005),
					new ItemDatum(ItemData.getString_0(107254005), Enum16.const_73, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253976),
					new ItemDatum(ItemData.getString_0(107253976), Enum16.const_73, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253983),
					new ItemDatum(ItemData.getString_0(107253983), Enum16.const_74, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253934),
					new ItemDatum(ItemData.getString_0(107253934), Enum16.const_74, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253949),
					new ItemDatum(ItemData.getString_0(107253949), Enum16.const_74, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253924),
					new ItemDatum(ItemData.getString_0(107253924), Enum16.const_74, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253875),
					new ItemDatum(ItemData.getString_0(107253875), Enum16.const_75, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253886),
					new ItemDatum(ItemData.getString_0(107253886), Enum16.const_75, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253861),
					new ItemDatum(ItemData.getString_0(107253861), Enum16.const_75, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254320),
					new ItemDatum(ItemData.getString_0(107254320), Enum16.const_75, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254299),
					new ItemDatum(ItemData.getString_0(107254299), Enum16.const_76, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254310),
					new ItemDatum(ItemData.getString_0(107254310), Enum16.const_76, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254257),
					new ItemDatum(ItemData.getString_0(107254257), Enum16.const_76, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254236),
					new ItemDatum(ItemData.getString_0(107254236), Enum16.const_76, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254247),
					new ItemDatum(ItemData.getString_0(107254247), Enum16.const_77, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254218),
					new ItemDatum(ItemData.getString_0(107254218), Enum16.const_77, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254161),
					new ItemDatum(ItemData.getString_0(107254161), Enum16.const_77, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254128),
					new ItemDatum(ItemData.getString_0(107254128), Enum16.const_77, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107254119),
					new ItemDatum(ItemData.getString_0(107254119), Enum16.const_78, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253578),
					new ItemDatum(ItemData.getString_0(107253578), Enum16.const_78, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253517),
					new ItemDatum(ItemData.getString_0(107253517), Enum16.const_78, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253488),
					new ItemDatum(ItemData.getString_0(107253488), Enum16.const_78, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253459),
					new ItemDatum(ItemData.getString_0(107253459), Enum16.const_79, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253474),
					new ItemDatum(ItemData.getString_0(107253474), Enum16.const_79, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253421),
					new ItemDatum(ItemData.getString_0(107253421), Enum16.const_79, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253404),
					new ItemDatum(ItemData.getString_0(107253404), Enum16.const_79, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253407),
					new ItemDatum(ItemData.getString_0(107253407), Enum16.const_80, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253382),
					new ItemDatum(ItemData.getString_0(107253382), Enum16.const_80, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253325),
					new ItemDatum(ItemData.getString_0(107253325), Enum16.const_80, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253812),
					new ItemDatum(ItemData.getString_0(107253812), Enum16.const_80, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253783),
					new ItemDatum(ItemData.getString_0(107253783), Enum16.const_81, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253798),
					new ItemDatum(ItemData.getString_0(107253798), Enum16.const_81, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253749),
					new ItemDatum(ItemData.getString_0(107253749), Enum16.const_81, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253764),
					new ItemDatum(ItemData.getString_0(107253764), Enum16.const_81, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253711),
					new ItemDatum(ItemData.getString_0(107253711), Enum16.const_82, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253686),
					new ItemDatum(ItemData.getString_0(107253686), Enum16.const_82, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253657),
					new ItemDatum(ItemData.getString_0(107253657), Enum16.const_82, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253624),
					new ItemDatum(ItemData.getString_0(107253624), Enum16.const_82, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253595),
					new ItemDatum(ItemData.getString_0(107253595), Enum16.const_83, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253610),
					new ItemDatum(ItemData.getString_0(107253610), Enum16.const_83, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253045),
					new ItemDatum(ItemData.getString_0(107253045), Enum16.const_84, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253020),
					new ItemDatum(ItemData.getString_0(107253020), Enum16.const_84, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253023),
					new ItemDatum(ItemData.getString_0(107253023), Enum16.const_85, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252974),
					new ItemDatum(ItemData.getString_0(107252974), Enum16.const_85, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252953),
					new ItemDatum(ItemData.getString_0(107252953), Enum16.const_85, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252968),
					new ItemDatum(ItemData.getString_0(107252968), Enum16.const_85, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252939),
					new ItemDatum(ItemData.getString_0(107252939), Enum16.const_86, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252886),
					new ItemDatum(ItemData.getString_0(107252886), Enum16.const_86, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252897),
					new ItemDatum(ItemData.getString_0(107252897), Enum16.const_86, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252876),
					new ItemDatum(ItemData.getString_0(107252876), Enum16.const_86, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107252823),
					new ItemDatum(ItemData.getString_0(107252823), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253318),
					new ItemDatum(ItemData.getString_0(107253318), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253277),
					new ItemDatum(ItemData.getString_0(107253277), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253200),
					new ItemDatum(ItemData.getString_0(107253200), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253191),
					new ItemDatum(ItemData.getString_0(107253191), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253150),
					new ItemDatum(ItemData.getString_0(107253150), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107253069),
					new ItemDatum(ItemData.getString_0(107253069), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285312),
					new ItemDatum(ItemData.getString_0(107285312), Enum16.const_87, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285235),
					new ItemDatum(ItemData.getString_0(107285235), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285218),
					new ItemDatum(ItemData.getString_0(107285218), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285145),
					new ItemDatum(ItemData.getString_0(107285145), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285132),
					new ItemDatum(ItemData.getString_0(107285132), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285091),
					new ItemDatum(ItemData.getString_0(107285091), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285530),
					new ItemDatum(ItemData.getString_0(107285530), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285513),
					new ItemDatum(ItemData.getString_0(107285513), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285436),
					new ItemDatum(ItemData.getString_0(107285436), Enum16.const_88, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285391),
					new ItemDatum(ItemData.getString_0(107285391), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107285374),
					new ItemDatum(ItemData.getString_0(107285374), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284789),
					new ItemDatum(ItemData.getString_0(107284789), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284776),
					new ItemDatum(ItemData.getString_0(107284776), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284735),
					new ItemDatum(ItemData.getString_0(107284735), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284662),
					new ItemDatum(ItemData.getString_0(107284662), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284645),
					new ItemDatum(ItemData.getString_0(107284645), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107284568),
					new ItemDatum(ItemData.getString_0(107284568), Enum16.const_89, Enum15.const_10, 1, 1)
				},
				{
					ItemData.getString_0(107358209),
					new ItemDatum(ItemData.getString_0(107358209), Enum16.const_0, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107358057),
					new ItemDatum(ItemData.getString_0(107358057), Enum16.const_0, Enum15.const_10, 1, 2)
				},
				{
					ItemData.getString_0(107358140),
					new ItemDatum(ItemData.getString_0(107358140), Enum16.const_0, Enum15.const_10, 2, 2)
				},
				{
					ItemData.getString_0(107357512),
					new ItemDatum(ItemData.getString_0(107357512), Enum16.const_0, Enum15.const_10, 2, 2)
				},
				{
					ItemData.getString_0(107358099),
					new ItemDatum(ItemData.getString_0(107358099), Enum16.const_0, Enum15.const_10, 2, 2)
				},
				{
					ItemData.getString_0(107357475),
					new ItemDatum(ItemData.getString_0(107357475), Enum16.const_0, Enum15.const_10, 2, 2)
				}
			};
			ItemData.dictionary_1 = new Dictionary<Enum16, string>
			{
				{
					Enum16.const_1,
					Class237.Claw
				},
				{
					Enum16.const_2,
					Class237.Dagger
				},
				{
					Enum16.const_3,
					Class237.Wand
				},
				{
					Enum16.const_4,
					Class237.OHSword
				},
				{
					Enum16.const_5,
					Class237.ThrustingOHSword
				},
				{
					Enum16.const_6,
					Class237.OHAxe
				},
				{
					Enum16.const_7,
					Class237.OHMace
				},
				{
					Enum16.const_8,
					Class237.Sceptre
				},
				{
					Enum16.const_9,
					Class237.RuneDagger
				},
				{
					Enum16.const_10,
					Class237.ConvokingWand
				},
				{
					Enum16.const_11,
					Class237.Bow
				},
				{
					Enum16.const_12,
					Class237.Staff
				},
				{
					Enum16.const_13,
					Class237.THSword
				},
				{
					Enum16.const_14,
					Class237.THAxe
				},
				{
					Enum16.const_15,
					Class237.THMace
				},
				{
					Enum16.const_16,
					Class237.Warstaff
				},
				{
					Enum16.const_17,
					Class237.CrimsonJewel
				},
				{
					Enum16.const_18,
					Class237.ViridianJewel
				},
				{
					Enum16.const_19,
					Class237.CobaltJewel
				},
				{
					Enum16.const_20,
					Class237.PrismaticJewel
				},
				{
					Enum16.const_21,
					Class237.MurderousEyeJewel
				},
				{
					Enum16.const_22,
					Class237.SearchingEyeJewel
				},
				{
					Enum16.const_23,
					Class237.HypnoticEyeJewel
				},
				{
					Enum16.const_24,
					Class237.GhastlyEyeJewel
				},
				{
					Enum16.const_25,
					Class237.TimelessJewel
				},
				{
					Enum16.const_29,
					Class237.Amulet
				},
				{
					Enum16.const_30,
					Class237.Ring
				},
				{
					Enum16.const_31,
					Class237.UnsetRing
				},
				{
					Enum16.const_32,
					Class237.Belt
				},
				{
					Enum16.const_33,
					Class237.Trinket
				},
				{
					Enum16.const_34,
					Class237.GlovesStr
				},
				{
					Enum16.const_35,
					Class237.GlovesDex
				},
				{
					Enum16.const_36,
					Class237.GlovesInt
				},
				{
					Enum16.const_37,
					Class237.GlovesStrDex
				},
				{
					Enum16.const_38,
					Class237.GlovesStrInt
				},
				{
					Enum16.const_39,
					Class237.GlovesDexInt
				},
				{
					Enum16.const_40,
					Class237.BootsStr
				},
				{
					Enum16.const_41,
					Class237.BootsDex
				},
				{
					Enum16.const_42,
					Class237.BootsInt
				},
				{
					Enum16.const_43,
					Class237.BootsStrDex
				},
				{
					Enum16.const_44,
					Class237.BootsStrInt
				},
				{
					Enum16.const_45,
					Class237.BootsDexInt
				},
				{
					Enum16.const_46,
					Class237.ArmorStr
				},
				{
					Enum16.const_47,
					Class237.ArmorDex
				},
				{
					Enum16.const_48,
					Class237.ArmorInt
				},
				{
					Enum16.const_49,
					Class237.ArmorStrDex
				},
				{
					Enum16.const_50,
					Class237.ArmorStrInt
				},
				{
					Enum16.const_51,
					Class237.ArmorDexInt
				},
				{
					Enum16.const_52,
					Class237.ArmorStrDexInt
				},
				{
					Enum16.const_53,
					Class237.HelmetStr
				},
				{
					Enum16.const_54,
					Class237.HelmetDex
				},
				{
					Enum16.const_55,
					Class237.HelmetInt
				},
				{
					Enum16.const_56,
					Class237.HelmetStrDex
				},
				{
					Enum16.const_57,
					Class237.HelmetStrInt
				},
				{
					Enum16.const_58,
					Class237.HelmetDexInt
				},
				{
					Enum16.const_59,
					Class237.Quiver
				},
				{
					Enum16.const_60,
					Class237.ShieldStr
				},
				{
					Enum16.const_61,
					Class237.ShieldDex
				},
				{
					Enum16.const_62,
					Class237.ShieldInt
				},
				{
					Enum16.const_63,
					Class237.ShieldStrDex
				},
				{
					Enum16.const_64,
					Class237.ShieldStrInt
				},
				{
					Enum16.const_65,
					Class237.ShieldDexInt
				},
				{
					Enum16.const_66,
					Class237.LifeFlask
				},
				{
					Enum16.const_67,
					Class237.ManaFlask
				},
				{
					Enum16.const_68,
					Class237.HybridFlask
				},
				{
					Enum16.const_69,
					Class237.UtilityFlask
				},
				{
					Enum16.const_70,
					Class237.Contract
				},
				{
					Enum16.const_71,
					Class237.Blueprint
				},
				{
					Enum16.const_72,
					Class237.SharpeningStone
				},
				{
					Enum16.const_73,
					Class237.Arrowhead
				},
				{
					Enum16.const_74,
					Class237.SpellStone
				},
				{
					Enum16.const_75,
					Class237.Lockpick
				},
				{
					Enum16.const_76,
					Class237.Bracer
				},
				{
					Enum16.const_77,
					Class237.SensingCharm
				},
				{
					Enum16.const_78,
					Class237.Flashpowder
				},
				{
					Enum16.const_79,
					Class237.Ward
				},
				{
					Enum16.const_80,
					Class237.Keyring
				},
				{
					Enum16.const_81,
					Class237.Sole
				},
				{
					Enum16.const_82,
					Class237.DisguiseKit
				},
				{
					Enum16.const_83,
					Class237.Drill
				},
				{
					Enum16.const_84,
					Class237.Blowtorch
				},
				{
					Enum16.const_85,
					Class237.Cloak
				},
				{
					Enum16.const_86,
					Class237.Brooch
				},
				{
					Enum16.const_87,
					Class237.ChromiumWatchstone
				},
				{
					Enum16.const_88,
					Class237.TitaniumWatchstone
				},
				{
					Enum16.const_89,
					Class237.PlatinumWatchstone
				}
			};
		}

		private static Dictionary<string, ItemDatum> dictionary_0;

		private static Dictionary<Enum16, string> dictionary_1;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class298
		{
			internal bool method_0(ItemDatum itemDatum_0)
			{
				return itemDatum_0.Category == this.enum15_0;
			}

			public Enum15 enum15_0;
		}
	}
}
