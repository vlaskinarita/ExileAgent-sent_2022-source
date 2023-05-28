using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class DescribedTaskRenderer : BaseRenderer
	{
		[Category("ObjectListView")]
		[DefaultValue(null)]
		[Description("The font that will be used to draw the title of the task")]
		public Font TitleFont
		{
			get
			{
				return this.titleFont;
			}
			set
			{
				this.titleFont = value;
			}
		}

		[Browsable(false)]
		public Font TitleFontOrDefault
		{
			get
			{
				return this.TitleFont ?? base.ListView.Font;
			}
		}

		[Category("ObjectListView")]
		[Description("The color of the title")]
		[DefaultValue(typeof(Color), "")]
		public Color TitleColor
		{
			get
			{
				return this.titleColor;
			}
			set
			{
				this.titleColor = value;
			}
		}

		[Browsable(false)]
		public Color TitleColorOrDefault
		{
			get
			{
				if (!base.IsItemSelected && !this.TitleColor.IsEmpty)
				{
					return this.TitleColor;
				}
				return this.GetForegroundColor();
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The font that will be used to draw the description of the task")]
		public Font DescriptionFont
		{
			get
			{
				return this.descriptionFont;
			}
			set
			{
				this.descriptionFont = value;
			}
		}

		[Browsable(false)]
		public Font DescriptionFontOrDefault
		{
			get
			{
				return this.DescriptionFont ?? base.ListView.Font;
			}
		}

		[Category("ObjectListView")]
		[Description("The color of the description")]
		[DefaultValue(typeof(Color), "DimGray")]
		public Color DescriptionColor
		{
			get
			{
				return this.descriptionColor;
			}
			set
			{
				this.descriptionColor = value;
			}
		}

		[Browsable(false)]
		public Color DescriptionColorOrDefault
		{
			get
			{
				if (!this.DescriptionColor.IsEmpty && (!base.IsItemSelected || base.ListView.UseTranslucentSelection))
				{
					return this.DescriptionColor;
				}
				return this.GetForegroundColor();
			}
		}

		[Description("The number of pixels that that will be left between the image and the text")]
		[Category("ObjectListView")]
		[DefaultValue(4)]
		public int ImageTextSpace
		{
			get
			{
				return this.imageTextSpace;
			}
			set
			{
				this.imageTextSpace = value;
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The name of the aspect of the model object that contains the task description")]
		public string DescriptionAspectName
		{
			get
			{
				return this.descriptionAspectName;
			}
			set
			{
				this.descriptionAspectName = value;
			}
		}

		protected string GetDescription()
		{
			if (string.IsNullOrEmpty(this.DescriptionAspectName))
			{
				return string.Empty;
			}
			if (this.descriptionGetter == null)
			{
				this.descriptionGetter = new Munger(this.DescriptionAspectName);
			}
			return this.descriptionGetter.GetValue(base.RowObject) as string;
		}

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			r = this.ApplyCellPadding(r);
			this.DrawDescribedTask(g, r, base.Aspect as string, this.GetDescription(), this.GetImage());
		}

		protected void DrawDescribedTask(Graphics g, Rectangle r, string title, string description, Image image)
		{
			Rectangle rectangle = this.ApplyCellPadding(r);
			Rectangle rectangle2 = rectangle;
			if (image != null)
			{
				g.DrawImage(image, rectangle.Location);
				int num = image.Width + this.ImageTextSpace;
				rectangle2.X += num;
				rectangle2.Width -= num;
			}
			if (base.IsItemSelected && !base.ListView.UseTranslucentSelection)
			{
				using (SolidBrush solidBrush = new SolidBrush(this.GetTextBackgroundColor()))
				{
					g.FillRectangle(solidBrush, rectangle2);
				}
			}
			if (!string.IsNullOrEmpty(title))
			{
				using (StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap))
				{
					stringFormat.Trimming = StringTrimming.EllipsisCharacter;
					stringFormat.Alignment = StringAlignment.Near;
					stringFormat.LineAlignment = StringAlignment.Near;
					Font titleFontOrDefault = this.TitleFontOrDefault;
					using (SolidBrush solidBrush2 = new SolidBrush(this.TitleColorOrDefault))
					{
						g.DrawString(title, titleFontOrDefault, solidBrush2, rectangle2, stringFormat);
					}
					SizeF sizeF = g.MeasureString(title, titleFontOrDefault, rectangle2.Width, stringFormat);
					rectangle2.Y += (int)sizeF.Height;
					rectangle2.Height -= (int)sizeF.Height;
				}
			}
			if (!string.IsNullOrEmpty(description))
			{
				using (StringFormat stringFormat2 = new StringFormat())
				{
					stringFormat2.Trimming = StringTrimming.EllipsisCharacter;
					using (SolidBrush solidBrush3 = new SolidBrush(this.DescriptionColorOrDefault))
					{
						g.DrawString(description, this.DescriptionFontOrDefault, solidBrush3, rectangle2, stringFormat2);
					}
				}
			}
		}

		protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
		{
			if (base.Bounds.Contains(x, y))
			{
				hti.HitTestLocation = HitTestLocation.Text;
			}
		}

		private Font titleFont;

		private Color titleColor;

		private Font descriptionFont;

		private Color descriptionColor = Color.DimGray;

		private int imageTextSpace = 4;

		private string descriptionAspectName;

		private Munger descriptionGetter;
	}
}
