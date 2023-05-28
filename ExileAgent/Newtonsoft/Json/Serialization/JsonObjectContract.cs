using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class JsonObjectContract : JsonContainerContract
	{
		public MemberSerialization MemberSerialization { get; set; }

		public Required? ItemRequired { get; set; }

		public NullValueHandling? ItemNullValueHandling { get; set; }

		public JsonPropertyCollection Properties { get; }

		public JsonPropertyCollection CreatorParameters
		{
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		public ObjectConstructor<object> OverrideCreator
		{
			get
			{
				return this._overrideCreator;
			}
			set
			{
				this._overrideCreator = value;
			}
		}

		internal ObjectConstructor<object> ParameterizedCreator
		{
			get
			{
				return this._parameterizedCreator;
			}
			set
			{
				this._parameterizedCreator = value;
			}
		}

		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = (value != null && typeof(JToken).IsAssignableFrom(value));
			}
		}

		public Func<string, string> ExtensionDataNameResolver { get; set; }

		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								if (!(defaultValueHandling.GetValueOrDefault() == DefaultValueHandling.Populate & defaultValueHandling != null))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException(JsonObjectContract.getString_1(107338560).FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		static JsonObjectContract()
		{
			Strings.CreateGetStringDelegate(typeof(JsonObjectContract));
		}

		internal bool ExtensionDataIsJToken;

		private bool? _hasRequiredOrDefaultValueProperties;

		private ObjectConstructor<object> _overrideCreator;

		private ObjectConstructor<object> _parameterizedCreator;

		private JsonPropertyCollection _creatorParameters;

		private Type _extensionDataValueType;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
