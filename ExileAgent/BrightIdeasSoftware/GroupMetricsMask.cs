using System;

namespace BrightIdeasSoftware
{
	[Flags]
	public enum GroupMetricsMask
	{
		LVGMF_NONE = 0,
		LVGMF_BORDERSIZE = 1,
		LVGMF_BORDERCOLOR = 2,
		LVGMF_TEXTCOLOR = 4
	}
}
