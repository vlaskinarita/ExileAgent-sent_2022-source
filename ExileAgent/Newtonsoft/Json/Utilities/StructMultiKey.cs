using System;

namespace Newtonsoft.Json.Utilities
{
	internal readonly struct StructMultiKey<T1, T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		public override int GetHashCode()
		{
			T1 value = this.Value1;
			ref T1 ptr = ref value;
			T1 t = default(T1);
			int num;
			if (t == null)
			{
				t = value;
				ptr = ref t;
				if (t == null)
				{
					num = 0;
					goto IL_38;
				}
			}
			num = ptr.GetHashCode();
			IL_38:
			T2 value2 = this.Value2;
			ref T2 ptr2 = ref value2;
			T2 t2 = default(T2);
			int num2;
			if (t2 == null)
			{
				t2 = value2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					num2 = 0;
					goto IL_70;
				}
			}
			num2 = ptr2.GetHashCode();
			IL_70:
			return num ^ num2;
		}

		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> other = (StructMultiKey<T1, T2>)obj;
				return this.Equals(other);
			}
			return false;
		}

		public bool Equals(StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		public readonly T1 Value1;

		public readonly T2 Value2;
	}
}
