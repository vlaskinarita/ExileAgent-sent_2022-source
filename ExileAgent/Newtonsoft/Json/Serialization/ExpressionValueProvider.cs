using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class ExpressionValueProvider : IValueProvider
	{
		public ExpressionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, ExpressionValueProvider.getString_0(107339244));
			this._memberInfo = memberInfo;
		}

		public void SetValue(object target, object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = ExpressionReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(ExpressionValueProvider.getString_0(107339195).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		public object GetValue(object target)
		{
			object result;
			try
			{
				if (this._getter == null)
				{
					this._getter = ExpressionReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				result = this._getter(target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException(ExpressionValueProvider.getString_0(107339174).FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return result;
		}

		static ExpressionValueProvider()
		{
			Strings.CreateGetStringDelegate(typeof(ExpressionValueProvider));
		}

		private readonly MemberInfo _memberInfo;

		private Func<object, object> _getter;

		private Action<object, object> _setter;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
