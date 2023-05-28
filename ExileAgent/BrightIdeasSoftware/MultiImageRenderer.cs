using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace BrightIdeasSoftware
{
	public sealed class MultiImageRenderer : BaseRenderer
	{
		public MultiImageRenderer()
		{
		}

		public MultiImageRenderer(object imageSelector, int maxImages, int minValue, int maxValue) : this()
		{
			this.ImageSelector = imageSelector;
			this.MaxNumberImages = maxImages;
			this.MinimumValue = minValue;
			this.MaximumValue = maxValue;
		}

		[Category("Behavior")]
		[Description("The index of the image that should be drawn")]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				if (this.imageSelector is int)
				{
					return (int)this.imageSelector;
				}
				return -1;
			}
			set
			{
				this.imageSelector = value;
			}
		}

		[Description("The index of the image that should be drawn")]
		[Category("Behavior")]
		[DefaultValue(null)]
		public string ImageName
		{
			get
			{
				return this.imageSelector as string;
			}
			set
			{
				this.imageSelector = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object ImageSelector
		{
			get
			{
				return this.imageSelector;
			}
			set
			{
				this.imageSelector = value;
			}
		}

		[Description("The maximum number of images that this renderer should draw")]
		[Category("Behavior")]
		[DefaultValue(10)]
		public int MaxNumberImages
		{
			get
			{
				return this.maxNumberImages;
			}
			set
			{
				this.maxNumberImages = value;
			}
		}

		[Description("Values less than or equal to this will have 0 images drawn")]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int MinimumValue
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

		[DefaultValue(100)]
		[Category("Behavior")]
		[Description("Values greater than or equal to this will have MaxNumberImages images drawn")]
		public int MaximumValue
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

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			r = this.ApplyCellPadding(r);
			Image image = this.GetImage(this.ImageSelector);
			if (image == null)
			{
				return;
			}
			IConvertible convertible = base.Aspect as IConvertible;
			if (convertible == null)
			{
				return;
			}
			double num = convertible.ToDouble(NumberFormatInfo.InvariantInfo);
			int num2;
			if (num <= (double)this.MinimumValue)
			{
				num2 = 0;
			}
			else if (num < (double)this.MaximumValue)
			{
				num2 = 1 + (int)((double)this.MaxNumberImages * (num - (double)this.MinimumValue) / (double)this.MaximumValue);
			}
			else
			{
				num2 = this.MaxNumberImages;
			}
			int num3 = image.Width;
			int height = image.Height;
			if (r.Height < image.Height)
			{
				num3 = (int)((float)image.Width * (float)r.Height / (float)image.Height);
				height = r.Height;
			}
			Rectangle inner = r;
			inner.Width = this.MaxNumberImages * (num3 + base.Spacing) - base.Spacing;
			inner.Height = height;
			inner = this.AlignRectangle(r, inner);
			for (int i = 0; i < num2; i++)
			{
				g.DrawImage(image, inner.X, inner.Y, num3, height);
				inner.X += num3 + base.Spacing;
			}
		}

		protected override Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			return base.CalculatePaddedAlignedBounds(g, cellBounds, preferredSize);
		}

		private object imageSelector;

		private int maxNumberImages = 10;

		private int minimumValue;

		private int maximumValue = 100;
	}
}
