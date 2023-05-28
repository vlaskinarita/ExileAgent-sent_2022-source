using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class FSharpUtils
	{
		public static Assembly FSharpCoreAssembly { get; private set; }

		public static MethodCall<object, object> IsUnion { get; private set; }

		public static MethodCall<object, object> GetUnionCases { get; private set; }

		public static MethodCall<object, object> PreComputeUnionTagReader { get; private set; }

		public static MethodCall<object, object> PreComputeUnionReader { get; private set; }

		public static MethodCall<object, object> PreComputeUnionConstructor { get; private set; }

		public static Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		public static Func<object, object> GetUnionCaseInfoName { get; private set; }

		public static Func<object, object> GetUnionCaseInfoTag { get; private set; }

		public static MethodCall<object, object> GetUnionCaseInfoFields { get; private set; }

		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (!FSharpUtils._initialized)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (!FSharpUtils._initialized)
					{
						FSharpUtils.FSharpCoreAssembly = fsharpCoreAssembly;
						Type type = fsharpCoreAssembly.GetType(FSharpUtils.getString_0(107344118));
						MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, FSharpUtils.getString_0(107344097), BindingFlags.Static | BindingFlags.Public);
						FSharpUtils.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
						MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, FSharpUtils.getString_0(107343540), BindingFlags.Static | BindingFlags.Public);
						FSharpUtils.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
						Type type2 = fsharpCoreAssembly.GetType(FSharpUtils.getString_0(107343551));
						FSharpUtils.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, FSharpUtils.getString_0(107343466));
						FSharpUtils.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, FSharpUtils.getString_0(107343433));
						FSharpUtils.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, FSharpUtils.getString_0(107343404));
						Type type3 = fsharpCoreAssembly.GetType(FSharpUtils.getString_0(107343399));
						FSharpUtils.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty(FSharpUtils.getString_0(107402489)));
						FSharpUtils.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty(FSharpUtils.getString_0(107343310)));
						FSharpUtils.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty(FSharpUtils.getString_0(107343305)));
						FSharpUtils.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod(FSharpUtils.getString_0(107343796)));
						FSharpUtils._ofSeq = fsharpCoreAssembly.GetType(FSharpUtils.getString_0(107343815)).GetMethod(FSharpUtils.getString_0(107343730));
						FSharpUtils._mapType = fsharpCoreAssembly.GetType(FSharpUtils.getString_0(107343721));
						Thread.MemoryBarrier();
						FSharpUtils._initialized = true;
					}
				}
			}
		}

		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo method = type.GetMethod(methodName, bindingFlags);
			if (method == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				method = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return method;
		}

		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod(FSharpUtils.getString_0(107343664), BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object target, object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		public static ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo method = FSharpUtils._ofSeq.MakeGenericMethod(new Type[]
			{
				t
			});
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
		}

		public static ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod(FSharpUtils.getString_0(107343687)).MakeGenericMethod(new Type[]
			{
				keyType,
				valueType
			}).Invoke(null, null);
		}

		public static ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = FSharpUtils._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[]
			{
				typeof(IEnumerable<Tuple<TKey, TValue>>)
			});
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate(object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = from kv in (IEnumerable<KeyValuePair<TKey, TValue>>)args[0]
				select new Tuple<TKey, TValue>(kv.Key, kv.Value);
				return ctorDelegate(new object[]
				{
					enumerable
				});
			};
		}

		// Note: this type is marked as 'beforefieldinit'.
		static FSharpUtils()
		{
			Strings.CreateGetStringDelegate(typeof(FSharpUtils));
			FSharpUtils.Lock = new object();
		}

		private static readonly object Lock;

		private static bool _initialized;

		private static MethodInfo _ofSeq;

		private static Type _mapType;

		public const string FSharpSetTypeName = "FSharpSet`1";

		public const string FSharpListTypeName = "FSharpList`1";

		public const string FSharpMapTypeName = "FSharpMap`2";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
