using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class Cluster : IComparable, ICluster
	{
		public Cluster(object key)
		{
			this.Count = 1;
			this.ClusterKey = key;
		}

		public override string ToString()
		{
			return this.DisplayLabel ?? Cluster.getString_0(107315899);
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

		public string DisplayLabel
		{
			get
			{
				return this.displayLabel;
			}
			set
			{
				this.displayLabel = value;
			}
		}

		public object ClusterKey
		{
			get
			{
				return this.clusterKey;
			}
			set
			{
				this.clusterKey = value;
			}
		}

		public int CompareTo(object other)
		{
			if (other != null)
			{
				if (other != DBNull.Value)
				{
					ICluster cluster = other as ICluster;
					if (cluster == null)
					{
						return 1;
					}
					string text = this.ClusterKey as string;
					if (text != null)
					{
						return string.Compare(text, cluster.ClusterKey as string, StringComparison.CurrentCultureIgnoreCase);
					}
					IComparable comparable = this.ClusterKey as IComparable;
					if (comparable != null)
					{
						return comparable.CompareTo(cluster.ClusterKey);
					}
					return -1;
				}
			}
			return 1;
		}

		static Cluster()
		{
			Strings.CreateGetStringDelegate(typeof(Cluster));
		}

		private int count;

		private string displayLabel;

		private object clusterKey;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
