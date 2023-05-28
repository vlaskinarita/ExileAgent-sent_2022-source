using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ns0;
using ns14;
using ns2;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class StackedDeckOpener
	{
		[DebuggerStepThrough]
		public static void smethod_0(MainForm mainForm_1)
		{
			StackedDeckOpener.Class348 @class = new StackedDeckOpener.Class348();
			@class.mainForm_0 = mainForm_1;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<StackedDeckOpener.Class348>(ref @class);
		}

		private static void smethod_1(bool bool_0 = true)
		{
			StackedDeckOpener.mainForm_0.method_121();
			StackedDeckOpener.mainForm_0.enum8_0 = Enum8.const_1;
			if (bool_0 && StackedDeckOpener.mainForm_0.thread_5 != null)
			{
				StackedDeckOpener.mainForm_0.thread_5.Abort();
				StackedDeckOpener.mainForm_0.thread_5 = null;
			}
		}

		private unsafe static bool smethod_2()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, StackedDeckOpener.getString_0(107273313));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((!Class255.StackedDeckStashIds.Any<int>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, StackedDeckOpener.getString_0(107271741));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[3] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						StackedDeckOpener.smethod_1(true);
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[4] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							Class181.smethod_3(Enum11.const_2, StackedDeckOpener.getString_0(107273175));
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

		private unsafe static void smethod_3()
		{
			void* ptr = stackalloc byte[2];
			JsonTab key = StackedDeckOpener.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
			JsonItem jsonItem = StackedDeckOpener.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
			*(byte*)ptr = ((StackedDeckOpener.int_0 != key.i) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				UI.smethod_35(key.i, false, 1);
				StackedDeckOpener.int_0 = key.i;
			}
			JsonItem jsonItem2 = new JsonItem
			{
				w = 1,
				h = 1
			};
			Position position = InventoryManager.smethod_8(StackedDeckOpener.list_0, jsonItem2).First<Position>();
			UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_2, false);
			Thread.Sleep(50);
			Win32.smethod_3();
			Thread.Sleep(50);
			UI.smethod_32(position.x, position.y, Enum2.const_3, true);
			Thread.Sleep(50);
			Win32.smethod_2(true);
			((byte*)ptr)[1] = ((jsonItem.stack == 1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				StackedDeckOpener.dictionary_0[key].Remove(jsonItem);
			}
			else
			{
				JsonItem jsonItem3 = jsonItem;
				int stack = jsonItem3.stack;
				jsonItem3.stack = stack - 1;
			}
			jsonItem2.x = position.x;
			jsonItem2.y = position.y;
			StackedDeckOpener.list_0.Add(jsonItem2);
			StackedDeckOpener.mainForm_0.method_123(1);
		}

		public unsafe static bool smethod_4()
		{
			void* ptr = stackalloc byte[15];
			foreach (JsonItem jsonItem in StackedDeckOpener.list_0.OrderBy(new Func<JsonItem, int>(StackedDeckOpener.<>c.<>9.method_0)).ThenBy(new Func<JsonItem, int>(StackedDeckOpener.<>c.<>9.method_1)))
			{
				((byte*)ptr)[8] = ((!StackedDeckOpener.list_1.Any<int>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Class181.smethod_3(Enum11.const_2, StackedDeckOpener.getString_0(107271696));
					((byte*)ptr)[9] = 0;
					goto IL_23B;
				}
				*(int*)ptr = StackedDeckOpener.list_1.First<int>();
				((byte*)ptr)[10] = ((StackedDeckOpener.int_0 != *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					UI.smethod_35(*(int*)ptr, false, 1);
					StackedDeckOpener.int_0 = *(int*)ptr;
				}
				UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
				Thread.Sleep(50);
				Win32.smethod_9();
			}
			StackedDeckOpener.smethod_5();
			((byte*)ptr)[11] = ((UI.smethod_83(12) > 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 11) != 0)
			{
				Class181.smethod_3(Enum11.const_2, StackedDeckOpener.getString_0(107271651));
				StackedDeckOpener.list_1.RemoveAt(0);
				int[,] array = UI.smethod_84();
				StackedDeckOpener.Class349 @class = new StackedDeckOpener.Class349();
				@class.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[14] = ((@class.int_0 < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) == 0)
					{
						break;
					}
					StackedDeckOpener.Class350 class2 = new StackedDeckOpener.Class350();
					class2.class349_0 = @class;
					class2.int_0 = 0;
					for (;;)
					{
						((byte*)ptr)[13] = ((class2.int_0 < array.GetLength(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) == 0)
						{
							break;
						}
						((byte*)ptr)[12] = ((array[class2.class349_0.int_0, class2.int_0] == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							StackedDeckOpener.list_0.RemoveAll(new Predicate<JsonItem>(class2.method_0));
						}
						*(int*)((byte*)ptr + 4) = class2.int_0;
						class2.int_0 = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)((byte*)ptr + 4) = @class.int_0;
					@class.int_0 = *(int*)((byte*)ptr + 4) + 1;
				}
				((byte*)ptr)[9] = (StackedDeckOpener.smethod_4() ? 1 : 0);
			}
			else
			{
				StackedDeckOpener.list_0.Clear();
				((byte*)ptr)[9] = 1;
			}
			IL_23B:
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		private unsafe static void smethod_5()
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[8] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				int[,] array = UI.smethod_84();
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[11] = ((*(int*)ptr < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[10] = ((*(int*)((byte*)ptr + 4) < array.GetLength(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) == 0)
						{
							break;
						}
						((byte*)ptr)[9] = ((array[*(int*)ptr, *(int*)((byte*)ptr + 4)] == 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							UI.smethod_32(*(int*)ptr, *(int*)((byte*)ptr + 4), Enum2.const_3, true);
							Thread.Sleep(50);
							Win32.smethod_9();
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static StackedDeckOpener()
		{
			Strings.CreateGetStringDelegate(typeof(StackedDeckOpener));
			StackedDeckOpener.list_0 = new List<JsonItem>();
			StackedDeckOpener.int_0 = -1;
			StackedDeckOpener.list_1 = new List<int>();
		}

		private static MainForm mainForm_0;

		private static Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private static List<JsonItem> list_0;

		private static int int_0;

		private static List<int> list_1;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class346
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				StackedDeckOpener.Class346.Class347 @class = new StackedDeckOpener.Class346.Class347();
				@class.class346_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<StackedDeckOpener.Class346.Class347>(ref @class);
			}

			internal unsafe void method_1()
			{
				void* ptr = stackalloc byte[5];
				StackedDeckOpener.mainForm_0 = this.mainForm_0;
				StackedDeckOpener.mainForm_0.thread_5 = Thread.CurrentThread;
				StackedDeckOpener.mainForm_0.method_116();
				UI.smethod_1();
				*(byte*)ptr = ((!StackedDeckOpener.smethod_2()) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					StackedDeckOpener.smethod_1(true);
				}
				else
				{
					StackedDeckOpener.dictionary_0 = StashManager.smethod_1(StackedDeckOpener.Class346.getString_0(107366748), true);
					StackedDeckOpener.list_1 = Class255.StackedDeckStashIds.ToList<int>();
					StackedDeckOpener.list_0.Clear();
					((byte*)ptr)[1] = ((StackedDeckOpener.dictionary_0.smethod_16(false) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Class181.smethod_3(Enum11.const_2, StackedDeckOpener.Class346.getString_0(107249726));
						StackedDeckOpener.smethod_1(true);
					}
					else
					{
						Class181.smethod_2(Enum11.const_0, StackedDeckOpener.Class346.getString_0(107249625), new object[]
						{
							StackedDeckOpener.dictionary_0.smethod_16(false)
						});
						StackedDeckOpener.mainForm_0.method_122(StackedDeckOpener.dictionary_0.smethod_16(false));
						for (;;)
						{
							((byte*)ptr)[4] = ((StackedDeckOpener.dictionary_0.smethod_16(false) > 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) == 0)
							{
								break;
							}
							StackedDeckOpener.smethod_3();
							((byte*)ptr)[2] = ((StackedDeckOpener.list_0.Count == 60) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								((byte*)ptr)[3] = ((!StackedDeckOpener.smethod_4()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 3) != 0)
								{
									goto IL_140;
								}
							}
						}
						return;
						IL_140:
						StackedDeckOpener.smethod_1(true);
					}
				}
			}

			static Class346()
			{
				Strings.CreateGetStringDelegate(typeof(StackedDeckOpener.Class346));
			}

			public MainForm mainForm_0;

			public Action action_0;

			[NonSerialized]
			internal static GetString getString_0;

			private sealed class Class347 : IAsyncStateMachine
			{
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							Action action;
							if ((action = this.class346_0.action_0) == null)
							{
								action = (this.class346_0.action_0 = new Action(this.class346_0.method_1));
							}
							awaiter = Task.Run(action).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.configuredTaskAwaiter_0 = awaiter;
								StackedDeckOpener.Class346.Class347 @class = this;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StackedDeckOpener.Class346.Class347>(ref awaiter, ref @class);
								return;
							}
						}
						else
						{
							awaiter = this.configuredTaskAwaiter_0;
							this.configuredTaskAwaiter_0 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.int_0 = -1;
						}
						awaiter.GetResult();
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				public int int_0;

				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				public StackedDeckOpener.Class346 class346_0;

				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class349
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class350
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.class349_0.int_0 && jsonItem_0.y == this.int_0;
			}

			public int int_0;

			public StackedDeckOpener.Class349 class349_0;
		}
	}
}
