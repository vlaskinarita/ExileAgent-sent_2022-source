using System;

namespace BrightIdeasSoftware
{
	public sealed class TreeBranchExpandingEventArgs : CancellableEventArgs
	{
		public TreeBranchExpandingEventArgs(object model, OLVListItem item)
		{
			this.Model = model;
			this.Item = item;
		}

		public object Model
		{
			get
			{
				return this.model;
			}
			private set
			{
				this.model = value;
			}
		}

		public OLVListItem Item
		{
			get
			{
				return this.item;
			}
			private set
			{
				this.item = value;
			}
		}

		private object model;

		private OLVListItem item;
	}
}
