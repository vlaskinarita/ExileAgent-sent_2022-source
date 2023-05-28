using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using ImageDiff;
using ns2;
using ns4;
using ns5;
using ns6;
using ns7;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns1
{
	internal sealed class Class1
	{
		private LabelerTypes LabelerType { get; set; }

		private double JustNoticeableDifference { get; set; }

		private int DetectionPadding { get; set; }

		private int BoundingBoxPadding { get; set; }

		private Color BoundingBoxColor { get; set; }

		private BoundingBoxModes BoundingBoxMode { get; set; }

		private AnalyzerTypes AnalyzerType { get; set; }

		private Interface1 Labeler { get; set; }

		private Interface2 BoundingBoxIdentifier { get; set; }

		private Interface3 BitmapAnalyzer { get; set; }

		public Class1(Class2 class2_0 = null)
		{
			if (class2_0 == null)
			{
				class2_0 = new Class2();
			}
			this.method_0(class2_0);
			this.BitmapAnalyzer = Class9.smethod_0(this.AnalyzerType, this.JustNoticeableDifference);
			this.Labeler = Class6.smethod_0(this.LabelerType, this.DetectionPadding);
			this.BoundingBoxIdentifier = Class7.smethod_0(this.BoundingBoxMode, this.BoundingBoxPadding);
		}

		private unsafe void method_0(Class2 class2_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((class2_0.BoundingBoxPadding < 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				throw new ArgumentException(Class1.getString_0(107396690));
			}
			((byte*)ptr)[1] = ((class2_0.DetectionPadding < 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				throw new ArgumentException(Class1.getString_0(107396633));
			}
			this.LabelerType = class2_0.Labeler;
			this.JustNoticeableDifference = class2_0.JustNoticeableDifference;
			this.BoundingBoxColor = class2_0.BoundingBoxColor;
			this.DetectionPadding = class2_0.DetectionPadding;
			this.BoundingBoxPadding = class2_0.BoundingBoxPadding;
			this.BoundingBoxMode = class2_0.BoundingBoxMode;
			this.AnalyzerType = class2_0.AnalyzerType;
		}

		public unsafe Tuple<Bitmap, Rectangle[]> method_1(Bitmap bitmap_0, Bitmap bitmap_1)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((bitmap_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				throw new ArgumentNullException(Class1.getString_0(107397124));
			}
			((byte*)ptr)[1] = ((bitmap_1 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				throw new ArgumentNullException(Class1.getString_0(107397075));
			}
			if (bitmap_0.Width != bitmap_1.Width || bitmap_0.Height != bitmap_1.Height)
			{
				throw new ArgumentException(string.Format(Class1.getString_0(107397090), new object[]
				{
					bitmap_0.Width,
					bitmap_0.Height,
					bitmap_1.Width,
					bitmap_1.Height
				}));
			}
			bool[,] bool_ = this.BitmapAnalyzer.imethod_0(bitmap_0, bitmap_1);
			int[,] array = this.Labeler.imethod_0(bool_);
			IEnumerable<Rectangle> ienumerable_ = this.BoundingBoxIdentifier.imethod_0(array);
			return this.method_3(bitmap_1, ienumerable_);
		}

		public unsafe bool method_2(Bitmap bitmap_0, Bitmap bitmap_1)
		{
			void* ptr = stackalloc byte[14];
			if (bitmap_0 == null && bitmap_1 == null)
			{
				((byte*)ptr)[8] = 1;
			}
			else
			{
				((byte*)ptr)[9] = ((bitmap_0 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					((byte*)ptr)[8] = 0;
				}
				else
				{
					((byte*)ptr)[10] = ((bitmap_1 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						((byte*)ptr)[8] = 0;
					}
					else if (bitmap_0.Width != bitmap_1.Width || bitmap_0.Height != bitmap_1.Height)
					{
						((byte*)ptr)[8] = 0;
					}
					else
					{
						bool[,] array = this.BitmapAnalyzer.imethod_0(bitmap_0, bitmap_1);
						*(int*)ptr = 0;
						for (;;)
						{
							((byte*)ptr)[13] = ((*(int*)ptr < array.GetLength(0)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 13) == 0)
							{
								break;
							}
							*(int*)((byte*)ptr + 4) = 0;
							for (;;)
							{
								((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 4) < array.GetLength(1)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) == 0)
								{
									break;
								}
								((byte*)ptr)[11] = (array[*(int*)ptr, *(int*)((byte*)ptr + 4)] ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 11) != 0)
								{
									goto IL_F4;
								}
								*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
							}
							*(int*)ptr = *(int*)ptr + 1;
						}
						((byte*)ptr)[8] = 1;
						goto IL_F9;
						IL_F4:
						((byte*)ptr)[8] = 0;
					}
				}
			}
			IL_F9:
			return *(sbyte*)((byte*)ptr + 8) != 0;
		}

		private unsafe Tuple<Bitmap, Rectangle[]> method_3(Bitmap bitmap_0, IEnumerable<Rectangle> ienumerable_0)
		{
			void* ptr = stackalloc byte[6];
			Rectangle[] item = new List<Rectangle>().ToArray();
			Bitmap bitmap = bitmap_0.Clone() as Bitmap;
			((byte*)ptr)[4] = ((bitmap == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				throw new Exception(Class1.getString_0(107396985));
			}
			Rectangle[] array = ienumerable_0.ToArray<Rectangle>();
			((byte*)ptr)[5] = ((array.Length == 0) ? 1 : 0);
			Tuple<Bitmap, Rectangle[]> result;
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				result = Tuple.Create<Bitmap, Rectangle[]>(bitmap, item);
			}
			else
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					Pen pen = new Pen(this.BoundingBoxColor);
					Rectangle[] array2 = array;
					*(int*)ptr = 0;
					while (*(int*)ptr < array2.Length)
					{
						Rectangle rect = array2[*(int*)ptr];
						graphics.DrawRectangle(pen, rect);
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				result = Tuple.Create<Bitmap, Rectangle[]>(bitmap, array);
			}
			return result;
		}

		static Class1()
		{
			Strings.CreateGetStringDelegate(typeof(Class1));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LabelerTypes labelerTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color color_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BoundingBoxModes boundingBoxModes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AnalyzerTypes analyzerTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Interface1 interface1_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Interface2 interface2_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Interface3 interface3_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
