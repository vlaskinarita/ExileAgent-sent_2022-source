using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	[Serializable]
	public sealed class CookieCollection : IEnumerable, IEnumerable<Cookie>, ICollection<Cookie>
	{
		public CookieCollection()
		{
			this._list = new List<Cookie>();
			this._sync = ((ICollection)this._list).SyncRoot;
		}

		internal IList<Cookie> List
		{
			get
			{
				return this._list;
			}
		}

		internal IEnumerable<Cookie> Sorted
		{
			get
			{
				List<Cookie> list = new List<Cookie>(this._list);
				if (list.Count > 1)
				{
					list.Sort(new Comparison<Cookie>(CookieCollection.compareForSorted));
				}
				return list;
			}
		}

		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
			internal set
			{
				this._readOnly = value;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException(CookieCollection.getString_0(107350366));
				}
				return this._list[index];
			}
		}

		public Cookie this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException(CookieCollection.getString_0(107378070));
				}
				StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
				foreach (Cookie cookie in this.Sorted)
				{
					if (cookie.Name.Equals(name, comparisonType))
					{
						return cookie;
					}
				}
				return null;
			}
		}

		public object SyncRoot
		{
			get
			{
				return this._sync;
			}
		}

		private void add(Cookie cookie)
		{
			int num = this.search(cookie);
			if (num == -1)
			{
				this._list.Add(cookie);
			}
			else
			{
				this._list[num] = cookie;
			}
		}

		private static int compareForSort(Cookie x, Cookie y)
		{
			return x.Name.Length + x.Value.Length - (y.Name.Length + y.Value.Length);
		}

		private static int compareForSorted(Cookie x, Cookie y)
		{
			int num = x.Version - y.Version;
			return (num != 0) ? num : (((num = x.Name.CompareTo(y.Name)) != 0) ? num : (y.Path.Length - x.Path.Length));
		}

		private static CookieCollection parseRequest(string value)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			int num = 0;
			StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
			List<string> list = value.SplitHeaderValue(new char[]
			{
				',',
				';'
			}).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				if (text.Length != 0)
				{
					int num2 = text.IndexOf('=');
					if (num2 == -1)
					{
						if (cookie != null && text.Equals(CookieCollection.getString_0(107132096), comparisonType))
						{
							cookie.Port = CookieCollection.getString_0(107314234);
						}
					}
					else if (num2 == 0)
					{
						if (cookie != null)
						{
							cookieCollection.add(cookie);
							cookie = null;
						}
					}
					else
					{
						string text2 = text.Substring(0, num2).TrimEnd(new char[]
						{
							' '
						});
						string text3 = (num2 < text.Length - 1) ? text.Substring(num2 + 1).TrimStart(new char[]
						{
							' '
						}) : string.Empty;
						if (text2.Equals(CookieCollection.getString_0(107132055), comparisonType))
						{
							int num3;
							if (text3.Length != 0 && int.TryParse(text3.Unquote(), out num3))
							{
								num = num3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107132042), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Path = text3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107132065), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Domain = text3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107132096), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Port = text3;
							}
						}
						else
						{
							if (cookie != null)
							{
								cookieCollection.add(cookie);
							}
							if (Cookie.TryCreate(text2, text3, out cookie) && num != 0)
							{
								cookie.Version = num;
							}
						}
					}
				}
			}
			if (cookie != null)
			{
				cookieCollection.add(cookie);
			}
			return cookieCollection;
		}

		private static CookieCollection parseResponse(string value)
		{
			CookieCollection cookieCollection = new CookieCollection();
			Cookie cookie = null;
			StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
			List<string> list = value.SplitHeaderValue(new char[]
			{
				',',
				';'
			}).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i].Trim();
				if (text.Length != 0)
				{
					int num = text.IndexOf('=');
					if (num == -1)
					{
						if (cookie != null)
						{
							if (text.Equals(CookieCollection.getString_0(107132020), comparisonType))
							{
								cookie.Port = CookieCollection.getString_0(107314234);
							}
							else if (text.Equals(CookieCollection.getString_0(107132011), comparisonType))
							{
								cookie.Discard = true;
							}
							else if (text.Equals(CookieCollection.getString_0(107400362), comparisonType))
							{
								cookie.Secure = true;
							}
							else if (text.Equals(CookieCollection.getString_0(107132030), comparisonType))
							{
								cookie.HttpOnly = true;
							}
						}
					}
					else if (num == 0)
					{
						if (cookie != null)
						{
							cookieCollection.add(cookie);
							cookie = null;
						}
					}
					else
					{
						string text2 = text.Substring(0, num).TrimEnd(new char[]
						{
							' '
						});
						string text3 = (num < text.Length - 1) ? text.Substring(num + 1).TrimStart(new char[]
						{
							' '
						}) : string.Empty;
						if (text2.Equals(CookieCollection.getString_0(107372226), comparisonType))
						{
							int version;
							if (cookie != null && text3.Length != 0 && int.TryParse(text3.Unquote(), out version))
							{
								cookie.Version = version;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107131985), comparisonType))
						{
							if (text3.Length != 0)
							{
								if (i == list.Count - 1)
								{
									break;
								}
								i++;
								if (cookie != null && !(cookie.Expires != DateTime.MinValue))
								{
									StringBuilder stringBuilder = new StringBuilder(text3, 32);
									stringBuilder.AppendFormat(CookieCollection.getString_0(107132004), list[i].Trim());
									DateTime dateTime;
									if (DateTime.TryParseExact(stringBuilder.ToString(), new string[]
									{
										CookieCollection.getString_0(107132488),
										CookieCollection.getString_0(107297582)
									}, CultureInfo.CreateSpecificCulture(CookieCollection.getString_0(107132403)), DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
									{
										cookie.Expires = dateTime.ToLocalTime();
									}
								}
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107131995), comparisonType))
						{
							int maxAge;
							if (cookie != null && text3.Length != 0 && int.TryParse(text3.Unquote(), out maxAge))
							{
								cookie.MaxAge = maxAge;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107250657), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Path = text3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107131950), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Domain = text3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107132020), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Port = text3;
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107251239), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.Comment = CookieCollection.urlDecode(text3, Encoding.UTF8);
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107131973), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.CommentUri = text3.Unquote().ToUri();
							}
						}
						else if (text2.Equals(CookieCollection.getString_0(107131924), comparisonType))
						{
							if (cookie != null && text3.Length != 0)
							{
								cookie.SameSite = text3.Unquote();
							}
						}
						else
						{
							if (cookie != null)
							{
								cookieCollection.add(cookie);
							}
							Cookie.TryCreate(text2, text3, out cookie);
						}
					}
				}
			}
			if (cookie != null)
			{
				cookieCollection.add(cookie);
			}
			return cookieCollection;
		}

		private int search(Cookie cookie)
		{
			for (int i = this._list.Count - 1; i >= 0; i--)
			{
				if (this._list[i].EqualsWithoutValue(cookie))
				{
					return i;
				}
			}
			return -1;
		}

		private static string urlDecode(string s, Encoding encoding)
		{
			string result;
			if (s.IndexOfAny(new char[]
			{
				'%',
				'+'
			}) == -1)
			{
				result = s;
			}
			else
			{
				try
				{
					result = HttpUtility.UrlDecode(s, encoding);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		internal static CookieCollection Parse(string value, bool response)
		{
			CookieCollection result;
			try
			{
				result = (response ? CookieCollection.parseResponse(value) : CookieCollection.parseRequest(value));
			}
			catch (Exception innerException)
			{
				throw new CookieException(CookieCollection.getString_0(107131943), innerException);
			}
			return result;
		}

		internal void SetOrRemove(Cookie cookie)
		{
			int num = this.search(cookie);
			if (num == -1)
			{
				if (!cookie.Expired)
				{
					this._list.Add(cookie);
				}
			}
			else if (cookie.Expired)
			{
				this._list.RemoveAt(num);
			}
			else
			{
				this._list[num] = cookie;
			}
		}

		internal void SetOrRemove(CookieCollection cookies)
		{
			foreach (Cookie orRemove in cookies._list)
			{
				this.SetOrRemove(orRemove);
			}
		}

		internal void Sort()
		{
			if (this._list.Count > 1)
			{
				this._list.Sort(new Comparison<Cookie>(CookieCollection.compareForSort));
			}
		}

		public void Add(Cookie cookie)
		{
			if (this._readOnly)
			{
				string message = CookieCollection.getString_0(107131910);
				throw new InvalidOperationException(message);
			}
			if (cookie == null)
			{
				throw new ArgumentNullException(CookieCollection.getString_0(107134942));
			}
			this.add(cookie);
		}

		public void Add(CookieCollection cookies)
		{
			if (this._readOnly)
			{
				string message = CookieCollection.getString_0(107131910);
				throw new InvalidOperationException(message);
			}
			if (cookies == null)
			{
				throw new ArgumentNullException(CookieCollection.getString_0(107131869));
			}
			foreach (Cookie cookie in cookies._list)
			{
				this.add(cookie);
			}
		}

		public void Clear()
		{
			if (this._readOnly)
			{
				string message = CookieCollection.getString_0(107131910);
				throw new InvalidOperationException(message);
			}
			this._list.Clear();
		}

		public bool Contains(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException(CookieCollection.getString_0(107134942));
			}
			return this.search(cookie) > -1;
		}

		public void CopyTo(Cookie[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException(CookieCollection.getString_0(107299668));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(CookieCollection.getString_0(107350366), CookieCollection.getString_0(107133854));
			}
			if (array.Length - index < this._list.Count)
			{
				string message = CookieCollection.getString_0(107132336);
				throw new ArgumentException(message);
			}
			this._list.CopyTo(array, index);
		}

		public IEnumerator<Cookie> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		public bool Remove(Cookie cookie)
		{
			if (this._readOnly)
			{
				string message = CookieCollection.getString_0(107131910);
				throw new InvalidOperationException(message);
			}
			if (cookie == null)
			{
				throw new ArgumentNullException(CookieCollection.getString_0(107134942));
			}
			int num = this.search(cookie);
			bool result;
			if (num == -1)
			{
				result = false;
			}
			else
			{
				this._list.RemoveAt(num);
				result = true;
			}
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		static CookieCollection()
		{
			Strings.CreateGetStringDelegate(typeof(CookieCollection));
		}

		private List<Cookie> _list;

		private bool _readOnly;

		private object _sync;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
