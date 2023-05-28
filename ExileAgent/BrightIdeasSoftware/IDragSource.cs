using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public interface IDragSource
	{
		object StartDrag(ObjectListView olv, MouseButtons button, OLVListItem item);

		DragDropEffects GetAllowedEffects(object dragObject);

		void EndDrag(object dragObject, DragDropEffects effect);
	}
}
