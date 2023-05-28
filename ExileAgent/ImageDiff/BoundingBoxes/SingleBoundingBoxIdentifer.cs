using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using ns1;

namespace ImageDiff.BoundingBoxes
{
	internal sealed class SingleBoundingBoxIdentifer : Interface2
	{
		private int Padding { get; set; }

		public SingleBoundingBoxIdentifer(int padding)
		{
			this.Padding = padding;
		}

		public IEnumerable<Rectangle> imethod_0(int[,] int_1)
		{
			List<Point> source = SingleBoundingBoxIdentifer.smethod_0(int_1);
			IEnumerable<Rectangle> result;
			if (!source.Any<Point>())
			{
				result = new List<Rectangle>();
			}
			else
			{
				Point point = new Point(source.Min(new Func<Point, int>(SingleBoundingBoxIdentifer.<>c.<>9.method_0)), source.Min(new Func<Point, int>(SingleBoundingBoxIdentifer.<>c.<>9.method_1)));
				Point point2 = new Point(source.Max(new Func<Point, int>(SingleBoundingBoxIdentifer.<>c.<>9.method_2)), source.Max(new Func<Point, int>(SingleBoundingBoxIdentifer.<>c.<>9.method_3)));
				Rectangle item = new Rectangle(point.X - this.Padding, point.Y - this.Padding, point2.X - point.X + this.Padding * 2, point2.Y - point.Y + this.Padding * 2);
				result = new List<Rectangle>
				{
					item
				};
			}
			return result;
		}

		private unsafe static List<Point> smethod_0(int[,] int_1)
		{
			void* ptr = stackalloc byte[19];
			*(int*)ptr = int_1.GetLength(0);
			*(int*)((byte*)ptr + 4) = int_1.GetLength(1);
			List<Point> list = new List<Point>();
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 8) < *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 12) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) == 0)
					{
						break;
					}
					((byte*)ptr)[16] = ((int_1[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						list.Add(new Point(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return list;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;
	}
}
