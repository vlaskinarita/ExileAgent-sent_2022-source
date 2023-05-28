using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal sealed class JsonSerializerInternalReader : JsonSerializerInternalBase
	{
		public JsonSerializerInternalReader(JsonSerializer serializer) : base(serializer)
		{
		}

		public void Populate(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(target, JsonSerializerInternalReader.getString_1(107350535));
			Type type = target.GetType();
			JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(type);
			if (!reader.MoveToContent())
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107338300));
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				if (jsonContract.ContractType == JsonContractType.Array)
				{
					JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
					IList list;
					if (!jsonArrayContract.ShouldCreateWrapper)
					{
						list = (IList)target;
					}
					else
					{
						IList list2 = jsonArrayContract.CreateWrapper(target);
						list = list2;
					}
					this.PopulateList(list, reader, jsonArrayContract, null, null);
					return;
				}
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304987).FormatWith(CultureInfo.InvariantCulture, type));
			}
			else
			{
				if (reader.TokenType != JsonToken.StartObject)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304860).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
				}
				reader.ReadAndAssert();
				string id = null;
				if (this.Serializer.MetadataPropertyHandling != MetadataPropertyHandling.Ignore && reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), JsonSerializerInternalReader.getString_1(107304894), StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					object value = reader.Value;
					id = ((value != null) ? value.ToString() : null);
					reader.ReadAndAssert();
				}
				if (jsonContract.ContractType == JsonContractType.Dictionary)
				{
					JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
					IDictionary dictionary;
					if (!jsonDictionaryContract.ShouldCreateWrapper)
					{
						dictionary = (IDictionary)target;
					}
					else
					{
						IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(target);
						dictionary = dictionary2;
					}
					this.PopulateDictionary(dictionary, reader, jsonDictionaryContract, null, id);
					return;
				}
				if (jsonContract.ContractType == JsonContractType.Object)
				{
					this.PopulateObject(target, reader, (JsonObjectContract)jsonContract, null, id);
					return;
				}
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304921).FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		private JsonContract GetContractSafe(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return this.Serializer._contractResolver.ResolveContract(type);
		}

		public object Deserialize(JsonReader reader, Type objectType, bool checkAdditionalContent)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(JsonSerializerInternalReader.getString_1(107247429));
			}
			JsonContract contractSafe = this.GetContractSafe(objectType);
			object result;
			try
			{
				JsonConverter converter = this.GetConverter(contractSafe, null, null, null);
				if (reader.TokenType == JsonToken.None && !reader.ReadForType(contractSafe, converter != null))
				{
					if (contractSafe != null && !contractSafe.IsNullable)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107305223).FormatWith(CultureInfo.InvariantCulture, contractSafe.UnderlyingType));
					}
					result = null;
				}
				else
				{
					object obj;
					if (converter != null && converter.CanRead)
					{
						obj = this.DeserializeConvertable(converter, reader, objectType, null);
					}
					else
					{
						obj = this.CreateValueInternal(reader, objectType, contractSafe, null, null, null, null);
					}
					if (checkAdditionalContent)
					{
						while (reader.Read())
						{
							if (reader.TokenType != JsonToken.Comment)
							{
								throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107353231));
							}
						}
					}
					result = obj;
				}
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(null, contractSafe, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					base.ClearErrorContext();
					throw;
				}
				this.HandleError(reader, false, 0);
				result = null;
			}
			return result;
		}

		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this.InternalSerializer == null)
			{
				this.InternalSerializer = new JsonSerializerProxy(this);
			}
			return this.InternalSerializer;
		}

		private JToken CreateJToken(JsonReader reader, JsonContract contract)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonSerializerInternalReader.getString_1(107247429));
			if (contract != null)
			{
				if (contract.UnderlyingType == typeof(JRaw))
				{
					return JRaw.Create(reader);
				}
				if (reader.TokenType == JsonToken.Null && !(contract.UnderlyingType == typeof(JValue)) && !(contract.UnderlyingType == typeof(JToken)))
				{
					return null;
				}
			}
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteToken(reader);
				token = jtokenWriter.Token;
			}
			return token;
		}

		private JToken CreateJObject(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonSerializerInternalReader.getString_1(107247429));
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteStartObject();
				for (;;)
				{
					if (reader.TokenType == JsonToken.PropertyName)
					{
						string text = (string)reader.Value;
						if (!reader.ReadAndMoveToContent())
						{
							goto IL_74;
						}
						if (!this.CheckPropertyName(reader, text))
						{
							jtokenWriter.WritePropertyName(text);
							jtokenWriter.WriteToken(reader, true, true, false);
						}
					}
					else if (reader.TokenType != JsonToken.Comment)
					{
						break;
					}
					if (!reader.Read())
					{
						goto Block_4;
					}
				}
				jtokenWriter.WriteEndObject();
				return jtokenWriter.Token;
				Block_4:
				IL_74:
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107305150));
			}
			JToken result;
			return result;
		}

		private object CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			if (contract != null && contract.ContractType == JsonContractType.Linq)
			{
				return this.CreateJToken(reader, contract);
			}
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.StartObject:
					goto IL_78;
				case JsonToken.StartArray:
					goto IL_8A;
				case JsonToken.StartConstructor:
					goto IL_99;
				case JsonToken.Comment:
					if (reader.Read())
					{
						continue;
					}
					goto IL_B5;
				case JsonToken.Raw:
					goto IL_CB;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
					goto IL_170;
				case JsonToken.String:
					goto IL_DC;
				case JsonToken.Null:
				case JsonToken.Undefined:
					goto IL_11D;
				}
				break;
			}
			goto IL_14A;
			IL_78:
			return this.CreateObject(reader, objectType, contract, member, containerContract, containerMember, existingValue);
			IL_8A:
			return this.CreateList(reader, objectType, contract, member, existingValue, null);
			IL_99:
			string value = reader.Value.ToString();
			return this.EnsureType(reader, value, CultureInfo.InvariantCulture, contract, objectType);
			IL_B5:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107305150));
			IL_CB:
			return new JRaw((string)reader.Value);
			IL_DC:
			string text = (string)reader.Value;
			if (objectType == typeof(byte[]))
			{
				return Convert.FromBase64String(text);
			}
			if (JsonSerializerInternalReader.CoerceEmptyStringToNull(objectType, contract, text))
			{
				return null;
			}
			return this.EnsureType(reader, text, CultureInfo.InvariantCulture, contract, objectType);
			IL_11D:
			if (objectType == typeof(DBNull))
			{
				return DBNull.Value;
			}
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
			IL_14A:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107305093) + reader.TokenType);
			IL_170:
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
		}

		private static bool CoerceEmptyStringToNull(Type objectType, JsonContract contract, string s)
		{
			return string.IsNullOrEmpty(s) && objectType != null && objectType != typeof(string) && objectType != typeof(object) && contract != null && contract.IsNullable;
		}

		internal string GetExpectedDescription(JsonContract contract)
		{
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			case JsonContractType.Dictionary:
			case JsonContractType.Dynamic:
			case JsonContractType.Serializable:
				return JsonSerializerInternalReader.getString_1(107305032);
			case JsonContractType.Array:
				return JsonSerializerInternalReader.getString_1(107305015);
			case JsonContractType.Primitive:
				return JsonSerializerInternalReader.getString_1(107304466);
			case JsonContractType.String:
				return JsonSerializerInternalReader.getString_1(107304357);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private JsonConverter GetConverter(JsonContract contract, JsonConverter memberConverter, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			JsonConverter result = null;
			if (memberConverter != null)
			{
				result = memberConverter;
			}
			else if (((containerProperty != null) ? containerProperty.ItemConverter : null) != null)
			{
				result = containerProperty.ItemConverter;
			}
			else if (((containerContract != null) ? containerContract.ItemConverter : null) != null)
			{
				result = containerContract.ItemConverter;
			}
			else if (contract != null)
			{
				JsonConverter matchingConverter;
				if (contract.Converter != null)
				{
					result = contract.Converter;
				}
				else if ((matchingConverter = this.Serializer.GetMatchingConverter(contract.UnderlyingType)) != null)
				{
					result = matchingConverter;
				}
				else if (contract.InternalConverter != null)
				{
					result = contract.InternalConverter;
				}
			}
			return result;
		}

		private object CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			Type type = objectType;
			string text;
			if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.Ignore)
			{
				reader.ReadAndAssert();
				text = null;
			}
			else if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.ReadAhead)
			{
				JTokenReader jtokenReader;
				if ((jtokenReader = (reader as JTokenReader)) == null)
				{
					jtokenReader = (JTokenReader)JToken.ReadFrom(reader).CreateReader();
					jtokenReader.Culture = reader.Culture;
					jtokenReader.DateFormatString = reader.DateFormatString;
					jtokenReader.DateParseHandling = reader.DateParseHandling;
					jtokenReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
					jtokenReader.FloatParseHandling = reader.FloatParseHandling;
					jtokenReader.SupportMultipleContent = reader.SupportMultipleContent;
					jtokenReader.ReadAndAssert();
					reader = jtokenReader;
				}
				object result;
				if (this.ReadMetadataPropertiesToken(jtokenReader, ref type, ref contract, member, containerContract, containerMember, existingValue, out result, out text))
				{
					return result;
				}
			}
			else
			{
				reader.ReadAndAssert();
				object result2;
				if (this.ReadMetadataProperties(reader, ref type, ref contract, member, containerContract, containerMember, existingValue, out result2, out text))
				{
					return result2;
				}
			}
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJObject(reader);
			}
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			{
				bool flag = false;
				JsonObjectContract jsonObjectContract = (JsonObjectContract)contract;
				object obj;
				if (existingValue != null && (type == objectType || type.IsAssignableFrom(existingValue.GetType())))
				{
					obj = existingValue;
				}
				else
				{
					obj = this.CreateNewObject(reader, jsonObjectContract, member, containerMember, text, out flag);
				}
				if (flag)
				{
					return obj;
				}
				return this.PopulateObject(obj, reader, jsonObjectContract, member, text);
			}
			case JsonContractType.Primitive:
			{
				JsonPrimitiveContract contract2 = (JsonPrimitiveContract)contract;
				if (this.Serializer.MetadataPropertyHandling != MetadataPropertyHandling.Ignore && reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), JsonSerializerInternalReader.getString_1(107351625), StringComparison.Ordinal))
				{
					reader.ReadAndAssert();
					if (reader.TokenType == JsonToken.StartObject)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304332) + reader.TokenType);
					}
					object result3 = this.CreateValueInternal(reader, type, contract2, member, null, null, existingValue);
					reader.ReadAndAssert();
					return result3;
				}
				break;
			}
			case JsonContractType.Dictionary:
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)contract;
				object result4;
				if (existingValue == null)
				{
					bool flag2;
					IDictionary dictionary = this.CreateNewDictionary(reader, jsonDictionaryContract, out flag2);
					if (flag2)
					{
						if (text != null)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304259).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnSerializingCallbacks.Count > 0)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304657).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (contract.OnErrorCallbacks.Count > 0)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107304479).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
						if (!jsonDictionaryContract.HasParameterizedCreatorInternal)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107303869).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
						}
					}
					this.PopulateDictionary(dictionary, reader, jsonDictionaryContract, member, text);
					if (flag2)
					{
						return (jsonDictionaryContract.OverrideCreator ?? jsonDictionaryContract.ParameterizedCreator)(new object[]
						{
							dictionary
						});
					}
					IWrappedDictionary wrappedDictionary;
					if ((wrappedDictionary = (dictionary as IWrappedDictionary)) != null)
					{
						return wrappedDictionary.UnderlyingDictionary;
					}
					result4 = dictionary;
				}
				else
				{
					IDictionary dictionary2;
					if (!jsonDictionaryContract.ShouldCreateWrapper && existingValue is IDictionary)
					{
						dictionary2 = (IDictionary)existingValue;
					}
					else
					{
						IDictionary dictionary3 = jsonDictionaryContract.CreateWrapper(existingValue);
						dictionary2 = dictionary3;
					}
					result4 = this.PopulateDictionary(dictionary2, reader, jsonDictionaryContract, member, text);
				}
				return result4;
			}
			case JsonContractType.Dynamic:
			{
				JsonDynamicContract contract3 = (JsonDynamicContract)contract;
				return this.CreateDynamic(reader, contract3, member, text);
			}
			case JsonContractType.Serializable:
			{
				JsonISerializableContract contract4 = (JsonISerializableContract)contract;
				return this.CreateISerializable(reader, contract4, member, text);
			}
			}
			string text2 = JsonSerializerInternalReader.getString_1(107303756) + Environment.NewLine + JsonSerializerInternalReader.getString_1(107304074) + Environment.NewLine;
			text2 = text2.FormatWith(CultureInfo.InvariantCulture, type, this.GetExpectedDescription(contract));
			throw JsonSerializationException.Create(reader, text2);
		}

		private bool ReadMetadataPropertiesToken(JTokenReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue, out object newValue, out string id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject jobject = (JObject)reader.CurrentToken;
				JToken jtoken = jobject[JsonSerializerInternalReader.getString_1(107303644)];
				if (jtoken != null)
				{
					if (jtoken.Type != JTokenType.String && jtoken.Type != JTokenType.Null)
					{
						throw JsonSerializationException.Create(jtoken, jtoken.Path, JsonSerializerInternalReader.getString_1(107303635).FormatWith(CultureInfo.InvariantCulture, JsonSerializerInternalReader.getString_1(107303644)), null);
					}
					JToken parent = jtoken.Parent;
					JToken jtoken2 = null;
					if (parent.Next != null)
					{
						jtoken2 = parent.Next;
					}
					else if (parent.Previous != null)
					{
						jtoken2 = parent.Previous;
					}
					string text = (string)jtoken;
					if (text != null)
					{
						if (jtoken2 != null)
						{
							throw JsonSerializationException.Create(jtoken2, jtoken2.Path, JsonSerializerInternalReader.getString_1(107303518).FormatWith(CultureInfo.InvariantCulture, JsonSerializerInternalReader.getString_1(107303644)), null);
						}
						newValue = this.Serializer.GetReferenceResolver().ResolveReference(this, text);
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
						{
							this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader, reader.Path, JsonSerializerInternalReader.getString_1(107302860).FormatWith(CultureInfo.InvariantCulture, text, newValue.GetType())), null);
						}
						reader.Skip();
						return true;
					}
				}
				JToken jtoken3 = jobject[JsonSerializerInternalReader.getString_1(107351655)];
				if (jtoken3 != null)
				{
					string qualifiedTypeName = (string)jtoken3;
					JsonReader jsonReader = jtoken3.CreateReader();
					jsonReader.ReadAndAssert();
					this.ResolveTypeName(jsonReader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
					if (jobject[JsonSerializerInternalReader.getString_1(107351625)] != null)
					{
						for (;;)
						{
							reader.ReadAndAssert();
							if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == JsonSerializerInternalReader.getString_1(107351625))
							{
								break;
							}
							reader.ReadAndAssert();
							reader.Skip();
						}
						return false;
					}
				}
				JToken jtoken4 = jobject[JsonSerializerInternalReader.getString_1(107304894)];
				if (jtoken4 != null)
				{
					id = (string)jtoken4;
				}
				JToken jtoken5 = jobject[JsonSerializerInternalReader.getString_1(107302839)];
				if (jtoken5 != null)
				{
					JsonReader jsonReader2 = jtoken5.CreateReader();
					jsonReader2.ReadAndAssert();
					newValue = this.CreateList(jsonReader2, objectType, contract, member, existingValue, id);
					reader.Skip();
					return true;
				}
			}
			reader.ReadAndAssert();
			return false;
		}

		private bool ReadMetadataProperties(JsonReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue, out object newValue, out string id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (text.Length > 0 && text[0] == '$')
				{
					string text2;
					do
					{
						text = reader.Value.ToString();
						bool flag;
						if (string.Equals(text, JsonSerializerInternalReader.getString_1(107303644), StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
							{
								goto Block_11;
							}
							object value = reader.Value;
							text2 = ((value != null) ? value.ToString() : null);
							reader.ReadAndAssert();
							if (text2 != null)
							{
								goto IL_185;
							}
							flag = true;
						}
						else if (string.Equals(text, JsonSerializerInternalReader.getString_1(107351655), StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							string qualifiedTypeName = reader.Value.ToString();
							this.ResolveTypeName(reader, ref objectType, ref contract, member, containerContract, containerMember, qualifiedTypeName);
							reader.ReadAndAssert();
							flag = true;
						}
						else if (string.Equals(text, JsonSerializerInternalReader.getString_1(107304894), StringComparison.Ordinal))
						{
							reader.ReadAndAssert();
							object value2 = reader.Value;
							id = ((value2 != null) ? value2.ToString() : null);
							reader.ReadAndAssert();
							flag = true;
						}
						else
						{
							if (string.Equals(text, JsonSerializerInternalReader.getString_1(107302839), StringComparison.Ordinal))
							{
								goto IL_22A;
							}
							flag = false;
						}
						if (!flag)
						{
							break;
						}
					}
					while (reader.TokenType == JsonToken.PropertyName);
					return false;
					Block_11:
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107303635).FormatWith(CultureInfo.InvariantCulture, JsonSerializerInternalReader.getString_1(107303644)));
					IL_185:
					if (reader.TokenType == JsonToken.PropertyName)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107303518).FormatWith(CultureInfo.InvariantCulture, JsonSerializerInternalReader.getString_1(107303644)));
					}
					newValue = this.Serializer.GetReferenceResolver().ResolveReference(this, text2);
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
					{
						this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107302860).FormatWith(CultureInfo.InvariantCulture, text2, newValue.GetType())), null);
					}
					return true;
					IL_22A:
					reader.ReadAndAssert();
					object obj = this.CreateList(reader, objectType, contract, member, existingValue, id);
					reader.ReadAndAssert();
					newValue = obj;
					return true;
				}
			}
			return false;
		}

		private void ResolveTypeName(JsonReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, string qualifiedTypeName)
		{
			if ((((member != null) ? member.TypeNameHandling : null) ?? (((containerContract != null) ? containerContract.ItemTypeNameHandling : null) ?? (((containerMember != null) ? containerMember.ItemTypeNameHandling : null) ?? this.Serializer._typeNameHandling))) != TypeNameHandling.None)
			{
				StructMultiKey<string, string> structMultiKey = ReflectionUtils.SplitFullyQualifiedTypeName(qualifiedTypeName);
				Type type;
				try
				{
					type = this.Serializer._serializationBinder.BindToType(structMultiKey.Value1, structMultiKey.Value2);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107302794).FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName), ex);
				}
				if (type == null)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107302733).FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName));
				}
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
				{
					this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107303180).FormatWith(CultureInfo.InvariantCulture, qualifiedTypeName, type)), null);
				}
				if (objectType != null && objectType != typeof(IDynamicMetaObjectProvider) && !objectType.IsAssignableFrom(type))
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107303143).FormatWith(CultureInfo.InvariantCulture, type.AssemblyQualifiedName, objectType.AssemblyQualifiedName));
				}
				objectType = type;
				contract = this.GetContractSafe(type);
			}
		}

		private JsonArrayContract EnsureArrayContract(JsonReader reader, Type objectType, JsonContract contract)
		{
			if (contract == null)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107303094).FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			JsonArrayContract jsonArrayContract = contract as JsonArrayContract;
			if (jsonArrayContract == null)
			{
				string text = JsonSerializerInternalReader.getString_1(107303029) + Environment.NewLine + JsonSerializerInternalReader.getString_1(107302307) + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, objectType, this.GetExpectedDescription(contract));
				throw JsonSerializationException.Create(reader, text);
			}
			return jsonArrayContract;
		}

		private object CreateList(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue, string id)
		{
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJToken(reader, contract);
			}
			JsonArrayContract jsonArrayContract = this.EnsureArrayContract(reader, objectType, contract);
			object result;
			if (existingValue == null)
			{
				bool flag;
				IList list = this.CreateNewList(reader, jsonArrayContract, out flag);
				if (flag)
				{
					if (id != null)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301909).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnSerializingCallbacks.Count > 0)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301735).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnErrorCallbacks.Count > 0)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107302133).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (!jsonArrayContract.HasParameterizedCreatorInternal && !jsonArrayContract.IsArray)
					{
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301963).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
				}
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.PopulateList(list, reader, jsonArrayContract, member, id);
				}
				else
				{
					this.PopulateMultidimensionalArray(list, reader, jsonArrayContract, member, id);
				}
				IWrappedCollection wrappedCollection;
				if (flag)
				{
					if (jsonArrayContract.IsMultidimensionalArray)
					{
						list = CollectionUtils.ToMultidimensionalArray(list, jsonArrayContract.CollectionItemType, contract.CreatedType.GetArrayRank());
					}
					else
					{
						if (!jsonArrayContract.IsArray)
						{
							return (jsonArrayContract.OverrideCreator ?? jsonArrayContract.ParameterizedCreator)(new object[]
							{
								list
							});
						}
						Array array = Array.CreateInstance(jsonArrayContract.CollectionItemType, list.Count);
						list.CopyTo(array, 0);
						list = array;
					}
				}
				else if ((wrappedCollection = (list as IWrappedCollection)) != null)
				{
					return wrappedCollection.UnderlyingCollection;
				}
				result = list;
			}
			else
			{
				if (!jsonArrayContract.CanDeserialize)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301378).FormatWith(CultureInfo.InvariantCulture, contract.CreatedType));
				}
				IList list2;
				IList list3;
				if (!jsonArrayContract.ShouldCreateWrapper && (list2 = (existingValue as IList)) != null)
				{
					list3 = list2;
				}
				else
				{
					IList list4 = jsonArrayContract.CreateWrapper(existingValue);
					list3 = list4;
				}
				result = this.PopulateList(list3, reader, jsonArrayContract, member, id);
			}
			return result;
		}

		private bool HasNoDefinedType(JsonContract contract)
		{
			return contract == null || contract.UnderlyingType == typeof(object) || contract.ContractType == JsonContractType.Linq || contract.UnderlyingType == typeof(IDynamicMetaObjectProvider);
		}

		private object EnsureType(JsonReader reader, object value, CultureInfo culture, JsonContract contract, Type targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			if (!(ReflectionUtils.GetObjectType(value) != targetType))
			{
				return value;
			}
			if (value == null && contract.IsNullable)
			{
				return null;
			}
			object result;
			try
			{
				if (contract.IsConvertable)
				{
					JsonPrimitiveContract jsonPrimitiveContract = (JsonPrimitiveContract)contract;
					string s;
					DateTime value3;
					if (contract.IsEnum)
					{
						string value2;
						if ((value2 = (value as string)) != null)
						{
							return EnumUtils.ParseEnum(contract.NonNullableUnderlyingType, null, value2, false);
						}
						if (ConvertUtils.IsInteger(jsonPrimitiveContract.TypeCode))
						{
							return Enum.ToObject(contract.NonNullableUnderlyingType, value);
						}
					}
					else if (contract.NonNullableUnderlyingType == typeof(DateTime) && (s = (value as string)) != null && DateTimeUtils.TryParseDateTime(s, reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out value3))
					{
						return DateTimeUtils.EnsureDateTime(value3, reader.DateTimeZoneHandling);
					}
					if (value is BigInteger)
					{
						BigInteger i = (BigInteger)value;
						result = ConvertUtils.FromBigInteger(i, contract.NonNullableUnderlyingType);
					}
					else
					{
						result = Convert.ChangeType(value, contract.NonNullableUnderlyingType, culture);
					}
				}
				else
				{
					result = ConvertUtils.ConvertOrCast(value, culture, contract.NonNullableUnderlyingType);
				}
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301369).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(value), targetType), ex);
			}
			return result;
		}

		private bool SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, object target)
		{
			bool flag;
			object value;
			JsonContract contract;
			bool flag2;
			bool result;
			if (this.CalculatePropertyDetails(property, ref propertyConverter, containerContract, containerProperty, reader, target, out flag, out value, out contract, out flag2, out result))
			{
				return result;
			}
			object obj;
			if (propertyConverter != null && propertyConverter.CanRead)
			{
				if (!flag2 && target != null && property.Readable)
				{
					value = property.ValueProvider.GetValue(target);
				}
				obj = this.DeserializeConvertable(propertyConverter, reader, property.PropertyType, value);
			}
			else
			{
				obj = this.CreateValueInternal(reader, property.PropertyType, contract, property, containerContract, containerProperty, flag ? value : null);
			}
			if ((!flag || obj != value) && this.ShouldSetPropertyValue(property, containerContract as JsonObjectContract, obj))
			{
				property.ValueProvider.SetValue(target, obj);
				if (property.SetIsSpecified != null)
				{
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
					{
						this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107301280).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
					}
					property.SetIsSpecified(target, true);
				}
				return true;
			}
			return flag;
		}

		private bool CalculatePropertyDetails(JsonProperty property, ref JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, object target, out bool useExistingValue, out object currentValue, out JsonContract propertyContract, out bool gottenCurrentValue, out bool ignoredValue)
		{
			currentValue = null;
			useExistingValue = false;
			propertyContract = null;
			gottenCurrentValue = false;
			ignoredValue = false;
			if (property.Ignored)
			{
				return true;
			}
			JsonToken tokenType = reader.TokenType;
			if (property.PropertyContract == null)
			{
				property.PropertyContract = this.GetContractSafe(property.PropertyType);
			}
			if (property.ObjectCreationHandling.GetValueOrDefault(this.Serializer._objectCreationHandling) != ObjectCreationHandling.Replace && (tokenType == JsonToken.StartArray || tokenType == JsonToken.StartObject || propertyConverter != null) && property.Readable)
			{
				currentValue = property.ValueProvider.GetValue(target);
				gottenCurrentValue = true;
				if (currentValue != null)
				{
					propertyContract = this.GetContractSafe(currentValue.GetType());
					useExistingValue = (!propertyContract.IsReadOnlyOrFixedSize && !propertyContract.UnderlyingType.IsValueType());
				}
			}
			if (!property.Writable && !useExistingValue)
			{
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
				{
					this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107301243).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
				}
				return true;
			}
			if (tokenType == JsonToken.Null && base.ResolvedNullValueHandling(containerContract as JsonObjectContract, property) == NullValueHandling.Ignore)
			{
				ignoredValue = true;
				return true;
			}
			if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) && !this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && JsonTokenUtils.IsPrimitiveToken(tokenType) && MiscellaneousUtils.ValueEquals(reader.Value, property.GetResolvedDefaultValue()))
			{
				ignoredValue = true;
				return true;
			}
			if (currentValue == null)
			{
				propertyContract = property.PropertyContract;
			}
			else
			{
				propertyContract = this.GetContractSafe(currentValue.GetType());
				if (propertyContract != property.PropertyContract)
				{
					propertyConverter = this.GetConverter(propertyContract, property.Converter, containerContract, containerProperty);
				}
			}
			return false;
		}

		private void AddReference(JsonReader reader, string id, object value)
		{
			try
			{
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
				{
					this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107301634).FormatWith(CultureInfo.InvariantCulture, id, value.GetType())), null);
				}
				this.Serializer.GetReferenceResolver().AddReference(this, id, value);
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301581).FormatWith(CultureInfo.InvariantCulture, id), ex);
			}
		}

		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool ShouldSetPropertyValue(JsonProperty property, JsonObjectContract contract, object value)
		{
			return (value != null || base.ResolvedNullValueHandling(contract, property) != NullValueHandling.Ignore) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) || this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) || !MiscellaneousUtils.ValueEquals(value, property.GetResolvedDefaultValue())) && property.Writable;
		}

		private IList CreateNewList(JsonReader reader, JsonArrayContract contract, out bool createdFromNonDefaultCreator)
		{
			if (!contract.CanDeserialize)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301560).FormatWith(CultureInfo.InvariantCulture, contract.CreatedType));
			}
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryCollection();
				}
				object obj = contract.OverrideCreator(new object[0]);
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultCreator = false;
				return (IList)obj;
			}
			else
			{
				if (contract.IsReadOnlyOrFixedSize)
				{
					createdFromNonDefaultCreator = true;
					IList list = contract.CreateTemporaryCollection();
					if (contract.ShouldCreateWrapper)
					{
						list = contract.CreateWrapper(list);
					}
					return list;
				}
				if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
				{
					object obj2 = contract.DefaultCreator();
					if (contract.ShouldCreateWrapper)
					{
						obj2 = contract.CreateWrapper(obj2);
					}
					createdFromNonDefaultCreator = false;
					return (IList)obj2;
				}
				if (contract.HasParameterizedCreatorInternal)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryCollection();
				}
				if (!contract.IsInstantiable)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301471).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300813).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
		}

		private IDictionary CreateNewDictionary(JsonReader reader, JsonDictionaryContract contract, out bool createdFromNonDefaultCreator)
		{
			if (contract.OverrideCreator != null)
			{
				if (contract.HasParameterizedCreator)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				createdFromNonDefaultCreator = false;
				return (IDictionary)contract.OverrideCreator(new object[0]);
			}
			else
			{
				if (contract.IsReadOnlyOrFixedSize)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
				{
					object obj = contract.DefaultCreator();
					if (contract.ShouldCreateWrapper)
					{
						obj = contract.CreateWrapper(obj);
					}
					createdFromNonDefaultCreator = false;
					return (IDictionary)obj;
				}
				if (contract.HasParameterizedCreatorInternal)
				{
					createdFromNonDefaultCreator = true;
					return contract.CreateTemporaryDictionary();
				}
				if (!contract.IsInstantiable)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301471).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300744).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
		}

		private void OnDeserializing(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107300699).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserializing(value, this.Serializer._context);
		}

		private void OnDeserialized(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107300662).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserialized(value, this.Serializer._context);
		}

		private object PopulateDictionary(IDictionary dictionary, JsonReader reader, JsonDictionaryContract contract, JsonProperty containerProperty, string id)
		{
			IWrappedDictionary wrappedDictionary;
			object obj = ((wrappedDictionary = (dictionary as IWrappedDictionary)) != null) ? wrappedDictionary.UnderlyingDictionary : dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.KeyContract == null)
			{
				contract.KeyContract = this.GetContractSafe(contract.DictionaryKeyType);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.GetContractSafe(contract.DictionaryValueType);
			}
			JsonConverter jsonConverter = contract.ItemConverter ?? this.GetConverter(contract.ItemContract, null, contract, containerProperty);
			JsonPrimitiveContract jsonPrimitiveContract;
			PrimitiveTypeCode primitiveTypeCode = ((jsonPrimitiveContract = (contract.KeyContract as JsonPrimitiveContract)) != null) ? jsonPrimitiveContract.TypeCode : PrimitiveTypeCode.Empty;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType == JsonToken.Comment)
					{
						goto IL_26D;
					}
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_11;
					}
					flag = true;
					goto IL_26D;
				}
				else
				{
					object obj2 = reader.Value;
					if (this.CheckPropertyName(reader, obj2.ToString()))
					{
						goto IL_26D;
					}
					try
					{
						try
						{
							if (primitiveTypeCode - PrimitiveTypeCode.DateTime > 1)
							{
								if (primitiveTypeCode - PrimitiveTypeCode.DateTimeOffset > 1)
								{
									obj2 = this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType);
								}
								else
								{
									DateTimeOffset dateTimeOffset;
									obj2 = (DateTimeUtils.TryParseDateTimeOffset(obj2.ToString(), reader.DateFormatString, reader.Culture, out dateTimeOffset) ? dateTimeOffset : this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
								}
							}
							else
							{
								DateTime dateTime;
								obj2 = (DateTimeUtils.TryParseDateTime(obj2.ToString(), reader.DateTimeZoneHandling, reader.DateFormatString, reader.Culture, out dateTime) ? dateTime : this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType));
							}
						}
						catch (Exception ex)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301137).FormatWith(CultureInfo.InvariantCulture, reader.Value, contract.DictionaryKeyType), ex);
						}
						if (!reader.ReadForType(contract.ItemContract, jsonConverter != null))
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107305150));
						}
						object value;
						if (jsonConverter != null && jsonConverter.CanRead)
						{
							value = this.DeserializeConvertable(jsonConverter, reader, contract.DictionaryValueType, null);
						}
						else
						{
							value = this.CreateValueInternal(reader, contract.DictionaryValueType, contract.ItemContract, null, contract, containerProperty, null);
						}
						dictionary[obj2] = value;
						goto IL_26D;
					}
					catch (Exception ex2)
					{
						if (!base.IsErrorHandled(obj, contract, obj2, reader as IJsonLineInfo, reader.Path, ex2))
						{
							throw;
						}
						this.HandleError(reader, true, depth);
						goto IL_26D;
					}
				}
				IL_250:
				if (!reader.Read())
				{
					break;
				}
				continue;
				IL_26D:
				if (!flag)
				{
					goto IL_250;
				}
				break;
			}
			goto IL_299;
			Block_11:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300955) + reader.TokenType);
			IL_299:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, JsonSerializerInternalReader.getString_1(107305150));
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object PopulateMultidimensionalArray(IList list, JsonReader reader, JsonArrayContract contract, JsonProperty containerProperty, string id)
		{
			int arrayRank = contract.UnderlyingType.GetArrayRank();
			if (id != null)
			{
				this.AddReference(reader, id, list);
			}
			this.OnDeserializing(reader, contract, list);
			JsonContract contractSafe = this.GetContractSafe(contract.CollectionItemType);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, containerProperty);
			int? num = null;
			Stack<IList> stack = new Stack<IList>();
			stack.Push(list);
			IList list2 = list;
			bool flag = false;
			for (;;)
			{
				int depth = reader.Depth;
				if (stack.Count == arrayRank)
				{
					try
					{
						if (!reader.ReadForType(contractSafe, converter != null))
						{
							goto IL_224;
						}
						JsonToken tokenType = reader.TokenType;
						if (tokenType != JsonToken.Comment)
						{
							if (tokenType == JsonToken.EndArray)
							{
								stack.Pop();
								list2 = stack.Peek();
								num = null;
							}
							else
							{
								object value;
								if (converter != null && converter.CanRead)
								{
									value = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
								}
								else
								{
									value = this.CreateValueInternal(reader, contract.CollectionItemType, contractSafe, null, contract, containerProperty, null);
								}
								list2.Add(value);
							}
						}
					}
					catch (Exception ex)
					{
						JsonPosition position = reader.GetPosition(depth);
						if (!base.IsErrorHandled(list, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
						{
							throw;
						}
						this.HandleError(reader, true, depth + 1);
						if (num != null)
						{
							int? num2 = num;
							int position2 = position.Position;
							if (num2.GetValueOrDefault() == position2 & num2 != null)
							{
								throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300350), ex);
							}
						}
						num = new int?(position.Position);
					}
				}
				else
				{
					if (!reader.Read())
					{
						goto IL_224;
					}
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.StartArray)
					{
						if (tokenType != JsonToken.Comment)
						{
							if (tokenType != JsonToken.EndArray)
							{
								break;
							}
							stack.Pop();
							if (stack.Count > 0)
							{
								list2 = stack.Peek();
							}
							else
							{
								flag = true;
							}
						}
					}
					else
					{
						IList list3 = new List<object>();
						list2.Add(list3);
						stack.Push(list3);
						list2 = list3;
					}
				}
				if (flag)
				{
					goto Block_9;
				}
			}
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300289) + reader.TokenType);
			Block_9:
			IL_224:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, list, JsonSerializerInternalReader.getString_1(107300240));
			}
			this.OnDeserialized(reader, contract, list);
			return list;
		}

		private void ThrowUnexpectedEndException(JsonReader reader, JsonContract contract, object currentObject, string message)
		{
			try
			{
				throw JsonSerializationException.Create(reader, message);
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(currentObject, contract, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					throw;
				}
				this.HandleError(reader, false, 0);
			}
		}

		private object PopulateList(IList list, JsonReader reader, JsonArrayContract contract, JsonProperty containerProperty, string id)
		{
			IWrappedCollection wrappedCollection;
			object obj = ((wrappedCollection = (list as IWrappedCollection)) != null) ? wrappedCollection.UnderlyingCollection : list;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			if (list.IsFixedSize)
			{
				reader.Skip();
				return obj;
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.GetContractSafe(contract.CollectionItemType);
			}
			JsonConverter converter = this.GetConverter(contract.ItemContract, null, contract, containerProperty);
			int? num = null;
			bool flag = false;
			do
			{
				try
				{
					if (!reader.ReadForType(contract.ItemContract, converter != null))
					{
						break;
					}
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndArray)
						{
							flag = true;
						}
						else
						{
							object value;
							if (converter != null && converter.CanRead)
							{
								value = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
							}
							else
							{
								value = this.CreateValueInternal(reader, contract.CollectionItemType, contract.ItemContract, null, contract, containerProperty, null);
							}
							list.Add(value);
						}
					}
				}
				catch (Exception ex)
				{
					JsonPosition position = reader.GetPosition(depth);
					if (!base.IsErrorHandled(obj, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
					{
						throw;
					}
					this.HandleError(reader, true, depth + 1);
					if (num != null)
					{
						int? num2 = num;
						int position2 = position.Position;
						if (num2.GetValueOrDefault() == position2 & num2 != null)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300350), ex);
						}
					}
					num = new int?(position.Position);
				}
			}
			while (!flag);
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, JsonSerializerInternalReader.getString_1(107300240));
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object CreateISerializable(JsonReader reader, JsonISerializableContract contract, JsonProperty member, string id)
		{
			Type underlyingType = contract.UnderlyingType;
			if (!JsonTypeReflector.FullyTrusted)
			{
				string text = JsonSerializerInternalReader.getString_1(107300183) + Environment.NewLine + JsonSerializerInternalReader.getString_1(107300437) + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, underlyingType);
				throw JsonSerializationException.Create(reader, text);
			}
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107300011).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, new JsonFormatterConverter(this, contract, member));
			bool flag = false;
			string text2;
			do
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							goto Block_8;
						}
						flag = true;
					}
				}
				else
				{
					text2 = reader.Value.ToString();
					if (!reader.Read())
					{
						goto IL_136;
					}
					serializationInfo.AddValue(text2, JToken.ReadFrom(reader));
				}
				if (flag)
				{
					break;
				}
			}
			while (reader.Read());
			goto IL_158;
			Block_8:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300955) + reader.TokenType);
			IL_136:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299942).FormatWith(CultureInfo.InvariantCulture, text2));
			IL_158:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, serializationInfo, JsonSerializerInternalReader.getString_1(107305150));
			}
			if (!contract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301471).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.ISerializableCreator == null)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299885).FormatWith(CultureInfo.InvariantCulture, underlyingType));
			}
			object obj = contract.ISerializableCreator(new object[]
			{
				serializationInfo,
				this.Serializer._context
			});
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		internal object CreateISerializableItem(JToken token, Type type, JsonISerializableContract contract, JsonProperty member)
		{
			JsonContract contractSafe = this.GetContractSafe(type);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, member);
			JsonReader jsonReader = token.CreateReader();
			jsonReader.ReadAndAssert();
			object result;
			if (converter != null && converter.CanRead)
			{
				result = this.DeserializeConvertable(converter, jsonReader, type, null);
			}
			else
			{
				result = this.CreateValueInternal(jsonReader, type, contractSafe, null, contract, member, null);
			}
			return result;
		}

		private object CreateDynamic(JsonReader reader, JsonDynamicContract contract, JsonProperty member, string id)
		{
			if (!contract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301471).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				IDynamicMetaObjectProvider dynamicMetaObjectProvider = (IDynamicMetaObjectProvider)contract.DefaultCreator();
				if (id != null)
				{
					this.AddReference(reader, id, dynamicMetaObjectProvider);
				}
				this.OnDeserializing(reader, contract, dynamicMetaObjectProvider);
				int depth = reader.Depth;
				bool flag = false;
				for (;;)
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.PropertyName)
					{
						if (tokenType != JsonToken.EndObject)
						{
							goto Block_8;
						}
						flag = true;
						goto IL_1EF;
					}
					else
					{
						string text = reader.Value.ToString();
						try
						{
							if (!reader.Read())
							{
								throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299942).FormatWith(CultureInfo.InvariantCulture, text));
							}
							JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
							if (closestMatchProperty != null && closestMatchProperty.Writable && !closestMatchProperty.Ignored)
							{
								if (closestMatchProperty.PropertyContract == null)
								{
									closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
								}
								JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, null, null);
								if (!this.SetPropertyValue(closestMatchProperty, converter, null, member, reader, dynamicMetaObjectProvider))
								{
									reader.Skip();
								}
							}
							else
							{
								Type type = JsonTokenUtils.IsPrimitiveToken(reader.TokenType) ? reader.ValueType : typeof(IDynamicMetaObjectProvider);
								JsonContract contractSafe = this.GetContractSafe(type);
								JsonConverter converter2 = this.GetConverter(contractSafe, null, null, member);
								object value;
								if (converter2 != null && converter2.CanRead)
								{
									value = this.DeserializeConvertable(converter2, reader, type, null);
								}
								else
								{
									value = this.CreateValueInternal(reader, type, contractSafe, null, null, member, null);
								}
								contract.TrySetMember(dynamicMetaObjectProvider, text, value);
							}
							goto IL_1EF;
						}
						catch (Exception ex)
						{
							if (!base.IsErrorHandled(dynamicMetaObjectProvider, contract, text, reader as IJsonLineInfo, reader.Path, ex))
							{
								throw;
							}
							this.HandleError(reader, true, depth);
							goto IL_1EF;
						}
					}
					IL_1D9:
					if (!reader.Read())
					{
						break;
					}
					continue;
					IL_1EF:
					if (!flag)
					{
						goto IL_1D9;
					}
					break;
				}
				goto IL_21A;
				Block_8:
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300955) + reader.TokenType);
				IL_21A:
				if (!flag)
				{
					this.ThrowUnexpectedEndException(reader, contract, dynamicMetaObjectProvider, JsonSerializerInternalReader.getString_1(107305150));
				}
				this.OnDeserialized(reader, contract, dynamicMetaObjectProvider);
				return dynamicMetaObjectProvider;
			}
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300744).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		private object CreateObjectUsingCreatorWithParameters(JsonReader reader, JsonObjectContract contract, JsonProperty containerProperty, ObjectConstructor<object> creator, string id)
		{
			ValidationUtils.ArgumentNotNull(creator, JsonSerializerInternalReader.getString_1(107340664));
			bool flag = contract.HasRequiredOrDefaultValueProperties || this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Populate);
			Type underlyingType = contract.UnderlyingType;
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				string arg = string.Join(JsonSerializerInternalReader.getString_1(107401725), from p in contract.CreatorParameters
				select p.PropertyName);
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107299115).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType, arg)), null);
			}
			List<JsonSerializerInternalReader.CreatorPropertyContext> list = this.ResolvePropertyAndCreatorValues(contract, containerProperty, reader, underlyingType);
			if (flag)
			{
				using (IEnumerator<JsonProperty> enumerator = contract.Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonProperty property = enumerator.Current;
						if (!property.Ignored && list.All((JsonSerializerInternalReader.CreatorPropertyContext p) => p.Property != property))
						{
							list.Add(new JsonSerializerInternalReader.CreatorPropertyContext
							{
								Property = property,
								Name = property.PropertyName,
								Presence = new JsonSerializerInternalReader.PropertyPresence?(JsonSerializerInternalReader.PropertyPresence.None)
							});
						}
					}
				}
			}
			object[] array = new object[contract.CreatorParameters.Count];
			foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext in list)
			{
				if (flag && creatorPropertyContext.Property != null && creatorPropertyContext.Presence == null)
				{
					object value = creatorPropertyContext.Value;
					JsonSerializerInternalReader.PropertyPresence value2;
					string s;
					if (value == null)
					{
						value2 = JsonSerializerInternalReader.PropertyPresence.Null;
					}
					else if ((s = (value as string)) != null)
					{
						value2 = (JsonSerializerInternalReader.CoerceEmptyStringToNull(creatorPropertyContext.Property.PropertyType, creatorPropertyContext.Property.PropertyContract, s) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
					}
					else
					{
						value2 = JsonSerializerInternalReader.PropertyPresence.Value;
					}
					creatorPropertyContext.Presence = new JsonSerializerInternalReader.PropertyPresence?(value2);
				}
				JsonProperty jsonProperty = creatorPropertyContext.ConstructorProperty;
				if (jsonProperty == null && creatorPropertyContext.Property != null)
				{
					jsonProperty = contract.CreatorParameters.ForgivingCaseSensitiveFind((JsonProperty p) => p.PropertyName, creatorPropertyContext.Property.UnderlyingName);
				}
				if (jsonProperty != null && !jsonProperty.Ignored)
				{
					if (flag)
					{
						JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext.Presence;
						if (!(presence.GetValueOrDefault() == JsonSerializerInternalReader.PropertyPresence.None & presence != null))
						{
							presence = creatorPropertyContext.Presence;
							if (!(presence.GetValueOrDefault() == JsonSerializerInternalReader.PropertyPresence.Null & presence != null))
							{
								goto IL_31C;
							}
						}
						if (jsonProperty.PropertyContract == null)
						{
							jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
						}
						if (this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate))
						{
							creatorPropertyContext.Value = this.EnsureType(reader, jsonProperty.GetResolvedDefaultValue(), CultureInfo.InvariantCulture, jsonProperty.PropertyContract, jsonProperty.PropertyType);
						}
					}
					IL_31C:
					int num = contract.CreatorParameters.IndexOf(jsonProperty);
					array[num] = creatorPropertyContext.Value;
					creatorPropertyContext.Used = true;
				}
			}
			object obj = creator(array);
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext2 in list)
			{
				if (!creatorPropertyContext2.Used && creatorPropertyContext2.Property != null && !creatorPropertyContext2.Property.Ignored)
				{
					JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext2.Presence;
					if (!(presence.GetValueOrDefault() == JsonSerializerInternalReader.PropertyPresence.None & presence != null))
					{
						JsonProperty property2 = creatorPropertyContext2.Property;
						object value3 = creatorPropertyContext2.Value;
						if (this.ShouldSetPropertyValue(property2, contract, value3))
						{
							property2.ValueProvider.SetValue(obj, value3);
							creatorPropertyContext2.Used = true;
						}
						else if (!property2.Writable && value3 != null)
						{
							JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(property2.PropertyType);
							if (jsonContract.ContractType != JsonContractType.Array)
							{
								goto IL_507;
							}
							JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
							if (jsonArrayContract.CanDeserialize && !jsonArrayContract.IsReadOnlyOrFixedSize)
							{
								object value4 = property2.ValueProvider.GetValue(obj);
								if (value4 != null)
								{
									IList list2;
									if (!jsonArrayContract.ShouldCreateWrapper)
									{
										list2 = (IList)value4;
									}
									else
									{
										IList list3 = jsonArrayContract.CreateWrapper(value4);
										list2 = list3;
									}
									IList list4 = list2;
									IEnumerable enumerable;
									if (!jsonArrayContract.ShouldCreateWrapper)
									{
										enumerable = (IList)value3;
									}
									else
									{
										IList list3 = jsonArrayContract.CreateWrapper(value3);
										enumerable = list3;
									}
									using (IEnumerator enumerator3 = enumerable.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											object value5 = enumerator3.Current;
											list4.Add(value5);
										}
										goto IL_5C6;
									}
									goto IL_507;
								}
							}
							IL_5C6:
							creatorPropertyContext2.Used = true;
							continue;
							IL_507:
							if (jsonContract.ContractType != JsonContractType.Dictionary)
							{
								goto IL_5C6;
							}
							JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
							if (jsonDictionaryContract.IsReadOnlyOrFixedSize)
							{
								goto IL_5C6;
							}
							object value6 = property2.ValueProvider.GetValue(obj);
							if (value6 != null)
							{
								IDictionary dictionary;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									dictionary = (IDictionary)value6;
								}
								else
								{
									IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value6);
									dictionary = dictionary2;
								}
								IDictionary dictionary3 = dictionary;
								IDictionary dictionary4;
								if (!jsonDictionaryContract.ShouldCreateWrapper)
								{
									dictionary4 = (IDictionary)value3;
								}
								else
								{
									IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value3);
									dictionary4 = dictionary2;
								}
								using (IDictionaryEnumerator enumerator4 = dictionary4.GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										DictionaryEntry entry = enumerator4.Entry;
										dictionary3[entry.Key] = entry.Value;
									}
								}
								goto IL_5C6;
							}
							goto IL_5C6;
						}
					}
				}
			}
			if (contract.ExtensionDataSetter != null)
			{
				foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext3 in list)
				{
					if (!creatorPropertyContext3.Used)
					{
						JsonSerializerInternalReader.PropertyPresence? presence = creatorPropertyContext3.Presence;
						if (!(presence.GetValueOrDefault() == JsonSerializerInternalReader.PropertyPresence.None & presence != null))
						{
							contract.ExtensionDataSetter(obj, creatorPropertyContext3.Name, creatorPropertyContext3.Value);
						}
					}
				}
			}
			if (flag)
			{
				foreach (JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext4 in list)
				{
					if (creatorPropertyContext4.Property != null)
					{
						this.EndProcessProperty(obj, reader, contract, reader.Depth, creatorPropertyContext4.Property, creatorPropertyContext4.Presence.GetValueOrDefault(), !creatorPropertyContext4.Used);
					}
				}
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		private object DeserializeConvertable(JsonConverter converter, JsonReader reader, Type objectType, object existingValue)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107299554).FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			object result = converter.ReadJson(reader, objectType, existingValue, this.GetInternalSerializer());
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107299493).FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			return result;
		}

		private List<JsonSerializerInternalReader.CreatorPropertyContext> ResolvePropertyAndCreatorValues(JsonObjectContract contract, JsonProperty containerProperty, JsonReader reader, Type objectType)
		{
			List<JsonSerializerInternalReader.CreatorPropertyContext> list = new List<JsonSerializerInternalReader.CreatorPropertyContext>();
			bool flag = false;
			string text;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType != JsonToken.EndObject)
						{
							goto Block_16;
						}
						flag = true;
					}
				}
				else
				{
					text = reader.Value.ToString();
					JsonSerializerInternalReader.CreatorPropertyContext creatorPropertyContext = new JsonSerializerInternalReader.CreatorPropertyContext
					{
						Name = reader.Value.ToString(),
						ConstructorProperty = contract.CreatorParameters.GetClosestMatchProperty(text),
						Property = contract.Properties.GetClosestMatchProperty(text)
					};
					list.Add(creatorPropertyContext);
					JsonProperty jsonProperty = creatorPropertyContext.ConstructorProperty ?? creatorPropertyContext.Property;
					if (jsonProperty != null && !jsonProperty.Ignored)
					{
						if (jsonProperty.PropertyContract == null)
						{
							jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
						}
						JsonConverter converter = this.GetConverter(jsonProperty.PropertyContract, jsonProperty.Converter, contract, containerProperty);
						if (!reader.ReadForType(jsonProperty.PropertyContract, converter != null))
						{
							goto IL_20A;
						}
						if (converter != null && converter.CanRead)
						{
							creatorPropertyContext.Value = this.DeserializeConvertable(converter, reader, jsonProperty.PropertyType, null);
						}
						else
						{
							creatorPropertyContext.Value = this.CreateValueInternal(reader, jsonProperty.PropertyType, jsonProperty.PropertyContract, jsonProperty, contract, containerProperty, null);
						}
					}
					else
					{
						if (!reader.Read())
						{
							goto IL_22B;
						}
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
						{
							this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107299428).FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
						}
						if (this.Serializer._missingMemberHandling == MissingMemberHandling.Error)
						{
							break;
						}
						if (contract.ExtensionDataSetter != null)
						{
							creatorPropertyContext.Value = this.ReadExtensionDataValue(contract, containerProperty, reader);
						}
						else
						{
							reader.Skip();
						}
					}
				}
				if (flag)
				{
					goto IL_274;
				}
				if (!reader.Read())
				{
					goto Block_14;
				}
			}
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299411).FormatWith(CultureInfo.InvariantCulture, text, objectType.Name));
			Block_14:
			goto IL_274;
			Block_16:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300955) + reader.TokenType);
			IL_20A:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299942).FormatWith(CultureInfo.InvariantCulture, text));
			IL_22B:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299942).FormatWith(CultureInfo.InvariantCulture, text));
			IL_274:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, null, JsonSerializerInternalReader.getString_1(107305150));
			}
			return list;
		}

		public object CreateNewObject(JsonReader reader, JsonObjectContract objectContract, JsonProperty containerMember, JsonProperty containerProperty, string id, out bool createdFromNonDefaultCreator)
		{
			object obj = null;
			if (objectContract.OverrideCreator != null)
			{
				if (objectContract.CreatorParameters.Count > 0)
				{
					createdFromNonDefaultCreator = true;
					return this.CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.OverrideCreator, id);
				}
				obj = objectContract.OverrideCreator(CollectionUtils.ArrayEmpty<object>());
			}
			else if (objectContract.DefaultCreator != null && (!objectContract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor || objectContract.ParameterizedCreator == null))
			{
				obj = objectContract.DefaultCreator();
			}
			else if (objectContract.ParameterizedCreator != null)
			{
				createdFromNonDefaultCreator = true;
				return this.CreateObjectUsingCreatorWithParameters(reader, objectContract, containerMember, objectContract.ParameterizedCreator, id);
			}
			if (obj != null)
			{
				createdFromNonDefaultCreator = false;
				return obj;
			}
			if (!objectContract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107301471).FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
			}
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107298830).FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
		}

		private object PopulateObject(object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, string id)
		{
			this.OnDeserializing(reader, contract, newObject);
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary;
			if (!contract.HasRequiredOrDefaultValueProperties && !this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Populate))
			{
				dictionary = null;
			}
			else
			{
				dictionary = contract.Properties.ToDictionary((JsonProperty m) => m, (JsonProperty m) => JsonSerializerInternalReader.PropertyPresence.None);
			}
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary2 = dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, newObject);
			}
			int depth = reader.Depth;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType == JsonToken.Comment)
					{
						goto IL_291;
					}
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_10;
					}
					flag = true;
					goto IL_291;
				}
				else
				{
					string text = reader.Value.ToString();
					if (this.CheckPropertyName(reader, text))
					{
						goto IL_291;
					}
					try
					{
						JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
						if (closestMatchProperty != null)
						{
							if (!closestMatchProperty.Ignored && this.ShouldDeserialize(reader, closestMatchProperty, newObject))
							{
								if (closestMatchProperty.PropertyContract == null)
								{
									closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
								}
								JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.Converter, contract, member);
								if (!reader.ReadForType(closestMatchProperty.PropertyContract, converter != null))
								{
									throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299942).FormatWith(CultureInfo.InvariantCulture, text));
								}
								this.SetPropertyPresence(reader, closestMatchProperty, dictionary2);
								if (!this.SetPropertyValue(closestMatchProperty, converter, contract, member, reader, newObject))
								{
									this.SetExtensionData(contract, member, reader, text, newObject);
								}
							}
							else if (reader.Read())
							{
								this.SetPropertyPresence(reader, closestMatchProperty, dictionary2);
								this.SetExtensionData(contract, member, reader, text, newObject);
							}
							goto IL_291;
						}
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
						{
							this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, JsonSerializerInternalReader.getString_1(107299052).FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
						}
						if (this.Serializer._missingMemberHandling == MissingMemberHandling.Error)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107299411).FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType.Name));
						}
						if (!reader.Read())
						{
							goto IL_291;
						}
						this.SetExtensionData(contract, member, reader, text, newObject);
						goto IL_291;
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(newObject, contract, text, reader as IJsonLineInfo, reader.Path, ex))
						{
							throw;
						}
						this.HandleError(reader, true, depth);
						goto IL_291;
					}
				}
				IL_277:
				if (!reader.Read())
				{
					break;
				}
				continue;
				IL_291:
				if (!flag)
				{
					goto IL_277;
				}
				break;
			}
			goto IL_2BC;
			Block_10:
			throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107300955) + reader.TokenType);
			IL_2BC:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, newObject, JsonSerializerInternalReader.getString_1(107305150));
			}
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<JsonProperty, JsonSerializerInternalReader.PropertyPresence> keyValuePair in dictionary2)
				{
					JsonProperty key = keyValuePair.Key;
					JsonSerializerInternalReader.PropertyPresence value = keyValuePair.Value;
					this.EndProcessProperty(newObject, reader, contract, depth, key, value, true);
				}
			}
			this.OnDeserialized(reader, contract, newObject);
			return newObject;
		}

		private bool ShouldDeserialize(JsonReader reader, JsonProperty property, object target)
		{
			if (property.ShouldDeserialize == null)
			{
				return true;
			}
			bool flag = property.ShouldDeserialize(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, reader.Path, JsonSerializerInternalReader.getString_1(107299035).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		private bool CheckPropertyName(JsonReader reader, string memberName)
		{
			if (this.Serializer.MetadataPropertyHandling == MetadataPropertyHandling.ReadAhead && (memberName == JsonSerializerInternalReader.getString_1(107304894) || memberName == JsonSerializerInternalReader.getString_1(107303644) || memberName == JsonSerializerInternalReader.getString_1(107351655) || memberName == JsonSerializerInternalReader.getString_1(107302839)))
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		private void SetExtensionData(JsonObjectContract contract, JsonProperty member, JsonReader reader, string memberName, object o)
		{
			if (contract.ExtensionDataSetter != null)
			{
				try
				{
					object value = this.ReadExtensionDataValue(contract, member, reader);
					contract.ExtensionDataSetter(o, memberName, value);
					return;
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107298958).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType), ex);
				}
			}
			reader.Skip();
		}

		private object ReadExtensionDataValue(JsonObjectContract contract, JsonProperty member, JsonReader reader)
		{
			object result;
			if (contract.ExtensionDataIsJToken)
			{
				result = JToken.ReadFrom(reader);
			}
			else
			{
				result = this.CreateValueInternal(reader, null, null, null, contract, member, null);
			}
			return result;
		}

		private void EndProcessProperty(object newObject, JsonReader reader, JsonObjectContract contract, int initialDepth, JsonProperty property, JsonSerializerInternalReader.PropertyPresence presence, bool setDefaultValue)
		{
			if (presence == JsonSerializerInternalReader.PropertyPresence.None || presence == JsonSerializerInternalReader.PropertyPresence.Null)
			{
				try
				{
					Required required = property.Ignored ? Required.Default : (property._required ?? (contract.ItemRequired ?? Required.Default));
					if (presence == JsonSerializerInternalReader.PropertyPresence.None)
					{
						if (required != Required.AllowNull)
						{
							if (required != Required.Always)
							{
								if (!setDefaultValue || property.Ignored)
								{
									goto IL_178;
								}
								if (property.PropertyContract == null)
								{
									property.PropertyContract = this.GetContractSafe(property.PropertyType);
								}
								if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && property.Writable)
								{
									property.ValueProvider.SetValue(newObject, this.EnsureType(reader, property.GetResolvedDefaultValue(), CultureInfo.InvariantCulture, property.PropertyContract, property.PropertyType));
									goto IL_178;
								}
								goto IL_178;
							}
						}
						throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107298853).FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
					}
					if (presence == JsonSerializerInternalReader.PropertyPresence.Null)
					{
						if (required == Required.Always)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107298284).FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
						}
						if (required == Required.DisallowNull)
						{
							throw JsonSerializationException.Create(reader, JsonSerializerInternalReader.getString_1(107298211).FormatWith(CultureInfo.InvariantCulture, property.PropertyName));
						}
					}
					IL_178:;
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(newObject, contract, property.PropertyName, reader as IJsonLineInfo, reader.Path, ex))
					{
						throw;
					}
					this.HandleError(reader, true, initialDepth);
				}
			}
		}

		private void SetPropertyPresence(JsonReader reader, JsonProperty property, Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> requiredProperties)
		{
			if (property != null && requiredProperties != null)
			{
				JsonToken tokenType = reader.TokenType;
				JsonSerializerInternalReader.PropertyPresence value;
				if (tokenType != JsonToken.String)
				{
					if (tokenType - JsonToken.Null > 1)
					{
						value = JsonSerializerInternalReader.PropertyPresence.Value;
					}
					else
					{
						value = JsonSerializerInternalReader.PropertyPresence.Null;
					}
				}
				else
				{
					value = (JsonSerializerInternalReader.CoerceEmptyStringToNull(property.PropertyType, property.PropertyContract, (string)reader.Value) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
				}
				requiredProperties[property] = value;
			}
		}

		private void HandleError(JsonReader reader, bool readPastError, int initialDepth)
		{
			base.ClearErrorContext();
			if (readPastError)
			{
				reader.Skip();
				while (reader.Depth > initialDepth && reader.Read())
				{
				}
			}
		}

		static JsonSerializerInternalReader()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSerializerInternalReader));
		}

		[NonSerialized]
		internal static GetString getString_1;

		internal enum PropertyPresence
		{
			None,
			Null,
			Value
		}

		internal sealed class CreatorPropertyContext
		{
			public string Name;

			public JsonProperty Property;

			public JsonProperty ConstructorProperty;

			public JsonSerializerInternalReader.PropertyPresence? Presence;

			public object Value;

			public bool Used;
		}
	}
}
