using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocket4Net
{
	public static class Extensions
	{
		public static void AppendFormatWithCrCf(this StringBuilder builder, string format, object arg)
		{
			builder.AppendFormat(format, arg);
			builder.Append(Extensions.m_CrCf);
		}

		public static void AppendFormatWithCrCf(this StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(format, args);
			builder.Append(Extensions.m_CrCf);
		}

		public static void AppendWithCrCf(this StringBuilder builder, string content)
		{
			builder.Append(content);
			builder.Append(Extensions.m_CrCf);
		}

		public static void AppendWithCrCf(this StringBuilder builder)
		{
			builder.Append(Extensions.m_CrCf);
		}

		public static bool ParseMimeHeader(this string source, IDictionary<string, object> valueContainer, out string verbLine)
		{
			verbLine = string.Empty;
			string text = string.Empty;
			StringReader stringReader = new StringReader(source);
			string text2;
			while (!string.IsNullOrEmpty(text2 = stringReader.ReadLine()))
			{
				if (string.IsNullOrEmpty(verbLine))
				{
					verbLine = text2;
				}
				else if (text2.StartsWith(Extensions.getString_0(107248056)) && !string.IsNullOrEmpty(text))
				{
					object arg;
					if (!valueContainer.TryGetValue(text, out arg))
					{
						return false;
					}
					valueContainer[text] = arg + text2.Trim();
				}
				else
				{
					int num = text2.IndexOf(':');
					if (num >= 0)
					{
						string text3 = text2.Substring(0, num);
						if (!string.IsNullOrEmpty(text3))
						{
							text3 = text3.Trim();
						}
						string text4 = text2.Substring(num + 1);
						if (!string.IsNullOrEmpty(text4) && text4.StartsWith(Extensions.getString_0(107404924)) && text4.Length > 1)
						{
							text4 = text4.Substring(1);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							object arg;
							if (!valueContainer.TryGetValue(text3, out arg))
							{
								valueContainer.Add(text3, text4);
							}
							else
							{
								valueContainer[text3] = arg + Extensions.getString_0(107404970) + text4;
							}
							text = text3;
						}
					}
				}
			}
			return true;
		}

		public static TValue GetValue<TValue>(this IDictionary<string, object> valueContainer, string name)
		{
			return valueContainer.GetValue(name, default(TValue));
		}

		public static TValue GetValue<TValue>(this IDictionary<string, object> valueContainer, string name, TValue defaultValue)
		{
			object obj;
			if (!valueContainer.TryGetValue(name, out obj))
			{
				return defaultValue;
			}
			return (TValue)((object)obj);
		}

		internal static bool IsSimpleType(this Type type)
		{
			return type.IsValueType || type.IsPrimitive || Extensions.m_SimpleTypes.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object;
		}

		public static string GetOrigin(this Uri uri)
		{
			return uri.GetLeftPart(UriPartial.Authority);
		}

		public static byte[] ComputeMD5Hash(this byte[] source)
		{
			return MD5.Create().ComputeHash(source);
		}

		public static string CalculateChallenge(this string source)
		{
			return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(source)));
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Extensions()
		{
			Strings.CreateGetStringDelegate(typeof(Extensions));
			Extensions.m_CrCf = new char[]
			{
				'\r',
				'\n'
			};
			Extensions.m_SimpleTypes = new Type[]
			{
				typeof(string),
				typeof(decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			};
		}

		private static readonly char[] m_CrCf;

		private const string m_Tab = "\t";

		private const char m_Colon = ':';

		private const string m_Space = " ";

		private const string m_ValueSeparator = ", ";

		private static Type[] m_SimpleTypes;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
