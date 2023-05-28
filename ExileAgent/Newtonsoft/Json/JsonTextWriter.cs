using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	public sealed class JsonTextWriter : JsonWriter
	{
		public override Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.FlushAsync(cancellationToken);
			}
			return this.DoFlushAsync(cancellationToken);
		}

		internal Task DoFlushAsync(CancellationToken cancellationToken)
		{
			return cancellationToken.CancelIfRequestedAsync() ?? this._writer.FlushAsync();
		}

		protected override Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteValueDelimiterAsync(cancellationToken);
			}
			return this.DoWriteValueDelimiterAsync(cancellationToken);
		}

		internal Task DoWriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(',', cancellationToken);
		}

		protected override Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(token, cancellationToken);
			}
			return this.DoWriteEndAsync(token, cancellationToken);
		}

		internal Task DoWriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return this._writer.WriteAsync('}', cancellationToken);
			case JsonToken.EndArray:
				return this._writer.WriteAsync(']', cancellationToken);
			case JsonToken.EndConstructor:
				return this._writer.WriteAsync(')', cancellationToken);
			default:
				throw JsonWriterException.Create(this, JsonTextWriter.getString_1(107348177) + token, null);
			}
		}

		public override Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.CloseAsync(cancellationToken);
			}
			return this.DoCloseAsync(cancellationToken);
		}

		internal async Task DoCloseAsync(CancellationToken cancellationToken)
		{
			if (base.Top == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
			}
			while (base.Top > 0)
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.WriteEndAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			this.CloseBufferAndWriter();
		}

		public override Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(cancellationToken);
			}
			return base.WriteEndInternalAsync(cancellationToken);
		}

		protected override Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentAsync(cancellationToken);
			}
			return this.DoWriteIndentAsync(cancellationToken);
		}

		internal Task DoWriteIndentAsync(CancellationToken cancellationToken)
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			if (num <= 12)
			{
				return this._writer.WriteAsync(this._indentChars, 0, num2 + num, cancellationToken);
			}
			return this.WriteIndentAsync(num, num2, cancellationToken);
		}

		private async Task WriteIndentAsync(int currentIndentCount, int newLineLen, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._indentChars, 0, newLineLen + Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			while ((currentIndentCount -= 12) > 0)
			{
				configuredTaskAwaiter = this._writer.WriteAsync(this._indentChars, newLineLen, Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
		}

		private Task WriteValueInternalAsync(JsonToken token, string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(token, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this._writer.WriteAsync(value, cancellationToken);
			}
			return this.WriteValueInternalAsync(task, value, cancellationToken);
		}

		private async Task WriteValueInternalAsync(Task task, string value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this._writer.WriteAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		protected override Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentSpaceAsync(cancellationToken);
			}
			return this.DoWriteIndentSpaceAsync(cancellationToken);
		}

		internal Task DoWriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(' ', cancellationToken);
		}

		public override Task WriteRawAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawAsync(json, cancellationToken);
		}

		internal Task DoWriteRawAsync(string json, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(json, cancellationToken);
		}

		public override Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteNullAsync(cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		internal Task DoWriteNullAsync(CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Null, JsonConvert.Null, cancellationToken);
		}

		private Task WriteDigitsAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			if (uvalue <= 9UL & !negative)
			{
				return this._writer.WriteAsync((char)(48UL + uvalue), cancellationToken);
			}
			int count = this.WriteNumberToBuffer(uvalue, negative);
			return this._writer.WriteAsync(this._writeBuffer, 0, count, cancellationToken);
		}

		private Task WriteIntegerValueAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Integer, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteDigitsAsync(uvalue, negative, cancellationToken);
			}
			return this.WriteIntegerValueAsync(task, uvalue, negative, cancellationToken);
		}

		private async Task WriteIntegerValueAsync(Task task, ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this.WriteDigitsAsync(uvalue, negative, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		internal Task WriteIntegerValueAsync(long value, CancellationToken cancellationToken)
		{
			bool negative;
			if (negative = (value < 0L))
			{
				value = -value;
			}
			return this.WriteIntegerValueAsync((ulong)value, negative, cancellationToken);
		}

		internal Task WriteIntegerValueAsync(ulong uvalue, CancellationToken cancellationToken)
		{
			return this.WriteIntegerValueAsync(uvalue, false, cancellationToken);
		}

		private Task WriteEscapedStringAsync(string value, bool quote, CancellationToken cancellationToken)
		{
			return JavaScriptUtils.WriteEscapedJavaScriptStringAsync(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this, this._writeBuffer, cancellationToken);
		}

		public override Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, cancellationToken);
		}

		internal Task DoWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			Task task = base.InternalWritePropertyNameAsync(name, cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return this.DoWritePropertyNameAsync(task, name, cancellationToken);
			}
			task = this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this._writer.WriteAsync(':', cancellationToken);
			}
			return JavaScriptUtils.WriteCharAsync(task, this._writer, ':', cancellationToken);
		}

		private async Task DoWritePropertyNameAsync(Task task, string name, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(':').ConfigureAwait(false);
		}

		public override Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, escape, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, escape, cancellationToken);
		}

		internal async Task DoWritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken)
		{
			await base.InternalWritePropertyNameAsync(name, cancellationToken).ConfigureAwait(false);
			if (escape)
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			else
			{
				if (this._quoteName)
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				await this._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false);
				if (this._quoteName)
				{
					await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
				}
			}
			await this._writer.WriteAsync(':').ConfigureAwait(false);
		}

		public override Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartArrayAsync(cancellationToken);
			}
			return this.DoWriteStartArrayAsync(cancellationToken);
		}

		internal Task DoWriteStartArrayAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartArray, JsonContainerType.Array, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this._writer.WriteAsync('[', cancellationToken);
			}
			return this.DoWriteStartArrayAsync(task, cancellationToken);
		}

		internal async Task DoWriteStartArrayAsync(Task task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this._writer.WriteAsync('[', cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public override Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartObjectAsync(cancellationToken);
			}
			return this.DoWriteStartObjectAsync(cancellationToken);
		}

		internal Task DoWriteStartObjectAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartObject, JsonContainerType.Object, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this._writer.WriteAsync('{', cancellationToken);
			}
			return this.DoWriteStartObjectAsync(task, cancellationToken);
		}

		internal async Task DoWriteStartObjectAsync(Task task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this._writer.WriteAsync('{', cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public override Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartConstructorAsync(name, cancellationToken);
			}
			return this.DoWriteStartConstructorAsync(name, cancellationToken);
		}

		internal async Task DoWriteStartConstructorAsync(string name, CancellationToken cancellationToken)
		{
			await base.InternalWriteStartAsync(JsonToken.StartConstructor, JsonContainerType.Constructor, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(JsonTextWriter.<DoWriteStartConstructorAsync>d__39.getString_0(107347485), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync('(').ConfigureAwait(false);
		}

		public override Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteUndefinedAsync(cancellationToken);
			}
			return this.DoWriteUndefinedAsync(cancellationToken);
		}

		internal Task DoWriteUndefinedAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this._writer.WriteAsync(JsonConvert.Undefined, cancellationToken);
			}
			return this.DoWriteUndefinedAsync(task, cancellationToken);
		}

		private async Task DoWriteUndefinedAsync(Task task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this._writer.WriteAsync(JsonConvert.Undefined, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public override Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteWhitespaceAsync(ws, cancellationToken);
			}
			return this.DoWriteWhitespaceAsync(ws, cancellationToken);
		}

		internal Task DoWriteWhitespaceAsync(string ws, CancellationToken cancellationToken)
		{
			base.InternalWriteWhitespace(ws);
			return this._writer.WriteAsync(ws, cancellationToken);
		}

		public override Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(bool value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Boolean, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(bool? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		public override Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(byte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteValueNonNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		internal async Task WriteValueNonNullAsync(byte[] value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Bytes, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			await this.Base64Encoder.EncodeAsync(value, 0, value.Length, cancellationToken).ConfigureAwait(false);
			await this.Base64Encoder.FlushAsync(cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
		}

		public override Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(char value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.String, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(char? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal async Task DoWriteValueAsync(DateTime value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._writeBuffer, 0, count, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			else
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				await this._writer.WriteAsync(value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(false);
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			}
		}

		public override Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(DateTime? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal async Task DoWriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._writeBuffer, 0, count, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			else
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				await this._writer.WriteAsync(value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(false);
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			}
		}

		public override Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(decimal value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(decimal? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		internal Task WriteValueAsync(double value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		public override Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		public override Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		internal Task WriteValueAsync(float value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		public override Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		public override Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal async Task DoWriteValueAsync(Guid value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			await this._writer.WriteAsync(value.ToString(JsonTextWriter.<DoWriteValueAsync>d__77.getString_0(107444843), CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
		}

		public override Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(Guid? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		public override Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(int? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		public override Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(long? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		internal Task WriteValueAsync(BigInteger value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Integer, value.ToString(CultureInfo.InvariantCulture), cancellationToken);
		}

		public override Task WriteValueAsync(object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			if (value is BigInteger)
			{
				BigInteger value2 = (BigInteger)value;
				return this.WriteValueAsync(value2, cancellationToken);
			}
			return JsonWriter.WriteValueAsync(this, ConvertUtils.GetTypeCode(value.GetType()), value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(sbyte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		public override Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(short? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return this.DoWriteValueAsync(task, value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteEscapedStringAsync(value, true, cancellationToken);
			}
			return this._writer.WriteAsync(JsonConvert.Null, cancellationToken);
		}

		private async Task DoWriteValueAsync(Task task, string value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = ((value == null) ? this._writer.WriteAsync(JsonConvert.Null, cancellationToken) : this.WriteEscapedStringAsync(value, true, cancellationToken)).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public override Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal async Task DoWriteValueAsync(TimeSpan value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(value.ToString(null, CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar, cancellationToken).ConfigureAwait(false);
		}

		public override Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(TimeSpan? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(uint? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(ulong? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!(value == null))
			{
				return this.WriteValueNotNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		internal Task WriteValueNotNullAsync(Uri value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken);
			}
			return this.WriteValueNotNullAsync(task, value, cancellationToken);
		}

		internal async Task WriteValueNotNullAsync(Task task, Uri value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(ushort? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteCommentAsync(string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteCommentAsync(text, cancellationToken);
			}
			return this.DoWriteCommentAsync(text, cancellationToken);
		}

		internal async Task DoWriteCommentAsync(string text, CancellationToken cancellationToken)
		{
			await base.InternalWriteCommentAsync(cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(JsonTextWriter.<DoWriteCommentAsync>d__114.getString_0(107347566), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(text, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(JsonTextWriter.<DoWriteCommentAsync>d__114.getString_0(107347561), cancellationToken).ConfigureAwait(false);
		}

		public override Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndArrayAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
		}

		public override Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndConstructorAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
		}

		public override Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndObjectAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
		}

		public override Task WriteRawValueAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawValueAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(json, cancellationToken);
		}

		internal Task DoWriteRawValueAsync(string json, CancellationToken cancellationToken)
		{
			base.UpdateScopeWithFinishedValue();
			Task task = base.AutoCompleteAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(task, json, cancellationToken);
		}

		private async Task DoWriteRawValueAsync(Task task, string json, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this.WriteRawAsync(json, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		internal char[] EnsureWriteBuffer(int length, int copyTo)
		{
			if (length < 35)
			{
				length = 35;
			}
			char[] writeBuffer = this._writeBuffer;
			if (writeBuffer == null)
			{
				return this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, length);
			}
			if (writeBuffer.Length >= length)
			{
				return writeBuffer;
			}
			char[] array = BufferUtils.RentBuffer(this._arrayPool, length);
			if (copyTo != 0)
			{
				Array.Copy(writeBuffer, array, copyTo);
			}
			BufferUtils.ReturnBuffer(this._arrayPool, writeBuffer);
			this._writeBuffer = array;
			return array;
		}

		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		public IArrayPool<char> ArrayPool
		{
			get
			{
				return this._arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(JsonTextWriter.getString_1(107453079));
				}
				this._arrayPool = value;
			}
		}

		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(JsonTextWriter.getString_1(107348148));
				}
				this._indentation = value;
			}
		}

		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException(JsonTextWriter.getString_1(107347547));
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				if (value != this._indentChar)
				{
					this._indentChar = value;
					this._indentChars = null;
				}
			}
		}

		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException(JsonTextWriter.getString_1(107347474));
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
			this._safeAsync = (base.GetType() == typeof(JsonTextWriter));
		}

		public override void Flush()
		{
			this._writer.Flush();
		}

		public override void Close()
		{
			base.Close();
			this.CloseBufferAndWriter();
		}

		private void CloseBufferAndWriter()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write('{');
		}

		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write('[');
		}

		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write(JsonTextWriter.getString_1(107347425));
			this._writer.Write(name);
			this._writer.Write('(');
		}

		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write('}');
				return;
			case JsonToken.EndArray:
				this._writer.Write(']');
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(')');
				return;
			default:
				throw JsonWriterException.Create(this, JsonTextWriter.getString_1(107348177) + token, null);
			}
		}

		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name, this._quoteName);
			this._writer.Write(':');
		}

		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name, this._quoteName);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		private void UpdateCharEscapeFlags()
		{
			this._charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, this._quoteChar);
		}

		protected override void WriteIndent()
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			this._writer.Write(this._indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				this._writer.Write(this._indentChars, num2, Math.Min(num, 12));
			}
		}

		private int SetIndentChars()
		{
			string newLine = this._writer.NewLine;
			int length = newLine.Length;
			bool flag;
			if (flag = (this._indentChars != null && this._indentChars.Length == 12 + length))
			{
				for (int num = 0; num != length; num++)
				{
					if (newLine[num] != this._indentChars[num])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				this._indentChars = (newLine + new string(this._indentChar, 12)).ToCharArray();
			}
			return length;
		}

		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				base.InternalWriteValue(JsonToken.Integer);
				this.WriteValueInternal(bigInteger.ToString(CultureInfo.InvariantCulture), JsonToken.String);
				return;
			}
			base.WriteValue(value);
		}

		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value, true);
		}

		private void WriteEscapedString(string value, bool quote)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this._arrayPool, ref this._writeBuffer);
		}

		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value, false);
		}

		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, count);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		private int WriteValueToBuffer(DateTime value)
		{
			this.EnsureWriteBuffer();
			this._writeBuffer[0] = this._quoteChar;
			int result = DateTimeUtils.WriteDateTimeString(this._writeBuffer, 1, value, null, value.Kind, base.DateFormatHandling);
			this._writeBuffer[result++] = this._quoteChar;
			return result;
		}

		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, count);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		private int WriteValueToBuffer(DateTimeOffset value)
		{
			this.EnsureWriteBuffer();
			this._writeBuffer[0] = this._quoteChar;
			int result = DateTimeUtils.WriteDateTimeString(this._writeBuffer, 1, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), DateTimeKind.Local, base.DateFormatHandling);
			this._writeBuffer[result++] = this._quoteChar;
			return result;
		}

		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			string value2 = value.ToString(JsonTextWriter.getString_1(107444752), CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(value2);
			this._writer.Write(this._quoteChar);
		}

		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			string value2 = value.ToString(null, CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(value2);
			this._writer.Write(this._quoteChar);
		}

		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteEscapedString(value.OriginalString, true);
		}

		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			this._writer.Write(JsonTextWriter.getString_1(107347448));
			this._writer.Write(text);
			this._writer.Write(JsonTextWriter.getString_1(107347443));
		}

		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, 35);
			}
		}

		private void WriteIntegerValue(long value)
		{
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			bool flag = value < 0L;
			this.WriteIntegerValue((ulong)(flag ? (-(ulong)value) : value), flag);
		}

		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative & value <= 9UL)
			{
				this._writer.Write((char)(48UL + value));
				return;
			}
			int count = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= 4294967295UL)
			{
				return this.WriteNumberToBuffer((uint)value, negative);
			}
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10UL;
				ulong num4 = value - num3 * 10UL;
				this._writeBuffer[--num2] = (char)(48UL + num4);
				value = num3;
			}
			while (value != 0UL);
			return num;
		}

		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				this._writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			this.WriteIntegerValue((uint)(flag ? (-(uint)value) : value), flag);
		}

		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative & value <= 9U)
			{
				this._writer.Write((char)(48U + value));
				return;
			}
			int count = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(uint value, bool negative)
		{
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength((ulong)value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10U;
				uint num4 = value - num3 * 10U;
				this._writeBuffer[--num2] = (char)(48U + num4);
				value = num3;
			}
			while (value != 0U);
			return num;
		}

		static JsonTextWriter()
		{
			Strings.CreateGetStringDelegate(typeof(JsonTextWriter));
		}

		private readonly bool _safeAsync;

		private const int IndentCharBufferSize = 12;

		private readonly TextWriter _writer;

		private Base64Encoder _base64Encoder;

		private char _indentChar;

		private int _indentation;

		private char _quoteChar;

		private bool _quoteName;

		private bool[] _charEscapeFlags;

		private char[] _writeBuffer;

		private IArrayPool<char> _arrayPool;

		private char[] _indentChars;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
