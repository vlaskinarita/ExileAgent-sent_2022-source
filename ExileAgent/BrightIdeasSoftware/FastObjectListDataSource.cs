using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class FastObjectListDataSource : AbstractVirtualListDataSource
	{
		public FastObjectListDataSource(FastObjectListView listView) : base(listView)
		{
		}

		public override object GetNthObject(int n)
		{
			if (n >= 0 && n < this.filteredObjectList.Count)
			{
				return this.filteredObjectList[n];
			}
			return null;
		}

		public override int GetObjectCount()
		{
			return this.filteredObjectList.Count;
		}

		public override int GetObjectIndex(object model)
		{
			int result;
			if (model != null && this.objectsToIndexMap.TryGetValue(model, out result))
			{
				return result;
			}
			return -1;
		}

		public override int SearchText(string text, int first, int last, OLVColumn column)
		{
			if (first <= last)
			{
				for (int i = first; i <= last; i++)
				{
					string stringValue = column.GetStringValue(this.listView.GetNthItemInDisplayOrder(i).RowObject);
					if (stringValue.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = first; j >= last; j--)
				{
					string stringValue2 = column.GetStringValue(this.listView.GetNthItemInDisplayOrder(j).RowObject);
					if (stringValue2.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
					{
						return j;
					}
				}
			}
			return -1;
		}

		public override void Sort(OLVColumn column, SortOrder sortOrder)
		{
			if (sortOrder != SortOrder.None)
			{
				ModelObjectComparer comparer = new ModelObjectComparer(column, sortOrder, this.listView.SecondarySortColumn, this.listView.SecondarySortOrder);
				this.fullObjectList.Sort(comparer);
				this.filteredObjectList.Sort(comparer);
			}
			this.RebuildIndexMap();
		}

		public override void AddObjects(ICollection modelObjects)
		{
			foreach (object obj in modelObjects)
			{
				if (obj != null)
				{
					this.fullObjectList.Add(obj);
				}
			}
			this.FilterObjects();
			this.RebuildIndexMap();
		}

		public override void RemoveObjects(ICollection modelObjects)
		{
			List<int> list = new List<int>();
			foreach (object obj in modelObjects)
			{
				int objectIndex = this.GetObjectIndex(obj);
				if (objectIndex >= 0)
				{
					list.Add(objectIndex);
				}
				this.fullObjectList.Remove(obj);
			}
			list.Sort();
			list.Reverse();
			foreach (int itemIndex in list)
			{
				this.listView.SelectedIndices.Remove(itemIndex);
			}
			this.FilterObjects();
			this.RebuildIndexMap();
		}

		public override void SetObjects(IEnumerable collection)
		{
			ArrayList arrayList = ObjectListView.EnumerableToArray(collection, true);
			this.fullObjectList = arrayList;
			this.FilterObjects();
			this.RebuildIndexMap();
		}

		public override void UpdateObject(int index, object modelObject)
		{
			if (index < 0 || index >= this.filteredObjectList.Count)
			{
				return;
			}
			int num = this.fullObjectList.IndexOf(this.filteredObjectList[index]);
			if (num < 0)
			{
				return;
			}
			this.fullObjectList[num] = modelObject;
			this.filteredObjectList[index] = modelObject;
			this.objectsToIndexMap[modelObject] = index;
		}

		public override void ApplyFilters(IModelFilter iModelFilter, IListFilter iListFilter)
		{
			this.modelFilter = iModelFilter;
			this.listFilter = iListFilter;
			this.SetObjects(this.fullObjectList);
		}

		public ArrayList ObjectList
		{
			get
			{
				return this.fullObjectList;
			}
		}

		public ArrayList FilteredObjectList
		{
			get
			{
				return this.filteredObjectList;
			}
		}

		protected void RebuildIndexMap()
		{
			this.objectsToIndexMap.Clear();
			for (int i = 0; i < this.filteredObjectList.Count; i++)
			{
				this.objectsToIndexMap[this.filteredObjectList[i]] = i;
			}
		}

		protected void FilterObjects()
		{
			if (!this.listView.UseFiltering || (this.modelFilter == null && this.listFilter == null))
			{
				this.filteredObjectList = new ArrayList(this.fullObjectList);
				return;
			}
			IEnumerable enumerable = (this.listFilter == null) ? this.fullObjectList : this.listFilter.Filter(this.fullObjectList);
			if (this.modelFilter == null)
			{
				this.filteredObjectList = ObjectListView.EnumerableToArray(enumerable, false);
				return;
			}
			this.filteredObjectList = new ArrayList();
			foreach (object obj in enumerable)
			{
				if (this.modelFilter.Filter(obj))
				{
					this.filteredObjectList.Add(obj);
				}
			}
		}

		private ArrayList fullObjectList = new ArrayList();

		private ArrayList filteredObjectList = new ArrayList();

		private IModelFilter modelFilter;

		private IListFilter listFilter;

		private readonly Dictionary<object, int> objectsToIndexMap = new Dictionary<object, int>();
	}
}
