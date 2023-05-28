using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ns0;
using ns21;
using ns25;
using ns27;
using ns28;
using ns29;
using ns36;
using PoEv2;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns35
{
	internal static class Class181
	{
		private static MainForm _form { get; set; }

		public static void smethod_0(MainForm mainForm_1)
		{
			Class181._form = mainForm_1;
			Thread thread = new Thread(new ThreadStart(Class181.smethod_7));
			thread.Start();
			Timer timer = new Timer();
			timer.Interval = 10000.0;
			timer.Elapsed += Class181.smethod_1;
			timer.Enabled = true;
		}

		private static void smethod_1(object sender, ElapsedEventArgs e)
		{
			if (Class181.list_4.Any<Class141>())
			{
				Class123.smethod_5(Enum3.const_8, JsonConvert.SerializeObject(Class181.list_4.ToList<Class141>()), DataMethods.POST);
				Class181.list_4.Clear();
			}
		}

		public static void smethod_2(Enum11 enum11_0, string string_0, params object[] object_0)
		{
			Class181.smethod_6(enum11_0, string.Format(string_0, object_0), Class181._form.richTextBox_0);
		}

		public static void smethod_3(Enum11 enum11_0, string string_0)
		{
			Class181.smethod_6(enum11_0, string_0, Class181._form.richTextBox_0);
		}

		public static void smethod_4(Enum11 enum11_0, string string_0, params object[] object_0)
		{
			Class181.smethod_6(enum11_0, string.Format(string_0, object_0), Class181._form.richTextBox_2);
		}

		public static void smethod_5(Enum11 enum11_0, string string_0)
		{
			Class181.smethod_6(enum11_0, string_0, Class181._form.richTextBox_2);
		}

		private unsafe static void smethod_6(Enum11 enum11_0, string string_0, RichTextBox richTextBox_0)
		{
			void* ptr = stackalloc byte[8];
			Class181.Class182 @class = new Class181.Class182();
			@class.richTextBox_0 = richTextBox_0;
			List<string> obj = Class181.list_0;
			*(byte*)ptr = 0;
			try
			{
				Monitor.Enter(obj, ref *(bool*)ptr);
				((byte*)ptr)[1] = ((!Class181._form.bool_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.list_0.Add(string_0);
					((byte*)ptr)[2] = ((Class181.list_0.Count > 3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.list_0 = Class181.list_0.smethod_0(3).ToList<string>();
					}
				}
			}
			finally
			{
				if (*(sbyte*)ptr != 0)
				{
					Monitor.Exit(obj);
				}
			}
			((byte*)ptr)[3] = ((enum11_0 == Enum11.const_3) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				string_0 = Class181.getString_0(107451976) + string_0;
				@class.color_0 = Color.Blue;
			}
			else
			{
				((byte*)ptr)[4] = ((enum11_0 == Enum11.const_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					string_0 = Class181.getString_0(107451931) + string_0;
					@class.color_0 = Color.DarkRed;
				}
				else
				{
					((byte*)ptr)[5] = ((enum11_0 == Enum11.const_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						string_0 = Class181.getString_0(107451950) + string_0;
						@class.color_0 = Color.Green;
					}
					else
					{
						@class.color_0 = Color.Black;
					}
				}
			}
			@class.string_0 = string.Format(Class181.getString_0(107451905), DateTime.Now, string_0, Environment.NewLine);
			List<string> obj2 = Class181.list_1;
			((byte*)ptr)[6] = 0;
			try
			{
				Monitor.Enter(obj2, ref *(bool*)((byte*)ptr + 6));
				Class181.list_1.Add(@class.string_0);
			}
			finally
			{
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					Monitor.Exit(obj2);
				}
			}
			if (enum11_0 != Enum11.const_3 || Class181._form.bool_2)
			{
				((byte*)ptr)[7] = ((enum11_0 != Enum11.const_3) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					Class181.list_4.Add(new Class141(enum11_0, @class.string_0));
				}
				try
				{
					Class181._form.Invoke(new Action(@class.method_0));
				}
				catch
				{
				}
			}
		}

		private unsafe static void smethod_7()
		{
			void* ptr = stackalloc byte[9];
			for (;;)
			{
				((byte*)ptr)[8] = 1;
				try
				{
					*(byte*)ptr = (Class181.list_1.Any<string>() ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						using (StreamWriter streamWriter = new StreamWriter(Class181.getString_0(107451920) + DateTime.Now.ToString(Class181.getString_0(107392953)) + Class181.getString_0(107392968), true))
						{
							foreach (string text in Class181.list_1.ToList<string>())
							{
								streamWriter.Write(text);
								Class181.list_1.Remove(text);
							}
						}
					}
					if (Class181.list_2.Any<Order>() && Class255.class105_0.method_4(ConfigOptions.JsonTradeLog))
					{
						try
						{
							((byte*)ptr)[1] = ((!File.Exists(Class181.getString_0(107451911))) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 1) != 0)
							{
								File.WriteAllText(Class181.getString_0(107451911), Class181.getString_0(107451890));
							}
							string value = File.ReadAllText(Class181.getString_0(107451911));
							List<Class245> list = JsonConvert.DeserializeObject<List<Class245>>(value);
							foreach (Order order_ in Class181.list_2)
							{
								list.Add(new Class245(order_));
							}
							File.WriteAllText(Class181.getString_0(107451911), JsonConvert.SerializeObject(list, Formatting.Indented));
						}
						catch (Exception ex)
						{
							Class181.smethod_2(Enum11.const_2, Class181.getString_0(107451885), new object[]
							{
								ex
							});
						}
					}
					((byte*)ptr)[2] = (Class181.list_2.Any<Order>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
						((byte*)ptr)[3] = ((!File.Exists(Class181.getString_0(107392370))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							htmlDocument.LoadHtml(Class238.TradeLog);
						}
						else
						{
							try
							{
								htmlDocument.Load(Class181.getString_0(107392370));
							}
							catch
							{
								htmlDocument.LoadHtml(Class238.TradeLog);
							}
						}
						using (List<Order>.Enumerator enumerator3 = Class181.list_2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								Class181.Class183 @class = new Class181.Class183();
								@class.order_0 = enumerator3.Current;
								try
								{
									HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode(Class181.getString_0(107451808));
									((byte*)ptr)[4] = ((htmlNode == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 4) != 0)
									{
										htmlDocument.LoadHtml(Class238.TradeLog);
										htmlNode = htmlDocument.DocumentNode.SelectSingleNode(Class181.getString_0(107451808));
									}
									HtmlNode htmlNode2 = HtmlNode.CreateNode(Class181.getString_0(107451827));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.dateTime_1.ToString().Replace(Class181.getString_0(107369463), Class181.getString_0(107372681)))));
									((byte*)ptr)[5] = ((@class.order_0.OrderType == Order.Type.Buy) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 5) != 0)
									{
										htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451253), Array.Empty<object>())));
									}
									else
									{
										htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451236), Array.Empty<object>())));
									}
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.my_item_amount.ToString())));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.my_item_name.ToString())));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.player_item_amount.ToString())));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.player_item_name.ToString())));
									List<string> list2 = new List<string>();
									foreach (KeyValuePair<JsonTab, int> keyValuePair in @class.order_0.my_inventory_items)
									{
										list2.Add(string.Format(Class181.getString_0(107451247), keyValuePair.Key.n, keyValuePair.Value));
									}
									string arg = string.Join(Class181.getString_0(107451202), list2);
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), arg)));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.player.name.ToString())));
									htmlNode2.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), @class.order_0.TradeCompleted.ToString())));
									htmlNode.AppendChild(htmlNode2);
									htmlDocument.Save(Class181.getString_0(107392370));
									Class181.list_2.RemoveAll(new Predicate<Order>(@class.method_0));
								}
								catch (NullReferenceException)
								{
									File.Delete(Class181.getString_0(107392370));
								}
								catch (Exception ex2)
								{
									Class181.smethod_2(Enum11.const_2, Class181.getString_0(107451197), new object[]
									{
										ex2
									});
								}
							}
						}
					}
					((byte*)ptr)[6] = (Class181.list_3.Any<Class264>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						HtmlAgilityPack.HtmlDocument htmlDocument2 = new HtmlAgilityPack.HtmlDocument();
						((byte*)ptr)[7] = ((!File.Exists(Class181.getString_0(107392260))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							htmlDocument2.LoadHtml(Class238.WhisperLog);
						}
						else
						{
							try
							{
								htmlDocument2.Load(Class181.getString_0(107392260));
							}
							catch
							{
								htmlDocument2.LoadHtml(Class238.WhisperLog);
							}
						}
						using (List<Class264>.Enumerator enumerator5 = Class181.list_3.ToList<Class264>().GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								Class181.Class184 class2 = new Class181.Class184();
								class2.class264_0 = enumerator5.Current;
								try
								{
									HtmlNode htmlNode3 = htmlDocument2.DocumentNode.SelectSingleNode(Class181.getString_0(107451808));
									HtmlNode htmlNode4 = HtmlNode.CreateNode(Class181.getString_0(107451827));
									htmlNode4.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), DateTime.Now.ToString().Replace(Class181.getString_0(107369463), Class181.getString_0(107372681)))));
									htmlNode4.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), class2.class264_0.Name)));
									htmlNode4.AppendChild(HtmlNode.CreateNode(string.Format(Class181.getString_0(107451814), class2.class264_0.Message)));
									htmlNode3.AppendChild(htmlNode4);
									htmlDocument2.Save(Class181.getString_0(107392260));
									Class181.list_3.RemoveAll(new Predicate<Class264>(class2.method_0));
								}
								catch (NullReferenceException)
								{
									File.Delete(Class181.getString_0(107392260));
								}
								catch (Exception ex3)
								{
									Class181.smethod_2(Enum11.const_2, Class181.getString_0(107451160), new object[]
									{
										ex3
									});
								}
							}
						}
					}
				}
				catch
				{
				}
				Thread.Sleep(500);
			}
		}

		public static void smethod_8(Order order_0)
		{
			Class181.list_2.Add(order_0);
		}

		public static void smethod_9(Class264 class264_0)
		{
			Class181.list_3.Add(class264_0);
		}

		public static void smethod_10(RichTextBox richTextBox_0, string string_0)
		{
			Class181.Class185 @class = new Class181.Class185();
			@class.richTextBox_0 = richTextBox_0;
			Regex regex = new Regex(Class181.getString_0(107451151));
			Regex regex2 = new Regex(Class181.getString_0(107451118));
			@class.match_0 = regex.Match(string_0);
			Match match = regex2.Match(string_0);
			@class.string_0 = string.Format(Class181.getString_0(107451061), match.Groups[1].Value, match.Groups[2].Value);
			@class.richTextBox_0.Invoke(new Action(@class.method_0));
		}

		private unsafe static Color smethod_11(string string_0)
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((string_0 == Class181.getString_0(107398602)) ? 1 : 0);
			Color result;
			if (*(sbyte*)ptr != 0)
			{
				result = Color.FromArgb(224, 0, 0);
			}
			else
			{
				((byte*)ptr)[1] = ((string_0 == Class181.getString_0(107451080)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = Color.FromArgb(188, 93, 0);
				}
				else
				{
					((byte*)ptr)[2] = ((string_0 == Class181.getString_0(107379275)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = Color.FromArgb(114, 70, 158);
					}
					else
					{
						((byte*)ptr)[3] = ((string_0 == Class181.getString_0(107397408)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = Color.FromArgb(232, 232, 232);
						}
						else
						{
							((byte*)ptr)[4] = ((string_0 == Class181.getString_0(107452654)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = Color.FromArgb(126, 126, 126);
							}
							else
							{
								result = Color.FromArgb(84, 178, 36);
							}
						}
					}
				}
			}
			return result;
		}

		private unsafe static Enum11 smethod_12(string string_0)
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((string_0 == Class181.getString_0(107398602)) ? 1 : 0);
			Enum11 result;
			if (*(sbyte*)ptr != 0)
			{
				result = Enum11.const_4;
			}
			else
			{
				((byte*)ptr)[1] = ((string_0 == Class181.getString_0(107451080)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = Enum11.const_5;
				}
				else
				{
					((byte*)ptr)[2] = ((string_0 == Class181.getString_0(107379275)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = Enum11.const_6;
					}
					else
					{
						((byte*)ptr)[3] = ((string_0 == Class181.getString_0(107397408)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = Enum11.const_9;
						}
						else
						{
							((byte*)ptr)[4] = ((string_0 == Class181.getString_0(107452654)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = Enum11.const_7;
							}
							else
							{
								result = Enum11.const_8;
							}
						}
					}
				}
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class181()
		{
			Strings.CreateGetStringDelegate(typeof(Class181));
			Class181.list_0 = new List<string>();
			Class181.list_1 = new List<string>();
			Class181.list_2 = new List<Order>();
			Class181.list_3 = new List<Class264>();
			Class181.list_4 = new List<Class141>();
		}

		private static List<string> list_0;

		private static List<string> list_1;

		private static List<Order> list_2;

		private static List<Class264> list_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static MainForm mainForm_0;

		private static List<Class141> list_4;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class182
		{
			internal void method_0()
			{
				this.richTextBox_0.SelectionColor = this.color_0;
				this.richTextBox_0.AppendText(this.string_0);
			}

			public RichTextBox richTextBox_0;

			public Color color_0;

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class183
		{
			internal bool method_0(Order order_1)
			{
				return order_1.player.name == this.order_0.player.name;
			}

			public Order order_0;
		}

		[CompilerGenerated]
		private sealed class Class184
		{
			internal bool method_0(Class264 class264_1)
			{
				return class264_1.Name == this.class264_0.Name && class264_1.Message == this.class264_0.Message;
			}

			public Class264 class264_0;
		}

		[CompilerGenerated]
		private sealed class Class185
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[6];
				string value = this.match_0.Groups[1].Value;
				if (!string.IsNullOrEmpty(value) && this.match_0.Groups[2].Length != 0)
				{
					char c = this.match_0.Groups[2].Value[0];
					string text = value;
					string text2 = text;
					if (text2 != null)
					{
						*(int*)ptr = (int)Class396.smethod_0(text2);
						if (*(uint*)ptr <= 2566012723U)
						{
							if (*(uint*)ptr <= 1211963829U)
							{
								if (*(uint*)ptr <= 682763176U)
								{
									if (*(uint*)ptr != 550270072U)
									{
										if (*(uint*)ptr == 682763176U)
										{
											if (text2 == Class181.Class185.getString_0(107398234))
											{
												return;
											}
										}
									}
									else if (text2 == Class181.Class185.getString_0(107249740))
									{
										return;
									}
								}
								else if (*(uint*)ptr != 1203031727U)
								{
									if (*(uint*)ptr == 1211963829U)
									{
										if (text2 == Class181.Class185.getString_0(107249611))
										{
											return;
										}
									}
								}
								else if (text2 == Class181.Class185.getString_0(107249750))
								{
									return;
								}
							}
							else if (*(uint*)ptr <= 1879400691U)
							{
								if (*(uint*)ptr != 1633766373U)
								{
									if (*(uint*)ptr == 1879400691U)
									{
										if (text2 == Class181.Class185.getString_0(107249731))
										{
											return;
										}
									}
								}
								else if (text2 == Class181.Class185.getString_0(107249691))
								{
									return;
								}
							}
							else if (*(uint*)ptr != 2066738941U)
							{
								if (*(uint*)ptr == 2566012723U)
								{
									if (text2 == Class181.Class185.getString_0(107249761))
									{
										return;
									}
								}
							}
							else if (text2 == Class181.Class185.getString_0(107249723))
							{
								return;
							}
						}
						else if (*(uint*)ptr <= 3247949794U)
						{
							if (*(uint*)ptr <= 2988966134U)
							{
								if (*(uint*)ptr != 2932321867U)
								{
									if (*(uint*)ptr == 2988966134U)
									{
										if (text2 == Class181.Class185.getString_0(107249714))
										{
											return;
										}
									}
								}
								else if (text2 == Class181.Class185.getString_0(107249700))
								{
									return;
								}
							}
							else if (*(uint*)ptr != 3237151635U)
							{
								if (*(uint*)ptr == 3247949794U)
								{
									if (text2 == Class181.Class185.getString_0(107249682))
									{
										return;
									}
								}
							}
							else if (text2 == Class181.Class185.getString_0(107249668))
							{
								return;
							}
						}
						else if (*(uint*)ptr <= 3478590034U)
						{
							if (*(uint*)ptr != 3320309686U)
							{
								if (*(uint*)ptr == 3478590034U)
								{
									if (text2 == Class181.Class185.getString_0(107249656))
									{
										return;
									}
								}
							}
							else if (text2 == Class181.Class185.getString_0(107249641))
							{
								return;
							}
						}
						else if (*(uint*)ptr != 3512062061U)
						{
							if (*(uint*)ptr != 3526092385U)
							{
								if (*(uint*)ptr == 3633728212U)
								{
									if (text2 == Class181.Class185.getString_0(107249709))
									{
										return;
									}
								}
							}
							else if (text2 == Class181.Class185.getString_0(107249673))
							{
								return;
							}
						}
						else if (text2 == Class181.Class185.getString_0(107249770))
						{
							return;
						}
					}
					if (value == Class181.Class185.getString_0(107452663) || value == Class181.Class185.getString_0(107398611) || value == Class181.Class185.getString_0(107379284) || value == Class181.Class185.getString_0(107451089) || value == Class181.Class185.getString_0(107397417) || c == ':')
					{
						((byte*)ptr)[4] = ((value == Class181.Class185.getString_0(107398611)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) == 0)
						{
							Color color_ = Class181.smethod_11(this.match_0.Groups[1].Value);
							string[] array = this.match_0.Groups[2].Value.Split(new char[]
							{
								':'
							});
							((byte*)ptr)[5] = ((this.match_0.Groups[1].Value == Class181.Class185.getString_0(107397417)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								string arg = string.Join(Class181.Class185.getString_0(107397418), array);
								string string_ = string.Format(Class181.Class185.getString_0(107440652) + Environment.NewLine, this.string_0, arg);
								this.richTextBox_0.smethod_14(string_, color_);
								Class141 @class = new Class141(Enum11.const_9, string_);
								if (!Class181.list_4.Any<Class141>() || !Class181.list_4.Last<Class141>().Equals(@class))
								{
									Class181.list_4.Add(@class);
								}
							}
							else
							{
								string text3 = array[0];
								string text4 = text3;
								if (text4 == null || (!(text4 == Class181.Class185.getString_0(107249770)) && !(text4 == Class181.Class185.getString_0(107249761))))
								{
									string string_2 = string.Format(Class181.Class185.getString_0(107249626), this.string_0, this.match_0.Groups[1].Value, array[0]);
									string string_3 = string.Format(Class181.Class185.getString_0(107440652), string.Join(Class181.Class185.getString_0(107397417), array.Skip(1)), Environment.NewLine);
									this.richTextBox_0.smethod_14(string_2, color_);
									this.richTextBox_0.smethod_14(string_3, Color.White);
									this.richTextBox_0.ScrollToCaret();
									Class141 class2 = new Class141(Class181.smethod_12(value), string_3, string_2);
									if (!Class181.list_4.Any<Class141>() || !Class181.list_4.Last<Class141>().Equals(class2))
									{
										Class181.list_4.Add(class2);
									}
								}
							}
						}
					}
				}
			}

			static Class185()
			{
				Strings.CreateGetStringDelegate(typeof(Class181.Class185));
			}

			public Match match_0;

			public string string_0;

			public RichTextBox richTextBox_0;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
