using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	public abstract class JsonReader : IDisposable
	{
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool>() ?? this.Read().ToAsync();
		}

		public async Task SkipAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (this.TokenType == JsonToken.PropertyName)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() && depth < this.Depth);
			}
		}

		internal async Task ReaderReadAndAssertAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		public virtual Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool?>() ?? Task.FromResult<bool?>(this.ReadAsBoolean());
		}

		public virtual Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<byte[]>() ?? Task.FromResult<byte[]>(this.ReadAsBytes());
		}

		internal async Task<byte[]> ReadArrayIntoByteArrayAsync(CancellationToken cancellationToken)
		{
			List<byte> buffer = new List<byte>();
			do
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(buffer));
			byte[] array = buffer.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		public virtual Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTime?>() ?? Task.FromResult<DateTime?>(this.ReadAsDateTime());
		}

		public virtual Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTimeOffset?>() ?? Task.FromResult<DateTimeOffset?>(this.ReadAsDateTimeOffset());
		}

		public virtual Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<decimal?>() ?? Task.FromResult<decimal?>(this.ReadAsDecimal());
		}

		public virtual Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult<double?>(this.ReadAsDouble());
		}

		public virtual Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<int?>() ?? Task.FromResult<int?>(this.ReadAsInt32());
		}

		public virtual Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<string>() ?? Task.FromResult<string>(this.ReadAsString());
		}

		internal async Task<bool> ReadAndMoveToContentAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (result = configuredTaskAwaiter.GetResult())
			{
				configuredTaskAwaiter = this.MoveToContentAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				result = configuredTaskAwaiter.GetResult();
			}
			return result;
		}

		internal Task<bool> MoveToContentAsync(CancellationToken cancellationToken)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType != JsonToken.None)
			{
				if (tokenType != JsonToken.Comment)
				{
					return AsyncUtils.True;
				}
			}
			return this.MoveToContentFromNonContentAsync(cancellationToken);
		}

		private async Task<bool> MoveToContentFromNonContentAsync(CancellationToken cancellationToken)
		{
			JsonToken tokenType;
			do
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					goto Block_3;
				}
				tokenType = this.TokenType;
			}
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment);
			return true;
			Block_3:
			return false;
		}

		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		public bool CloseInput { get; set; }

		public bool SupportMultipleContent { get; set; }

		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException(JsonReader.getString_0(107452630));
				}
				this._dateTimeZoneHandling = value;
			}
		}

		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException(JsonReader.getString_0(107452630));
				}
				this._dateParseHandling = value;
			}
		}

		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException(JsonReader.getString_0(107452630));
				}
				this._floatParseHandling = value;
			}
		}

		public string DateFormatString
		{
			get
			{
				return this._dateFormatString;
			}
			set
			{
				this._dateFormatString = value;
			}
		}

		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				if (num.GetValueOrDefault() <= 0 & num != null)
				{
					throw new ArgumentException(JsonReader.getString_0(107351426), JsonReader.getString_0(107452630));
				}
				this._maxDepth = value;
			}
		}

		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		public virtual Type ValueType
		{
			get
			{
				object value = this._value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		public virtual int Depth
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (!JsonTokenUtils.IsStartToken(this.TokenType) && this._currentPosition.Type != JsonContainerType.None)
				{
					return num + 1;
				}
				return num;
			}
		}

		public virtual string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null;
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		internal JsonPosition GetPosition(int depth)
		{
			if (this._stack != null && depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this.CloseInput = true;
		}

		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			if (this._stack == null)
			{
				this._stack = new List<JsonPosition>();
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null)
			{
				int num = this.Depth + 1;
				int? maxDepth = this._maxDepth;
				if ((num > maxDepth.GetValueOrDefault() & maxDepth != null) && !this._hasExceededMaxDepth)
				{
					this._hasExceededMaxDepth = true;
					throw JsonReaderException.Create(this, JsonReader.getString_0(107351393).FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
				}
			}
		}

		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				currentPosition = this._currentPosition;
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				currentPosition = this._currentPosition;
				this._currentPosition = default(JsonPosition);
			}
			if (this._maxDepth != null)
			{
				int depth = this.Depth;
				int? maxDepth = this._maxDepth;
				if (depth <= maxDepth.GetValueOrDefault() & maxDepth != null)
				{
					this._hasExceededMaxDepth = false;
				}
			}
			return currentPosition.Type;
		}

		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		public abstract bool Read();

		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					object obj;
					int num;
					if ((obj = value) is int)
					{
						num = (int)obj;
						return new int?(num);
					}
					if ((obj = value) is BigInteger)
					{
						BigInteger value2 = (BigInteger)obj;
						num = (int)value2;
					}
					else
					{
						try
						{
							num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, JsonReader.getString_0(107351328).FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Integer, num, false);
					return new int?(num);
				}
				case JsonToken.String:
				{
					string s = (string)this.Value;
					return this.ReadInt32String(s);
				}
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_FC;
				}
				throw JsonReaderException.Create(this, JsonReader.getString_0(107351311).FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_FC:
			return null;
		}

		internal int? ReadInt32String(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			int num;
			if (!int.TryParse(s, NumberStyles.Integer, this.Culture, out num))
			{
				this.SetToken(JsonToken.String, s, false);
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350738).FormatWith(CultureInfo.InvariantCulture, s));
			}
			this.SetToken(JsonToken.Integer, num, false);
			return new int?(num);
		}

		public virtual string ReadAsString()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken == JsonToken.None)
				{
					goto IL_AD;
				}
				if (contentToken == JsonToken.String)
				{
					return (string)this.Value;
				}
			}
			else
			{
				if (contentToken == JsonToken.Null)
				{
					goto IL_AD;
				}
				if (contentToken == JsonToken.EndArray)
				{
					goto IL_AD;
				}
			}
			if (JsonTokenUtils.IsPrimitiveToken(contentToken))
			{
				object value = this.Value;
				if (value != null)
				{
					IFormattable formattable;
					string text;
					if ((formattable = (value as IFormattable)) != null)
					{
						text = formattable.ToString(null, this.Culture);
					}
					else
					{
						Uri uri;
						text = (((uri = (value as Uri)) != null) ? uri.OriginalString : value.ToString());
					}
					this.SetToken(JsonToken.String, text, false);
					return text;
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350649).FormatWith(CultureInfo.InvariantCulture, contentToken));
			IL_AD:
			return null;
		}

		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				switch (contentToken)
				{
				case JsonToken.None:
					goto IL_13E;
				case JsonToken.StartObject:
				{
					this.ReadIntoWrappedTypeObject();
					byte[] array = this.ReadAsBytes();
					this.ReaderReadAndAssert();
					if (this.TokenType != JsonToken.EndObject)
					{
						throw JsonReaderException.Create(this, JsonReader.getString_0(107350588).FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
					this.SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				case JsonToken.StartArray:
					return this.ReadArrayIntoByteArray();
				default:
					if (contentToken == JsonToken.String)
					{
						string text = (string)this.Value;
						byte[] array2;
						Guid guid;
						if (text.Length == 0)
						{
							array2 = CollectionUtils.ArrayEmpty<byte>();
						}
						else if (ConvertUtils.TryConvertGuid(text, out guid))
						{
							array2 = guid.ToByteArray();
						}
						else
						{
							array2 = Convert.FromBase64String(text);
						}
						this.SetToken(JsonToken.Bytes, array2, false);
						return array2;
					}
					break;
				}
			}
			else
			{
				if (contentToken == JsonToken.Null || contentToken == JsonToken.EndArray)
				{
					goto IL_13E;
				}
				if (contentToken == JsonToken.Bytes)
				{
					object value;
					if ((value = this.Value) is Guid)
					{
						byte[] array3 = ((Guid)value).ToByteArray();
						this.SetToken(JsonToken.Bytes, array3, false);
						return array3;
					}
					return (byte[])this.Value;
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350588).FormatWith(CultureInfo.InvariantCulture, contentToken));
			IL_13E:
			return null;
		}

		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!this.Read())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType <= JsonToken.Comment)
			{
				if (tokenType == JsonToken.None)
				{
					throw JsonReaderException.Create(this, JsonReader.getString_0(107350527));
				}
				if (tokenType == JsonToken.Comment)
				{
					return false;
				}
			}
			else
			{
				if (tokenType == JsonToken.Integer)
				{
					buffer.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
					return false;
				}
				if (tokenType == JsonToken.EndArray)
				{
					return true;
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350510).FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					object obj;
					double num;
					if ((obj = value) is double)
					{
						num = (double)obj;
						return new double?(num);
					}
					if ((obj = value) is BigInteger)
					{
						BigInteger value2 = (BigInteger)obj;
						num = (double)value2;
					}
					else
					{
						num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Float, num, false);
					return new double?(num);
				}
				case JsonToken.String:
					return this.ReadDoubleString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_CF;
				}
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350965).FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_CF:
			return null;
		}

		internal double? ReadDoubleString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			double num;
			if (!double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, this.Culture, out num))
			{
				this.SetToken(JsonToken.String, s, false);
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350904).FormatWith(CultureInfo.InvariantCulture, s));
			}
			this.SetToken(JsonToken.Float, num, false);
			return new double?(num);
		}

		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value;
					bool flag;
					if ((value = this.Value) is BigInteger)
					{
						BigInteger left = (BigInteger)value;
						flag = (left != 0L);
					}
					else
					{
						flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Boolean, flag, false);
					return new bool?(flag);
				}
				case JsonToken.String:
					return this.ReadBooleanString((string)this.Value);
				case JsonToken.Boolean:
					return new bool?((bool)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_D1;
				}
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350815).FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_D1:
			return null;
		}

		internal bool? ReadBooleanString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			bool flag;
			if (!bool.TryParse(s, out flag))
			{
				this.SetToken(JsonToken.String, s, false);
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350754).FormatWith(CultureInfo.InvariantCulture, s));
			}
			this.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
		}

		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					object obj;
					decimal num;
					if ((obj = value) is decimal)
					{
						num = (decimal)obj;
						return new decimal?(num);
					}
					if ((obj = value) is BigInteger)
					{
						BigInteger value2 = (BigInteger)obj;
						num = (decimal)value2;
					}
					else
					{
						try
						{
							num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, JsonReader.getString_0(107350217).FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Float, num, false);
					return new decimal?(num);
				}
				case JsonToken.String:
					return this.ReadDecimalString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_F8;
				}
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350168).FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_F8:
			return null;
		}

		internal decimal? ReadDecimalString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			decimal num;
			if (decimal.TryParse(s, NumberStyles.Number, this.Culture, out num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			if (ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out num) == ParseResult.Success)
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350075).FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual DateTime? ReadAsDateTime()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken == JsonToken.None)
				{
					goto IL_A5;
				}
				if (contentToken == JsonToken.String)
				{
					string s = (string)this.Value;
					return this.ReadDateTimeString(s);
				}
			}
			else
			{
				if (contentToken == JsonToken.Null || contentToken == JsonToken.EndArray)
				{
					goto IL_A5;
				}
				if (contentToken == JsonToken.Date)
				{
					object value;
					if ((value = this.Value) is DateTimeOffset)
					{
						this.SetToken(JsonToken.Date, ((DateTimeOffset)value).DateTime, false);
					}
					return new DateTime?((DateTime)this.Value);
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350018).FormatWith(CultureInfo.InvariantCulture, this.TokenType));
			IL_A5:
			return null;
		}

		internal DateTime? ReadDateTimeString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTime dateTime;
			if (DateTimeUtils.TryParseDateTime(s, this.DateTimeZoneHandling, this._dateFormatString, this.Culture, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			if (!DateTime.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTime))
			{
				throw JsonReaderException.Create(this, JsonReader.getString_0(107349993).FormatWith(CultureInfo.InvariantCulture, s));
			}
			dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
			this.SetToken(JsonToken.Date, dateTime, false);
			return new DateTime?(dateTime);
		}

		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken == JsonToken.None)
				{
					goto IL_9F;
				}
				if (contentToken == JsonToken.String)
				{
					string s = (string)this.Value;
					return this.ReadDateTimeOffsetString(s);
				}
			}
			else
			{
				if (contentToken == JsonToken.Null || contentToken == JsonToken.EndArray)
				{
					goto IL_9F;
				}
				if (contentToken == JsonToken.Date)
				{
					object value;
					if ((value = this.Value) is DateTime)
					{
						DateTime dateTime = (DateTime)value;
						this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime), false);
					}
					return new DateTimeOffset?((DateTimeOffset)this.Value);
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350018).FormatWith(CultureInfo.InvariantCulture, contentToken));
			IL_9F:
			return null;
		}

		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeUtils.TryParseDateTimeOffset(s, this._dateFormatString, this.Culture, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			if (!DateTimeOffset.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
			{
				this.SetToken(JsonToken.String, s, false);
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350448).FormatWith(CultureInfo.InvariantCulture, s));
			}
			this.SetToken(JsonToken.Date, dateTimeOffset, false);
			return new DateTimeOffset?(dateTimeOffset);
		}

		internal void ReaderReadAndAssert()
		{
			if (!this.Read())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, JsonReader.getString_0(107350383));
		}

		internal void ReadIntoWrappedTypeObject()
		{
			this.ReaderReadAndAssert();
			if (this.Value != null && this.Value.ToString() == JsonReader.getString_0(107350306))
			{
				this.ReaderReadAndAssert();
				if (this.Value != null && this.Value.ToString().StartsWith(JsonReader.getString_0(107350297), StringComparison.Ordinal))
				{
					this.ReaderReadAndAssert();
					if (this.Value.ToString() == JsonReader.getString_0(107350276))
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, JsonReader.getString_0(107350588).FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null, true);
		}

		protected void SetToken(JsonToken newToken, object value)
		{
			this.SetToken(newToken, value, true);
		}

		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
		{
			this._tokenType = newToken;
			this._value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this._currentPosition.PropertyName = (string)value;
				return;
			case JsonToken.Comment:
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.SetPostValueState(updateIndex);
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				return;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				return;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				return;
			default:
				return;
			}
		}

		internal void SetPostValueState(bool updateIndex)
		{
			if (this.Peek() == JsonContainerType.None && !this.SupportMultipleContent)
			{
				this.SetFinished();
			}
			else
			{
				this._currentState = JsonReader.State.PostValue;
			}
			if (updateIndex)
			{
				this.UpdateScopeWithFinishedValue();
			}
		}

		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, JsonReader.getString_0(107350267).FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (this.Peek() == JsonContainerType.None && !this.SupportMultipleContent)
			{
				this.SetFinished();
				return;
			}
			this._currentState = JsonReader.State.PostValue;
		}

		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this.SetFinished();
				return;
			case JsonContainerType.Object:
				this._currentState = JsonReader.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonReader.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				return;
			default:
				throw JsonReaderException.Create(this, JsonReader.getString_0(107349714).FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		private void SetFinished()
		{
			this._currentState = (this.SupportMultipleContent ? JsonReader.State.Start : JsonReader.State.Finished);
		}

		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, JsonReader.getString_0(107349580).FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		internal void ReadAndAssert()
		{
			if (!this.Read())
			{
				throw JsonSerializationException.Create(this, JsonReader.getString_0(107350383));
			}
		}

		internal void ReadForTypeAndAssert(JsonContract contract, bool hasConverter)
		{
			if (!this.ReadForType(contract, hasConverter))
			{
				throw JsonSerializationException.Create(this, JsonReader.getString_0(107350383));
			}
		}

		internal bool ReadForType(JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return this.Read();
			}
			if (contract != null)
			{
				switch (contract.InternalReadType)
				{
				case ReadType.Read:
					goto IL_48;
				case ReadType.ReadAsInt32:
					this.ReadAsInt32();
					break;
				case ReadType.ReadAsInt64:
				{
					bool result = this.ReadAndMoveToContent();
					if (this.TokenType == JsonToken.Undefined)
					{
						string format = JsonReader.getString_0(107349503);
						IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
						Type arg;
						if (contract != null)
						{
							if ((arg = contract.UnderlyingType) != null)
							{
								goto IL_9A;
							}
						}
						arg = typeof(long);
						IL_9A:
						throw JsonReaderException.Create(this, format.FormatWith(invariantCulture, arg));
					}
					return result;
				}
				case ReadType.ReadAsBytes:
					this.ReadAsBytes();
					break;
				case ReadType.ReadAsString:
					this.ReadAsString();
					break;
				case ReadType.ReadAsDecimal:
					this.ReadAsDecimal();
					break;
				case ReadType.ReadAsDateTime:
					this.ReadAsDateTime();
					break;
				case ReadType.ReadAsDateTimeOffset:
					this.ReadAsDateTimeOffset();
					break;
				case ReadType.ReadAsDouble:
					this.ReadAsDouble();
					break;
				case ReadType.ReadAsBoolean:
					this.ReadAsBoolean();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return this.TokenType > JsonToken.None;
			}
			IL_48:
			return this.ReadAndMoveToContent();
		}

		internal bool ReadAndMoveToContent()
		{
			return this.Read() && this.MoveToContent();
		}

		internal bool MoveToContent()
		{
			JsonToken tokenType = this.TokenType;
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				if (!this.Read())
				{
					return false;
				}
				tokenType = this.TokenType;
			}
			return true;
		}

		private JsonToken GetContentToken()
		{
			while (this.Read())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					return tokenType;
				}
			}
			this.SetToken(JsonToken.None);
			return JsonToken.None;
		}

		static JsonReader()
		{
			Strings.CreateGetStringDelegate(typeof(JsonReader));
		}

		private JsonToken _tokenType;

		private object _value;

		internal char _quoteChar;

		internal JsonReader.State _currentState;

		private JsonPosition _currentPosition;

		private CultureInfo _culture;

		private DateTimeZoneHandling _dateTimeZoneHandling;

		private int? _maxDepth;

		private bool _hasExceededMaxDepth;

		internal DateParseHandling _dateParseHandling;

		internal FloatParseHandling _floatParseHandling;

		private string _dateFormatString;

		private List<JsonPosition> _stack;

		[NonSerialized]
		internal static GetString getString_0;

		protected internal enum State
		{
			Start,
			Complete,
			Property,
			ObjectStart,
			Object,
			ArrayStart,
			Array,
			Closed,
			PostValue,
			ConstructorStart,
			Constructor,
			Error,
			Finished
		}
	}
}
