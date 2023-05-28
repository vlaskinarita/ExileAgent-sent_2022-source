using System;
using System.Collections;
using System.Drawing;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class MappedImageRenderer : BaseRenderer
	{
		public static MappedImageRenderer Boolean(object trueImage, object falseImage)
		{
			return new MappedImageRenderer(true, trueImage, false, falseImage);
		}

		public static MappedImageRenderer TriState(object trueImage, object falseImage, object nullImage)
		{
			return new MappedImageRenderer(new object[]
			{
				true,
				trueImage,
				false,
				falseImage,
				null,
				nullImage
			});
		}

		public MappedImageRenderer()
		{
			this.map = new Hashtable();
		}

		public MappedImageRenderer(object key, object image) : this()
		{
			this.Add(key, image);
		}

		public MappedImageRenderer(object key1, object image1, object key2, object image2) : this()
		{
			this.Add(key1, image1);
			this.Add(key2, image2);
		}

		public MappedImageRenderer(object[] keysAndImages) : this()
		{
			if (keysAndImages.GetLength(0) % 2 != 0)
			{
				throw new ArgumentException(MappedImageRenderer.getString_0(107313464));
			}
			for (int i = 0; i < keysAndImages.GetLength(0); i += 2)
			{
				this.Add(keysAndImages[i], keysAndImages[i + 1]);
			}
		}

		public void Add(object value, object image)
		{
			if (value == null)
			{
				this.nullImage = image;
				return;
			}
			this.map[value] = image;
		}

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			r = this.ApplyCellPadding(r);
			ICollection collection = base.Aspect as ICollection;
			if (collection == null)
			{
				this.RenderOne(g, r, base.Aspect);
				return;
			}
			this.RenderCollection(g, r, collection);
		}

		protected void RenderCollection(Graphics g, Rectangle r, ICollection imageSelectors)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in imageSelectors)
			{
				Image image;
				if (obj == null)
				{
					image = this.GetImage(this.nullImage);
				}
				else if (this.map.ContainsKey(obj))
				{
					image = this.GetImage(this.map[obj]);
				}
				else
				{
					image = null;
				}
				if (image != null)
				{
					arrayList.Add(image);
				}
			}
			this.DrawImages(g, r, arrayList);
		}

		protected void RenderOne(Graphics g, Rectangle r, object selector)
		{
			Image image = null;
			if (selector == null)
			{
				image = this.GetImage(this.nullImage);
			}
			else if (this.map.ContainsKey(selector))
			{
				image = this.GetImage(this.map[selector]);
			}
			if (image != null)
			{
				this.DrawAlignedImage(g, r, image);
			}
		}

		static MappedImageRenderer()
		{
			Strings.CreateGetStringDelegate(typeof(MappedImageRenderer));
		}

		private Hashtable map;

		private object nullImage;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
