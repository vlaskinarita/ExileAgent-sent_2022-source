using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class ConvertUtils
	{
		public static PrimitiveTypeCode GetTypeCode(Type t)
		{
			bool flag;
			return ConvertUtils.GetTypeCode(t, out flag);
		}

		public static PrimitiveTypeCode GetTypeCode(Type t, out bool isEnum)
		{
			PrimitiveTypeCode result;
			if (ConvertUtils.TypeCodeMap.TryGetValue(t, out result))
			{
				isEnum = false;
				return result;
			}
			if (t.IsEnum())
			{
				isEnum = true;
				return ConvertUtils.GetTypeCode(Enum.GetUnderlyingType(t));
			}
			if (ReflectionUtils.IsNullableType(t))
			{
				Type underlyingType = Nullable.GetUnderlyingType(t);
				if (underlyingType.IsEnum())
				{
					Type t2 = typeof(Nullable<>).MakeGenericType(new Type[]
					{
						Enum.GetUnderlyingType(underlyingType)
					});
					isEnum = true;
					return ConvertUtils.GetTypeCode(t2);
				}
			}
			isEnum = false;
			return PrimitiveTypeCode.Object;
		}

		public static TypeInformation GetTypeInformation(IConvertible convertable)
		{
			return ConvertUtils.PrimitiveTypeCodes[(int)convertable.GetTypeCode()];
		}

		public static bool IsConvertible(Type t)
		{
			return typeof(IConvertible).IsAssignableFrom(t);
		}

		public static TimeSpan ParseTimeSpan(string input)
		{
			return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
		}

		private static Func<object, object> CreateCastConverter(StructMultiKey<Type, Type> t)
		{
			Type value = t.Value1;
			Type value2 = t.Value2;
			MethodInfo method;
			if ((method = value2.GetMethod(ConvertUtils.getString_0(107345529), new Type[]
			{
				value
			})) == null)
			{
				method = value2.GetMethod(ConvertUtils.getString_0(107345544), new Type[]
				{
					value
				});
			}
			MethodInfo methodInfo = method;
			if (methodInfo == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object o) => call(null, new object[]
			{
				o
			});
		}

		internal static BigInteger ToBigInteger(object value)
		{
			if (value is BigInteger)
			{
				return (BigInteger)value;
			}
			string value2;
			if ((value2 = (value as string)) != null)
			{
				return BigInteger.Parse(value2, CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				float value3 = (float)value;
				return new BigInteger(value3);
			}
			if (value is double)
			{
				double value4 = (double)value;
				return new BigInteger(value4);
			}
			if (value is decimal)
			{
				decimal value5 = (decimal)value;
				return new BigInteger(value5);
			}
			if (value is int)
			{
				int value6 = (int)value;
				return new BigInteger(value6);
			}
			if (value is long)
			{
				long value7 = (long)value;
				return new BigInteger(value7);
			}
			if (value is uint)
			{
				uint value8 = (uint)value;
				return new BigInteger(value8);
			}
			if (value is ulong)
			{
				ulong value9 = (ulong)value;
				return new BigInteger(value9);
			}
			byte[] value10;
			if ((value10 = (value as byte[])) == null)
			{
				throw new InvalidCastException(ConvertUtils.getString_0(107345495).FormatWith(CultureInfo.InvariantCulture, value.GetType()));
			}
			return new BigInteger(value10);
		}

		public static object FromBigInteger(BigInteger i, Type targetType)
		{
			if (targetType == typeof(decimal))
			{
				return (decimal)i;
			}
			if (targetType == typeof(double))
			{
				return (double)i;
			}
			if (targetType == typeof(float))
			{
				return (float)i;
			}
			if (targetType == typeof(ulong))
			{
				return (ulong)i;
			}
			if (targetType == typeof(bool))
			{
				return i != 0L;
			}
			object result;
			try
			{
				result = System.Convert.ChangeType((long)i, targetType, CultureInfo.InvariantCulture);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(ConvertUtils.getString_0(107345482).FormatWith(CultureInfo.InvariantCulture, targetType), innerException);
			}
			return result;
		}

		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			object result;
			switch (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out result))
			{
			case ConvertUtils.ConvertResult.Success:
				return result;
			case ConvertUtils.ConvertResult.CannotConvertNull:
				throw new Exception(ConvertUtils.getString_0(107344885).FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			case ConvertUtils.ConvertResult.NotInstantiableType:
				throw new ArgumentException(ConvertUtils.getString_0(107344820).FormatWith(CultureInfo.InvariantCulture, targetType), ConvertUtils.getString_0(107344739));
			case ConvertUtils.ConvertResult.NoValidConversion:
				throw new InvalidOperationException(ConvertUtils.getString_0(107344754).FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			default:
				throw new InvalidOperationException(ConvertUtils.getString_0(107344709));
			}
		}

		private static bool TryConvert(object initialValue, CultureInfo culture, Type targetType, out object value)
		{
			bool result;
			try
			{
				if (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out value) == ConvertUtils.ConvertResult.Success)
				{
					result = true;
				}
				else
				{
					value = null;
					result = false;
				}
			}
			catch
			{
				value = null;
				result = false;
			}
			return result;
		}

		private static ConvertUtils.ConvertResult TryConvertInternal(object initialValue, CultureInfo culture, Type targetType, out object value)
		{
			if (initialValue == null)
			{
				throw new ArgumentNullException(ConvertUtils.getString_0(107345148));
			}
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			Type type = initialValue.GetType();
			if (targetType == type)
			{
				value = initialValue;
				return ConvertUtils.ConvertResult.Success;
			}
			if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						value = Enum.Parse(targetType, initialValue.ToString(), true);
						return ConvertUtils.ConvertResult.Success;
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						value = Enum.ToObject(targetType, initialValue);
						return ConvertUtils.ConvertResult.Success;
					}
				}
				value = System.Convert.ChangeType(initialValue, targetType, culture);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is DateTime)
			{
				DateTime dateTime = (DateTime)initialValue;
				if (targetType == typeof(DateTimeOffset))
				{
					value = new DateTimeOffset(dateTime);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			byte[] b;
			if ((b = (initialValue as byte[])) != null && targetType == typeof(Guid))
			{
				value = new Guid(b);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is Guid)
			{
				Guid guid = (Guid)initialValue;
				if (targetType == typeof(byte[]))
				{
					value = guid.ToByteArray();
					return ConvertUtils.ConvertResult.Success;
				}
			}
			string text;
			if ((text = (initialValue as string)) != null)
			{
				if (targetType == typeof(Guid))
				{
					value = new Guid(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Uri))
				{
					value = new Uri(text, UriKind.RelativeOrAbsolute);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(TimeSpan))
				{
					value = ConvertUtils.ParseTimeSpan(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(byte[]))
				{
					value = System.Convert.FromBase64String(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Version))
				{
					Version version;
					if (ConvertUtils.VersionTryParse(text, out version))
					{
						value = version;
						return ConvertUtils.ConvertResult.Success;
					}
					value = null;
					return ConvertUtils.ConvertResult.NoValidConversion;
				}
				else if (typeof(Type).IsAssignableFrom(targetType))
				{
					value = Type.GetType(text, true);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			if (targetType == typeof(BigInteger))
			{
				value = ConvertUtils.ToBigInteger(initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is BigInteger)
			{
				BigInteger i = (BigInteger)initialValue;
				value = ConvertUtils.FromBigInteger(i, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				value = converter.ConvertTo(null, culture, initialValue, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				value = converter2.ConvertFrom(null, culture, initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue == DBNull.Value)
			{
				if (ReflectionUtils.IsNullable(targetType))
				{
					value = ConvertUtils.EnsureTypeAssignable(null, type, targetType);
					return ConvertUtils.ConvertResult.Success;
				}
				value = null;
				return ConvertUtils.ConvertResult.CannotConvertNull;
			}
			else
			{
				if (!targetType.IsInterface() && !targetType.IsGenericTypeDefinition() && !targetType.IsAbstract())
				{
					value = null;
					return ConvertUtils.ConvertResult.NoValidConversion;
				}
				value = null;
				return ConvertUtils.ConvertResult.NotInstantiableType;
			}
		}

		public static object ConvertOrCast(object initialValue, CultureInfo culture, Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			object result;
			if (ConvertUtils.TryConvert(initialValue, culture, targetType, out result))
			{
				return result;
			}
			return ConvertUtils.EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		private static object EnsureTypeAssignable(object value, Type initialType, Type targetType)
		{
			Type type = (value != null) ? value.GetType() : null;
			if (value != null)
			{
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Get(new StructMultiKey<Type, Type>(type, targetType));
				if (func != null)
				{
					return func(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			string format = ConvertUtils.getString_0(107345163);
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string arg;
			if (initialType != null)
			{
				if ((arg = initialType.ToString()) != null)
				{
					goto IL_73;
				}
			}
			arg = ConvertUtils.getString_0(107345106);
			IL_73:
			throw new ArgumentException(format.FormatWith(invariantCulture, arg, targetType));
		}

		public static bool VersionTryParse(string input, out Version result)
		{
			return Version.TryParse(input, out result);
		}

		public static bool IsInteger(object value)
		{
			switch (ConvertUtils.GetTypeCode(value.GetType()))
			{
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return true;
			default:
				return false;
			}
		}

		public static ParseResult Int32TryParse(char[] chars, int start, int length, out int value)
		{
			value = 0;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag;
			if (flag = (chars[start] == '-'))
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length <= 10 && (length != 10 || chars[start] - '0' <= '\u0002'))
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
					int num3 = 10 * value - num2;
					if (num3 > value)
					{
						for (i++; i < num; i++)
						{
							num2 = (int)(chars[i] - '0');
							if (num2 < 0 || num2 > 9)
							{
								return ParseResult.Invalid;
							}
						}
						return ParseResult.Overflow;
					}
					value = num3;
				}
				if (!flag)
				{
					if (value == -2147483648)
					{
						return ParseResult.Overflow;
					}
					value = -value;
				}
				return ParseResult.Success;
			}
			for (int j = start; j < num; j++)
			{
				int num4 = (int)(chars[j] - '0');
				if (num4 < 0 || num4 > 9)
				{
					return ParseResult.Invalid;
				}
			}
			return ParseResult.Overflow;
		}

		public static ParseResult Int64TryParse(char[] chars, int start, int length, out long value)
		{
			value = 0L;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag;
			if (flag = (chars[start] == '-'))
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			if (length > 19)
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				long num4 = 10L * value - (long)num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -9223372036854775808L)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		public static ParseResult DecimalTryParse(char[] chars, int start, int length, out decimal value)
		{
			value = 0m;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag;
			if (flag = (chars[start] == '-'))
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int i = start;
			int num = start + length;
			int num2 = num;
			int num3 = num;
			int num4 = 0;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			int num7 = 0;
			int num8 = 0;
			char? c = null;
			bool? flag2 = null;
			while (i < num)
			{
				char c2 = chars[i];
				if (c2 == '.')
				{
					goto IL_234;
				}
				if (c2 != 'E' && c2 != 'e')
				{
					if (c2 >= '0' && c2 <= '9')
					{
						if (i == start && c2 == '0')
						{
							i++;
							if (i != num)
							{
								c2 = chars[i];
								if (c2 == '.')
								{
									goto IL_234;
								}
								if (c2 != 'e' && c2 != 'E')
								{
									return ParseResult.Invalid;
								}
								goto IL_1A8;
							}
						}
						if (num7 < 29)
						{
							if (num7 == 28)
							{
								bool? flag3 = flag2;
								bool valueOrDefault;
								if (flag3 == null)
								{
									flag2 = new bool?(num5 > 7922816251426433759UL || (num5 == 7922816251426433759UL && (num6 > 354395033UL || (num6 == 354395033UL && c2 > '5'))));
									bool? flag4 = flag2;
									valueOrDefault = flag4.GetValueOrDefault();
								}
								else
								{
									valueOrDefault = flag3.GetValueOrDefault();
								}
								if (valueOrDefault)
								{
									goto IL_18B;
								}
							}
							if (num7 < 19)
							{
								num5 = num5 * 10UL + (ulong)((long)(c2 - '0'));
							}
							else
							{
								num6 = num6 * 10UL + (ulong)((long)(c2 - '0'));
							}
							num7++;
							goto IL_255;
						}
						IL_18B:
						if (c == null)
						{
							c = new char?(c2);
						}
						num8++;
						goto IL_255;
					}
					return ParseResult.Invalid;
				}
				IL_1A8:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i == num2)
				{
					return ParseResult.Invalid;
				}
				i++;
				if (i == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 < num)
				{
					num3 = i - 1;
				}
				c2 = chars[i];
				bool flag5 = false;
				if (c2 != '+')
				{
					if (c2 == '-')
					{
						flag5 = true;
						i++;
					}
				}
				else
				{
					i++;
				}
				while (i < num)
				{
					c2 = chars[i];
					if (c2 < '0' || c2 > '9')
					{
						return ParseResult.Invalid;
					}
					int num9 = 10 * num4 + (int)(c2 - '0');
					if (num4 < num9)
					{
						num4 = num9;
					}
					i++;
				}
				if (flag5)
				{
					num4 = -num4;
				}
				IL_255:
				i++;
				continue;
				IL_234:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i + 1 == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 != num)
				{
					return ParseResult.Invalid;
				}
				num2 = i + 1;
				goto IL_255;
			}
			num4 += num8;
			num4 -= num3 - num2;
			if (num7 <= 19)
			{
				value = num5;
			}
			else
			{
				value = num5 / new decimal(1, 0, 0, false, (byte)(num7 - 19)) + num6;
			}
			if (num4 > 0)
			{
				num7 += num4;
				if (num7 > 29)
				{
					return ParseResult.Overflow;
				}
				if (num7 == 29)
				{
					if (num4 > 1)
					{
						value /= new decimal(1, 0, 0, false, (byte)(num4 - 1));
						if (value > 7922816251426433759354395033m)
						{
							return ParseResult.Overflow;
						}
					}
					else if (value == 7922816251426433759354395033m)
					{
						char? c3 = c;
						int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null;
						if (num10.GetValueOrDefault() > 53 & num10 != null)
						{
							return ParseResult.Overflow;
						}
					}
					value *= 10m;
				}
				else
				{
					value /= new decimal(1, 0, 0, false, (byte)num4);
				}
			}
			else
			{
				char? c3 = c;
				int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null;
				if ((num10.GetValueOrDefault() >= 53 & num10 != null) && num4 >= -28)
				{
					value = ++value;
				}
				if (num4 < 0)
				{
					if (num7 + num4 + 28 <= 0)
					{
						value = (flag ? 0m : 0m);
						return ParseResult.Success;
					}
					if (num4 >= -28)
					{
						value *= new decimal(1, 0, 0, false, (byte)(-(byte)num4));
					}
					else
					{
						value /= 10000000000000000000000000000m;
						value *= new decimal(1, 0, 0, false, (byte)(-num4 - 28));
					}
				}
			}
			if (flag)
			{
				value = -value;
			}
			return ParseResult.Success;
		}

		public static bool TryConvertGuid(string s, out Guid g)
		{
			return Guid.TryParseExact(s, ConvertUtils.getString_0(107445162), out g);
		}

		public static bool TryHexTextToInt(char[] text, int start, int end, out int value)
		{
			value = 0;
			for (int i = start; i < end; i++)
			{
				char c = text[i];
				int num;
				if (c <= '9' && c >= '0')
				{
					num = (int)(c - '0');
				}
				else if (c <= 'F' && c >= 'A')
				{
					num = (int)(c - '7');
				}
				else
				{
					if (c > 'f' || c < 'a')
					{
						value = 0;
						return false;
					}
					num = (int)(c - 'W');
				}
				value += num << (end - 1 - i) * 4;
			}
			return true;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ConvertUtils()
		{
			Strings.CreateGetStringDelegate(typeof(ConvertUtils));
			ConvertUtils.TypeCodeMap = new Dictionary<Type, PrimitiveTypeCode>
			{
				{
					typeof(char),
					PrimitiveTypeCode.Char
				},
				{
					typeof(char?),
					PrimitiveTypeCode.CharNullable
				},
				{
					typeof(bool),
					PrimitiveTypeCode.Boolean
				},
				{
					typeof(bool?),
					PrimitiveTypeCode.BooleanNullable
				},
				{
					typeof(sbyte),
					PrimitiveTypeCode.SByte
				},
				{
					typeof(sbyte?),
					PrimitiveTypeCode.SByteNullable
				},
				{
					typeof(short),
					PrimitiveTypeCode.Int16
				},
				{
					typeof(short?),
					PrimitiveTypeCode.Int16Nullable
				},
				{
					typeof(ushort),
					PrimitiveTypeCode.UInt16
				},
				{
					typeof(ushort?),
					PrimitiveTypeCode.UInt16Nullable
				},
				{
					typeof(int),
					PrimitiveTypeCode.Int32
				},
				{
					typeof(int?),
					PrimitiveTypeCode.Int32Nullable
				},
				{
					typeof(byte),
					PrimitiveTypeCode.Byte
				},
				{
					typeof(byte?),
					PrimitiveTypeCode.ByteNullable
				},
				{
					typeof(uint),
					PrimitiveTypeCode.UInt32
				},
				{
					typeof(uint?),
					PrimitiveTypeCode.UInt32Nullable
				},
				{
					typeof(long),
					PrimitiveTypeCode.Int64
				},
				{
					typeof(long?),
					PrimitiveTypeCode.Int64Nullable
				},
				{
					typeof(ulong),
					PrimitiveTypeCode.UInt64
				},
				{
					typeof(ulong?),
					PrimitiveTypeCode.UInt64Nullable
				},
				{
					typeof(float),
					PrimitiveTypeCode.Single
				},
				{
					typeof(float?),
					PrimitiveTypeCode.SingleNullable
				},
				{
					typeof(double),
					PrimitiveTypeCode.Double
				},
				{
					typeof(double?),
					PrimitiveTypeCode.DoubleNullable
				},
				{
					typeof(DateTime),
					PrimitiveTypeCode.DateTime
				},
				{
					typeof(DateTime?),
					PrimitiveTypeCode.DateTimeNullable
				},
				{
					typeof(DateTimeOffset),
					PrimitiveTypeCode.DateTimeOffset
				},
				{
					typeof(DateTimeOffset?),
					PrimitiveTypeCode.DateTimeOffsetNullable
				},
				{
					typeof(decimal),
					PrimitiveTypeCode.Decimal
				},
				{
					typeof(decimal?),
					PrimitiveTypeCode.DecimalNullable
				},
				{
					typeof(Guid),
					PrimitiveTypeCode.Guid
				},
				{
					typeof(Guid?),
					PrimitiveTypeCode.GuidNullable
				},
				{
					typeof(TimeSpan),
					PrimitiveTypeCode.TimeSpan
				},
				{
					typeof(TimeSpan?),
					PrimitiveTypeCode.TimeSpanNullable
				},
				{
					typeof(BigInteger),
					PrimitiveTypeCode.BigInteger
				},
				{
					typeof(BigInteger?),
					PrimitiveTypeCode.BigIntegerNullable
				},
				{
					typeof(Uri),
					PrimitiveTypeCode.Uri
				},
				{
					typeof(string),
					PrimitiveTypeCode.String
				},
				{
					typeof(byte[]),
					PrimitiveTypeCode.Bytes
				},
				{
					typeof(DBNull),
					PrimitiveTypeCode.DBNull
				}
			};
			ConvertUtils.PrimitiveTypeCodes = new TypeInformation[]
			{
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Empty
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Object
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.DBNull
				},
				new TypeInformation
				{
					Type = typeof(bool),
					TypeCode = PrimitiveTypeCode.Boolean
				},
				new TypeInformation
				{
					Type = typeof(char),
					TypeCode = PrimitiveTypeCode.Char
				},
				new TypeInformation
				{
					Type = typeof(sbyte),
					TypeCode = PrimitiveTypeCode.SByte
				},
				new TypeInformation
				{
					Type = typeof(byte),
					TypeCode = PrimitiveTypeCode.Byte
				},
				new TypeInformation
				{
					Type = typeof(short),
					TypeCode = PrimitiveTypeCode.Int16
				},
				new TypeInformation
				{
					Type = typeof(ushort),
					TypeCode = PrimitiveTypeCode.UInt16
				},
				new TypeInformation
				{
					Type = typeof(int),
					TypeCode = PrimitiveTypeCode.Int32
				},
				new TypeInformation
				{
					Type = typeof(uint),
					TypeCode = PrimitiveTypeCode.UInt32
				},
				new TypeInformation
				{
					Type = typeof(long),
					TypeCode = PrimitiveTypeCode.Int64
				},
				new TypeInformation
				{
					Type = typeof(ulong),
					TypeCode = PrimitiveTypeCode.UInt64
				},
				new TypeInformation
				{
					Type = typeof(float),
					TypeCode = PrimitiveTypeCode.Single
				},
				new TypeInformation
				{
					Type = typeof(double),
					TypeCode = PrimitiveTypeCode.Double
				},
				new TypeInformation
				{
					Type = typeof(decimal),
					TypeCode = PrimitiveTypeCode.Decimal
				},
				new TypeInformation
				{
					Type = typeof(DateTime),
					TypeCode = PrimitiveTypeCode.DateTime
				},
				new TypeInformation
				{
					Type = typeof(object),
					TypeCode = PrimitiveTypeCode.Empty
				},
				new TypeInformation
				{
					Type = typeof(string),
					TypeCode = PrimitiveTypeCode.String
				}
			};
			ConvertUtils.CastConverters = new ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>>(new Func<StructMultiKey<Type, Type>, Func<object, object>>(ConvertUtils.CreateCastConverter));
		}

		private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap;

		private static readonly TypeInformation[] PrimitiveTypeCodes;

		private static readonly ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>> CastConverters;

		[NonSerialized]
		internal static GetString getString_0;

		internal enum ConvertResult
		{
			Success,
			CannotConvertNull,
			NotInstantiableType,
			NoValidConversion
		}
	}
}
