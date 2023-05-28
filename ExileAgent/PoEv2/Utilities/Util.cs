using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ns12;
using ns29;
using ns35;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Utilities
{
	public static class Util
	{
		public static double smethod_0(double double_0, double double_1)
		{
			while (double_0 != 0.0 && double_1 != 0.0)
			{
				if (double_0 > double_1)
				{
					double_0 %= double_1;
				}
				else
				{
					double_1 %= double_0;
				}
			}
			return (double_0 == 0.0) ? double_1 : double_0;
		}

		public static Bitmap smethod_1(Image image_0, int int_0, int int_1)
		{
			Rectangle destRect = new Rectangle(0, 0, int_0, int_1);
			Bitmap bitmap = new Bitmap(int_0, int_1);
			bitmap.SetResolution(image_0.HorizontalResolution, image_0.VerticalResolution);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image_0, destRect, 0, 0, image_0.Width, image_0.Height, GraphicsUnit.Pixel, imageAttributes);
				}
			}
			return bitmap;
		}

		public static int smethod_2(JsonTab jsonTab_0, Item item_0)
		{
			return jsonTab_0.IsSpecialTab ? Util.smethod_3(item_0.Name) : item_0.stack_size;
		}

		public static int smethod_3(string string_0)
		{
			int result;
			if (string_0 != null && (string_0 == Util.getString_0(107382895) || string_0 == Util.getString_0(107361248)))
			{
				result = 50000;
			}
			else
			{
				result = 5000;
			}
			return result;
		}

		public unsafe static double smethod_4(int int_0, double double_0, double double_1)
		{
			void* ptr = stackalloc byte[24];
			*(double*)ptr = Math.Abs(double_0 - (double)int_0);
			*(double*)((byte*)ptr + 8) = Math.Abs(double_1 - (double)int_0);
			if (*(double*)ptr == *(double*)((byte*)ptr + 8) || *(double*)ptr < *(double*)((byte*)ptr + 8))
			{
				*(double*)((byte*)ptr + 16) = double_0;
			}
			else
			{
				*(double*)((byte*)ptr + 16) = double_1;
			}
			return *(double*)((byte*)ptr + 16);
		}

		public unsafe static string smethod_5(string string_0)
		{
			void* ptr = stackalloc byte[6];
			string text = string_0;
			((byte*)ptr)[4] = (text.smethod_9(Util.getString_0(107369299)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[5] = ((*(int*)ptr < 3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) == 0)
					{
						break;
					}
					text = text.Remove(text.LastIndexOf(Environment.NewLine));
					*(int*)ptr = *(int*)ptr + 1;
				}
				text += Environment.NewLine;
			}
			return text;
		}

		public unsafe static decimal smethod_6(string string_0)
		{
			void* ptr = stackalloc byte[3];
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberDecimalSeparator = Util.getString_0(107369290);
			decimal num;
			*(byte*)ptr = (decimal.TryParse(string_0, NumberStyles.Float, numberFormatInfo, out num) ? 1 : 0);
			decimal result;
			if (*(sbyte*)ptr != 0)
			{
				result = num;
			}
			else
			{
				numberFormatInfo.NumberDecimalSeparator = Util.getString_0(107392860);
				((byte*)ptr)[1] = (decimal.TryParse(string_0, NumberStyles.Float, numberFormatInfo, out num) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = num;
				}
				else
				{
					numberFormatInfo.NumberDecimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					((byte*)ptr)[2] = (decimal.TryParse(string_0, NumberStyles.Float, numberFormatInfo, out num) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						throw new SystemException(string.Format(Util.getString_0(107369317), string_0));
					}
					result = num;
				}
			}
			return result;
		}

		public unsafe static int smethod_7(string string_0)
		{
			void* ptr = stackalloc byte[9];
			string_0 = Util.smethod_9(string_0);
			((byte*)ptr)[8] = (int.TryParse(string_0, out *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)((byte*)ptr + 4) = *(int*)ptr;
			}
			else
			{
				*(int*)((byte*)ptr + 4) = 0;
			}
			return *(int*)((byte*)ptr + 4);
		}

		public unsafe static int smethod_8(string string_0, Font font_0)
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[4] = ((string_0 == Util.getString_0(107397236)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = 0;
			}
			else
			{
				StringFormat stringFormat = new StringFormat(StringFormat.GenericDefault);
				RectangleF bounds = new RectangleF(0f, 0f, 1000f, 1000f);
				CharacterRange[] measurableCharacterRanges = new CharacterRange[]
				{
					new CharacterRange(0, string_0.Length)
				};
				Region[] array = new Region[1];
				stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
				stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
				using (Bitmap bitmap = new Bitmap(1, 1))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						array = graphics.MeasureCharacterRanges(string_0, font_0, bounds, stringFormat);
						bounds = array[0].GetBounds(graphics);
					}
				}
				*(int*)ptr = (int)bounds.Right;
			}
			return *(int*)ptr;
		}

		public unsafe static string smethod_9(string string_0)
		{
			void* ptr = stackalloc byte[5];
			string text = string.Empty;
			*(int*)ptr = 0;
			while (*(int*)ptr < string_0.Length)
			{
				char c = string_0[*(int*)ptr];
				((byte*)ptr)[4] = (char.IsDigit(c) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					text += c.ToString();
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return text;
		}

		public static JsonSerializerSettings smethod_10()
		{
			return new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore
			};
		}

		public static JsonSerializerSettings smethod_11()
		{
			return new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore,
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		public static JsonSerializerSettings smethod_12()
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
			jsonSerializerSettings.Error = new EventHandler<ErrorEventArgs>(Util.<>c.<>9.method_0);
			return jsonSerializerSettings;
		}

		public static Predicate<T> smethod_13<T>(params Predicate<T>[] predicate_0)
		{
			Util.Class150<T> @class = new Util.Class150<T>();
			@class.predicate_0 = predicate_0;
			return new Predicate<T>(@class.method_0);
		}

		public unsafe static List<Position> smethod_14(int int_0, int int_1, int int_2, int int_3)
		{
			void* ptr = stackalloc byte[20];
			List<Position> list = new List<Position>();
			*(int*)ptr = int_0 + 1;
			for (;;)
			{
				((byte*)ptr)[16] = ((*(int*)ptr < int_0 + int_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) == 0)
				{
					break;
				}
				list.Add(new Position(*(int*)ptr, int_1));
				*(int*)ptr = *(int*)ptr + 1;
			}
			*(int*)((byte*)ptr + 4) = int_1 + 1;
			for (;;)
			{
				((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 4) < int_1 + int_3) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0)
				{
					break;
				}
				list.Add(new Position(int_0, *(int*)((byte*)ptr + 4)));
				*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
			}
			*(int*)((byte*)ptr + 8) = int_0 + 1;
			for (;;)
			{
				((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 8) < int_0 + int_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 19) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = int_1 + 1;
				for (;;)
				{
					((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 12) < int_1 + int_3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) == 0)
					{
						break;
					}
					list.Add(new Position(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return list;
		}

		public unsafe static bool smethod_15(string string_0)
		{
			void* ptr = stackalloc byte[6];
			if (string_0.Length < 5 || string_0.Length > 10)
			{
				((byte*)ptr)[4] = 0;
			}
			else if (string_0.Any(new Func<char, bool>(Util.<>c.<>9.method_1)))
			{
				((byte*)ptr)[4] = 0;
			}
			else
			{
				*(int*)ptr = 0;
				while (*(int*)ptr < string_0.Length)
				{
					char c = string_0[*(int*)ptr];
					if (char.IsUpper(c) || !char.IsLetterOrDigit(c))
					{
						((byte*)ptr)[4] = 0;
						goto IL_ED;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				foreach (string text in Class120.EnglishDictionary)
				{
					((byte*)ptr)[5] = (string_0.ToLower().Contains(text.ToLower()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						((byte*)ptr)[4] = 0;
						goto IL_ED;
					}
				}
				((byte*)ptr)[4] = 1;
			}
			IL_ED:
			return *(sbyte*)((byte*)ptr + 4) != 0;
		}

		public static string smethod_16()
		{
			return DateTime.Now.ToString(Util.getString_0(107369284));
		}

		public unsafe static void smethod_17(int[,] int_0, bool bool_0 = true)
		{
			void* ptr = stackalloc byte[10];
			string text = Util.getString_0(107397236);
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[9] = ((*(int*)ptr < int_0.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) == 0)
				{
					break;
				}
				text += Util.getString_0(107369227);
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[8] = ((*(int*)((byte*)ptr + 4) < int_0.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) == 0)
					{
						break;
					}
					text = text + int_0[*(int*)((byte*)ptr + 4), *(int*)ptr].ToString() + Util.getString_0(107392860);
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				text = text.Substring(0, text.Length - 1);
				text += Util.getString_0(107369254);
				Class181.smethod_3(bool_0 ? Enum11.const_3 : Enum11.const_0, text);
				text = Util.getString_0(107397236);
				*(int*)ptr = *(int*)ptr + 1;
			}
		}

		public unsafe static Point smethod_18(string string_0)
		{
			void* ptr = stackalloc byte[3];
			string[] array = string_0.Replace(Util.getString_0(107369249), Util.getString_0(107397236)).Split(new string[]
			{
				Util.getString_0(107397319)
			}, StringSplitOptions.RemoveEmptyEntries);
			Point point = default(Point);
			*(byte*)ptr = ((array.Count<string>() != 2) ? 1 : 0);
			Point result;
			if (*(sbyte*)ptr != 0)
			{
				result = point;
			}
			else
			{
				point.X = int.Parse(array[0]);
				point.Y = int.Parse(array[1]);
				((byte*)ptr)[1] = ((point.X < 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					point.X = 0;
				}
				((byte*)ptr)[2] = ((point.Y < 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					point.Y = 0;
				}
				result = point;
			}
			return result;
		}

		public unsafe static Size smethod_19(string string_0)
		{
			void* ptr = stackalloc byte[3];
			string[] array = string_0.Replace(Util.getString_0(107369249), Util.getString_0(107397236)).Split(new string[]
			{
				Util.getString_0(107397319)
			}, StringSplitOptions.RemoveEmptyEntries);
			Size size = default(Size);
			*(byte*)ptr = ((array.Count<string>() != 2) ? 1 : 0);
			Size result;
			if (*(sbyte*)ptr != 0)
			{
				result = size;
			}
			else
			{
				size.Width = int.Parse(array[0]);
				size.Height = int.Parse(array[1]);
				((byte*)ptr)[1] = ((size.Width < 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					size.Width = 0;
				}
				((byte*)ptr)[2] = ((size.Height < 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					size.Height = 0;
				}
				result = size;
			}
			return result;
		}

		public unsafe static string smethod_20(bool bool_0, bool bool_1, bool bool_2, bool bool_3, int int_0, int int_1)
		{
			void* ptr = stackalloc byte[13];
			Random random = new Random();
			*(int*)ptr = random.Next(int_0, int_1);
			string text = Util.getString_0(107397236);
			string text2 = Util.getString_0(107397236);
			((byte*)ptr)[8] = (bool_0 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				text += Util.getString_0(107352502);
			}
			((byte*)ptr)[9] = (bool_1 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 9) != 0)
			{
				text += Util.getString_0(107369244);
			}
			((byte*)ptr)[10] = (bool_2 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 10) != 0)
			{
				text += Util.getString_0(107368663);
			}
			((byte*)ptr)[11] = (bool_3 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 11) != 0)
			{
				text += bool_3.ToString();
			}
			*(int*)((byte*)ptr + 4) = 0;
			for (;;)
			{
				((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) == 0)
				{
					break;
				}
				text2 += text[random.Next(0, text.Length)].ToString();
				*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
			}
			return text2;
		}

		public static int smethod_21(decimal decimal_0)
		{
			return (int)Math.Round(decimal_0);
		}

		public static int smethod_22(double double_0)
		{
			return (int)Math.Round(double_0);
		}

		public static int smethod_23(params int[] int_0)
		{
			return new List<int>(int_0).Min();
		}

		public static string smethod_24(string string_0)
		{
			string result;
			if (!File.Exists(string_0))
			{
				result = string.Empty;
			}
			else
			{
				using (MD5 md = MD5.Create())
				{
					using (FileStream fileStream = File.OpenRead(string_0))
					{
						byte[] value = md.ComputeHash(fileStream);
						result = BitConverter.ToString(value).Replace(Util.getString_0(107369155), string.Empty).ToLowerInvariant();
					}
				}
			}
			return result;
		}

		public static void smethod_25()
		{
			string text = Util.getString_0(107397236);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(Util.getString_0(107368678)))
			{
				ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
				if (managementObjectCollection != null)
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						text = managementObject[Util.getString_0(107368597)].ToString();
					}
				}
				text = text.Replace(Util.getString_0(107368584), Util.getString_0(107368567));
				text = text.Replace(Util.getString_0(107368562), Util.getString_0(107368577));
			}
			Class120.UsingWindows7 = text.Contains(Util.getString_0(107368528));
			Class120.UsingWindows8 = text.Contains(Util.getString_0(107368547));
		}

		public static string smethod_26(Point point_0)
		{
			return JsonConvert.SerializeObject(point_0);
		}

		public static string smethod_27(Size size_0)
		{
			return JsonConvert.SerializeObject(size_0);
		}

		public static bool smethod_28(ushort ushort_0, int int_0)
		{
			return (ushort_0 >> int_0 & 1) != 0;
		}

		public static Thread smethod_29(ThreadStart threadStart_0)
		{
			Thread thread = new Thread(threadStart_0);
			thread.SetApartmentState(ApartmentState.STA);
			return thread;
		}

		public static JsonSerializerSettings smethod_30()
		{
			return new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All
			};
		}

		public static byte smethod_31(char char_0)
		{
			return Encoding.Unicode.GetBytes(char_0.ToString())[0];
		}

		public static void smethod_32(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				TextBox textBox = sender as TextBox;
				if (e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == ',')
				{
					if (textBox.Text.Contains(e.KeyChar))
					{
						e.Handled = true;
					}
				}
				else
				{
					e.Handled = true;
				}
			}
		}

		public unsafe static int smethod_33(string string_0)
		{
			void* ptr = stackalloc byte[8];
			int.TryParse(string_0, out *(int*)ptr);
			*(int*)((byte*)ptr + 4) = *(int*)ptr;
			return *(int*)((byte*)ptr + 4);
		}

		static Util()
		{
			Strings.CreateGetStringDelegate(typeof(Util));
		}

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class150<T>
		{
			internal unsafe bool method_0(T gparam_0)
			{
				void* ptr = stackalloc byte[6];
				Predicate<T>[] array = this.predicate_0;
				*(int*)ptr = 0;
				while (*(int*)ptr < array.Length)
				{
					Predicate<T> predicate = array[*(int*)ptr];
					((byte*)ptr)[4] = ((!predicate(gparam_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						((byte*)ptr)[5] = 0;
						IL_43:
						return *(sbyte*)((byte*)ptr + 5) != 0;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				((byte*)ptr)[5] = 1;
				goto IL_43;
			}

			public Predicate<T>[] predicate_0;
		}
	}
}
