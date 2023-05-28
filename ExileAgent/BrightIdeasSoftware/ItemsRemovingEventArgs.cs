using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class ItemsRemovingEventArgs : CancellableEventArgs
	{
		public ItemsRemovingEventArgs(ICollection objectsToRemove)
		{
			this.ObjectsToRemove = objectsToRemove;
		}

		public ICollection ObjectsToRemove;
	}
}
