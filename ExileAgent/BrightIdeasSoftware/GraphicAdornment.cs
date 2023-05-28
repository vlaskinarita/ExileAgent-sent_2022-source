using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BrightIdeasSoftware
{
	public class GraphicAdornment
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ContentAlignment AdornmentCorner
		{
			get
			{
				return this.adornmentCorner;
			}
			set
			{
				this.adornmentCorner = value;
			}
		}

		[DefaultValue(ContentAlignment.BottomRight)]
		[Category("ObjectListView")]
		[Description("How will the adornment be aligned")]
		[NotifyParentProperty(true)]
		public ContentAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
				this.ReferenceCorner = value;
				this.AdornmentCorner = value;
			}
		}

		[Category("ObjectListView")]
		[Description("The offset by which the position of the adornment will be adjusted")]
		[DefaultValue(typeof(Size), "0,0")]
		public Size Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ContentAlignment ReferenceCorner
		{
			get
			{
				return this.referenceCorner;
			}
			set
			{
				this.referenceCorner = value;
			}
		}

		[Category("ObjectListView")]
		[NotifyParentProperty(true)]
		[Description("The degree of rotation that will be applied to the adornment.")]
		[DefaultValue(0)]
		public int Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		[DefaultValue(128)]
		[Category("ObjectListView")]
		[Description("The transparency of this adornment. 0 is completely transparent, 255 is completely opaque.")]
		public int Transparency
		{
			get
			{
				return this.transparency;
			}
			set
			{
				this.transparency = Math.Min(255, Math.Max(0, value));
			}
		}

		public virtual Point CalculateAlignedPosition(Point pt, Size size, ContentAlignment corner)
		{
			if (corner <= ContentAlignment.MiddleCenter)
			{
				switch (corner)
				{
				case ContentAlignment.TopLeft:
					return pt;
				case ContentAlignment.TopCenter:
					return new Point(pt.X - size.Width / 2, pt.Y);
				case (ContentAlignment)3:
					break;
				case ContentAlignment.TopRight:
					return new Point(pt.X - size.Width, pt.Y);
				default:
					if (corner == ContentAlignment.MiddleLeft)
					{
						return new Point(pt.X, pt.Y - size.Height / 2);
					}
					if (corner == ContentAlignment.MiddleCenter)
					{
						return new Point(pt.X - size.Width / 2, pt.Y - size.Height / 2);
					}
					break;
				}
			}
			else if (corner <= ContentAlignment.BottomLeft)
			{
				if (corner == ContentAlignment.MiddleRight)
				{
					return new Point(pt.X - size.Width, pt.Y - size.Height / 2);
				}
				if (corner == ContentAlignment.BottomLeft)
				{
					return new Point(pt.X, pt.Y - size.Height);
				}
			}
			else
			{
				if (corner == ContentAlignment.BottomCenter)
				{
					return new Point(pt.X - size.Width / 2, pt.Y - size.Height);
				}
				if (corner == ContentAlignment.BottomRight)
				{
					return new Point(pt.X - size.Width, pt.Y - size.Height);
				}
			}
			return pt;
		}

		public virtual Rectangle CreateAlignedRectangle(Rectangle r, Size sz)
		{
			return this.CreateAlignedRectangle(r, sz, this.ReferenceCorner, this.AdornmentCorner, this.Offset);
		}

		public virtual Rectangle CreateAlignedRectangle(Rectangle r, Size sz, ContentAlignment corner, ContentAlignment referenceCorner, Size offset)
		{
			Point pt = this.CalculateCorner(r, referenceCorner);
			Point pt2 = this.CalculateAlignedPosition(pt, sz, corner);
			return new Rectangle(pt2 + offset, sz);
		}

		public virtual Point CalculateCorner(Rectangle r, ContentAlignment corner)
		{
			if (corner <= ContentAlignment.MiddleCenter)
			{
				switch (corner)
				{
				case ContentAlignment.TopLeft:
					return new Point(r.Left, r.Top);
				case ContentAlignment.TopCenter:
					return new Point(r.X + r.Width / 2, r.Top);
				case (ContentAlignment)3:
					break;
				case ContentAlignment.TopRight:
					return new Point(r.Right, r.Top);
				default:
					if (corner == ContentAlignment.MiddleLeft)
					{
						return new Point(r.Left, r.Top + r.Height / 2);
					}
					if (corner == ContentAlignment.MiddleCenter)
					{
						return new Point(r.X + r.Width / 2, r.Top + r.Height / 2);
					}
					break;
				}
			}
			else if (corner <= ContentAlignment.BottomLeft)
			{
				if (corner == ContentAlignment.MiddleRight)
				{
					return new Point(r.Right, r.Top + r.Height / 2);
				}
				if (corner == ContentAlignment.BottomLeft)
				{
					return new Point(r.Left, r.Bottom);
				}
			}
			else
			{
				if (corner == ContentAlignment.BottomCenter)
				{
					return new Point(r.X + r.Width / 2, r.Bottom);
				}
				if (corner == ContentAlignment.BottomRight)
				{
					return new Point(r.Right, r.Bottom);
				}
			}
			return r.Location;
		}

		public virtual Rectangle CalculateItemBounds(OLVListItem item, OLVListSubItem subItem)
		{
			if (item == null)
			{
				return Rectangle.Empty;
			}
			if (subItem == null)
			{
				return item.Bounds;
			}
			return item.GetSubItemBounds(item.SubItems.IndexOf(subItem));
		}

		protected virtual void ApplyRotation(Graphics g, Rectangle r)
		{
			if (this.Rotation == 0)
			{
				return;
			}
			g.ResetTransform();
			Matrix matrix = new Matrix();
			matrix.RotateAt((float)this.Rotation, new Point(r.Left + r.Width / 2, r.Top + r.Height / 2));
			g.Transform = matrix;
		}

		protected virtual void UnapplyRotation(Graphics g)
		{
			if (this.Rotation != 0)
			{
				g.ResetTransform();
			}
		}

		private ContentAlignment adornmentCorner = ContentAlignment.MiddleCenter;

		private ContentAlignment alignment = ContentAlignment.BottomRight;

		private Size offset = default(Size);

		private ContentAlignment referenceCorner = ContentAlignment.MiddleCenter;

		private int rotation;

		private int transparency = 128;
	}
}
