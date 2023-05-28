using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class StringUtils
	{
		public static string FormatWith(this string format, IFormatProvider provider, object arg0)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0
			});
		}

		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1
			});
		}

		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2, object arg3)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		private static string FormatWith(this string format, IFormatProvider provider, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, StringUtils.getString_0(107340463));
			return string.Format(provider, format, args);
		}

		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(StringUtils.getString_0(107400167));
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static StringWriter CreateStringWriter(int capacity)
		{
			return new StringWriter(new StringBuilder(capacity), CultureInfo.InvariantCulture);
		}

		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = MathUtils.IntToHex((int)(c >> 12 & '\u000f'));
			buffer[3] = MathUtils.IntToHex((int)(c >> 8 & '\u000f'));
			buffer[4] = MathUtils.IntToHex((int)(c >> 4 & '\u000f'));
			buffer[5] = MathUtils.IntToHex((int)(c & '\u000f'));
		}

		public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException(StringUtils.getString_0(107244105));
			}
			if (valueSelector == null)
			{
				throw new ArgumentNullException(StringUtils.getString_0(107340454));
			}
			IEnumerable<TSource> source2 = from s in source
			where string.Equals(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase)
			select s;
			if (source2.Count<TSource>() <= 1)
			{
				return source2.SingleOrDefault<TSource>();
			}
			return (from s in source
			where string.Equals(valueSelector(s), testValue, StringComparison.Ordinal)
			select s).SingleOrDefault<TSource>();
		}

		public static string ToCamelCase(string s)
		{
			if (!string.IsNullOrEmpty(s) && char.IsUpper(s[0]))
			{
				char[] array = s.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					if (i != 1 || char.IsUpper(array[i]))
					{
						bool flag = i + 1 < array.Length;
						if (i <= 0 || !flag || char.IsUpper(array[i + 1]))
						{
							array[i] = StringUtils.ToLower(array[i]);
							i++;
							continue;
						}
						if (char.IsSeparator(array[i + 1]))
						{
							array[i] = StringUtils.ToLower(array[i]);
						}
					}
					IL_7A:
					return new string(array);
				}
				goto IL_7A;
			}
			return s;
		}

		private static char ToLower(char c)
		{
			c = char.ToLower(c, CultureInfo.InvariantCulture);
			return c;
		}

		public static string ToSnakeCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringUtils.SnakeCaseState snakeCaseState = StringUtils.SnakeCaseState.Start;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == ' ')
				{
					if (snakeCaseState != StringUtils.SnakeCaseState.Start)
					{
						snakeCaseState = StringUtils.SnakeCaseState.NewWord;
					}
				}
				else if (char.IsUpper(s[i]))
				{
					switch (snakeCaseState)
					{
					case StringUtils.SnakeCaseState.Lower:
					case StringUtils.SnakeCaseState.NewWord:
						stringBuilder.Append('_');
						break;
					case StringUtils.SnakeCaseState.Upper:
					{
						bool flag = i + 1 < s.Length;
						if (i > 0 && flag)
						{
							char c = s[i + 1];
							if (!char.IsUpper(c) && c != '_')
							{
								stringBuilder.Append('_');
							}
						}
						break;
					}
					}
					char value = char.ToLower(s[i], CultureInfo.InvariantCulture);
					stringBuilder.Append(value);
					snakeCaseState = StringUtils.SnakeCaseState.Upper;
				}
				else if (s[i] == '_')
				{
					stringBuilder.Append('_');
					snakeCaseState = StringUtils.SnakeCaseState.Start;
				}
				else
				{
					if (snakeCaseState == StringUtils.SnakeCaseState.NewWord)
					{
						stringBuilder.Append('_');
					}
					stringBuilder.Append(s[i]);
					snakeCaseState = StringUtils.SnakeCaseState.Lower;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		public static bool StartsWith(this string source, char value)
		{
			return source.Length > 0 && source[0] == value;
		}

		public static bool EndsWith(this string source, char value)
		{
			return source.Length > 0 && source[source.Length - 1] == value;
		}

		public static string Trim(this string s, int start, int length)
		{
			if (s == null)
			{
				throw new ArgumentNullException();
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException(StringUtils.getString_0(107340433));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(StringUtils.getString_0(107340424));
			}
			int num = start + length - 1;
			if (num >= s.Length)
			{
				throw new ArgumentOutOfRangeException(StringUtils.getString_0(107340424));
			}
			while (start < num)
			{
				if (!char.IsWhiteSpace(s[start]))
				{
					IL_8A:
					while (num >= start && char.IsWhiteSpace(s[num]))
					{
						num--;
					}
					return s.Substring(start, num - start + 1);
				}
				start++;
			}
			goto IL_8A;
		}

		static StringUtils()
		{
			Strings.CreateGetStringDelegate(typeof(StringUtils));
		}

		public const string CarriageReturnLineFeed = "\r\n";

		public const string Empty = "";

		public const char CarriageReturn = '\r';

		public const char LineFeed = '\n';

		public const char Tab = '\t';

		[NonSerialized]
		internal static GetString getString_0;

		internal enum SnakeCaseState
		{
			Start,
			Lower,
			Upper,
			NewWord
		}
	}
}
