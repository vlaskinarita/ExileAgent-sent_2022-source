using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public class AbstractDecoration : IOverlay, IDecoration
	{
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

		public Rectangle RowBounds
		{
			get
			{
				if (this.ListItem == null)
				{
					return Rectangle.Empty;
				}
				return this.ListItem.Bounds;
			}
		}

		public Rectangle CellBounds
		{
			get
			{
				if (this.ListItem != null && this.SubItem != null)
				{
					return this.ListItem.GetSubItemBounds(this.ListItem.SubItems.IndexOf(this.SubItem));
				}
				return Rectangle.Empty;
			}
		}

		public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
		}

		private OLVListItem listItem;

		private OLVListSubItem subItem;
	}
}
