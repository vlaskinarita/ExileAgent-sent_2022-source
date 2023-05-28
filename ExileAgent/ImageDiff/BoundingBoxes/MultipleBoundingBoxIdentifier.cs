using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ns1;

namespace ImageDiff.BoundingBoxes
{
	internal sealed class MultipleBoundingBoxIdentifier : Interface2
	{
		private int Padding { get; set; }

		public MultipleBoundingBoxIdentifier(int padding)
		{
			this.Padding = padding;
		}

		public IEnumerable<Rectangle> imethod_0(int[,] int_1)
		{
			Dictionary<int, List<Point>> dictionary_ = MultipleBoundingBoxIdentifier.smethod_0(int_1);
			return this.method_0(dictionary_);
		}

		private IEnumerable<Rectangle> method_0(Dictionary<int, List<Point>> dictionary_0)
		{
			MultipleBoundingBoxIdentifier.Class8 @class = new MultipleBoundingBoxIdentifier.Class8(-2);
			@class.multipleBoundingBoxIdentifier_0 = this;
			@class.dictionary_1 = dictionary_0;
			return @class;
		}

		private unsafe static Dictionary<int, List<Point>> smethod_0(int[,] int_1)
		{
			void* ptr = stackalloc byte[24];
			*(int*)ptr = int_1.GetLength(0);
			*(int*)((byte*)ptr + 4) = int_1.GetLength(1);
			Dictionary<int, List<Point>> dictionary = new Dictionary<int, List<Point>>();
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[23] = ((*(int*)((byte*)ptr + 8) < *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 23) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[22] = ((*(int*)((byte*)ptr + 12) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 22) == 0)
					{
						break;
					}
					((byte*)ptr)[20] = ((int_1[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) == 0)
					{
						*(int*)((byte*)ptr + 16) = int_1[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)];
						((byte*)ptr)[21] = ((!dictionary.ContainsKey(*(int*)((byte*)ptr + 16))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 21) != 0)
						{
							dictionary.Add(*(int*)((byte*)ptr + 16), new List<Point>
							{
								new Point(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12))
							});
						}
						else
						{
							dictionary[*(int*)((byte*)ptr + 16)].Add(new Point(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
						}
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return dictionary;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;
	}
}
