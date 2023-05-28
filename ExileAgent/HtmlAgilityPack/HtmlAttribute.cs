using System;
using System.Collections.Generic;
using System.Diagnostics;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	[DebuggerDisplay("Name: {OriginalName}, Value: {Value}")]
	public sealed class HtmlAttribute : IComparable
	{
		internal HtmlAttribute(HtmlDocument ownerdocument)
		{
			this._ownerdocument = ownerdocument;
		}

		public int Line
		{
			get
			{
				return this._line;
			}
			internal set
			{
				this._line = value;
			}
		}

		public int LinePosition
		{
			get
			{
				return this._lineposition;
			}
		}

		public int ValueStartIndex
		{
			get
			{
				return this._valuestartindex;
			}
		}

		public int ValueLength
		{
			get
			{
				return this._valuelength;
			}
		}

		public bool UseOriginalName { get; set; }

		public string Name
		{
			get
			{
				if (this._name == null)
				{
					this._name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
				}
				if (!this.UseOriginalName)
				{
					return this._name.ToLowerInvariant();
				}
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(HtmlAttribute.getString_0(107451957));
				}
				this._name = value;
				if (this._ownernode != null)
				{
					this._ownernode.SetChanged();
				}
			}
		}

		public string OriginalName
		{
			get
			{
				return this._name;
			}
		}

		public HtmlDocument OwnerDocument
		{
			get
			{
				return this._ownerdocument;
			}
		}

		public HtmlNode OwnerNode
		{
			get
			{
				return this._ownernode;
			}
		}

		public AttributeValueQuote QuoteType
		{
			get
			{
				return this._quoteType;
			}
			set
			{
				this._quoteType = value;
			}
		}

		public int StreamPosition
		{
			get
			{
				return this._streamposition;
			}
		}

		public string Value
		{
			get
			{
				if (this._value == null && this._ownerdocument.Text == null && this._valuestartindex == 0 && this._valuelength == 0)
				{
					return null;
				}
				if (this._value == null)
				{
					this._value = this._ownerdocument.Text.Substring(this._valuestartindex, this._valuelength);
					if (!this._ownerdocument.BackwardCompatibility)
					{
						this._value = HtmlEntity.DeEntitize(this._value);
					}
				}
				return this._value;
			}
			set
			{
				this._value = value;
				if (this._ownernode != null)
				{
					this._ownernode.SetChanged();
				}
			}
		}

		public string DeEntitizeValue
		{
			get
			{
				return HtmlEntity.DeEntitize(this.Value);
			}
		}

		internal string XmlName
		{
			get
			{
				return HtmlDocument.GetXmlName(this.Name, true, this.OwnerDocument.OptionPreserveXmlNamespaces);
			}
		}

		internal string XmlValue
		{
			get
			{
				return this.Value;
			}
		}

		public string XPath
		{
			get
			{
				return ((this.OwnerNode == null) ? HtmlAttribute.getString_0(107374892) : (this.OwnerNode.XPath + HtmlAttribute.getString_0(107374892))) + this.GetRelativeXpath();
			}
		}

		public int CompareTo(object obj)
		{
			HtmlAttribute htmlAttribute = obj as HtmlAttribute;
			if (htmlAttribute == null)
			{
				throw new ArgumentException(HtmlAttribute.getString_0(107246234));
			}
			return this.Name.CompareTo(htmlAttribute.Name);
		}

		public HtmlAttribute Clone()
		{
			return new HtmlAttribute(this._ownerdocument)
			{
				Name = this.Name,
				Value = this.Value,
				QuoteType = this.QuoteType
			};
		}

		public void Remove()
		{
			this._ownernode.Attributes.Remove(this);
		}

		private string GetRelativeXpath()
		{
			if (this.OwnerNode == null)
			{
				return this.Name;
			}
			int num = 1;
			foreach (HtmlAttribute htmlAttribute in ((IEnumerable<HtmlAttribute>)this.OwnerNode.Attributes))
			{
				if (!(htmlAttribute.Name != this.Name))
				{
					if (htmlAttribute == this)
					{
						break;
					}
					num++;
				}
			}
			return string.Concat(new string[]
			{
				HtmlAttribute.getString_0(107381486),
				this.Name,
				HtmlAttribute.getString_0(107371611),
				num.ToString(),
				HtmlAttribute.getString_0(107371638)
			});
		}

		static HtmlAttribute()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlAttribute));
		}

		private int _line;

		internal int _lineposition;

		internal string _name;

		internal int _namelength;

		internal int _namestartindex;

		internal HtmlDocument _ownerdocument;

		internal HtmlNode _ownernode;

		private AttributeValueQuote _quoteType = AttributeValueQuote.DoubleQuote;

		internal int _streamposition;

		internal string _value;

		internal int _valuelength;

		internal int _valuestartindex;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
