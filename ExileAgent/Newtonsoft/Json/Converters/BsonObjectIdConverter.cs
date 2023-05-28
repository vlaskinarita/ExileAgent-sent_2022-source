using System;
using System.Globalization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public sealed class BsonObjectIdConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			BsonObjectId bsonObjectId = (BsonObjectId)value;
			BsonWriter bsonWriter;
			if ((bsonWriter = (writer as BsonWriter)) != null)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
				return;
			}
			writer.WriteValue(bsonObjectId.Value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Bytes)
			{
				throw new JsonSerializationException(BsonObjectIdConverter.getString_0(107290282).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			return new BsonObjectId((byte[])reader.Value);
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BsonObjectId);
		}

		static BsonObjectIdConverter()
		{
			Strings.CreateGetStringDelegate(typeof(BsonObjectIdConverter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
