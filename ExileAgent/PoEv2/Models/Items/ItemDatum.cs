using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ns38;
using ns6;

namespace PoEv2.Models.Items
{
	public sealed class ItemDatum
	{
		public string Name { get; set; }

		public Enum16 CraftingClass { get; set; }

		public Enum15 Category { get; set; }

		public Size Dimensions { get; set; }

		public Bitmap ItemImage { get; set; }

		public ItemDatum(string name, Enum16 craftClass, Enum15 category, int width, int height)
		{
			this.Name = name;
			this.CraftingClass = craftClass;
			this.Category = category;
			this.Dimensions = new Size(width, height);
		}

		public ItemDatum(string name, Enum16 craftClass, Enum15 category, int width, int height, Bitmap itemImage)
		{
			this.Name = name;
			this.CraftingClass = craftClass;
			this.Category = category;
			this.Dimensions = new Size(width, height);
			this.ItemImage = itemImage;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum16 enum16_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum15 enum15_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Size size_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Bitmap bitmap_0;
	}
}
