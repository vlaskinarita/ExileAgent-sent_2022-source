using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class DynamicValueProvider : IValueProvider
	{
		public DynamicValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, DynamicValueProvider.getString_0(107339232));
			this._memberInfo = memberInfo;
		}

		public void SetValue(object target, object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = DynamicReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(DynamicValueProvider.getString_0(107339183).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		public object GetValue(object target)
		{
			object result;
			try
			{
				if (this._getter == null)
				{
					this._getter = DynamicReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				result = this._getter(target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(DynamicValueProvider.getString_0(107339162).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return result;
		}

		static DynamicValueProvider()
		{
			Strings.CreateGetStringDelegate(typeof(DynamicValueProvider));
		}

		private readonly MemberInfo _memberInfo;

		private Func<object, object> _getter;

		private Action<object, object> _setter;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
