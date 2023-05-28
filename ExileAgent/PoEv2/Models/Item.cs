using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using PoEv2.Classes;
using PoEv2.Models.Items;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class Item
	{
		public string name { get; set; }

		public string typeLine { get; set; }

		public string rarity { get; set; }

		public bool awakened { get; set; }

		public int quality { get; set; } = 0;

		public int gem_level { get; set; } = 0;

		public int item_level { get; set; }

		public int stack { get; set; } = 1;

		public int stack_size { get; set; } = 1;

		public int tier { get; set; } = 0;

		public bool corrupted { get; set; }

		public bool identified { get; set; }

		public string stash { get; set; }

		public string note { get; set; } = Item.getString_0(107398024);

		public int Left { get; set; } = 0;

		public int Top { get; set; } = 0;

		public int height { get; set; } = 1;

		public int width { get; set; } = 1;

		public int socketRed { get; set; } = 0;

		public int socketBlue { get; set; } = 0;

		public int socketGreen { get; set; } = 0;

		public int socketWhite { get; set; } = 0;

		public int socketAbyss { get; set; } = 0;

		public int LinkCount { get; set; } = 0;

		public string text { get; set; }

		public bool placeholder { get; set; } = false;

		public int ItemQuantity { get; set; }

		public int PackSize { get; set; }

		public int smallestX { get; set; } = 13;

		public int largestX { get; set; } = -1;

		public int smallestY { get; set; } = 6;

		public int largestY { get; set; } = -1;

		public string baseStats { get; set; } = null;

		public string requirements { get; set; }

		public List<string> implicits { get; set; }

		public List<string> enchants { get; set; }

		public List<string> fractured { get; set; }

		public string ItemClass { get; set; }

		public Item()
		{
		}

		public Item(JsonItem item)
		{
			this.name = item.name;
			this.typeLine = item.typeLine;
			this.rarity = item.ItemRarity.ToString();
			this.width = item.w;
			this.height = item.h;
			this.Left = item.x;
			this.Top = item.y;
			this.stack = item.stackSize;
			this.stack_size = item.maxStackSize;
			this.text = item.method_3();
		}

		public Item(string clipboard)
		{
			this.text = clipboard;
			this.method_0();
		}

		public unsafe void method_0()
		{
			void* ptr = stackalloc byte[19];
			if (this.text != null && !(this.text == Item.getString_0(107382650)))
			{
				MatchCollection matchCollection = Item.regex_0.Matches(this.text);
				using (IEnumerator enumerator = matchCollection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Match match = (Match)enumerator.Current;
						this.typeLine = match.Groups[1].Value.Replace(Item.getString_0(107368348), Item.getString_0(107398024));
						string text = Environment.NewLine;
						((byte*)ptr)[8] = (this.typeLine.Contains(Environment.NewLine) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							text = Environment.NewLine;
						}
						else
						{
							((byte*)ptr)[9] = (this.typeLine.Contains(Item.getString_0(107396818)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								text = Item.getString_0(107396818);
							}
						}
						List<string> list = new List<string>(this.typeLine.Split(new string[]
						{
							text
						}, StringSplitOptions.RemoveEmptyEntries));
						((byte*)ptr)[10] = ((list.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							this.name = list[0];
							this.typeLine = list[1];
						}
						else
						{
							this.name = string.Empty;
							this.typeLine = this.typeLine.Replace(text, Item.getString_0(107398024));
						}
					}
				}
				matchCollection = Item.regex_1.Matches(this.text);
				foreach (object obj in matchCollection)
				{
					Match match2 = (Match)obj;
					this.rarity = match2.Groups[1].Value;
				}
				matchCollection = Item.regex_2.Matches(this.text);
				foreach (object obj2 in matchCollection)
				{
					Match match3 = (Match)obj2;
					((byte*)ptr)[11] = (match3.Groups[1].Value.Contains(Item.getString_0(107440472)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						this.awakened = true;
					}
					else
					{
						this.awakened = false;
					}
				}
				matchCollection = Item.regex_3.Matches(this.text);
				foreach (object obj3 in matchCollection)
				{
					Match match4 = (Match)obj3;
					this.quality = Convert.ToInt32(match4.Groups[1].Value);
				}
				matchCollection = Item.regex_17.Matches(this.text);
				foreach (object obj4 in matchCollection)
				{
					Match match5 = (Match)obj4;
					this.ItemQuantity = Convert.ToInt32(match5.Groups[1].Value);
				}
				matchCollection = Item.regex_18.Matches(this.text);
				foreach (object obj5 in matchCollection)
				{
					Match match6 = (Match)obj5;
					this.PackSize = Convert.ToInt32(match6.Groups[1].Value);
				}
				matchCollection = Item.regex_4.Matches(this.text);
				foreach (object obj6 in matchCollection)
				{
					Match match7 = (Match)obj6;
					this.gem_level = Convert.ToInt32(match7.Groups[1].Value);
				}
				matchCollection = Item.regex_5.Matches(this.text);
				foreach (object obj7 in matchCollection)
				{
					Match match8 = (Match)obj7;
					this.item_level = Convert.ToInt32(match8.Groups[1].Value);
				}
				matchCollection = Item.regex_6.Matches(this.text);
				foreach (object obj8 in matchCollection)
				{
					Match match9 = (Match)obj8;
					string text2 = string.Empty;
					string value = match9.Groups[1].Value;
					*(int*)ptr = 0;
					while (*(int*)ptr < value.Length)
					{
						char c = value[*(int*)ptr];
						((byte*)ptr)[12] = (char.IsDigit(c) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							text2 += c.ToString();
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					this.stack = Convert.ToInt32(text2);
				}
				matchCollection = Item.regex_7.Matches(this.text);
				foreach (object obj9 in matchCollection)
				{
					Match match10 = (Match)obj9;
					string value2 = Util.smethod_9(match10.Groups[1].Value);
					this.stack_size = Convert.ToInt32(value2);
				}
				matchCollection = Item.regex_8.Matches(this.text);
				foreach (object obj10 in matchCollection)
				{
					Match match11 = (Match)obj10;
					this.tier = Convert.ToInt32(match11.Groups[1].Value);
				}
				matchCollection = Item.regex_9.Matches(this.text);
				foreach (object obj11 in matchCollection)
				{
					Match match12 = (Match)obj11;
					((byte*)ptr)[13] = ((match12.Groups[1].Value == Item.getString_0(107441176)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						this.corrupted = true;
					}
					else
					{
						this.corrupted = false;
					}
				}
				this.identified = !Item.regex_10.IsMatch(this.text);
				matchCollection = Item.regex_11.Matches(this.text);
				foreach (object obj12 in matchCollection)
				{
					Match match13 = (Match)obj12;
					this.note = match13.Groups[1].Value.Replace(Item.getString_0(107462394), Item.getString_0(107398024)).Replace('.', this.char_0);
				}
				matchCollection = Item.regex_12.Matches(this.text);
				foreach (object obj13 in matchCollection)
				{
					Match match14 = (Match)obj13;
					string text3 = match14.Groups[1].Value.Replace(Item.getString_0(107440427), Item.getString_0(107398024));
					this.socketRed = text3.Split(new char[]
					{
						'R'
					}).Length - 1;
					this.socketBlue = text3.Split(new char[]
					{
						'B'
					}).Length - 1;
					this.socketGreen = text3.Split(new char[]
					{
						'G'
					}).Length - 1;
					this.socketWhite = text3.Split(new char[]
					{
						'W'
					}).Length - 1;
					this.socketAbyss = text3.Split(new char[]
					{
						'A'
					}).Length - 1;
					((byte*)ptr)[14] = ((!text3.Contains(Item.getString_0(107369943))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						this.LinkCount = 0;
					}
					else
					{
						string[] array = text3.Split(new char[]
						{
							' '
						});
						string[] array2 = array;
						*(int*)((byte*)ptr + 4) = 0;
						while (*(int*)((byte*)ptr + 4) < array2.Length)
						{
							string text4 = array2[*(int*)((byte*)ptr + 4)];
							this.LinkCount = Math.Max(this.LinkCount, text4.Split(new char[]
							{
								'-'
							}).Count<string>());
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
					}
				}
				matchCollection = Item.regex_13.Matches(this.text);
				foreach (object obj14 in matchCollection)
				{
					Match match15 = (Match)obj14;
					this.requirements = match15.Groups[1].Value;
				}
				matchCollection = Item.regex_14.Matches(this.text);
				((byte*)ptr)[15] = ((matchCollection.Count > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					this.implicits = new List<string>();
				}
				foreach (object obj15 in matchCollection)
				{
					Match match16 = (Match)obj15;
					this.implicits.Add(match16.Groups[1].Value);
				}
				matchCollection = Item.regex_15.Matches(this.text);
				((byte*)ptr)[16] = ((matchCollection.Count > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					this.enchants = new List<string>();
				}
				foreach (object obj16 in matchCollection)
				{
					Match match17 = (Match)obj16;
					this.enchants.Add(match17.Groups[1].Value);
				}
				matchCollection = Item.regex_16.Matches(this.text);
				((byte*)ptr)[17] = ((matchCollection.Count > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					this.fractured = new List<string>();
				}
				foreach (object obj17 in matchCollection)
				{
					Match match18 = (Match)obj17;
					this.fractured.Add(match18.Groups[1].Value);
				}
				matchCollection = Item.regex_19.Matches(this.text);
				((byte*)ptr)[18] = ((matchCollection.Count > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					this.ItemClass = matchCollection[0].Groups[1].Value.Replace(Item.getString_0(107462394), string.Empty);
				}
			}
		}

		public bool HasStack
		{
			get
			{
				return Item.regex_6.Match(this.text).Success;
			}
		}

		public string ToString()
		{
			return string.Format(Item.getString_0(107440422), new object[]
			{
				this.Name,
				this.Left,
				this.Top,
				this.width,
				this.height
			});
		}

		public bool IsGem
		{
			get
			{
				return this.rarity == Item.getString_0(107440401);
			}
		}

		public bool isNormal
		{
			get
			{
				return this.rarity == Item.getString_0(107398084);
			}
		}

		public bool isMagic
		{
			get
			{
				return this.rarity == Item.getString_0(107440396);
			}
		}

		public bool isRare
		{
			get
			{
				return this.rarity == Item.getString_0(107403426);
			}
		}

		public bool isUnique
		{
			get
			{
				return this.rarity == Item.getString_0(107440867);
			}
		}

		public bool IsMap
		{
			get
			{
				return this.tier > 0;
			}
		}

		public bool IsCard
		{
			get
			{
				return this.rarity == Item.getString_0(107440890);
			}
		}

		public bool IsProphecy
		{
			get
			{
				return this.text.smethod_9(Item.getString_0(107441524));
			}
		}

		public bool IsIncubator
		{
			get
			{
				return this.Name.smethod_9(Item.getString_0(107440841));
			}
		}

		public bool IsMavenInvitation
		{
			get
			{
				return this.typeLine.Contains(Item.getString_0(107452030));
			}
		}

		public bool IsFractured
		{
			get
			{
				return this.fractured != null && this.fractured.Any<string>();
			}
		}

		public unsafe bool method_1(Item item_0, int int_22, int int_23)
		{
			void* ptr = stackalloc byte[10];
			string a = Util.smethod_5(item_0.text);
			string b = Util.smethod_5(this.text);
			*(byte*)ptr = ((a != b) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				Size size = ItemData.smethod_2(this.typeLine);
				if (size.Width == 1 && size.Height == 1)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((item_0.rarity == Item.getString_0(107395083)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[3] = ((!item_0.typeLine.Contains(Item.getString_0(107440828))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[1] = 0;
							goto IL_256;
						}
						((byte*)ptr)[4] = (item_0.typeLine.Contains(Item.getString_0(107440811)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							((byte*)ptr)[1] = 0;
							goto IL_256;
						}
						((byte*)ptr)[5] = (item_0.typeLine.Contains(Item.getString_0(107440826)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							((byte*)ptr)[6] = ((this.Left != int_22) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								((byte*)ptr)[1] = 0;
								goto IL_256;
							}
							((byte*)ptr)[7] = ((this.Top + 1 != int_23) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								((byte*)ptr)[1] = 0;
								goto IL_256;
							}
							((byte*)ptr)[1] = 1;
							goto IL_256;
						}
						else if (item_0.typeLine.Contains(Item.getString_0(107440813)) || item_0.typeLine.Contains(Item.getString_0(107440768)))
						{
							if (this.Left != int_22 && this.Left + 1 != int_22)
							{
								((byte*)ptr)[1] = 0;
								goto IL_256;
							}
							if (this.Top != int_23 && this.Top + 1 != int_23)
							{
								((byte*)ptr)[1] = 0;
								goto IL_256;
							}
							((byte*)ptr)[1] = 1;
							goto IL_256;
						}
					}
					((byte*)ptr)[8] = (item_0.isNormal ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						foreach (string text in this.list_3)
						{
							((byte*)ptr)[9] = (item_0.typeLine.ToLower().Contains(text.ToLower()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								((byte*)ptr)[1] = 0;
								goto IL_256;
							}
						}
					}
					((byte*)ptr)[1] = 1;
				}
			}
			IL_256:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public string Name
		{
			get
			{
				return string.Format(Item.getString_0(107396438), this.name, this.typeLine).Trim();
			}
		}

		public JsonItem method_2()
		{
			return new JsonItem(this, this.Left, this.Top);
		}

		public bool IsSeed
		{
			get
			{
				return this.text.Contains(Item.getString_0(107440791));
			}
		}

		public unsafe string ExplicitMods
		{
			get
			{
				void* ptr = stackalloc byte[13];
				string result;
				if (!this.text.Contains(Item.getString_0(107440742)) || this.isNormal)
				{
					result = string.Empty;
				}
				else
				{
					ref int ptr2 = ref *(int*)ptr;
					string text = this.text;
					string str = Item.getString_0(107440742);
					*(int*)((byte*)ptr + 8) = this.item_level;
					ptr2 = text.IndexOf(str + ((int*)((byte*)ptr + 8))->ToString());
					*(int*)ptr = this.text.IndexOf(Item.getString_0(107441366), *(int*)ptr);
					List<string> enchants = this.enchants;
					if (enchants != null && enchants.Any<string>())
					{
						*(int*)ptr = this.text.IndexOf(Item.getString_0(107441366), *(int*)ptr + 1);
					}
					List<string> implicits = this.implicits;
					if (implicits != null && implicits.Any<string>())
					{
						*(int*)ptr = this.text.IndexOf(Item.getString_0(107441366), *(int*)ptr + 1);
					}
					*(int*)((byte*)ptr + 4) = this.text.IndexOf(Item.getString_0(107441366), *(int*)ptr + 1);
					((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 4) == -1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						*(int*)((byte*)ptr + 4) = this.text.Length;
					}
					result = this.text.Substring(*(int*)ptr + Item.getString_0(107441366).Length, *(int*)((byte*)ptr + 4) - *(int*)ptr - Item.getString_0(107441366).Length).Trim();
				}
				return result;
			}
		}

		public bool HasPrefix
		{
			get
			{
				return this.typeLine != null && !this.typeLine.Replace(Item.getString_0(107368348), Item.getString_0(107398024)).Replace(Item.getString_0(107452621), Item.getString_0(107398024)).StartsWith(API.smethod_15(this.method_2()));
			}
		}

		public bool HasSuffix
		{
			get
			{
				return this.typeLine != null && this.typeLine.Contains(Item.getString_0(107452005));
			}
		}

		public int SocketCount
		{
			get
			{
				return this.socketRed + this.socketGreen + this.socketBlue + this.socketAbyss + this.socketWhite;
			}
		}

		public unsafe List<string> Properties
		{
			get
			{
				void* ptr = stackalloc byte[13];
				List<string> list = new List<string>();
				*(int*)ptr = this.text.IndexOf(Item.getString_0(107441366) + Environment.NewLine) + Item.getString_0(107441366).Length + Environment.NewLine.Length;
				*(int*)((byte*)ptr + 4) = this.text.IndexOf(Environment.NewLine, *(int*)ptr + 1);
				*(int*)((byte*)ptr + 8) = this.text.IndexOf(Item.getString_0(107441366), *(int*)((byte*)ptr + 4));
				string text = this.text.Substring(*(int*)ptr, *(int*)((byte*)ptr + 4) - *(int*)ptr);
				List<string> result;
				if (text.Contains(Item.getString_0(107440757)) || text.Contains(Item.getString_0(107402259)) || text.Contains(Item.getString_0(107440708)))
				{
					result = list;
				}
				else
				{
					((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 8) - *(int*)ptr < 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						*(int*)((byte*)ptr + 8) = this.text.Length;
					}
					list.AddRange(this.text.Substring(*(int*)ptr, *(int*)((byte*)ptr + 8) - *(int*)ptr).smethod_19(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
					result = list;
				}
				return result;
			}
		}

		public bool IsFlask
		{
			get
			{
				return this.typeLine.Contains(Item.getString_0(107441600));
			}
		}

		public unsafe bool IsInfluenced
		{
			get
			{
				void* ptr = stackalloc byte[2];
				*(byte*)ptr = (this.text.smethod_25() ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else if (this.text.Contains(Item.getString_0(107440723)) || this.text.Contains(Item.getString_0(107440670)) || this.text.Contains(Item.getString_0(107440685)) || this.text.Contains(Item.getString_0(107440636)) || this.text.Contains(Item.getString_0(107440103)) || this.text.Contains(Item.getString_0(107440118)) || this.text.Contains(Item.getString_0(107440606)))
				{
					((byte*)ptr)[1] = 1;
				}
				else
				{
					((byte*)ptr)[1] = 0;
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Item()
		{
			Strings.CreateGetStringDelegate(typeof(Item));
			Item.regex_0 = new Regex(Item.getString_0(107440069));
			Item.regex_1 = new Regex(Item.getString_0(107440008));
			Item.regex_2 = new Regex(Item.getString_0(107440019));
			Item.regex_3 = new Regex(Item.getString_0(107439994));
			Item.regex_4 = new Regex(Item.getString_0(107439937));
			Item.regex_5 = new Regex(Item.getString_0(107439924));
			Item.regex_6 = new Regex(Item.getString_0(107439899));
			Item.regex_7 = new Regex(Item.getString_0(107440350));
			Item.regex_8 = new Regex(Item.getString_0(107440345));
			Item.regex_9 = new Regex(Item.getString_0(107440288));
			Item.regex_10 = new Regex(Item.getString_0(107440263));
			Item.regex_11 = new Regex(Item.getString_0(107440274));
			Item.regex_12 = new Regex(Item.getString_0(107440225));
			Item.regex_13 = new Regex(Item.getString_0(107440236));
			Item.regex_14 = new Regex(Item.getString_0(107440187));
			Item.regex_15 = new Regex(Item.getString_0(107440130));
			Item.regex_16 = new Regex(Item.getString_0(107439593));
			Item.regex_17 = new Regex(Item.getString_0(107439596));
			Item.regex_18 = new Regex(Item.getString_0(107439531));
			Item.regex_19 = new Regex(Item.getString_0(107439494));
		}

		private char char_0 = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

		private const string string_0 = "--------";

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

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
		private int int_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_8;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_9;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_10;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_11;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_12;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_13;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_14;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_15;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_16;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_17;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_18;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_19;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_20;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_21;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_8;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_9;

		private static Regex regex_0;

		private static Regex regex_1;

		private static Regex regex_2;

		private static Regex regex_3;

		private static Regex regex_4;

		private static Regex regex_5;

		private static Regex regex_6;

		private static Regex regex_7;

		private static Regex regex_8;

		private static Regex regex_9;

		private static Regex regex_10;

		private static Regex regex_11;

		private static Regex regex_12;

		private static Regex regex_13;

		private static Regex regex_14;

		private static Regex regex_15;

		private static Regex regex_16;

		private static Regex regex_17;

		private static Regex regex_18;

		private static Regex regex_19;

		private List<string> list_3 = new List<string>
		{
			Item.getString_0(107440596),
			Item.getString_0(107440547),
			Item.getString_0(107440558),
			Item.getString_0(107440517),
			Item.getString_0(107440536),
			Item.getString_0(107440491),
			Item.getString_0(107440502),
			Item.getString_0(107358124),
			Item.getString_0(107359267),
			Item.getString_0(107359305),
			Item.getString_0(107440457),
			Item.getString_0(107379472)
		};

		[NonSerialized]
		internal static GetString getString_0;
	}
}
