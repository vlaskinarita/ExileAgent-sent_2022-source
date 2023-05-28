using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	public abstract class JsonWriter : IDisposable
	{
		internal Task AutoCompleteAsync(JsonToken tokenBeingWritten, CancellationToken cancellationToken)
		{
			JsonWriter.State currentState = this._currentState;
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107346335).FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), currentState.ToString()), null);
			}
			this._currentState = state;
			if (this._formatting == Formatting.Indented)
			{
				switch (currentState)
				{
				case JsonWriter.State.Start:
					goto IL_FB;
				case JsonWriter.State.Property:
					return this.WriteIndentSpaceAsync(cancellationToken);
				case JsonWriter.State.Object:
					if (tokenBeingWritten == JsonToken.PropertyName)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.WriteValueDelimiterAsync(cancellationToken);
					}
					goto IL_FB;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.ConstructorStart:
					return this.WriteIndentAsync(cancellationToken);
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					return this.WriteIndentAsync(cancellationToken);
				}
				if (tokenBeingWritten == JsonToken.PropertyName)
				{
					return this.WriteIndentAsync(cancellationToken);
				}
			}
			else if (tokenBeingWritten != JsonToken.Comment)
			{
				switch (currentState)
				{
				case JsonWriter.State.Object:
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					return this.WriteValueDelimiterAsync(cancellationToken);
				}
			}
			IL_FB:
			return AsyncUtils.CompletedTask;
		}

		private async Task AutoCompleteAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.WriteValueDelimiterAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = this.WriteIndentAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public virtual Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Close();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Flush();
			return AsyncUtils.CompletedTask;
		}

		protected virtual Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd(token);
			return AsyncUtils.CompletedTask;
		}

		protected virtual Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndent();
			return AsyncUtils.CompletedTask;
		}

		protected virtual Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValueDelimiter();
			return AsyncUtils.CompletedTask;
		}

		protected virtual Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndentSpace();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteRawAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRaw(json);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd();
			return AsyncUtils.CompletedTask;
		}

		internal Task WriteEndInternalAsync(CancellationToken cancellationToken)
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.Object:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonContainerType.Array:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonContainerType.Constructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			default:
				if (cancellationToken.IsCancellationRequested)
				{
					return cancellationToken.FromCanceled();
				}
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107346218) + jsonContainerType, null);
			}
		}

		internal Task InternalWriteEndAsync(JsonContainerType type, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			int levelsToComplete = this.CalculateLevelsToComplete(type);
			while (levelsToComplete-- > 0)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				Task task;
				if (this._currentState == JsonWriter.State.Property)
				{
					task = this.WriteNullAsync(cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						return this.method_0(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					task = this.WriteIndentAsync(cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						return this.method_1(task, levelsToComplete, closeTokenForType, cancellationToken);
					}
				}
				task = this.WriteEndAsync(closeTokenForType, cancellationToken);
				if (!task.IsCompletedSucessfully())
				{
					return this.method_2(task, levelsToComplete, cancellationToken);
				}
				this.UpdateCurrentState();
			}
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndArray();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndConstructor();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndObject();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteNull();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name, escape);
			return AsyncUtils.CompletedTask;
		}

		internal Task InternalWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this._currentPosition.PropertyName = name;
			return this.AutoCompleteAsync(JsonToken.PropertyName, cancellationToken);
		}

		public virtual Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartArray();
			return AsyncUtils.CompletedTask;
		}

		internal async Task InternalWriteStartAsync(JsonToken token, JsonContainerType container, CancellationToken cancellationToken)
		{
			this.UpdateScopeWithFinishedValue();
			await this.AutoCompleteAsync(token, cancellationToken).ConfigureAwait(false);
			this.Push(container);
		}

		public virtual Task WriteCommentAsync(string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteComment(text);
			return AsyncUtils.CompletedTask;
		}

		internal Task InternalWriteCommentAsync(CancellationToken cancellationToken)
		{
			return this.AutoCompleteAsync(JsonToken.Comment, cancellationToken);
		}

		public virtual Task WriteRawValueAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRawValue(json);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartConstructor(name);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartObject();
			return AsyncUtils.CompletedTask;
		}

		public Task WriteTokenAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(reader, true, cancellationToken);
		}

		public Task WriteTokenAsync(JsonReader reader, bool writeChildren, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, JsonWriter.getString_0(107246669));
			return this.WriteTokenAsync(reader, writeChildren, true, true, cancellationToken);
		}

		public Task WriteTokenAsync(JsonToken token, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(token, null, cancellationToken);
		}

		public Task WriteTokenAsync(JsonToken token, object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.None:
				return AsyncUtils.CompletedTask;
			case JsonToken.StartObject:
				return this.WriteStartObjectAsync(cancellationToken);
			case JsonToken.StartArray:
				return this.WriteStartArrayAsync(cancellationToken);
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				return this.WriteStartConstructorAsync(value.ToString(), cancellationToken);
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				return this.WritePropertyNameAsync(value.ToString(), cancellationToken);
			case JsonToken.Comment:
				return this.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Raw:
				return this.WriteRawValueAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					return this.WriteValueAsync(bigInteger, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToInt64(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					return this.WriteValueAsync(value2, cancellationToken);
				}
				if (value is double)
				{
					double value3 = (double)value;
					return this.WriteValueAsync(value3, cancellationToken);
				}
				if (value is float)
				{
					float value4 = (float)value;
					return this.WriteValueAsync(value4, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDouble(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				return this.WriteValueAsync(value.ToString(), cancellationToken);
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				return this.WriteValueAsync(Convert.ToBoolean(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Null:
				return this.WriteNullAsync(cancellationToken);
			case JsonToken.Undefined:
				return this.WriteUndefinedAsync(cancellationToken);
			case JsonToken.EndObject:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonToken.EndArray:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonToken.EndConstructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					return this.WriteValueAsync(value5, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDateTime(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is Guid)
				{
					Guid value6 = (Guid)value;
					return this.WriteValueAsync(value6, cancellationToken);
				}
				return this.WriteValueAsync((byte[])value, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JsonWriter.getString_0(107345689), token, JsonWriter.getString_0(107345648));
			}
		}

		internal virtual async Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			int initialDepth = this.CalculateWriteTokenInitialDepth(reader);
			bool flag;
			do
			{
				if (writeDateConstructorAsDate && reader.TokenType == JsonToken.StartConstructor && string.Equals(reader.Value.ToString(), JsonWriter.<WriteTokenAsync>d__30.getString_0(107345556), StringComparison.Ordinal))
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.WriteConstructorDateAsync(reader, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				else if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.WriteTokenAsync(reader.TokenType, reader.Value, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				if (flag = (initialDepth - 1 < reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) && writeChildren))
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					flag = configuredTaskAwaiter3.GetResult();
				}
			}
			while (flag);
			if (initialDepth < this.CalculateWriteTokenFinalDepth(reader))
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteTokenAsync>d__30.getString_0(107345547), null);
			}
		}

		internal async Task WriteTokenSyncReadingAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			int initialDepth = this.CalculateWriteTokenInitialDepth(reader);
			bool flag;
			do
			{
				if (reader.TokenType == JsonToken.StartConstructor && string.Equals(reader.Value.ToString(), JsonWriter.<WriteTokenSyncReadingAsync>d__31.getString_0(107345564), StringComparison.Ordinal))
				{
					this.WriteConstructorDate(reader);
				}
				else
				{
					this.WriteToken(reader.TokenType, reader.Value);
				}
				if (flag = (initialDepth - 1 < reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0)))
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					flag = configuredTaskAwaiter.GetResult();
				}
			}
			while (flag);
			if (initialDepth < this.CalculateWriteTokenFinalDepth(reader))
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteTokenSyncReadingAsync>d__31.getString_0(107345555), null);
			}
		}

		private async Task WriteConstructorDateAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteConstructorDateAsync>d__32.getString_0(107341744), null);
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteConstructorDateAsync>d__32.getString_0(107341683) + reader.TokenType, null);
			}
			DateTime date = DateTimeUtils.ConvertJavaScriptTicksToDateTime((long)reader.Value);
			configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteConstructorDateAsync>d__32.getString_0(107341744), null);
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw JsonWriterException.Create(this, JsonWriter.<WriteConstructorDateAsync>d__32.getString_0(107316593) + reader.TokenType, null);
			}
			await this.WriteValueAsync(date, cancellationToken).ConfigureAwait(false);
		}

		public virtual Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteValueAsync(Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteUndefined();
			return AsyncUtils.CompletedTask;
		}

		public virtual Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteWhitespace(ws);
			return AsyncUtils.CompletedTask;
		}

		internal Task InternalWriteValueAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.UpdateScopeWithFinishedValue();
			return this.AutoCompleteAsync(token, cancellationToken);
		}

		protected Task SetWriteStateAsync(JsonToken token, object value, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.StartObject:
				return this.InternalWriteStartAsync(token, JsonContainerType.Object, cancellationToken);
			case JsonToken.StartArray:
				return this.InternalWriteStartAsync(token, JsonContainerType.Array, cancellationToken);
			case JsonToken.StartConstructor:
				return this.InternalWriteStartAsync(token, JsonContainerType.Constructor, cancellationToken);
			case JsonToken.PropertyName:
			{
				string name;
				if ((name = (value as string)) == null)
				{
					throw new ArgumentException(JsonWriter.getString_0(107345615), JsonWriter.getString_0(107453219));
				}
				return this.InternalWritePropertyNameAsync(name, cancellationToken);
			}
			case JsonToken.Comment:
				return this.InternalWriteCommentAsync(cancellationToken);
			case JsonToken.Raw:
				return AsyncUtils.CompletedTask;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return this.InternalWriteValueAsync(token, cancellationToken);
			case JsonToken.EndObject:
				return this.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
			case JsonToken.EndArray:
				return this.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
			case JsonToken.EndConstructor:
				return this.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
			default:
				throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107345689));
			}
		}

		internal static Task WriteValueAsync(JsonWriter writer, PrimitiveTypeCode typeCode, object value, CancellationToken cancellationToken)
		{
			for (;;)
			{
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					goto IL_D4;
				case PrimitiveTypeCode.CharNullable:
					goto IL_E2;
				case PrimitiveTypeCode.Boolean:
					goto IL_103;
				case PrimitiveTypeCode.BooleanNullable:
					goto IL_111;
				case PrimitiveTypeCode.SByte:
					goto IL_132;
				case PrimitiveTypeCode.SByteNullable:
					goto IL_140;
				case PrimitiveTypeCode.Int16:
					goto IL_161;
				case PrimitiveTypeCode.Int16Nullable:
					goto IL_16F;
				case PrimitiveTypeCode.UInt16:
					goto IL_191;
				case PrimitiveTypeCode.UInt16Nullable:
					goto IL_19F;
				case PrimitiveTypeCode.Int32:
					goto IL_1C1;
				case PrimitiveTypeCode.Int32Nullable:
					goto IL_1CF;
				case PrimitiveTypeCode.Byte:
					goto IL_1F1;
				case PrimitiveTypeCode.ByteNullable:
					goto IL_1FF;
				case PrimitiveTypeCode.UInt32:
					goto IL_221;
				case PrimitiveTypeCode.UInt32Nullable:
					goto IL_22F;
				case PrimitiveTypeCode.Int64:
					goto IL_251;
				case PrimitiveTypeCode.Int64Nullable:
					goto IL_25F;
				case PrimitiveTypeCode.UInt64:
					goto IL_281;
				case PrimitiveTypeCode.UInt64Nullable:
					goto IL_28F;
				case PrimitiveTypeCode.Single:
					goto IL_2B1;
				case PrimitiveTypeCode.SingleNullable:
					goto IL_2BF;
				case PrimitiveTypeCode.Double:
					goto IL_2E1;
				case PrimitiveTypeCode.DoubleNullable:
					goto IL_2EF;
				case PrimitiveTypeCode.DateTime:
					goto IL_311;
				case PrimitiveTypeCode.DateTimeNullable:
					goto IL_31F;
				case PrimitiveTypeCode.DateTimeOffset:
					goto IL_341;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					goto IL_34F;
				case PrimitiveTypeCode.Decimal:
					goto IL_371;
				case PrimitiveTypeCode.DecimalNullable:
					goto IL_37F;
				case PrimitiveTypeCode.Guid:
					goto IL_3A1;
				case PrimitiveTypeCode.GuidNullable:
					goto IL_3AF;
				case PrimitiveTypeCode.TimeSpan:
					goto IL_3D1;
				case PrimitiveTypeCode.TimeSpanNullable:
					goto IL_3DF;
				case PrimitiveTypeCode.BigInteger:
					goto IL_401;
				case PrimitiveTypeCode.BigIntegerNullable:
					goto IL_414;
				case PrimitiveTypeCode.Uri:
					goto IL_43B;
				case PrimitiveTypeCode.String:
					goto IL_449;
				case PrimitiveTypeCode.Bytes:
					goto IL_457;
				case PrimitiveTypeCode.DBNull:
					goto IL_465;
				default:
				{
					IConvertible convertible;
					if ((convertible = (value as IConvertible)) == null)
					{
						goto IL_C1;
					}
					JsonWriter.ResolveConvertibleValue(convertible, out typeCode, out value);
					break;
				}
				}
			}
			IL_C1:
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
			IL_D4:
			return writer.WriteValueAsync((char)value, cancellationToken);
			IL_E2:
			return writer.WriteValueAsync((value == null) ? null : new char?((char)value), cancellationToken);
			IL_103:
			return writer.WriteValueAsync((bool)value, cancellationToken);
			IL_111:
			return writer.WriteValueAsync((value == null) ? null : new bool?((bool)value), cancellationToken);
			IL_132:
			return writer.WriteValueAsync((sbyte)value, cancellationToken);
			IL_140:
			return writer.WriteValueAsync((value == null) ? null : new sbyte?((sbyte)value), cancellationToken);
			IL_161:
			return writer.WriteValueAsync((short)value, cancellationToken);
			IL_16F:
			return writer.WriteValueAsync((value == null) ? null : new short?((short)value), cancellationToken);
			IL_191:
			return writer.WriteValueAsync((ushort)value, cancellationToken);
			IL_19F:
			return writer.WriteValueAsync((value == null) ? null : new ushort?((ushort)value), cancellationToken);
			IL_1C1:
			return writer.WriteValueAsync((int)value, cancellationToken);
			IL_1CF:
			return writer.WriteValueAsync((value == null) ? null : new int?((int)value), cancellationToken);
			IL_1F1:
			return writer.WriteValueAsync((byte)value, cancellationToken);
			IL_1FF:
			return writer.WriteValueAsync((value == null) ? null : new byte?((byte)value), cancellationToken);
			IL_221:
			return writer.WriteValueAsync((uint)value, cancellationToken);
			IL_22F:
			return writer.WriteValueAsync((value == null) ? null : new uint?((uint)value), cancellationToken);
			IL_251:
			return writer.WriteValueAsync((long)value, cancellationToken);
			IL_25F:
			return writer.WriteValueAsync((value == null) ? null : new long?((long)value), cancellationToken);
			IL_281:
			return writer.WriteValueAsync((ulong)value, cancellationToken);
			IL_28F:
			return writer.WriteValueAsync((value == null) ? null : new ulong?((ulong)value), cancellationToken);
			IL_2B1:
			return writer.WriteValueAsync((float)value, cancellationToken);
			IL_2BF:
			return writer.WriteValueAsync((value == null) ? null : new float?((float)value), cancellationToken);
			IL_2E1:
			return writer.WriteValueAsync((double)value, cancellationToken);
			IL_2EF:
			return writer.WriteValueAsync((value == null) ? null : new double?((double)value), cancellationToken);
			IL_311:
			return writer.WriteValueAsync((DateTime)value, cancellationToken);
			IL_31F:
			return writer.WriteValueAsync((value == null) ? null : new DateTime?((DateTime)value), cancellationToken);
			IL_341:
			return writer.WriteValueAsync((DateTimeOffset)value, cancellationToken);
			IL_34F:
			return writer.WriteValueAsync((value == null) ? null : new DateTimeOffset?((DateTimeOffset)value), cancellationToken);
			IL_371:
			return writer.WriteValueAsync((decimal)value, cancellationToken);
			IL_37F:
			return writer.WriteValueAsync((value == null) ? null : new decimal?((decimal)value), cancellationToken);
			IL_3A1:
			return writer.WriteValueAsync((Guid)value, cancellationToken);
			IL_3AF:
			return writer.WriteValueAsync((value == null) ? null : new Guid?((Guid)value), cancellationToken);
			IL_3D1:
			return writer.WriteValueAsync((TimeSpan)value, cancellationToken);
			IL_3DF:
			return writer.WriteValueAsync((value == null) ? null : new TimeSpan?((TimeSpan)value), cancellationToken);
			IL_401:
			return writer.WriteValueAsync((BigInteger)value, cancellationToken);
			IL_414:
			return writer.WriteValueAsync((value == null) ? null : new BigInteger?((BigInteger)value), cancellationToken);
			IL_43B:
			return writer.WriteValueAsync((Uri)value, cancellationToken);
			IL_449:
			return writer.WriteValueAsync((string)value, cancellationToken);
			IL_457:
			return writer.WriteValueAsync((byte[])value, cancellationToken);
			IL_465:
			return writer.WriteNullAsync(cancellationToken);
		}

		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = JsonWriter.StateArrayTempate.ToList<JsonWriter.State[]>();
			JsonWriter.State[] item = JsonWriter.StateArrayTempate[0];
			JsonWriter.State[] item2 = JsonWriter.StateArrayTempate[7];
			foreach (ulong num in EnumUtils.GetEnumValuesAndNames(typeof(JsonToken)).Values)
			{
				if (list.Count <= (int)num)
				{
					JsonToken jsonToken = (JsonToken)num;
					if (jsonToken - JsonToken.Integer > 5 && jsonToken - JsonToken.Date > 1)
					{
						list.Add(item);
					}
					else
					{
						list.Add(item2);
					}
				}
			}
			return list.ToArray();
		}

		static JsonWriter()
		{
			Strings.CreateGetStringDelegate(typeof(JsonWriter));
			JsonWriter.StateArrayTempate = new JsonWriter.State[][]
			{
				new JsonWriter.State[]
				{
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.ObjectStart,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.ArrayStart,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.ConstructorStart,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.Property,
					JsonWriter.State.Error,
					JsonWriter.State.Property,
					JsonWriter.State.Property,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.Start,
					JsonWriter.State.Property,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.Object,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.Array,
					JsonWriter.State.Constructor,
					JsonWriter.State.Constructor,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.Start,
					JsonWriter.State.Property,
					JsonWriter.State.ObjectStart,
					JsonWriter.State.Object,
					JsonWriter.State.ArrayStart,
					JsonWriter.State.Array,
					JsonWriter.State.Constructor,
					JsonWriter.State.Constructor,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				},
				new JsonWriter.State[]
				{
					JsonWriter.State.Start,
					JsonWriter.State.Object,
					JsonWriter.State.Error,
					JsonWriter.State.Error,
					JsonWriter.State.Array,
					JsonWriter.State.Array,
					JsonWriter.State.Constructor,
					JsonWriter.State.Constructor,
					JsonWriter.State.Error,
					JsonWriter.State.Error
				}
			};
			JsonWriter.StateArray = JsonWriter.BuildStateArray();
		}

		public bool CloseOutput { get; set; }

		public bool AutoCompleteOnClose { get; set; }

		protected internal int Top
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, JsonWriter.getString_0(107345542) + this._currentState, null);
				}
			}
		}

		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type != JsonContainerType.None && this._stack != null)
				{
					return JsonPosition.BuildPath(this._stack, null);
				}
				return string.Empty;
			}
		}

		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null;
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				if (value < Formatting.None || value > Formatting.Indented)
				{
					throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107453219));
				}
				this._formatting = value;
			}
		}

		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				if (value < DateFormatHandling.IsoDateFormat || value > DateFormatHandling.MicrosoftDateFormat)
				{
					throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107453219));
				}
				this._dateFormatHandling = value;
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
					throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107453219));
				}
				this._dateTimeZoneHandling = value;
			}
		}

		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				if (value < StringEscapeHandling.Default || value > StringEscapeHandling.EscapeHtml)
				{
					throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107453219));
				}
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				if (value < FloatFormatHandling.String || value > FloatFormatHandling.DefaultValue)
				{
					throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107453219));
				}
				this._floatFormatHandling = value;
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

		protected JsonWriter()
		{
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
			this.AutoCompleteOnClose = true;
		}

		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				if (this._stack == null)
				{
					this._stack = new List<JsonPosition>();
				}
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		private JsonContainerType Pop()
		{
			ref JsonPosition currentPosition = this._currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		public abstract void Flush();

		public virtual void Close()
		{
			if (this.AutoCompleteOnClose)
			{
				this.AutoCompleteAll();
			}
		}

		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true);
		}

		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonWriter.getString_0(107246669));
			this.WriteToken(reader, writeChildren, true, true);
		}

		public void WriteToken(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
				this.WriteStartObject();
				return;
			case JsonToken.StartArray:
				this.WriteStartArray();
				return;
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				this.WriteStartConstructor(value.ToString());
				return;
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				this.WritePropertyName(value.ToString());
				return;
			case JsonToken.Comment:
				this.WriteComment((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Raw:
				this.WriteRawValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					this.WriteValue(bigInteger);
					return;
				}
				this.WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is decimal)
				{
					decimal value2 = (decimal)value;
					this.WriteValue(value2);
					return;
				}
				if (value is double)
				{
					double value3 = (double)value;
					this.WriteValue(value3);
					return;
				}
				if (value is float)
				{
					float value4 = (float)value;
					this.WriteValue(value4);
					return;
				}
				this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				this.WriteValue(value.ToString());
				return;
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				this.WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Null:
				this.WriteNull();
				return;
			case JsonToken.Undefined:
				this.WriteUndefined();
				return;
			case JsonToken.EndObject:
				this.WriteEndObject();
				return;
			case JsonToken.EndArray:
				this.WriteEndArray();
				return;
			case JsonToken.EndConstructor:
				this.WriteEndConstructor();
				return;
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is DateTimeOffset)
				{
					DateTimeOffset value5 = (DateTimeOffset)value;
					this.WriteValue(value5);
					return;
				}
				this.WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, JsonWriter.getString_0(107453219));
				if (value is Guid)
				{
					Guid value6 = (Guid)value;
					this.WriteValue(value6);
					return;
				}
				this.WriteValue((byte[])value);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JsonWriter.getString_0(107345689), token, JsonWriter.getString_0(107345648));
			}
		}

		public void WriteToken(JsonToken token)
		{
			this.WriteToken(token, null);
		}

		internal virtual void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = this.CalculateWriteTokenInitialDepth(reader);
			do
			{
				if (writeDateConstructorAsDate && reader.TokenType == JsonToken.StartConstructor && string.Equals(reader.Value.ToString(), JsonWriter.getString_0(107345521), StringComparison.Ordinal))
				{
					this.WriteConstructorDate(reader);
				}
				else if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					this.WriteToken(reader.TokenType, reader.Value);
				}
				if (num - 1 >= reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) || !writeChildren)
				{
					break;
				}
			}
			while (reader.Read());
			if (num < this.CalculateWriteTokenFinalDepth(reader))
			{
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107345512), null);
			}
		}

		private int CalculateWriteTokenInitialDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		private int CalculateWriteTokenFinalDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsEndToken(tokenType))
			{
				return reader.Depth;
			}
			return reader.Depth - 1;
		}

		private void WriteConstructorDate(JsonReader reader)
		{
			DateTime value;
			string message;
			if (!JavaScriptUtils.TryGetDateFromConstructorJson(reader, out value, out message))
			{
				throw JsonWriterException.Create(this, message, null);
			}
			this.WriteValue(value);
		}

		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107346218) + type, null);
			}
		}

		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107345495) + type, null);
			}
		}

		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = this.CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				this.UpdateCurrentState();
			}
		}

		private int CalculateLevelsToComplete(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int index = num2 - i;
					if (this._stack[index].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107345938), null);
			}
			return num;
		}

		private void UpdateCurrentState()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107345945) + jsonContainerType, null);
			}
		}

		protected virtual void WriteEnd(JsonToken token)
		{
		}

		protected virtual void WriteIndent()
		{
		}

		protected virtual void WriteValueDelimiter()
		{
		}

		protected virtual void WriteIndentSpace()
		{
		}

		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107346335).FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is BigInteger)
			{
				throw JsonWriter.CreateUnsupportedTypeException(this, value);
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed && disposing)
			{
				this.Close();
			}
		}

		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			for (;;)
			{
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					goto IL_D3;
				case PrimitiveTypeCode.CharNullable:
					goto IL_E0;
				case PrimitiveTypeCode.Boolean:
					goto IL_100;
				case PrimitiveTypeCode.BooleanNullable:
					goto IL_10D;
				case PrimitiveTypeCode.SByte:
					goto IL_12D;
				case PrimitiveTypeCode.SByteNullable:
					goto IL_13A;
				case PrimitiveTypeCode.Int16:
					goto IL_15A;
				case PrimitiveTypeCode.Int16Nullable:
					goto IL_167;
				case PrimitiveTypeCode.UInt16:
					goto IL_188;
				case PrimitiveTypeCode.UInt16Nullable:
					goto IL_195;
				case PrimitiveTypeCode.Int32:
					goto IL_1B6;
				case PrimitiveTypeCode.Int32Nullable:
					goto IL_1C3;
				case PrimitiveTypeCode.Byte:
					goto IL_1E4;
				case PrimitiveTypeCode.ByteNullable:
					goto IL_1F1;
				case PrimitiveTypeCode.UInt32:
					goto IL_212;
				case PrimitiveTypeCode.UInt32Nullable:
					goto IL_21F;
				case PrimitiveTypeCode.Int64:
					goto IL_240;
				case PrimitiveTypeCode.Int64Nullable:
					goto IL_24D;
				case PrimitiveTypeCode.UInt64:
					goto IL_26E;
				case PrimitiveTypeCode.UInt64Nullable:
					goto IL_27B;
				case PrimitiveTypeCode.Single:
					goto IL_29C;
				case PrimitiveTypeCode.SingleNullable:
					goto IL_2A9;
				case PrimitiveTypeCode.Double:
					goto IL_2CA;
				case PrimitiveTypeCode.DoubleNullable:
					goto IL_2D7;
				case PrimitiveTypeCode.DateTime:
					goto IL_2F8;
				case PrimitiveTypeCode.DateTimeNullable:
					goto IL_305;
				case PrimitiveTypeCode.DateTimeOffset:
					goto IL_326;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					goto IL_333;
				case PrimitiveTypeCode.Decimal:
					goto IL_354;
				case PrimitiveTypeCode.DecimalNullable:
					goto IL_361;
				case PrimitiveTypeCode.Guid:
					goto IL_382;
				case PrimitiveTypeCode.GuidNullable:
					goto IL_38F;
				case PrimitiveTypeCode.TimeSpan:
					goto IL_3B0;
				case PrimitiveTypeCode.TimeSpanNullable:
					goto IL_3BD;
				case PrimitiveTypeCode.BigInteger:
					goto IL_3DE;
				case PrimitiveTypeCode.BigIntegerNullable:
					goto IL_3F0;
				case PrimitiveTypeCode.Uri:
					goto IL_416;
				case PrimitiveTypeCode.String:
					goto IL_423;
				case PrimitiveTypeCode.Bytes:
					goto IL_430;
				case PrimitiveTypeCode.DBNull:
					goto IL_43D;
				default:
				{
					IConvertible convertible;
					if ((convertible = (value as IConvertible)) == null)
					{
						goto IL_C1;
					}
					JsonWriter.ResolveConvertibleValue(convertible, out typeCode, out value);
					break;
				}
				}
			}
			IL_C1:
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
			IL_D3:
			writer.WriteValue((char)value);
			return;
			IL_E0:
			writer.WriteValue((value == null) ? null : new char?((char)value));
			return;
			IL_100:
			writer.WriteValue((bool)value);
			return;
			IL_10D:
			writer.WriteValue((value == null) ? null : new bool?((bool)value));
			return;
			IL_12D:
			writer.WriteValue((sbyte)value);
			return;
			IL_13A:
			writer.WriteValue((value == null) ? null : new sbyte?((sbyte)value));
			return;
			IL_15A:
			writer.WriteValue((short)value);
			return;
			IL_167:
			writer.WriteValue((value == null) ? null : new short?((short)value));
			return;
			IL_188:
			writer.WriteValue((ushort)value);
			return;
			IL_195:
			writer.WriteValue((value == null) ? null : new ushort?((ushort)value));
			return;
			IL_1B6:
			writer.WriteValue((int)value);
			return;
			IL_1C3:
			writer.WriteValue((value == null) ? null : new int?((int)value));
			return;
			IL_1E4:
			writer.WriteValue((byte)value);
			return;
			IL_1F1:
			writer.WriteValue((value == null) ? null : new byte?((byte)value));
			return;
			IL_212:
			writer.WriteValue((uint)value);
			return;
			IL_21F:
			writer.WriteValue((value == null) ? null : new uint?((uint)value));
			return;
			IL_240:
			writer.WriteValue((long)value);
			return;
			IL_24D:
			writer.WriteValue((value == null) ? null : new long?((long)value));
			return;
			IL_26E:
			writer.WriteValue((ulong)value);
			return;
			IL_27B:
			writer.WriteValue((value == null) ? null : new ulong?((ulong)value));
			return;
			IL_29C:
			writer.WriteValue((float)value);
			return;
			IL_2A9:
			writer.WriteValue((value == null) ? null : new float?((float)value));
			return;
			IL_2CA:
			writer.WriteValue((double)value);
			return;
			IL_2D7:
			writer.WriteValue((value == null) ? null : new double?((double)value));
			return;
			IL_2F8:
			writer.WriteValue((DateTime)value);
			return;
			IL_305:
			writer.WriteValue((value == null) ? null : new DateTime?((DateTime)value));
			return;
			IL_326:
			writer.WriteValue((DateTimeOffset)value);
			return;
			IL_333:
			writer.WriteValue((value == null) ? null : new DateTimeOffset?((DateTimeOffset)value));
			return;
			IL_354:
			writer.WriteValue((decimal)value);
			return;
			IL_361:
			writer.WriteValue((value == null) ? null : new decimal?((decimal)value));
			return;
			IL_382:
			writer.WriteValue((Guid)value);
			return;
			IL_38F:
			writer.WriteValue((value == null) ? null : new Guid?((Guid)value));
			return;
			IL_3B0:
			writer.WriteValue((TimeSpan)value);
			return;
			IL_3BD:
			writer.WriteValue((value == null) ? null : new TimeSpan?((TimeSpan)value));
			return;
			IL_3DE:
			writer.WriteValue((BigInteger)value);
			return;
			IL_3F0:
			writer.WriteValue((value == null) ? null : new BigInteger?((BigInteger)value));
			return;
			IL_416:
			writer.WriteValue((Uri)value);
			return;
			IL_423:
			writer.WriteValue((string)value);
			return;
			IL_430:
			writer.WriteValue((byte[])value);
			return;
			IL_43D:
			writer.WriteNull();
		}

		private static void ResolveConvertibleValue(IConvertible convertible, out PrimitiveTypeCode typeCode, out object value)
		{
			TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertible);
			typeCode = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode);
			Type conversionType = (typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type;
			value = convertible.ToType(conversionType, CultureInfo.InvariantCulture);
		}

		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, JsonWriter.getString_0(107352564).FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
			{
				string name;
				if ((name = (value as string)) == null)
				{
					throw new ArgumentException(JsonWriter.getString_0(107345615), JsonWriter.getString_0(107453219));
				}
				this.InternalWritePropertyName(name);
				return;
			}
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException(JsonWriter.getString_0(107345689));
			}
		}

		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		internal void InternalWriteRaw()
		{
		}

		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, JsonWriter.getString_0(107345920), null);
			}
		}

		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		[CompilerGenerated]
		private async Task method_0(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			await task.ConfigureAwait(false);
			if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
			{
				await this.WriteIndentAsync(CancellationToken).ConfigureAwait(false);
			}
			await this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false);
			this.UpdateCurrentState();
			await this.method_3(LevelsToComplete, CancellationToken).ConfigureAwait(false);
		}

		[CompilerGenerated]
		private async Task method_1(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false);
			this.UpdateCurrentState();
			await this.method_3(LevelsToComplete, CancellationToken).ConfigureAwait(false);
		}

		[CompilerGenerated]
		private async Task method_2(Task task, int LevelsToComplete, CancellationToken CancellationToken)
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
			this.UpdateCurrentState();
			configuredTaskAwaiter = this.method_3(LevelsToComplete, CancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		[CompilerGenerated]
		private async Task method_3(int LevelsToComplete, CancellationToken CancellationToken)
		{
			while (LevelsToComplete-- > 0)
			{
				JsonToken token = this.GetCloseTokenForType(this.Pop());
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				if (this._currentState == JsonWriter.State.Property)
				{
					configuredTaskAwaiter = this.WriteNullAsync(CancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					configuredTaskAwaiter = this.WriteIndentAsync(CancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				configuredTaskAwaiter = this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				this.UpdateCurrentState();
			}
		}

		private static readonly JsonWriter.State[][] StateArray;

		internal static readonly JsonWriter.State[][] StateArrayTempate;

		private List<JsonPosition> _stack;

		private JsonPosition _currentPosition;

		private JsonWriter.State _currentState;

		private Formatting _formatting;

		private DateFormatHandling _dateFormatHandling;

		private DateTimeZoneHandling _dateTimeZoneHandling;

		private StringEscapeHandling _stringEscapeHandling;

		private FloatFormatHandling _floatFormatHandling;

		private string _dateFormatString;

		private CultureInfo _culture;

		[NonSerialized]
		internal static GetString getString_0;

		internal enum State
		{
			Start,
			Property,
			ObjectStart,
			Object,
			ArrayStart,
			Array,
			ConstructorStart,
			Constructor,
			Closed,
			Error
		}
	}
}
