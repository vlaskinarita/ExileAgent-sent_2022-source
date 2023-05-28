using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal abstract class ReflectionDelegateFactory
	{
		public Func<T, object> CreateGet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo;
			if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
			{
				if (propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException(ReflectionDelegateFactory.getString_0(107341359).FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				return this.CreateGet<T>(propertyInfo);
			}
			else
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = (memberInfo as FieldInfo)) == null)
				{
					throw new Exception(ReflectionDelegateFactory.getString_0(107341262).FormatWith(CultureInfo.InvariantCulture, memberInfo));
				}
				return this.CreateGet<T>(fieldInfo);
			}
		}

		public Action<T, object> CreateSet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo;
			if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo;
			if ((fieldInfo = (memberInfo as FieldInfo)) == null)
			{
				throw new Exception(ReflectionDelegateFactory.getString_0(107341697).FormatWith(CultureInfo.InvariantCulture, memberInfo));
			}
			return this.CreateSet<T>(fieldInfo);
		}

		public abstract MethodCall<T, object> CreateMethodCall<T>(MethodBase method);

		public abstract ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method);

		public abstract Func<T> CreateDefaultConstructor<T>(Type type);

		public abstract Func<T, object> CreateGet<T>(PropertyInfo propertyInfo);

		public abstract Func<T, object> CreateGet<T>(FieldInfo fieldInfo);

		public abstract Action<T, object> CreateSet<T>(FieldInfo fieldInfo);

		public abstract Action<T, object> CreateSet<T>(PropertyInfo propertyInfo);

		static ReflectionDelegateFactory()
		{
			Strings.CreateGetStringDelegate(typeof(ReflectionDelegateFactory));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
