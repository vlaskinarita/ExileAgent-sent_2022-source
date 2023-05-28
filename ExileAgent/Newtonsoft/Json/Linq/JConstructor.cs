using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JConstructor : JContainer
	{
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartConstructorAsync(this._name, cancellationToken).ConfigureAwait(false);
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
			configuredTaskAwaiter = writer.WriteEndConstructorAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		public new static Task<JConstructor> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JConstructor.LoadAsync(reader, null, cancellationToken);
		}

		public new static async Task<JConstructor> LoadAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, JConstructor.<LoadAsync>d__2.getString_0(107294609));
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, JConstructor.<LoadAsync>d__2.getString_0(107294548).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor c = new JConstructor((string)reader.Value);
			c.SetLineInfo(reader as IJsonLineInfo, settings);
			await c.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return c;
		}

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		internal override int IndexOfItem(JToken item)
		{
			return this._values.IndexOfReference(item);
		}

		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JConstructor jconstructor;
			if ((jconstructor = (content as JConstructor)) == null)
			{
				return;
			}
			if (jconstructor.Name != null)
			{
				this.Name = jconstructor.Name;
			}
			JContainer.MergeEnumerableContent(this, jconstructor, settings);
		}

		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		public JConstructor()
		{
		}

		public JConstructor(JConstructor other) : base(other)
		{
			this._name = other.Name;
		}

		public JConstructor(string name, params object[] content) : this(name, content)
		{
		}

		public JConstructor(string name, object content) : this(name)
		{
			this.Add(content);
		}

		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(JConstructor.getString_2(107374684));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(JConstructor.getString_2(107295359), JConstructor.getString_2(107374684));
			}
			this._name = name;
		}

		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor;
			return (jconstructor = (node as JConstructor)) != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		internal override JToken CloneToken()
		{
			return new JConstructor(this);
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			int count = this._values.Count;
			for (int i = 0; i < count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, JConstructor.getString_2(107454078));
				if (!(key is int))
				{
					throw new ArgumentException(JConstructor.getString_2(107295314).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				int index = (int)key;
				return this.GetItem(index);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, JConstructor.getString_2(107454078));
				if (!(key is int))
				{
					throw new ArgumentException(JConstructor.getString_2(107295189).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				int index = (int)key;
				this.SetItem(index, value);
			}
		}

		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ base.ContentsHashCode();
		}

		public new static JConstructor Load(JsonReader reader)
		{
			return JConstructor.Load(reader, null);
		}

		public new static JConstructor Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, JConstructor.getString_2(107294592));
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, JConstructor.getString_2(107294531).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo, settings);
			jconstructor.ReadTokenFrom(reader, settings);
			return jconstructor;
		}

		static JConstructor()
		{
			Strings.CreateGetStringDelegate(typeof(JConstructor));
		}

		private string _name;

		private readonly List<JToken> _values = new List<JToken>();

		[NonSerialized]
		internal static GetString getString_2;
	}
}
