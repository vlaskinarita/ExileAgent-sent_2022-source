using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	internal static class Tools
	{
		internal static bool IsDefinedAttribute(this Type type, Type attributeType)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107241168));
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107241071));
			}
			return type.IsDefined(attributeType, false);
		}

		internal static IEnumerable<PropertyInfo> GetPropertiesDefinedXPath(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107241442));
			}
			return type.GetProperties().HAPWhere((PropertyInfo x) => x.IsDefined(typeof(XPathAttribute), false));
		}

		internal static bool IsIEnumerable(this PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107241357));
			}
			return !(propertyInfo.PropertyType == typeof(string)) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
		}

		internal static IEnumerable<Type> GetGenericTypes(this PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240724));
			}
			return propertyInfo.PropertyType.GetGenericArguments();
		}

		internal static MethodInfo GetMethodByItsName(this Type type, string methodName)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240627));
			}
			if (methodName == null || methodName == Tools.getString_0(107399845))
			{
				throw new ArgumentNullException(Tools.getString_0(107240554));
			}
			return type.GetMethod(methodName);
		}

		internal static IList CreateIListOfType(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240977));
			}
			return Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
			{
				type
			})) as IList;
		}

		internal static T GetNodeValueBasedOnXPathReturnType<T>(HtmlNode htmlNode, XPathAttribute xPathAttribute)
		{
			if (htmlNode == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240908));
			}
			if (xPathAttribute == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240839));
			}
			Type typeFromHandle = typeof(T);
			object obj;
			switch (xPathAttribute.NodeReturnType)
			{
			case ReturnType.InnerText:
				obj = Convert.ChangeType(htmlNode.InnerText, typeFromHandle);
				break;
			case ReturnType.InnerHtml:
				obj = Convert.ChangeType(htmlNode.InnerHtml, typeFromHandle);
				break;
			case ReturnType.OuterHtml:
				obj = Convert.ChangeType(htmlNode.OuterHtml, typeFromHandle);
				break;
			default:
				throw new Exception();
			}
			return (T)((object)obj);
		}

		internal static IList GetNodesValuesBasedOnXPathReturnType(HtmlNodeCollection htmlNodeCollection, XPathAttribute xPathAttribute, Type listGenericType)
		{
			if (htmlNodeCollection != null && htmlNodeCollection.Count != 0)
			{
				if (xPathAttribute == null)
				{
					throw new ArgumentNullException(Tools.getString_0(107240839));
				}
				IList list = listGenericType.CreateIListOfType();
				switch (xPathAttribute.NodeReturnType)
				{
				case ReturnType.InnerText:
					using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>)htmlNodeCollection).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							HtmlNode htmlNode = enumerator.Current;
							list.Add(Convert.ChangeType(htmlNode.InnerText, listGenericType));
						}
						return list;
					}
					break;
				case ReturnType.InnerHtml:
					break;
				case ReturnType.OuterHtml:
					goto IL_C3;
				default:
					return list;
				}
				using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>)htmlNodeCollection).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HtmlNode htmlNode2 = enumerator.Current;
						list.Add(Convert.ChangeType(htmlNode2.InnerHtml, listGenericType));
					}
					return list;
				}
				IL_C3:
				using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>)htmlNodeCollection).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HtmlNode htmlNode3 = enumerator.Current;
						list.Add(Convert.ChangeType(htmlNode3.OuterHtml, listGenericType));
					}
					return list;
				}
			}
			throw new ArgumentNullException(Tools.getString_0(107240794));
		}

		internal static IEnumerable<TSource> HAPWhere<TSource>(this IEnumerable<TSource> source, Tools.HAPFunc<TSource, bool> predicate)
		{
			Tools.<HAPWhere>d__9<TSource> <HAPWhere>d__ = new Tools.<HAPWhere>d__9<TSource>(-2);
			<HAPWhere>d__.<>3__source = source;
			<HAPWhere>d__.<>3__predicate = predicate;
			return <HAPWhere>d__;
		}

		internal static bool IsInstantiable(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240729));
			}
			return !(type.GetConstructor(Type.EmptyTypes) == null);
		}

		internal static int CountOfIEnumerable<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(Tools.getString_0(107240200));
			}
			int num = 0;
			foreach (T t in source)
			{
				num++;
			}
			return num;
		}

		static Tools()
		{
			Strings.CreateGetStringDelegate(typeof(Tools));
		}

		[NonSerialized]
		internal static GetString getString_0;

		internal delegate TResult HAPFunc<T, TResult>(T arg);
	}
}
