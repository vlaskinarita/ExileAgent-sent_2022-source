using System;
using System.Collections.Generic;

namespace HtmlAgilityPack
{
	internal static class Utilities
	{
		public static TValue GetDictionaryValueOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue)) where TKey : class
		{
			TValue result;
			if (!dict.TryGetValue(key, out result))
			{
				return defaultValue;
			}
			return result;
		}
	}
}
