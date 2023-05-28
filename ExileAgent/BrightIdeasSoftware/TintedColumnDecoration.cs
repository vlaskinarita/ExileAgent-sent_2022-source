using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class TintedColumnDecoration : AbstractDecoration
	{
		public TintedColumnDecoration()
		{
			this.Tint = Color.FromArgb(15, Color.Blue);
		}

		public TintedColumnDecoration(OLVColumn column) : this()
		{
			this.ColumnToTint = column;
		}

		public OLVColumn ColumnToTint
		{
			get
			{
				return this.columnToTint;
			}
			set
			{
				this.columnToTint = value;
			}
		}

		public Color Tint
		{
			get
			{
				return this.tint;
			}
			set
			{
				if (this.tint == value)
				{
					return;
				}
				if (this.tintBrush != null)
				{
					this.tintBrush.Dispose();
					this.tintBrush = null;
				}
				this.tint = value;
				this.tintBrush = new SolidBrush(this.tint);
			}
		}

		public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			if (olv.View != View.Details)
			{
				return;
			}
			if (olv.GetItemCount() == 0)
			{
				return;
			}
			OLVColumn olvcolumn = this.ColumnToTint ?? olv.SelectedColumn;
			if (olvcolumn == null)
			{
				return;
			}
			Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides(olv, olvcolumn.Index);
			if (scrolledColumnSides.X == -1)
			{
				return;
			}
			Rectangle rect = new Rectangle(scrolledColumnSides.X, r.Top, scrolledColumnSides.Y - scrolledColumnSides.X, r.Bottom);
			OLVListItem lastItemInDisplayOrder = olv.GetLastItemInDisplayOrder();
			if (lastItemInDisplayOrder != null)
			{
				Rectangle bounds = lastItemInDisplayOrder.Bounds;
				if (!bounds.IsEmpty && bounds.Bottom < rect.Bottom)
				{
					rect.Height = bounds.Bottom - rect.Top;
				}
			}
			g.FillRectangle(this.tintBrush, rect);
		}

		private OLVColumn columnToTint;

		private Color tint;

		private SolidBrush tintBrush;
	}
}
