using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(CamelCasePropertyNamesContractResolver.getString_1(107374107));
			}
			StructMultiKey<Type, Type> key = new StructMultiKey<Type, Type>(base.GetType(), type);
			Dictionary<StructMultiKey<Type, Type>, JsonContract> contractCache = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (contractCache == null || !contractCache.TryGetValue(key, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					contractCache = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary = (contractCache != null) ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(contractCache) : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
					dictionary[key] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary;
				}
			}
			return jsonContract;
		}

		internal override DefaultJsonNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static CamelCasePropertyNamesContractResolver()
		{
			Strings.CreateGetStringDelegate(typeof(CamelCasePropertyNamesContractResolver));
			CamelCasePropertyNamesContractResolver.TypeContractCacheLock = new object();
			CamelCasePropertyNamesContractResolver.NameTable = new DefaultJsonNameTable();
		}

		private static readonly object TypeContractCacheLock;

		private static readonly DefaultJsonNameTable NameTable;

		private static Dictionary<StructMultiKey<Type, Type>, JsonContract> _contractCache;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
