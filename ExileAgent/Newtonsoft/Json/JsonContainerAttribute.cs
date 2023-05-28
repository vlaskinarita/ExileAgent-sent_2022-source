using System;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public Type ItemConverterType { get; set; }

		public object[] ItemConverterParameters { get; set; }

		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		public object[] NamingStrategyParameters
		{
			get
			{
				return this._namingStrategyParameters;
			}
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		internal NamingStrategy NamingStrategyInstance { get; set; }

		public bool IsReference
		{
			get
			{
				return this._isReference ?? false;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference ?? false;
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? itemReferenceLoopHandling = this._itemReferenceLoopHandling;
				if (itemReferenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				TypeNameHandling? itemTypeNameHandling = this._itemTypeNameHandling;
				if (itemTypeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		protected JsonContainerAttribute()
		{
		}

		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		internal bool? _isReference;

		internal bool? _itemIsReference;

		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		internal TypeNameHandling? _itemTypeNameHandling;

		private Type _namingStrategyType;

		private object[] _namingStrategyParameters;
	}
}
