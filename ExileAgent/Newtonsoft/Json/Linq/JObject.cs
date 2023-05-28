using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JObject : JContainer, IEnumerable, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertyChanging
	{
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WriteStartObjectAsync(cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return this.method_0(task, 0, writer, cancellationToken, converters);
			}
			for (int i = 0; i < this._properties.Count; i++)
			{
				task = this._properties[i].WriteToAsync(writer, cancellationToken, converters);
				if (!task.IsCompletedSucessfully())
				{
					return this.method_0(task, i + 1, writer, cancellationToken, converters);
				}
			}
			return writer.WriteEndObjectAsync(cancellationToken);
		}

		public new static Task<JObject> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JObject.LoadAsync(reader, null, cancellationToken);
		}

		public new static async Task<JObject> LoadAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, JObject.<LoadAsync>d__2.getString_0(107247833));
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
					throw JsonReaderException.Create(reader, JObject.<LoadAsync>d__2.getString_0(107292918));
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, JObject.<LoadAsync>d__2.getString_0(107292865).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject o = new JObject();
			o.SetLineInfo(reader as IJsonLineInfo, settings);
			await o.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return o;
		}

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;

		public JObject()
		{
		}

		public JObject(JObject other) : base(other)
		{
		}

		public JObject(params object[] content) : this(content)
		{
		}

		public JObject(object content)
		{
			this.Add(content);
		}

		internal override bool DeepEquals(JToken node)
		{
			JObject jobject;
			return (jobject = (node as JObject)) != null && this._properties.Compare(jobject._properties);
		}

		internal override int IndexOfItem(JToken item)
		{
			return this._properties.IndexOfReference(item);
		}

		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item, skipParentCheck);
		}

		internal override void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, JObject.getString_2(107293887));
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException(JObject.getString_2(107293882).FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException(JObject.getString_2(107293771).FormatWith(CultureInfo.InvariantCulture, jproperty.Name, base.GetType()));
			}
		}

		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JObject jobject;
			if ((jobject = (content as JObject)) == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
			{
				JProperty jproperty = this.Property(keyValuePair.Key, (settings != null) ? settings.PropertyNameComparison : StringComparison.Ordinal);
				if (jproperty == null)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (keyValuePair.Value != null)
				{
					JContainer jcontainer;
					if ((jcontainer = (jproperty.Value as JContainer)) != null)
					{
						if (jcontainer.Type == keyValuePair.Value.Type)
						{
							jcontainer.Merge(keyValuePair.Value, settings);
							continue;
						}
					}
					if (!JObject.IsNull(keyValuePair.Value) || (settings != null && settings.MergeNullValueHandling == MergeNullValueHandling.Merge))
					{
						jproperty.Value = keyValuePair.Value;
					}
				}
			}
		}

		private static bool IsNull(JToken token)
		{
			JValue jvalue;
			return token.Type == JTokenType.Null || ((jvalue = (token as JValue)) != null && jvalue.Value == null);
		}

		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOfItem(childProperty)));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, childProperty, childProperty, this.IndexOfItem(childProperty)));
			}
		}

		internal void InternalPropertyChanging(JProperty childProperty)
		{
			this.OnPropertyChanging(childProperty.Name);
		}

		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		public IEnumerable<JProperty> Properties()
		{
			return this._properties.Cast<JProperty>();
		}

		public JProperty Property(string name)
		{
			return this.Property(name, StringComparison.Ordinal);
		}

		public JProperty Property(string name, StringComparison comparison)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			if (this._properties.TryGetValue(name, out jtoken))
			{
				return (JProperty)jtoken;
			}
			if (comparison != StringComparison.Ordinal)
			{
				for (int i = 0; i < this._properties.Count; i++)
				{
					JProperty jproperty = (JProperty)this._properties[i];
					if (string.Equals(jproperty.Name, name, comparison))
					{
						return jproperty;
					}
				}
			}
			return null;
		}

		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in this.Properties()
			select p.Value);
		}

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, JObject.getString_2(107454101));
				string propertyName;
				if ((propertyName = (key as string)) == null)
				{
					throw new ArgumentException(JObject.getString_2(107293654).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[propertyName];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, JObject.getString_2(107454101));
				string propertyName;
				if ((propertyName = (key as string)) == null)
				{
					throw new ArgumentException(JObject.getString_2(107293061).FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[propertyName] = value;
			}
		}

		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, JObject.getString_2(107292956));
				JProperty jproperty = this.Property(propertyName);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			set
			{
				JProperty jproperty = this.Property(propertyName);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.OnPropertyChanging(propertyName);
				this.Add(new JProperty(propertyName, value));
				this.OnPropertyChanged(propertyName);
			}
		}

		public new static JObject Load(JsonReader reader)
		{
			return JObject.Load(reader, null);
		}

		public new static JObject Load(JsonReader reader, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, JObject.getString_2(107247822));
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, JObject.getString_2(107292907));
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, JObject.getString_2(107292854).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo, settings);
			jobject.ReadTokenFrom(reader, settings);
			return jobject;
		}

		public new static JObject Parse(string json)
		{
			return JObject.Parse(json, null);
		}

		public new static JObject Parse(string json, JsonLoadSettings settings)
		{
			JObject result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JObject jobject = JObject.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jobject;
			}
			return result;
		}

		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken != null && jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException(JObject.getString_2(107293285).FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, StringComparison.Ordinal);
		}

		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName, comparison);
			if (jproperty == null)
			{
				return null;
			}
			return jproperty.Value;
		}

		public bool TryGetValue(string propertyName, StringComparison comparison, out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		public void Add(string propertyName, JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		public bool ContainsKey(string propertyName)
		{
			ValidationUtils.ArgumentNotNull(propertyName, JObject.getString_2(107292956));
			return this._properties.Contains(propertyName);
		}

		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key);
			return jproperty != null && jproperty.Value == item.Value;
		}

		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException(JObject.getString_2(107296305));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException(JObject.getString_2(107294716), JObject.getString_2(107294667));
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException(JObject.getString_2(107294662));
			}
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException(JObject.getString_2(107294037));
			}
			int num = 0;
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			JObject.<GetEnumerator>d__63 <GetEnumerator>d__ = new JObject.<GetEnumerator>d__63(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanging(string propertyName)
		{
			PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				propertyDescriptorCollection.Add(new JPropertyDescriptor(keyValuePair.Key));
			}
			return propertyDescriptorCollection;
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is JPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy());
		}

		[CompilerGenerated]
		private async Task method_0(Task task, int i, JsonWriter Writer, CancellationToken CancellationToken, JsonConverter[] Converters)
		{
			await task.ConfigureAwait(false);
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			while (i < this._properties.Count)
			{
				configuredTaskAwaiter = this._properties[i].WriteToAsync(Writer, CancellationToken, Converters).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				i++;
			}
			configuredTaskAwaiter = Writer.WriteEndObjectAsync(CancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		static JObject()
		{
			Strings.CreateGetStringDelegate(typeof(JObject));
		}

		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		[NonSerialized]
		internal static GetString getString_2;

		private sealed class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			public override bool TrySetMember(JObject instance, SetMemberBinder binder, object value)
			{
				JToken value2 = (value as JToken) ?? new JValue(value);
				instance[binder.Name] = value2;
				return true;
			}

			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return from p in instance.Properties()
				select p.Name;
			}
		}
	}
}
