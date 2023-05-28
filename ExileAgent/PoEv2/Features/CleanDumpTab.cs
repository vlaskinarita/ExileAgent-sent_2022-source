using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ns14;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class CleanDumpTab
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			CleanDumpTab.mainForm_0 = mainForm_1;
		}

		[DebuggerStepThrough]
		public static Task<Tuple<bool, string>> smethod_1(int int_0)
		{
			CleanDumpTab.Class380 @class = new CleanDumpTab.Class380();
			@class.int_1 = int_0;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder<Tuple<bool, string>>.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder<Tuple<bool, string>> asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<CleanDumpTab.Class380>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		private static void smethod_2(Dictionary<JsonTab, List<JsonItem>> dictionary_0)
		{
			foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair in dictionary_0)
			{
				UI.smethod_35(keyValuePair.Key.i, false, 1);
				foreach (JsonItem jsonItem in keyValuePair.Value)
				{
					UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_20, true);
					Win32.smethod_9();
				}
			}
		}

		private static bool HaveLeftovers
		{
			get
			{
				bool result;
				using (Bitmap bitmap = UI.smethod_67())
				{
					result = UI.smethod_59(UI.bitmap_0, bitmap, CleanDumpTab.getString_0(107399322), 0.4, 0).Any<Rectangle>();
				}
				return result;
			}
		}

		private static void smethod_3()
		{
			CleanDumpTab.mainForm_0.Invoke(new Action(CleanDumpTab.<>c.<>9.method_4));
		}

		static CleanDumpTab()
		{
			Strings.CreateGetStringDelegate(typeof(CleanDumpTab));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class378
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[11];
				CleanDumpTab.Class379 @class = new CleanDumpTab.Class379();
				CleanDumpTab.mainForm_0.thread_5 = Thread.CurrentThread;
				@class.list_0 = Stashes.Items[this.int_0].OrderBy(new Func<JsonItem, int>(CleanDumpTab.<>c.<>9.method_0)).ThenBy(new Func<JsonItem, int>(CleanDumpTab.<>c.<>9.method_1)).ThenBy(new Func<JsonItem, int>(CleanDumpTab.<>c.<>9.method_2)).ThenBy(new Func<JsonItem, string>(CleanDumpTab.<>c.<>9.method_3)).ToList<JsonItem>();
				List<JsonItem> list = new List<JsonItem>();
				Dictionary<JsonTab, List<JsonItem>> dictionary = new Dictionary<JsonTab, List<JsonItem>>();
				CleanDumpTab.mainForm_0.Invoke(new Action(@class.method_0));
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[9] = ((*(int*)ptr < @class.list_0.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					JsonItem jsonItem = @class.list_0[*(int*)ptr];
					JsonTab jsonTab = Stashes.smethod_14(API.smethod_9(jsonItem));
					((byte*)ptr)[4] = ((jsonTab.i == this.int_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						CleanDumpTab.smethod_3();
					}
					else
					{
						List<Position> source = InventoryManager.smethod_8(list, jsonItem);
						((byte*)ptr)[5] = (source.Any<Position>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							Position position = source.First<Position>();
							UI.smethod_34(this.jsonTab_0.type, jsonItem.x, jsonItem.y, Enum2.const_20, false);
							Win32.smethod_9();
							jsonItem.x = position.Left;
							jsonItem.y = position.Top;
							list.Add(jsonItem);
							((byte*)ptr)[6] = ((!dictionary.ContainsKey(jsonTab)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								dictionary.Add(jsonTab, new List<JsonItem>());
							}
							dictionary[jsonTab].Add(jsonItem);
							CleanDumpTab.smethod_3();
						}
						else
						{
							*(int*)ptr = *(int*)ptr - 1;
							CleanDumpTab.smethod_2(dictionary);
							((byte*)ptr)[7] = (CleanDumpTab.HaveLeftovers ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								UI.smethod_31(false, 1, 12, 5);
								((byte*)ptr)[8] = (CleanDumpTab.mainForm_0.list_14.Any<Item>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 8) != 0)
								{
									goto IL_290;
								}
							}
							UI.smethod_35(this.jsonTab_0.i, true, 1);
							dictionary.Clear();
							list.Clear();
						}
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				CleanDumpTab.smethod_2(dictionary);
				((byte*)ptr)[10] = (CleanDumpTab.HaveLeftovers ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					this.tuple_0 = Tuple.Create<bool, string>(false, CleanDumpTab.Class378.getString_0(107248455));
					return;
				}
				return;
				IL_290:
				this.tuple_0 = Tuple.Create<bool, string>(false, CleanDumpTab.Class378.getString_0(107248455));
			}

			static Class378()
			{
				Strings.CreateGetStringDelegate(typeof(CleanDumpTab.Class378));
			}

			public int int_0;

			public JsonTab jsonTab_0;

			public Tuple<bool, string> tuple_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class379
		{
			internal void method_0()
			{
				CleanDumpTab.mainForm_0.toolStripProgressBar_0.Maximum = this.list_0.Count;
				CleanDumpTab.mainForm_0.toolStripProgressBar_0.Value = 0;
			}

			public List<JsonItem> list_0;
		}
	}
}
