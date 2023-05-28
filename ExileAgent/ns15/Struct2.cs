using System;

namespace ns15
{
	internal struct Struct2
	{
		public int height
		{
			get
			{
				return this.int_3 - this.int_1;
			}
		}

		public int width
		{
			get
			{
				return this.int_2 - this.int_0;
			}
		}

		public bool Equals(object obj)
		{
			bool result;
			if (!(obj is Struct2))
			{
				result = false;
			}
			else
			{
				Struct2 @struct = (Struct2)obj;
				result = (@struct.int_0 == this.int_0 && @struct.int_1 == this.int_1 && @struct.int_2 == this.int_2 && @struct.int_3 == this.int_3);
			}
			return result;
		}

		public int GetHashCode()
		{
			return this.int_0.GetHashCode() + this.int_1.GetHashCode() + this.int_2.GetHashCode() + this.int_3.GetHashCode();
		}

		public int int_0;

		public int int_1;

		public int int_2;

		public int int_3;
	}
}
