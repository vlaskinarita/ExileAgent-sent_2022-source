using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace BrightIdeasSoftware
{
	public class ImageAdornment : GraphicAdornment
	{
		[Description("The image that will be drawn")]
		[Category("ObjectListView")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Will the image be shrunk to fit within its width?")]
		public bool ShrinkToWidth
		{
			get
			{
				return this.shrinkToWidth;
			}
			set
			{
				this.shrinkToWidth = value;
			}
		}

		public virtual void DrawImage(Graphics g, Rectangle r)
		{
			if (this.ShrinkToWidth)
			{
				this.DrawScaledImage(g, r, this.Image, base.Transparency);
				return;
			}
			this.DrawImage(g, r, this.Image, base.Transparency);
		}

		public virtual void DrawImage(Graphics g, Rectangle r, Image image, int transparency)
		{
			if (image != null)
			{
				this.DrawImage(g, r, image, image.Size, transparency);
			}
		}

		public virtual void DrawImage(Graphics g, Rectangle r, Image image, Size sz, int transparency)
		{
			if (image == null)
			{
				return;
			}
			Rectangle r2 = this.CreateAlignedRectangle(r, sz);
			try
			{
				this.ApplyRotation(g, r2);
				this.DrawTransparentBitmap(g, r2, image, transparency);
			}
			finally
			{
				this.UnapplyRotation(g);
			}
		}

		public virtual void DrawScaledImage(Graphics g, Rectangle r, Image image, int transparency)
		{
			if (image == null)
			{
				return;
			}
			Size size = image.Size;
			if (image.Width > r.Width)
			{
				float num = (float)r.Width / (float)image.Width;
				size.Height = (int)((float)image.Height * num);
				size.Width = r.Width - 1;
			}
			this.DrawImage(g, r, image, size, transparency);
		}

		protected virtual void DrawTransparentBitmap(Graphics g, Rectangle r, Image image, int transparency)
		{
			ImageAttributes imageAttributes = null;
			if (transparency != 255)
			{
				imageAttributes = new ImageAttributes();
				float num = (float)transparency / 255f;
				float[][] array = new float[5][];
				float[][] array2 = array;
				int num2 = 0;
				float[] array3 = new float[5];
				array3[0] = 1f;
				array2[num2] = array3;
				float[][] array4 = array;
				int num3 = 1;
				float[] array5 = new float[5];
				array5[1] = 1f;
				array4[num3] = array5;
				float[][] array6 = array;
				int num4 = 2;
				float[] array7 = new float[5];
				array7[2] = 1f;
				array6[num4] = array7;
				float[][] array8 = array;
				int num5 = 3;
				float[] array9 = new float[5];
				array9[3] = num;
				array8[num5] = array9;
				array[4] = new float[]
				{
					0f,
					0f,
					0f,
					0f,
					1f
				};
				float[][] newColorMatrix = array;
				imageAttributes.SetColorMatrix(new ColorMatrix(newColorMatrix));
			}
			g.DrawImage(image, r, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttributes);
		}

		private Image image;

		private bool shrinkToWidth;
	}
}
