using System;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XDeclarationWrapper : XObjectWrapper, IXmlNode, IXmlDeclaration
	{
		internal XDeclaration Declaration { get; }

		public XDeclarationWrapper(XDeclaration declaration) : base(null)
		{
			this.Declaration = declaration;
		}

		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
