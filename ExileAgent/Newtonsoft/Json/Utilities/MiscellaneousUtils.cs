using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class MiscellaneousUtils
	{
		public static bool ValueEquals(object objA, object objB)
		{
			if (objA == objB)
			{
				return true;
			}
			if (objA == null || objB == null)
			{
				return false;
			}
			if (!(objA.GetType() != objB.GetType()))
			{
				return objA.Equals(objB);
			}
			if (ConvertUtils.IsInteger(objA) && ConvertUtils.IsInteger(objB))
			{
				return Convert.ToDecimal(objA, CultureInfo.CurrentCulture).Equals(Convert.ToDecimal(objB, CultureInfo.CurrentCulture));
			}
			return (objA is double || objA is float || objA is decimal) && (objB is double || objB is float || objB is decimal) && MathUtils.ApproxEquals(Convert.ToDouble(objA, CultureInfo.CurrentCulture), Convert.ToDouble(objB, CultureInfo.CurrentCulture));
		}

		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string message2 = message + Environment.NewLine + MiscellaneousUtils.getString_0(107341532).FormatWith(CultureInfo.InvariantCulture, actualValue);
			return new ArgumentOutOfRangeException(paramName, message2);
		}

		public static string ToString(object value)
		{
			if (value == null)
			{
				return MiscellaneousUtils.getString_0(107345346);
			}
			string str;
			if ((str = (value as string)) == null)
			{
				return value.ToString();
			}
			return MiscellaneousUtils.getString_0(107373405) + str + MiscellaneousUtils.getString_0(107373405);
		}

		public static int ByteArrayCompare(byte[] a1, byte[] a2)
		{
			int num = a1.Length.CompareTo(a2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = a1[i].CompareTo(a2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		public static string GetPrefix(string qualifiedName)
		{
			string result;
			string text;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out result, out text);
			return result;
		}

		public static string GetLocalName(string qualifiedName)
		{
			string text;
			string result;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out text, out result);
			return result;
		}

		public static void GetQualifiedNameParts(string qualifiedName, out string prefix, out string localName)
		{
			int num = qualifiedName.IndexOf(':');
			if (num != -1 && num != 0)
			{
				if (qualifiedName.Length - 1 != num)
				{
					prefix = qualifiedName.Substring(0, num);
					localName = qualifiedName.Substring(num + 1);
					return;
				}
			}
			prefix = null;
			localName = qualifiedName;
		}

		internal static RegexOptions GetRegexOptions(string optionsText)
		{
			RegexOptions regexOptions = RegexOptions.None;
			foreach (char c in optionsText)
			{
				if (c <= 'm')
				{
					if (c != 'i')
					{
						if (c == 'm')
						{
							regexOptions |= RegexOptions.Multiline;
						}
					}
					else
					{
						regexOptions |= RegexOptions.IgnoreCase;
					}
				}
				else if (c != 's')
				{
					if (c == 'x')
					{
						regexOptions |= RegexOptions.ExplicitCapture;
					}
				}
				else
				{
					regexOptions |= RegexOptions.Singleline;
				}
			}
			return regexOptions;
		}

		static MiscellaneousUtils()
		{
			Strings.CreateGetStringDelegate(typeof(MiscellaneousUtils));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
