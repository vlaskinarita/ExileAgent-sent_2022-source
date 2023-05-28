using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class TailFilter : AbstractListFilter
	{
		public TailFilter()
		{
		}

		public TailFilter(int numberOfObjects)
		{
			this.Count = numberOfObjects;
		}

		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		public override IEnumerable Filter(IEnumerable modelObjects)
		{
			if (this.Count <= 0)
			{
				return modelObjects;
			}
			ArrayList arrayList = ObjectListView.EnumerableToArray(modelObjects, false);
			if (this.Count > arrayList.Count)
			{
				return arrayList;
			}
			object[] array = new object[this.Count];
			arrayList.CopyTo(arrayList.Count - this.Count, array, 0, this.Count);
			return new ArrayList(array);
		}

		private int count;
	}
}
