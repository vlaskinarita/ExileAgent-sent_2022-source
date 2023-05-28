using System;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public class AbstractVirtualGroups : IVirtualGroups
	{
		public virtual IList<OLVGroup> GetGroups(GroupingParameters parameters)
		{
			return new List<OLVGroup>();
		}

		public virtual int GetGroupMember(OLVGroup group, int indexWithinGroup)
		{
			return -1;
		}

		public virtual int GetGroup(int itemIndex)
		{
			return -1;
		}

		public virtual int GetIndexWithinGroup(OLVGroup group, int itemIndex)
		{
			return -1;
		}

		public virtual void CacheHint(int fromGroupIndex, int fromIndex, int toGroupIndex, int toIndex)
		{
		}
	}
}
