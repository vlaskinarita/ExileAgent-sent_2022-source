using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public interface IOverlay
	{
		void Draw(ObjectListView olv, Graphics g, Rectangle r);
	}
}
