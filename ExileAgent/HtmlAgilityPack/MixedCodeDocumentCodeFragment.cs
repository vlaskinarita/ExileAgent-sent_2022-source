using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class MixedCodeDocumentCodeFragment : MixedCodeDocumentFragment
	{
		internal MixedCodeDocumentCodeFragment(MixedCodeDocument doc) : base(doc, MixedCodeDocumentFragmentType.Code)
		{
		}

		public string Code
		{
			get
			{
				if (this._code == null)
				{
					this._code = base.FragmentText.Substring(this.Doc.TokenCodeStart.Length, base.FragmentText.Length - this.Doc.TokenCodeEnd.Length - this.Doc.TokenCodeStart.Length - 1).Trim();
					if (this._code.StartsWith(MixedCodeDocumentCodeFragment.getString_0(107226298)))
					{
						this._code = this.Doc.TokenResponseWrite + this._code.Substring(1, this._code.Length - 1);
					}
				}
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}

		static MixedCodeDocumentCodeFragment()
		{
			Strings.CreateGetStringDelegate(typeof(MixedCodeDocumentCodeFragment));
		}

		private string _code;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
