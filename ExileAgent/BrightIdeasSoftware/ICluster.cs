using System;

namespace BrightIdeasSoftware
{
	public interface ICluster : IComparable
	{
		int Count { get; set; }

		string DisplayLabel { get; set; }

		object ClusterKey { get; set; }
	}
}
