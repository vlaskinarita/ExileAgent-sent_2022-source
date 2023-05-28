using System;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public sealed class CompositeAnyFilter : CompositeFilter
	{
		public CompositeAnyFilter(List<IModelFilter> filters) : base(filters)
		{
		}

		public override bool FilterObject(object modelObject)
		{
			foreach (IModelFilter modelFilter in base.Filters)
			{
				if (modelFilter.Filter(modelObject))
				{
					return true;
				}
			}
			return false;
		}
	}
}
