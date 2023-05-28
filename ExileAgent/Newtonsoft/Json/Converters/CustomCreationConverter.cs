using System;
using ns20;

namespace Newtonsoft.Json.Converters
{
	public abstract class CustomCreationConverter<T> : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException(Class401.smethod_0(107282686));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException(Class401.smethod_0(107282565));
			}
			serializer.Populate(reader, t);
			return t;
		}

		public abstract T Create(Type objectType);

		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
