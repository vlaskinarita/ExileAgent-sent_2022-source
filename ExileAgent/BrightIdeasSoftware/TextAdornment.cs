using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public class TextAdornment : GraphicAdornment
	{
		[DefaultValue(typeof(Color), "")]
		[Category("ObjectListView")]
		[Description("The background color of the text")]
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		[Browsable(false)]
		public Brush BackgroundBrush
		{
			get
			{
				return new SolidBrush(Color.FromArgb(this.workingTransparency, this.BackColor));
			}
		}

		[Category("ObjectListView")]
		[Description("The color of the border around the text")]
		[DefaultValue(typeof(Color), "")]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				this.borderColor = value;
			}
		}

		[Browsable(false)]
		public Pen BorderPen
		{
			get
			{
				return new Pen(Color.FromArgb(this.workingTransparency, this.BorderColor), this.BorderWidth);
			}
		}

		[Description("The width of the border around the text")]
		[Category("ObjectListView")]
		[DefaultValue(0f)]
		public float BorderWidth
		{
			get
			{
				return this.borderWidth;
			}
			set
			{
				this.borderWidth = value;
			}
		}

		[DefaultValue(16f)]
		[Category("ObjectListView")]
		[Description("How rounded should the corners of the border be? 0 means no rounding.")]
		[NotifyParentProperty(true)]
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

		[Category("ObjectListView")]
		[Description("The font that will be used to draw the text")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		[Browsable(false)]
		public Font FontOrDefault
		{
			get
			{
				return this.Font ?? new Font(TextAdornment.getString_0(107315988), 16f);
			}
		}

		[Browsable(false)]
		public bool HasBackground
		{
			get
			{
				return this.BackColor != Color.Empty;
			}
		}

		[Browsable(false)]
		public bool HasBorder
		{
			get
			{
				return this.BorderColor != Color.Empty && this.BorderWidth > 0f;
			}
		}

		[Category("ObjectListView")]
		[Description("The maximum width the text (0 means no maximum). Text longer than this will wrap")]
		[DefaultValue(0)]
		public int MaximumTextWidth
		{
			get
			{
				return this.maximumTextWidth;
			}
			set
			{
				this.maximumTextWidth = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual StringFormat StringFormat
		{
			get
			{
				if (this.stringFormat == null)
				{
					this.stringFormat = new StringFormat();
					this.stringFormat.Alignment = StringAlignment.Center;
					this.stringFormat.LineAlignment = StringAlignment.Center;
					this.stringFormat.Trimming = StringTrimming.EllipsisCharacter;
					if (!this.Wrap)
					{
						this.stringFormat.FormatFlags = StringFormatFlags.NoWrap;
					}
				}
				return this.stringFormat;
			}
			set
			{
				this.stringFormat = value;
			}
		}

		[Category("ObjectListView")]
		[Localizable(true)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Description("The text that will be drawn over the top of the ListView")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		[Browsable(false)]
		public Brush TextBrush
		{
			get
			{
				return new SolidBrush(Color.FromArgb(this.workingTransparency, this.TextColor));
			}
		}

		[NotifyParentProperty(true)]
		[Category("ObjectListView")]
		[Description("The color of the text")]
		[DefaultValue(typeof(Color), "DarkBlue")]
		public Color TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}

		[Description("Will the text wrap?")]
		[DefaultValue(true)]
		[Category("ObjectListView")]
		public bool Wrap
		{
			get
			{
				return this.wrap;
			}
			set
			{
				this.wrap = value;
			}
		}

		public virtual void DrawText(Graphics g, Rectangle r)
		{
			this.DrawText(g, r, this.Text, base.Transparency);
		}

		public virtual void DrawText(Graphics g, Rectangle r, string s, int transparency)
		{
			if (string.IsNullOrEmpty(s))
			{
				return;
			}
			Rectangle textRect = this.CalculateTextBounds(g, r, s);
			this.DrawBorderedText(g, textRect, s, transparency);
		}

		protected virtual void DrawBorderedText(Graphics g, Rectangle textRect, string text, int transparency)
		{
			Rectangle rect = textRect;
			rect.Inflate((int)this.BorderWidth / 2, (int)this.BorderWidth / 2);
			rect.Y--;
			try
			{
				this.ApplyRotation(g, textRect);
				using (GraphicsPath roundedRect = this.GetRoundedRect(rect, this.CornerRounding))
				{
					this.workingTransparency = transparency;
					if (this.HasBackground)
					{
						using (Brush backgroundBrush = this.BackgroundBrush)
						{
							g.FillPath(backgroundBrush, roundedRect);
						}
					}
					using (Brush textBrush = this.TextBrush)
					{
						g.DrawString(text, this.FontOrDefault, textBrush, textRect, this.StringFormat);
					}
					if (this.HasBorder)
					{
						using (Pen borderPen = this.BorderPen)
						{
							g.DrawPath(borderPen, roundedRect);
						}
					}
				}
			}
			finally
			{
				this.UnapplyRotation(g);
			}
		}

		protected virtual Rectangle CalculateTextBounds(Graphics g, Rectangle r, string s)
		{
			int width = (this.MaximumTextWidth <= 0) ? r.Width : this.MaximumTextWidth;
			SizeF sizeF = g.MeasureString(s, this.FontOrDefault, width, this.StringFormat);
			Size sz = new Size(1 + (int)sizeF.Width, 1 + (int)sizeF.Height);
			return this.CreateAlignedRectangle(r, sz);
		}

		protected virtual GraphicsPath GetRoundedRect(Rectangle rect, float diameter)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (diameter > 0f)
			{
				RectangleF rect2 = new RectangleF((float)rect.X, (float)rect.Y, diameter, diameter);
				graphicsPath.AddArc(rect2, 180f, 90f);
				rect2.X = (float)rect.Right - diameter;
				graphicsPath.AddArc(rect2, 270f, 90f);
				rect2.Y = (float)rect.Bottom - diameter;
				graphicsPath.AddArc(rect2, 0f, 90f);
				rect2.X = (float)rect.Left;
				graphicsPath.AddArc(rect2, 90f, 90f);
				graphicsPath.CloseFigure();
			}
			else
			{
				graphicsPath.AddRectangle(rect);
			}
			return graphicsPath;
		}

		static TextAdornment()
		{
			Strings.CreateGetStringDelegate(typeof(TextAdornment));
		}

		private Color backColor = Color.Empty;

		private Color borderColor = Color.Empty;

		private float borderWidth;

		private float cornerRounding = 16f;

		private Font font;

		private int maximumTextWidth;

		private StringFormat stringFormat;

		private string text;

		private Color textColor = Color.DarkBlue;

		private bool wrap = true;

		private int workingTransparency;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
