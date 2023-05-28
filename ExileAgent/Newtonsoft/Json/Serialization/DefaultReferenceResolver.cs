using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal sealed class DefaultReferenceResolver : IReferenceResolver
	{
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase;
			if ((jsonSerializerInternalBase = (context as JsonSerializerInternalBase)) == null)
			{
				JsonSerializerProxy jsonSerializerProxy;
				if ((jsonSerializerProxy = (context as JsonSerializerProxy)) == null)
				{
					throw new JsonException(DefaultReferenceResolver.getString_0(107338888));
				}
				jsonSerializerInternalBase = jsonSerializerProxy.GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		public object ResolveReference(object context, string reference)
		{
			object result;
			this.GetMappings(context).TryGetByFirst(reference, out result);
			return result;
		}

		public string GetReference(object context, object value)
		{
			BidirectionalDictionary<string, object> mappings = this.GetMappings(context);
			string text;
			if (!mappings.TryGetBySecond(value, out text))
			{
				this._referenceCount++;
				text = this._referenceCount.ToString(CultureInfo.InvariantCulture);
				mappings.Set(text, value);
			}
			return text;
		}

		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Set(reference, value);
		}

		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		static DefaultReferenceResolver()
		{
			Strings.CreateGetStringDelegate(typeof(DefaultReferenceResolver));
		}

		private int _referenceCount;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
