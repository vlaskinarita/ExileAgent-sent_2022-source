using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class ReflectionUtils
	{
		static ReflectionUtils()
		{
			Strings.CreateGetStringDelegate(typeof(ReflectionUtils));
			ReflectionUtils.EmptyTypes = Type.EmptyTypes;
		}

		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, ReflectionUtils.getString_0(107344295));
			MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod(true);
			return methodInfo != null && methodInfo.IsVirtual;
		}

		public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, ReflectionUtils.getString_0(107344295));
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetBaseDefinition();
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (setMethod == null)
			{
				return null;
			}
			return setMethod.GetBaseDefinition();
		}

		public static bool IsPublic(PropertyInfo property)
		{
			return (property.GetGetMethod() != null && property.GetGetMethod().IsPublic) || (property.GetSetMethod() != null && property.GetSetMethod().IsPublic);
		}

		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		public static string GetTypeName(Type t, TypeNameAssemblyFormatHandling assemblyFormat, ISerializationBinder binder)
		{
			string fullyQualifiedTypeName = ReflectionUtils.GetFullyQualifiedTypeName(t, binder);
			if (assemblyFormat == TypeNameAssemblyFormatHandling.Simple)
			{
				return ReflectionUtils.RemoveAssemblyDetails(fullyQualifiedTypeName);
			}
			if (assemblyFormat != TypeNameAssemblyFormatHandling.Full)
			{
				throw new ArgumentOutOfRangeException();
			}
			return fullyQualifiedTypeName;
		}

		private static string GetFullyQualifiedTypeName(Type t, ISerializationBinder binder)
		{
			if (binder != null)
			{
				string text;
				string str;
				binder.BindToName(t, out text, out str);
				return str + ((text == null) ? ReflectionUtils.getString_0(107401403) : (ReflectionUtils.getString_0(107401486) + text));
			}
			return t.AssemblyQualifiedName;
		}

		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				if (c != ',')
				{
					if (c != '[')
					{
						if (c != ']')
						{
							if (!flag2)
							{
								stringBuilder.Append(c);
							}
						}
						else
						{
							flag = false;
							flag2 = false;
							stringBuilder.Append(c);
						}
					}
					else
					{
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
					}
				}
				else if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, ReflectionUtils.getString_0(107341681));
			return t.IsValueType() || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return t.GetConstructors(bindingFlags).SingleOrDefault((ConstructorInfo c) => !c.GetParameters().Any<ParameterInfo>());
		}

		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, ReflectionUtils.getString_0(107341681));
			return !t.IsValueType() || ReflectionUtils.IsNullableType(t);
		}

		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, ReflectionUtils.getString_0(107341681));
			return t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		public static Type EnsureNotNullableType(Type t)
		{
			if (!ReflectionUtils.IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		public static Type EnsureNotByRefType(Type t)
		{
			if (t.IsByRef && t.HasElementType)
			{
				return t.GetElementType();
			}
			return t;
		}

		public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == genericInterfaceDefinition;
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, ReflectionUtils.getString_0(107374066));
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, ReflectionUtils.getString_0(107341708));
			if (genericInterfaceDefinition.IsInterface() && genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				if (type.IsInterface() && type.IsGenericType())
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition)
					{
						implementingType = type;
						return true;
					}
				}
				foreach (Type type2 in type.GetInterfaces())
				{
					if (type2.IsGenericType())
					{
						Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
						if (genericInterfaceDefinition == genericTypeDefinition2)
						{
							implementingType = type2;
							return true;
						}
					}
				}
				implementingType = null;
				return false;
			}
			throw new ArgumentNullException(ReflectionUtils.getString_0(107341671).FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, ReflectionUtils.getString_0(107374066));
			ValidationUtils.ArgumentNotNull(genericClassDefinition, ReflectionUtils.getString_0(107341098));
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException(ReflectionUtils.getString_0(107341065).FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
		{
			while (!currentType.IsGenericType() || !(genericClassDefinition == currentType.GetGenericTypeDefinition()))
			{
				currentType = currentType.BaseType();
				if (!(currentType != null))
				{
					implementingType = null;
					return false;
				}
			}
			implementingType = currentType;
			return true;
		}

		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, ReflectionUtils.getString_0(107374066));
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable<>), out type2))
			{
				if (type2.IsGenericTypeDefinition())
				{
					throw new Exception(ReflectionUtils.getString_0(107340976).FormatWith(CultureInfo.InvariantCulture, type));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (!typeof(IEnumerable).IsAssignableFrom(type))
				{
					throw new Exception(ReflectionUtils.getString_0(107340976).FormatWith(CultureInfo.InvariantCulture, type));
				}
				return null;
			}
		}

		public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, ReflectionUtils.getString_0(107340967));
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out type))
			{
				if (type.IsGenericTypeDefinition())
				{
					throw new Exception(ReflectionUtils.getString_0(107340914).FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (!typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					throw new Exception(ReflectionUtils.getString_0(107340914).FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				keyType = null;
				valueType = null;
				return;
			}
		}

		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, ReflectionUtils.getString_0(107340905));
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes <= MemberTypes.Field)
			{
				if (memberTypes == MemberTypes.Event)
				{
					return ((EventInfo)member).EventHandlerType;
				}
				if (memberTypes == MemberTypes.Field)
				{
					return ((FieldInfo)member).FieldType;
				}
			}
			else
			{
				if (memberTypes == MemberTypes.Method)
				{
					return ((MethodInfo)member).ReturnType;
				}
				if (memberTypes == MemberTypes.Property)
				{
					return ((PropertyInfo)member).PropertyType;
				}
			}
			throw new ArgumentException(ReflectionUtils.getString_0(107340896), ReflectionUtils.getString_0(107340905));
		}

		public static bool IsByRefLikeType(Type type)
		{
			if (!type.IsValueType())
			{
				return false;
			}
			Attribute[] attributes = ReflectionUtils.GetAttributes(type, null, false);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (string.Equals(attributes[i].GetType().FullName, ReflectionUtils.getString_0(107341275), StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, ReflectionUtils.getString_0(107341202));
			return property.GetIndexParameters().Length != 0;
		}

		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, ReflectionUtils.getString_0(107340905));
			ValidationUtils.ArgumentNotNull(target, ReflectionUtils.getString_0(107350296));
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				return ((FieldInfo)member).GetValue(target);
			}
			if (memberTypes != MemberTypes.Property)
			{
				throw new ArgumentException(ReflectionUtils.getString_0(107341136).FormatWith(CultureInfo.InvariantCulture, member.Name), ReflectionUtils.getString_0(107340905));
			}
			object value;
			try
			{
				value = ((PropertyInfo)member).GetValue(target, null);
			}
			catch (TargetParameterCountException innerException)
			{
				throw new ArgumentException(ReflectionUtils.getString_0(107341221).FormatWith(CultureInfo.InvariantCulture, member.Name), innerException);
			}
			return value;
		}

		public static void SetMemberValue(MemberInfo member, object target, object value)
		{
			ValidationUtils.ArgumentNotNull(member, ReflectionUtils.getString_0(107340905));
			ValidationUtils.ArgumentNotNull(target, ReflectionUtils.getString_0(107350296));
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				((FieldInfo)member).SetValue(target, value);
				return;
			}
			if (memberTypes != MemberTypes.Property)
			{
				throw new ArgumentException(ReflectionUtils.getString_0(107340579).FormatWith(CultureInfo.InvariantCulture, member.Name), ReflectionUtils.getString_0(107340905));
			}
			((PropertyInfo)member).SetValue(target, value, null);
		}

		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			if (memberTypes != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
		}

		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			MemberTypes memberTypes = member.MemberType();
			if (memberTypes == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return !fieldInfo.IsLiteral && (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			if (memberTypes != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
		}

		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			foreach (IGrouping<string, MemberInfo> grouping in from m in list
			group m by m.Name)
			{
				if (grouping.Count<MemberInfo>() == 1)
				{
					list2.Add(grouping.First<MemberInfo>());
				}
				else
				{
					List<MemberInfo> list3 = new List<MemberInfo>();
					using (IEnumerator<MemberInfo> enumerator2 = grouping.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							MemberInfo memberInfo = enumerator2.Current;
							if (list3.Count == 0)
							{
								list3.Add(memberInfo);
							}
							else if ((!ReflectionUtils.IsOverridenGenericMember(memberInfo, bindingAttr) || memberInfo.Name == ReflectionUtils.getString_0(107241380)) && !list3.Any((MemberInfo m) => m.DeclaringType == memberInfo.DeclaringType))
							{
								list3.Add(memberInfo);
							}
						}
					}
					list2.AddRange(list3);
				}
			}
			return list2;
		}

		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType() != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			if (!propertyInfo.IsVirtual())
			{
				return false;
			}
			Type declaringType = propertyInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
			return member.Length != 0 && ReflectionUtils.GetMemberUnderlyingType(member[0]).IsGenericParameter;
		}

		public static T GetAttribute<T>(object attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		public static T GetAttribute<T>(object attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return default(T);
			}
			return attributes.FirstOrDefault<T>();
		}

		public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : Attribute
		{
			Attribute[] attributes = ReflectionUtils.GetAttributes(attributeProvider, typeof(T), inherit);
			T[] result;
			if ((result = (attributes as T[])) != null)
			{
				return result;
			}
			return attributes.Cast<T>().ToArray<T>();
		}

		public static Attribute[] GetAttributes(object attributeProvider, Type attributeType, bool inherit)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, ReflectionUtils.getString_0(107340466));
			if (attributeProvider != null)
			{
				Type type;
				if ((type = (attributeProvider as Type)) != null)
				{
					Type type2 = type;
					return ((attributeType != null) ? type2.GetCustomAttributes(attributeType, inherit) : type2.GetCustomAttributes(inherit)).Cast<Attribute>().ToArray<Attribute>();
				}
				Assembly assembly;
				if ((assembly = (attributeProvider as Assembly)) == null)
				{
					MemberInfo memberInfo;
					if ((memberInfo = (attributeProvider as MemberInfo)) == null)
					{
						Module module;
						if ((module = (attributeProvider as Module)) == null)
						{
							ParameterInfo parameterInfo;
							if ((parameterInfo = (attributeProvider as ParameterInfo)) != null)
							{
								ParameterInfo element = parameterInfo;
								if (!(attributeType != null))
								{
									return Attribute.GetCustomAttributes(element, inherit);
								}
								return Attribute.GetCustomAttributes(element, attributeType, inherit);
							}
						}
						else
						{
							Module element2 = module;
							if (!(attributeType != null))
							{
								return Attribute.GetCustomAttributes(element2, inherit);
							}
							return Attribute.GetCustomAttributes(element2, attributeType, inherit);
						}
					}
					else
					{
						MemberInfo element3 = memberInfo;
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(element3, inherit);
						}
						return Attribute.GetCustomAttributes(element3, attributeType, inherit);
					}
				}
				else
				{
					Assembly element4 = assembly;
					if (!(attributeType != null))
					{
						return Attribute.GetCustomAttributes(element4);
					}
					return Attribute.GetCustomAttributes(element4, attributeType);
				}
			}
			ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
			return (Attribute[])((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
		}

		public static StructMultiKey<string, string> SplitFullyQualifiedTypeName(string fullyQualifiedTypeName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			string v;
			string v2;
			if (assemblyDelimiterIndex != null)
			{
				v = fullyQualifiedTypeName.Trim(0, assemblyDelimiterIndex.GetValueOrDefault());
				v2 = fullyQualifiedTypeName.Trim(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1);
			}
			else
			{
				v = fullyQualifiedTypeName;
				v2 = null;
			}
			return new StructMultiKey<string, string>(v2, v);
		}

		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName[i];
				if (c != ',')
				{
					if (c != '[')
					{
						if (c == ']')
						{
							num--;
						}
					}
					else
					{
						num++;
					}
				}
				else if (num == 0)
				{
					return new int?(i);
				}
			}
			return null;
		}

		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			MemberTypes memberTypes = memberInfo.MemberType();
			if (memberTypes == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] types = (from p in propertyInfo.GetIndexParameters()
				select p.ParameterType).ToArray<Type>();
				return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
			}
			return targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault<MemberInfo>();
		}

		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, ReflectionUtils.getString_0(107344990));
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			ReflectionUtils.GetChildPrivateFields(list, targetType, bindingAttr);
			return list.Cast<FieldInfo>();
		}

		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType()) != null)
				{
					IEnumerable<FieldInfo> collection = from f in targetType.GetFields(bindingAttr2)
					where f.IsPrivate
					select f;
					initialFields.AddRange(collection);
				}
			}
		}

		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, ReflectionUtils.getString_0(107344990));
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			if (targetType.IsInterface())
			{
				foreach (Type type in targetType.GetInterfaces())
				{
					list.AddRange(type.GetProperties(bindingAttr));
				}
			}
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo = list[j];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo value = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[j] = value;
				}
			}
			return list;
		}

		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			while ((targetType = targetType.BaseType()) != null)
			{
				foreach (PropertyInfo subTypeProperty in targetType.GetProperties(bindingAttr))
				{
					ReflectionUtils.<>c__DisplayClass44_0 CS$<>8__locals1 = new ReflectionUtils.<>c__DisplayClass44_0();
					CS$<>8__locals1.subTypeProperty = subTypeProperty;
					if (CS$<>8__locals1.subTypeProperty.IsVirtual())
					{
						ReflectionUtils.<>c__DisplayClass44_1 CS$<>8__locals2 = new ReflectionUtils.<>c__DisplayClass44_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						ReflectionUtils.<>c__DisplayClass44_1 CS$<>8__locals3 = CS$<>8__locals2;
						MethodInfo baseDefinition = CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.GetBaseDefinition();
						if (baseDefinition == null)
						{
							goto IL_E3;
						}
						Type declaringType;
						if ((declaringType = baseDefinition.DeclaringType) == null)
						{
							goto IL_E3;
						}
						IL_F5:
						CS$<>8__locals3.subTypePropertyDeclaringType = declaringType;
						if (initialProperties.IndexOf(delegate(PropertyInfo p)
						{
							if (p.Name == CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.Name && p.IsVirtual())
							{
								MethodInfo baseDefinition2 = p.GetBaseDefinition();
								Type declaringType2;
								if (baseDefinition2 != null)
								{
									if ((declaringType2 = baseDefinition2.DeclaringType) != null)
									{
										goto IL_41;
									}
								}
								declaringType2 = p.DeclaringType;
								IL_41:
								return declaringType2.IsAssignableFrom(CS$<>8__locals2.subTypePropertyDeclaringType);
							}
							return false;
						}) == -1)
						{
							initialProperties.Add(CS$<>8__locals2.CS$<>8__locals1.subTypeProperty);
							goto IL_122;
						}
						goto IL_122;
						IL_E3:
						declaringType = CS$<>8__locals2.CS$<>8__locals1.subTypeProperty.DeclaringType;
						goto IL_F5;
					}
					if (!ReflectionUtils.IsPublic(CS$<>8__locals1.subTypeProperty))
					{
						int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name);
						if (num == -1)
						{
							initialProperties.Add(CS$<>8__locals1.subTypeProperty);
						}
						else if (!ReflectionUtils.IsPublic(initialProperties[num]))
						{
							initialProperties[num] = CS$<>8__locals1.subTypeProperty;
						}
					}
					else if (initialProperties.IndexOf((PropertyInfo p) => p.Name == CS$<>8__locals1.subTypeProperty.Name && p.DeclaringType == CS$<>8__locals1.subTypeProperty.DeclaringType) == -1)
					{
						initialProperties.Add(CS$<>8__locals1.subTypeProperty);
					}
					IL_122:;
				}
			}
		}

		public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
		{
			return currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any((MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
		}

		public static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
				return 0;
			case PrimitiveTypeCode.CharNullable:
			case PrimitiveTypeCode.BooleanNullable:
			case PrimitiveTypeCode.SByteNullable:
			case PrimitiveTypeCode.Int16Nullable:
			case PrimitiveTypeCode.UInt16Nullable:
			case PrimitiveTypeCode.Int32Nullable:
			case PrimitiveTypeCode.ByteNullable:
			case PrimitiveTypeCode.UInt32Nullable:
			case PrimitiveTypeCode.Int64Nullable:
			case PrimitiveTypeCode.UInt64Nullable:
			case PrimitiveTypeCode.SingleNullable:
			case PrimitiveTypeCode.DoubleNullable:
			case PrimitiveTypeCode.DateTimeNullable:
			case PrimitiveTypeCode.DateTimeOffsetNullable:
			case PrimitiveTypeCode.DecimalNullable:
				break;
			case PrimitiveTypeCode.Boolean:
				return false;
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return 0L;
			case PrimitiveTypeCode.Single:
				return 0f;
			case PrimitiveTypeCode.Double:
				return 0.0;
			case PrimitiveTypeCode.DateTime:
				return default(DateTime);
			case PrimitiveTypeCode.DateTimeOffset:
				return default(DateTimeOffset);
			case PrimitiveTypeCode.Decimal:
				return 0m;
			case PrimitiveTypeCode.Guid:
				return default(Guid);
			default:
				if (typeCode == PrimitiveTypeCode.BigInteger)
				{
					return default(BigInteger);
				}
				break;
			}
			if (ReflectionUtils.IsNullable(type))
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		public static readonly Type[] EmptyTypes;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
