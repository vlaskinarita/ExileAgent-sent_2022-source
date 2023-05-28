using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public class AbstractListFilter : IListFilter
	{
		public virtual IEnumerable Filter(IEnumerable modelObjects)
		{
			return modelObjects;
		}
	}
}
