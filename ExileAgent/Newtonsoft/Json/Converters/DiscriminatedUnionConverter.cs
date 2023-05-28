using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class DiscriminatedUnionConverter : JsonConverter
	{
		private static Type CreateUnionTypeLookup(Type t)
		{
			MethodCall<object, object> getUnionCases = FSharpUtils.GetUnionCases;
			object target = null;
			object[] array = new object[2];
			array[0] = t;
			object arg = ((object[])getUnionCases(target, array)).First<object>();
			return (Type)FSharpUtils.GetUnionCaseInfoDeclaringType(arg);
		}

		private static DiscriminatedUnionConverter.Union CreateUnion(Type t)
		{
			DiscriminatedUnionConverter.Union union = new DiscriminatedUnionConverter.Union();
			DiscriminatedUnionConverter.Union union2 = union;
			MethodCall<object, object> preComputeUnionTagReader = FSharpUtils.PreComputeUnionTagReader;
			object target = null;
			object[] array = new object[2];
			array[0] = t;
			union2.TagReader = (FSharpFunction)preComputeUnionTagReader(target, array);
			union.Cases = new List<DiscriminatedUnionConverter.UnionCase>();
			MethodCall<object, object> getUnionCases = FSharpUtils.GetUnionCases;
			object target2 = null;
			object[] array2 = new object[2];
			array2[0] = t;
			foreach (object obj in (object[])getUnionCases(target2, array2))
			{
				DiscriminatedUnionConverter.UnionCase unionCase = new DiscriminatedUnionConverter.UnionCase();
				unionCase.Tag = (int)FSharpUtils.GetUnionCaseInfoTag(obj);
				unionCase.Name = (string)FSharpUtils.GetUnionCaseInfoName(obj);
				unionCase.Fields = (PropertyInfo[])FSharpUtils.GetUnionCaseInfoFields(obj, new object[0]);
				DiscriminatedUnionConverter.UnionCase unionCase2 = unionCase;
				MethodCall<object, object> preComputeUnionReader = FSharpUtils.PreComputeUnionReader;
				object target3 = null;
				object[] array4 = new object[2];
				array4[0] = obj;
				unionCase2.FieldReader = (FSharpFunction)preComputeUnionReader(target3, array4);
				DiscriminatedUnionConverter.UnionCase unionCase3 = unionCase;
				MethodCall<object, object> preComputeUnionConstructor = FSharpUtils.PreComputeUnionConstructor;
				object target4 = null;
				object[] array5 = new object[2];
				array5[0] = obj;
				unionCase3.Constructor = (FSharpFunction)preComputeUnionConstructor(target4, array5);
				union.Cases.Add(unionCase);
			}
			return union;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			Type key = DiscriminatedUnionConverter.UnionTypeLookupCache.Get(value.GetType());
			DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(key);
			int tag = (int)union.TagReader.Invoke(new object[]
			{
				value
			});
			DiscriminatedUnionConverter.UnionCase unionCase = union.Cases.Single((DiscriminatedUnionConverter.UnionCase c) => c.Tag == tag);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(DiscriminatedUnionConverter.getString_0(107289967)) : DiscriminatedUnionConverter.getString_0(107289967));
			writer.WriteValue(unionCase.Name);
			if (unionCase.Fields != null && unionCase.Fields.Length != 0)
			{
				object[] array = (object[])unionCase.FieldReader.Invoke(new object[]
				{
					value
				});
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(DiscriminatedUnionConverter.getString_0(107289990)) : DiscriminatedUnionConverter.getString_0(107289990));
				writer.WriteStartArray();
				foreach (object value2 in array)
				{
					serializer.Serialize(writer, value2);
				}
				writer.WriteEndArray();
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DiscriminatedUnionConverter.UnionCase unionCase = null;
			string caseName = null;
			JArray jarray = null;
			reader.ReadAndAssert();
			Func<DiscriminatedUnionConverter.UnionCase, bool> <>9__0;
			while (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (string.Equals(text, DiscriminatedUnionConverter.getString_0(107289967), StringComparison.OrdinalIgnoreCase))
				{
					reader.ReadAndAssert();
					DiscriminatedUnionConverter.Union union = DiscriminatedUnionConverter.UnionCache.Get(objectType);
					caseName = reader.Value.ToString();
					IEnumerable<DiscriminatedUnionConverter.UnionCase> cases = union.Cases;
					Func<DiscriminatedUnionConverter.UnionCase, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((DiscriminatedUnionConverter.UnionCase c) => c.Name == caseName));
					}
					unionCase = cases.SingleOrDefault(predicate);
					if (unionCase == null)
					{
						throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107289981).FormatWith(CultureInfo.InvariantCulture, caseName));
					}
				}
				else
				{
					if (!string.Equals(text, DiscriminatedUnionConverter.getString_0(107289990), StringComparison.OrdinalIgnoreCase))
					{
						throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107289847).FormatWith(CultureInfo.InvariantCulture, text));
					}
					reader.ReadAndAssert();
					if (reader.TokenType != JsonToken.StartArray)
					{
						throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107289924));
					}
					jarray = (JArray)JToken.ReadFrom(reader);
				}
				reader.ReadAndAssert();
			}
			if (unionCase == null)
			{
				throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107289778).FormatWith(CultureInfo.InvariantCulture, DiscriminatedUnionConverter.getString_0(107289967)));
			}
			object[] array = new object[unionCase.Fields.Length];
			if (unionCase.Fields.Length != 0 && jarray == null)
			{
				throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107321977).FormatWith(CultureInfo.InvariantCulture, DiscriminatedUnionConverter.getString_0(107289990)));
			}
			if (jarray != null)
			{
				if (unionCase.Fields.Length != jarray.Count)
				{
					throw JsonSerializationException.Create(reader, DiscriminatedUnionConverter.getString_0(107321952).FormatWith(CultureInfo.InvariantCulture, caseName));
				}
				for (int i = 0; i < jarray.Count; i++)
				{
					JToken jtoken = jarray[i];
					PropertyInfo propertyInfo = unionCase.Fields[i];
					array[i] = jtoken.ToObject(propertyInfo.PropertyType, serializer);
				}
			}
			object[] args = new object[]
			{
				array
			};
			return unionCase.Constructor.Invoke(args);
		}

		public override bool CanConvert(Type objectType)
		{
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
			{
				return false;
			}
			object[] customAttributes = objectType.GetCustomAttributes(true);
			bool flag = false;
			object[] array = customAttributes;
			int i = 0;
			while (i < array.Length)
			{
				Type type = array[i].GetType();
				if (!(type.FullName == DiscriminatedUnionConverter.getString_0(107321831)))
				{
					i++;
				}
				else
				{
					FSharpUtils.EnsureInitialized(type.Assembly());
					flag = true;
					IL_60:
					if (!flag)
					{
						return false;
					}
					MethodCall<object, object> isUnion = FSharpUtils.IsUnion;
					object target = null;
					object[] array2 = new object[2];
					array2[0] = objectType;
					return (bool)isUnion(target, array2);
				}
			}
			goto IL_60;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DiscriminatedUnionConverter()
		{
			Strings.CreateGetStringDelegate(typeof(DiscriminatedUnionConverter));
			DiscriminatedUnionConverter.UnionCache = new ThreadSafeStore<Type, DiscriminatedUnionConverter.Union>(new Func<Type, DiscriminatedUnionConverter.Union>(DiscriminatedUnionConverter.CreateUnion));
			DiscriminatedUnionConverter.UnionTypeLookupCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(DiscriminatedUnionConverter.CreateUnionTypeLookup));
		}

		private const string CasePropertyName = "Case";

		private const string FieldsPropertyName = "Fields";

		private static readonly ThreadSafeStore<Type, DiscriminatedUnionConverter.Union> UnionCache;

		private static readonly ThreadSafeStore<Type, Type> UnionTypeLookupCache;

		[NonSerialized]
		internal static GetString getString_0;

		internal sealed class Union
		{
			public FSharpFunction TagReader { get; set; }

			public List<DiscriminatedUnionConverter.UnionCase> Cases;
		}

		internal sealed class UnionCase
		{
			public int Tag;

			public string Name;

			public PropertyInfo[] Fields;

			public FSharpFunction FieldReader;

			public FSharpFunction Constructor;
		}
	}
}
