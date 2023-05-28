using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class HighlightTextRenderer : BaseRenderer
	{
		public HighlightTextRenderer()
		{
			this.FramePen = Pens.DarkGreen;
			this.FillBrush = Brushes.Yellow;
		}

		public HighlightTextRenderer(TextMatchFilter filter) : this()
		{
			this.Filter = filter;
		}

		[Obsolete("Use HighlightTextRenderer(TextMatchFilter) instead", true)]
		public HighlightTextRenderer(string text)
		{
		}

		[Category("Appearance")]
		[DefaultValue(3f)]
		[Description("How rounded will be the corners of the text match frame?")]
		public float CornerRoundness
		{
			get
			{
				return this.cornerRoundness;
			}
			set
			{
				this.cornerRoundness = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TextMatchFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Pen FramePen
		{
			get
			{
				return this.framePen;
			}
			set
			{
				this.framePen = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Will the frame around a text match will have rounded corners?")]
		public bool UseRoundedRectangle
		{
			get
			{
				return this.useRoundedRectangle;
			}
			set
			{
				this.useRoundedRectangle = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Set the Filter directly rather than just the text", true)]
		public string TextToHighlight
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		[Obsolete("Set the Filter directly rather than just this setting", true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public StringComparison StringComparison
		{
			get
			{
				return StringComparison.CurrentCultureIgnoreCase;
			}
			set
			{
			}
		}

		protected override Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			return base.StandardGetEditRectangle(g, cellBounds, preferredSize);
		}

		protected override void DrawTextGdi(Graphics g, Rectangle r, string txt)
		{
			if (this.ShouldDrawHighlighting)
			{
				this.DrawGdiTextHighlighting(g, r, txt);
			}
			base.DrawTextGdi(g, r, txt);
		}

		protected virtual void DrawGdiTextHighlighting(Graphics g, Rectangle r, string txt)
		{
			TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsTranslateTransform;
			int num = 6;
			Font font = base.Font;
			foreach (CharacterRange characterRange in this.Filter.FindAllMatchedRanges(txt))
			{
				Size size = Size.Empty;
				if (characterRange.First > 0)
				{
					string text = txt.Substring(0, characterRange.First);
					size = TextRenderer.MeasureText(g, text, font, r.Size, flags);
					size.Width -= num;
				}
				string text2 = txt.Substring(characterRange.First, characterRange.Length);
				Size size2 = TextRenderer.MeasureText(g, text2, font, r.Size, flags);
				size2.Width -= num;
				float x = (float)(r.X + size.Width + 1);
				float y = (float)base.AlignVertically(r, size2.Height);
				this.DrawSubstringFrame(g, x, y, (float)size2.Width, (float)size2.Height);
			}
		}

		protected virtual void DrawSubstringFrame(Graphics g, float x, float y, float width, float height)
		{
			if (this.UseRoundedRectangle)
			{
				using (GraphicsPath roundedRect = this.GetRoundedRect(x, y, width, height, 3f))
				{
					if (this.FillBrush != null)
					{
						g.FillPath(this.FillBrush, roundedRect);
					}
					if (this.FramePen != null)
					{
						g.DrawPath(this.FramePen, roundedRect);
					}
					return;
				}
			}
			if (this.FillBrush != null)
			{
				g.FillRectangle(this.FillBrush, x, y, width, height);
			}
			if (this.FramePen != null)
			{
				g.DrawRectangle(this.FramePen, x, y, width, height);
			}
		}

		protected override void DrawTextGdiPlus(Graphics g, Rectangle r, string txt)
		{
			if (this.ShouldDrawHighlighting)
			{
				this.DrawGdiPlusTextHighlighting(g, r, txt);
			}
			base.DrawTextGdiPlus(g, r, txt);
		}

		protected virtual void DrawGdiPlusTextHighlighting(Graphics g, Rectangle r, string txt)
		{
			List<CharacterRange> list = new List<CharacterRange>(this.Filter.FindAllMatchedRanges(txt));
			if (list.Count == 0)
			{
				return;
			}
			using (StringFormat stringFormatForGdiPlus = this.StringFormatForGdiPlus)
			{
				RectangleF layoutRect = r;
				stringFormatForGdiPlus.SetMeasurableCharacterRanges(list.ToArray());
				Region[] array = g.MeasureCharacterRanges(txt, base.Font, layoutRect, stringFormatForGdiPlus);
				foreach (Region region in array)
				{
					RectangleF bounds = region.GetBounds(g);
					this.DrawSubstringFrame(g, bounds.X - 1f, bounds.Y - 1f, bounds.Width + 2f, bounds.Height);
				}
			}
		}

		protected bool ShouldDrawHighlighting
		{
			get
			{
				return base.Column == null || (base.Column.Searchable && this.Filter != null && this.Filter.HasComponents);
			}
		}

		protected GraphicsPath GetRoundedRect(float x, float y, float width, float height, float diameter)
		{
			return this.GetRoundedRect(new RectangleF(x, y, width, height), diameter);
		}

		protected GraphicsPath GetRoundedRect(RectangleF rect, float diameter)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			if (diameter > 0f)
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
			else
			{
				graphicsPath.AddRectangle(rect);
			}
			return graphicsPath;
		}

		private float cornerRoundness = 3f;

		private Brush fillBrush;

		private TextMatchFilter filter;

		private Pen framePen;

		private bool useRoundedRectangle = true;
	}
}
