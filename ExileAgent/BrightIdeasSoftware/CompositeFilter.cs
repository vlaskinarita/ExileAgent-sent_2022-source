using System;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public abstract class CompositeFilter : IModelFilter
	{
		public CompositeFilter()
		{
		}

		public CompositeFilter(IEnumerable<IModelFilter> filters)
		{
			foreach (IModelFilter modelFilter in filters)
			{
				if (modelFilter != null)
				{
					this.Filters.Add(modelFilter);
				}
			}
		}

		public IList<IModelFilter> Filters
		{
			get
			{
				return this.filters;
			}
			set
			{
				this.filters = value;
			}
		}

		public virtual bool Filter(object modelObject)
		{
			return this.Filters == null || this.Filters.Count == 0 || this.FilterObject(modelObject);
		}

		public abstract bool FilterObject(object modelObject);

		private IList<IModelFilter> filters = new List<IModelFilter>();
	}
}
