using System;

namespace Newtonsoft.Json.Converters
{
	internal interface IXmlElement : IXmlNode
	{
		void SetAttributeNode(IXmlNode attribute);

		string GetPrefixOfNamespace(string namespaceUri);

		bool IsEmpty { get; }
	}
}
