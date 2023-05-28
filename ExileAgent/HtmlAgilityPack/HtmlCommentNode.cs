using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlCommentNode : HtmlNode
	{
		internal HtmlCommentNode(HtmlDocument ownerdocument, int index) : base(HtmlNodeType.Comment, ownerdocument, index)
		{
		}

		public string Comment
		{
			get
			{
				if (this._comment == null)
				{
					return base.InnerHtml;
				}
				return this._comment;
			}
			set
			{
				this._comment = value;
			}
		}

		public override string InnerHtml
		{
			get
			{
				if (this._comment == null)
				{
					return base.InnerHtml;
				}
				return this._comment;
			}
			set
			{
				this._comment = value;
			}
		}

		public override string OuterHtml
		{
			get
			{
				if (this._comment == null)
				{
					return base.OuterHtml;
				}
				return HtmlCommentNode.getString_1(107246175) + this._comment + HtmlCommentNode.getString_1(107245622);
			}
		}

		static HtmlCommentNode()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlCommentNode));
		}

		private string _comment;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
