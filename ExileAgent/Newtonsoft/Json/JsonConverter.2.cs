using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using ns20;

namespace Newtonsoft.Json
{
	public abstract class JsonConverter<T> : JsonConverter
	{
		public sealed override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!((value != null) ? (value is T) : ReflectionUtils.IsNullable(typeof(T))))
			{
				throw new JsonSerializationException(Class401.smethod_0(107346135).FormatWith(CultureInfo.InvariantCulture, typeof(T)));
			}
			this.WriteJson(writer, (T)((object)value), serializer);
		}

		public abstract void WriteJson(JsonWriter writer, T value, JsonSerializer serializer);

		public sealed override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag;
			if (!(flag = (existingValue == null)) && !(existingValue is T))
			{
				throw new JsonSerializationException(Class401.smethod_0(107346046).FormatWith(CultureInfo.InvariantCulture, typeof(T)));
			}
			return this.ReadJson(reader, objectType, flag ? default(T) : ((T)((object)existingValue)), !flag, serializer);
		}

		public abstract T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer);

		public sealed override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}
	}
}
