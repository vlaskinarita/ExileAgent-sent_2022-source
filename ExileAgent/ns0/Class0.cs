using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns20;

namespace ns0
{
	[CompilerGenerated]
	[DebuggerDisplay("\\{ body = {body}, type = {type} }", Type = "<Anonymous Type>")]
	internal sealed class Class0<T, U>
	{
		public T body
		{
			get
			{
				return this.gparam_0;
			}
		}

		public U type
		{
			get
			{
				return this.gparam_1;
			}
		}

		[DebuggerHidden]
		public Class0(T gparam_2, U gparam_3)
		{
			this.gparam_0 = gparam_2;
			this.gparam_1 = gparam_3;
		}

		[DebuggerHidden]
		public bool Equals(object obj)
		{
			Class0<T, U> @class = obj as Class0<T, U>;
			return @class != null && EqualityComparer<T>.Default.Equals(this.gparam_0, @class.gparam_0) && EqualityComparer<U>.Default.Equals(this.gparam_1, @class.gparam_1);
		}

		[DebuggerHidden]
		public int GetHashCode()
		{
			return (-1487863944 + EqualityComparer<T>.Default.GetHashCode(this.gparam_0)) * -1521134295 + EqualityComparer<U>.Default.GetHashCode(this.gparam_1);
		}

		[DebuggerHidden]
		public string ToString()
		{
			IFormatProvider provider = null;
			string format = Class401.smethod_0(107396715);
			object[] array = new object[2];
			int num = 0;
			T t = this.gparam_0;
			ref T ptr = ref t;
			T t2 = default(T);
			object obj;
			if (t2 == null)
			{
				t2 = t;
				ptr = ref t2;
				if (t2 == null)
				{
					obj = null;
					goto IL_4B;
				}
			}
			obj = ptr.ToString();
			IL_4B:
			array[num] = obj;
			int num2 = 1;
			U u = this.gparam_1;
			ref U ptr2 = ref u;
			U u2 = default(U);
			object obj2;
			if (u2 == null)
			{
				u2 = u;
				ptr2 = ref u2;
				if (u2 == null)
				{
					obj2 = null;
					goto IL_86;
				}
			}
			obj2 = ptr2.ToString();
			IL_86:
			array[num2] = obj2;
			return string.Format(provider, format, array);
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly T gparam_0;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly U gparam_1;
	}
}
