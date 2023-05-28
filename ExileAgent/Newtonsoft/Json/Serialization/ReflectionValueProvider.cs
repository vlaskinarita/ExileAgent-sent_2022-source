using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class ReflectionValueProvider : IValueProvider
	{
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, ReflectionValueProvider.getString_0(107339432));
			this._memberInfo = memberInfo;
		}

		public void SetValue(object target, object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(ReflectionValueProvider.getString_0(107339383).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				PropertyInfo propertyInfo;
				if ((propertyInfo = (this._memberInfo as PropertyInfo)) != null && propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException(ReflectionValueProvider.getString_0(107341805).FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(ReflectionValueProvider.getString_0(107339362).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return memberValue;
		}

		static ReflectionValueProvider()
		{
			Strings.CreateGetStringDelegate(typeof(ReflectionValueProvider));
		}

		private readonly MemberInfo _memberInfo;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
