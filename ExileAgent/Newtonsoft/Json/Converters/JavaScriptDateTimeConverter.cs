using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class JavaScriptDateTimeConverter : DateTimeConverterBase
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			long value2;
			if (value is DateTime)
			{
				value2 = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(((DateTime)value).ToUniversalTime());
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException(JavaScriptDateTimeConverter.getString_0(107321377));
				}
				value2 = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(((DateTimeOffset)value).ToUniversalTime().UtcDateTime);
			}
			writer.WriteStartConstructor(JavaScriptDateTimeConverter.getString_0(107346959));
			writer.WriteValue(value2);
			writer.WriteEndConstructor();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullable(objectType))
				{
					throw JsonSerializationException.Create(reader, JavaScriptDateTimeConverter.getString_0(107290063).FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				if (reader.TokenType != JsonToken.StartConstructor || !string.Equals(reader.Value.ToString(), JavaScriptDateTimeConverter.getString_0(107346959), StringComparison.Ordinal))
				{
					throw JsonSerializationException.Create(reader, JavaScriptDateTimeConverter.getString_0(107321340).FormatWith(CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
				}
				DateTime dateTime;
				string message;
				if (!JavaScriptUtils.TryGetDateFromConstructorJson(reader, out dateTime, out message))
				{
					throw JsonSerializationException.Create(reader, message);
				}
				if ((ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(dateTime);
				}
				return dateTime;
			}
		}

		static JavaScriptDateTimeConverter()
		{
			Strings.CreateGetStringDelegate(typeof(JavaScriptDateTimeConverter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
