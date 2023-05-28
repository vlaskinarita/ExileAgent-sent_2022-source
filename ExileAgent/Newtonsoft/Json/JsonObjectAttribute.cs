using System;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				NullValueHandling? itemNullValueHandling = this._itemNullValueHandling;
				if (itemNullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		public Required ItemRequired
		{
			get
			{
				Required? itemRequired = this._itemRequired;
				if (itemRequired == null)
				{
					return Required.Default;
				}
				return itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		public JsonObjectAttribute()
		{
		}

		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		public JsonObjectAttribute(string id) : base(id)
		{
		}

		private MemberSerialization _memberSerialization;

		internal Required? _itemRequired;

		internal NullValueHandling? _itemNullValueHandling;
	}
}
