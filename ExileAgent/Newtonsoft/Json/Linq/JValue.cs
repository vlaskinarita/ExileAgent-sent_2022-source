using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public class JValue : JToken, IEquatable<JValue>, IComparable<JValue>, IComparable, IConvertible, IFormattable
	{
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return AsyncUtils.CompletedTask;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				return writer.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			}
			case JTokenType.Integer:
			{
				object value2;
				if ((value2 = this._value) is int)
				{
					int value3 = (int)value2;
					return writer.WriteValueAsync(value3, cancellationToken);
				}
				if ((value2 = this._value) is long)
				{
					long value4 = (long)value2;
					return writer.WriteValueAsync(value4, cancellationToken);
				}
				if ((value2 = this._value) is ulong)
				{
					ulong value5 = (ulong)value2;
					return writer.WriteValueAsync(value5, cancellationToken);
				}
				if ((value2 = this._value) is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					return writer.WriteValueAsync(bigInteger, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToInt64(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Float:
			{
				object value2;
				if ((value2 = this._value) is decimal)
				{
					decimal value6 = (decimal)value2;
					return writer.WriteValueAsync(value6, cancellationToken);
				}
				if ((value2 = this._value) is double)
				{
					double value7 = (double)value2;
					return writer.WriteValueAsync(value7, cancellationToken);
				}
				if ((value2 = this._value) is float)
				{
					float value8 = (float)value2;
					return writer.WriteValueAsync(value8, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDouble(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				return writer.WriteValueAsync((value9 != null) ? value9.ToString() : null, cancellationToken);
			}
			case JTokenType.Boolean:
				return writer.WriteValueAsync(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture), cancellationToken);
			case JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case JTokenType.Date:
			{
				object value2;
				if ((value2 = this._value) is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					return writer.WriteValueAsync(value10, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				return writer.WriteRawValueAsync((value11 != null) ? value11.ToString() : null, cancellationToken);
			}
			case JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])this._value, cancellationToken);
			case JTokenType.Guid:
				return writer.WriteValueAsync((this._value != null) ? ((Guid?)this._value) : null, cancellationToken);
			case JTokenType.Uri:
				return writer.WriteValueAsync((Uri)this._value, cancellationToken);
			case JTokenType.TimeSpan:
				return writer.WriteValueAsync((this._value != null) ? ((TimeSpan?)this._value) : null, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JValue.getString_1(107254450), this._valueType, JValue.getString_1(107346864));
			}
		}

		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		public JValue(decimal value) : this(value, JTokenType.Float)
		{
		}

		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		public JValue(DateTimeOffset value) : this(value, JTokenType.Date)
		{
		}

		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		public JValue(object value) : this(value, JValue.GetValueType(null, value))
		{
		}

		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue;
			return (jvalue = (node as JValue)) != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (!(i2 is double) && !(i2 is float))
			{
				return num;
			}
			double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
			return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
		}

		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string strA = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string strB = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(strA, strB);
			}
			case JTokenType.Integer:
				if (objA is BigInteger)
				{
					BigInteger i = (BigInteger)objA;
					return JValue.CompareBigInteger(i, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i2 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (!(objA is float) && !(objB is float) && !(objA is double) && !(objB is double))
				{
					return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Float:
				if (objA is BigInteger)
				{
					BigInteger i3 = (BigInteger)objA;
					return JValue.CompareBigInteger(i3, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i4 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i4, objA);
				}
				if (!(objA is ulong) && !(objB is ulong) && !(objA is decimal) && !(objB is decimal))
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool value = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(value);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime value2;
					if (objB is DateTimeOffset)
					{
						value2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						value2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(value2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset other;
				if (objB is DateTimeOffset)
				{
					other = (DateTimeOffset)objB;
				}
				else
				{
					other = new DateTimeOffset(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(other);
			}
			case JTokenType.Bytes:
			{
				byte[] a;
				if ((a = (objB as byte[])) == null)
				{
					throw new ArgumentException(JValue.getString_1(107291021));
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], a);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException(JValue.getString_1(107291012));
				}
				Guid guid = (Guid)objA;
				Guid value3 = (Guid)objB;
				return guid.CompareTo(value3);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException(JValue.getString_1(107290971));
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException(JValue.getString_1(107290934));
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan value4 = (TimeSpan)objB;
				return timeSpan.CompareTo(value4);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JValue.getString_1(107290857), valueType, JValue.getString_1(107290876).FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if (objA is string || objB is string)
			{
				if (operation != ExpressionType.Add)
				{
					if (operation != ExpressionType.AddAssign)
					{
						goto IL_20;
					}
				}
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			IL_20:
			if (!(objA is BigInteger) && !(objB is BigInteger))
			{
				if (!(objA is ulong) && !(objB is ulong) && !(objA is decimal) && !(objB is decimal))
				{
					if (!(objA is float) && !(objB is float) && !(objA is double) && !(objB is double))
					{
						if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
						{
							if (objA != null && objB != null)
							{
								long num = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
								long num2 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
								if (operation <= ExpressionType.Subtract)
								{
									if (operation <= ExpressionType.Divide)
									{
										if (operation == ExpressionType.Add)
										{
											goto IL_16F;
										}
										if (operation != ExpressionType.Divide)
										{
											goto IL_380;
										}
									}
									else
									{
										if (operation == ExpressionType.Multiply)
										{
											goto IL_19A;
										}
										if (operation != ExpressionType.Subtract)
										{
											goto IL_380;
										}
										goto IL_18C;
									}
								}
								else if (operation <= ExpressionType.DivideAssign)
								{
									if (operation == ExpressionType.AddAssign)
									{
										goto IL_16F;
									}
									if (operation != ExpressionType.DivideAssign)
									{
										goto IL_380;
									}
								}
								else
								{
									if (operation == ExpressionType.MultiplyAssign)
									{
										goto IL_19A;
									}
									if (operation != ExpressionType.SubtractAssign)
									{
										goto IL_380;
									}
									goto IL_18C;
								}
								result = num / num2;
								return true;
								IL_16F:
								result = num + num2;
								return true;
								IL_18C:
								result = num - num2;
								return true;
								IL_19A:
								result = num * num2;
								return true;
							}
							result = null;
							return true;
						}
					}
					else
					{
						if (objA != null && objB != null)
						{
							double num3 = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
							double num4 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
							if (operation <= ExpressionType.Subtract)
							{
								if (operation <= ExpressionType.Divide)
								{
									if (operation == ExpressionType.Add)
									{
										goto IL_21B;
									}
									if (operation != ExpressionType.Divide)
									{
										goto IL_380;
									}
								}
								else
								{
									if (operation == ExpressionType.Multiply)
									{
										goto IL_246;
									}
									if (operation != ExpressionType.Subtract)
									{
										goto IL_380;
									}
									goto IL_238;
								}
							}
							else if (operation <= ExpressionType.DivideAssign)
							{
								if (operation == ExpressionType.AddAssign)
								{
									goto IL_21B;
								}
								if (operation != ExpressionType.DivideAssign)
								{
									goto IL_380;
								}
							}
							else
							{
								if (operation == ExpressionType.MultiplyAssign)
								{
									goto IL_246;
								}
								if (operation != ExpressionType.SubtractAssign)
								{
									goto IL_380;
								}
								goto IL_238;
							}
							result = num3 / num4;
							return true;
							IL_21B:
							result = num3 + num4;
							return true;
							IL_238:
							result = num3 - num4;
							return true;
							IL_246:
							result = num3 * num4;
							return true;
						}
						result = null;
						return true;
					}
				}
				else
				{
					if (objA != null && objB != null)
					{
						decimal d = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
						decimal d2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
						if (operation <= ExpressionType.Subtract)
						{
							if (operation <= ExpressionType.Divide)
							{
								if (operation == ExpressionType.Add)
								{
									goto IL_2C7;
								}
								if (operation != ExpressionType.Divide)
								{
									goto IL_380;
								}
							}
							else
							{
								if (operation == ExpressionType.Multiply)
								{
									goto IL_2F6;
								}
								if (operation != ExpressionType.Subtract)
								{
									goto IL_380;
								}
								goto IL_2E6;
							}
						}
						else if (operation <= ExpressionType.DivideAssign)
						{
							if (operation == ExpressionType.AddAssign)
							{
								goto IL_2C7;
							}
							if (operation != ExpressionType.DivideAssign)
							{
								goto IL_380;
							}
						}
						else
						{
							if (operation == ExpressionType.MultiplyAssign)
							{
								goto IL_2F6;
							}
							if (operation != ExpressionType.SubtractAssign)
							{
								goto IL_380;
							}
							goto IL_2E6;
						}
						result = d / d2;
						return true;
						IL_2C7:
						result = d + d2;
						return true;
						IL_2E6:
						result = d - d2;
						return true;
						IL_2F6:
						result = d * d2;
						return true;
					}
					result = null;
					return true;
				}
			}
			else
			{
				if (objA != null && objB != null)
				{
					BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
					BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
					if (operation <= ExpressionType.Subtract)
					{
						if (operation <= ExpressionType.Divide)
						{
							if (operation == ExpressionType.Add)
							{
								goto IL_366;
							}
							if (operation != ExpressionType.Divide)
							{
								goto IL_380;
							}
						}
						else
						{
							if (operation == ExpressionType.Multiply)
							{
								goto IL_395;
							}
							if (operation != ExpressionType.Subtract)
							{
								goto IL_380;
							}
							goto IL_385;
						}
					}
					else if (operation <= ExpressionType.DivideAssign)
					{
						if (operation == ExpressionType.AddAssign)
						{
							goto IL_366;
						}
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_380;
						}
					}
					else
					{
						if (operation == ExpressionType.MultiplyAssign)
						{
							goto IL_395;
						}
						if (operation != ExpressionType.SubtractAssign)
						{
							goto IL_380;
						}
						goto IL_385;
					}
					result = bigInteger / bigInteger2;
					return true;
					IL_366:
					result = bigInteger + bigInteger2;
					return true;
					IL_385:
					result = bigInteger - bigInteger2;
					return true;
					IL_395:
					result = bigInteger * bigInteger2;
					return true;
				}
				result = null;
				return true;
			}
			IL_380:
			result = null;
			return false;
		}

		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException(JValue.getString_1(107291351).FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault != JTokenType.Comment && valueOrDefault != JTokenType.String)
			{
				if (valueOrDefault != JTokenType.Raw)
				{
					return JTokenType.String;
				}
			}
			return current.GetValueOrDefault();
		}

		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				object value2 = this._value;
				Type left = (value2 != null) ? value2.GetType() : null;
				Type right = (value != null) ? value.GetType() : null;
				if (left != right)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object value2;
				if ((value2 = this._value) is int)
				{
					int value3 = (int)value2;
					writer.WriteValue(value3);
					return;
				}
				if ((value2 = this._value) is long)
				{
					long value4 = (long)value2;
					writer.WriteValue(value4);
					return;
				}
				if ((value2 = this._value) is ulong)
				{
					ulong value5 = (ulong)value2;
					writer.WriteValue(value5);
					return;
				}
				if ((value2 = this._value) is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					writer.WriteValue(bigInteger);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object value2;
				if ((value2 = this._value) is decimal)
				{
					decimal value6 = (decimal)value2;
					writer.WriteValue(value6);
					return;
				}
				if ((value2 = this._value) is double)
				{
					double value7 = (double)value2;
					writer.WriteValue(value7);
					return;
				}
				if ((value2 = this._value) is float)
				{
					float value8 = (float)value2;
					writer.WriteValue(value8);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				writer.WriteValue((value9 != null) ? value9.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object value2;
				if ((value2 = this._value) is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					writer.WriteValue(value10);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				writer.WriteRawValue((value11 != null) ? value11.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : null);
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JValue.getString_1(107254450), this._valueType, JValue.getString_1(107346864));
			}
		}

		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as JValue);
		}

		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable;
			if ((formattable = (this._value as IFormattable)) != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue;
			object objB;
			JTokenType valueType2;
			if ((jvalue = (obj as JValue)) != null)
			{
				objB = jvalue.Value;
				JTokenType valueType;
				if (this._valueType == JTokenType.String)
				{
					if (this._valueType != jvalue._valueType)
					{
						valueType = jvalue._valueType;
						goto IL_3D;
					}
				}
				valueType = this._valueType;
				IL_3D:
				valueType2 = valueType;
			}
			else
			{
				objB = obj;
				valueType2 = this._valueType;
			}
			return JValue.Compare(valueType2, this._value, objB);
		}

		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JTokenType valueType;
			if (this._valueType == JTokenType.String)
			{
				if (this._valueType != obj._valueType)
				{
					valueType = obj._valueType;
					goto IL_2C;
				}
			}
			valueType = this._valueType;
			IL_2C:
			return JValue.Compare(valueType, this._value, obj._value);
		}

		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible;
			if ((convertible = (this._value as IConvertible)) != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		static JValue()
		{
			Strings.CreateGetStringDelegate(typeof(JValue));
		}

		private JTokenType _valueType;

		private object _value;

		[NonSerialized]
		internal static GetString getString_1;

		private sealed class JValueDynamicProxy : DynamicProxy<JValue>
		{
			public override bool TryConvert(JValue instance, ConvertBinder binder, out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, out object result)
			{
				JValue jvalue;
				object objB = ((jvalue = (arg as JValue)) != null) ? jvalue.Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= ExpressionType.NotEqual)
				{
					if (operation <= ExpressionType.LessThanOrEqual)
					{
						if (operation != ExpressionType.Add)
						{
							switch (operation)
							{
							case ExpressionType.Divide:
								break;
							case ExpressionType.Equal:
								result = (JValue.Compare(instance.Type, instance.Value, objB) == 0);
								return true;
							case ExpressionType.ExclusiveOr:
							case ExpressionType.Invoke:
							case ExpressionType.Lambda:
							case ExpressionType.LeftShift:
								goto IL_178;
							case ExpressionType.GreaterThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) > 0);
								return true;
							case ExpressionType.GreaterThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) >= 0);
								return true;
							case ExpressionType.LessThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) < 0);
								return true;
							case ExpressionType.LessThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) <= 0);
								return true;
							default:
								goto IL_178;
							}
						}
					}
					else if (operation != ExpressionType.Multiply)
					{
						if (operation != ExpressionType.NotEqual)
						{
							goto IL_178;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= ExpressionType.AddAssign)
				{
					if (operation != ExpressionType.Subtract && operation != ExpressionType.AddAssign)
					{
						goto IL_178;
					}
				}
				else if (operation != ExpressionType.DivideAssign && operation != ExpressionType.MultiplyAssign && operation != ExpressionType.SubtractAssign)
				{
					goto IL_178;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_178:
				result = null;
				return false;
			}
		}
	}
}
