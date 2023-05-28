using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal static class JsonTypeReflector
	{
		public static T GetCachedAttribute<T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, JsonTypeReflector.getString_0(107297427), StringComparison.Ordinal) && !string.Equals(type2.FullName, JsonTypeReflector.getString_0(107297402), StringComparison.Ordinal) && !string.Equals(type2.FullName, JsonTypeReflector.getString_0(107296833), StringComparison.Ordinal) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			Type type2 = type;
			while (type2 != null)
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = type2.BaseType();
			}
			return null;
		}

		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (attribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (attribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return attribute;
		}

		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (JsonTypeReflector.GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && JsonTypeReflector.IsSerializable(objectType))
			{
				return MemberSerialization.Fields;
			}
			return MemberSerialization.OptOut;
		}

		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = JsonTypeReflector.CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		public static JsonConverter CreateJsonConverterInstance(Type converterType, object[] args)
		{
			return (JsonConverter)JsonTypeReflector.CreatorCache.Get(converterType)(args);
		}

		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, object[] args)
		{
			return (NamingStrategy)JsonTypeReflector.CreatorCache.Get(namingStrategyType)(args);
		}

		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = JsonTypeReflector.CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		private static Func<object[], object> GetCreator(Type type)
		{
			Func<object> defaultConstructor = ReflectionUtils.HasDefaultConstructor(type, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null;
			return delegate(object[] parameters)
			{
				object result;
				try
				{
					if (parameters != null)
					{
						Type[] types = parameters.Select(delegate(object param)
						{
							if (param == null)
							{
								throw new InvalidOperationException(JsonTypeReflector.<>c.getString_0(107315229));
							}
							return param.GetType();
						}).ToArray<Type>();
						ConstructorInfo constructor = type.GetConstructor(types);
						if (!(constructor != null))
						{
							throw new JsonException(JsonTypeReflector.<>c__DisplayClass22_0.getString_0(107315905).FormatWith(CultureInfo.InvariantCulture, type));
						}
						result = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (defaultConstructor == null)
						{
							throw new JsonException(JsonTypeReflector.<>c__DisplayClass22_0.getString_0(107315832).FormatWith(CultureInfo.InvariantCulture, type));
						}
						result = defaultConstructor();
					}
				}
				catch (Exception innerException)
				{
					throw new JsonException(JsonTypeReflector.<>c__DisplayClass22_0.getString_0(107315255).FormatWith(CultureInfo.InvariantCulture, type), innerException);
				}
				return result;
			};
		}

		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			foreach (Attribute attribute in ReflectionUtils.GetAttributes(type, null, true))
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, JsonTypeReflector.getString_0(107296768), StringComparison.Ordinal))
				{
					if (JsonTypeReflector._metadataTypeAttributeReflectionObject == null)
					{
						JsonTypeReflector._metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, new string[]
						{
							JsonTypeReflector.getString_0(107296655)
						});
					}
					return (Type)JsonTypeReflector._metadataTypeAttributeReflectionObject.GetValue(attribute, JsonTypeReflector.getString_0(107296655));
				}
			}
			return null;
		}

		private static T GetAttribute<T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T attribute;
			if (associatedMetadataType != null)
			{
				attribute = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(type, true);
			if (attribute != null)
			{
				return attribute;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				attribute = ReflectionUtils.GetAttribute<T>(interfaces[i], true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return default(T);
		}

		private static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T attribute;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (attribute != null)
			{
				return attribute;
			}
			if (memberInfo.DeclaringType != null)
			{
				Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (attribute != null)
						{
							return attribute;
						}
					}
				}
			}
			return default(T);
		}

		public static bool IsNonSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<NonSerializedAttribute>(provider, false) != null;
		}

		public static bool IsSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<SerializableAttribute>(provider, false) != null;
		}

		public static T GetAttribute<T>(object provider) where T : Attribute
		{
			Type type;
			if ((type = (provider as Type)) != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo;
			if ((memberInfo = (provider as MemberInfo)) != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}

		public static bool DynamicCodeGeneration
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					try
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
						new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Demand();
						new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
						new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
						new SecurityPermission(PermissionState.Unrestricted).Demand();
						JsonTypeReflector._dynamicCodeGeneration = new bool?(true);
					}
					catch (Exception)
					{
						JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
					}
				}
				return JsonTypeReflector._dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					JsonTypeReflector._fullyTrusted = new bool?(currentDomain.IsHomogenous && currentDomain.IsFullyTrusted);
				}
				return JsonTypeReflector._fullyTrusted.GetValueOrDefault();
			}
		}

		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				if (JsonTypeReflector.DynamicCodeGeneration)
				{
					return DynamicReflectionDelegateFactory.Instance;
				}
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JsonTypeReflector()
		{
			Strings.CreateGetStringDelegate(typeof(JsonTypeReflector));
			JsonTypeReflector.CreatorCache = new ThreadSafeStore<Type, Func<object[], object>>(new Func<Type, Func<object[], object>>(JsonTypeReflector.GetCreator));
			JsonTypeReflector.AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));
		}

		private static bool? _dynamicCodeGeneration;

		private static bool? _fullyTrusted;

		public const string IdPropertyName = "$id";

		public const string RefPropertyName = "$ref";

		public const string TypePropertyName = "$type";

		public const string ValuePropertyName = "$value";

		public const string ArrayValuesPropertyName = "$values";

		public const string ShouldSerializePrefix = "ShouldSerialize";

		public const string SpecifiedPostfix = "Specified";

		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		private static readonly ThreadSafeStore<Type, Func<object[], object>> CreatorCache;

		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache;

		private static ReflectionObject _metadataTypeAttributeReflectionObject;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
