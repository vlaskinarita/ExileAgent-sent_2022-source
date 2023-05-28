using System;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class EntityKeyMemberConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			EntityKeyMemberConverter.EnsureReflectionObject(value.GetType());
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			string value2 = (string)EntityKeyMemberConverter._reflectionObject.GetValue(value, EntityKeyMemberConverter.getString_0(107322290));
			object value3 = EntityKeyMemberConverter._reflectionObject.GetValue(value, EntityKeyMemberConverter.getString_0(107322285));
			Type type = (value3 != null) ? value3.GetType() : null;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(EntityKeyMemberConverter.getString_0(107322290)) : EntityKeyMemberConverter.getString_0(107322290));
			writer.WriteValue(value2);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(EntityKeyMemberConverter.getString_0(107254665)) : EntityKeyMemberConverter.getString_0(107254665));
			writer.WriteValue((type != null) ? type.FullName : null);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(EntityKeyMemberConverter.getString_0(107322285)) : EntityKeyMemberConverter.getString_0(107322285));
			if (type != null)
			{
				string value4;
				if (JsonSerializerInternalWriter.TryConvertToString(value3, type, out value4))
				{
					writer.WriteValue(value4);
				}
				else
				{
					writer.WriteValue(value3);
				}
			}
			else
			{
				writer.WriteNull();
			}
			writer.WriteEndObject();
		}

		private static void ReadAndAssertProperty(JsonReader reader, string propertyName)
		{
			reader.ReadAndAssert();
			if (reader.TokenType != JsonToken.PropertyName || !string.Equals(reader.Value.ToString(), propertyName, StringComparison.OrdinalIgnoreCase))
			{
				throw new JsonSerializationException(EntityKeyMemberConverter.getString_0(107322244).FormatWith(CultureInfo.InvariantCulture, propertyName));
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			EntityKeyMemberConverter.EnsureReflectionObject(objectType);
			object obj = EntityKeyMemberConverter._reflectionObject.Creator(new object[0]);
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, EntityKeyMemberConverter.getString_0(107322290));
			reader.ReadAndAssert();
			EntityKeyMemberConverter._reflectionObject.SetValue(obj, EntityKeyMemberConverter.getString_0(107322290), reader.Value.ToString());
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, EntityKeyMemberConverter.getString_0(107254665));
			reader.ReadAndAssert();
			Type type = Type.GetType(reader.Value.ToString());
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, EntityKeyMemberConverter.getString_0(107322285));
			reader.ReadAndAssert();
			EntityKeyMemberConverter._reflectionObject.SetValue(obj, EntityKeyMemberConverter.getString_0(107322285), serializer.Deserialize(reader, type));
			reader.ReadAndAssert();
			return obj;
		}

		private static void EnsureReflectionObject(Type objectType)
		{
			if (EntityKeyMemberConverter._reflectionObject == null)
			{
				EntityKeyMemberConverter._reflectionObject = ReflectionObject.Create(objectType, new string[]
				{
					EntityKeyMemberConverter.getString_0(107322290),
					EntityKeyMemberConverter.getString_0(107322285)
				});
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.AssignableToTypeName(EntityKeyMemberConverter.getString_0(107322235), false);
		}

		static EntityKeyMemberConverter()
		{
			Strings.CreateGetStringDelegate(typeof(EntityKeyMemberConverter));
		}

		private const string EntityKeyMemberFullTypeName = "System.Data.EntityKeyMember";

		private const string KeyPropertyName = "Key";

		private const string TypePropertyName = "Type";

		private const string ValuePropertyName = "Value";

		private static ReflectionObject _reflectionObject;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
