using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class DynamicUtils
	{
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}

		internal static class BinderWrapper
		{
			private static void Init()
			{
				if (!DynamicUtils.BinderWrapper._init)
				{
					if (Type.GetType(DynamicUtils.BinderWrapper.getString_0(107316264), false) == null)
					{
						throw new InvalidOperationException(DynamicUtils.BinderWrapper.getString_0(107316610).FormatWith(CultureInfo.InvariantCulture, DynamicUtils.BinderWrapper.getString_0(107316264)));
					}
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[1]);
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[]
					{
						0,
						3
					});
					DynamicUtils.BinderWrapper.CreateMemberCalls();
					DynamicUtils.BinderWrapper._init = true;
				}
			}

			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				Type type = Type.GetType(DynamicUtils.BinderWrapper.getString_0(107316456));
				Type type2 = Type.GetType(DynamicUtils.BinderWrapper.getString_0(107315794));
				Array array = Array.CreateInstance(type, values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					MethodBase method = type.GetMethod(DynamicUtils.BinderWrapper.getString_0(107344622), new Type[]
					{
						type2,
						typeof(string)
					});
					object obj = null;
					object[] array2 = new object[2];
					array2[0] = 0;
					object value = method.Invoke(obj, array2);
					array.SetValue(value, i);
				}
				return array;
			}

			private static void CreateMemberCalls()
			{
				Type type = Type.GetType(DynamicUtils.BinderWrapper.getString_0(107316456), true);
				Type type2 = Type.GetType(DynamicUtils.BinderWrapper.getString_0(107316116), true);
				Type type3 = Type.GetType(DynamicUtils.BinderWrapper.getString_0(107316264), true);
				Type type4 = typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					type
				});
				MethodInfo method = type3.GetMethod(DynamicUtils.BinderWrapper.getString_0(107315934), new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				MethodInfo method2 = type3.GetMethod(DynamicUtils.BinderWrapper.getString_0(107315921), new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			public static CallSiteBinder GetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._getMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray
				});
			}

			public static CallSiteBinder SetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._setMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray
				});
			}

			static BinderWrapper()
			{
				Strings.CreateGetStringDelegate(typeof(DynamicUtils.BinderWrapper));
			}

			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private static object _getCSharpArgumentInfoArray;

			private static object _setCSharpArgumentInfoArray;

			private static MethodCall<object, object> _getMemberCall;

			private static MethodCall<object, object> _setMemberCall;

			private static bool _init;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
