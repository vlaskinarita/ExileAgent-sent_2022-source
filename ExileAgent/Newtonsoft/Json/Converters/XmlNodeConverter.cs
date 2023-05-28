using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class XmlNodeConverter : JsonConverter
	{
		public string DeserializeRootElementName { get; set; }

		public bool WriteArrayAttribute { get; set; }

		public bool OmitRootObject { get; set; }

		public bool EncodeSpecialCharacters { get; set; }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			IXmlNode node = this.WrapXml(value);
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			this.PushParentNamespaces(node, manager);
			if (!this.OmitRootObject)
			{
				writer.WriteStartObject();
			}
			this.SerializeNode(writer, node, manager, !this.OmitRootObject);
			if (!this.OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		private IXmlNode WrapXml(object value)
		{
			XObject node;
			if ((node = (value as XObject)) != null)
			{
				return XContainerWrapper.WrapNode(node);
			}
			XmlNode node2;
			if ((node2 = (value as XmlNode)) == null)
			{
				throw new ArgumentException(XmlNodeConverter.getString_0(107320779), XmlNodeConverter.getString_0(107454698));
			}
			return XmlNodeWrapper.WrapNode(node2);
		}

		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list != null)
			{
				list.Reverse();
				foreach (IXmlNode xmlNode2 in list)
				{
					manager.PushScope();
					foreach (IXmlNode xmlNode3 in xmlNode2.Attributes)
					{
						if (xmlNode3.NamespaceUri == XmlNodeConverter.getString_0(107320738) && xmlNode3.LocalName != XmlNodeConverter.getString_0(107320756))
						{
							manager.AddNamespace(xmlNode3.LocalName, xmlNode3.Value);
						}
					}
				}
			}
		}

		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = (node.NamespaceUri == null || (node.LocalName == XmlNodeConverter.getString_0(107320756) && node.NamespaceUri == XmlNodeConverter.getString_0(107320738))) ? null : manager.LookupPrefix(node.NamespaceUri);
			if (!string.IsNullOrEmpty(text))
			{
				return text + XmlNodeConverter.getString_0(107402360) + XmlConvert.DecodeName(node.LocalName);
			}
			return XmlConvert.DecodeName(node.LocalName);
		}

		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				if (node.NamespaceUri == XmlNodeConverter.getString_0(107320665))
				{
					return XmlNodeConverter.getString_0(107456032) + node.LocalName;
				}
				return this.ResolveFullName(node, manager);
			case XmlNodeType.Attribute:
				if (node.NamespaceUri == XmlNodeConverter.getString_0(107320665))
				{
					return XmlNodeConverter.getString_0(107456032) + node.LocalName;
				}
				return XmlNodeConverter.getString_0(107384227) + this.ResolveFullName(node, manager);
			case XmlNodeType.Text:
				return XmlNodeConverter.getString_0(107245211);
			case XmlNodeType.CDATA:
				return XmlNodeConverter.getString_0(107320640);
			case XmlNodeType.ProcessingInstruction:
				return XmlNodeConverter.getString_0(107288238) + this.ResolveFullName(node, manager);
			case XmlNodeType.Comment:
				return XmlNodeConverter.getString_0(107245237);
			case XmlNodeType.DocumentType:
				return XmlNodeConverter.getString_0(107343278) + this.ResolveFullName(node, manager);
			case XmlNodeType.Whitespace:
				return XmlNodeConverter.getString_0(107320586);
			case XmlNodeType.SignificantWhitespace:
				return XmlNodeConverter.getString_0(107320619);
			case XmlNodeType.XmlDeclaration:
				return XmlNodeConverter.getString_0(107248610);
			}
			throw new JsonSerializationException(XmlNodeConverter.getString_0(107320025) + node.NodeType);
		}

		private bool IsArray(IXmlNode node)
		{
			foreach (IXmlNode xmlNode in node.Attributes)
			{
				if (xmlNode.LocalName == XmlNodeConverter.getString_0(107319960) && xmlNode.NamespaceUri == XmlNodeConverter.getString_0(107320665))
				{
					return XmlConvert.ToBoolean(xmlNode.Value);
				}
			}
			return false;
		}

		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			int count = node.ChildNodes.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					string propertyName = this.GetPropertyName(node.ChildNodes[0], manager);
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, propertyName);
					return;
				}
				Dictionary<string, object> dictionary = null;
				string text = null;
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					IXmlNode xmlNode = node.ChildNodes[i];
					string propertyName2 = this.GetPropertyName(xmlNode, manager);
					object obj;
					if (dictionary == null)
					{
						if (text == null)
						{
							text = propertyName2;
						}
						else if (!(propertyName2 == text))
						{
							dictionary = new Dictionary<string, object>();
							if (i > 1)
							{
								List<IXmlNode> list = new List<IXmlNode>(i);
								for (int j = 0; j < i; j++)
								{
									list.Add(node.ChildNodes[j]);
								}
								dictionary.Add(text, list);
							}
							else
							{
								dictionary.Add(text, node.ChildNodes[0]);
							}
							dictionary.Add(propertyName2, xmlNode);
						}
					}
					else if (!dictionary.TryGetValue(propertyName2, out obj))
					{
						dictionary.Add(propertyName2, xmlNode);
					}
					else
					{
						List<IXmlNode> list2;
						if ((list2 = (obj as List<IXmlNode>)) == null)
						{
							list2 = new List<IXmlNode>
							{
								(IXmlNode)obj
							};
							dictionary[propertyName2] = list2;
						}
						list2.Add(xmlNode);
					}
				}
				if (dictionary == null)
				{
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, text);
					return;
				}
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					List<IXmlNode> groupedNodes;
					if ((groupedNodes = (keyValuePair.Value as List<IXmlNode>)) != null)
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, groupedNodes, keyValuePair.Key);
					}
					else
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, (IXmlNode)keyValuePair.Value, keyValuePair.Key);
					}
				}
			}
		}

		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, List<IXmlNode> groupedNodes, string elementNames)
		{
			if (groupedNodes.Count == 1 && !this.IsArray(groupedNodes[0]))
			{
				this.SerializeNode(writer, groupedNodes[0], manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			for (int i = 0; i < groupedNodes.Count; i++)
			{
				this.SerializeNode(writer, groupedNodes[i], manager, false);
			}
			writer.WriteEndArray();
		}

		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, IXmlNode node, string elementNames)
		{
			if (!this.IsArray(node))
			{
				this.SerializeNode(writer, node, manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			this.SerializeNode(writer, node, manager, false);
			writer.WriteEndArray();
		}

		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				if (this.IsArray(node) && XmlNodeConverter.AllSameName(node) && node.ChildNodes.Count > 0)
				{
					this.SerializeGroupedNodes(writer, node, manager, false);
					return;
				}
				manager.PushScope();
				foreach (IXmlNode xmlNode in node.Attributes)
				{
					if (xmlNode.NamespaceUri == XmlNodeConverter.getString_0(107320738))
					{
						string prefix = (xmlNode.LocalName != XmlNodeConverter.getString_0(107320756)) ? XmlConvert.DecodeName(xmlNode.LocalName) : string.Empty;
						string value = xmlNode.Value;
						manager.AddNamespace(prefix, value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				if (!this.ValueAttributes(node.Attributes) && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == XmlNodeType.Text)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
				}
				else if (node.ChildNodes.Count == 0 && node.Attributes.Count == 0)
				{
					if (((IXmlElement)node).IsEmpty)
					{
						writer.WriteNull();
					}
					else
					{
						writer.WriteValue(string.Empty);
					}
				}
				else
				{
					writer.WriteStartObject();
					for (int i = 0; i < node.Attributes.Count; i++)
					{
						this.SerializeNode(writer, node.Attributes[i], manager, true);
					}
					this.SerializeGroupedNodes(writer, node, manager, true);
					writer.WriteEndObject();
				}
				manager.PopScope();
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				if (node.NamespaceUri == XmlNodeConverter.getString_0(107320738) && node.Value == XmlNodeConverter.getString_0(107320665))
				{
					return;
				}
				if (node.NamespaceUri == XmlNodeConverter.getString_0(107320665) && node.LocalName == XmlNodeConverter.getString_0(107319960))
				{
					return;
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				writer.WriteValue(node.Value);
				return;
			case XmlNodeType.Comment:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
					return;
				}
				return;
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				this.SerializeGroupedNodes(writer, node, manager, writePropertyName);
				return;
			case XmlNodeType.DocumentType:
			{
				IXmlDocumentType xmlDocumentType = (IXmlDocumentType)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDocumentType.Name))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319940));
					writer.WriteValue(xmlDocumentType.Name);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.Public))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319899));
					writer.WriteValue(xmlDocumentType.Public);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.System))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319886));
					writer.WriteValue(xmlDocumentType.System);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.InternalSubset))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319905));
					writer.WriteValue(xmlDocumentType.InternalSubset);
				}
				writer.WriteEndObject();
				return;
			}
			case XmlNodeType.XmlDeclaration:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319951));
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319970));
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName(XmlNodeConverter.getString_0(107319925));
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException(XmlNodeConverter.getString_0(107319884) + node.NodeType);
		}

		private static bool AllSameName(IXmlNode node)
		{
			using (List<IXmlNode>.Enumerator enumerator = node.ChildNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.LocalName != node.LocalName)
					{
						return false;
					}
				}
			}
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType != JsonToken.StartObject)
			{
				if (tokenType == JsonToken.Null)
				{
					return null;
				}
				throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319819));
			}
			else
			{
				XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
				IXmlDocument xmlDocument = null;
				IXmlNode xmlNode = null;
				if (typeof(XObject).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(XContainer) && objectType != typeof(XDocument) && objectType != typeof(XElement) && objectType != typeof(XNode) && objectType != typeof(XObject))
					{
						throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107320210));
					}
					xmlDocument = new XDocumentWrapper(new XDocument());
					xmlNode = xmlDocument;
				}
				if (typeof(XmlNode).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(XmlDocument) && objectType != typeof(XmlElement) && objectType != typeof(XmlNode))
					{
						throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107320080));
					}
					xmlDocument = new XmlDocumentWrapper(new XmlDocument
					{
						XmlResolver = null
					});
					xmlNode = xmlDocument;
				}
				if (xmlDocument == null || xmlNode == null)
				{
					throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319491) + objectType);
				}
				if (!string.IsNullOrEmpty(this.DeserializeRootElementName))
				{
					this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, manager);
				}
				else
				{
					reader.ReadAndAssert();
					this.DeserializeNode(reader, xmlDocument, manager, xmlNode);
				}
				if (objectType == typeof(XElement))
				{
					XElement xelement = (XElement)xmlDocument.DocumentElement.WrappedNode;
					xelement.Remove();
					return xelement;
				}
				if (objectType == typeof(XmlElement))
				{
					return xmlDocument.DocumentElement.WrappedNode;
				}
				return xmlDocument.WrappedNode;
			}
		}

		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (!this.EncodeSpecialCharacters)
			{
				if (propertyName == XmlNodeConverter.getString_0(107245211))
				{
					currentNode.AppendChild(document.CreateTextNode(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == XmlNodeConverter.getString_0(107320640))
				{
					currentNode.AppendChild(document.CreateCDataSection(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == XmlNodeConverter.getString_0(107320586))
				{
					currentNode.AppendChild(document.CreateWhitespace(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (propertyName == XmlNodeConverter.getString_0(107320619))
				{
					currentNode.AppendChild(document.CreateSignificantWhitespace(XmlNodeConverter.ConvertTokenToXmlValue(reader)));
					return;
				}
				if (!string.IsNullOrEmpty(propertyName) && propertyName[0] == '?')
				{
					this.CreateInstruction(reader, document, currentNode, propertyName);
					return;
				}
				if (string.Equals(propertyName, XmlNodeConverter.getString_0(107319406), StringComparison.OrdinalIgnoreCase))
				{
					this.CreateDocumentType(reader, document, currentNode);
					return;
				}
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
				return;
			}
			this.ReadElement(reader, document, currentNode, propertyName, manager);
		}

		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319425));
			}
			Dictionary<string, string> attributeNameValues = null;
			string elementPrefix = null;
			if (!this.EncodeSpecialCharacters)
			{
				attributeNameValues = (this.ShouldReadInto(reader) ? this.ReadAttributeElements(reader, manager) : null);
				elementPrefix = MiscellaneousUtils.GetPrefix(propertyName);
				if (propertyName.StartsWith('@'))
				{
					string text = propertyName.Substring(1);
					string prefix = MiscellaneousUtils.GetPrefix(text);
					XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, text, manager, prefix);
					return;
				}
				if (propertyName.StartsWith('$'))
				{
					if (propertyName == XmlNodeConverter.getString_0(107303558))
					{
						propertyName = propertyName.Substring(1);
						elementPrefix = manager.LookupPrefix(XmlNodeConverter.getString_0(107320665));
						this.CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
						return;
					}
					if (propertyName == XmlNodeConverter.getString_0(107305613) || propertyName == XmlNodeConverter.getString_0(107304363) || propertyName == XmlNodeConverter.getString_0(107352374) || propertyName == XmlNodeConverter.getString_0(107352344))
					{
						string attributeName = propertyName.Substring(1);
						string attributePrefix = manager.LookupPrefix(XmlNodeConverter.getString_0(107320665));
						XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, attributeName, manager, attributePrefix);
						return;
					}
				}
			}
			else if (this.ShouldReadInto(reader))
			{
				reader.ReadAndAssert();
			}
			this.CreateElement(reader, document, currentNode, propertyName, manager, elementPrefix, attributeNameValues);
		}

		private void CreateElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string elementName, XmlNamespaceManager manager, string elementPrefix, Dictionary<string, string> attributeNameValues)
		{
			IXmlElement xmlElement = this.CreateElement(elementName, document, elementPrefix, manager);
			currentNode.AppendChild(xmlElement);
			if (attributeNameValues != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in attributeNameValues)
				{
					string text = XmlConvert.EncodeName(keyValuePair.Key);
					string prefix = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
					IXmlNode attributeNode = (!string.IsNullOrEmpty(prefix)) ? document.CreateAttribute(text, manager.LookupNamespace(prefix) ?? string.Empty, keyValuePair.Value) : document.CreateAttribute(text, keyValuePair.Value);
					xmlElement.SetAttributeNode(attributeNode);
				}
			}
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				string text2 = XmlNodeConverter.ConvertTokenToXmlValue(reader);
				if (text2 != null)
				{
					xmlElement.AppendChild(document.CreateTextNode(text2));
					return;
				}
				return;
			}
			case JsonToken.Null:
				return;
			case JsonToken.EndObject:
				manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
				return;
			}
			manager.PushScope();
			this.DeserializeNode(reader, document, manager, xmlElement);
			manager.PopScope();
			manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
		}

		private static void AddAttribute(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, string attributeName, XmlNamespaceManager manager, string attributePrefix)
		{
			if (currentNode.NodeType == XmlNodeType.Document)
			{
				throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319328).FormatWith(CultureInfo.InvariantCulture, propertyName));
			}
			string text = XmlConvert.EncodeName(attributeName);
			string value = XmlNodeConverter.ConvertTokenToXmlValue(reader);
			IXmlNode attributeNode = (!string.IsNullOrEmpty(attributePrefix)) ? document.CreateAttribute(text, manager.LookupNamespace(attributePrefix), value) : document.CreateAttribute(text, value);
			((IXmlElement)currentNode).SetAttributeNode(attributeNode);
		}

		private static string ConvertTokenToXmlValue(JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
			{
				object value;
				if ((value = reader.Value) is BigInteger)
				{
					return ((BigInteger)value).ToString(CultureInfo.InvariantCulture);
				}
				return XmlConvert.ToString(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
			}
			case JsonToken.Float:
			{
				object value;
				if ((value = reader.Value) is decimal)
				{
					decimal value2 = (decimal)value;
					return XmlConvert.ToString(value2);
				}
				if ((value = reader.Value) is float)
				{
					float value3 = (float)value;
					return XmlConvert.ToString(value3);
				}
				return XmlConvert.ToString(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			}
			case JsonToken.String:
			{
				object value4 = reader.Value;
				if (value4 == null)
				{
					return null;
				}
				return value4.ToString();
			}
			case JsonToken.Boolean:
				return XmlConvert.ToString(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Null:
				return null;
			case JsonToken.Date:
			{
				object value;
				if ((value = reader.Value) is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					return XmlConvert.ToString(value5);
				}
				DateTime value6 = Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture);
				return XmlConvert.ToString(value6, DateTimeUtils.ToSerializationMode(value6.Kind));
			}
			case JsonToken.Bytes:
				return Convert.ToBase64String((byte[])reader.Value);
			}
			throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319570).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndArray)
				{
					break;
				}
				this.DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (this.WriteArrayAttribute)
			{
				this.AddJsonArrayAttribute(xmlElement, document);
			}
			if (num == 1 && this.WriteArrayAttribute)
			{
				using (List<IXmlNode>.Enumerator enumerator = xmlElement.ChildNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IXmlElement xmlElement2;
						if ((xmlElement2 = (enumerator.Current as IXmlElement)) != null && xmlElement2.LocalName == propertyName)
						{
							this.AddJsonArrayAttribute(xmlElement2, document);
							break;
						}
					}
				}
			}
		}

		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute(XmlNodeConverter.getString_0(107319017), XmlNodeConverter.getString_0(107320665), XmlNodeConverter.getString_0(107457417)));
			if (element is XElementWrapper && element.GetPrefixOfNamespace(XmlNodeConverter.getString_0(107320665)) == null)
			{
				element.SetAttributeNode(document.CreateAttribute(XmlNodeConverter.getString_0(107318968), XmlNodeConverter.getString_0(107320738), XmlNodeConverter.getString_0(107320665)));
			}
		}

		private bool ShouldReadInto(JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonToken.StartConstructor:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return false;
			default:
				return true;
			}
		}

		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = null;
			bool flag = false;
			while (!flag && reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment && tokenType != JsonToken.EndObject)
					{
						throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107318974) + reader.TokenType);
					}
					flag = true;
				}
				else
				{
					string text = reader.Value.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						char c = text[0];
						if (c != '$')
						{
							if (c == '@')
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								text = text.Substring(1);
								reader.ReadAndAssert();
								string text2 = XmlNodeConverter.ConvertTokenToXmlValue(reader);
								dictionary.Add(text, text2);
								string prefix;
								if (this.IsNamespaceAttribute(text, out prefix))
								{
									manager.AddNamespace(prefix, text2);
								}
							}
							else
							{
								flag = true;
							}
						}
						else if (!(text == XmlNodeConverter.getString_0(107303558)) && !(text == XmlNodeConverter.getString_0(107305613)) && !(text == XmlNodeConverter.getString_0(107304363)) && !(text == XmlNodeConverter.getString_0(107352374)) && !(text == XmlNodeConverter.getString_0(107352344)))
						{
							flag = true;
						}
						else
						{
							string text3 = manager.LookupPrefix(XmlNodeConverter.getString_0(107320665));
							if (text3 == null)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								int? num = null;
								while (manager.LookupNamespace(XmlNodeConverter.getString_0(107297688) + num) != null)
								{
									num = new int?(num.GetValueOrDefault() + 1);
								}
								text3 = XmlNodeConverter.getString_0(107297688) + num;
								dictionary.Add(XmlNodeConverter.getString_0(107318983) + text3, XmlNodeConverter.getString_0(107320665));
								manager.AddNamespace(text3, XmlNodeConverter.getString_0(107320665));
							}
							if (text == XmlNodeConverter.getString_0(107303558))
							{
								flag = true;
							}
							else
							{
								text = text.Substring(1);
								reader.ReadAndAssert();
								if (!JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
								{
									throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107318974) + reader.TokenType);
								}
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								object value = reader.Value;
								string text2 = (value != null) ? value.ToString() : null;
								dictionary.Add(text3 + XmlNodeConverter.getString_0(107402360) + text, text2);
							}
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return dictionary;
		}

		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == XmlNodeConverter.getString_0(107248610))
			{
				string version = null;
				string encoding = null;
				string standalone = null;
				while (reader.Read())
				{
					if (reader.TokenType == JsonToken.EndObject)
					{
						break;
					}
					string a = reader.Value.ToString();
					if (!(a == XmlNodeConverter.getString_0(107319951)))
					{
						if (!(a == XmlNodeConverter.getString_0(107319970)))
						{
							if (!(a == XmlNodeConverter.getString_0(107319925)))
							{
								throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107318941) + reader.Value);
							}
							reader.ReadAndAssert();
							standalone = XmlNodeConverter.ConvertTokenToXmlValue(reader);
						}
						else
						{
							reader.ReadAndAssert();
							encoding = XmlNodeConverter.ConvertTokenToXmlValue(reader);
						}
					}
					else
					{
						reader.ReadAndAssert();
						version = XmlNodeConverter.ConvertTokenToXmlValue(reader);
					}
				}
				IXmlNode newChild = document.CreateXmlDeclaration(version, encoding, standalone);
				currentNode.AppendChild(newChild);
				return;
			}
			IXmlNode newChild2 = document.CreateProcessingInstruction(propertyName.Substring(1), XmlNodeConverter.ConvertTokenToXmlValue(reader));
			currentNode.AppendChild(newChild2);
		}

		private void CreateDocumentType(JsonReader reader, IXmlDocument document, IXmlNode currentNode)
		{
			string name = null;
			string publicId = null;
			string systemId = null;
			string internalSubset = null;
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					break;
				}
				string a = reader.Value.ToString();
				if (!(a == XmlNodeConverter.getString_0(107319940)))
				{
					if (!(a == XmlNodeConverter.getString_0(107319899)))
					{
						if (!(a == XmlNodeConverter.getString_0(107319886)))
						{
							if (!(a == XmlNodeConverter.getString_0(107319905)))
							{
								throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107318941) + reader.Value);
							}
							reader.ReadAndAssert();
							internalSubset = XmlNodeConverter.ConvertTokenToXmlValue(reader);
						}
						else
						{
							reader.ReadAndAssert();
							systemId = XmlNodeConverter.ConvertTokenToXmlValue(reader);
						}
					}
					else
					{
						reader.ReadAndAssert();
						publicId = XmlNodeConverter.ConvertTokenToXmlValue(reader);
					}
				}
				else
				{
					reader.ReadAndAssert();
					name = XmlNodeConverter.ConvertTokenToXmlValue(reader);
				}
			}
			IXmlNode newChild = document.CreateXmlDocumentType(name, publicId, systemId, internalSubset);
			currentNode.AppendChild(newChild);
		}

		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			string text = this.EncodeSpecialCharacters ? XmlConvert.EncodeLocalName(elementName) : XmlConvert.EncodeName(elementName);
			string text2 = string.IsNullOrEmpty(elementPrefix) ? manager.DefaultNamespace : manager.LookupNamespace(elementPrefix);
			if (string.IsNullOrEmpty(text2))
			{
				return document.CreateElement(text);
			}
			return document.CreateElement(text, text2);
		}

		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.StartConstructor:
				{
					string propertyName = reader.Value.ToString();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndConstructor)
						{
							break;
						}
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
					}
					goto IL_173;
				}
				case JsonToken.PropertyName:
					if (currentNode.NodeType != XmlNodeType.Document || document.DocumentElement == null)
					{
						string text = reader.Value.ToString();
						reader.ReadAndAssert();
						if (reader.TokenType == JsonToken.StartArray)
						{
							int num = 0;
							while (reader.Read())
							{
								if (reader.TokenType == JsonToken.EndArray)
								{
									break;
								}
								this.DeserializeValue(reader, document, manager, text, currentNode);
								num++;
							}
							if (num != 1 || !this.WriteArrayAttribute)
							{
								goto IL_173;
							}
							string text2;
							string b;
							MiscellaneousUtils.GetQualifiedNameParts(text, out text2, out b);
							string b2 = string.IsNullOrEmpty(text2) ? manager.DefaultNamespace : manager.LookupNamespace(text2);
							using (List<IXmlNode>.Enumerator enumerator = currentNode.ChildNodes.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IXmlElement xmlElement;
									if ((xmlElement = (enumerator.Current as IXmlElement)) != null && xmlElement.LocalName == b && xmlElement.NamespaceUri == b2)
									{
										this.AddJsonArrayAttribute(xmlElement, document);
										break;
									}
								}
								goto IL_173;
							}
						}
						this.DeserializeValue(reader, document, manager, text, currentNode);
						goto IL_173;
					}
					goto IL_1AE;
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_173;
				}
				break;
				IL_173:
				if (!reader.Read())
				{
					return;
				}
			}
			if (tokenType - JsonToken.EndObject > 1)
			{
				throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107319114) + reader.TokenType);
			}
			return;
			IL_1AE:
			throw JsonSerializationException.Create(reader, XmlNodeConverter.getString_0(107318808));
		}

		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith(XmlNodeConverter.getString_0(107320756), StringComparison.Ordinal))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName[5] == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		private bool ValueAttributes(List<IXmlNode> c)
		{
			foreach (IXmlNode xmlNode in c)
			{
				if (!(xmlNode.NamespaceUri == XmlNodeConverter.getString_0(107320665)) && (!(xmlNode.NamespaceUri == XmlNodeConverter.getString_0(107320738)) || !(xmlNode.Value == XmlNodeConverter.getString_0(107320665))))
				{
					return true;
				}
			}
			return false;
		}

		public override bool CanConvert(Type valueType)
		{
			if (valueType.AssignableToTypeName(XmlNodeConverter.getString_0(107319049), false))
			{
				return this.IsXObject(valueType);
			}
			return valueType.AssignableToTypeName(XmlNodeConverter.getString_0(107318504), false) && this.IsXmlNode(valueType);
		}

		private bool IsXObject(Type valueType)
		{
			return typeof(XObject).IsAssignableFrom(valueType);
		}

		private bool IsXmlNode(Type valueType)
		{
			return typeof(XmlNode).IsAssignableFrom(valueType);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static XmlNodeConverter()
		{
			Strings.CreateGetStringDelegate(typeof(XmlNodeConverter));
			XmlNodeConverter.EmptyChildNodes = new List<IXmlNode>();
		}

		internal static readonly List<IXmlNode> EmptyChildNodes;

		private const string TextName = "#text";

		private const string CommentName = "#comment";

		private const string CDataName = "#cdata-section";

		private const string WhitespaceName = "#whitespace";

		private const string SignificantWhitespaceName = "#significant-whitespace";

		private const string DeclarationName = "?xml";

		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
