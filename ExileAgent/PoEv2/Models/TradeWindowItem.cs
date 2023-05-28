using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ns29;
using PoEv2.Managers;
using PoEv2.Utilities;

namespace PoEv2.Models
{
	public sealed class TradeWindowItem
	{
		public Bitmap Image { get; set; }

		public Bitmap CroppedImage { get; set; }

		public Position Position { get; set; }

		public Item Item { get; set; }

		public bool MousedOverOnce { get; set; }

		public TradeWindowItem(Bitmap image, Position pos)
		{
			this.Image = new Bitmap(image);
			this.Position = pos;
			int num = TradeWindowItem.smethod_0();
			this.CroppedImage = new Bitmap(num, num);
			using (Graphics graphics = Graphics.FromImage(this.CroppedImage))
			{
				graphics.DrawImage(this.Image, 0, 0, this.CroppedImage.Width, this.CroppedImage.Height);
			}
		}

		public unsafe void method_0()
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = Class308.smethod_0(Images.EmptyCell).Item1.Width;
			*(int*)((byte*)ptr + 4) = TradeWindowItem.smethod_0();
			this.Image.smethod_12();
			this.CroppedImage.smethod_12();
			this.Image = new Bitmap(*(int*)ptr, *(int*)ptr);
			this.CroppedImage = new Bitmap(*(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 4));
			this.Item = null;
			this.MousedOverOnce = false;
		}

		private unsafe static int smethod_0()
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = Class308.smethod_0(Images.EmptyCell).Item1.Width;
			*(int*)((byte*)ptr + 4) = Util.smethod_22((double)(*(int*)ptr) * 0.47);
			return *(int*)((byte*)ptr + 4);
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Bitmap bitmap_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Bitmap bitmap_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Position position_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Item item_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;
	}
}
