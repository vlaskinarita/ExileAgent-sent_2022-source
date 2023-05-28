using System;

namespace BrightIdeasSoftware
{
	[Flags]
	public enum GroupState
	{
		LVGS_NORMAL = 0,
		LVGS_COLLAPSED = 1,
		LVGS_HIDDEN = 2,
		LVGS_NOHEADER = 4,
		LVGS_COLLAPSIBLE = 8,
		LVGS_FOCUSED = 16,
		LVGS_SELECTED = 32,
		LVGS_SUBSETED = 64,
		LVGS_SUBSETLINKFOCUSED = 128,
		LVGS_ALL = 65535
	}
}
