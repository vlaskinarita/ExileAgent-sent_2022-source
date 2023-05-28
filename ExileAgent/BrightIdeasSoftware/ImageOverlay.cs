using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	[TypeConverter("BrightIdeasSoftware.Design.OverlayConverter")]
	public sealed class ImageOverlay : ImageAdornment, IOverlay, ITransparentOverlay
	{
		public ImageOverlay()
		{
			base.Alignment = ContentAlignment.BottomRight;
		}

		[DefaultValue(20)]
		[Category("ObjectListView")]
		[NotifyParentProperty(true)]
		[Description("The horizontal inset by which the position of the overlay will be adjusted")]
		public int InsetX
		{
			get
			{
				return this.insetX;
			}
			set
			{
				this.insetX = Math.Max(0, value);
			}
		}

		[NotifyParentProperty(true)]
		[Description("Gets or sets the vertical inset by which the position of the overlay will be adjusted")]
		[DefaultValue(20)]
		[Category("ObjectListView")]
		public int InsetY
		{
			get
			{
				return this.insetY;
			}
			set
			{
				this.insetY = Math.Max(0, value);
			}
		}

		public void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			Rectangle r2 = r;
			r2.Inflate(-this.InsetX, -this.InsetY);
			this.DrawImage(g, r2, base.Image, 255);
		}

		private int insetX = 20;

		private int insetY = 20;
	}
}
