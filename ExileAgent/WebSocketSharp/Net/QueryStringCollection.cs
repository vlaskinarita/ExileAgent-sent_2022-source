using System;
using System.Collections.Specialized;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class QueryStringCollection : NameValueCollection
	{
		public QueryStringCollection()
		{
		}

		public QueryStringCollection(int capacity) : base(capacity)
		{
		}

		private static string urlDecode(string s, Encoding encoding)
		{
			return (s.IndexOfAny(new char[]
			{
				'%',
				'+'
			}) > -1) ? HttpUtility.UrlDecode(s, encoding) : s;
		}

		public static QueryStringCollection Parse(string query)
		{
			return QueryStringCollection.Parse(query, Encoding.UTF8);
		}

		public static QueryStringCollection Parse(string query, Encoding encoding)
		{
			QueryStringCollection result;
			if (query == null)
			{
				result = new QueryStringCollection(1);
			}
			else
			{
				int length = query.Length;
				if (length == 0)
				{
					result = new QueryStringCollection(1);
				}
				else if (query == QueryStringCollection.getString_0(107291524))
				{
					result = new QueryStringCollection(1);
				}
				else
				{
					if (query[0] == '?')
					{
						query = query.Substring(1);
					}
					if (encoding == null)
					{
						encoding = Encoding.UTF8;
					}
					QueryStringCollection queryStringCollection = new QueryStringCollection();
					string[] array = query.Split(new char[]
					{
						'&'
					});
					foreach (string text in array)
					{
						length = text.Length;
						if (length != 0 && !(text == QueryStringCollection.getString_0(107231957)))
						{
							int num = text.IndexOf('=');
							if (num < 0)
							{
								queryStringCollection.Add(null, QueryStringCollection.urlDecode(text, encoding));
							}
							else if (num == 0)
							{
								queryStringCollection.Add(null, QueryStringCollection.urlDecode(text.Substring(1), encoding));
							}
							else
							{
								string name = QueryStringCollection.urlDecode(text.Substring(0, num), encoding);
								int num2 = num + 1;
								string value = (num2 < length) ? QueryStringCollection.urlDecode(text.Substring(num2), encoding) : string.Empty;
								queryStringCollection.Add(name, value);
							}
						}
					}
					result = queryStringCollection;
				}
			}
			return result;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.AllKeys)
			{
				stringBuilder.AppendFormat(QueryStringCollection.getString_0(107161200), text, base[text]);
			}
			if (stringBuilder.Length > 0)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				int length = stringBuilder2.Length;
				stringBuilder2.Length = length - 1;
			}
			return stringBuilder.ToString();
		}

		static QueryStringCollection()
		{
			Strings.CreateGetStringDelegate(typeof(QueryStringCollection));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
