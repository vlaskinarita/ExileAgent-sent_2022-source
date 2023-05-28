using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class FlagClusteringStrategy : ClusteringStrategy
	{
		public FlagClusteringStrategy(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException(FlagClusteringStrategy.getString_1(107345456));
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(FlagClusteringStrategy.getString_1(107314839), FlagClusteringStrategy.getString_1(107345456));
			}
			if (enumType.GetCustomAttributes(typeof(FlagsAttribute), false) == null)
			{
				throw new ArgumentException(FlagClusteringStrategy.getString_1(107314814), FlagClusteringStrategy.getString_1(107345456));
			}
			List<long> list = new List<long>();
			foreach (object value in Enum.GetValues(enumType))
			{
				list.Add(Convert.ToInt64(value));
			}
			List<string> list2 = new List<string>();
			foreach (string item in Enum.GetNames(enumType))
			{
				list2.Add(item);
			}
			this.SetValues(list.ToArray(), list2.ToArray());
		}

		public FlagClusteringStrategy(long[] values, string[] labels)
		{
			this.SetValues(values, labels);
		}

		public long[] Values
		{
			get
			{
				return this.values;
			}
			private set
			{
				this.values = value;
			}
		}

		public string[] Labels
		{
			get
			{
				return this.labels;
			}
			private set
			{
				this.labels = value;
			}
		}

		private void SetValues(long[] flags, string[] flagLabels)
		{
			if (flags == null || flags.Length == 0)
			{
				throw new ArgumentNullException(FlagClusteringStrategy.getString_1(107314769));
			}
			if (flagLabels == null || flagLabels.Length == 0)
			{
				throw new ArgumentNullException(FlagClusteringStrategy.getString_1(107314728));
			}
			if (flags.Length != flagLabels.Length)
			{
				throw new ArgumentException(FlagClusteringStrategy.getString_1(107314743), FlagClusteringStrategy.getString_1(107314769));
			}
			this.Values = flags;
			this.Labels = flagLabels;
		}

		public override object GetClusterKey(object model)
		{
			List<long> list = new List<long>();
			object result;
			try
			{
				long num = Convert.ToInt64(base.Column.GetValue(model));
				foreach (long num2 in this.Values)
				{
					if ((num2 & num) == num2)
					{
						list.Add(num2);
					}
				}
				result = list;
			}
			catch (InvalidCastException)
			{
				result = list;
			}
			catch (FormatException)
			{
				result = list;
			}
			return result;
		}

		public override string GetClusterDisplayLabel(ICluster cluster)
		{
			long num = Convert.ToInt64(cluster.ClusterKey);
			for (int i = 0; i < this.Values.Length; i++)
			{
				if (num == this.Values[i])
				{
					return this.ApplyDisplayFormat(cluster, this.Labels[i]);
				}
			}
			return this.ApplyDisplayFormat(cluster, num.ToString(CultureInfo.CurrentUICulture));
		}

		public override IModelFilter CreateFilter(IList valuesChosenForFiltering)
		{
			return new FlagBitSetFilter(new AspectGetterDelegate(this.GetClusterKey), valuesChosenForFiltering);
		}

		static FlagClusteringStrategy()
		{
			Strings.CreateGetStringDelegate(typeof(FlagClusteringStrategy));
		}

		private long[] values;

		private string[] labels;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
