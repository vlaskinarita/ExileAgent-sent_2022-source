using System;
using System.Xml;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlNode, IXmlDocumentType
	{
		public XmlDocumentTypeWrapper(XmlDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		public override string LocalName
		{
			get
			{
				return XmlDocumentTypeWrapper.getString_0(107320781);
			}
		}

		static XmlDocumentTypeWrapper()
		{
			Strings.CreateGetStringDelegate(typeof(XmlDocumentTypeWrapper));
		}

		private readonly XmlDocumentType _documentType;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
