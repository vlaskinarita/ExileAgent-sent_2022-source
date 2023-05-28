using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class ExpandoObjectConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return this.ReadValue(reader);
		}

		private object ReadValue(JsonReader reader)
		{
			if (!reader.MoveToContent())
			{
				throw JsonSerializationException.Create(reader, ExpandoObjectConverter.getString_0(107322199));
			}
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadObject(reader);
			}
			if (tokenType == JsonToken.StartArray)
			{
				return this.ReadList(reader);
			}
			if (JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
			{
				return reader.Value;
			}
			throw JsonSerializationException.Create(reader, ExpandoObjectConverter.getString_0(107322110).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		private object ReadList(JsonReader reader)
		{
			IList<object> list = new List<object>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.EndArray)
					{
						return list;
					}
					object item = this.ReadValue(reader);
					list.Add(item);
				}
			}
			throw JsonSerializationException.Create(reader, ExpandoObjectConverter.getString_0(107322199));
		}

		private object ReadObject(JsonReader reader)
		{
			IDictionary<string, object> dictionary = new ExpandoObject();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							return dictionary;
						}
					}
				}
				else
				{
					string key = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, ExpandoObjectConverter.getString_0(107322199));
					}
					object value = this.ReadValue(reader);
					dictionary[key] = value;
				}
			}
			throw JsonSerializationException.Create(reader, ExpandoObjectConverter.getString_0(107322199));
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ExpandoObject);
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		static ExpandoObjectConverter()
		{
			Strings.CreateGetStringDelegate(typeof(ExpandoObjectConverter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
