using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public delegate bool RenderDelegate(EventArgs e, Graphics g, Rectangle r, object rowObject);
}
