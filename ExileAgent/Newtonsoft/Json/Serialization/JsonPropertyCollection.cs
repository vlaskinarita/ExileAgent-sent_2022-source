using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		public JsonPropertyCollection(Type type) : base(StringComparer.Ordinal)
		{
			ValidationUtils.ArgumentNotNull(type, JsonPropertyCollection.getString_0(107374298));
			this._type = type;
			this._list = (List<JsonProperty>)base.Items;
		}

		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		public void AddProperty(JsonProperty property)
		{
			if (base.Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				bool flag = true;
				if (jsonProperty.Ignored)
				{
					base.Remove(jsonProperty);
					flag = false;
				}
				else if (property.DeclaringType != null && jsonProperty.DeclaringType != null)
				{
					if (property.DeclaringType.IsSubclassOf(jsonProperty.DeclaringType) || (jsonProperty.DeclaringType.IsInterface() && property.DeclaringType.ImplementInterface(jsonProperty.DeclaringType)))
					{
						base.Remove(jsonProperty);
						flag = false;
					}
					if (jsonProperty.DeclaringType.IsSubclassOf(property.DeclaringType) || (property.DeclaringType.IsInterface() && jsonProperty.DeclaringType.ImplementInterface(property.DeclaringType)))
					{
						return;
					}
					if (this._type.ImplementInterface(jsonProperty.DeclaringType) && this._type.ImplementInterface(property.DeclaringType))
					{
						return;
					}
				}
				if (flag)
				{
					throw new JsonSerializationException(JsonPropertyCollection.getString_0(107339028).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, this._type));
				}
			}
			base.Add(property);
		}

		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty property = this.GetProperty(propertyName, StringComparison.Ordinal);
			if (property == null)
			{
				property = this.GetProperty(propertyName, StringComparison.OrdinalIgnoreCase);
			}
			return property;
		}

		private new bool TryGetValue(string key, out JsonProperty item)
		{
			if (base.Dictionary == null)
			{
				item = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, out item);
		}

		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			if (comparisonType != StringComparison.Ordinal)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					JsonProperty jsonProperty = this._list[i];
					if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
					{
						return jsonProperty;
					}
				}
				return null;
			}
			JsonProperty result;
			if (this.TryGetValue(propertyName, out result))
			{
				return result;
			}
			return null;
		}

		static JsonPropertyCollection()
		{
			Strings.CreateGetStringDelegate(typeof(JsonPropertyCollection));
		}

		private readonly Type _type;

		private readonly List<JsonProperty> _list;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
