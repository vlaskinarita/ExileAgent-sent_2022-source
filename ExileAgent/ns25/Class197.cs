using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ns15;
using PoEv2;

namespace ns25
{
	internal sealed class Class197
	{
		public unsafe static Bitmap GameImage
		{
			get
			{
				void* ptr = stackalloc byte[8];
				IntPtr intptr_ = UI.intptr_0;
				IntPtr windowDC = Class197.Class199.GetWindowDC(intptr_);
				Struct2 @struct = default(Struct2);
				Class197.Class199.GetWindowRect(intptr_, ref @struct);
				@struct = UI.smethod_82(@struct);
				*(int*)ptr = @struct.int_2 - @struct.int_0;
				*(int*)((byte*)ptr + 4) = @struct.int_3 - @struct.int_1;
				IntPtr intptr_2 = Class197.Class198.CreateCompatibleDC(windowDC);
				IntPtr intPtr = Class197.Class198.CreateCompatibleBitmap(windowDC, *(int*)ptr, *(int*)((byte*)ptr + 4));
				IntPtr intptr_3 = Class197.Class198.SelectObject(intptr_2, intPtr);
				Class197.Class198.BitBlt(intptr_2, 0, 0, *(int*)ptr, *(int*)((byte*)ptr + 4), windowDC, 0, 0, 1087111200);
				Class197.Class198.SelectObject(intptr_2, intptr_3);
				Class197.Class198.DeleteDC(intptr_2);
				Class197.Class199.ReleaseDC(intptr_, windowDC);
				Bitmap result = Image.FromHbitmap(intPtr);
				Class197.Class198.DeleteObject(intPtr);
				return result;
			}
		}

		public static Bitmap smethod_0(Bitmap bitmap_0, int int_0, int int_1, int int_2, int int_3, string string_0 = "")
		{
			Bitmap result;
			try
			{
				Rectangle srcRect = new Rectangle(int_0, int_1, int_2, int_3);
				if (srcRect.Width != 0 && srcRect.Height != 0 && bitmap_0 != null)
				{
					using (Bitmap bitmap = new Bitmap(int_2, int_3))
					{
						using (Graphics graphics = Graphics.FromImage(bitmap))
						{
							graphics.DrawImage(bitmap_0, 0, 0, srcRect, GraphicsUnit.Pixel);
						}
						return new Bitmap(bitmap);
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Bitmap smethod_1(Rectangle rectangle_0, string string_0 = "")
		{
			Bitmap result;
			try
			{
				if (rectangle_0.Width != 0 && rectangle_0.Height != 0)
				{
					using (Bitmap bitmap = new Bitmap(Class197.GameImage))
					{
						using (Bitmap bitmap2 = new Bitmap(rectangle_0.Width, rectangle_0.Height))
						{
							using (Graphics graphics = Graphics.FromImage(bitmap2))
							{
								graphics.DrawImage(bitmap, 0, 0, rectangle_0, GraphicsUnit.Pixel);
							}
							return new Bitmap(bitmap2);
						}
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Bitmap smethod_2(Bitmap bitmap_0, Rectangle rectangle_0, string string_0 = "")
		{
			Bitmap result;
			try
			{
				if (rectangle_0.Width != 0 && rectangle_0.Height != 0 && bitmap_0 != null)
				{
					using (Bitmap bitmap = new Bitmap(bitmap_0))
					{
						using (Bitmap bitmap2 = new Bitmap(rectangle_0.Width, rectangle_0.Height))
						{
							using (Graphics graphics = Graphics.FromImage(bitmap2))
							{
								graphics.DrawImage(bitmap, 0, 0, rectangle_0, GraphicsUnit.Pixel);
							}
							return new Bitmap(bitmap2);
						}
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public unsafe static Bitmap WebImage
		{
			get
			{
				void* ptr = stackalloc byte[16];
				IntPtr intptr_ = UI.intptr_0;
				IntPtr windowDC = Class197.Class199.GetWindowDC(intptr_);
				Struct2 struct2_ = default(Struct2);
				Struct2 @struct = default(Struct2);
				Class197.Class199.GetWindowRect(intptr_, ref struct2_);
				Class197.Class199.GetClientRect(intptr_, ref @struct);
				struct2_ = UI.smethod_82(struct2_);
				*(int*)ptr = @struct.width;
				*(int*)((byte*)ptr + 4) = @struct.height;
				*(int*)((byte*)ptr + 8) = (struct2_.width - @struct.width) / 2;
				*(int*)((byte*)ptr + 12) = struct2_.height - @struct.height - *(int*)((byte*)ptr + 8);
				IntPtr intptr_2 = Class197.Class198.CreateCompatibleDC(windowDC);
				IntPtr intPtr = Class197.Class198.CreateCompatibleBitmap(windowDC, *(int*)ptr, *(int*)((byte*)ptr + 4));
				IntPtr intptr_3 = Class197.Class198.SelectObject(intptr_2, intPtr);
				Class197.Class198.BitBlt(intptr_2, 0, 0, *(int*)ptr, *(int*)((byte*)ptr + 4), windowDC, *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12), 13369376);
				Class197.Class198.SelectObject(intptr_2, intptr_3);
				Class197.Class198.DeleteDC(intptr_2);
				Class197.Class199.ReleaseDC(intptr_, windowDC);
				Bitmap result = Image.FromHbitmap(intPtr);
				Class197.Class198.DeleteObject(intPtr);
				return result;
			}
		}

		private static class Class198
		{
			[DllImport("gdi32.dll")]
			public static extern bool BitBlt(IntPtr intptr_0, int int_2, int int_3, int int_4, int int_5, IntPtr intptr_1, int int_6, int int_7, int int_8);

			[DllImport("gdi32.dll")]
			public static extern IntPtr CreateCompatibleBitmap(IntPtr intptr_0, int int_2, int int_3);

			[DllImport("gdi32.dll")]
			public static extern IntPtr CreateCompatibleDC(IntPtr intptr_0);

			[DllImport("gdi32.dll")]
			public static extern bool DeleteDC(IntPtr intptr_0);

			[DllImport("gdi32.dll")]
			public static extern bool DeleteObject(IntPtr intptr_0);

			[DllImport("gdi32.dll")]
			public static extern IntPtr SelectObject(IntPtr intptr_0, IntPtr intptr_1);

			public const int int_0 = 1073741824;

			public const int int_1 = 13369376;
		}

		private static class Class199
		{
			[DllImport("user32.dll")]
			public static extern IntPtr GetDesktopWindow();

			[DllImport("user32.dll")]
			public static extern IntPtr GetWindowDC(IntPtr intptr_0);

			[DllImport("user32.dll")]
			public static extern IntPtr ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);

			[DllImport("user32.dll")]
			public static extern IntPtr GetWindowRect(IntPtr intptr_0, ref Struct2 struct2_0);

			[DllImport("user32.dll")]
			public static extern IntPtr GetClientRect(IntPtr intptr_0, ref Struct2 struct2_0);
		}
	}
}
