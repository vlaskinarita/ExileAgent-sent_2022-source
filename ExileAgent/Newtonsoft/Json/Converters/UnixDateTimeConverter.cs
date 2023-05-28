using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class UnixDateTimeConverter : DateTimeConverterBase
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			long num;
			if (value is DateTime)
			{
				num = (long)(((DateTime)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException(UnixDateTimeConverter.getString_0(107321389));
				}
				num = (long)(((DateTimeOffset)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			if (num < 0L)
			{
				throw new JsonSerializationException(UnixDateTimeConverter.getString_0(107320791));
			}
			writer.WriteValue(num);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullable(objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!flag)
				{
					throw JsonSerializationException.Create(reader, UnixDateTimeConverter.getString_0(107290075).FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				long num;
				if (reader.TokenType == JsonToken.Integer)
				{
					num = (long)reader.Value;
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, UnixDateTimeConverter.getString_0(107321137).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					if (!long.TryParse((string)reader.Value, out num))
					{
						throw JsonSerializationException.Create(reader, UnixDateTimeConverter.getString_0(107321218).FormatWith(CultureInfo.InvariantCulture, objectType));
					}
				}
				if (num < 0L)
				{
					throw JsonSerializationException.Create(reader, UnixDateTimeConverter.getString_0(107321044).FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				DateTime dateTime = UnixDateTimeConverter.UnixEpoch.AddSeconds((double)num);
				if ((flag ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(dateTime, TimeSpan.Zero);
				}
				return dateTime;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static UnixDateTimeConverter()
		{
			Strings.CreateGetStringDelegate(typeof(UnixDateTimeConverter));
			UnixDateTimeConverter.UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}

		internal static readonly DateTime UnixEpoch;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
