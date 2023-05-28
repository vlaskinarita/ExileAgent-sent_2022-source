using System;
using System.Globalization;
using System.IO;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal sealed class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write(TraceJsonReader.getString_1(107296627) + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		public override bool Read()
		{
			bool result = this._innerReader.Read();
			this.WriteCurrentToken();
			return result;
		}

		public override int? ReadAsInt32()
		{
			int? result = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return result;
		}

		public override string ReadAsString()
		{
			string result = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return result;
		}

		public override byte[] ReadAsBytes()
		{
			byte[] result = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return result;
		}

		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return result;
		}

		public override double? ReadAsDouble()
		{
			double? result = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return result;
		}

		public override bool? ReadAsBoolean()
		{
			bool? result = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return result;
		}

		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return result;
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return result;
		}

		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		public override object Value
		{
			get
			{
				return this._innerReader.Value;
			}
		}

		public override Type ValueType
		{
			get
			{
				return this._innerReader.ValueType;
			}
		}

		public override void Close()
		{
			this._innerReader.Close();
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo;
			return (jsonLineInfo = (this._innerReader as IJsonLineInfo)) != null && jsonLineInfo.HasLineInfo();
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo;
				if ((jsonLineInfo = (this._innerReader as IJsonLineInfo)) == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo;
				if ((jsonLineInfo = (this._innerReader as IJsonLineInfo)) == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		static TraceJsonReader()
		{
			Strings.CreateGetStringDelegate(typeof(TraceJsonReader));
		}

		private readonly JsonReader _innerReader;

		private readonly JsonTextWriter _textWriter;

		private readonly StringWriter _sw;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
