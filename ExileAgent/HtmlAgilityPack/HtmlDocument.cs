using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlDocument : IXPathNavigable
	{
		public static bool DisableBehaviorTagP
		{
			get
			{
				return HtmlDocument._disableBehaviorTagP;
			}
			set
			{
				if (value)
				{
					if (HtmlNode.ElementsFlags.ContainsKey(HtmlDocument.getString_0(107398568)))
					{
						HtmlNode.ElementsFlags.Remove(HtmlDocument.getString_0(107398568));
					}
				}
				else if (!HtmlNode.ElementsFlags.ContainsKey(HtmlDocument.getString_0(107398568)))
				{
					HtmlNode.ElementsFlags.Add(HtmlDocument.getString_0(107398568), HtmlElementFlag.Empty | HtmlElementFlag.Closed);
				}
				HtmlDocument._disableBehaviorTagP = value;
			}
		}

		public static Action<HtmlDocument> DefaultBuilder { get; set; }

		public Action<HtmlDocument> ParseExecuting { get; set; }

		public HtmlDocument()
		{
			if (HtmlDocument.DefaultBuilder != null)
			{
				HtmlDocument.DefaultBuilder(this);
			}
			this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
			this.OptionDefaultStreamEncoding = Encoding.Default;
		}

		public string ParsedText
		{
			get
			{
				return this.Text;
			}
		}

		public static int MaxDepthLevel
		{
			get
			{
				return HtmlDocument._maxDepthLevel;
			}
			set
			{
				HtmlDocument._maxDepthLevel = value;
			}
		}

		public int CheckSum
		{
			get
			{
				if (this._crc32 != null)
				{
					return (int)this._crc32.CheckSum;
				}
				return 0;
			}
		}

		public Encoding DeclaredEncoding
		{
			get
			{
				return this._declaredencoding;
			}
		}

		public HtmlNode DocumentNode
		{
			get
			{
				return this._documentnode;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return this.GetOutEncoding();
			}
		}

		public IEnumerable<HtmlParseError> ParseErrors
		{
			get
			{
				return this._parseerrors;
			}
		}

		public string Remainder
		{
			get
			{
				return this._remainder;
			}
		}

		public int RemainderOffset
		{
			get
			{
				return this._remainderOffset;
			}
		}

		public Encoding StreamEncoding
		{
			get
			{
				return this._streamencoding;
			}
		}

		public static string GetXmlName(string name)
		{
			return HtmlDocument.GetXmlName(name, false, false);
		}

		public void UseAttributeOriginalName(string tagName)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.DocumentNode.SelectNodes(HtmlDocument.getString_0(107229906) + tagName)))
			{
				foreach (HtmlAttribute htmlAttribute in ((IEnumerable<HtmlAttribute>)htmlNode.Attributes))
				{
					htmlAttribute.UseOriginalName = true;
				}
			}
		}

		public static string GetXmlName(string name, bool isAttribute, bool preserveXmlNamespaces)
		{
			string text = string.Empty;
			bool flag = true;
			int i = 0;
			while (i < name.Length)
			{
				if ((name[i] >= 'a' && name[i] <= 'z') || (name[i] >= 'A' && name[i] <= 'Z') || (name[i] >= '0' && name[i] <= '9') || ((isAttribute || preserveXmlNamespaces) && name[i] == ':') || name[i] == '_' || name[i] == '-')
				{
					goto IL_100;
				}
				if (name[i] == '.')
				{
					goto IL_100;
				}
				flag = false;
				byte[] bytes = Encoding.UTF8.GetBytes(new char[]
				{
					name[i]
				});
				for (int j = 0; j < bytes.Length; j++)
				{
					text += bytes[j].ToString(HtmlDocument.getString_0(107245790));
				}
				text += HtmlDocument.getString_0(107247753);
				IL_116:
				i++;
				continue;
				IL_100:
				text += name[i].ToString();
				goto IL_116;
			}
			if (flag)
			{
				return text;
			}
			return HtmlDocument.getString_0(107247753) + text;
		}

		public static string HtmlEncode(string html)
		{
			return HtmlDocument.HtmlEncodeWithCompatibility(html, true);
		}

		internal static string HtmlEncodeWithCompatibility(string html, bool backwardCompatibility = true)
		{
			if (html == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245817));
			}
			return (backwardCompatibility ? new Regex(HtmlDocument.getString_0(107245743), RegexOptions.IgnoreCase) : new Regex(HtmlDocument.getString_0(107245808), RegexOptions.IgnoreCase)).Replace(html, HtmlDocument.getString_0(107245666)).Replace(HtmlDocument.getString_0(107245689), HtmlDocument.getString_0(107245684)).Replace(HtmlDocument.getString_0(107409916), HtmlDocument.getString_0(107245643)).Replace(HtmlDocument.getString_0(107371829), HtmlDocument.getString_0(107245634));
		}

		public static bool IsWhiteSpace(int c)
		{
			if (c != 10 && c != 13 && c != 32)
			{
				if (c != 9)
				{
					return false;
				}
			}
			return true;
		}

		public HtmlAttribute CreateAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107372488));
			}
			HtmlAttribute htmlAttribute = this.CreateAttribute();
			htmlAttribute.Name = name;
			return htmlAttribute;
		}

		public HtmlAttribute CreateAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107372488));
			}
			HtmlAttribute htmlAttribute = this.CreateAttribute(name);
			htmlAttribute.Value = value;
			return htmlAttribute;
		}

		public HtmlCommentNode CreateComment()
		{
			return (HtmlCommentNode)this.CreateNode(HtmlNodeType.Comment);
		}

		public HtmlCommentNode CreateComment(string comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245657));
			}
			HtmlCommentNode htmlCommentNode = this.CreateComment();
			htmlCommentNode.Comment = comment;
			return htmlCommentNode;
		}

		public HtmlNode CreateElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107372488));
			}
			HtmlNode htmlNode = this.CreateNode(HtmlNodeType.Element);
			htmlNode.Name = name;
			return htmlNode;
		}

		public HtmlTextNode CreateTextNode()
		{
			return (HtmlTextNode)this.CreateNode(HtmlNodeType.Text);
		}

		public HtmlTextNode CreateTextNode(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107400859));
			}
			HtmlTextNode htmlTextNode = this.CreateTextNode();
			htmlTextNode.Text = text;
			return htmlTextNode;
		}

		public Encoding DetectEncoding(Stream stream)
		{
			return this.DetectEncoding(stream, false);
		}

		public Encoding DetectEncoding(Stream stream, bool checkHtml)
		{
			this._useHtmlEncodingForStream = checkHtml;
			if (stream == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245644));
			}
			return this.DetectEncoding(new StreamReader(stream));
		}

		public Encoding DetectEncoding(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245603));
			}
			this._onlyDetectEncoding = true;
			if (this.OptionCheckSyntax)
			{
				this.Openednodes = new Dictionary<int, HtmlNode>();
			}
			else
			{
				this.Openednodes = null;
			}
			if (this.OptionUseIdAttribute)
			{
				this.Nodesid = new Dictionary<string, HtmlNode>(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				this.Nodesid = null;
			}
			StreamReader streamReader = reader as StreamReader;
			if (streamReader != null && !this._useHtmlEncodingForStream)
			{
				this.Text = streamReader.ReadToEnd();
				this._streamencoding = streamReader.CurrentEncoding;
				return this._streamencoding;
			}
			this._streamencoding = null;
			this._declaredencoding = null;
			this.Text = reader.ReadToEnd();
			this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
			Encoding encoding;
			try
			{
				this.Parse();
				goto IL_C7;
			}
			catch (EncodingFoundException ex)
			{
				encoding = ex.Encoding;
			}
			return encoding;
			IL_C7:
			return this._streamencoding;
		}

		public Encoding DetectEncodingHtml(string html)
		{
			if (html == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245817));
			}
			Encoding result;
			using (StringReader stringReader = new StringReader(html))
			{
				result = this.DetectEncoding(stringReader);
			}
			return result;
		}

		public HtmlNode GetElementbyId(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107453918));
			}
			if (this.Nodesid == null)
			{
				throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
			}
			if (!this.Nodesid.ContainsKey(id))
			{
				return null;
			}
			return this.Nodesid[id];
		}

		public void Load(Stream stream)
		{
			this.Load(new StreamReader(stream, this.OptionDefaultStreamEncoding));
		}

		public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
		{
			this.Load(new StreamReader(stream, detectEncodingFromByteOrderMarks));
		}

		public void Load(Stream stream, Encoding encoding)
		{
			this.Load(new StreamReader(stream, encoding));
		}

		public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
		{
			this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
		}

		public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
		{
			this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
		}

		public void Load(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245603));
			}
			this._onlyDetectEncoding = false;
			if (this.OptionCheckSyntax)
			{
				this.Openednodes = new Dictionary<int, HtmlNode>();
			}
			else
			{
				this.Openednodes = null;
			}
			if (this.OptionUseIdAttribute)
			{
				this.Nodesid = new Dictionary<string, HtmlNode>(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				this.Nodesid = null;
			}
			StreamReader streamReader = reader as StreamReader;
			if (streamReader != null)
			{
				try
				{
					streamReader.Peek();
				}
				catch (Exception)
				{
				}
				this._streamencoding = streamReader.CurrentEncoding;
			}
			else
			{
				this._streamencoding = null;
			}
			this._declaredencoding = null;
			this.Text = reader.ReadToEnd();
			this._documentnode = this.CreateNode(HtmlNodeType.Document, 0);
			this.Parse();
			if (this.OptionCheckSyntax && this.Openednodes != null)
			{
				foreach (HtmlNode htmlNode in this.Openednodes.Values)
				{
					if (htmlNode._starttag)
					{
						string text;
						if (this.OptionExtractErrorSourceText)
						{
							text = htmlNode.OuterHtml;
							if (text.Length > this.OptionExtractErrorSourceTextMaxLength)
							{
								text = text.Substring(0, this.OptionExtractErrorSourceTextMaxLength);
							}
						}
						else
						{
							text = string.Empty;
						}
						this.AddError(HtmlParseErrorCode.TagNotClosed, htmlNode._line, htmlNode._lineposition, htmlNode._streamposition, text, HtmlDocument.getString_0(107245626) + htmlNode.Name + HtmlDocument.getString_0(107245577));
					}
				}
				this.Openednodes.Clear();
				return;
			}
		}

		public void LoadHtml(string html)
		{
			if (html == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245817));
			}
			using (StringReader stringReader = new StringReader(html))
			{
				this.Load(stringReader);
			}
		}

		public void Save(Stream outStream)
		{
			StreamWriter writer = new StreamWriter(outStream, this.GetOutEncoding());
			this.Save(writer);
		}

		public void Save(Stream outStream, Encoding encoding)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245588));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246055));
			}
			StreamWriter writer = new StreamWriter(outStream, encoding);
			this.Save(writer);
		}

		public void Save(StreamWriter writer)
		{
			this.Save(writer);
		}

		public void Save(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246074));
			}
			this.DocumentNode.WriteTo(writer, 0);
			writer.Flush();
		}

		public void Save(XmlWriter writer)
		{
			this.DocumentNode.WriteTo(writer);
			writer.Flush();
		}

		internal HtmlAttribute CreateAttribute()
		{
			return new HtmlAttribute(this);
		}

		internal HtmlNode CreateNode(HtmlNodeType type)
		{
			return this.CreateNode(type, -1);
		}

		internal HtmlNode CreateNode(HtmlNodeType type, int index)
		{
			if (type == HtmlNodeType.Comment)
			{
				return new HtmlCommentNode(this, index);
			}
			if (type != HtmlNodeType.Text)
			{
				return new HtmlNode(type, this, index);
			}
			return new HtmlTextNode(this, index);
		}

		internal Encoding GetOutEncoding()
		{
			Encoding result;
			if ((result = this._declaredencoding) == null)
			{
				result = (this._streamencoding ?? this.OptionDefaultStreamEncoding);
			}
			return result;
		}

		internal HtmlNode GetXmlDeclaration()
		{
			if (!this._documentnode.HasChildNodes)
			{
				return null;
			}
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this._documentnode._childnodes))
			{
				if (htmlNode.Name == HtmlDocument.getString_0(107246065))
				{
					return htmlNode;
				}
			}
			return null;
		}

		internal void SetIdForNode(HtmlNode node, string id)
		{
			if (!this.OptionUseIdAttribute)
			{
				return;
			}
			if (this.Nodesid == null || id == null)
			{
				return;
			}
			if (node == null)
			{
				this.Nodesid.Remove(id);
				return;
			}
			this.Nodesid[id] = node;
		}

		internal void UpdateLastParentNode()
		{
			do
			{
				if (this._lastparentnode.Closed)
				{
					this._lastparentnode = this._lastparentnode.ParentNode;
				}
				if (this._lastparentnode == null)
				{
					break;
				}
			}
			while (this._lastparentnode.Closed);
			if (this._lastparentnode == null)
			{
				this._lastparentnode = this._documentnode;
			}
		}

		private void AddError(HtmlParseErrorCode code, int line, int linePosition, int streamPosition, string sourceText, string reason)
		{
			HtmlParseError item = new HtmlParseError(code, line, linePosition, streamPosition, sourceText, reason);
			this._parseerrors.Add(item);
		}

		private void CloseCurrentNode()
		{
			if (this._currentnode.Closed)
			{
				return;
			}
			bool flag = false;
			HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name, null);
			if (dictionaryValueOrDefault == null)
			{
				if (HtmlNode.IsClosedElement(this._currentnode.Name))
				{
					this._currentnode.CloseNode(this._currentnode, 0);
					if (this._lastparentnode != null)
					{
						HtmlNode htmlNode = null;
						Stack<HtmlNode> stack = new Stack<HtmlNode>();
						HtmlNode htmlNode2 = this._lastparentnode.LastChild;
						while (htmlNode2 != null)
						{
							if (!(htmlNode2.Name == this._currentnode.Name) || htmlNode2.HasChildNodes)
							{
								stack.Push(htmlNode2);
								htmlNode2 = htmlNode2.PreviousSibling;
							}
							else
							{
								htmlNode = htmlNode2;
								IL_B3:
								if (htmlNode != null)
								{
									while (stack.Count != 0)
									{
										HtmlNode htmlNode3 = stack.Pop();
										this._lastparentnode.RemoveChild(htmlNode3);
										htmlNode.AppendChild(htmlNode3);
									}
									goto IL_2FF;
								}
								this._lastparentnode.AppendChild(this._currentnode);
								goto IL_2FF;
							}
						}
						goto IL_B3;
					}
				}
				else if (HtmlNode.CanOverlapElement(this._currentnode.Name))
				{
					HtmlNode htmlNode4 = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex);
					htmlNode4._outerlength = this._currentnode._outerlength;
					((HtmlTextNode)htmlNode4).Text = ((HtmlTextNode)htmlNode4).Text.ToLowerInvariant();
					if (this._lastparentnode != null)
					{
						this._lastparentnode.AppendChild(htmlNode4);
					}
				}
				else if (HtmlNode.IsEmptyElement(this._currentnode.Name))
				{
					this.AddError(HtmlParseErrorCode.EndTagNotRequired, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, HtmlDocument.getString_0(107245626) + this._currentnode.Name + HtmlDocument.getString_0(107246024));
				}
				else
				{
					this.AddError(HtmlParseErrorCode.TagNotOpened, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, HtmlDocument.getString_0(107246031) + this._currentnode.Name + HtmlDocument.getString_0(107245577));
					flag = true;
				}
			}
			else
			{
				if (this.OptionFixNestedTags && this.FindResetterNodes(dictionaryValueOrDefault, this.GetResetters(this._currentnode.Name)))
				{
					this.AddError(HtmlParseErrorCode.EndTagInvalidHere, this._currentnode._line, this._currentnode._lineposition, this._currentnode._streamposition, this._currentnode.OuterHtml, HtmlDocument.getString_0(107245626) + this._currentnode.Name + HtmlDocument.getString_0(107245982));
					flag = true;
				}
				if (!flag)
				{
					this.Lastnodes[this._currentnode.Name] = dictionaryValueOrDefault._prevwithsamename;
					dictionaryValueOrDefault.CloseNode(this._currentnode, 0);
				}
			}
			IL_2FF:
			if (!flag && this._lastparentnode != null && (!HtmlNode.IsClosedElement(this._currentnode.Name) || this._currentnode._starttag))
			{
				this.UpdateLastParentNode();
			}
		}

		private string CurrentNodeName()
		{
			return this.Text.Substring(this._currentnode._namestartindex, this._currentnode._namelength);
		}

		private void DecrementPosition()
		{
			this._index--;
			if (this._lineposition == 0)
			{
				this._lineposition = this._maxlineposition;
				this._line--;
				return;
			}
			this._lineposition--;
		}

		private HtmlNode FindResetterNode(HtmlNode node, string name)
		{
			HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, name, null);
			if (dictionaryValueOrDefault == null)
			{
				return null;
			}
			if (dictionaryValueOrDefault.Closed)
			{
				return null;
			}
			if (dictionaryValueOrDefault._streamposition < node._streamposition)
			{
				return null;
			}
			return dictionaryValueOrDefault;
		}

		private bool FindResetterNodes(HtmlNode node, string[] names)
		{
			if (names == null)
			{
				return false;
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (this.FindResetterNode(node, names[i]) != null)
				{
					return true;
				}
			}
			return false;
		}

		private void FixNestedTag(string name, string[] resetters)
		{
			if (resetters == null)
			{
				return;
			}
			HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name, null);
			if (dictionaryValueOrDefault == null || this.Lastnodes[name].Closed)
			{
				return;
			}
			if (this.FindResetterNodes(dictionaryValueOrDefault, resetters))
			{
				return;
			}
			HtmlNode htmlNode = new HtmlNode(dictionaryValueOrDefault.NodeType, this, -1);
			htmlNode._endnode = htmlNode;
			dictionaryValueOrDefault.CloseNode(htmlNode, 0);
		}

		private void FixNestedTags()
		{
			if (!this._currentnode._starttag)
			{
				return;
			}
			string name = this.CurrentNodeName();
			this.FixNestedTag(name, this.GetResetters(name));
		}

		private string[] GetResetters(string name)
		{
			string[] result;
			if (!HtmlDocument.HtmlResetters.TryGetValue(name, out result))
			{
				return null;
			}
			return result;
		}

		private void IncrementPosition()
		{
			if (this._crc32 != null)
			{
				this._crc32.AddToCRC32(this._c);
			}
			this._index++;
			this._maxlineposition = this._lineposition;
			if (this._c == 10)
			{
				this._lineposition = 0;
				this._line++;
				return;
			}
			this._lineposition++;
		}

		private bool IsValidTag()
		{
			return this._c == 60 && this._index < this.Text.Length && (char.IsLetter(this.Text[this._index]) || this.Text[this._index] == '/' || this.Text[this._index] == '?' || this.Text[this._index] == '!' || this.Text[this._index] == '%');
		}

		private bool NewCheck()
		{
			if (this._c != 60 || !this.IsValidTag())
			{
				return false;
			}
			if (this._index < this.Text.Length && this.Text[this._index] == '%')
			{
				if (this.DisableServerSideCode)
				{
					return false;
				}
				HtmlDocument.ParseState state = this._state;
				if (state != HtmlDocument.ParseState.WhichTag)
				{
					if (state != HtmlDocument.ParseState.BetweenAttributes)
					{
						if (state == HtmlDocument.ParseState.AttributeAfterEquals)
						{
							this.PushAttributeValueStart(this._index - 1);
						}
					}
					else
					{
						this.PushAttributeNameStart(this._index - 1, this._lineposition - 1);
					}
				}
				else
				{
					this.PushNodeNameStart(true, this._index - 1);
					this._state = HtmlDocument.ParseState.Tag;
				}
				this._oldstate = this._state;
				this._state = HtmlDocument.ParseState.ServerSideCode;
				return true;
			}
			else
			{
				if (!this.PushNodeEnd(this._index - 1, true))
				{
					this._index = this.Text.Length;
					return true;
				}
				this._state = HtmlDocument.ParseState.WhichTag;
				if (this._index - 1 <= this.Text.Length - 2 && (this.Text[this._index] == '!' || this.Text[this._index] == '?'))
				{
					this.PushNodeStart(HtmlNodeType.Comment, this._index - 1, this._lineposition - 1);
					this.PushNodeNameStart(true, this._index);
					this.PushNodeNameEnd(this._index + 1);
					this._state = HtmlDocument.ParseState.Comment;
					if (this._index < this.Text.Length - 2)
					{
						if (this.Text[this._index + 1] == '-' && this.Text[this._index + 2] == '-')
						{
							this._fullcomment = true;
						}
						else
						{
							this._fullcomment = false;
						}
					}
					return true;
				}
				this.PushNodeStart(HtmlNodeType.Element, this._index - 1, this._lineposition - 1);
				return true;
			}
		}

		private void Parse()
		{
			if (this.ParseExecuting != null)
			{
				this.ParseExecuting(this);
			}
			int num = 0;
			if (this.OptionComputeChecksum)
			{
				this._crc32 = new Crc32();
			}
			this.Lastnodes = new Dictionary<string, HtmlNode>();
			this._c = 0;
			this._fullcomment = false;
			this._parseerrors = new List<HtmlParseError>();
			this._line = 1;
			this._lineposition = 0;
			this._maxlineposition = 0;
			this._state = HtmlDocument.ParseState.Text;
			this._oldstate = this._state;
			this._documentnode._innerlength = this.Text.Length;
			this._documentnode._outerlength = this.Text.Length;
			this._remainderOffset = this.Text.Length;
			this._lastparentnode = this._documentnode;
			this._currentnode = this.CreateNode(HtmlNodeType.Text, 0);
			this._currentattribute = null;
			this._index = 0;
			this.PushNodeStart(HtmlNodeType.Text, 0, this._lineposition);
			while (this._index < this.Text.Length)
			{
				this._c = (int)this.Text[this._index];
				this.IncrementPosition();
				switch (this._state)
				{
				case HtmlDocument.ParseState.Text:
					if (this.NewCheck())
					{
					}
					break;
				case HtmlDocument.ParseState.WhichTag:
					if (!this.NewCheck())
					{
						if (this._c == 47)
						{
							this.PushNodeNameStart(false, this._index);
						}
						else
						{
							this.PushNodeNameStart(true, this._index - 1);
							this.DecrementPosition();
						}
						this._state = HtmlDocument.ParseState.Tag;
					}
					break;
				case HtmlDocument.ParseState.Tag:
					if (!this.NewCheck())
					{
						if (HtmlDocument.IsWhiteSpace(this._c))
						{
							this.CloseParentImplicitExplicitNode();
							this.PushNodeNameEnd(this._index - 1);
							if (this._state == HtmlDocument.ParseState.Tag)
							{
								this._state = HtmlDocument.ParseState.BetweenAttributes;
							}
						}
						else if (this._c == 47)
						{
							this.CloseParentImplicitExplicitNode();
							this.PushNodeNameEnd(this._index - 1);
							if (this._state == HtmlDocument.ParseState.Tag)
							{
								this._state = HtmlDocument.ParseState.EmptyTag;
							}
						}
						else if (this._c == 62)
						{
							this.CloseParentImplicitExplicitNode();
							this.PushNodeNameEnd(this._index - 1);
							if (this._state == HtmlDocument.ParseState.Tag)
							{
								if (!this.PushNodeEnd(this._index, false))
								{
									this._index = this.Text.Length;
								}
								else if (this._state == HtmlDocument.ParseState.Tag)
								{
									this._state = HtmlDocument.ParseState.Text;
									this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
								}
							}
						}
					}
					break;
				case HtmlDocument.ParseState.BetweenAttributes:
					if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
					{
						if (this._c != 47)
						{
							if (this._c != 63)
							{
								if (this._c != 62)
								{
									this.PushAttributeNameStart(this._index - 1, this._lineposition - 1);
									this._state = HtmlDocument.ParseState.AttributeName;
									break;
								}
								if (!this.PushNodeEnd(this._index, false))
								{
									this._index = this.Text.Length;
									break;
								}
								if (this._state == HtmlDocument.ParseState.BetweenAttributes)
								{
									this._state = HtmlDocument.ParseState.Text;
									this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
									break;
								}
								break;
							}
						}
						this._state = HtmlDocument.ParseState.EmptyTag;
					}
					break;
				case HtmlDocument.ParseState.EmptyTag:
					if (!this.NewCheck())
					{
						if (this._c == 62)
						{
							if (!this.PushNodeEnd(this._index, true))
							{
								this._index = this.Text.Length;
							}
							else if (this._state == HtmlDocument.ParseState.EmptyTag)
							{
								this._state = HtmlDocument.ParseState.Text;
								this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
							}
						}
						else if (!HtmlDocument.IsWhiteSpace(this._c))
						{
							this.DecrementPosition();
							this._state = HtmlDocument.ParseState.BetweenAttributes;
						}
						else
						{
							this._state = HtmlDocument.ParseState.BetweenAttributes;
						}
					}
					break;
				case HtmlDocument.ParseState.AttributeName:
					if (!this.NewCheck())
					{
						if (HtmlDocument.IsWhiteSpace(this._c))
						{
							this.PushAttributeNameEnd(this._index - 1);
							this._state = HtmlDocument.ParseState.AttributeBeforeEquals;
						}
						else if (this._c == 61)
						{
							this.PushAttributeNameEnd(this._index - 1);
							this._state = HtmlDocument.ParseState.AttributeAfterEquals;
						}
						else if (this._c == 62)
						{
							this.PushAttributeNameEnd(this._index - 1);
							if (!this.PushNodeEnd(this._index, false))
							{
								this._index = this.Text.Length;
							}
							else if (this._state == HtmlDocument.ParseState.AttributeName)
							{
								this._state = HtmlDocument.ParseState.Text;
								this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
							}
						}
					}
					break;
				case HtmlDocument.ParseState.AttributeBeforeEquals:
					if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
					{
						if (this._c == 62)
						{
							if (!this.PushNodeEnd(this._index, false))
							{
								this._index = this.Text.Length;
							}
							else if (this._state == HtmlDocument.ParseState.AttributeBeforeEquals)
							{
								this._state = HtmlDocument.ParseState.Text;
								this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
							}
						}
						else if (this._c == 61)
						{
							this._state = HtmlDocument.ParseState.AttributeAfterEquals;
						}
						else
						{
							this._state = HtmlDocument.ParseState.BetweenAttributes;
							this.DecrementPosition();
						}
					}
					break;
				case HtmlDocument.ParseState.AttributeAfterEquals:
					if (!this.NewCheck() && !HtmlDocument.IsWhiteSpace(this._c))
					{
						if (this._c != 39)
						{
							if (this._c != 34)
							{
								if (this._c != 62)
								{
									this.PushAttributeValueStart(this._index - 1);
									this._state = HtmlDocument.ParseState.AttributeValue;
									break;
								}
								if (!this.PushNodeEnd(this._index, false))
								{
									this._index = this.Text.Length;
									break;
								}
								if (this._state == HtmlDocument.ParseState.AttributeAfterEquals)
								{
									this._state = HtmlDocument.ParseState.Text;
									this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
									break;
								}
								break;
							}
						}
						this._state = HtmlDocument.ParseState.QuotedAttributeValue;
						this.PushAttributeValueStart(this._index, this._c);
						num = this._c;
					}
					break;
				case HtmlDocument.ParseState.AttributeValue:
					if (!this.NewCheck())
					{
						if (HtmlDocument.IsWhiteSpace(this._c))
						{
							this.PushAttributeValueEnd(this._index - 1);
							this._state = HtmlDocument.ParseState.BetweenAttributes;
						}
						else if (this._c == 62)
						{
							this.PushAttributeValueEnd(this._index - 1);
							if (!this.PushNodeEnd(this._index, false))
							{
								this._index = this.Text.Length;
							}
							else if (this._state == HtmlDocument.ParseState.AttributeValue)
							{
								this._state = HtmlDocument.ParseState.Text;
								this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
							}
						}
					}
					break;
				case HtmlDocument.ParseState.Comment:
					if (this._c == 62 && (!this._fullcomment || (this.Text[this._index - 2] == '-' && this.Text[this._index - 3] == '-') || (this.Text[this._index - 2] == '!' && this.Text[this._index - 3] == '-' && this.Text[this._index - 4] == '-')))
					{
						if (!this.PushNodeEnd(this._index, false))
						{
							this._index = this.Text.Length;
						}
						else
						{
							this._state = HtmlDocument.ParseState.Text;
							this.PushNodeStart(HtmlNodeType.Text, this._index, this._lineposition);
						}
					}
					break;
				case HtmlDocument.ParseState.QuotedAttributeValue:
					if (this._c == num)
					{
						this.PushAttributeValueEnd(this._index - 1);
						this._state = HtmlDocument.ParseState.BetweenAttributes;
					}
					else if (this._c == 60 && this._index < this.Text.Length && this.Text[this._index] == '%')
					{
						this._oldstate = this._state;
						this._state = HtmlDocument.ParseState.ServerSideCode;
					}
					break;
				case HtmlDocument.ParseState.ServerSideCode:
					if (this._c == 37)
					{
						if (this._index < this.Text.Length && this.Text[this._index] == '>')
						{
							HtmlDocument.ParseState oldstate = this._oldstate;
							if (oldstate != HtmlDocument.ParseState.BetweenAttributes)
							{
								if (oldstate == HtmlDocument.ParseState.AttributeAfterEquals)
								{
									this._state = HtmlDocument.ParseState.AttributeValue;
								}
								else
								{
									this._state = this._oldstate;
								}
							}
							else
							{
								this.PushAttributeNameEnd(this._index + 1);
								this._state = HtmlDocument.ParseState.BetweenAttributes;
							}
							this.IncrementPosition();
						}
					}
					else if (this._oldstate == HtmlDocument.ParseState.QuotedAttributeValue && this._c == num)
					{
						this._state = this._oldstate;
						this.DecrementPosition();
					}
					break;
				case HtmlDocument.ParseState.PcData:
					if (this._currentnode._namelength + 3 <= this.Text.Length - (this._index - 1) && string.Compare(this.Text.Substring(this._index - 1, this._currentnode._namelength + 2), HtmlDocument.getString_0(107245961) + this._currentnode.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						int num2 = (int)this.Text[this._index - 1 + 2 + this._currentnode.Name.Length];
						if (num2 == 62 || HtmlDocument.IsWhiteSpace(num2))
						{
							HtmlNode htmlNode = this.CreateNode(HtmlNodeType.Text, this._currentnode._outerstartindex + this._currentnode._outerlength);
							htmlNode._outerlength = this._index - 1 - htmlNode._outerstartindex;
							htmlNode._streamposition = htmlNode._outerstartindex;
							htmlNode._line = this._currentnode.Line;
							htmlNode._lineposition = this._currentnode.LinePosition + this._currentnode._namelength + 2;
							this._currentnode.AppendChild(htmlNode);
							if (this._currentnode.Name.ToLowerInvariant().Equals(HtmlDocument.getString_0(107245956)) || this._currentnode.Name.ToLowerInvariant().Equals(HtmlDocument.getString_0(107245979)))
							{
								this._currentnode._isHideInnerText = true;
							}
							this.PushNodeStart(HtmlNodeType.Element, this._index - 1, this._lineposition - 1);
							this.PushNodeNameStart(false, this._index - 1 + 2);
							this._state = HtmlDocument.ParseState.Tag;
							this.IncrementPosition();
						}
					}
					break;
				}
			}
			if (this._currentnode._namestartindex > 0)
			{
				this.PushNodeNameEnd(this._index);
			}
			this.PushNodeEnd(this._index, false);
			this.Lastnodes.Clear();
		}

		private void PushAttributeNameEnd(int index)
		{
			this._currentattribute._namelength = index - this._currentattribute._namestartindex;
			if (this._currentattribute.Name != null && !HtmlDocument.BlockAttributes.Contains(this._currentattribute.Name))
			{
				this._currentnode.Attributes.Append(this._currentattribute);
			}
		}

		private void PushAttributeNameStart(int index, int lineposition)
		{
			this._currentattribute = this.CreateAttribute();
			this._currentattribute._namestartindex = index;
			this._currentattribute.Line = this._line;
			this._currentattribute._lineposition = lineposition;
			this._currentattribute._streamposition = index;
		}

		private void PushAttributeValueEnd(int index)
		{
			this._currentattribute._valuelength = index - this._currentattribute._valuestartindex;
		}

		private void PushAttributeValueStart(int index)
		{
			this.PushAttributeValueStart(index, 0);
		}

		private void CloseParentImplicitExplicitNode()
		{
			bool flag = true;
			while (flag && !this._lastparentnode.Closed)
			{
				flag = false;
				bool flag2 = false;
				if (this.IsParentImplicitEnd())
				{
					if (this.OptionOutputAsXml)
					{
						flag2 = true;
					}
					else
					{
						this.CloseParentImplicitEnd();
						flag = true;
					}
				}
				if (flag2 || this.IsParentExplicitEnd())
				{
					this.CloseParentExplicitEnd();
					flag = true;
				}
			}
		}

		private bool IsParentImplicitEnd()
		{
			if (!this._currentnode._starttag)
			{
				return false;
			}
			bool result = false;
			string name = this._lastparentnode.Name;
			string a = this.Text.Substring(this._currentnode._namestartindex, this._index - this._currentnode._namestartindex - 1).ToLowerInvariant();
			if (name != null)
			{
				if (!(name == HtmlDocument.getString_0(107245970)))
				{
					if (!(name == HtmlDocument.getString_0(107245965)))
					{
						if (!(name == HtmlDocument.getString_0(107245928)))
						{
							if (!(name == HtmlDocument.getString_0(107245923)))
							{
								if (!(name == HtmlDocument.getString_0(107398568)))
								{
									if (name == HtmlDocument.getString_0(107245918))
									{
										result = (a == HtmlDocument.getString_0(107245918));
									}
								}
								else if (HtmlDocument.DisableBehaviorTagP)
								{
									result = (a == HtmlDocument.getString_0(107245941) || a == HtmlDocument.getString_0(107245896) || a == HtmlDocument.getString_0(107245915) || a == HtmlDocument.getString_0(107245906) || a == HtmlDocument.getString_0(107245857) || a == HtmlDocument.getString_0(107452606) || a == HtmlDocument.getString_0(107245852) || a == HtmlDocument.getString_0(107245879) || a == HtmlDocument.getString_0(107245834) || a == HtmlDocument.getString_0(107245825) || a == HtmlDocument.getString_0(107245848) || a == HtmlDocument.getString_0(107245843) || a == HtmlDocument.getString_0(107245838) || a == HtmlDocument.getString_0(107245289) || a == HtmlDocument.getString_0(107245284) || a == HtmlDocument.getString_0(107245279) || a == HtmlDocument.getString_0(107245306) || a == HtmlDocument.getString_0(107245297) || a == HtmlDocument.getString_0(107353276) || a == HtmlDocument.getString_0(107245292) || a == HtmlDocument.getString_0(107245255) || a == HtmlDocument.getString_0(107398568) || a == HtmlDocument.getString_0(107245250) || a == HtmlDocument.getString_0(107382284) || a == HtmlDocument.getString_0(107245245) || a == HtmlDocument.getString_0(107245268));
								}
								else
								{
									result = (a == HtmlDocument.getString_0(107398568));
								}
							}
							else
							{
								result = (a == HtmlDocument.getString_0(107245923));
							}
						}
						else
						{
							result = (a == HtmlDocument.getString_0(107245928) || a == HtmlDocument.getString_0(107245965));
						}
					}
					else
					{
						result = (a == HtmlDocument.getString_0(107245928) || a == HtmlDocument.getString_0(107245965));
					}
				}
				else
				{
					result = (a == HtmlDocument.getString_0(107245970));
				}
			}
			return result;
		}

		private bool IsParentExplicitEnd()
		{
			if (!this._currentnode._starttag)
			{
				return false;
			}
			bool result = false;
			string name = this._lastparentnode.Name;
			string a = this.Text.Substring(this._currentnode._namestartindex, this._index - this._currentnode._namestartindex - 1).ToLowerInvariant();
			if (name != null)
			{
				uint num = <PrivateImplementationDetails>1.ComputeStringHash(name);
				if (num <= 2352688966U)
				{
					if (num <= 1095059089U)
					{
						if (num != 1027948613U)
						{
							if (num == 1095059089U)
							{
								if (name == HtmlDocument.getString_0(107245212))
								{
									result = (a == HtmlDocument.getString_0(107245217) || a == HtmlDocument.getString_0(107245212) || a == HtmlDocument.getString_0(107245222));
								}
							}
						}
						else if (name == HtmlDocument.getString_0(107245217))
						{
							result = (a == HtmlDocument.getString_0(107245217) || a == HtmlDocument.getString_0(107245212) || a == HtmlDocument.getString_0(107245222));
						}
					}
					else if (num != 1195724803U)
					{
						if (num != 1251777503U)
						{
							if (num == 2352688966U)
							{
								if (name == HtmlDocument.getString_0(107245284))
								{
									result = (a == HtmlDocument.getString_0(107245848) || a == HtmlDocument.getString_0(107245843) || a == HtmlDocument.getString_0(107245838) || a == HtmlDocument.getString_0(107245289));
								}
							}
						}
						else if (name == HtmlDocument.getString_0(107245245))
						{
							result = (a == HtmlDocument.getString_0(107245245));
						}
					}
					else if (name == HtmlDocument.getString_0(107245222))
					{
						result = (a == HtmlDocument.getString_0(107245222));
					}
				}
				else if (num <= 2403021823U)
				{
					if (num != 2369466585U)
					{
						if (num != 2386244204U)
						{
							if (num == 2403021823U)
							{
								if (name == HtmlDocument.getString_0(107245843))
								{
									result = (a == HtmlDocument.getString_0(107245848) || a == HtmlDocument.getString_0(107245838) || a == HtmlDocument.getString_0(107245289) || a == HtmlDocument.getString_0(107245284));
								}
							}
						}
						else if (name == HtmlDocument.getString_0(107245838))
						{
							result = (a == HtmlDocument.getString_0(107245848) || a == HtmlDocument.getString_0(107245843) || a == HtmlDocument.getString_0(107245289) || a == HtmlDocument.getString_0(107245284));
						}
					}
					else if (name == HtmlDocument.getString_0(107245289))
					{
						result = (a == HtmlDocument.getString_0(107245848) || a == HtmlDocument.getString_0(107245843) || a == HtmlDocument.getString_0(107245838) || a == HtmlDocument.getString_0(107245284));
					}
				}
				else if (num != 2419799442U)
				{
					if (num != 2556802313U)
					{
						if (num == 4111221743U)
						{
							if (name == HtmlDocument.getString_0(107398568))
							{
								result = (a == HtmlDocument.getString_0(107452606));
							}
						}
					}
					else if (name == HtmlDocument.getString_0(107245263))
					{
						result = (a == HtmlDocument.getString_0(107245263));
					}
				}
				else if (name == HtmlDocument.getString_0(107245848))
				{
					result = (a == HtmlDocument.getString_0(107245843) || a == HtmlDocument.getString_0(107245838) || a == HtmlDocument.getString_0(107245289) || a == HtmlDocument.getString_0(107245284));
				}
			}
			return result;
		}

		private void CloseParentImplicitEnd()
		{
			HtmlNode htmlNode = new HtmlNode(this._lastparentnode.NodeType, this, -1);
			htmlNode._endnode = htmlNode;
			htmlNode._isImplicitEnd = true;
			this._lastparentnode._isImplicitEnd = true;
			this._lastparentnode.CloseNode(htmlNode, 0);
		}

		private void CloseParentExplicitEnd()
		{
			HtmlNode htmlNode = new HtmlNode(this._lastparentnode.NodeType, this, -1);
			htmlNode._endnode = htmlNode;
			this._lastparentnode.CloseNode(htmlNode, 0);
		}

		private void PushAttributeValueStart(int index, int quote)
		{
			this._currentattribute._valuestartindex = index;
			if (quote == 39)
			{
				this._currentattribute.QuoteType = AttributeValueQuote.SingleQuote;
			}
		}

		private bool PushNodeEnd(int index, bool close)
		{
			this._currentnode._outerlength = index - this._currentnode._outerstartindex;
			if (this._currentnode._nodetype != HtmlNodeType.Text)
			{
				if (this._currentnode._nodetype != HtmlNodeType.Comment)
				{
					if (!this._currentnode._starttag || this._lastparentnode == this._currentnode)
					{
						goto IL_17E;
					}
					if (this._lastparentnode != null)
					{
						this._lastparentnode.AppendChild(this._currentnode);
					}
					this.ReadDocumentEncoding(this._currentnode);
					HtmlNode dictionaryValueOrDefault = Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this.Lastnodes, this._currentnode.Name, null);
					this._currentnode._prevwithsamename = dictionaryValueOrDefault;
					this.Lastnodes[this._currentnode.Name] = this._currentnode;
					if (this._currentnode.NodeType == HtmlNodeType.Document || this._currentnode.NodeType == HtmlNodeType.Element)
					{
						this._lastparentnode = this._currentnode;
					}
					if (HtmlNode.IsCDataElement(this.CurrentNodeName()))
					{
						this._state = HtmlDocument.ParseState.PcData;
						return true;
					}
					if (HtmlNode.IsClosedElement(this._currentnode.Name) || HtmlNode.IsEmptyElement(this._currentnode.Name))
					{
						close = true;
						goto IL_17E;
					}
					goto IL_17E;
				}
			}
			if (this._currentnode._outerlength > 0)
			{
				this._currentnode._innerlength = this._currentnode._outerlength;
				this._currentnode._innerstartindex = this._currentnode._outerstartindex;
				if (this._lastparentnode != null)
				{
					this._lastparentnode.AppendChild(this._currentnode);
				}
			}
			IL_17E:
			if (close || !this._currentnode._starttag)
			{
				if (this.OptionStopperNodeName != null && this._remainder == null && string.Compare(this._currentnode.Name, this.OptionStopperNodeName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this._remainderOffset = index;
					this._remainder = this.Text.Substring(this._remainderOffset);
					this.CloseCurrentNode();
					return false;
				}
				this.CloseCurrentNode();
			}
			return true;
		}

		private void PushNodeNameEnd(int index)
		{
			this._currentnode._namelength = index - this._currentnode._namestartindex;
			if (this.OptionFixNestedTags)
			{
				this.FixNestedTags();
			}
		}

		private void PushNodeNameStart(bool starttag, int index)
		{
			this._currentnode._starttag = starttag;
			this._currentnode._namestartindex = index;
		}

		private void PushNodeStart(HtmlNodeType type, int index, int lineposition)
		{
			this._currentnode = this.CreateNode(type, index);
			this._currentnode._line = this._line;
			this._currentnode._lineposition = lineposition;
			this._currentnode._streamposition = index;
		}

		private void ReadDocumentEncoding(HtmlNode node)
		{
			if (!this.OptionReadEncoding)
			{
				return;
			}
			if (node._namelength != 4)
			{
				return;
			}
			if (node.Name != HtmlDocument.getString_0(107245239))
			{
				return;
			}
			string text = null;
			HtmlAttribute htmlAttribute = node.Attributes[HtmlDocument.getString_0(107245230)];
			if (htmlAttribute != null)
			{
				if (string.Compare(htmlAttribute.Value, HtmlDocument.getString_0(107245181), StringComparison.OrdinalIgnoreCase) != 0)
				{
					return;
				}
				HtmlAttribute htmlAttribute2 = node.Attributes[HtmlDocument.getString_0(107466294)];
				if (htmlAttribute2 != null)
				{
					text = NameValuePairList.GetNameValuePairsValue(htmlAttribute2.Value, HtmlDocument.getString_0(107245196));
				}
			}
			else
			{
				htmlAttribute = node.Attributes[HtmlDocument.getString_0(107245196)];
				if (htmlAttribute != null)
				{
					text = htmlAttribute.Value;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (string.Equals(text, HtmlDocument.getString_0(107245151), StringComparison.OrdinalIgnoreCase))
				{
					text = HtmlDocument.getString_0(107245174);
				}
				try
				{
					this._declaredencoding = Encoding.GetEncoding(text);
				}
				catch (ArgumentException)
				{
					this._declaredencoding = null;
				}
				if (this._onlyDetectEncoding)
				{
					throw new EncodingFoundException(this._declaredencoding);
				}
				if (this._streamencoding != null && this._declaredencoding != null && this._declaredencoding.CodePage != this._streamencoding.CodePage)
				{
					this.AddError(HtmlParseErrorCode.CharsetMismatch, this._line, this._lineposition, this._index, node.OuterHtml, HtmlDocument.getString_0(107245165) + this._streamencoding.WebName + HtmlDocument.getString_0(107245108) + this._declaredencoding.WebName);
				}
			}
		}

		public void DetectEncodingAndLoad(string path)
		{
			this.DetectEncodingAndLoad(path, true);
		}

		public void DetectEncodingAndLoad(string path, bool detectEncoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			Encoding encoding;
			if (detectEncoding)
			{
				encoding = this.DetectEncoding(path);
			}
			else
			{
				encoding = null;
			}
			if (encoding == null)
			{
				this.Load(path);
				return;
			}
			this.Load(path, encoding);
		}

		public Encoding DetectEncoding(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			Encoding result;
			using (StreamReader streamReader = new StreamReader(path, this.OptionDefaultStreamEncoding))
			{
				result = this.DetectEncoding(streamReader);
			}
			return result;
		}

		public void Load(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			using (StreamReader streamReader = new StreamReader(path, this.OptionDefaultStreamEncoding))
			{
				this.Load(streamReader);
			}
		}

		public void Load(string path, bool detectEncodingFromByteOrderMarks)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			using (StreamReader streamReader = new StreamReader(path, detectEncodingFromByteOrderMarks))
			{
				this.Load(streamReader);
			}
		}

		public void Load(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246055));
			}
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				this.Load(streamReader);
			}
		}

		public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246055));
			}
			using (StreamReader streamReader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks))
			{
				this.Load(streamReader);
			}
		}

		public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245075));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246055));
			}
			using (StreamReader streamReader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize))
			{
				this.Load(streamReader);
			}
		}

		public void Save(string filename)
		{
			using (StreamWriter streamWriter = new StreamWriter(filename, false, this.GetOutEncoding()))
			{
				this.Save(streamWriter);
			}
		}

		public void Save(string filename, Encoding encoding)
		{
			if (filename == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107245546));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(HtmlDocument.getString_0(107246055));
			}
			using (StreamWriter streamWriter = new StreamWriter(filename, false, encoding))
			{
				this.Save(streamWriter);
			}
		}

		public XPathNavigator CreateNavigator()
		{
			return new HtmlNodeNavigator(this, this._documentnode);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static HtmlDocument()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlDocument));
			HtmlDocument._disableBehaviorTagP = true;
			HtmlDocument._maxDepthLevel = int.MaxValue;
			HtmlDocument.HtmlExceptionRefNotChild = HtmlDocument.getString_0(107245533);
			HtmlDocument.HtmlExceptionUseIdAttributeFalse = HtmlDocument.getString_0(107245472);
			HtmlDocument.HtmlExceptionClassDoesNotExist = HtmlDocument.getString_0(107245375);
			HtmlDocument.HtmlExceptionClassExists = HtmlDocument.getString_0(107245342);
			HtmlDocument.HtmlResetters = new Dictionary<string, string[]>
			{
				{
					HtmlDocument.getString_0(107245923),
					new string[]
					{
						HtmlDocument.getString_0(107245268),
						HtmlDocument.getString_0(107245255)
					}
				},
				{
					HtmlDocument.getString_0(107245222),
					new string[]
					{
						HtmlDocument.getString_0(107245245)
					}
				},
				{
					HtmlDocument.getString_0(107245212),
					new string[]
					{
						HtmlDocument.getString_0(107245222),
						HtmlDocument.getString_0(107245245)
					}
				},
				{
					HtmlDocument.getString_0(107245217),
					new string[]
					{
						HtmlDocument.getString_0(107245222),
						HtmlDocument.getString_0(107245245)
					}
				}
			};
			HtmlDocument.BlockAttributes = new List<string>
			{
				HtmlDocument.getString_0(107371829),
				HtmlDocument.getString_0(107455513)
			};
		}

		internal static bool _disableBehaviorTagP;

		private static int _maxDepthLevel;

		private int _c;

		private Crc32 _crc32;

		private HtmlAttribute _currentattribute;

		private HtmlNode _currentnode;

		private Encoding _declaredencoding;

		private HtmlNode _documentnode;

		private bool _fullcomment;

		private int _index;

		internal Dictionary<string, HtmlNode> Lastnodes = new Dictionary<string, HtmlNode>();

		private HtmlNode _lastparentnode;

		private int _line;

		private int _lineposition;

		private int _maxlineposition;

		internal Dictionary<string, HtmlNode> Nodesid;

		private HtmlDocument.ParseState _oldstate;

		private bool _onlyDetectEncoding;

		internal Dictionary<int, HtmlNode> Openednodes;

		private List<HtmlParseError> _parseerrors = new List<HtmlParseError>();

		private string _remainder;

		private int _remainderOffset;

		private HtmlDocument.ParseState _state;

		private Encoding _streamencoding;

		private bool _useHtmlEncodingForStream;

		public string Text;

		public bool BackwardCompatibility = true;

		public bool OptionAddDebuggingAttributes;

		public bool OptionAutoCloseOnEnd;

		public bool OptionCheckSyntax = true;

		public bool OptionComputeChecksum;

		public bool OptionEmptyCollection;

		public bool DisableServerSideCode;

		public Encoding OptionDefaultStreamEncoding;

		public bool OptionExtractErrorSourceText;

		public int OptionExtractErrorSourceTextMaxLength = 100;

		public bool OptionFixNestedTags;

		public bool OptionOutputAsXml;

		public bool OptionPreserveXmlNamespaces;

		public bool OptionOutputOptimizeAttributeValues;

		public bool OptionOutputOriginalCase;

		public bool OptionOutputUpperCase;

		public bool OptionReadEncoding = true;

		public string OptionStopperNodeName;

		public bool OptionUseIdAttribute = true;

		public bool OptionWriteEmptyNodes;

		public int OptionMaxNestedChildNodes;

		internal static readonly string HtmlExceptionRefNotChild;

		internal static readonly string HtmlExceptionUseIdAttributeFalse;

		internal static readonly string HtmlExceptionClassDoesNotExist;

		internal static readonly string HtmlExceptionClassExists;

		internal static readonly Dictionary<string, string[]> HtmlResetters;

		private static List<string> BlockAttributes;

		[NonSerialized]
		internal static GetString getString_0;

		private enum ParseState
		{
			Text,
			WhichTag,
			Tag,
			BetweenAttributes,
			EmptyTag,
			AttributeName,
			AttributeBeforeEquals,
			AttributeAfterEquals,
			AttributeValue,
			Comment,
			QuotedAttributeValue,
			ServerSideCode,
			PcData
		}
	}
}
