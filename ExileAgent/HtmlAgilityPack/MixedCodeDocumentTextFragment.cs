using System;

namespace HtmlAgilityPack
{
	public sealed class MixedCodeDocumentTextFragment : MixedCodeDocumentFragment
	{
		internal MixedCodeDocumentTextFragment(MixedCodeDocument doc) : base(doc, MixedCodeDocumentFragmentType.Text)
		{
		}

		public string Text
		{
			get
			{
				return base.FragmentText;
			}
			set
			{
				base.FragmentText = value;
			}
		}
	}
}
