using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class ListFilter : AbstractListFilter
	{
		public ListFilter(ListFilter.ListFilterDelegate function)
		{
			this.Function = function;
		}

		public ListFilter.ListFilterDelegate Function
		{
			get
			{
				return this.function;
			}
			set
			{
				this.function = value;
			}
		}

		public override IEnumerable Filter(IEnumerable modelObjects)
		{
			if (this.Function == null)
			{
				return modelObjects;
			}
			return this.Function(modelObjects);
		}

		private ListFilter.ListFilterDelegate function;

		public delegate IEnumerable ListFilterDelegate(IEnumerable rowObjects);
	}
}
