using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns33
{
	internal sealed class Class253
	{
		public int Width { get; set; }

		public int Height { get; set; }

		public Type Type { get; set; }

		public Class253(int int_2, int int_3, Type type_1)
		{
			this.Width = int_2;
			this.Height = int_3;
			this.Type = type_1;
		}

		public string ToString()
		{
			return string.Format(Class253.getString_0(107442061), this.Width, this.Height);
		}

		public bool Equals(object obj)
		{
			bool result;
			if (obj == null || !base.GetType().Equals(obj.GetType()))
			{
				result = false;
			}
			else
			{
				Class253 @class = (Class253)obj;
				result = (this.Width == @class.Width && this.Height == @class.Height);
			}
			return result;
		}

		public unsafe int GetHashCode()
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = this.Width;
			ref int ptr2 = ref *(int*)((byte*)ptr + 4);
			int hashCode = ((int*)ptr)->GetHashCode();
			*(int*)ptr = this.Height;
			ptr2 = hashCode + ((int*)ptr)->GetHashCode();
			return *(int*)((byte*)ptr + 4);
		}

		static Class253()
		{
			Strings.CreateGetStringDelegate(typeof(Class253));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Type type_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
