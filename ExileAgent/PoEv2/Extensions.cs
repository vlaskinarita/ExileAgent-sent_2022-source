using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using PoEv2.Models;

namespace PoEv2
{
	public static class Extensions
	{
		public static IEnumerable<T> smethod_0<T>(this IEnumerable<T> ienumerable_0, int int_0)
		{
			return ienumerable_0.Skip(Math.Max(0, ienumerable_0.Count<T>() - int_0));
		}

		public static string smethod_1(this ComboBox comboBox_0)
		{
			Extensions.Class107 @class = new Extensions.Class107();
			@class.comboBox_0 = comboBox_0;
			@class.string_0 = string.Empty;
			try
			{
				@class.comboBox_0.Invoke(new Action(@class.method_0));
			}
			catch
			{
			}
			return @class.string_0;
		}

		public static void smethod_2(this NumericUpDown numericUpDown_0, decimal decimal_0)
		{
			Extensions.Class108 @class = new Extensions.Class108();
			@class.decimal_0 = decimal_0;
			@class.numericUpDown_0 = numericUpDown_0;
			@class.numericUpDown_0.Invoke(new Action(@class.method_0));
		}

		public unsafe static Position smethod_3(this Position position_0, Point point_0)
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = position_0.Left + point_0.X;
			*(int*)((byte*)ptr + 4) = position_0.Top + point_0.Y;
			return new Position(*(int*)ptr, *(int*)((byte*)ptr + 4));
		}

		public unsafe static Position smethod_4(this Position position_0, Point point_0)
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = position_0.Left + (int)Math.Round(point_0.X);
			*(int*)((byte*)ptr + 4) = position_0.Top + (int)Math.Round(point_0.Y);
			return new Position(*(int*)ptr, *(int*)((byte*)ptr + 4));
		}

		public unsafe static Position smethod_5(this Position position_0, Position position_1)
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = position_0.Left + position_1.X;
			*(int*)((byte*)ptr + 4) = position_0.Top + position_1.Y;
			return new Position(*(int*)ptr, *(int*)((byte*)ptr + 4));
		}

		public unsafe static Position smethod_6(this Position position_0, int int_0, int int_1)
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = position_0.Left + int_0;
			*(int*)((byte*)ptr + 4) = position_0.Top + int_1;
			return new Position(*(int*)ptr, *(int*)((byte*)ptr + 4));
		}

		public static Point smethod_7(this Position position_0)
		{
			return new Point(position_0.X, position_0.Y);
		}

		public static void smethod_8(this List<JsonItem> list_0, JsonItem jsonItem_0)
		{
			Extensions.Class109 @class = new Extensions.Class109();
			@class.jsonItem_0 = jsonItem_0;
			if (!list_0.Any(new Func<JsonItem, bool>(@class.method_0)))
			{
				list_0.Add(@class.jsonItem_0);
			}
		}

		public unsafe static bool smethod_9(this string string_0, string string_1)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((string_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (string_0.Contains(string_1) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static string smethod_10(this object object_0)
		{
			return (object_0 == null) ? string.Empty : object_0.ToString();
		}

		public static List<JsonItem> smethod_11(this List<Item> list_0)
		{
			List<JsonItem> list = new List<JsonItem>();
			List<JsonItem> result;
			if (list_0 == null)
			{
				result = list;
			}
			else
			{
				foreach (Item item in list_0)
				{
					list.Add(item.method_2());
				}
				result = list;
			}
			return result;
		}

		public static void smethod_12(this Bitmap bitmap_0)
		{
			if (bitmap_0 != null)
			{
				bitmap_0.Dispose();
			}
		}

		public static void smethod_13<T>(this List<T> list_0, Func<T, bool> func_0)
		{
			T t = list_0.FirstOrDefault(func_0);
			if (t != null)
			{
				list_0.Remove(t);
			}
		}

		public static void smethod_14(this RichTextBox richTextBox_0, string string_0, Color color_0)
		{
			richTextBox_0.SelectionStart = richTextBox_0.TextLength;
			richTextBox_0.SelectionLength = 0;
			richTextBox_0.SelectionColor = color_0;
			richTextBox_0.AppendText(string_0);
			richTextBox_0.SelectionColor = richTextBox_0.ForeColor;
		}

		public static List<JsonItem> smethod_15<T>(this Dictionary<T, List<JsonItem>> dictionary_0)
		{
			List<JsonItem> list = new List<JsonItem>();
			foreach (KeyValuePair<T, List<JsonItem>> keyValuePair in dictionary_0)
			{
				list.AddRange(keyValuePair.Value);
			}
			return list;
		}

		public static int smethod_16(this Dictionary<JsonTab, List<JsonItem>> dictionary_0, bool bool_0 = false)
		{
			int result;
			if (bool_0)
			{
				result = dictionary_0.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(Extensions.<>c.<>9.method_0));
			}
			else
			{
				result = dictionary_0.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(Extensions.<>c.<>9.method_3));
			}
			return result;
		}

		public static IEnumerable<TradeWindowItem> smethod_17(this List<TradeWindowItem> list_0)
		{
			return list_0.Where(new Func<TradeWindowItem, bool>(Extensions.<>c.<>9.method_5));
		}

		public static string[] smethod_18(this string string_0, string string_1)
		{
			return string_0.Split(new string[]
			{
				string_1
			}, StringSplitOptions.None);
		}

		public static string[] smethod_19(this string string_0, string string_1, StringSplitOptions stringSplitOptions_0)
		{
			return string_0.Split(new string[]
			{
				string_1
			}, stringSplitOptions_0);
		}

		public static bool smethod_20(this string string_0, string string_1, StringComparison stringComparison_0)
		{
			return string_0 != null && string_0.IndexOf(string_1, stringComparison_0) >= 0;
		}

		public static void smethod_21<T>(this List<T> list_0, IEnumerable<T> ienumerable_0)
		{
			if (ienumerable_0 != null)
			{
				list_0.AddRange(ienumerable_0);
			}
		}

		public static void smethod_22(this ComboBox.ObjectCollection objectCollection_0, IEnumerable<object> ienumerable_0)
		{
			objectCollection_0.AddRange(ienumerable_0.ToArray<object>());
		}

		public static void smethod_23<T>(this List<T> list_0, params IEnumerable<T>[] ienumerable_0)
		{
			foreach (IEnumerable<T> ienumerable_ in ienumerable_0)
			{
				list_0.smethod_21(ienumerable_);
			}
		}

		public unsafe static void smethod_24<T>(this IList<T> ilist_0)
		{
			void* ptr = stackalloc byte[9];
			*(int*)ptr = ilist_0.Count;
			for (;;)
			{
				((byte*)ptr)[8] = ((*(int*)ptr > 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) == 0)
				{
					break;
				}
				*(int*)ptr = *(int*)ptr - 1;
				*(int*)((byte*)ptr + 4) = Extensions.random_0.Next(*(int*)ptr + 1);
				T value = ilist_0[*(int*)((byte*)ptr + 4)];
				ilist_0[*(int*)((byte*)ptr + 4)] = ilist_0[*(int*)ptr];
				ilist_0[*(int*)ptr] = value;
			}
		}

		public static bool smethod_25(this string string_0)
		{
			return string.IsNullOrEmpty(string_0);
		}

		public static void smethod_26(this DataGridViewCell dataGridViewCell_0, bool bool_0)
		{
			dataGridViewCell_0.Style.BackColor = (bool_0 ? Color.White : Color.LightGray);
			dataGridViewCell_0.Style.BackColor = (bool_0 ? Color.White : Color.LightGray);
			dataGridViewCell_0.ReadOnly = !bool_0;
			dataGridViewCell_0.ReadOnly = !bool_0;
		}

		private static Random random_0 = new Random();

		[CompilerGenerated]
		private sealed class Class107
		{
			internal void method_0()
			{
				if (this.comboBox_0 == null || this.comboBox_0.SelectedItem == null)
				{
					this.string_0 = string.Empty;
				}
				else
				{
					this.string_0 = this.comboBox_0.SelectedItem.ToString();
				}
			}

			public ComboBox comboBox_0;

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class108
		{
			internal void method_0()
			{
				if (this.decimal_0 < this.numericUpDown_0.Minimum || this.decimal_0 > this.numericUpDown_0.Maximum)
				{
					this.decimal_0 = this.numericUpDown_0.Minimum;
				}
				this.numericUpDown_0.Value = this.decimal_0;
			}

			public decimal decimal_0;

			public NumericUpDown numericUpDown_0;
		}

		[CompilerGenerated]
		private sealed class Class109
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}
	}
}
