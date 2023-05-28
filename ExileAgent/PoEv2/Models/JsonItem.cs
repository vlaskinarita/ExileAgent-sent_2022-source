using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using ns30;
using PoEv2.Classes;
using PoEv2.Models.Api;
using PoEv2.Models.Items;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class JsonItem
	{
		public bool verified { get; set; }

		public int w { get; set; }

		public int h { get; set; }

		public int x { get; set; }

		public int y { get; set; }

		public string league { get; set; }

		public string id { get; set; }

		public JsonItem.GClass11 influences { get; set; }

		public string name { get; set; }

		public string typeLine { get; set; }

		public bool identified { get; set; } = true;

		public bool corrupted { get; set; } = false;

		public bool awakened { get; set; } = false;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int ilvl { get; set; }

		public List<JsonItem.GClass10> requirements { get; set; }

		public List<string> implicitMods { get; set; }

		public List<string> explicitMods { get; set; }

		public List<string> craftedMods { get; set; }

		public List<string> enchantMods { get; set; }

		public List<string> fracturedMods { get; set; }

		public string inventoryId { get; set; }

		public string note { get; set; }

		public int frameType { get; set; }

		public string icon { get; set; }

		public List<string> flavourText { get; set; }

		public string descrText { get; set; }

		public JsonItem.GClass7 extended { get; set; }

		public JsonItem.GClass8 hybrid { get; set; }

		public bool delve { get; set; }

		public string prophecyText { get; set; }

		public bool synthesised { get; set; }

		public List<object> socketedItems { get; set; }

		public object incubatedItem { get; set; }

		public List<JsonItem.Sockets> sockets { get; set; }

		public int stackSize { get; set; }

		public int maxStackSize { get; set; }

		public List<JsonItem.GClass9> properties { get; set; }

		public Bitmap ItemImage { get; set; }

		public int SocketCount
		{
			get
			{
				return (this.sockets != null) ? this.sockets.Count : 0;
			}
		}

		public List<string> SocketColour
		{
			get
			{
				List<string> result;
				if (this.sockets == null)
				{
					result = new List<string>();
				}
				else
				{
					result = this.sockets.Select(new Func<JsonItem.Sockets, string>(JsonItem.<>c.<>9.method_0)).ToList<string>();
				}
				return result;
			}
		}

		public string Name
		{
			get
			{
				return string.Format(JsonItem.getString_0(107396341), this.name, this.typeLine).Trim();
			}
		}

		public string ToString()
		{
			return string.Format(JsonItem.getString_0(107442128), this.Name, this.x, this.y);
		}

		public int Left
		{
			get
			{
				return this.x;
			}
		}

		public int Top
		{
			get
			{
				return this.y;
			}
		}

		public bool IsSeed
		{
			get
			{
				return this.SeedLevel > 0;
			}
		}

		public bool IsMap
		{
			get
			{
				return this.MapTier > 0 || this.typeLine.smethod_9(JsonItem.getString_0(107369574));
			}
		}

		public bool IsBeast
		{
			get
			{
				return !string.IsNullOrEmpty(this.method_4(JsonItem.getString_0(107442075)));
			}
		}

		public ItemRarity ItemRarity
		{
			get
			{
				return (ItemRarity)this.frameType;
			}
		}

		public bool IsUnique
		{
			get
			{
				return this.ItemRarity == ItemRarity.Unique;
			}
		}

		public bool IsGem
		{
			get
			{
				return this.ItemRarity == ItemRarity.Gem;
			}
		}

		public bool IsWatchstone
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107379375));
			}
		}

		public bool IsFractured
		{
			get
			{
				return this.fracturedMods != null;
			}
		}

		public bool IsHeist
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107442098)) || this.typeLine.smethod_9(JsonItem.getString_0(107441537));
			}
		}

		public bool IsMavenInvitation
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107451933));
			}
		}

		public bool IsInvitation
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107441552));
			}
		}

		public bool IsFlask
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107441503));
			}
		}

		public string CleanedTypeLine
		{
			get
			{
				return API.smethod_15(this);
			}
		}

		public string CleanedName
		{
			get
			{
				return this.Name.Replace(JsonItem.getString_0(107368251), JsonItem.getString_0(107397927));
			}
		}

		public bool IsBlightedMap
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107452964)) && this.typeLine.smethod_9(JsonItem.getString_0(107369574));
			}
		}

		public bool IsCard
		{
			get
			{
				return this.ItemRarity == ItemRarity.Divination;
			}
		}

		public bool IsPriced
		{
			get
			{
				return this.note.smethod_9(JsonItem.getString_0(107368066)) || this.note.smethod_9(JsonItem.getString_0(107441526));
			}
		}

		public bool IsClusterJewel
		{
			get
			{
				return this.typeLine.smethod_9(JsonItem.getString_0(107441517));
			}
		}

		public JsonItem()
		{
		}

		public JsonItem(Item item, int x, int y)
		{
			this.name = item.name;
			this.typeLine = item.typeLine;
			this.stackSize = item.stack;
			this.maxStackSize = item.stack_size;
			this.x = x;
			this.y = y;
			this.w = item.width;
			this.h = item.height;
			this.ilvl = item.item_level;
			if (item.rarity != null)
			{
				this.frameType = (int)Enum.Parse(typeof(ItemRarity), item.rarity);
			}
			this.corrupted = item.corrupted;
			this.implicitMods = item.implicits;
			this.enchantMods = item.enchants;
			if (item.IsCard)
			{
				this.frameType = 6;
			}
			foreach (string text in item.Properties)
			{
				string[] array = text.smethod_18(JsonItem.getString_0(107441496));
				if (array.Count<string>() > 1)
				{
					string string_ = array[0];
					string string_2 = array[1].Replace(JsonItem.getString_0(107441491), JsonItem.getString_0(107397927));
					this.method_0(string_, string_2, text.Contains(JsonItem.getString_0(107441442)) ? 1 : 0);
				}
				else
				{
					this.method_1(text);
				}
			}
			if (!string.IsNullOrEmpty(item.ExplicitMods))
			{
				List<string> list = new List<string>(item.ExplicitMods.smethod_19(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
				foreach (string text2 in list)
				{
					if (text2.EndsWith(JsonItem.getString_0(107441461)))
					{
						if (this.fracturedMods == null)
						{
							this.fracturedMods = new List<string>();
						}
						this.fracturedMods.Add(text2.Replace(JsonItem.getString_0(107441412), JsonItem.getString_0(107397927)));
					}
					else
					{
						if (this.explicitMods == null)
						{
							this.explicitMods = new List<string>();
						}
						this.explicitMods.Add(text2);
					}
				}
			}
			if (item.text.Contains(JsonItem.getString_0(107441427)))
			{
				this.frameType = 8;
				this.prophecyText = item.text;
			}
		}

		public unsafe bool HasSocketedItemsInside
		{
			get
			{
				void* ptr = stackalloc byte[2];
				*(byte*)ptr = (this.delve ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = (this.descrText.Contains(JsonItem.getString_0(107441358)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						return true;
					}
				}
				return this.socketedItems != null && this.socketedItems.Any<object>();
			}
		}

		public unsafe Dictionary<int, int> SocketLinks
		{
			get
			{
				void* ptr = stackalloc byte[6];
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				((byte*)ptr)[4] = ((this.sockets != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					foreach (JsonItem.Sockets sockets in this.sockets)
					{
						((byte*)ptr)[5] = (dictionary.ContainsKey(sockets.group) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							Dictionary<int, int> dictionary2 = dictionary;
							*(int*)ptr = sockets.group;
							dictionary2[*(int*)ptr] = dictionary2[*(int*)ptr] + 1;
						}
						else
						{
							dictionary.Add(sockets.group, 1);
						}
					}
				}
				return dictionary;
			}
		}

		public unsafe int Links
		{
			get
			{
				void* ptr = stackalloc byte[9];
				*(int*)ptr = 0;
				foreach (KeyValuePair<int, int> keyValuePair in this.SocketLinks)
				{
					((byte*)ptr)[8] = ((Math.Max(keyValuePair.Value, *(int*)ptr) > *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						*(int*)ptr = keyValuePair.Value;
					}
				}
				*(int*)((byte*)ptr + 4) = *(int*)ptr;
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int stack
		{
			get
			{
				void* ptr = stackalloc byte[5];
				((byte*)ptr)[4] = ((this.stackSize == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					this.stackSize = 1;
				}
				*(int*)ptr = this.stackSize;
				return *(int*)ptr;
			}
			set
			{
				this.stackSize = value;
			}
		}

		public unsafe int stack_size
		{
			get
			{
				void* ptr = stackalloc byte[5];
				((byte*)ptr)[4] = ((this.maxStackSize == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					this.maxStackSize = 1;
				}
				*(int*)ptr = this.maxStackSize;
				return *(int*)ptr;
			}
			set
			{
				this.maxStackSize = value;
			}
		}

		public unsafe int MapTier
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string s = this.method_4(JsonItem.getString_0(107441329));
				((byte*)ptr)[8] = (s.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(s, out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int AreaLevel
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string s = this.method_4(JsonItem.getString_0(107441796));
				((byte*)ptr)[8] = (s.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(s, out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int Quality
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string text = this.method_4(JsonItem.getString_0(107441811));
				((byte*)ptr)[8] = (text.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(text.Replace(JsonItem.getString_0(107391111), JsonItem.getString_0(107397927)).Replace(JsonItem.getString_0(107415938), JsonItem.getString_0(107397927)), out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int ItemQuantity
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string text = this.method_4(JsonItem.getString_0(107441766));
				((byte*)ptr)[8] = (text.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(text.Replace(JsonItem.getString_0(107391111), JsonItem.getString_0(107397927)).Replace(JsonItem.getString_0(107415938), JsonItem.getString_0(107397927)), out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int PackSize
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string text = this.method_4(JsonItem.getString_0(107441777));
				((byte*)ptr)[8] = (text.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(text.Replace(JsonItem.getString_0(107391111), JsonItem.getString_0(107397927)).Replace(JsonItem.getString_0(107415938), JsonItem.getString_0(107397927)), out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int GemLevel
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string text = this.method_4(JsonItem.getString_0(107441752));
				((byte*)ptr)[8] = (text.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(text.Split(new char[]
					{
						' '
					})[0], out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int BaseItemStackSize
		{
			get
			{
				void* ptr = stackalloc byte[7];
				((byte*)ptr)[4] = ((this.properties == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					*(int*)ptr = 1;
				}
				else
				{
					((byte*)ptr)[5] = ((this.properties[0].name != JsonItem.getString_0(107441743)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						*(int*)ptr = 1;
					}
					else
					{
						string[] array = this.properties[0].values[0][0].ToString().Split(new char[]
						{
							'/'
						});
						((byte*)ptr)[6] = ((array.Count<string>() != 2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							*(int*)ptr = 1;
						}
						else
						{
							*(int*)ptr = Util.smethod_7(array[1]);
						}
					}
				}
				return *(int*)ptr;
			}
		}

		public unsafe string UniqueIdentifiers
		{
			get
			{
				void* ptr = stackalloc byte[9];
				List<string> list = new List<string>();
				*(byte*)ptr = ((!Class146.smethod_0(this.Name)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					list.Add(string.Format(JsonItem.getString_0(107441694), this.MapTier));
				}
				list.Add(string.Format(JsonItem.getString_0(107441673), this.ItemRarity == ItemRarity.Unique));
				if (!this.IsMap && !this.IsHeist)
				{
					list.Add(string.Format(JsonItem.getString_0(107441688), this.corrupted));
					if (this.ItemRarity == ItemRarity.Rare && this.explicitMods != null)
					{
						list.Add(string.Format(JsonItem.getString_0(107441635), string.Join(JsonItem.getString_0(107393551), this.explicitMods)));
					}
				}
				if (this.typeLine.smethod_9(JsonItem.getString_0(107441646)) || this.typeLine.smethod_9(JsonItem.getString_0(107441605)))
				{
					list.Add(this.Name);
				}
				((byte*)ptr)[1] = ((this.SocketCount >= 5) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					list.Add(string.Format(JsonItem.getString_0(107441612), this.sockets));
					list.Add(string.Format(JsonItem.getString_0(107441563), this.Links));
				}
				((byte*)ptr)[2] = (this.IsGem ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					list.Add(string.Format(JsonItem.getString_0(107441034), this.GemLevel));
					((byte*)ptr)[3] = ((!this.IsSpecialGem) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						list.Add(string.Format(JsonItem.getString_0(107441045), this.Quality));
					}
				}
				((byte*)ptr)[4] = ((this.influences != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in this.influences.Results)
					{
						list.Add(string.Format(JsonItem.getString_0(107363477), keyValuePair.Key, keyValuePair.Value));
					}
				}
				((byte*)ptr)[5] = ((!this.IsBlightedMap) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[6] = ((this.implicitMods != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						list.Add(string.Format(JsonItem.getString_0(107440988), string.Join(JsonItem.getString_0(107393551), this.implicitMods)));
					}
					((byte*)ptr)[7] = ((this.prophecyText != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						list.Add(string.Format(JsonItem.getString_0(107440967), this.prophecyText));
					}
					List<string> enchantMods = this.enchantMods;
					if (enchantMods != null)
					{
						enchantMods.RemoveAll(new Predicate<string>(JsonItem.<>c.<>9.method_1));
					}
					if (this.enchantMods != null && this.enchantMods.Any<string>())
					{
						list.Add(string.Format(JsonItem.getString_0(107440974), string.Join(JsonItem.getString_0(107393551), this.enchantMods)));
					}
					((byte*)ptr)[8] = ((this.SeedLevel > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						list.Add(string.Format(JsonItem.getString_0(107440953), this.SeedLevel));
					}
				}
				return string.Join(JsonItem.getString_0(107393551), list);
			}
		}

		public bool IsSpecialGem
		{
			get
			{
				string[] source = new string[]
				{
					JsonItem.getString_0(107440900),
					JsonItem.getString_0(107440907),
					JsonItem.getString_0(107440886)
				};
				return source.Contains(this.typeLine);
			}
		}

		private void method_0(string string_9, string string_10, int int_8)
		{
			if (this.properties == null)
			{
				this.properties = new List<JsonItem.GClass9>();
			}
			JsonItem.GClass9 item = new JsonItem.GClass9
			{
				name = string_9,
				values = new List<List<object>>
				{
					new List<object>
					{
						string_10,
						int_8
					}
				}
			};
			this.properties.Add(item);
		}

		private void method_1(string string_9)
		{
			if (this.properties == null)
			{
				this.properties = new List<JsonItem.GClass9>();
			}
			JsonItem.GClass9 item = new JsonItem.GClass9
			{
				name = string_9,
				values = new List<List<object>>()
			};
			this.properties.Add(item);
		}

		public unsafe int SeedTier
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string s = this.method_4(JsonItem.getString_0(107440833));
				((byte*)ptr)[8] = (s.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(s, out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public unsafe int SeedLevel
		{
			get
			{
				void* ptr = stackalloc byte[9];
				string s = this.method_4(JsonItem.getString_0(107440852));
				((byte*)ptr)[8] = (s.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					int.TryParse(s, out *(int*)ptr);
					*(int*)((byte*)ptr + 4) = *(int*)ptr;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		public string BaseItemName
		{
			get
			{
				string result;
				if (this.IsUnique)
				{
					result = this.Name.Replace(JsonItem.getString_0(107368251), JsonItem.getString_0(107397927));
				}
				else
				{
					result = API.smethod_15(this);
				}
				return result;
			}
		}

		public JsonItem method_2()
		{
			return new JsonItem
			{
				awakened = this.awakened,
				craftedMods = this.craftedMods,
				corrupted = this.corrupted,
				delve = this.delve,
				descrText = this.descrText,
				enchantMods = this.enchantMods,
				explicitMods = this.explicitMods,
				extended = this.extended,
				flavourText = this.flavourText,
				frameType = this.frameType,
				h = this.h,
				hybrid = this.hybrid,
				icon = this.icon,
				id = this.id,
				identified = this.identified,
				ilvl = this.ilvl,
				implicitMods = this.implicitMods,
				incubatedItem = this.incubatedItem,
				influences = this.influences,
				inventoryId = this.inventoryId,
				league = this.league,
				maxStackSize = this.maxStackSize,
				name = this.name,
				note = this.note,
				properties = this.properties,
				prophecyText = this.prophecyText,
				requirements = this.requirements,
				socketedItems = this.socketedItems,
				sockets = this.sockets,
				stackSize = this.stackSize,
				typeLine = this.typeLine,
				verified = this.verified,
				w = this.w,
				x = this.x,
				y = this.y
			};
		}

		public unsafe string method_3()
		{
			void* ptr = stackalloc byte[33];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441275), this.ItemClass));
			stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441254), Enum.GetName(typeof(ItemRarity), this.frameType)));
			((byte*)ptr)[12] = ((!string.IsNullOrEmpty(this.name)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				stringBuilder.AppendLine(this.name);
			}
			stringBuilder.AppendLine(this.typeLine);
			((byte*)ptr)[13] = ((this.properties != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 13) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (JsonItem.GClass9 gclass in this.properties)
				{
					((byte*)ptr)[14] = (gclass.values.Any<List<object>>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						((byte*)ptr)[15] = ((gclass.displayMode == 3) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 15) != 0)
						{
							stringBuilder.AppendLine(string.Format(gclass.name, gclass.values.Select(new Func<List<object>, object>(JsonItem.<>c.<>9.method_2)).ToArray<object>()));
						}
						else
						{
							int.TryParse(gclass.values[0][1].ToString(), out *(int*)ptr);
							((byte*)ptr)[16] = ((*(int*)ptr == 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441224), gclass.name, gclass.values[0][0]));
							}
							else
							{
								stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107363477), gclass.name, gclass.values[0][0]));
							}
						}
					}
					else
					{
						stringBuilder.AppendLine(gclass.name);
					}
				}
			}
			((byte*)ptr)[17] = ((this.requirements != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.AppendLine(JsonItem.getString_0(107441227));
				foreach (JsonItem.GClass10 gclass2 in this.requirements)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107363477), gclass2.name, gclass2.values[0][0]));
				}
			}
			((byte*)ptr)[18] = ((this.sockets != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 18) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.Append(JsonItem.getString_0(107441206));
				*(int*)((byte*)ptr + 4) = 0;
				foreach (KeyValuePair<int, int> keyValuePair in this.SocketLinks)
				{
					*(int*)((byte*)ptr + 8) = 0;
					for (;;)
					{
						((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 8) < keyValuePair.Value) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 19) == 0)
						{
							break;
						}
						stringBuilder.Append(string.Format(JsonItem.getString_0(107441161), this.SocketColour[*(int*)((byte*)ptr + 4)], (*(int*)((byte*)ptr + 8) == keyValuePair.Value - 1) ? JsonItem.getString_0(107397964) : JsonItem.getString_0(107369846)));
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.AppendLine();
			}
			((byte*)ptr)[20] = ((this.ilvl > 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441152), this.ilvl));
			}
			((byte*)ptr)[21] = ((this.enchantMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (string arg in this.enchantMods)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441163), arg));
				}
			}
			((byte*)ptr)[22] = ((this.implicitMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 22) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (string arg2 in this.implicitMods)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441142), arg2));
				}
			}
			((byte*)ptr)[23] = ((!this.identified) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 23) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.AppendLine(JsonItem.getString_0(107454285));
			}
			((byte*)ptr)[24] = ((this.fracturedMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 24) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (string arg3 in this.fracturedMods)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441089), arg3));
				}
			}
			((byte*)ptr)[25] = ((this.explicitMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 25) != 0)
			{
				((byte*)ptr)[26] = ((this.fracturedMods == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 26) != 0)
				{
					stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				}
				foreach (string arg4 in this.explicitMods)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107453086), arg4));
				}
				((byte*)ptr)[27] = ((this.craftedMods != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 27) == 0)
				{
					goto IL_75E;
				}
				using (List<string>.Enumerator enumerator8 = this.craftedMods.GetEnumerator())
				{
					while (enumerator8.MoveNext())
					{
						string arg5 = enumerator8.Current;
						stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441100), arg5));
					}
					goto IL_75E;
				}
			}
			((byte*)ptr)[28] = ((this.craftedMods != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 28) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (string arg6 in this.craftedMods)
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107441100), arg6));
				}
			}
			IL_75E:
			((byte*)ptr)[29] = (this.corrupted ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 29) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.AppendLine(JsonItem.getString_0(107441079));
			}
			((byte*)ptr)[30] = ((this.influences != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 30) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (KeyValuePair<string, bool> keyValuePair2 in this.influences.Results.Where(new Func<KeyValuePair<string, bool>, bool>(JsonItem.<>c.<>9.method_3)))
				{
					stringBuilder.AppendLine(string.Format(JsonItem.getString_0(107440522), keyValuePair2.Key));
				}
			}
			((byte*)ptr)[31] = ((this.flavourText != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 31) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				foreach (string text in this.flavourText)
				{
					stringBuilder.AppendLine(text.Replace(JsonItem.getString_0(107462297), JsonItem.getString_0(107397927)));
				}
			}
			((byte*)ptr)[32] = ((!string.IsNullOrEmpty(this.descrText)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 32) != 0)
			{
				stringBuilder.AppendLine(JsonItem.getString_0(107441269));
				stringBuilder.AppendLine(this.descrText);
			}
			return stringBuilder.ToString();
		}

		public string Item64
		{
			get
			{
				string result;
				if (this.extended != null && !this.extended.text.smethod_25())
				{
					result = this.extended.text;
				}
				else
				{
					result = Convert.ToBase64String(Encoding.UTF8.GetBytes(this.method_3()));
				}
				return result;
			}
		}

		public unsafe bool ValidChaosRecipe
		{
			get
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = ((this.ilvl < 60) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((this.ItemRarity != ItemRarity.Rare) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[1] = 1;
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe bool ValidRegalRecipe
		{
			get
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = ((this.ilvl < 75) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((this.ItemRarity != ItemRarity.Rare) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[1] = 1;
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe bool ValidGCPRecipe
		{
			get
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = ((!this.IsGem) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((this.Quality == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[1] = 1;
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe bool ValidExaltedShardRecipe
		{
			get
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = ((this.influences == null) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else if (!this.influences.Shaper && !this.influences.Elder)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((this.ItemRarity != ItemRarity.Rare) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[1] = 1;
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe int StashId
		{
			get
			{
				void* ptr = stackalloc byte[9];
				((byte*)ptr)[8] = ((!int.TryParse(this.inventoryId.Replace(JsonItem.getString_0(107386988), JsonItem.getString_0(107397927)), out *(int*)ptr)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(int*)((byte*)ptr + 4) = 0;
				}
				else
				{
					*(int*)((byte*)ptr + 4) = *(int*)ptr - 1;
				}
				return *(int*)((byte*)ptr + 4);
			}
		}

		private unsafe string method_4(string string_9)
		{
			void* ptr = stackalloc byte[2];
			JsonItem.Class258 @class = new JsonItem.Class258();
			@class.string_0 = string_9;
			*(byte*)ptr = ((this.properties == null) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				JsonItem.GClass9 gclass = this.properties.FirstOrDefault(new Func<JsonItem.GClass9, bool>(@class.method_0));
				((byte*)ptr)[1] = ((gclass == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = null;
				}
				else
				{
					result = gclass.values[0][0].smethod_10();
				}
			}
			return result;
		}

		public bool IsArmor
		{
			get
			{
				string item = API.smethod_16(this.typeLine);
				return ItemData.BodyArmour.Contains(item) || ItemData.Boots.Contains(item) || ItemData.Gloves.Contains(item) || ItemData.Helmets.Contains(item) || ItemData.Shields.Contains(item);
			}
		}

		public bool IsJewelry
		{
			get
			{
				string item = API.smethod_15(this);
				return ItemData.Amulets.Contains(item) || ItemData.Rings.Contains(item) || ItemData.Belts.Contains(item);
			}
		}

		public bool IsWeapon
		{
			get
			{
				string item = API.smethod_16(this.typeLine);
				return ItemData.Weapons1H.Contains(item) || ItemData.Weapons2H.Contains(item);
			}
		}

		public string ItemClass
		{
			get
			{
				ApiItem apiItem = API.smethod_7(this.CleanedTypeLine);
				string result;
				if (apiItem.Type != JsonItem.getString_0(107372194))
				{
					result = apiItem.Class;
				}
				else
				{
					result = JsonItem.getString_0(107397927);
				}
				return result;
			}
		}

		public unsafe bool UsePoePrices
		{
			get
			{
				void* ptr = stackalloc byte[5];
				*(byte*)ptr = ((this.ItemRarity != ItemRarity.Rare) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = (this.IsMap ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[3] = (this.IsBeast ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[1] = 0;
						}
						else
						{
							((byte*)ptr)[4] = (this.IsHeist ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								((byte*)ptr)[1] = 0;
							}
							else
							{
								((byte*)ptr)[1] = 1;
							}
						}
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe bool UsesItemLevel
		{
			get
			{
				void* ptr = stackalloc byte[5];
				*(byte*)ptr = ((this.ilvl == 0) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = (this.IsMavenInvitation ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[3] = (this.IsBeast ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[1] = 0;
						}
						else
						{
							((byte*)ptr)[4] = (this.IsHeist ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								((byte*)ptr)[1] = 0;
							}
							else
							{
								((byte*)ptr)[1] = 1;
							}
						}
					}
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe bool IsAlternateGem
		{
			get
			{
				void* ptr = stackalloc byte[3];
				if (this.properties == null || !this.IsGem)
				{
					*(byte*)ptr = 0;
				}
				else
				{
					foreach (JsonItem.GClass9 gclass in this.properties)
					{
						((byte*)ptr)[1] = ((!gclass.values.Any<List<object>>()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) == 0)
						{
							((byte*)ptr)[2] = ((gclass.values[0][0].smethod_10() == JsonItem.getString_0(107440509)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								*(byte*)ptr = 1;
								goto IL_A8;
							}
						}
					}
					*(byte*)ptr = 0;
				}
				IL_A8:
				return *(sbyte*)ptr != 0;
			}
		}

		static JsonItem()
		{
			Strings.CreateGetStringDelegate(typeof(JsonItem));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem.GClass11 gclass11_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<JsonItem.GClass10> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem.GClass7 gclass7_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem.GClass8 gclass8_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_8;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<object> list_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object object_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<JsonItem.Sockets> list_8;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<JsonItem.GClass9> list_9;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Bitmap bitmap_0;

		[NonSerialized]
		internal static GetString getString_0;

		public sealed class GClass7
		{
			public string text { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;
		}

		public sealed class GClass8
		{
			public string baseTypeName { get; set; }

			public List<string> explicitMods { get; set; }

			public string secDescrText { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<string> list_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;
		}

		public sealed class GClass9
		{
			public string name { get; set; }

			public List<List<object>> values { get; set; }

			public int displayMode { get; set; }

			public int type { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<List<object>> list_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_1;
		}

		public sealed class GClass10
		{
			public string name { get; set; }

			public List<List<object>> values { get; set; }

			public int displayMode { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<List<object>> list_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;
		}

		public sealed class GClass11
		{
			public bool Crusader { get; set; }

			public bool Elder { get; set; }

			public bool Hunter { get; set; }

			public bool Redeemer { get; set; }

			public bool Shaper { get; set; }

			public bool Warlord { get; set; }

			public Dictionary<string, bool> Results
			{
				get
				{
					return new Dictionary<string, bool>
					{
						{
							JsonItem.GClass11.getString_0(107249024),
							this.Crusader
						},
						{
							JsonItem.GClass11.getString_0(107248979),
							this.Elder
						},
						{
							JsonItem.GClass11.getString_0(107249002),
							this.Hunter
						},
						{
							JsonItem.GClass11.getString_0(107248993),
							this.Redeemer
						},
						{
							JsonItem.GClass11.getString_0(107248948),
							this.Shaper
						},
						{
							JsonItem.GClass11.getString_0(107248971),
							this.Warlord
						}
					};
				}
			}

			static GClass11()
			{
				Strings.CreateGetStringDelegate(typeof(JsonItem.GClass11));
			}

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_2;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_3;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_4;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_5;

			[NonSerialized]
			internal static GetString getString_0;
		}

		public sealed class Sockets
		{
			public int group { get; set; }

			public string attr { get; set; }

			public string sColour { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;
		}

		[CompilerGenerated]
		private sealed class Class258
		{
			internal bool method_0(JsonItem.GClass9 gclass9_0)
			{
				return gclass9_0.name == this.string_0;
			}

			public string string_0;
		}
	}
}
