using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	[TypeConverter("BrightIdeasSoftware.Design.OverlayConverter")]
	public class TextOverlay : TextAdornment, IOverlay, ITransparentOverlay
	{
		public TextOverlay()
		{
			base.Alignment = ContentAlignment.BottomRight;
		}

		[Description("The horizontal inset by which the position of the overlay will be adjusted")]
		[NotifyParentProperty(true)]
		[Category("ObjectListView")]
		[DefaultValue(20)]
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

		[Category("ObjectListView")]
		[Description("Gets or sets the vertical inset by which the position of the overlay will be adjusted")]
		[DefaultValue(20)]
		[NotifyParentProperty(true)]
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

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use CornerRounding instead", false)]
		public bool RoundCorneredBorder
		{
			get
			{
				return base.CornerRounding > 0f;
			}
			set
			{
				if (value)
				{
					base.CornerRounding = 16f;
					return;
				}
				base.CornerRounding = 0f;
			}
		}

		public virtual void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			if (string.IsNullOrEmpty(base.Text))
			{
				return;
			}
			Rectangle r2 = r;
			r2.Inflate(-this.InsetX, -this.InsetY);
			this.DrawText(g, r2, base.Text, 255);
		}

		private int insetX = 20;

		private int insetY = 20;
	}
}
