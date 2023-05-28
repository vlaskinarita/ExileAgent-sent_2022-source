using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BrightIdeasSoftware
{
	public sealed class EditingCellBorderDecoration : BorderDecoration
	{
		public EditingCellBorderDecoration()
		{
			base.FillBrush = null;
			base.BorderPen = new Pen(Color.DarkBlue, 2f);
			base.CornerRounding = 8f;
			base.BoundsPadding = new Size(10, 8);
		}

		public EditingCellBorderDecoration(bool useLightBox) : this()
		{
			this.UseLightbox = this.useLightbox;
		}

		public bool UseLightbox
		{
			get
			{
				return this.useLightbox;
			}
			set
			{
				if (this.useLightbox == value)
				{
					return;
				}
				this.useLightbox = value;
				if (this.useLightbox && base.FillBrush == null)
				{
					base.FillBrush = new SolidBrush(Color.FromArgb(64, Color.Black));
				}
			}
		}

		public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			if (!olv.IsCellEditing)
			{
				return;
			}
			Rectangle bounds = olv.CellEditor.Bounds;
			if (bounds.IsEmpty)
			{
				return;
			}
			bounds.Inflate(base.BoundsPadding);
			GraphicsPath roundedRect = base.GetRoundedRect(bounds, base.CornerRounding);
			if (base.FillBrush != null)
			{
				if (this.UseLightbox)
				{
					using (Region region = new Region(r))
					{
						region.Exclude(roundedRect);
						Region clip = g.Clip;
						g.Clip = region;
						g.FillRectangle(base.FillBrush, r);
						g.Clip = clip;
						goto IL_98;
					}
				}
				g.FillPath(base.FillBrush, roundedRect);
			}
			IL_98:
			if (base.BorderPen != null)
			{
				g.DrawPath(base.BorderPen, roundedRect);
			}
		}

		private bool useLightbox;
	}
}
