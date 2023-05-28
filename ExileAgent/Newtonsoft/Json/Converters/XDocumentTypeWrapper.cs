using System;
using System.Xml.Linq;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XDocumentTypeWrapper : XObjectWrapper, IXmlNode, IXmlDocumentType
	{
		public XDocumentTypeWrapper(XDocumentType documentType) : base(documentType)
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
				return XDocumentTypeWrapper.getString_0(107320785);
			}
		}

		static XDocumentTypeWrapper()
		{
			Strings.CreateGetStringDelegate(typeof(XDocumentTypeWrapper));
		}

		private readonly XDocumentType _documentType;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
