using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class DefaultSerializationBinder : SerializationBinder, ISerializationBinder
	{
		public DefaultSerializationBinder()
		{
			this._typeCache = new ThreadSafeStore<StructMultiKey<string, string>, Type>(new Func<StructMultiKey<string, string>, Type>(this.GetTypeFromTypeNameKey));
		}

		private Type GetTypeFromTypeNameKey(StructMultiKey<string, string> typeNameKey)
		{
			string value = typeNameKey.Value1;
			string value2 = typeNameKey.Value2;
			if (value == null)
			{
				return Type.GetType(value2);
			}
			Assembly assembly = Assembly.LoadWithPartialName(value);
			if (assembly == null)
			{
				foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (assembly2.FullName == value || assembly2.GetName().Name == value)
					{
						assembly = assembly2;
						break;
					}
				}
			}
			if (assembly == null)
			{
				throw new JsonSerializationException(DefaultSerializationBinder.getString_0(107339358).FormatWith(CultureInfo.InvariantCulture, value));
			}
			Type type = assembly.GetType(value2);
			if (type == null)
			{
				if (value2.IndexOf('`') >= 0)
				{
					try
					{
						type = this.GetGenericTypeFromTypeName(value2, assembly);
					}
					catch (Exception innerException)
					{
						throw new JsonSerializationException(DefaultSerializationBinder.getString_0(107339317).FormatWith(CultureInfo.InvariantCulture, value2, assembly.FullName), innerException);
					}
				}
				if (type == null)
				{
					throw new JsonSerializationException(DefaultSerializationBinder.getString_0(107339317).FormatWith(CultureInfo.InvariantCulture, value2, assembly.FullName));
				}
			}
			return type;
		}

		private Type GetGenericTypeFromTypeName(string typeName, Assembly assembly)
		{
			Type result = null;
			int num = typeName.IndexOf('[');
			if (num >= 0)
			{
				string name = typeName.Substring(0, num);
				Type type = assembly.GetType(name);
				if (type != null)
				{
					List<Type> list = new List<Type>();
					int num2 = 0;
					int num3 = 0;
					int num4 = typeName.Length - 1;
					for (int i = num + 1; i < num4; i++)
					{
						char c = typeName[i];
						if (c != '[')
						{
							if (c == ']')
							{
								num2--;
								if (num2 == 0)
								{
									StructMultiKey<string, string> typeNameKey = ReflectionUtils.SplitFullyQualifiedTypeName(typeName.Substring(num3, i - num3));
									list.Add(this.GetTypeByName(typeNameKey));
								}
							}
						}
						else
						{
							if (num2 == 0)
							{
								num3 = i + 1;
							}
							num2++;
						}
					}
					result = type.MakeGenericType(list.ToArray());
				}
			}
			return result;
		}

		private Type GetTypeByName(StructMultiKey<string, string> typeNameKey)
		{
			return this._typeCache.Get(typeNameKey);
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			return this.GetTypeByName(new StructMultiKey<string, string>(assemblyName, typeName));
		}

		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = serializedType.Assembly.FullName;
			typeName = serializedType.FullName;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DefaultSerializationBinder()
		{
			Strings.CreateGetStringDelegate(typeof(DefaultSerializationBinder));
			DefaultSerializationBinder.Instance = new DefaultSerializationBinder();
		}

		internal static readonly DefaultSerializationBinder Instance;

		private readonly ThreadSafeStore<StructMultiKey<string, string>, Type> _typeCache;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
