using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class AbstractDragSource : IDragSource
	{
		public object StartDrag(ObjectListView olv, MouseButtons button, OLVListItem item)
		{
			return null;
		}

		public DragDropEffects GetAllowedEffects(object data)
		{
			return DragDropEffects.None;
		}

		public void EndDrag(object dragObject, DragDropEffects effect)
		{
		}
	}
}
