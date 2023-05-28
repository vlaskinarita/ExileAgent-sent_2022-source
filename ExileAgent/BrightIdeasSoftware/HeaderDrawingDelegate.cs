using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public delegate bool HeaderDrawingDelegate(Graphics g, Rectangle r, int columnIndex, OLVColumn column, bool isPressed, HeaderStateStyle stateStyle);
}
