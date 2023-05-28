using System;

namespace BrightIdeasSoftware
{
	public interface IFilterableDataSource
	{
		void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter);
	}
}
