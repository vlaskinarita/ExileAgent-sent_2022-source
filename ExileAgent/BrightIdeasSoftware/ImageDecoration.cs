using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class ImageDecoration : ImageAdornment, IOverlay, IDecoration
	{
		public ImageDecoration()
		{
			base.Alignment = ContentAlignment.MiddleRight;
		}

		public ImageDecoration(Image image) : this()
		{
			base.Image = image;
		}

		public ImageDecoration(Image image, int transparency) : this()
		{
			base.Image = image;
			base.Transparency = transparency;
		}

		public ImageDecoration(Image image, ContentAlignment alignment) : this()
		{
			base.Image = image;
			base.Alignment = alignment;
		}

		public ImageDecoration(Image image, int transparency, ContentAlignment alignment) : this()
		{
			base.Image = image;
			base.Transparency = transparency;
			base.Alignment = alignment;
		}

		public OLVListItem ListItem
		{
			get
			{
				return this.listItem;
			}
			set
			{
				this.listItem = value;
			}
		}

		public OLVListSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
			set
			{
				this.subItem = value;
			}
		}

		public void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			this.DrawImage(g, this.CalculateItemBounds(this.ListItem, this.SubItem));
		}

		private OLVListItem listItem;

		private OLVListSubItem subItem;
	}
}
