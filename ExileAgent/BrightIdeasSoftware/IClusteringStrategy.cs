using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public interface IClusteringStrategy
	{
		OLVColumn Column { get; set; }

		object GetClusterKey(object model);

		ICluster CreateCluster(object clusterKey);

		string GetClusterDisplayLabel(ICluster cluster);

		IModelFilter CreateFilter(IList valuesChosenForFiltering);
	}
}
