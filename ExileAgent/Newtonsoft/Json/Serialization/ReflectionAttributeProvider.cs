using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	public sealed class ReflectionAttributeProvider : IAttributeProvider
	{
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, ReflectionAttributeProvider.getString_0(107340755));
			this._attributeProvider = attributeProvider;
		}

		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		static ReflectionAttributeProvider()
		{
			Strings.CreateGetStringDelegate(typeof(ReflectionAttributeProvider));
		}

		private readonly object _attributeProvider;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
