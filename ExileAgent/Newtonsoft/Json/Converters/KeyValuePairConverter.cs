using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class KeyValuePairConverter : JsonConverter
	{
		private static ReflectionObject InitializeReflectionObject(Type t)
		{
			Type[] genericArguments = t.GetGenericArguments();
			Type type = ((IList<Type>)genericArguments)[0];
			Type type2 = ((IList<Type>)genericArguments)[1];
			return ReflectionObject.Create(t, t.GetConstructor(new Type[]
			{
				type,
				type2
			}), new string[]
			{
				KeyValuePairConverter.getString_0(107322301),
				KeyValuePairConverter.getString_0(107322296)
			});
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ReflectionObject reflectionObject = KeyValuePairConverter.ReflectionObjectPerType.Get(value.GetType());
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(KeyValuePairConverter.getString_0(107322301)) : KeyValuePairConverter.getString_0(107322301));
			serializer.Serialize(writer, reflectionObject.GetValue(value, KeyValuePairConverter.getString_0(107322301)), reflectionObject.GetType(KeyValuePairConverter.getString_0(107322301)));
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(KeyValuePairConverter.getString_0(107322296)) : KeyValuePairConverter.getString_0(107322296));
			serializer.Serialize(writer, reflectionObject.GetValue(value, KeyValuePairConverter.getString_0(107322296)), reflectionObject.GetType(KeyValuePairConverter.getString_0(107322296)));
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				object obj = null;
				object obj2 = null;
				reader.ReadAndAssert();
				Type key = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
				ReflectionObject reflectionObject = KeyValuePairConverter.ReflectionObjectPerType.Get(key);
				JsonContract jsonContract = serializer.ContractResolver.ResolveContract(reflectionObject.GetType(KeyValuePairConverter.getString_0(107322301)));
				JsonContract jsonContract2 = serializer.ContractResolver.ResolveContract(reflectionObject.GetType(KeyValuePairConverter.getString_0(107322296)));
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string a = reader.Value.ToString();
					if (string.Equals(a, KeyValuePairConverter.getString_0(107322301), StringComparison.OrdinalIgnoreCase))
					{
						reader.ReadForTypeAndAssert(jsonContract, false);
						obj = serializer.Deserialize(reader, jsonContract.UnderlyingType);
					}
					else if (string.Equals(a, KeyValuePairConverter.getString_0(107322296), StringComparison.OrdinalIgnoreCase))
					{
						reader.ReadForTypeAndAssert(jsonContract2, false);
						obj2 = serializer.Deserialize(reader, jsonContract2.UnderlyingType);
					}
					else
					{
						reader.Skip();
					}
					reader.ReadAndAssert();
				}
				return reflectionObject.Creator(new object[]
				{
					obj,
					obj2
				});
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, KeyValuePairConverter.getString_0(107321763));
			}
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			Type type = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
			return type.IsValueType() && type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static KeyValuePairConverter()
		{
			Strings.CreateGetStringDelegate(typeof(KeyValuePairConverter));
			KeyValuePairConverter.ReflectionObjectPerType = new ThreadSafeStore<Type, ReflectionObject>(new Func<Type, ReflectionObject>(KeyValuePairConverter.InitializeReflectionObject));
		}

		private const string KeyName = "Key";

		private const string ValueName = "Value";

		private static readonly ThreadSafeStore<Type, ReflectionObject> ReflectionObjectPerType;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
