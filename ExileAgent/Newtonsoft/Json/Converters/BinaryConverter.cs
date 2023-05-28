using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class BinaryConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			byte[] byteArray = this.GetByteArray(value);
			writer.WriteValue(byteArray);
		}

		private byte[] GetByteArray(object value)
		{
			if (value.GetType().FullName == BinaryConverter.getString_0(107290176))
			{
				BinaryConverter.EnsureReflectionObject(value.GetType());
				return (byte[])BinaryConverter._reflectionObject.GetValue(value, BinaryConverter.getString_0(107290143));
			}
			if (!(value is SqlBinary))
			{
				throw new JsonSerializationException(BinaryConverter.getString_0(107290098).FormatWith(CultureInfo.InvariantCulture, value.GetType()));
			}
			return ((SqlBinary)value).Value;
		}

		private static void EnsureReflectionObject(Type t)
		{
			if (BinaryConverter._reflectionObject == null)
			{
				BinaryConverter._reflectionObject = ReflectionObject.Create(t, t.GetConstructor(new Type[]
				{
					typeof(byte[])
				}), new string[]
				{
					BinaryConverter.getString_0(107290143)
				});
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullable(objectType))
				{
					throw JsonSerializationException.Create(reader, BinaryConverter.getString_0(107290033).FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				byte[] array;
				if (reader.TokenType == JsonToken.StartArray)
				{
					array = this.ReadByteArray(reader);
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, BinaryConverter.getString_0(107290532).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					array = Convert.FromBase64String(reader.Value.ToString());
				}
				Type type = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
				if (type.FullName == BinaryConverter.getString_0(107290176))
				{
					BinaryConverter.EnsureReflectionObject(type);
					return BinaryConverter._reflectionObject.Creator(new object[]
					{
						array
					});
				}
				if (!(type == typeof(SqlBinary)))
				{
					throw JsonSerializationException.Create(reader, BinaryConverter.getString_0(107290435).FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return new SqlBinary(array);
			}
		}

		private byte[] ReadByteArray(JsonReader reader)
		{
			List<byte> list = new List<byte>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType != JsonToken.Integer)
					{
						if (tokenType != JsonToken.EndArray)
						{
							throw JsonSerializationException.Create(reader, BinaryConverter.getString_0(107290370).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
						}
						return list.ToArray();
					}
					else
					{
						list.Add(Convert.ToByte(reader.Value, CultureInfo.InvariantCulture));
					}
				}
			}
			throw JsonSerializationException.Create(reader, BinaryConverter.getString_0(107352524));
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.FullName == BinaryConverter.getString_0(107290176) || objectType == typeof(SqlBinary) || objectType == typeof(SqlBinary?);
		}

		static BinaryConverter()
		{
			Strings.CreateGetStringDelegate(typeof(BinaryConverter));
		}

		private const string BinaryTypeName = "System.Data.Linq.Binary";

		private const string BinaryToArrayName = "ToArray";

		private static ReflectionObject _reflectionObject;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
