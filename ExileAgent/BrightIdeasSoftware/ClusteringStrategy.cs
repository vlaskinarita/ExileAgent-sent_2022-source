using System;
using System.Collections;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public class ClusteringStrategy : IClusteringStrategy
	{
		public static string DefaultDisplayLabelFormatSingular
		{
			get
			{
				return ClusteringStrategy.defaultDisplayLabelFormatSingular;
			}
			set
			{
				ClusteringStrategy.defaultDisplayLabelFormatSingular = value;
			}
		}

		public static string DefaultDisplayLabelFormatPlural
		{
			get
			{
				return ClusteringStrategy.defaultDisplayLabelFormatPural;
			}
			set
			{
				ClusteringStrategy.defaultDisplayLabelFormatPural = value;
			}
		}

		public ClusteringStrategy()
		{
			this.DisplayLabelFormatSingular = ClusteringStrategy.DefaultDisplayLabelFormatSingular;
			this.DisplayLabelFormatPlural = ClusteringStrategy.DefaultDisplayLabelFormatPlural;
		}

		public OLVColumn Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		public string DisplayLabelFormatSingular
		{
			get
			{
				return this.displayLabelFormatSingular;
			}
			set
			{
				this.displayLabelFormatSingular = value;
			}
		}

		public string DisplayLabelFormatPlural
		{
			get
			{
				return this.displayLabelFormatPural;
			}
			set
			{
				this.displayLabelFormatPural = value;
			}
		}

		public virtual object GetClusterKey(object model)
		{
			return this.Column.GetValue(model);
		}

		public virtual ICluster CreateCluster(object clusterKey)
		{
			return new Cluster(clusterKey);
		}

		public virtual string GetClusterDisplayLabel(ICluster cluster)
		{
			string text = this.Column.ValueToString(cluster.ClusterKey) ?? ClusteringStrategy.NULL_LABEL;
			if (string.IsNullOrEmpty(text))
			{
				text = ClusteringStrategy.EMPTY_LABEL;
			}
			return this.ApplyDisplayFormat(cluster, text);
		}

		public virtual IModelFilter CreateFilter(IList valuesChosenForFiltering)
		{
			return new OneOfFilter(new AspectGetterDelegate(this.GetClusterKey), valuesChosenForFiltering);
		}

		protected virtual string ApplyDisplayFormat(ICluster cluster, string s)
		{
			string text = (cluster.Count == 1) ? this.DisplayLabelFormatSingular : this.DisplayLabelFormatPlural;
			if (!string.IsNullOrEmpty(text))
			{
				return string.Format(text, s, cluster.Count);
			}
			return s;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ClusteringStrategy()
		{
			Strings.CreateGetStringDelegate(typeof(ClusteringStrategy));
			ClusteringStrategy.NULL_LABEL = ClusteringStrategy.getString_0(107315475);
			ClusteringStrategy.EMPTY_LABEL = ClusteringStrategy.getString_0(107315434);
			ClusteringStrategy.defaultDisplayLabelFormatSingular = ClusteringStrategy.getString_0(107315421);
			ClusteringStrategy.defaultDisplayLabelFormatPural = ClusteringStrategy.getString_0(107314888);
		}

		public static string NULL_LABEL;

		public static string EMPTY_LABEL;

		private static string defaultDisplayLabelFormatSingular;

		private static string defaultDisplayLabelFormatPural;

		private OLVColumn column;

		private string displayLabelFormatSingular;

		private string displayLabelFormatPural;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
