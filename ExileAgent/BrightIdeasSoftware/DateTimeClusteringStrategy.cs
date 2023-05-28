using System;
using System.Globalization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class DateTimeClusteringStrategy : ClusteringStrategy
	{
		public DateTimeClusteringStrategy() : this(DateTimePortion.Year | DateTimePortion.Month, DateTimeClusteringStrategy.getString_1(107314902))
		{
		}

		public DateTimeClusteringStrategy(DateTimePortion portions, string format)
		{
			this.Portions = portions;
			this.Format = format;
		}

		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		public DateTimePortion Portions
		{
			get
			{
				return this.portions;
			}
			set
			{
				this.portions = value;
			}
		}

		public override object GetClusterKey(object model)
		{
			DateTime? dateTime = base.Column.GetValue(model) as DateTime?;
			if (dateTime == null)
			{
				return null;
			}
			int year = ((this.Portions & DateTimePortion.Year) == DateTimePortion.Year) ? dateTime.Value.Year : 1;
			int month = ((this.Portions & DateTimePortion.Month) == DateTimePortion.Month) ? dateTime.Value.Month : 1;
			int day = ((this.Portions & DateTimePortion.Day) == DateTimePortion.Day) ? dateTime.Value.Day : 1;
			int hour = ((this.Portions & DateTimePortion.Hour) == DateTimePortion.Hour) ? dateTime.Value.Hour : 0;
			int minute = ((this.Portions & DateTimePortion.Minute) == DateTimePortion.Minute) ? dateTime.Value.Minute : 0;
			int second = ((this.Portions & DateTimePortion.Second) == DateTimePortion.Second) ? dateTime.Value.Second : 0;
			return new DateTime(year, month, day, hour, minute, second);
		}

		public override string GetClusterDisplayLabel(ICluster cluster)
		{
			DateTime? dateTime = cluster.ClusterKey as DateTime?;
			return this.ApplyDisplayFormat(cluster, (dateTime != null) ? this.DateToString(dateTime.Value) : ClusteringStrategy.NULL_LABEL);
		}

		protected string DateToString(DateTime dateTime)
		{
			if (string.IsNullOrEmpty(this.Format))
			{
				return dateTime.ToString(CultureInfo.CurrentUICulture);
			}
			string result;
			try
			{
				result = dateTime.ToString(this.Format);
			}
			catch (FormatException)
			{
				result = string.Format(DateTimeClusteringStrategy.getString_1(107314857), this.Format, dateTime);
			}
			return result;
		}

		static DateTimeClusteringStrategy()
		{
			Strings.CreateGetStringDelegate(typeof(DateTimeClusteringStrategy));
		}

		private string format;

		private DateTimePortion portions = DateTimePortion.Year | DateTimePortion.Month;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
