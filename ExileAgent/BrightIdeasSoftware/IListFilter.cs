using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public interface IListFilter
	{
		IEnumerable Filter(IEnumerable modelObjects);
	}
}
