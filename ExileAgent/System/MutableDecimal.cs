using System;

namespace System
{
	internal struct MutableDecimal
	{
		public bool IsNegative
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
			set
			{
				this.Flags = ((this.Flags & 2147483647U) | (value ? 2147483648U : 0U));
			}
		}

		public int Scale
		{
			get
			{
				return (int)((byte)(this.Flags >> 16));
			}
			set
			{
				this.Flags = ((this.Flags & 4278255615U) | (uint)((uint)value << 16));
			}
		}

		public uint Flags;

		public uint High;

		public uint Low;

		public uint Mid;

		private const uint SignMask = 2147483648U;

		private const uint ScaleMask = 16711680U;

		private const int ScaleShift = 16;
	}
}
