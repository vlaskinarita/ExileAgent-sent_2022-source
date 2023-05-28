using System;

namespace BrightIdeasSoftware
{
	public sealed class ClustersFromGroupsStrategy : ClusteringStrategy
	{
		public override object GetClusterKey(object model)
		{
			return base.Column.GetGroupKey(model);
		}

		public override string GetClusterDisplayLabel(ICluster cluster)
		{
			string text = base.Column.ConvertGroupKeyToTitle(cluster.ClusterKey);
			if (string.IsNullOrEmpty(text))
			{
				text = ClusteringStrategy.EMPTY_LABEL;
			}
			return this.ApplyDisplayFormat(cluster, text);
		}
	}
}
