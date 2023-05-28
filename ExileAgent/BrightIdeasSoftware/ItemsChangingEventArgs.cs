using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class ItemsChangingEventArgs : CancellableEventArgs
	{
		public ItemsChangingEventArgs(IEnumerable oldObjects, IEnumerable newObjects)
		{
			this.oldObjects = oldObjects;
			this.NewObjects = newObjects;
		}

		public IEnumerable OldObjects
		{
			get
			{
				return this.oldObjects;
			}
		}

		private IEnumerable oldObjects;

		public IEnumerable NewObjects;
	}
}
