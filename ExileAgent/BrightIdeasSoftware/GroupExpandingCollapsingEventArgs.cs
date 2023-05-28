using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class GroupExpandingCollapsingEventArgs : CancellableEventArgs
	{
		public GroupExpandingCollapsingEventArgs(OLVGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException(GroupExpandingCollapsingEventArgs.getString_0(107314902));
			}
			this.olvGroup = group;
		}

		public OLVGroup Group
		{
			get
			{
				return this.olvGroup;
			}
		}

		public bool IsExpanding
		{
			get
			{
				return this.Group.Collapsed;
			}
		}

		static GroupExpandingCollapsingEventArgs()
		{
			Strings.CreateGetStringDelegate(typeof(GroupExpandingCollapsingEventArgs));
		}

		private readonly OLVGroup olvGroup;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
