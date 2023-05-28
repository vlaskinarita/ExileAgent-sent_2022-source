using System;

namespace BrightIdeasSoftware
{
	[Flags]
	public enum DropTargetLocation
	{
		None = 0,
		Background = 1,
		Item = 2,
		BetweenItems = 4,
		AboveItem = 8,
		BelowItem = 16,
		SubItem = 32,
		RightOfItem = 64,
		LeftOfItem = 128
	}
}
