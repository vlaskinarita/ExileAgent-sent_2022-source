using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class SimpleDragSource : IDragSource
	{
		public SimpleDragSource()
		{
		}

		public SimpleDragSource(bool refreshAfterDrop)
		{
			this.RefreshAfterDrop = refreshAfterDrop;
		}

		public bool RefreshAfterDrop
		{
			get
			{
				return this.refreshAfterDrop;
			}
			set
			{
				this.refreshAfterDrop = value;
			}
		}

		public object StartDrag(ObjectListView olv, MouseButtons button, OLVListItem item)
		{
			if (button != MouseButtons.Left)
			{
				return null;
			}
			return this.CreateDataObject(olv);
		}

		public DragDropEffects GetAllowedEffects(object data)
		{
			return DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Scroll;
		}

		public void EndDrag(object dragObject, DragDropEffects effect)
		{
			OLVDataObject olvdataObject = dragObject as OLVDataObject;
			if (olvdataObject == null)
			{
				return;
			}
			if (this.RefreshAfterDrop)
			{
				olvdataObject.ListView.RefreshObjects(olvdataObject.ModelObjects);
			}
		}

		protected object CreateDataObject(ObjectListView olv)
		{
			return new OLVDataObject(olv);
		}

		private bool refreshAfterDrop;
	}
}
