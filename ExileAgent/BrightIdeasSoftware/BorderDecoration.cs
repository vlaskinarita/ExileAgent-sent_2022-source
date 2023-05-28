using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BrightIdeasSoftware
{
	public class BorderDecoration : AbstractDecoration
	{
		public BorderDecoration() : this(new Pen(Color.FromArgb(64, Color.Blue), 1f))
		{
		}

		public BorderDecoration(Pen borderPen)
		{
			this.boundsPadding = new Size(-1, 2);
			this.cornerRounding = 16f;
			this.fillBrush = new SolidBrush(Color.FromArgb(64, Color.Blue));
			this.fillGradientMode = LinearGradientMode.Vertical;
			base..ctor();
			this.BorderPen = borderPen;
		}

		public BorderDecoration(Pen borderPen, Brush fill)
		{
			this.boundsPadding = new Size(-1, 2);
			this.cornerRounding = 16f;
			this.fillBrush = new SolidBrush(Color.FromArgb(64, Color.Blue));
			this.fillGradientMode = LinearGradientMode.Vertical;
			base..ctor();
			this.BorderPen = borderPen;
			this.FillBrush = fill;
		}

		public Pen BorderPen
		{
			get
			{
				return this.borderPen;
			}
			set
			{
				this.borderPen = value;
			}
		}

		public Size BoundsPadding
		{
			get
			{
				return this.boundsPadding;
			}
			set
			{
				this.boundsPadding = value;
			}
		}

		public float CornerRounding
		{
			get
			{
				return this.cornerRounding;
			}
			set
			{
				this.cornerRounding = value;
			}
		}

		public Brush FillBrush
		{
			get
			{
				return this.fillBrush;
			}
			set
			{
				this.fillBrush = value;
			}
		}

		public Color? FillGradientFrom
		{
			get
			{
				return this.fillGradientFrom;
			}
			set
			{
				this.fillGradientFrom = value;
			}
		}

		public Color? FillGradientTo
		{
			get
			{
				return this.fillGradientTo;
			}
			set
			{
				this.fillGradientTo = value;
			}
		}

		public LinearGradientMode FillGradientMode
		{
			get
			{
				return this.fillGradientMode;
			}
			set
			{
				this.fillGradientMode = value;
			}
		}

		public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			Rectangle bounds = this.CalculateBounds();
			if (!bounds.IsEmpty)
			{
				this.DrawFilledBorder(g, bounds);
			}
		}

		protected virtual Rectangle CalculateBounds()
		{
			return Rectangle.Empty;
		}

		protected void DrawFilledBorder(Graphics g, Rectangle bounds)
		{
			bounds.Inflate(this.BoundsPadding);
			GraphicsPath roundedRect = this.GetRoundedRect(bounds, this.CornerRounding);
			if (this.FillGradientFrom != null && this.FillGradientTo != null)
			{
				if (this.FillBrush != null)
				{
					this.FillBrush.Dispose();
				}
				this.FillBrush = new LinearGradientBrush(bounds, this.FillGradientFrom.Value, this.FillGradientTo.Value, this.FillGradientMode);
			}
			if (this.FillBrush != null)
			{
				g.FillPath(this.FillBrush, roundedRect);
			}
			if (this.BorderPen != null)
			{
				g.DrawPath(this.BorderPen, roundedRect);
			}
		}

		protected GraphicsPath GetRoundedRect(RectangleF rect, float diameter)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (diameter <= 0f)
			{
				graphicsPath.AddRectangle(rect);
			}
			else
			{
				RectangleF rect2 = new RectangleF(rect.X, rect.Y, diameter, diameter);
				graphicsPath.AddArc(rect2, 180f, 90f);
				rect2.X = rect.Right - diameter;
				graphicsPath.AddArc(rect2, 270f, 90f);
				rect2.Y = rect.Bottom - diameter;
				graphicsPath.AddArc(rect2, 0f, 90f);
				rect2.X = rect.Left;
				graphicsPath.AddArc(rect2, 90f, 90f);
				graphicsPath.CloseFigure();
			}
			return graphicsPath;
		}

		private Pen borderPen;

		private Size boundsPadding;

		private float cornerRounding;

		private Brush fillBrush;

		private Color? fillGradientFrom;

		private Color? fillGradientTo;

		private LinearGradientMode fillGradientMode;
	}
}
