using System;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public interface IVirtualGroups
	{
		IList<OLVGroup> GetGroups(GroupingParameters parameters);

		int GetGroupMember(OLVGroup group, int indexWithinGroup);

		int GetGroup(int itemIndex);

		int GetIndexWithinGroup(OLVGroup group, int itemIndex);

		void CacheHint(int fromGroupIndex, int fromIndex, int toGroupIndex, int toIndex);
	}
}
