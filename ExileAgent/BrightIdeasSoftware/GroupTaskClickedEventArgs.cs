using System;

namespace BrightIdeasSoftware
{
	public sealed class GroupTaskClickedEventArgs : EventArgs
	{
		public GroupTaskClickedEventArgs(OLVGroup group)
		{
			this.group = group;
		}

		public OLVGroup Group
		{
			get
			{
				return this.group;
			}
		}

		private readonly OLVGroup group;
	}
}
