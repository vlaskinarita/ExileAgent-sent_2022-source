using System;

namespace BrightIdeasSoftware
{
	public interface IDecoration : IOverlay
	{
		OLVListItem ListItem { get; set; }

		OLVListSubItem SubItem { get; set; }
	}
}
