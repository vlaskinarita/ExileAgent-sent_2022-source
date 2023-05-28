using System;
using System.Collections;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class AbstractVirtualListDataSource : IVirtualListDataSource, IFilterableDataSource
	{
		public AbstractVirtualListDataSource(VirtualObjectListView listView)
		{
			this.listView = listView;
		}

		public virtual object GetNthObject(int n)
		{
			return null;
		}

		public virtual int GetObjectCount()
		{
			return -1;
		}

		public virtual int GetObjectIndex(object model)
		{
			return -1;
		}

		public virtual void PrepareCache(int from, int to)
		{
		}

		public virtual int SearchText(string value, int first, int last, OLVColumn column)
		{
			return -1;
		}

		public virtual void Sort(OLVColumn column, SortOrder order)
		{
		}

		public virtual void AddObjects(ICollection modelObjects)
		{
		}

		public virtual void RemoveObjects(ICollection modelObjects)
		{
		}

		public virtual void SetObjects(IEnumerable collection)
		{
		}

		public virtual void UpdateObject(int index, object modelObject)
		{
		}

		public static int DefaultSearchText(string value, int first, int last, OLVColumn column, IVirtualListDataSource source)
		{
			if (first <= last)
			{
				for (int i = first; i <= last; i++)
				{
					string stringValue = column.GetStringValue(source.GetNthObject(i));
					if (stringValue.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = first; j >= last; j--)
				{
					string stringValue2 = column.GetStringValue(source.GetNthObject(j));
					if (stringValue2.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
					{
						return j;
					}
				}
			}
			return -1;
		}

		public virtual void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter)
		{
		}

		protected VirtualObjectListView listView;
	}
}
