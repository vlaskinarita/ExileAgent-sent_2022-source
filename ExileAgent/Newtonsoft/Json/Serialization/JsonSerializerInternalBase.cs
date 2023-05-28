using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Serialization
{
	internal abstract class JsonSerializerInternalBase
	{
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, JsonSerializerInternalBase.getString_0(107338856));
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
		}

		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), JsonSerializerInternalBase.getString_0(107338871), JsonSerializerInternalBase.getString_0(107338810));
				}
				return this._mappings;
			}
		}

		protected NullValueHandling ResolvedNullValueHandling(JsonObjectContract containerContract, JsonProperty property)
		{
			NullValueHandling? nullValueHandling = property.NullValueHandling;
			if (nullValueHandling != null)
			{
				return nullValueHandling.GetValueOrDefault();
			}
			NullValueHandling? nullValueHandling2 = (containerContract != null) ? containerContract.ItemNullValueHandling : null;
			if (nullValueHandling2 == null)
			{
				return this.Serializer._nullValueHandling;
			}
			return nullValueHandling2.GetValueOrDefault();
		}

		private ErrorContext GetErrorContext(object currentObject, object member, string path, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, path, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException(JsonSerializerInternalBase.getString_0(107338524));
			}
			return this._currentErrorContext;
		}

		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException(JsonSerializerInternalBase.getString_0(107338411));
			}
			this._currentErrorContext = null;
		}

		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, string path, Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = (base.GetType() == typeof(JsonSerializerInternalWriter)) ? JsonSerializerInternalBase.getString_0(107338329) : JsonSerializerInternalBase.getString_0(107338358);
				if (contract != null)
				{
					text = text + JsonSerializerInternalBase.getString_0(107401678) + contract.UnderlyingType;
				}
				text = text + JsonSerializerInternalBase.getString_0(107338272) + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(TraceLevel.Error, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		static JsonSerializerInternalBase()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSerializerInternalBase));
		}

		private ErrorContext _currentErrorContext;

		private BidirectionalDictionary<string, object> _mappings;

		internal readonly JsonSerializer Serializer;

		internal readonly ITraceWriter TraceWriter;

		protected JsonSerializerProxy InternalSerializer;

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}
	}
}
