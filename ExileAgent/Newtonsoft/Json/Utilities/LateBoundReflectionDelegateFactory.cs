using System;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, LateBoundReflectionDelegateFactory.getString_1(107344259));
			ConstructorInfo c;
			if ((c = (method as ConstructorInfo)) != null)
			{
				return (object[] a) => c.Invoke(a);
			}
			return (object[] a) => method.Invoke(null, a);
		}

		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, LateBoundReflectionDelegateFactory.getString_1(107344259));
			ConstructorInfo c;
			if ((c = (method as ConstructorInfo)) != null)
			{
				return (T o, object[] a) => c.Invoke(a);
			}
			return (T o, object[] a) => method.Invoke(o, a);
		}

		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, LateBoundReflectionDelegateFactory.getString_1(107374044));
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, LateBoundReflectionDelegateFactory.getString_1(107344273));
			return (T o) => propertyInfo.GetValue(o, null);
		}

		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, LateBoundReflectionDelegateFactory.getString_1(107344243));
			return (T o) => fieldInfo.GetValue(o);
		}

		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, LateBoundReflectionDelegateFactory.getString_1(107344243));
			return delegate(T o, object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, LateBoundReflectionDelegateFactory.getString_1(107344273));
			return delegate(T o, object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Note: this type is marked as 'beforefieldinit'.
		static LateBoundReflectionDelegateFactory()
		{
			Strings.CreateGetStringDelegate(typeof(LateBoundReflectionDelegateFactory));
			LateBoundReflectionDelegateFactory._instance = new LateBoundReflectionDelegateFactory();
		}

		private static readonly LateBoundReflectionDelegateFactory _instance;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
