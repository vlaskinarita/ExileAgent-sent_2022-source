using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal sealed class JsonFormatterConverter : IFormatterConverter
	{
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonFormatterConverter.getString_0(107247370));
			ValidationUtils.ArgumentNotNull(contract, JsonFormatterConverter.getString_0(107338623));
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		private T GetTokenValue<T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, JsonFormatterConverter.getString_0(107453920));
			return (T)((object)System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, JsonFormatterConverter.getString_0(107453920));
			JToken token;
			if ((token = (value as JToken)) == null)
			{
				throw new ArgumentException(JsonFormatterConverter.getString_0(107338578), JsonFormatterConverter.getString_0(107453920));
			}
			return this._reader.CreateISerializableItem(token, type, this._contract, this._member);
		}

		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, JsonFormatterConverter.getString_0(107453920));
			JValue jvalue;
			if ((jvalue = (value as JValue)) != null)
			{
				value = jvalue.Value;
			}
			return System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		static JsonFormatterConverter()
		{
			Strings.CreateGetStringDelegate(typeof(JsonFormatterConverter));
		}

		private readonly JsonSerializerInternalReader _reader;

		private readonly JsonISerializableContract _contract;

		private readonly JsonProperty _member;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
