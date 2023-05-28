using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JArray : JContainer, IEnumerable, IEnumerable<JToken>, IList<JToken>, ICollection<JToken>
	{
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false);
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			for (int i = 0; i < this._values.Count; i++)
			{
				configuredTaskAwaiter = this._values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			configuredTaskAwaiter = writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		public new static async Task<JArray> LoadAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, JArray.<LoadAsync>d__2.getString_0(107295801));
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, JArray.<LoadAsync>d__2.getString_0(107295780).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray a = new JArray();
			a.SetLineInfo(reader as IJsonLineInfo, settings);
			await a.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return a;
		}

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		public JArray()
		{
		}

		public JArray(JArray other) : base(other)
		{
		}

		public JArray(params object[] content) : this(content)
		{
		}

		public JArray(object content)
		{
			this.Add(content);
		}

		internal override bool DeepEquals(JToken node)
		{
			JArray container;
			return (container = (node as JArray)) != null && base.ContentsEqual(container);
		}

		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		public new static JArray Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, JArray.getString_2(107295784));
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, JArray.getString_2(107295763).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		public new static JArray Parse(string json, JsonLoadSettings settings)
		{
			JArray result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jarray;
			}
			return result;
		}

		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException(JArray.getString_2(107295618).FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, JArray.getString_2(107454058));
				if (!(key is int))
				{
					throw new ArgumentException(JArray.getString_2(107295037).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, JArray.getString_2(107454058));
				if (!(key is int))
				{
					throw new ArgumentException(JArray.getString_2(107294960).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		internal override int IndexOfItem(JToken item)
		{
			return this._values.IndexOfReference(item);
		}

		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			IEnumerable enumerable = (base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null;
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		public void Add(JToken item)
		{
			this.Add(item);
		}

		public void Clear()
		{
			this.ClearItems();
		}

		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		static JArray()
		{
			Strings.CreateGetStringDelegate(typeof(JArray));
		}

		private readonly List<JToken> _values = new List<JToken>();

		[NonSerialized]
		internal static GetString getString_2;
	}
}
