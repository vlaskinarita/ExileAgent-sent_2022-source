using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal sealed class JsonSerializerInternalWriter : JsonSerializerInternalBase
	{
		public JsonSerializerInternalWriter(JsonSerializer serializer) : base(serializer)
		{
		}

		public void Serialize(JsonWriter jsonWriter, object value, Type objectType)
		{
			if (jsonWriter == null)
			{
				throw new ArgumentNullException(JsonSerializerInternalWriter.getString_1(107350546));
			}
			this._rootType = objectType;
			this._rootLevel = this._serializeStack.Count + 1;
			JsonContract contractSafe = this.GetContractSafe(value);
			try
			{
				if (this.ShouldWriteReference(value, null, contractSafe, null, null))
				{
					this.WriteReference(jsonWriter, value);
				}
				else
				{
					this.SerializeValue(jsonWriter, value, contractSafe, null, null, null);
				}
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(null, contractSafe, null, null, jsonWriter.Path, ex))
				{
					base.ClearErrorContext();
					throw;
				}
				this.HandleError(jsonWriter, 0);
			}
			finally
			{
				this._rootType = null;
			}
		}

		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this.InternalSerializer == null)
			{
				this.InternalSerializer = new JsonSerializerProxy(this);
			}
			return this.InternalSerializer;
		}

		private JsonContract GetContractSafe(object value)
		{
			if (value == null)
			{
				return null;
			}
			return this.Serializer._contractResolver.ResolveContract(value.GetType());
		}

		private void SerializePrimitive(JsonWriter writer, object value, JsonPrimitiveContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			if (contract.TypeCode == PrimitiveTypeCode.Bytes && this.ShouldWriteType(TypeNameHandling.Objects, contract, member, containerContract, containerProperty))
			{
				writer.WriteStartObject();
				this.WriteTypeProperty(writer, contract.CreatedType);
				writer.WritePropertyName(JsonSerializerInternalWriter.getString_1(107351645), false);
				JsonWriter.WriteValue(writer, contract.TypeCode, value);
				writer.WriteEndObject();
				return;
			}
			JsonWriter.WriteValue(writer, contract.TypeCode, value);
		}

		private void SerializeValue(JsonWriter writer, object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			JsonConverter jsonConverter;
			if (member != null)
			{
				if ((jsonConverter = member.Converter) != null)
				{
					goto IL_65;
				}
			}
			if (containerProperty != null)
			{
				if ((jsonConverter = containerProperty.ItemConverter) != null)
				{
					goto IL_65;
				}
			}
			if (containerContract != null)
			{
				if ((jsonConverter = containerContract.ItemConverter) != null)
				{
					goto IL_65;
				}
			}
			if ((jsonConverter = valueContract.Converter) == null)
			{
				jsonConverter = (this.Serializer.GetMatchingConverter(valueContract.UnderlyingType) ?? valueContract.InternalConverter);
			}
			IL_65:
			JsonConverter jsonConverter2 = jsonConverter;
			if (jsonConverter2 != null && jsonConverter2.CanWrite)
			{
				this.SerializeConvertable(writer, jsonConverter2, value, valueContract, containerContract, containerProperty);
				return;
			}
			switch (valueContract.ContractType)
			{
			case JsonContractType.Object:
				this.SerializeObject(writer, value, (JsonObjectContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Array:
			{
				JsonArrayContract jsonArrayContract = (JsonArrayContract)valueContract;
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.SerializeList(writer, (IEnumerable)value, jsonArrayContract, member, containerContract, containerProperty);
					return;
				}
				this.SerializeMultidimensionalArray(writer, (Array)value, jsonArrayContract, member, containerContract, containerProperty);
				return;
			}
			case JsonContractType.Primitive:
				this.SerializePrimitive(writer, value, (JsonPrimitiveContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.String:
				this.SerializeString(writer, value, (JsonStringContract)valueContract);
				return;
			case JsonContractType.Dictionary:
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)valueContract;
				IDictionary dictionary;
				IDictionary values;
				if ((dictionary = (value as IDictionary)) == null)
				{
					IDictionary dictionary2 = jsonDictionaryContract.CreateWrapper(value);
					values = dictionary2;
				}
				else
				{
					values = dictionary;
				}
				this.SerializeDictionary(writer, values, jsonDictionaryContract, member, containerContract, containerProperty);
				return;
			}
			case JsonContractType.Dynamic:
				this.SerializeDynamic(writer, (IDynamicMetaObjectProvider)value, (JsonDynamicContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Serializable:
				this.SerializeISerializable(writer, (ISerializable)value, (JsonISerializableContract)valueContract, member, containerContract, containerProperty);
				return;
			case JsonContractType.Linq:
				((JToken)value).WriteTo(writer, this.Serializer.Converters.ToArray<JsonConverter>());
				return;
			default:
				return;
			}
		}

		private bool? ResolveIsReference(JsonContract contract, JsonProperty property, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			bool? result = null;
			if (property != null)
			{
				result = property.IsReference;
			}
			if (result == null && containerProperty != null)
			{
				result = containerProperty.ItemIsReference;
			}
			if (result == null && collectionContract != null)
			{
				result = collectionContract.ItemIsReference;
			}
			if (result == null)
			{
				result = contract.IsReference;
			}
			return result;
		}

		private bool ShouldWriteReference(object value, JsonProperty property, JsonContract valueContract, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			if (value == null)
			{
				return false;
			}
			if (valueContract.ContractType != JsonContractType.Primitive)
			{
				if (valueContract.ContractType != JsonContractType.String)
				{
					bool? flag = this.ResolveIsReference(valueContract, property, collectionContract, containerProperty);
					if (flag == null)
					{
						if (valueContract.ContractType == JsonContractType.Array)
						{
							flag = new bool?(this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Arrays));
						}
						else
						{
							flag = new bool?(this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Objects));
						}
					}
					return flag.GetValueOrDefault() && this.Serializer.GetReferenceResolver().IsReferenced(this, value);
				}
			}
			return false;
		}

		private bool ShouldWriteProperty(object memberValue, JsonObjectContract containerContract, JsonProperty property)
		{
			return (memberValue != null || base.ResolvedNullValueHandling(containerContract, property) != NullValueHandling.Ignore) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) || !MiscellaneousUtils.ValueEquals(memberValue, property.GetResolvedDefaultValue()));
		}

		private bool CheckForCircularReference(JsonWriter writer, object value, JsonProperty property, JsonContract contract, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			if (value != null && contract.ContractType != JsonContractType.Primitive)
			{
				if (contract.ContractType != JsonContractType.String)
				{
					ReferenceLoopHandling? referenceLoopHandling = null;
					if (property != null)
					{
						referenceLoopHandling = property.ReferenceLoopHandling;
					}
					if (referenceLoopHandling == null && containerProperty != null)
					{
						referenceLoopHandling = containerProperty.ItemReferenceLoopHandling;
					}
					if (referenceLoopHandling == null && containerContract != null)
					{
						referenceLoopHandling = containerContract.ItemReferenceLoopHandling;
					}
					if ((this.Serializer._equalityComparer != null) ? this._serializeStack.Contains(value, this.Serializer._equalityComparer) : this._serializeStack.Contains(value))
					{
						string text = JsonSerializerInternalWriter.getString_1(107298162);
						if (property != null)
						{
							text += JsonSerializerInternalWriter.getString_1(107298153).FormatWith(CultureInfo.InvariantCulture, property.PropertyName);
						}
						text += JsonSerializerInternalWriter.getString_1(107298124).FormatWith(CultureInfo.InvariantCulture, value.GetType());
						switch (referenceLoopHandling.GetValueOrDefault(this.Serializer._referenceLoopHandling))
						{
						case ReferenceLoopHandling.Error:
							throw JsonSerializationException.Create(null, writer.ContainerPath, text, null);
						case ReferenceLoopHandling.Ignore:
							if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
							{
								this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, text + JsonSerializerInternalWriter.getString_1(107298579)), null);
							}
							return false;
						case ReferenceLoopHandling.Serialize:
							if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
							{
								this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, text + JsonSerializerInternalWriter.getString_1(107298518)), null);
							}
							return true;
						}
					}
					return true;
				}
			}
			return true;
		}

		private void WriteReference(JsonWriter writer, object value)
		{
			string reference = this.GetReference(writer, value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107298501).FormatWith(CultureInfo.InvariantCulture, reference, value.GetType())), null);
			}
			writer.WriteStartObject();
			writer.WritePropertyName(JsonSerializerInternalWriter.getString_1(107303664), false);
			writer.WriteValue(reference);
			writer.WriteEndObject();
		}

		private string GetReference(JsonWriter writer, object value)
		{
			string reference;
			try
			{
				reference = this.Serializer.GetReferenceResolver().GetReference(this, value);
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, JsonSerializerInternalWriter.getString_1(107298440).FormatWith(CultureInfo.InvariantCulture, value.GetType()), ex);
			}
			return reference;
		}

		internal static bool TryConvertToString(object value, Type type, out string s)
		{
			TypeConverter typeConverter;
			if (JsonTypeReflector.CanTypeDescriptorConvertString(type, out typeConverter))
			{
				s = typeConverter.ConvertToInvariantString(value);
				return true;
			}
			type = (value as Type);
			if (type != null)
			{
				s = type.AssemblyQualifiedName;
				return true;
			}
			s = null;
			return false;
		}

		private void SerializeString(JsonWriter writer, object value, JsonStringContract contract)
		{
			this.OnSerializing(writer, contract, value);
			string value2;
			JsonSerializerInternalWriter.TryConvertToString(value, contract.UnderlyingType, out value2);
			writer.WriteValue(value2);
			this.OnSerialized(writer, contract, value);
		}

		private void OnSerializing(JsonWriter writer, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107298383).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerializing(value, this.Serializer._context);
		}

		private void OnSerialized(JsonWriter writer, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107297838).FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnSerialized(value, this.Serializer._context);
		}

		private void SerializeObject(JsonWriter writer, object value, JsonObjectContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				JsonProperty jsonProperty = contract.Properties[i];
				try
				{
					JsonContract valueContract;
					object value2;
					if (this.CalculatePropertyValues(writer, value, contract, member, jsonProperty, out valueContract, out value2))
					{
						jsonProperty.WritePropertyName(writer);
						this.SerializeValue(writer, value2, valueContract, jsonProperty, contract, member);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
			}
			ExtensionDataGetter extensionDataGetter = contract.ExtensionDataGetter;
			IEnumerable<KeyValuePair<object, object>> enumerable = (extensionDataGetter != null) ? extensionDataGetter(value) : null;
			if (enumerable != null)
			{
				foreach (KeyValuePair<object, object> keyValuePair in enumerable)
				{
					JsonContract contractSafe = this.GetContractSafe(keyValuePair.Key);
					JsonContract contractSafe2 = this.GetContractSafe(keyValuePair.Value);
					bool flag;
					string text = this.GetPropertyName(writer, keyValuePair.Key, contractSafe, out flag);
					text = ((contract.ExtensionDataNameResolver != null) ? contract.ExtensionDataNameResolver(text) : text);
					if (this.ShouldWriteReference(keyValuePair.Value, null, contractSafe2, contract, member))
					{
						writer.WritePropertyName(text);
						this.WriteReference(writer, keyValuePair.Value);
					}
					else if (this.CheckForCircularReference(writer, keyValuePair.Value, null, contractSafe2, contract, member))
					{
						writer.WritePropertyName(text);
						this.SerializeValue(writer, keyValuePair.Value, contractSafe2, null, contract, member);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		private bool CalculatePropertyValues(JsonWriter writer, object value, JsonContainerContract contract, JsonProperty member, JsonProperty property, out JsonContract memberContract, out object memberValue)
		{
			if (!property.Ignored && property.Readable && this.ShouldSerialize(writer, property, value) && this.IsSpecified(writer, property, value))
			{
				if (property.PropertyContract == null)
				{
					property.PropertyContract = this.Serializer._contractResolver.ResolveContract(property.PropertyType);
				}
				memberValue = property.ValueProvider.GetValue(value);
				memberContract = (property.PropertyContract.IsSealed ? property.PropertyContract : this.GetContractSafe(memberValue));
				if (this.ShouldWriteProperty(memberValue, contract as JsonObjectContract, property))
				{
					if (this.ShouldWriteReference(memberValue, property, memberContract, contract, member))
					{
						property.WritePropertyName(writer);
						this.WriteReference(writer, memberValue);
						return false;
					}
					if (!this.CheckForCircularReference(writer, memberValue, property, memberContract, contract, member))
					{
						return false;
					}
					if (memberValue == null)
					{
						JsonObjectContract jsonObjectContract = contract as JsonObjectContract;
						Required required = property._required ?? (((jsonObjectContract != null) ? jsonObjectContract.ItemRequired : null) ?? Required.Default);
						if (required == Required.Always)
						{
							throw JsonSerializationException.Create(null, writer.ContainerPath, JsonSerializerInternalWriter.getString_1(107297805).FormatWith(CultureInfo.InvariantCulture, property.PropertyName), null);
						}
						if (required == Required.DisallowNull)
						{
							throw JsonSerializationException.Create(null, writer.ContainerPath, JsonSerializerInternalWriter.getString_1(107297708).FormatWith(CultureInfo.InvariantCulture, property.PropertyName), null);
						}
					}
					return true;
				}
			}
			memberContract = null;
			memberValue = null;
			return false;
		}

		private void WriteObjectStart(JsonWriter writer, object value, JsonContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			writer.WriteStartObject();
			if ((this.ResolveIsReference(contract, member, collectionContract, containerProperty) ?? this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Objects)) && (member == null || member.Writable || this.HasCreatorParameter(collectionContract, member)))
			{
				this.WriteReferenceIdProperty(writer, contract.UnderlyingType, value);
			}
			if (this.ShouldWriteType(TypeNameHandling.Objects, contract, member, collectionContract, containerProperty))
			{
				this.WriteTypeProperty(writer, contract.UnderlyingType);
			}
		}

		private bool HasCreatorParameter(JsonContainerContract contract, JsonProperty property)
		{
			JsonObjectContract jsonObjectContract;
			return (jsonObjectContract = (contract as JsonObjectContract)) != null && jsonObjectContract.CreatorParameters.Contains(property.PropertyName);
		}

		private void WriteReferenceIdProperty(JsonWriter writer, Type type, object value)
		{
			string reference = this.GetReference(writer, value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107298079).FormatWith(CultureInfo.InvariantCulture, reference, type)), null);
			}
			writer.WritePropertyName(JsonSerializerInternalWriter.getString_1(107304914), false);
			writer.WriteValue(reference);
		}

		private void WriteTypeProperty(JsonWriter writer, Type type)
		{
			string typeName = ReflectionUtils.GetTypeName(type, this.Serializer._typeNameAssemblyFormatHandling, this.Serializer._serializationBinder);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107298054).FormatWith(CultureInfo.InvariantCulture, typeName, type)), null);
			}
			writer.WritePropertyName(JsonSerializerInternalWriter.getString_1(107351675), false);
			writer.WriteValue(typeName);
		}

		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(PreserveReferencesHandling value, PreserveReferencesHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(TypeNameHandling value, TypeNameHandling flag)
		{
			return (value & flag) == flag;
		}

		private void SerializeConvertable(JsonWriter writer, JsonConverter converter, object value, JsonContract contract, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			if (this.ShouldWriteReference(value, null, contract, collectionContract, containerProperty))
			{
				this.WriteReference(writer, value);
				return;
			}
			if (!this.CheckForCircularReference(writer, value, null, contract, collectionContract, containerProperty))
			{
				return;
			}
			this._serializeStack.Add(value);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107297977).FormatWith(CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
			}
			converter.WriteJson(writer, value, this.GetInternalSerializer());
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107297916).FormatWith(CultureInfo.InvariantCulture, value.GetType(), converter.GetType())), null);
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
		}

		private void SerializeList(JsonWriter writer, IEnumerable values, JsonArrayContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			IWrappedCollection wrappedCollection;
			object obj = ((wrappedCollection = (values as IWrappedCollection)) != null) ? wrappedCollection.UnderlyingCollection : values;
			this.OnSerializing(writer, contract, obj);
			this._serializeStack.Add(obj);
			bool flag = this.WriteStartArray(writer, obj, contract, member, collectionContract, containerProperty);
			writer.WriteStartArray();
			int top = writer.Top;
			int num = 0;
			foreach (object value in values)
			{
				try
				{
					JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
					if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
					{
						this.WriteReference(writer, value);
					}
					else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
					{
						this.SerializeValue(writer, value, jsonContract, null, contract, member);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(obj, contract, num, null, writer.ContainerPath, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
				finally
				{
					num++;
				}
			}
			writer.WriteEndArray();
			if (flag)
			{
				writer.WriteEndObject();
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, obj);
		}

		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, values);
			this._serializeStack.Add(values);
			bool flag = this.WriteStartArray(writer, values, contract, member, collectionContract, containerProperty);
			this.SerializeMultidimensionalArray(writer, values, contract, member, writer.Top, CollectionUtils.ArrayEmpty<int>());
			if (flag)
			{
				writer.WriteEndObject();
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, values);
		}

		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, JsonProperty member, int initialDepth, int[] indices)
		{
			int num = indices.Length;
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			writer.WriteStartArray();
			int j = values.GetLowerBound(num);
			while (j <= values.GetUpperBound(num))
			{
				array[num] = j;
				if (array.Length == values.Rank)
				{
					object value = values.GetValue(array);
					try
					{
						JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
						if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
						{
							this.WriteReference(writer, value);
						}
						else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
						{
							this.SerializeValue(writer, value, jsonContract, null, contract, member);
						}
						goto IL_E4;
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(values, contract, j, null, writer.ContainerPath, ex))
						{
							throw;
						}
						this.HandleError(writer, initialDepth + 1);
						goto IL_E4;
					}
					goto IL_D4;
				}
				goto IL_D4;
				IL_E4:
				j++;
				continue;
				IL_D4:
				this.SerializeMultidimensionalArray(writer, values, contract, member, initialDepth + 1, array);
				goto IL_E4;
			}
			writer.WriteEndArray();
		}

		private bool WriteStartArray(JsonWriter writer, object values, JsonArrayContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			bool flag = (this.ResolveIsReference(contract, member, containerContract, containerProperty) ?? this.HasFlag(this.Serializer._preserveReferencesHandling, PreserveReferencesHandling.Arrays)) && (member == null || member.Writable || this.HasCreatorParameter(containerContract, member));
			bool flag2 = this.ShouldWriteType(TypeNameHandling.Arrays, contract, member, containerContract, containerProperty);
			bool flag3 = flag || flag2;
			if (flag3)
			{
				writer.WriteStartObject();
				if (flag)
				{
					this.WriteReferenceIdProperty(writer, contract.UnderlyingType, values);
				}
				if (flag2)
				{
					this.WriteTypeProperty(writer, values.GetType());
				}
				writer.WritePropertyName(JsonSerializerInternalWriter.getString_1(107302859), false);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.Serializer._contractResolver.ResolveContract(contract.CollectionItemType ?? typeof(object));
			}
			return flag3;
		}

		[SecuritySafeCritical]
		private void SerializeISerializable(JsonWriter writer, ISerializable value, JsonISerializableContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				string text = JsonSerializerInternalWriter.getString_1(107297855) + Environment.NewLine + JsonSerializerInternalWriter.getString_1(107300457) + Environment.NewLine;
				text = text.FormatWith(CultureInfo.InvariantCulture, value.GetType());
				throw JsonSerializationException.Create(null, writer.ContainerPath, text, null);
			}
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, new FormatterConverter());
			value.GetObjectData(serializationInfo, this.Serializer._context);
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				JsonContract contractSafe = this.GetContractSafe(serializationEntry.Value);
				if (this.ShouldWriteReference(serializationEntry.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(serializationEntry.Name);
					this.WriteReference(writer, serializationEntry.Value);
				}
				else if (this.CheckForCircularReference(writer, serializationEntry.Value, null, contractSafe, contract, member))
				{
					writer.WritePropertyName(serializationEntry.Name);
					this.SerializeValue(writer, serializationEntry.Value, contractSafe, null, contract, member);
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		private void SerializeDynamic(JsonWriter writer, IDynamicMetaObjectProvider value, JsonDynamicContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			this.OnSerializing(writer, contract, value);
			this._serializeStack.Add(value);
			this.WriteObjectStart(writer, value, contract, member, collectionContract, containerProperty);
			int top = writer.Top;
			for (int i = 0; i < contract.Properties.Count; i++)
			{
				JsonProperty jsonProperty = contract.Properties[i];
				if (jsonProperty.HasMemberAttribute)
				{
					try
					{
						JsonContract valueContract;
						object value2;
						if (this.CalculatePropertyValues(writer, value, contract, member, jsonProperty, out valueContract, out value2))
						{
							jsonProperty.WritePropertyName(writer);
							this.SerializeValue(writer, value2, valueContract, jsonProperty, contract, member);
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(value, contract, jsonProperty.PropertyName, null, writer.ContainerPath, ex))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			foreach (string text in value.GetDynamicMemberNames())
			{
				object obj;
				if (contract.TryGetMember(value, text, out obj))
				{
					try
					{
						JsonContract contractSafe = this.GetContractSafe(obj);
						if (this.ShouldWriteDynamicProperty(obj))
						{
							if (this.CheckForCircularReference(writer, obj, null, contractSafe, contract, member))
							{
								string name = (contract.PropertyNameResolver != null) ? contract.PropertyNameResolver(text) : text;
								writer.WritePropertyName(name);
								this.SerializeValue(writer, obj, contractSafe, null, contract, member);
							}
						}
					}
					catch (Exception ex2)
					{
						if (!base.IsErrorHandled(value, contract, text, null, writer.ContainerPath, ex2))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, value);
		}

		private bool ShouldWriteDynamicProperty(object memberValue)
		{
			return (this.Serializer._nullValueHandling != NullValueHandling.Ignore || memberValue != null) && (!this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Ignore) || (memberValue != null && !MiscellaneousUtils.ValueEquals(memberValue, ReflectionUtils.GetDefaultValue(memberValue.GetType()))));
		}

		private bool ShouldWriteType(TypeNameHandling typeNameHandlingFlag, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			TypeNameHandling value = ((member != null) ? member.TypeNameHandling : null) ?? (((containerProperty != null) ? containerProperty.ItemTypeNameHandling : null) ?? (((containerContract != null) ? containerContract.ItemTypeNameHandling : null) ?? this.Serializer._typeNameHandling));
			if (this.HasFlag(value, typeNameHandlingFlag))
			{
				return true;
			}
			if (this.HasFlag(value, TypeNameHandling.Auto))
			{
				if (member != null)
				{
					if (contract.NonNullableUnderlyingType != member.PropertyContract.CreatedType)
					{
						return true;
					}
				}
				else if (containerContract != null)
				{
					if (containerContract.ItemContract == null || contract.NonNullableUnderlyingType != containerContract.ItemContract.CreatedType)
					{
						return true;
					}
				}
				else if (this._rootType != null && this._serializeStack.Count == this._rootLevel)
				{
					JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(this._rootType);
					if (contract.NonNullableUnderlyingType != jsonContract.CreatedType)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void SerializeDictionary(JsonWriter writer, IDictionary values, JsonDictionaryContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)
		{
			IWrappedDictionary wrappedDictionary;
			object obj = ((wrappedDictionary = (values as IWrappedDictionary)) != null) ? wrappedDictionary.UnderlyingDictionary : values;
			this.OnSerializing(writer, contract, obj);
			this._serializeStack.Add(obj);
			this.WriteObjectStart(writer, obj, contract, member, collectionContract, containerProperty);
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.Serializer._contractResolver.ResolveContract(contract.DictionaryValueType ?? typeof(object));
			}
			if (contract.KeyContract == null)
			{
				contract.KeyContract = this.Serializer._contractResolver.ResolveContract(contract.DictionaryKeyType ?? typeof(object));
			}
			int top = writer.Top;
			using (IDictionaryEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DictionaryEntry entry = enumerator.Entry;
					bool escape;
					string text = this.GetPropertyName(writer, entry.Key, contract.KeyContract, out escape);
					text = ((contract.DictionaryKeyResolver != null) ? contract.DictionaryKeyResolver(text) : text);
					try
					{
						object value = entry.Value;
						JsonContract jsonContract = contract.FinalItemContract ?? this.GetContractSafe(value);
						if (this.ShouldWriteReference(value, null, jsonContract, contract, member))
						{
							writer.WritePropertyName(text, escape);
							this.WriteReference(writer, value);
						}
						else if (this.CheckForCircularReference(writer, value, null, jsonContract, contract, member))
						{
							writer.WritePropertyName(text, escape);
							this.SerializeValue(writer, value, jsonContract, null, contract, member);
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(obj, contract, text, null, writer.ContainerPath, ex))
						{
							throw;
						}
						this.HandleError(writer, top);
					}
				}
			}
			writer.WriteEndObject();
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			this.OnSerialized(writer, contract, obj);
		}

		private string GetPropertyName(JsonWriter writer, object name, JsonContract contract, out bool escape)
		{
			if (contract.ContractType == JsonContractType.Primitive)
			{
				JsonPrimitiveContract jsonPrimitiveContract = (JsonPrimitiveContract)contract;
				switch (jsonPrimitiveContract.TypeCode)
				{
				case PrimitiveTypeCode.Single:
				case PrimitiveTypeCode.SingleNullable:
				{
					float num = (float)name;
					escape = false;
					return num.ToString(JsonSerializerInternalWriter.getString_1(107402924), CultureInfo.InvariantCulture);
				}
				case PrimitiveTypeCode.Double:
				case PrimitiveTypeCode.DoubleNullable:
				{
					double num2 = (double)name;
					escape = false;
					return num2.ToString(JsonSerializerInternalWriter.getString_1(107402924), CultureInfo.InvariantCulture);
				}
				case PrimitiveTypeCode.DateTime:
				case PrimitiveTypeCode.DateTimeNullable:
				{
					DateTime value = DateTimeUtils.EnsureDateTime((DateTime)name, writer.DateTimeZoneHandling);
					escape = false;
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					DateTimeUtils.WriteDateTimeString(stringWriter, value, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter.ToString();
				}
				case PrimitiveTypeCode.DateTimeOffset:
				case PrimitiveTypeCode.DateTimeOffsetNullable:
				{
					escape = false;
					StringWriter stringWriter2 = new StringWriter(CultureInfo.InvariantCulture);
					DateTimeUtils.WriteDateTimeOffsetString(stringWriter2, (DateTimeOffset)name, writer.DateFormatHandling, writer.DateFormatString, writer.Culture);
					return stringWriter2.ToString();
				}
				default:
				{
					escape = true;
					string result;
					if (jsonPrimitiveContract.IsEnum && EnumUtils.TryToString(jsonPrimitiveContract.NonNullableUnderlyingType, name, null, out result))
					{
						return result;
					}
					return Convert.ToString(name, CultureInfo.InvariantCulture);
				}
				}
			}
			else
			{
				string result2;
				if (JsonSerializerInternalWriter.TryConvertToString(name, name.GetType(), out result2))
				{
					escape = true;
					return result2;
				}
				escape = true;
				return name.ToString();
			}
		}

		private void HandleError(JsonWriter writer, int initialDepth)
		{
			base.ClearErrorContext();
			if (writer.WriteState == WriteState.Property)
			{
				writer.WriteNull();
			}
			while (writer.Top > initialDepth)
			{
				writer.WriteEnd();
			}
		}

		private bool ShouldSerialize(JsonWriter writer, JsonProperty property, object target)
		{
			if (property.ShouldSerialize == null)
			{
				return true;
			}
			bool flag = property.ShouldSerialize(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107297089).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		private bool IsSpecified(JsonWriter writer, JsonProperty property, object target)
		{
			if (property.GetIsSpecified == null)
			{
				return true;
			}
			bool flag = property.GetIsSpecified(target);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(null, writer.Path, JsonSerializerInternalWriter.getString_1(107297528).FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType, flag)), null);
			}
			return flag;
		}

		static JsonSerializerInternalWriter()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSerializerInternalWriter));
		}

		private Type _rootType;

		private int _rootLevel;

		private readonly List<object> _serializeStack = new List<object>();

		[NonSerialized]
		internal static GetString getString_1;
	}
}
