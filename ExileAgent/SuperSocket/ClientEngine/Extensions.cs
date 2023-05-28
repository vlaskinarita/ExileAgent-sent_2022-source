using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine
{
	public static class Extensions
	{
		public static int IndexOf<T>(this IList<T> source, T target, int pos, int length) where T : IEquatable<T>
		{
			for (int i = pos; i < pos + length; i++)
			{
				T t = source[i];
				if (t.Equals(target))
				{
					return i;
				}
			}
			return -1;
		}

		public static int? SearchMark<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.SearchMark(0, source.Count, mark, 0);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			return source.SearchMark(offset, length, mark, 0);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched) where T : IEquatable<T>
		{
			int num = offset;
			int num2 = offset + length - 1;
			int num3 = matched;
			if (matched > 0)
			{
				int i = num3;
				while (i < mark.Length)
				{
					T t = source[num++];
					if (t.Equals(mark[i]))
					{
						num3++;
						if (num <= num2)
						{
							i++;
						}
						else
						{
							if (num3 == mark.Length)
							{
								return new int?(offset);
							}
							return new int?(0 - num3);
						}
					}
					else
					{
						IL_64:
						if (num3 == mark.Length)
						{
							return new int?(offset);
						}
						num = offset;
						num3 = 0;
						goto IL_CB;
					}
				}
				goto IL_64;
			}
			for (;;)
			{
				IL_CB:
				num = source.IndexOf(mark[num3], num, length - num + offset);
				if (num < 0)
				{
					goto Block_10;
				}
				num3++;
				for (int j = num3; j < mark.Length; j++)
				{
					int num4 = num + j;
					if (num4 > num2)
					{
						goto IL_EE;
					}
					T t = source[num4];
					if (!t.Equals(mark[j]))
					{
						break;
					}
					num3++;
				}
				if (num3 == mark.Length)
				{
					break;
				}
				num++;
				num3 = 0;
			}
			return new int?(num);
			Block_10:
			return null;
			IL_EE:
			return new int?(0 - num3);
		}

		public static int SearchMark<T>(this IList<T> source, int offset, int length, SearchMarkState<T> searchState) where T : IEquatable<T>
		{
			int? num = source.SearchMark(offset, length, searchState.Mark, searchState.Matched);
			if (num == null)
			{
				searchState.Matched = 0;
				return -1;
			}
			if (num.Value < 0)
			{
				searchState.Matched = 0 - num.Value;
				return -1;
			}
			searchState.Matched = 0;
			return num.Value;
		}

		public static int StartsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.StartsWith(0, source.Count, mark);
		}

		public static int StartsWith<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			int num = offset + length - 1;
			for (int i = 0; i < mark.Length; i++)
			{
				int num2 = offset + i;
				if (num2 > num)
				{
					return i;
				}
				T t = source[num2];
				if (!t.Equals(mark[i]))
				{
					return -1;
				}
			}
			return mark.Length;
		}

		public static bool EndsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.EndsWith(0, source.Count, mark);
		}

		public static bool EndsWith<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			if (mark.Length > length)
			{
				return false;
			}
			for (int i = 0; i < Math.Min(length, mark.Length); i++)
			{
				if (!mark[i].Equals(source[offset + length - mark.Length + i]))
				{
					return false;
				}
			}
			return true;
		}

		public static T[] CloneRange<T>(this T[] source, int offset, int length)
		{
			T[] array = new T[length];
			Array.Copy(source, offset, array, 0, length);
			return array;
		}

		public static T[] RandomOrder<T>(this T[] source)
		{
			int num = source.Length / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = Extensions.m_Random.Next(0, source.Length - 1);
				int num3 = Extensions.m_Random.Next(0, source.Length - 1);
				if (num2 != num3)
				{
					T t = source[num3];
					source[num3] = source[num2];
					source[num2] = t;
				}
			}
			return source;
		}

		public static string GetValue(this NameValueCollection collection, string key)
		{
			return collection.GetValue(key, string.Empty);
		}

		public static string GetValue(this NameValueCollection collection, string key, string defaultValue)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException(Extensions.getString_0(107456565));
			}
			if (collection == null)
			{
				return defaultValue;
			}
			string text = collection[key];
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Extensions()
		{
			Strings.CreateGetStringDelegate(typeof(Extensions));
			Extensions.m_Random = new Random();
		}

		private static Random m_Random;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
