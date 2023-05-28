using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	internal sealed class NameValuePairList
	{
		internal NameValuePairList() : this(null)
		{
		}

		internal NameValuePairList(string text)
		{
			this.Text = text;
			this._allPairs = new List<KeyValuePair<string, string>>();
			this._pairsWithName = new Dictionary<string, List<KeyValuePair<string, string>>>();
			this.Parse(text);
		}

		internal static string GetNameValuePairsValue(string text, string name)
		{
			return new NameValuePairList(text).GetNameValuePairValue(name);
		}

		internal List<KeyValuePair<string, string>> GetNameValuePairs(string name)
		{
			if (name == null)
			{
				return this._allPairs;
			}
			if (!this._pairsWithName.ContainsKey(name))
			{
				return new List<KeyValuePair<string, string>>();
			}
			return this._pairsWithName[name];
		}

		internal string GetNameValuePairValue(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			List<KeyValuePair<string, string>> nameValuePairs = this.GetNameValuePairs(name);
			if (nameValuePairs.Count == 0)
			{
				return string.Empty;
			}
			return nameValuePairs[0].Value.Trim();
		}

		private void Parse(string text)
		{
			this._allPairs.Clear();
			this._pairsWithName.Clear();
			if (text == null)
			{
				return;
			}
			foreach (string text2 in text.Split(new char[]
			{
				';'
			}))
			{
				if (text2.Length != 0)
				{
					string[] array2 = text2.Split(new char[]
					{
						'='
					}, 2);
					if (array2.Length != 0)
					{
						KeyValuePair<string, string> item = new KeyValuePair<string, string>(array2[0].Trim().ToLowerInvariant(), (array2.Length < 2) ? NameValuePairList.getString_0(107400007) : array2[1]);
						this._allPairs.Add(item);
						List<KeyValuePair<string, string>> list;
						if (!this._pairsWithName.TryGetValue(item.Key, out list))
						{
							list = new List<KeyValuePair<string, string>>();
							this._pairsWithName.Add(item.Key, list);
						}
						list.Add(item);
					}
				}
			}
		}

		static NameValuePairList()
		{
			Strings.CreateGetStringDelegate(typeof(NameValuePairList));
		}

		internal readonly string Text;

		private List<KeyValuePair<string, string>> _allPairs;

		private Dictionary<string, List<KeyValuePair<string, string>>> _pairsWithName;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
