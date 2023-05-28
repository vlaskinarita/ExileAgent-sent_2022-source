using System;

namespace BrightIdeasSoftware
{
	public sealed class ItemsChangedEventArgs : EventArgs
	{
		public ItemsChangedEventArgs()
		{
		}

		public ItemsChangedEventArgs(int oldObjectCount, int newObjectCount)
		{
			this.oldObjectCount = oldObjectCount;
			this.newObjectCount = newObjectCount;
		}

		public int OldObjectCount
		{
			get
			{
				return this.oldObjectCount;
			}
		}

		public int NewObjectCount
		{
			get
			{
				return this.newObjectCount;
			}
		}

		private int oldObjectCount;

		private int newObjectCount;
	}
}
