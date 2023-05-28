using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class VersionConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if (!(value is Version))
			{
				throw new JsonSerializationException(VersionConverter.getString_0(107320412));
			}
			writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				try
				{
					return new Version((string)reader.Value);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, VersionConverter.getString_0(107320371).FormatWith(CultureInfo.InvariantCulture, reader.Value), ex);
				}
			}
			throw JsonSerializationException.Create(reader, VersionConverter.getString_0(107320358).FormatWith(CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Version);
		}

		static VersionConverter()
		{
			Strings.CreateGetStringDelegate(typeof(VersionConverter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
