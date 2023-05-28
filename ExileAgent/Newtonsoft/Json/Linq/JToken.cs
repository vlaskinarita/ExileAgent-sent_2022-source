using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public abstract class JToken : IEnumerable, IEnumerable<JToken>, IJEnumerable<JToken>, IDynamicMetaObjectProvider, ICloneable, IJsonLineInfo
	{
		public virtual Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			throw new NotImplementedException();
		}

		public Task WriteToAsync(JsonWriter writer, params JsonConverter[] converters)
		{
			return this.WriteToAsync(writer, default(CancellationToken), converters);
		}

		public static Task<JToken> ReadFromAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.ReadFromAsync(reader, null, cancellationToken);
		}

		public static async Task<JToken> ReadFromAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, JToken.<ReadFromAsync>d__3.getString_0(107247709));
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = ((settings == null || settings.CommentHandling != CommentHandling.Ignore) ? reader.ReadAsync(cancellationToken) : reader.ReadAndMoveToContentAsync(cancellationToken)).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, JToken.<ReadFromAsync>d__3.getString_0(107291664));
				}
			}
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
			{
				ConfiguredTaskAwaitable<JObject>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = JObject.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					ConfiguredTaskAwaitable<JObject>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<JObject>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter3.GetResult();
			}
			case JsonToken.StartArray:
			{
				ConfiguredTaskAwaitable<JArray>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = JArray.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter5.IsCompleted)
				{
					await configuredTaskAwaiter5;
					ConfiguredTaskAwaitable<JArray>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
					configuredTaskAwaiter5 = configuredTaskAwaiter6;
					configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<JArray>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter5.GetResult();
			}
			case JsonToken.StartConstructor:
			{
				ConfiguredTaskAwaitable<JConstructor>.ConfiguredTaskAwaiter configuredTaskAwaiter7 = JConstructor.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter7.IsCompleted)
				{
					await configuredTaskAwaiter7;
					ConfiguredTaskAwaitable<JConstructor>.ConfiguredTaskAwaiter configuredTaskAwaiter8;
					configuredTaskAwaiter7 = configuredTaskAwaiter8;
					configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<JConstructor>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter7.GetResult();
			}
			case JsonToken.PropertyName:
			{
				ConfiguredTaskAwaitable<JProperty>.ConfiguredTaskAwaiter configuredTaskAwaiter9 = JProperty.LoadAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter9.IsCompleted)
				{
					await configuredTaskAwaiter9;
					ConfiguredTaskAwaitable<JProperty>.ConfiguredTaskAwaiter configuredTaskAwaiter10;
					configuredTaskAwaiter9 = configuredTaskAwaiter10;
					configuredTaskAwaiter10 = default(ConfiguredTaskAwaitable<JProperty>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter9.GetResult();
			}
			case JsonToken.Comment:
			{
				JValue jvalue = JValue.CreateComment(reader.Value.ToString());
				jvalue.SetLineInfo(lineInfo, settings);
				return jvalue;
			}
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				JValue jvalue2 = new JValue(reader.Value);
				jvalue2.SetLineInfo(lineInfo, settings);
				return jvalue2;
			}
			case JsonToken.Null:
			{
				JValue jvalue3 = JValue.CreateNull();
				jvalue3.SetLineInfo(lineInfo, settings);
				return jvalue3;
			}
			case JsonToken.Undefined:
			{
				JValue jvalue4 = JValue.CreateUndefined();
				jvalue4.SetLineInfo(lineInfo, settings);
				return jvalue4;
			}
			}
			throw JsonReaderException.Create(reader, JToken.<ReadFromAsync>d__3.getString_0(107291579).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		public static Task<JToken> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.LoadAsync(reader, null, cancellationToken);
		}

		public static Task<JToken> LoadAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JToken.ReadFromAsync(reader, settings, cancellationToken);
		}

		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		public JToken Root
		{
			get
			{
				JContainer parent = this.Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		internal abstract JToken CloneToken();

		internal abstract bool DeepEquals(JToken node);

		public abstract JTokenType Type { get; }

		public abstract bool HasValues { get; }

		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		public JToken Next
		{
			get
			{
				return this._next;
			}
			internal set
			{
				this._next = value;
			}
		}

		public JToken Previous
		{
			get
			{
				return this._previous;
			}
			internal set
			{
				this._previous = value;
			}
		}

		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return string.Empty;
				}
				List<JsonPosition> list = new List<JsonPosition>();
				JToken jtoken = null;
				for (JToken jtoken2 = this; jtoken2 != null; jtoken2 = jtoken2.Parent)
				{
					JTokenType type = jtoken2.Type;
					if (type - JTokenType.Array > 1)
					{
						if (type == JTokenType.Property)
						{
							JProperty jproperty = (JProperty)jtoken2;
							List<JsonPosition> list2 = list;
							JsonPosition item = new JsonPosition(JsonContainerType.Object)
							{
								PropertyName = jproperty.Name
							};
							list2.Add(item);
						}
					}
					else if (jtoken != null)
					{
						int position = ((IList<JToken>)jtoken2).IndexOf(jtoken);
						List<JsonPosition> list3 = list;
						JsonPosition item = new JsonPosition(JsonContainerType.Array)
						{
							Position = position
						};
						list3.Add(item);
					}
					jtoken = jtoken2;
				}
				list.FastReverse<JsonPosition>();
				return JsonPosition.BuildPath(list, null);
			}
		}

		internal JToken()
		{
		}

		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException(JToken.getString_0(107292225));
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content, false);
		}

		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException(JToken.getString_0(107292225));
			}
			int index = this._parent.IndexOfItem(this);
			this._parent.AddInternal(index, content, false);
		}

		public IEnumerable<JToken> Ancestors()
		{
			return this.GetAncestors(false);
		}

		public IEnumerable<JToken> AncestorsAndSelf()
		{
			return this.GetAncestors(true);
		}

		internal IEnumerable<JToken> GetAncestors(bool self)
		{
			JToken.<GetAncestors>d__48 <GetAncestors>d__ = new JToken.<GetAncestors>d__48(-2);
			<GetAncestors>d__.<>4__this = this;
			<GetAncestors>d__.<>3__self = self;
			return <GetAncestors>d__;
		}

		public IEnumerable<JToken> AfterSelf()
		{
			JToken.<AfterSelf>d__49 <AfterSelf>d__ = new JToken.<AfterSelf>d__49(-2);
			<AfterSelf>d__.<>4__this = this;
			return <AfterSelf>d__;
		}

		public IEnumerable<JToken> BeforeSelf()
		{
			JToken.<BeforeSelf>d__50 <BeforeSelf>d__ = new JToken.<BeforeSelf>d__50(-2);
			<BeforeSelf>d__.<>4__this = this;
			return <BeforeSelf>d__;
		}

		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException(JToken.getString_0(107292704).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			set
			{
				throw new InvalidOperationException(JToken.getString_0(107292627).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		public virtual T Value<T>(object key)
		{
			JToken jtoken = this[key];
			if (jtoken != null)
			{
				return jtoken.Convert<JToken, T>();
			}
			return default(T);
		}

		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException(JToken.getString_0(107292704).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException(JToken.getString_0(107292704).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(this.Children().OfType<T>());
		}

		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException(JToken.getString_0(107292704).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
		}

		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException(JToken.getString_0(107292225));
			}
			this._parent.RemoveItem(this);
		}

		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException(JToken.getString_0(107292225));
			}
			this._parent.ReplaceItem(this, value);
		}

		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				result = stringWriter.ToString();
			}
			return result;
		}

		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(JToken.getString_0(107454246));
			}
			JProperty jproperty;
			if ((jproperty = (value as JProperty)) != null)
			{
				value = jproperty.Value;
			}
			return value as JValue;
		}

		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, JToken.getString_0(107346716));
			JProperty jproperty;
			if ((jproperty = (token as JProperty)) != null)
			{
				token = jproperty.Value;
			}
			return token.Type.ToString();
		}

		private static bool ValidateToken(JToken o, JTokenType[] validTypes, bool nullable)
		{
			return Array.IndexOf<JTokenType>(validTypes, o.Type) != -1 || (nullable && (o.Type == JTokenType.Null || o.Type == JTokenType.Undefined));
		}

		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292586).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return Convert.ToBoolean((int)value3);
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292573).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is DateTimeOffset)
			{
				return (DateTimeOffset)value2;
			}
			string input;
			if ((input = (jvalue.Value as string)) != null)
			{
				return DateTimeOffset.Parse(input, CultureInfo.InvariantCulture);
			}
			return new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292586).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new bool?(Convert.ToBoolean((int)value3));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292520).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (long)value3;
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292479).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is DateTimeOffset)
			{
				return new DateTime?(((DateTimeOffset)value2).DateTime);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292573).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			object value2;
			if ((value2 = jvalue.Value) is DateTimeOffset)
			{
				DateTimeOffset value3 = (DateTimeOffset)value2;
				return new DateTimeOffset?(value3);
			}
			string input;
			if ((input = (jvalue.Value as string)) != null)
			{
				return new DateTimeOffset?(DateTimeOffset.Parse(input, CultureInfo.InvariantCulture));
			}
			return new DateTimeOffset?(new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291890).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new decimal?((decimal)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291877).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new double?((double)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new double?(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator char?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291836).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new char?((char)((ushort)value3));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new char?(Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291763).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (int)value3;
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291722).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (short)value3;
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291713).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (ushort)value3;
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator char(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291836).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (char)((ushort)value3);
			}
			return Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator byte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292152).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (byte)value3;
			}
			return Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator sbyte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292111).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (sbyte)value3;
			}
			return Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291763).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new int?((int)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291722).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new short?((short)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		[CLSCompliant(false)]
		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291713).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new ushort?((ushort)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ushort?(Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator byte?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292152).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new byte?((byte)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new byte?(Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		[CLSCompliant(false)]
		public static explicit operator sbyte?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292111).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new sbyte?((sbyte)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new sbyte?(Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292479).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is DateTimeOffset)
			{
				return ((DateTimeOffset)value2).DateTime;
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292520).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new long?((long)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292102).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new float?((float)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291890).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (decimal)value3;
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107292061).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new uint?((uint)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291988).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return new ulong?((ulong)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291877).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (double)value3;
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292102).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (float)value3;
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.StringTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291947).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] inArray;
			if ((inArray = (jvalue.Value as byte[])) != null)
			{
				return Convert.ToBase64String(inArray);
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				return ((BigInteger)value2).ToString(CultureInfo.InvariantCulture);
			}
			return Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107292061).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (uint)value3;
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291988).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				BigInteger value3 = (BigInteger)value2;
				return (ulong)value3;
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator byte[](JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BytesTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291426).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is string)
			{
				return Convert.FromBase64String(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			object value2;
			if ((value2 = jvalue.Value) is BigInteger)
			{
				return ((BigInteger)value2).ToByteArray();
			}
			byte[] result;
			if ((result = (jvalue.Value as byte[])) == null)
			{
				throw new ArgumentException(JToken.getString_0(107291426).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return result;
		}

		public static explicit operator Guid(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291345).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			byte[] b;
			if ((b = (jvalue.Value as byte[])) != null)
			{
				return new Guid(b);
			}
			object value2;
			if ((value2 = jvalue.Value) is Guid)
			{
				return (Guid)value2;
			}
			return new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator Guid?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291345).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] b;
			if ((b = (jvalue.Value as byte[])) != null)
			{
				return new Guid?(new Guid(b));
			}
			object value2;
			Guid value3;
			if ((value2 = jvalue.Value) is Guid)
			{
				Guid guid = (Guid)value2;
				value3 = guid;
			}
			else
			{
				value3 = new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new Guid?(value3);
		}

		public static explicit operator TimeSpan(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291336).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2;
			if ((value2 = jvalue.Value) is TimeSpan)
			{
				return (TimeSpan)value2;
			}
			return ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		public static explicit operator TimeSpan?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291336).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			object value2;
			TimeSpan value3;
			if ((value2 = jvalue.Value) is TimeSpan)
			{
				TimeSpan timeSpan = (TimeSpan)value2;
				value3 = timeSpan;
			}
			else
			{
				value3 = ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new TimeSpan?(value3);
		}

		public static explicit operator Uri(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.UriTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291291).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			Uri result;
			if ((result = (jvalue.Value as Uri)) == null)
			{
				return new Uri(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return result;
		}

		private static BigInteger ToBigInteger(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, false))
			{
				throw new ArgumentException(JToken.getString_0(107291222).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return ConvertUtils.ToBigInteger(jvalue.Value);
		}

		private static BigInteger? ToBigIntegerNullable(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, true))
			{
				throw new ArgumentException(JToken.getString_0(107291222).FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new BigInteger?(ConvertUtils.ToBigInteger(jvalue.Value));
		}

		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(byte value)
		{
			return new JValue((long)((ulong)value));
		}

		public static implicit operator JToken(byte? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte value)
		{
			return new JValue((long)value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(Uri value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(TimeSpan value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(TimeSpan? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(Guid value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(Guid? value)
		{
			return new JValue(value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<JToken>)this).GetEnumerator();
		}

		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		internal abstract int GetDeepHashCode();

		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, JToken.getString_0(107293761));
			ValidationUtils.ArgumentNotNull(jsonSerializer, JToken.getString_0(107291205));
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, JsonSerializer.CreateDefault());
		}

		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		public T ToObject<T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		public object ToObject(Type objectType)
		{
			if (JsonConvert.DefaultSettings == null)
			{
				bool flag;
				PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(objectType, out flag);
				if (flag)
				{
					if (this.Type == JTokenType.String)
					{
						object result;
						try
						{
							result = this.ToObject(objectType, JsonSerializer.CreateDefault());
						}
						catch (Exception innerException)
						{
							Type type = objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType);
							throw new ArgumentException(JToken.getString_0(107291664).FormatWith(CultureInfo.InvariantCulture, (string)this, type.Name), innerException);
						}
						return result;
					}
					if (this.Type == JTokenType.Integer)
					{
						return Enum.ToObject(objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType), ((JValue)this).Value);
					}
				}
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					return (char)this;
				case PrimitiveTypeCode.CharNullable:
					return (char?)this;
				case PrimitiveTypeCode.Boolean:
					return (bool)this;
				case PrimitiveTypeCode.BooleanNullable:
					return (bool?)this;
				case PrimitiveTypeCode.SByte:
					return (sbyte)this;
				case PrimitiveTypeCode.SByteNullable:
					return (sbyte?)this;
				case PrimitiveTypeCode.Int16:
					return (short)this;
				case PrimitiveTypeCode.Int16Nullable:
					return (short?)this;
				case PrimitiveTypeCode.UInt16:
					return (ushort)this;
				case PrimitiveTypeCode.UInt16Nullable:
					return (ushort?)this;
				case PrimitiveTypeCode.Int32:
					return (int)this;
				case PrimitiveTypeCode.Int32Nullable:
					return (int?)this;
				case PrimitiveTypeCode.Byte:
					return (byte)this;
				case PrimitiveTypeCode.ByteNullable:
					return (byte?)this;
				case PrimitiveTypeCode.UInt32:
					return (uint)this;
				case PrimitiveTypeCode.UInt32Nullable:
					return (uint?)this;
				case PrimitiveTypeCode.Int64:
					return (long)this;
				case PrimitiveTypeCode.Int64Nullable:
					return (long?)this;
				case PrimitiveTypeCode.UInt64:
					return (ulong)this;
				case PrimitiveTypeCode.UInt64Nullable:
					return (ulong?)this;
				case PrimitiveTypeCode.Single:
					return (float)this;
				case PrimitiveTypeCode.SingleNullable:
					return (float?)this;
				case PrimitiveTypeCode.Double:
					return (double)this;
				case PrimitiveTypeCode.DoubleNullable:
					return (double?)this;
				case PrimitiveTypeCode.DateTime:
					return (DateTime)this;
				case PrimitiveTypeCode.DateTimeNullable:
					return (DateTime?)this;
				case PrimitiveTypeCode.DateTimeOffset:
					return (DateTimeOffset)this;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					return (DateTimeOffset?)this;
				case PrimitiveTypeCode.Decimal:
					return (decimal)this;
				case PrimitiveTypeCode.DecimalNullable:
					return (decimal?)this;
				case PrimitiveTypeCode.Guid:
					return (Guid)this;
				case PrimitiveTypeCode.GuidNullable:
					return (Guid?)this;
				case PrimitiveTypeCode.TimeSpan:
					return (TimeSpan)this;
				case PrimitiveTypeCode.TimeSpanNullable:
					return (TimeSpan?)this;
				case PrimitiveTypeCode.BigInteger:
					return JToken.ToBigInteger(this);
				case PrimitiveTypeCode.BigIntegerNullable:
					return JToken.ToBigIntegerNullable(this);
				case PrimitiveTypeCode.Uri:
					return (Uri)this;
				case PrimitiveTypeCode.String:
					return (string)this;
				}
			}
			return this.ToObject(objectType, JsonSerializer.CreateDefault());
		}

		public T ToObject<T>(JsonSerializer jsonSerializer)
		{
			return (T)((object)this.ToObject(typeof(T), jsonSerializer));
		}

		public object ToObject(Type objectType, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, JToken.getString_0(107291205));
			object result;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				result = jsonSerializer.Deserialize(jtokenReader, objectType);
			}
			return result;
		}

		public static JToken ReadFrom(JsonReader reader)
		{
			return JToken.ReadFrom(reader, null);
		}

		public static JToken ReadFrom(JsonReader reader, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, JToken.getString_0(107247696));
			bool flag;
			if (reader.TokenType == JsonToken.None)
			{
				flag = ((settings == null || settings.CommentHandling != CommentHandling.Ignore) ? reader.Read() : reader.ReadAndMoveToContent());
			}
			else
			{
				flag = (reader.TokenType != JsonToken.Comment || settings == null || settings.CommentHandling != CommentHandling.Ignore || reader.ReadAndMoveToContent());
			}
			if (!flag)
			{
				throw JsonReaderException.Create(reader, JToken.getString_0(107291651));
			}
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return JObject.Load(reader, settings);
			case JsonToken.StartArray:
				return JArray.Load(reader, settings);
			case JsonToken.StartConstructor:
				return JConstructor.Load(reader, settings);
			case JsonToken.PropertyName:
				return JProperty.Load(reader, settings);
			case JsonToken.Comment:
			{
				JValue jvalue = JValue.CreateComment(reader.Value.ToString());
				jvalue.SetLineInfo(lineInfo, settings);
				return jvalue;
			}
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				JValue jvalue2 = new JValue(reader.Value);
				jvalue2.SetLineInfo(lineInfo, settings);
				return jvalue2;
			}
			case JsonToken.Null:
			{
				JValue jvalue3 = JValue.CreateNull();
				jvalue3.SetLineInfo(lineInfo, settings);
				return jvalue3;
			}
			case JsonToken.Undefined:
			{
				JValue jvalue4 = JValue.CreateUndefined();
				jvalue4.SetLineInfo(lineInfo, settings);
				return jvalue4;
			}
			}
			throw JsonReaderException.Create(reader, JToken.getString_0(107291566).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		public static JToken Parse(string json)
		{
			return JToken.Parse(json, null);
		}

		public static JToken Parse(string json, JsonLoadSettings settings)
		{
			JToken result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JToken jtoken = JToken.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jtoken;
			}
			return result;
		}

		public static JToken Load(JsonReader reader, JsonLoadSettings settings)
		{
			return JToken.ReadFrom(reader, settings);
		}

		public static JToken Load(JsonReader reader)
		{
			return JToken.Load(reader, null);
		}

		internal void SetLineInfo(IJsonLineInfo lineInfo, JsonLoadSettings settings)
		{
			if (settings != null && settings.LineInfoHandling != LineInfoHandling.Load)
			{
				return;
			}
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
				return;
			}
		}

		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new JToken.LineInfoAnnotation(lineNumber, linePosition));
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			return this.Annotation<JToken.LineInfoAnnotation>() != null;
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LineNumber;
				}
				return 0;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LinePosition;
				}
				return 0;
			}
		}

		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			JToken jtoken = null;
			foreach (JToken jtoken2 in jpath.Evaluate(this, this, errorWhenNoMatch))
			{
				if (jtoken != null)
				{
					throw new JsonException(JToken.getString_0(107291517));
				}
				jtoken = jtoken2;
			}
			return jtoken;
		}

		public IEnumerable<JToken> SelectTokens(string path)
		{
			return this.SelectTokens(path, false);
		}

		public IEnumerable<JToken> SelectTokens(string path, bool errorWhenNoMatch)
		{
			return new JPath(path).Evaluate(this, this, errorWhenNoMatch);
		}

		protected virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JToken>(parameter, this, new DynamicProxy<JToken>());
		}

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return this.GetMetaObject(parameter);
		}

		object ICloneable.Clone()
		{
			return this.DeepClone();
		}

		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException(JToken.getString_0(107291444));
			}
			if (this._annotations == null)
			{
				object annotations;
				if (!(annotation is object[]))
				{
					annotations = annotation;
				}
				else
				{
					(annotations = new object[1])[0] = annotation;
				}
				this._annotations = annotations;
				return;
			}
			object[] array;
			if ((array = (this._annotations as object[])) == null)
			{
				this._annotations = new object[]
				{
					this._annotations,
					annotation
				};
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array;
				if ((array = (this._annotations as object[])) == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T result;
					if ((result = (obj as T)) != null)
					{
						return result;
					}
				}
			}
			return default(T);
		}

		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(JToken.getString_0(107374572));
			}
			if (this._annotations != null)
			{
				object[] array;
				if ((array = (this._annotations as object[])) == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this._annotations == null)
			{
				yield break;
			}
			object[] annotations;
			if ((annotations = (this._annotations as object[])) != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					T t;
					if ((t = (obj as T)) != null)
					{
						yield return t;
					}
					num = i;
				}
				yield break;
			}
			T t2;
			if ((t2 = (this._annotations as T)) == null)
			{
				yield break;
			}
			yield return t2;
			yield break;
		}

		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(JToken.<Annotations>d__183.getString_0(107374617));
			}
			if (this._annotations == null)
			{
				yield break;
			}
			object[] annotations;
			if ((annotations = (this._annotations as object[])) != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					if (type.IsInstanceOfType(obj))
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}
			if (!type.IsInstanceOfType(this._annotations))
			{
				yield break;
			}
			yield return this._annotations;
			yield break;
		}

		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array;
				if ((array = (this._annotations as object[])) == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(JToken.getString_0(107374572));
			}
			if (this._annotations != null)
			{
				object[] array;
				if ((array = (this._annotations as object[])) == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JToken()
		{
			Strings.CreateGetStringDelegate(typeof(JToken));
			JToken.BooleanTypes = new JTokenType[]
			{
				JTokenType.Integer,
				JTokenType.Float,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Boolean
			};
			JToken.NumberTypes = new JTokenType[]
			{
				JTokenType.Integer,
				JTokenType.Float,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Boolean
			};
			JToken.BigIntegerTypes = new JTokenType[]
			{
				JTokenType.Integer,
				JTokenType.Float,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Boolean,
				JTokenType.Bytes
			};
			JToken.StringTypes = new JTokenType[]
			{
				JTokenType.Date,
				JTokenType.Integer,
				JTokenType.Float,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Boolean,
				JTokenType.Bytes,
				JTokenType.Guid,
				JTokenType.TimeSpan,
				JTokenType.Uri
			};
			JToken.GuidTypes = new JTokenType[]
			{
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Guid,
				JTokenType.Bytes
			};
			JToken.TimeSpanTypes = new JTokenType[]
			{
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.TimeSpan
			};
			JToken.UriTypes = new JTokenType[]
			{
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Uri
			};
			JToken.CharTypes = new JTokenType[]
			{
				JTokenType.Integer,
				JTokenType.Float,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw
			};
			JToken.DateTimeTypes = new JTokenType[]
			{
				JTokenType.Date,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw
			};
			JToken.BytesTypes = new JTokenType[]
			{
				JTokenType.Bytes,
				JTokenType.String,
				JTokenType.Comment,
				JTokenType.Raw,
				JTokenType.Integer
			};
		}

		private static JTokenEqualityComparer _equalityComparer;

		private JContainer _parent;

		private JToken _previous;

		private JToken _next;

		private object _annotations;

		private static readonly JTokenType[] BooleanTypes;

		private static readonly JTokenType[] NumberTypes;

		private static readonly JTokenType[] BigIntegerTypes;

		private static readonly JTokenType[] StringTypes;

		private static readonly JTokenType[] GuidTypes;

		private static readonly JTokenType[] TimeSpanTypes;

		private static readonly JTokenType[] UriTypes;

		private static readonly JTokenType[] CharTypes;

		private static readonly JTokenType[] DateTimeTypes;

		private static readonly JTokenType[] BytesTypes;

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class LineInfoAnnotation
		{
			public LineInfoAnnotation(int lineNumber, int linePosition)
			{
				this.LineNumber = lineNumber;
				this.LinePosition = linePosition;
			}

			internal readonly int LineNumber;

			internal readonly int LinePosition;
		}
	}
}
