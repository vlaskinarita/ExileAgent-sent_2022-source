using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class CellBorderDecoration : BorderDecoration
	{
		protected override Rectangle CalculateBounds()
		{
			return base.CellBounds;
		}
	}
}
