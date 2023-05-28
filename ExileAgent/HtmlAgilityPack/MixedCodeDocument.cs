using System;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class MixedCodeDocument
	{
		public MixedCodeDocument()
		{
			this._codefragments = new MixedCodeDocumentFragmentList(this);
			this._textfragments = new MixedCodeDocumentFragmentList(this);
			this._fragments = new MixedCodeDocumentFragmentList(this);
		}

		public string Code
		{
			get
			{
				string text = MixedCodeDocument.getString_0(107399983);
				int num = 0;
				foreach (MixedCodeDocumentFragment mixedCodeDocumentFragment in this._fragments)
				{
					MixedCodeDocumentFragmentType type = mixedCodeDocumentFragment._type;
					if (type != MixedCodeDocumentFragmentType.Code)
					{
						if (type == MixedCodeDocumentFragmentType.Text)
						{
							text = text + this.TokenResponseWrite + string.Format(this.TokenTextBlock, num) + MixedCodeDocument.getString_0(107398777);
							num++;
						}
					}
					else
					{
						text = text + ((MixedCodeDocumentCodeFragment)mixedCodeDocumentFragment).Code + MixedCodeDocument.getString_0(107398777);
					}
				}
				return text;
			}
		}

		public MixedCodeDocumentFragmentList CodeFragments
		{
			get
			{
				return this._codefragments;
			}
		}

		public MixedCodeDocumentFragmentList Fragments
		{
			get
			{
				return this._fragments;
			}
		}

		public Encoding StreamEncoding
		{
			get
			{
				return this._streamencoding;
			}
		}

		public MixedCodeDocumentFragmentList TextFragments
		{
			get
			{
				return this._textfragments;
			}
		}

		public MixedCodeDocumentCodeFragment CreateCodeFragment()
		{
			return (MixedCodeDocumentCodeFragment)this.CreateFragment(MixedCodeDocumentFragmentType.Code);
		}

		public MixedCodeDocumentTextFragment CreateTextFragment()
		{
			return (MixedCodeDocumentTextFragment)this.CreateFragment(MixedCodeDocumentFragmentType.Text);
		}

		public void Load(Stream stream)
		{
			this.Load(new StreamReader(stream));
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

		public void Load(string path)
		{
			this.Load(new StreamReader(path));
		}

		public void Load(string path, bool detectEncodingFromByteOrderMarks)
		{
			this.Load(new StreamReader(path, detectEncodingFromByteOrderMarks));
		}

		public void Load(string path, Encoding encoding)
		{
			this.Load(new StreamReader(path, encoding));
		}

		public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
		{
			this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks));
		}

		public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
		{
			this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize));
		}

		public void Load(TextReader reader)
		{
			this._codefragments.Clear();
			this._textfragments.Clear();
			using (StreamReader streamReader = reader as StreamReader)
			{
				if (streamReader != null)
				{
					this._streamencoding = streamReader.CurrentEncoding;
				}
				this._text = reader.ReadToEnd();
			}
			this.Parse();
		}

		public void LoadHtml(string html)
		{
			this.Load(new StringReader(html));
		}

		public void Save(Stream outStream)
		{
			StreamWriter writer = new StreamWriter(outStream, this.GetOutEncoding());
			this.Save(writer);
		}

		public void Save(Stream outStream, Encoding encoding)
		{
			StreamWriter writer = new StreamWriter(outStream, encoding);
			this.Save(writer);
		}

		public void Save(string filename)
		{
			StreamWriter writer = new StreamWriter(filename, false, this.GetOutEncoding());
			this.Save(writer);
		}

		public void Save(string filename, Encoding encoding)
		{
			StreamWriter writer = new StreamWriter(filename, false, encoding);
			this.Save(writer);
		}

		public void Save(StreamWriter writer)
		{
			this.Save(writer);
		}

		public void Save(TextWriter writer)
		{
			writer.Flush();
		}

		internal MixedCodeDocumentFragment CreateFragment(MixedCodeDocumentFragmentType type)
		{
			if (type == MixedCodeDocumentFragmentType.Code)
			{
				return new MixedCodeDocumentCodeFragment(this);
			}
			if (type == MixedCodeDocumentFragmentType.Text)
			{
				return new MixedCodeDocumentTextFragment(this);
			}
			throw new NotSupportedException();
		}

		internal Encoding GetOutEncoding()
		{
			if (this._streamencoding != null)
			{
				return this._streamencoding;
			}
			return Encoding.UTF8;
		}

		private void IncrementPosition()
		{
			this._index++;
			if (this._c == 10)
			{
				this._lineposition = 1;
				this._line++;
				return;
			}
			this._lineposition++;
		}

		private void Parse()
		{
			this._state = MixedCodeDocument.ParseState.Text;
			this._index = 0;
			this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
			while (this._index < this._text.Length)
			{
				this._c = (int)this._text[this._index];
				this.IncrementPosition();
				MixedCodeDocument.ParseState state = this._state;
				if (state != MixedCodeDocument.ParseState.Text)
				{
					if (state == MixedCodeDocument.ParseState.Code)
					{
						if (this._index + this.TokenCodeEnd.Length < this._text.Length && this._text.Substring(this._index - 1, this.TokenCodeEnd.Length) == this.TokenCodeEnd)
						{
							this._state = MixedCodeDocument.ParseState.Text;
							this._currentfragment.Length = this._index + this.TokenCodeEnd.Length - this._currentfragment.Index;
							this._index += this.TokenCodeEnd.Length;
							this._lineposition += this.TokenCodeEnd.Length;
							this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
							this.SetPosition();
						}
					}
				}
				else if (this._index + this.TokenCodeStart.Length < this._text.Length && this._text.Substring(this._index - 1, this.TokenCodeStart.Length) == this.TokenCodeStart)
				{
					this._state = MixedCodeDocument.ParseState.Code;
					this._currentfragment.Length = this._index - 1 - this._currentfragment.Index;
					this._currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Code);
					this.SetPosition();
				}
			}
			this._currentfragment.Length = this._index - this._currentfragment.Index;
		}

		private void SetPosition()
		{
			this._currentfragment.Line = this._line;
			this._currentfragment._lineposition = this._lineposition;
			this._currentfragment.Index = this._index - 1;
			this._currentfragment.Length = 0;
		}

		static MixedCodeDocument()
		{
			Strings.CreateGetStringDelegate(typeof(MixedCodeDocument));
		}

		private int _c;

		internal MixedCodeDocumentFragmentList _codefragments;

		private MixedCodeDocumentFragment _currentfragment;

		internal MixedCodeDocumentFragmentList _fragments;

		private int _index;

		private int _line;

		private int _lineposition;

		private MixedCodeDocument.ParseState _state;

		private Encoding _streamencoding;

		internal string _text;

		internal MixedCodeDocumentFragmentList _textfragments;

		public string TokenCodeEnd = MixedCodeDocument.getString_0(107320677);

		public string TokenCodeStart = MixedCodeDocument.getString_0(107320704);

		public string TokenDirective = MixedCodeDocument.getString_0(107381849);

		public string TokenResponseWrite = MixedCodeDocument.getString_0(107320699);

		private string TokenTextBlock = MixedCodeDocument.getString_0(107320646);

		[NonSerialized]
		internal static GetString getString_0;

		private enum ParseState
		{
			Text,
			Code
		}
	}
}
