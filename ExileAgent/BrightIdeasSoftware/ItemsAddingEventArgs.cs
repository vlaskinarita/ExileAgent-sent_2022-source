using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class ItemsAddingEventArgs : CancellableEventArgs
	{
		public ItemsAddingEventArgs(ICollection objectsToAdd)
		{
			this.ObjectsToAdd = objectsToAdd;
		}

		public ICollection ObjectsToAdd;
	}
}
