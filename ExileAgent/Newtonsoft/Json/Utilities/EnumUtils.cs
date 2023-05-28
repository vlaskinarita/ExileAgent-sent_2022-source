using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class EnumUtils
	{
		private static EnumInfo InitializeValuesAndNames(StructMultiKey<Type, NamingStrategy> key)
		{
			Type value = key.Value1;
			string[] names = Enum.GetNames(value);
			string[] array = new string[names.Length];
			ulong[] array2 = new ulong[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string name = names[i];
				FieldInfo field = value.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				array2[i] = EnumUtils.ToUInt64(field.GetValue(null));
				string text = (from EnumMemberAttribute a in field.GetCustomAttributes(typeof(EnumMemberAttribute), true)
				select a.Value).SingleOrDefault<string>() ?? field.Name;
				if (Array.IndexOf<string>(array, text, 0, i) != -1)
				{
					throw new InvalidOperationException(EnumUtils.getString_0(107344033).FormatWith(CultureInfo.InvariantCulture, text, value.Name));
				}
				array[i] = ((key.Value2 != null) ? key.Value2.GetPropertyName(text, false) : text);
			}
			return new EnumInfo(value.IsDefined(typeof(FlagsAttribute), false), array2, names, array);
		}

		public static IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException(EnumUtils.getString_0(107343972).FormatWith(CultureInfo.InvariantCulture, typeFromHandle));
			}
			Type underlyingType = Enum.GetUnderlyingType(value.GetType());
			ulong num = EnumUtils.ToUInt64(value);
			EnumInfo enumValuesAndNames = EnumUtils.GetEnumValuesAndNames(typeFromHandle);
			IList<T> list = new List<T>();
			for (int i = 0; i < enumValuesAndNames.Values.Length; i++)
			{
				ulong num2 = enumValuesAndNames.Values[i];
				if ((num & num2) == num2 && num2 != 0UL)
				{
					list.Add((T)((object)Convert.ChangeType(num2, underlyingType, CultureInfo.CurrentCulture)));
				}
			}
			if (list.Count == 0)
			{
				if (enumValuesAndNames.Values.Any((ulong v) => v == 0UL))
				{
					list.Add(default(T));
				}
			}
			return list;
		}

		public static bool TryToString(Type enumType, object value, bool camelCase, out string name)
		{
			return EnumUtils.TryToString(enumType, value, camelCase ? EnumUtils._camelCaseNamingStrategy : null, out name);
		}

		public static bool TryToString(Type enumType, object value, NamingStrategy namingStrategy, out string name)
		{
			EnumInfo enumInfo = EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, namingStrategy));
			ulong num = EnumUtils.ToUInt64(value);
			if (enumInfo.IsFlags)
			{
				name = EnumUtils.InternalFlagsFormat(enumInfo, num);
				return name != null;
			}
			int num2 = Array.BinarySearch<ulong>(enumInfo.Values, num);
			if (num2 >= 0)
			{
				name = enumInfo.ResolvedNames[num2];
				return true;
			}
			name = null;
			return false;
		}

		private static string InternalFlagsFormat(EnumInfo entry, ulong result)
		{
			string[] resolvedNames = entry.ResolvedNames;
			ulong[] values = entry.Values;
			int num = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num2 = result;
			while (num >= 0 && (num != 0 || values[num] != 0UL))
			{
				if ((result & values[num]) == values[num])
				{
					result -= values[num];
					if (!flag)
					{
						stringBuilder.Insert(0, EnumUtils.getString_0(107401354));
					}
					string value = resolvedNames[num];
					stringBuilder.Insert(0, value);
					flag = false;
				}
				num--;
			}
			string result2;
			if (result != 0UL)
			{
				result2 = null;
			}
			else if (num2 == 0UL)
			{
				if (values.Length != 0 && values[0] == 0UL)
				{
					result2 = resolvedNames[0];
				}
				else
				{
					result2 = null;
				}
			}
			else
			{
				result2 = stringBuilder.ToString();
			}
			return result2;
		}

		public static EnumInfo GetEnumValuesAndNames(Type enumType)
		{
			return EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, null));
		}

		private static ulong ToUInt64(object value)
		{
			bool flag;
			switch (ConvertUtils.GetTypeCode(value.GetType(), out flag))
			{
			case PrimitiveTypeCode.Char:
				return (ulong)((char)value);
			case PrimitiveTypeCode.Boolean:
				return (ulong)Convert.ToByte((bool)value);
			case PrimitiveTypeCode.SByte:
				return (ulong)((long)((sbyte)value));
			case PrimitiveTypeCode.Int16:
				return (ulong)((long)((short)value));
			case PrimitiveTypeCode.UInt16:
				return (ulong)((ushort)value);
			case PrimitiveTypeCode.Int32:
				return (ulong)((long)((int)value));
			case PrimitiveTypeCode.Byte:
				return (ulong)((byte)value);
			case PrimitiveTypeCode.UInt32:
				return (ulong)((uint)value);
			case PrimitiveTypeCode.Int64:
				return (ulong)((long)value);
			case PrimitiveTypeCode.UInt64:
				return (ulong)value;
			}
			throw new InvalidOperationException(EnumUtils.getString_0(107343891));
		}

		public static object ParseEnum(Type enumType, NamingStrategy namingStrategy, string value, bool disallowNumber)
		{
			ValidationUtils.ArgumentNotNull(enumType, EnumUtils.getString_0(107343866));
			ValidationUtils.ArgumentNotNull(value, EnumUtils.getString_0(107453608));
			if (!enumType.IsEnum())
			{
				throw new ArgumentException(EnumUtils.getString_0(107343853), EnumUtils.getString_0(107343866));
			}
			EnumInfo enumInfo = EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, namingStrategy));
			string[] names = enumInfo.Names;
			string[] resolvedNames = enumInfo.ResolvedNames;
			ulong[] values = enumInfo.Values;
			int? num = EnumUtils.FindIndexByName(resolvedNames, value, 0, value.Length, StringComparison.Ordinal);
			if (num != null)
			{
				return Enum.ToObject(enumType, values[num.Value]);
			}
			int num2 = -1;
			int i = 0;
			while (i < value.Length)
			{
				if (char.IsWhiteSpace(value[i]))
				{
					i++;
				}
				else
				{
					num2 = i;
					IL_D1:
					if (num2 == -1)
					{
						throw new ArgumentException(EnumUtils.getString_0(107343844));
					}
					char c = value[num2];
					if (char.IsDigit(c) || c == '-' || c == '+')
					{
						Type underlyingType = Enum.GetUnderlyingType(enumType);
						value = value.Trim();
						object obj = null;
						try
						{
							obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
						}
						catch (FormatException)
						{
						}
						if (obj != null)
						{
							if (disallowNumber)
							{
								throw new FormatException(EnumUtils.getString_0(107344247).FormatWith(CultureInfo.InvariantCulture, value));
							}
							return Enum.ToObject(enumType, obj);
						}
					}
					ulong num3 = 0UL;
					int j = num2;
					while (j <= value.Length)
					{
						int num4 = value.IndexOf(',', j);
						if (num4 == -1)
						{
							num4 = value.Length;
						}
						int num5 = num4;
						while (j < num4 && char.IsWhiteSpace(value[j]))
						{
							j++;
						}
						while (num5 > j && char.IsWhiteSpace(value[num5 - 1]))
						{
							num5--;
						}
						int valueSubstringLength = num5 - j;
						num = EnumUtils.MatchName(value, names, resolvedNames, j, valueSubstringLength, StringComparison.Ordinal);
						if (num == null)
						{
							num = EnumUtils.MatchName(value, names, resolvedNames, j, valueSubstringLength, StringComparison.OrdinalIgnoreCase);
						}
						if (num != null)
						{
							num3 |= values[num.Value];
							j = num4 + 1;
						}
						else
						{
							num = EnumUtils.FindIndexByName(resolvedNames, value, 0, value.Length, StringComparison.OrdinalIgnoreCase);
							if (num == null)
							{
								throw new ArgumentException(EnumUtils.getString_0(107344230).FormatWith(CultureInfo.InvariantCulture, value));
							}
							return Enum.ToObject(enumType, values[num.Value]);
						}
					}
					return Enum.ToObject(enumType, num3);
				}
			}
			goto IL_D1;
		}

		private static int? MatchName(string value, string[] enumNames, string[] resolvedNames, int valueIndex, int valueSubstringLength, StringComparison comparison)
		{
			int? result = EnumUtils.FindIndexByName(resolvedNames, value, valueIndex, valueSubstringLength, comparison);
			if (result == null)
			{
				result = EnumUtils.FindIndexByName(enumNames, value, valueIndex, valueSubstringLength, comparison);
			}
			return result;
		}

		private static int? FindIndexByName(string[] enumNames, string value, int valueIndex, int valueSubstringLength, StringComparison comparison)
		{
			for (int i = 0; i < enumNames.Length; i++)
			{
				if (enumNames[i].Length == valueSubstringLength && string.Compare(enumNames[i], 0, value, valueIndex, valueSubstringLength, comparison) == 0)
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static EnumUtils()
		{
			Strings.CreateGetStringDelegate(typeof(EnumUtils));
			EnumUtils.ValuesAndNamesPerEnum = new ThreadSafeStore<StructMultiKey<Type, NamingStrategy>, EnumInfo>(new Func<StructMultiKey<Type, NamingStrategy>, EnumInfo>(EnumUtils.InitializeValuesAndNames));
			EnumUtils._camelCaseNamingStrategy = new CamelCaseNamingStrategy();
		}

		private const char EnumSeparatorChar = ',';

		private const string EnumSeparatorString = ", ";

		private static readonly ThreadSafeStore<StructMultiKey<Type, NamingStrategy>, EnumInfo> ValuesAndNamesPerEnum;

		private static CamelCaseNamingStrategy _camelCaseNamingStrategy;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
