using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class BarRenderer : BaseRenderer
	{
		public BarRenderer()
		{
		}

		public BarRenderer(int minimum, int maximum) : this()
		{
			this.MinimumValue = (double)minimum;
			this.MaximumValue = (double)maximum;
		}

		public BarRenderer(Pen pen, Brush brush) : this()
		{
			this.Pen = pen;
			this.Brush = brush;
			this.UseStandardBar = false;
		}

		public BarRenderer(int minimum, int maximum, Pen pen, Brush brush) : this(minimum, maximum)
		{
			this.Pen = pen;
			this.Brush = brush;
			this.UseStandardBar = false;
		}

		public BarRenderer(Pen pen, Color start, Color end) : this()
		{
			this.Pen = pen;
			this.SetGradient(start, end);
		}

		public BarRenderer(int minimum, int maximum, Pen pen, Color start, Color end) : this(minimum, maximum)
		{
			this.Pen = pen;
			this.SetGradient(start, end);
		}

		[Category("ObjectListView")]
		[Description("Should this bar be drawn in the system style?")]
		[DefaultValue(true)]
		public bool UseStandardBar
		{
			get
			{
				return this.useStandardBar;
			}
			set
			{
				this.useStandardBar = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(2)]
		[Description("How many pixels in from our cell border will this bar be drawn")]
		public int Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		[DefaultValue(typeof(Color), "AliceBlue")]
		[Description("The color of the interior of the bar")]
		[Category("ObjectListView")]
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
			}
		}

		[Category("ObjectListView")]
		[Description("What color should the frame of the progress bar be")]
		[DefaultValue(typeof(Color), "Black")]
		public Color FrameColor
		{
			get
			{
				return this.frameColor;
			}
			set
			{
				this.frameColor = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(1f)]
		[Description("How many pixels wide should the frame of the progress bar be")]
		public float FrameWidth
		{
			get
			{
				return this.frameWidth;
			}
			set
			{
				this.frameWidth = value;
			}
		}

		[Category("ObjectListView")]
		[Description("What color should the 'filled in' part of the progress bar be")]
		[DefaultValue(typeof(Color), "BlueViolet")]
		public Color FillColor
		{
			get
			{
				return this.fillColor;
			}
			set
			{
				this.fillColor = value;
			}
		}

		[DefaultValue(typeof(Color), "CornflowerBlue")]
		[Category("ObjectListView")]
		[Description("Use a gradient to fill the progress bar starting with this color")]
		public Color GradientStartColor
		{
			get
			{
				return this.startColor;
			}
			set
			{
				this.startColor = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Use a gradient to fill the progress bar ending with this color")]
		[DefaultValue(typeof(Color), "DarkBlue")]
		public Color GradientEndColor
		{
			get
			{
				return this.endColor;
			}
			set
			{
				this.endColor = value;
			}
		}

		[Category("Behavior")]
		[Description("The progress bar will never be wider than this")]
		[DefaultValue(100)]
		public int MaximumWidth
		{
			get
			{
				return this.maximumWidth;
			}
			set
			{
				this.maximumWidth = value;
			}
		}

		[DefaultValue(16)]
		[Description("The progress bar will never be taller than this")]
		[Category("Behavior")]
		public int MaximumHeight
		{
			get
			{
				return this.maximumHeight;
			}
			set
			{
				this.maximumHeight = value;
			}
		}

		[DefaultValue(0.0)]
		[Category("Behavior")]
		[Description("The minimum data value expected. Values less than this will given an empty bar")]
		public double MinimumValue
		{
			get
			{
				return this.minimumValue;
			}
			set
			{
				this.minimumValue = value;
			}
		}

		[Category("Behavior")]
		[Description("The maximum value for the range. Values greater than this will give a full bar")]
		[DefaultValue(100.0)]
		public double MaximumValue
		{
			get
			{
				return this.maximumValue;
			}
			set
			{
				this.maximumValue = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Pen Pen
		{
			get
			{
				if (this.pen == null && !this.FrameColor.IsEmpty)
				{
					return new Pen(this.FrameColor, this.FrameWidth);
				}
				return this.pen;
			}
			set
			{
				this.pen = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Brush Brush
		{
			get
			{
				if (this.brush == null && !this.FillColor.IsEmpty)
				{
					return new SolidBrush(this.FillColor);
				}
				return this.brush;
			}
			set
			{
				this.brush = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Brush BackgroundBrush
		{
			get
			{
				if (this.backgroundBrush == null && !this.BackgroundColor.IsEmpty)
				{
					return new SolidBrush(this.BackgroundColor);
				}
				return this.backgroundBrush;
			}
			set
			{
				this.backgroundBrush = value;
			}
		}

		public void SetGradient(Color start, Color end)
		{
			this.GradientStartColor = start;
			this.GradientEndColor = end;
		}

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			r = this.ApplyCellPadding(r);
			Rectangle rectangle = Rectangle.Inflate(r, -this.Padding, -this.Padding);
			rectangle.Width = Math.Min(rectangle.Width, this.MaximumWidth);
			rectangle.Height = Math.Min(rectangle.Height, this.MaximumHeight);
			rectangle = this.AlignRectangle(r, rectangle);
			IConvertible convertible = base.Aspect as IConvertible;
			if (convertible == null)
			{
				return;
			}
			double num = convertible.ToDouble(NumberFormatInfo.InvariantInfo);
			Rectangle rectangle2 = Rectangle.Inflate(rectangle, -1, -1);
			if (num <= this.MinimumValue)
			{
				rectangle2.Width = 0;
			}
			else if (num < this.MaximumValue)
			{
				rectangle2.Width = (int)((double)rectangle2.Width * (num - this.MinimumValue) / this.MaximumValue);
			}
			if (this.UseStandardBar && ProgressBarRenderer.IsSupported && !base.IsPrinting)
			{
				ProgressBarRenderer.DrawHorizontalBar(g, rectangle);
				ProgressBarRenderer.DrawHorizontalChunks(g, rectangle2);
				return;
			}
			g.FillRectangle(this.BackgroundBrush, rectangle);
			if (rectangle2.Width > 0)
			{
				rectangle2.Width++;
				rectangle2.Height++;
				if (this.GradientStartColor == Color.Empty)
				{
					g.FillRectangle(this.Brush, rectangle2);
				}
				else
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.GradientStartColor, this.GradientEndColor, LinearGradientMode.Horizontal))
					{
						g.FillRectangle(linearGradientBrush, rectangle2);
					}
				}
			}
			g.DrawRectangle(this.Pen, rectangle);
		}

		protected override Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			return base.CalculatePaddedAlignedBounds(g, cellBounds, preferredSize);
		}

		private bool useStandardBar = true;

		private int padding = 2;

		private Color backgroundColor = Color.AliceBlue;

		private Color frameColor = Color.Black;

		private float frameWidth = 1f;

		private Color fillColor = Color.BlueViolet;

		private Color startColor = Color.CornflowerBlue;

		private Color endColor = Color.DarkBlue;

		private int maximumWidth = 100;

		private int maximumHeight = 16;

		private double minimumValue;

		private double maximumValue = 100.0;

		private Pen pen;

		private Brush brush;

		private Brush backgroundBrush;
	}
}
