using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using ns4;

namespace ns1
{
	internal sealed class Class4 : Interface1
	{
		private int Padding { get; set; }

		private int[,] Labels { get; set; }

		private Dictionary<int, List<int>> Linked { get; set; }

		public Class4(int int_2)
		{
			this.Padding = int_2;
			this.Linked = new Dictionary<int, List<int>>();
		}

		public unsafe int[,] imethod_0(bool[,] bool_0)
		{
			void* ptr = stackalloc byte[42];
			*(int*)ptr = bool_0.GetLength(0);
			*(int*)((byte*)ptr + 4) = bool_0.GetLength(1);
			this.Labels = new int[*(int*)ptr, *(int*)((byte*)ptr + 4)];
			*(int*)((byte*)ptr + 8) = 1;
			*(int*)((byte*)ptr + 12) = 0;
			for (;;)
			{
				((byte*)ptr)[38] = ((*(int*)((byte*)ptr + 12) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 38) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 16) = 0;
				for (;;)
				{
					((byte*)ptr)[37] = ((*(int*)((byte*)ptr + 16) < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 37) == 0)
					{
						break;
					}
					((byte*)ptr)[36] = ((!bool_0[*(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 12)]) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 36) == 0)
					{
						List<Point> list = this.method_2(bool_0, *(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 12));
						if (list == null || list.Count == 0)
						{
							this.Linked.Add(*(int*)((byte*)ptr + 8), new List<int>
							{
								*(int*)((byte*)ptr + 8)
							});
							this.Labels[*(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 12)] = *(int*)((byte*)ptr + 8);
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
						}
						else
						{
							List<int> list2 = this.method_1(list);
							this.Labels[*(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 12)] = list2.Min();
							foreach (int num in list2)
							{
								*(int*)((byte*)ptr + 20) = num;
								this.Linked[*(int*)((byte*)ptr + 20)] = this.Linked[*(int*)((byte*)ptr + 20)].Union(list2).ToList<int>();
							}
						}
					}
					*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
				}
				*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
			}
			*(int*)((byte*)ptr + 24) = 0;
			for (;;)
			{
				((byte*)ptr)[41] = ((*(int*)((byte*)ptr + 24) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 41) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 28) = 0;
				for (;;)
				{
					((byte*)ptr)[40] = ((*(int*)((byte*)ptr + 28) < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 40) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 32) = this.Labels[*(int*)((byte*)ptr + 28), *(int*)((byte*)ptr + 24)];
					((byte*)ptr)[39] = ((*(int*)((byte*)ptr + 32) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 39) == 0)
					{
						this.Labels[*(int*)((byte*)ptr + 28), *(int*)((byte*)ptr + 24)] = this.method_0(*(int*)((byte*)ptr + 32));
					}
					*(int*)((byte*)ptr + 28) = *(int*)((byte*)ptr + 28) + 1;
				}
				*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + 1;
			}
			return this.Labels;
		}

		private int method_0(int int_2)
		{
			List<int> source = this.Linked[int_2];
			return source.Min();
		}

		private List<int> method_1(IEnumerable<Point> ienumerable_0)
		{
			return ienumerable_0.Select(new Func<Point, int>(this.method_4)).ToList<int>();
		}

		private List<Point> method_2(bool[,] bool_0, int int_2, int int_3)
		{
			Class4.Class5 @class = new Class4.Class5();
			@class.bool_0 = bool_0;
			IEnumerable<Point> source = this.method_3(@class.bool_0.GetLength(0), int_2, int_3);
			return source.Where(new Func<Point, bool>(@class.method_0)).ToList<Point>();
		}

		private unsafe IEnumerable<Point> method_3(int int_2, int int_3, int int_4)
		{
			void* ptr = stackalloc byte[13];
			List<Point> list = new List<Point>();
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[12] = ((*(int*)ptr <= this.Padding) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 4) = *(int*)ptr + 1;
				((byte*)ptr)[8] = ((int_3 > *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					list.Add(new Point(int_3 - *(int*)((byte*)ptr + 4), int_4));
					((byte*)ptr)[9] = ((int_4 > *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						list.Add(new Point(int_3 - *(int*)((byte*)ptr + 4), int_4 - *(int*)((byte*)ptr + 4)));
					}
				}
				((byte*)ptr)[10] = ((int_4 > *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					list.Add(new Point(int_3, int_4 - *(int*)((byte*)ptr + 4)));
					((byte*)ptr)[11] = ((int_3 < int_2 - *(int*)((byte*)ptr + 4)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						list.Add(new Point(int_3 + *(int*)((byte*)ptr + 4), int_4 - *(int*)((byte*)ptr + 4)));
					}
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return list;
		}

		[CompilerGenerated]
		private int method_4(Point point_0)
		{
			return this.Labels[point_0.X, point_0.Y];
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int[,] int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<int, List<int>> dictionary_0;

		[CompilerGenerated]
		private sealed class Class5
		{
			internal bool method_0(Point point_0)
			{
				return this.bool_0[point_0.X, point_0.Y];
			}

			public bool[,] bool_0;
		}
	}
}
