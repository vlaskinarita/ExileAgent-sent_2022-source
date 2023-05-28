using System;

namespace BrightIdeasSoftware
{
	[Flags]
	public enum DateTimePortion
	{
		Year = 1,
		Month = 2,
		Day = 4,
		Hour = 8,
		Minute = 16,
		Second = 32
	}
}
