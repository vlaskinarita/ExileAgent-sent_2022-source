using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	public class JsonContainerContract : JsonContract
	{
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		public JsonConverter ItemConverter { get; set; }

		public bool? ItemIsReference { get; set; }

		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		internal JsonContainerContract(Type underlyingType) : base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		private JsonContract _itemContract;

		private JsonContract _finalItemContract;
	}
}
