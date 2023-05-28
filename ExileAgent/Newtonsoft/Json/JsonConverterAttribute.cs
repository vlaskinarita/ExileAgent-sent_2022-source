using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		public object[] ConverterParameters { get; }

		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException(JsonConverterAttribute.getString_0(107350974));
			}
			this._converterType = converterType;
		}

		public JsonConverterAttribute(Type converterType, params object[] converterParameters) : this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		static JsonConverterAttribute()
		{
			Strings.CreateGetStringDelegate(typeof(JsonConverterAttribute));
		}

		private readonly Type _converterType;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
