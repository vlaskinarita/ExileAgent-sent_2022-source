using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JsonMergeSettings
	{
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException(JsonMergeSettings.getString_0(107454443));
				}
				this._mergeArrayHandling = value;
			}
		}

		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException(JsonMergeSettings.getString_0(107454443));
				}
				this._mergeNullValueHandling = value;
			}
		}

		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < StringComparison.CurrentCulture || value > StringComparison.OrdinalIgnoreCase)
				{
					throw new ArgumentOutOfRangeException(JsonMergeSettings.getString_0(107454443));
				}
				this._propertyNameComparison = value;
			}
		}

		static JsonMergeSettings()
		{
			Strings.CreateGetStringDelegate(typeof(JsonMergeSettings));
		}

		private MergeArrayHandling _mergeArrayHandling;

		private MergeNullValueHandling _mergeNullValueHandling;

		private StringComparison _propertyNameComparison;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
