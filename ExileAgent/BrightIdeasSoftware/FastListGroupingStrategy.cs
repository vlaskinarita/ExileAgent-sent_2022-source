using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class FastListGroupingStrategy : AbstractVirtualGroups
	{
		public override IList<OLVGroup> GetGroups(GroupingParameters parmameters)
		{
			FastObjectListView folv = (FastObjectListView)parmameters.ListView;
			int num = 0;
			NullableDictionary<object, List<object>> nullableDictionary = new NullableDictionary<object, List<object>>();
			foreach (object obj in folv.FilteredObjects)
			{
				object groupKey = parmameters.GroupByColumn.GetGroupKey(obj);
				if (!nullableDictionary.ContainsKey(groupKey))
				{
					nullableDictionary[groupKey] = new List<object>();
				}
				nullableDictionary[groupKey].Add(obj);
				num++;
			}
			OLVColumn col = parmameters.SortItemsByPrimaryColumn ? parmameters.ListView.GetColumn(0) : parmameters.PrimarySort;
			ModelObjectComparer comparer = new ModelObjectComparer(col, parmameters.PrimarySortOrder, parmameters.SecondarySort, parmameters.SecondarySortOrder);
			foreach (object key in nullableDictionary.Keys)
			{
				nullableDictionary[key].Sort(comparer);
			}
			List<OLVGroup> list = new List<OLVGroup>();
			foreach (object obj2 in nullableDictionary.Keys)
			{
				string text = parmameters.GroupByColumn.ConvertGroupKeyToTitle(obj2);
				if (!string.IsNullOrEmpty(parmameters.TitleFormat))
				{
					int count = nullableDictionary[obj2].Count;
					string text2 = (count == 1) ? parmameters.TitleSingularFormat : parmameters.TitleFormat;
					try
					{
						text = string.Format(text2, text, count);
					}
					catch (FormatException)
					{
						text = FastListGroupingStrategy.getString_0(107316796) + text2;
					}
				}
				OLVGroup olvgroup = new OLVGroup(text);
				olvgroup.Collapsible = folv.HasCollapsibleGroups;
				olvgroup.Key = obj2;
				olvgroup.SortValue = (obj2 as IComparable);
				olvgroup.Contents = nullableDictionary[obj2].ConvertAll<int>((object x) => folv.IndexOf(x));
				olvgroup.VirtualItemCount = nullableDictionary[obj2].Count;
				if (parmameters.GroupByColumn.GroupFormatter != null)
				{
					parmameters.GroupByColumn.GroupFormatter(olvgroup, parmameters);
				}
				list.Add(olvgroup);
			}
			if (parmameters.GroupByOrder != SortOrder.None)
			{
				list.Sort(parmameters.GroupComparer ?? new OLVGroupComparer(parmameters.GroupByOrder));
			}
			this.indexToGroupMap = new List<int>(num);
			this.indexToGroupMap.AddRange(new int[num]);
			for (int i = 0; i < list.Count; i++)
			{
				OLVGroup olvgroup2 = list[i];
				List<int> list2 = (List<int>)olvgroup2.Contents;
				foreach (int index in list2)
				{
					this.indexToGroupMap[index] = i;
				}
			}
			return list;
		}

		public override int GetGroupMember(OLVGroup group, int indexWithinGroup)
		{
			return (int)group.Contents[indexWithinGroup];
		}

		public override int GetGroup(int itemIndex)
		{
			return this.indexToGroupMap[itemIndex];
		}

		public override int GetIndexWithinGroup(OLVGroup group, int itemIndex)
		{
			return group.Contents.IndexOf(itemIndex);
		}

		static FastListGroupingStrategy()
		{
			Strings.CreateGetStringDelegate(typeof(FastListGroupingStrategy));
		}

		private List<int> indexToGroupMap;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
