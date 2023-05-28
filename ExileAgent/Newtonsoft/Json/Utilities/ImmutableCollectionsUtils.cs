using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class ImmutableCollectionsUtils
	{
		internal static bool TryBuildImmutableForArrayContract(Type underlyingType, Type collectionItemType, out Type createdType, out ObjectConstructor<object> parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = ImmutableCollectionsUtils.ArrayContractImmutableCollectionDefinitions.FirstOrDefault((ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						MethodInfo methodInfo = type2.GetMethods().FirstOrDefault((MethodInfo m) => m.Name == ImmutableCollectionsUtils.<>c.getString_0(107315955) && m.GetParameters().Length == 1);
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(new Type[]
							{
								collectionItemType
							});
							MethodInfo method = methodInfo.MakeGenericMethod(new Type[]
							{
								collectionItemType
							});
							parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}

		internal static bool TryBuildImmutableForDictionaryContract(Type underlyingType, Type keyItemType, Type valueItemType, out Type createdType, out ObjectConstructor<object> parameterizedCreator)
		{
			if (underlyingType.IsGenericType())
			{
				Type genericTypeDefinition = underlyingType.GetGenericTypeDefinition();
				string name = genericTypeDefinition.FullName;
				ImmutableCollectionsUtils.ImmutableCollectionTypeInfo immutableCollectionTypeInfo = ImmutableCollectionsUtils.DictionaryContractImmutableCollectionDefinitions.FirstOrDefault((ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) => d.ContractTypeName == name);
				if (immutableCollectionTypeInfo != null)
				{
					Type type = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.CreatedTypeName);
					Type type2 = genericTypeDefinition.Assembly().GetType(immutableCollectionTypeInfo.BuilderTypeName);
					if (type != null && type2 != null)
					{
						MethodInfo methodInfo = type2.GetMethods().FirstOrDefault(delegate(MethodInfo m)
						{
							ParameterInfo[] parameters = m.GetParameters();
							return m.Name == ImmutableCollectionsUtils.<>c.getString_0(107315955) && parameters.Length == 1 && parameters[0].ParameterType.IsGenericType() && parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
						});
						if (methodInfo != null)
						{
							createdType = type.MakeGenericType(new Type[]
							{
								keyItemType,
								valueItemType
							});
							MethodInfo method = methodInfo.MakeGenericMethod(new Type[]
							{
								keyItemType,
								valueItemType
							});
							parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
							return true;
						}
					}
				}
			}
			createdType = null;
			parameterizedCreator = null;
			return false;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ImmutableCollectionsUtils()
		{
			Strings.CreateGetStringDelegate(typeof(ImmutableCollectionsUtils));
			ImmutableCollectionsUtils.ArrayContractImmutableCollectionDefinitions = new List<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
			{
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343663), ImmutableCollectionsUtils.getString_0(107343602), ImmutableCollectionsUtils.getString_0(107343029)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343602), ImmutableCollectionsUtils.getString_0(107343602), ImmutableCollectionsUtils.getString_0(107343029)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343004), ImmutableCollectionsUtils.getString_0(107342939), ImmutableCollectionsUtils.getString_0(107342878)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342939), ImmutableCollectionsUtils.getString_0(107342939), ImmutableCollectionsUtils.getString_0(107342878)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343329), ImmutableCollectionsUtils.getString_0(107343264), ImmutableCollectionsUtils.getString_0(107343203)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343264), ImmutableCollectionsUtils.getString_0(107343264), ImmutableCollectionsUtils.getString_0(107343203)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107343110), ImmutableCollectionsUtils.getString_0(107342537), ImmutableCollectionsUtils.getString_0(107342500)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342537), ImmutableCollectionsUtils.getString_0(107342537), ImmutableCollectionsUtils.getString_0(107342500)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342435), ImmutableCollectionsUtils.getString_0(107342435), ImmutableCollectionsUtils.getString_0(107342370)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342821), ImmutableCollectionsUtils.getString_0(107342821), ImmutableCollectionsUtils.getString_0(107342728))
			};
			ImmutableCollectionsUtils.DictionaryContractImmutableCollectionDefinitions = new List<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>
			{
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342667), ImmutableCollectionsUtils.getString_0(107342598), ImmutableCollectionsUtils.getString_0(107342041)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107342598), ImmutableCollectionsUtils.getString_0(107342598), ImmutableCollectionsUtils.getString_0(107342041)),
				new ImmutableCollectionsUtils.ImmutableCollectionTypeInfo(ImmutableCollectionsUtils.getString_0(107341936), ImmutableCollectionsUtils.getString_0(107341936), ImmutableCollectionsUtils.getString_0(107341867))
			};
		}

		private const string ImmutableListGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableList`1";

		private const string ImmutableQueueGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableQueue`1";

		private const string ImmutableStackGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableStack`1";

		private const string ImmutableSetGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableSet`1";

		private const string ImmutableArrayTypeName = "System.Collections.Immutable.ImmutableArray";

		private const string ImmutableArrayGenericTypeName = "System.Collections.Immutable.ImmutableArray`1";

		private const string ImmutableListTypeName = "System.Collections.Immutable.ImmutableList";

		private const string ImmutableListGenericTypeName = "System.Collections.Immutable.ImmutableList`1";

		private const string ImmutableQueueTypeName = "System.Collections.Immutable.ImmutableQueue";

		private const string ImmutableQueueGenericTypeName = "System.Collections.Immutable.ImmutableQueue`1";

		private const string ImmutableStackTypeName = "System.Collections.Immutable.ImmutableStack";

		private const string ImmutableStackGenericTypeName = "System.Collections.Immutable.ImmutableStack`1";

		private const string ImmutableSortedSetTypeName = "System.Collections.Immutable.ImmutableSortedSet";

		private const string ImmutableSortedSetGenericTypeName = "System.Collections.Immutable.ImmutableSortedSet`1";

		private const string ImmutableHashSetTypeName = "System.Collections.Immutable.ImmutableHashSet";

		private const string ImmutableHashSetGenericTypeName = "System.Collections.Immutable.ImmutableHashSet`1";

		private static readonly IList<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> ArrayContractImmutableCollectionDefinitions;

		private const string ImmutableDictionaryGenericInterfaceTypeName = "System.Collections.Immutable.IImmutableDictionary`2";

		private const string ImmutableDictionaryTypeName = "System.Collections.Immutable.ImmutableDictionary";

		private const string ImmutableDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableDictionary`2";

		private const string ImmutableSortedDictionaryTypeName = "System.Collections.Immutable.ImmutableSortedDictionary";

		private const string ImmutableSortedDictionaryGenericTypeName = "System.Collections.Immutable.ImmutableSortedDictionary`2";

		private static readonly IList<ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> DictionaryContractImmutableCollectionDefinitions;

		[NonSerialized]
		internal static GetString getString_0;

		internal sealed class ImmutableCollectionTypeInfo
		{
			public ImmutableCollectionTypeInfo(string contractTypeName, string createdTypeName, string builderTypeName)
			{
				this.ContractTypeName = contractTypeName;
				this.CreatedTypeName = createdTypeName;
				this.BuilderTypeName = builderTypeName;
			}

			public string ContractTypeName { get; set; }

			public string CreatedTypeName { get; set; }

			public string BuilderTypeName { get; set; }
		}
	}
}
