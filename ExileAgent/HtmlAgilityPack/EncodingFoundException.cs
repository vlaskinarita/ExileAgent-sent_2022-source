using System;
using System.Text;

namespace HtmlAgilityPack
{
	internal sealed class EncodingFoundException : Exception
	{
		internal EncodingFoundException(Encoding encoding)
		{
			this._encoding = encoding;
		}

		internal Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		private Encoding _encoding;
	}
}
