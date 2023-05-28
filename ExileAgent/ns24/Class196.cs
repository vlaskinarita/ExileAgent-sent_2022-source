using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PoEv2.Models;

namespace ns24
{
	internal sealed class Class196
	{
		public static Position smethod_0(Bitmap bitmap_0, Bitmap bitmap_1, double double_0)
		{
			Position position = new Position();
			Position result;
			if (bitmap_0 == null || bitmap_1 == null)
			{
				result = position;
			}
			else
			{
				Image<Bgr, byte> image = new Image<Bgr, byte>(bitmap_0);
				Image<Bgr, byte> image2 = new Image<Bgr, byte>(bitmap_1);
				try
				{
					using (Image<Gray, float> image3 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
					{
						double[] array;
						double[] array2;
						Point[] array3;
						Point[] array4;
						image3.MinMax(out array, out array2, out array3, out array4);
						if (array2[0] > double_0)
						{
							Rectangle rect = new Rectangle(array4[0], image2.Size);
							image3.Draw(rect, default(Gray), 3, LineType.EightConnected, 0);
							position = new Position
							{
								Left = array4[0].X,
								Top = array4[0].Y,
								Width = image2.Size.Width,
								Height = image2.Size.Height
							};
						}
						image.Dispose();
						image2.Dispose();
						image3.Dispose();
					}
				}
				catch
				{
				}
				result = position;
			}
			return result;
		}

		public unsafe static List<Position> smethod_1(Bitmap bitmap_0, Bitmap bitmap_1, double double_0 = 0.95)
		{
			void* ptr = stackalloc byte[2];
			List<Position> list = new List<Position>();
			Image<Bgr, byte> image = new Image<Bgr, byte>(bitmap_0);
			Image<Bgr, byte> image2 = new Image<Bgr, byte>(bitmap_1);
			List<Position> result;
			if (bitmap_0 == null || bitmap_1 == null)
			{
				result = list;
			}
			else
			{
				try
				{
					using (Image<Gray, float> image3 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
					{
						for (;;)
						{
							((byte*)ptr)[1] = 1;
							double[] array;
							double[] array2;
							Point[] array3;
							Point[] array4;
							image3.MinMax(out array, out array2, out array3, out array4);
							*(byte*)ptr = ((array2[0] > double_0) ? 1 : 0);
							if (*(sbyte*)ptr == 0)
							{
								break;
							}
							Rectangle rect = new Rectangle(array4[0], image2.Size);
							image3.Draw(rect, default(Gray), 3, LineType.EightConnected, 0);
							list.Add(new Position
							{
								Left = array4[0].X,
								Top = array4[0].Y,
								Width = image2.Size.Width,
								Height = image2.Size.Height
							});
						}
						image.Dispose();
						image2.Dispose();
						image3.Dispose();
					}
				}
				catch
				{
				}
				result = list;
			}
			return result;
		}
	}
}
