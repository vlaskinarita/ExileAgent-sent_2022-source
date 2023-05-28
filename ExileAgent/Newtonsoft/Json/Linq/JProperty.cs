using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JProperty : JContainer
	{
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
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
			configuredTaskAwaiter = this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		public new static async Task<JProperty> LoadAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, JProperty.<LoadAsync>d__4.getString_0(107293168));
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, JProperty.<LoadAsync>d__4.getString_0(107292599).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty p = new JProperty((string)reader.Value);
			p.SetLineInfo(reader as IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return p;
		}

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken item = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, item, false);
					return;
				}
				this.SetItem(0, item);
			}
		}

		public JProperty(JProperty other) : base(other)
		{
			this._name = other.Name;
		}

		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException(JProperty.getString_2(107293241).FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override void RemoveItemAt(int index)
		{
			throw new JsonException(JProperty.getString_2(107293241).FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override int IndexOfItem(JToken item)
		{
			return this._content.IndexOf(item);
		}

		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			if (this.Value != null)
			{
				throw new JsonException(JProperty.getString_2(107293160).FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			base.InsertItem(0, item, false);
		}

		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = (jproperty != null) ? jproperty.Value : null;
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		internal override void ClearItems()
		{
			throw new JsonException(JProperty.getString_2(107293241).FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty;
			return (jproperty = (node as JProperty)) != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, JProperty.getString_2(107374736));
			this._name = name;
		}

		public JProperty(string name, params object[] content) : this(name, content)
		{
		}

		public JProperty(string name, object content)
		{
			ValidationUtils.ArgumentNotNull(name, JProperty.getString_2(107374736));
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		public new static JProperty Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, JProperty.getString_2(107293147));
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, JProperty.getString_2(107292578).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		static JProperty()
		{
			Strings.CreateGetStringDelegate(typeof(JProperty));
		}

		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		private readonly string _name;

		[NonSerialized]
		internal static GetString getString_2;

		private sealed class JPropertyList : IEnumerable, IEnumerable<JToken>, IList<JToken>, ICollection<JToken>
		{
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			public void Add(JToken item)
			{
				this._token = item;
			}

			public void Clear()
			{
				this._token = null;
			}

			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						return null;
					}
					return this._token;
				}
				set
				{
					if (index == 0)
					{
						this._token = value;
					}
				}
			}

			internal JToken _token;
		}
	}
}
