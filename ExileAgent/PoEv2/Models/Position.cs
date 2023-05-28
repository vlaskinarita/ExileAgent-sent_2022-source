using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class Position
	{
		public int Left { get; set; } = -1;

		public int Top { get; set; } = -1;

		public int Width { get; set; }

		public int Height { get; set; }

		public Position()
		{
		}

		public int X
		{
			get
			{
				return this.Left;
			}
		}

		public int Y
		{
			get
			{
				return this.Top;
			}
		}

		public int x
		{
			get
			{
				return this.Left;
			}
		}

		public int y
		{
			get
			{
				return this.Top;
			}
		}

		public Position(int left, int top)
		{
			this.Left = left;
			this.Top = top;
		}

		public Position(Point point)
		{
			this.Left = point.X;
			this.Top = point.Y;
		}

		public Position(double left, double top)
		{
			this.Left = Util.smethod_22(left);
			this.Top = Util.smethod_22(top);
		}

		public bool IsVisible
		{
			get
			{
				return this.Left >= 0 && this.Top >= 0;
			}
		}

		public string ToString()
		{
			return string.Format(Position.getString_0(107439596), this.Left, this.Top);
		}

		public bool Equals(object obj)
		{
			bool result;
			if (obj == null)
			{
				result = false;
			}
			else
			{
				Position position = obj as Position;
				result = (position != null && this.Left == position.Left && this.Top == position.Top);
			}
			return result;
		}

		public unsafe int GetHashCode()
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = this.Left;
			ref int ptr2 = ref *(int*)((byte*)ptr + 4);
			int hashCode = ((int*)ptr)->GetHashCode();
			*(int*)ptr = this.Top;
			ptr2 = hashCode + ((int*)ptr)->GetHashCode();
			return *(int*)((byte*)ptr + 4);
		}

		public unsafe static bool smethod_0(Position position_0, Position position_1)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((position_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((position_1 == null) ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[1] = (position_0.Equals(position_1) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_1(Position position_0, Position position_1)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((position_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((position_1 != null) ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[1] = ((!position_0.Equals(position_1)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		static Position()
		{
			Strings.CreateGetStringDelegate(typeof(Position));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
