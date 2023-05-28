using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	public static class JsonConvert
	{
		public static Func<JsonSerializerSettings> DefaultSettings { get; set; }

		public static string ToString(DateTime value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind);
		}

		public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
		{
			DateTime value2 = DateTimeUtils.EnsureDateTime(value, timeZoneHandling);
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeString(stringWriter, value2, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		public static string ToString(DateTimeOffset value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat);
		}

		public static string ToString(DateTimeOffset value, DateFormatHandling format)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		public static string ToString(bool value)
		{
			if (!value)
			{
				return JsonConvert.False;
			}
			return JsonConvert.True;
		}

		public static string ToString(char value)
		{
			return JsonConvert.ToString(char.ToString(value));
		}

		public static string ToString(Enum value)
		{
			return value.ToString(JsonConvert.getString_0(107444252));
		}

		public static string ToString(int value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(short value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(long value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		private static string ToStringInternal(BigInteger value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(float value)
		{
			return JsonConvert.EnsureDecimalPlace((double)value, value.ToString(JsonConvert.getString_0(107401504), CultureInfo.InvariantCulture));
		}

		internal static string ToString(float value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat((double)value, JsonConvert.EnsureDecimalPlace((double)value, value.ToString(JsonConvert.getString_0(107401504), CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		private static string EnsureFloatFormat(double value, string text, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			if (floatFormatHandling == FloatFormatHandling.Symbol || (!double.IsInfinity(value) && !double.IsNaN(value)))
			{
				return text;
			}
			if (floatFormatHandling != FloatFormatHandling.DefaultValue)
			{
				return quoteChar.ToString() + text + quoteChar.ToString();
			}
			if (nullable)
			{
				return JsonConvert.Null;
			}
			return JsonConvert.getString_0(107351496);
		}

		public static string ToString(double value)
		{
			return JsonConvert.EnsureDecimalPlace(value, value.ToString(JsonConvert.getString_0(107401504), CultureInfo.InvariantCulture));
		}

		internal static string ToString(double value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat(value, JsonConvert.EnsureDecimalPlace(value, value.ToString(JsonConvert.getString_0(107401504), CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		private static string EnsureDecimalPlace(double value, string text)
		{
			if (!double.IsNaN(value) && !double.IsInfinity(value) && text.IndexOf('.') == -1 && text.IndexOf('E') == -1)
			{
				if (text.IndexOf('e') == -1)
				{
					return text + JsonConvert.getString_0(107351523);
				}
			}
			return text;
		}

		private static string EnsureDecimalPlace(string text)
		{
			if (text.IndexOf('.') != -1)
			{
				return text;
			}
			return text + JsonConvert.getString_0(107351523);
		}

		public static string ToString(byte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		public static string ToString(decimal value)
		{
			return JsonConvert.EnsureDecimalPlace(value.ToString(null, CultureInfo.InvariantCulture));
		}

		public static string ToString(Guid value)
		{
			return JsonConvert.ToString(value, '"');
		}

		internal static string ToString(Guid value, char quoteChar)
		{
			string str = value.ToString(JsonConvert.getString_0(107444252), CultureInfo.InvariantCulture);
			string text = quoteChar.ToString(CultureInfo.InvariantCulture);
			return text + str + text;
		}

		public static string ToString(TimeSpan value)
		{
			return JsonConvert.ToString(value, '"');
		}

		internal static string ToString(TimeSpan value, char quoteChar)
		{
			return JsonConvert.ToString(value.ToString(), quoteChar);
		}

		public static string ToString(Uri value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			return JsonConvert.ToString(value, '"');
		}

		internal static string ToString(Uri value, char quoteChar)
		{
			return JsonConvert.ToString(value.OriginalString, quoteChar);
		}

		public static string ToString(string value)
		{
			return JsonConvert.ToString(value, '"');
		}

		public static string ToString(string value, char delimiter)
		{
			return JsonConvert.ToString(value, delimiter, StringEscapeHandling.Default);
		}

		public static string ToString(string value, char delimiter, StringEscapeHandling stringEscapeHandling)
		{
			if (delimiter != '"' && delimiter != '\'')
			{
				throw new ArgumentException(JsonConvert.getString_0(107351518), JsonConvert.getString_0(107351969));
			}
			return JavaScriptUtils.ToEscapedJavaScriptString(value, delimiter, true, stringEscapeHandling);
		}

		public static string ToString(object value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			switch (ConvertUtils.GetTypeCode(value.GetType()))
			{
			case PrimitiveTypeCode.Char:
				return JsonConvert.ToString((char)value);
			case PrimitiveTypeCode.Boolean:
				return JsonConvert.ToString((bool)value);
			case PrimitiveTypeCode.SByte:
				return JsonConvert.ToString((sbyte)value);
			case PrimitiveTypeCode.Int16:
				return JsonConvert.ToString((short)value);
			case PrimitiveTypeCode.UInt16:
				return JsonConvert.ToString((ushort)value);
			case PrimitiveTypeCode.Int32:
				return JsonConvert.ToString((int)value);
			case PrimitiveTypeCode.Byte:
				return JsonConvert.ToString((byte)value);
			case PrimitiveTypeCode.UInt32:
				return JsonConvert.ToString((uint)value);
			case PrimitiveTypeCode.Int64:
				return JsonConvert.ToString((long)value);
			case PrimitiveTypeCode.UInt64:
				return JsonConvert.ToString((ulong)value);
			case PrimitiveTypeCode.Single:
				return JsonConvert.ToString((float)value);
			case PrimitiveTypeCode.Double:
				return JsonConvert.ToString((double)value);
			case PrimitiveTypeCode.DateTime:
				return JsonConvert.ToString((DateTime)value);
			case PrimitiveTypeCode.DateTimeOffset:
				return JsonConvert.ToString((DateTimeOffset)value);
			case PrimitiveTypeCode.Decimal:
				return JsonConvert.ToString((decimal)value);
			case PrimitiveTypeCode.Guid:
				return JsonConvert.ToString((Guid)value);
			case PrimitiveTypeCode.TimeSpan:
				return JsonConvert.ToString((TimeSpan)value);
			case PrimitiveTypeCode.BigInteger:
				return JsonConvert.ToStringInternal((BigInteger)value);
			case PrimitiveTypeCode.Uri:
				return JsonConvert.ToString((Uri)value);
			case PrimitiveTypeCode.String:
				return JsonConvert.ToString((string)value);
			case PrimitiveTypeCode.DBNull:
				return JsonConvert.Null;
			}
			throw new ArgumentException(JsonConvert.getString_0(107351924).FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, null, null);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, Formatting formatting)
		{
			return JsonConvert.SerializeObject(value, formatting, null);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings;
			if (converters != null && converters.Length != 0)
			{
				(jsonSerializerSettings = new JsonSerializerSettings()).Converters = converters;
			}
			else
			{
				jsonSerializerSettings = null;
			}
			JsonSerializerSettings settings = jsonSerializerSettings;
			return JsonConvert.SerializeObject(value, null, settings);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, Formatting formatting, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings;
			if (converters != null && converters.Length != 0)
			{
				(jsonSerializerSettings = new JsonSerializerSettings()).Converters = converters;
			}
			else
			{
				jsonSerializerSettings = null;
			}
			JsonSerializerSettings settings = jsonSerializerSettings;
			return JsonConvert.SerializeObject(value, null, formatting, settings);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, null, settings);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, Type type, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			return JsonConvert.SerializeObjectInternal(value, type, jsonSerializer);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, null, formatting, settings);
		}

		[DebuggerStepThrough]
		public static string SerializeObject(object value, Type type, Formatting formatting, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			jsonSerializer.Formatting = formatting;
			return JsonConvert.SerializeObjectInternal(value, type, jsonSerializer);
		}

		private static string SerializeObjectInternal(object value, Type type, JsonSerializer jsonSerializer)
		{
			StringWriter stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = jsonSerializer.Formatting;
				jsonSerializer.Serialize(jsonTextWriter, value, type);
			}
			return stringWriter.ToString();
		}

		[DebuggerStepThrough]
		public static object DeserializeObject(string value)
		{
			return JsonConvert.DeserializeObject(value, null, null);
		}

		[DebuggerStepThrough]
		public static object DeserializeObject(string value, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject(value, null, settings);
		}

		[DebuggerStepThrough]
		public static object DeserializeObject(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type, null);
		}

		[DebuggerStepThrough]
		public static T DeserializeObject<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, null);
		}

		[DebuggerStepThrough]
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		[DebuggerStepThrough]
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject<T>(value, settings);
		}

		[DebuggerStepThrough]
		public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), converters));
		}

		[DebuggerStepThrough]
		public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), settings));
		}

		[DebuggerStepThrough]
		public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings;
			if (converters != null && converters.Length != 0)
			{
				(jsonSerializerSettings = new JsonSerializerSettings()).Converters = converters;
			}
			else
			{
				jsonSerializerSettings = null;
			}
			JsonSerializerSettings settings = jsonSerializerSettings;
			return JsonConvert.DeserializeObject(value, type, settings);
		}

		public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
		{
			ValidationUtils.ArgumentNotNull(value, JsonConvert.getString_0(107452579));
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			if (!jsonSerializer.IsCheckAdditionalContentSet())
			{
				jsonSerializer.CheckAdditionalContent = true;
			}
			object result;
			using (JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(value)))
			{
				result = jsonSerializer.Deserialize(jsonTextReader, type);
			}
			return result;
		}

		[DebuggerStepThrough]
		public static void PopulateObject(string value, object target)
		{
			JsonConvert.PopulateObject(value, target, null);
		}

		public static void PopulateObject(string value, object target, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(value)))
			{
				jsonSerializer.Populate(jsonReader, target);
				if (settings != null && settings.CheckAdditionalContent)
				{
					while (jsonReader.Read())
					{
						if (jsonReader.TokenType != JsonToken.Comment)
						{
							throw JsonSerializationException.Create(jsonReader, JsonConvert.getString_0(107351831));
						}
					}
				}
			}
		}

		public static string SerializeXmlNode(XmlNode node)
		{
			return JsonConvert.SerializeXmlNode(node, Formatting.None);
		}

		public static string SerializeXmlNode(XmlNode node, Formatting formatting)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static string SerializeXmlNode(XmlNode node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static XmlDocument DeserializeXmlNode(string value)
		{
			return JsonConvert.DeserializeXmlNode(value, null);
		}

		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, false);
		}

		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute, false);
		}

		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (XmlDocument)JsonConvert.DeserializeObject(value, typeof(XmlDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static string SerializeXNode(XObject node)
		{
			return JsonConvert.SerializeXNode(node, Formatting.None);
		}

		public static string SerializeXNode(XObject node, Formatting formatting)
		{
			return JsonConvert.SerializeXNode(node, formatting, false);
		}

		public static string SerializeXNode(XObject node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		public static XDocument DeserializeXNode(string value)
		{
			return JsonConvert.DeserializeXNode(value, null);
		}

		public static XDocument DeserializeXNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, false);
		}

		public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute, false);
		}

		public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (XDocument)JsonConvert.DeserializeObject(value, typeof(XDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JsonConvert()
		{
			Strings.CreateGetStringDelegate(typeof(JsonConvert));
			JsonConvert.True = JsonConvert.getString_0(107455298);
			JsonConvert.False = JsonConvert.getString_0(107351186);
			JsonConvert.Null = JsonConvert.getString_0(107384868);
			JsonConvert.Undefined = JsonConvert.getString_0(107351177);
			JsonConvert.PositiveInfinity = JsonConvert.getString_0(107351196);
			JsonConvert.NegativeInfinity = JsonConvert.getString_0(107351151);
			JsonConvert.NaN = JsonConvert.getString_0(107351170);
		}

		public static readonly string True;

		public static readonly string False;

		public static readonly string Null;

		public static readonly string Undefined;

		public static readonly string PositiveInfinity;

		public static readonly string NegativeInfinity;

		public static readonly string NaN;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
