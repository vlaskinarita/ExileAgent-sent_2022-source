using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public abstract class JContainer : JToken, IEnumerable, IEnumerable<JToken>, IList<JToken>, ICollection<JToken>, IList, ICollection, ITypedList, IBindingList, INotifyCollectionChanged
	{
		internal async Task ReadTokenFromAsync(JsonReader reader, JsonLoadSettings options, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, JContainer.<ReadTokenFromAsync>d__0.getString_0(107247757));
			int startDepth = reader.Depth;
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
				throw JsonReaderException.Create(reader, JContainer.<ReadTokenFromAsync>d__0.getString_0(107294296).FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadContentFromAsync(reader, options, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			if (reader.Depth > startDepth)
			{
				throw JsonReaderException.Create(reader, JContainer.<ReadTokenFromAsync>d__0.getString_0(107294215).FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		private async Task ReadContentFromAsync(JsonReader reader, JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			JContainer parent = this;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			do
			{
				JProperty jproperty;
				if ((jproperty = (parent as JProperty)) != null && jproperty.Value != null)
				{
					if (parent == this)
					{
						break;
					}
					parent = parent.Parent;
				}
				switch (reader.TokenType)
				{
				case JsonToken.None:
					goto IL_335;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					parent.Add(jobject);
					parent = jobject;
					goto IL_335;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					parent.Add(jarray);
					parent = jarray;
					goto IL_335;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(reader.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					parent.Add(jconstructor);
					parent = jconstructor;
					goto IL_335;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(reader, settings, lineInfo, parent);
					if (jproperty2 != null)
					{
						parent = jproperty2;
						goto IL_335;
					}
					TaskAwaiter taskAwaiter = reader.SkipAsync(default(CancellationToken)).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
					}
					taskAwaiter.GetResult();
					goto IL_335;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(reader.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						parent.Add(jvalue);
						goto IL_335;
					}
					goto IL_335;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(reader.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_335;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_335;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_335;
				}
				case JsonToken.EndObject:
					if (parent == this)
					{
						goto Block_8;
					}
					parent = parent.Parent;
					goto IL_335;
				case JsonToken.EndArray:
					if (parent == this)
					{
						goto Block_9;
					}
					parent = parent.Parent;
					goto IL_335;
				case JsonToken.EndConstructor:
					if (parent == this)
					{
						goto Block_10;
					}
					parent = parent.Parent;
					goto IL_335;
				}
				goto Block_4;
				IL_335:
				configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
			}
			while (configuredTaskAwaiter.GetResult());
			return;
			Block_4:
			goto IL_3DC;
			Block_8:
			Block_9:
			Block_10:
			return;
			IL_3DC:
			throw new InvalidOperationException(JContainer.<ReadContentFromAsync>d__1.getString_0(107294192).FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Combine(this._listChanged, value);
			}
			remove
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Remove(this._listChanged, value);
			}
		}

		public event AddingNewEventHandler AddingNew
		{
			add
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Combine(this._addingNew, value);
			}
			remove
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Remove(this._addingNew, value);
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		protected abstract IList<JToken> ChildrenTokens { get; }

		internal JContainer()
		{
		}

		internal JContainer(JContainer other) : this()
		{
			ValidationUtils.ArgumentNotNull(other, JContainer.getString_1(107294354));
			int num = 0;
			foreach (JToken content in ((IEnumerable<JToken>)other))
			{
				this.AddInternal(num, content, false);
				num++;
			}
		}

		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException(JContainer.getString_1(107294313).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = this._addingNew;
			if (addingNew == null)
			{
				return;
			}
			addingNew(this, e);
		}

		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = this._listChanged;
			if (listChanged != null)
			{
				this._busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this._collectionChanged;
			if (collectionChanged != null)
			{
				this._busy = true;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = this.ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		public override JToken First
		{
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		public override JToken Last
		{
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		public IEnumerable<JToken> Descendants()
		{
			return this.GetDescendants(false);
		}

		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return this.GetDescendants(true);
		}

		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			JContainer.<GetDescendants>d__36 <GetDescendants>d__ = new JContainer.<GetDescendants>d__36(-2);
			<GetDescendants>d__.<>4__this = this;
			<GetDescendants>d__.<>3__self = self;
			return <GetDescendants>d__;
		}

		internal bool IsMultiContent(object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		internal JToken EnsureParentToken(JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		internal abstract int IndexOfItem(JToken item);

		internal virtual void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107346928), JContainer.getString_1(107294756));
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck);
			JToken jtoken = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken2 = (index == childrenTokens.Count) ? null : childrenTokens[index];
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			}
		}

		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107346928), JContainer.getString_1(107294695));
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107346928), JContainer.getString_1(107294666));
			}
			this.CheckReentrancy();
			JToken jtoken = childrenTokens[index];
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			childrenTokens.RemoveAt(index);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, jtoken, index));
			}
		}

		internal virtual bool RemoveItem(JToken item)
		{
			int num = this.IndexOfItem(item);
			if (num >= 0)
			{
				this.RemoveItemAt(num);
				return true;
			}
			return false;
		}

		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107346928), JContainer.getString_1(107294695));
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107346928), JContainer.getString_1(107294666));
			}
			JToken jtoken = childrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			childrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, jtoken, index));
			}
		}

		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			IList<JToken> childrenTokens = this.ChildrenTokens;
			foreach (JToken jtoken in childrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			childrenTokens.Clear();
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing != null)
			{
				if (existing.Parent == this)
				{
					int index = this.IndexOfItem(existing);
					this.SetItem(index, replacement);
					return;
				}
			}
		}

		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException(JContainer.getString_1(107296230));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107294641), JContainer.getString_1(107294592));
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException(JContainer.getString_1(107294587));
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException(JContainer.getString_1(107293962));
			}
			int num = 0;
			foreach (JToken value in this.ChildrenTokens)
			{
				array.SetValue(value, arrayIndex + num);
				num++;
			}
		}

		internal static bool IsTokenUnchanged(JToken currentValue, JToken newValue)
		{
			JValue jvalue;
			return (jvalue = (currentValue as JValue)) != null && ((jvalue.Type == JTokenType.Null && newValue == null) || jvalue.Equals(newValue));
		}

		internal virtual void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, JContainer.getString_1(107293812));
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException(JContainer.getString_1(107293807).FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content, false);
		}

		internal void AddAndSkipParentCheck(JToken token)
		{
			this.AddInternal(this.ChildrenTokens.Count, token, true);
		}

		public void AddFirst(object content)
		{
			this.AddInternal(0, content, false);
		}

		internal void AddInternal(int index, object content, bool skipParentCheck)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object content2 = enumerator.Current;
						this.AddInternal(num, content2, skipParentCheck);
						num++;
					}
					return;
				}
			}
			JToken item = JContainer.CreateFromContent(content);
			this.InsertItem(index, item, skipParentCheck);
		}

		internal static JToken CreateFromContent(object content)
		{
			JToken result;
			if ((result = (content as JToken)) != null)
			{
				return result;
			}
			return new JValue(content);
		}

		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		public void RemoveAll()
		{
			this.ClearItems();
		}

		internal abstract void MergeItem(object content, JsonMergeSettings settings);

		public void Merge(object content)
		{
			this.MergeItem(content, new JsonMergeSettings());
		}

		public void Merge(object content, JsonMergeSettings settings)
		{
			this.MergeItem(content, settings);
		}

		internal void ReadTokenFrom(JsonReader reader, JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, JContainer.getString_1(107294286).FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, JContainer.getString_1(107294205).FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		internal void ReadContentFrom(JsonReader r, JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, JContainer.getString_1(107294144));
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				JProperty jproperty;
				if ((jproperty = (jcontainer as JProperty)) != null && jproperty.Value != null)
				{
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_1CC;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_1CC;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_1CC;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_1CC;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(r, settings, lineInfo, jcontainer);
					if (jproperty2 != null)
					{
						jcontainer = jproperty2;
						goto IL_1CC;
					}
					r.Skip();
					goto IL_1CC;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(r.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						jcontainer.Add(jvalue);
						goto IL_1CC;
					}
					goto IL_1CC;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1CC;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1CC;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_1CC;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1CC;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1CC;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_1CC;
				}
				break;
				IL_1CC:
				if (!r.Read())
				{
					return;
				}
			}
			throw new InvalidOperationException(JContainer.getString_1(107294171).FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		private static JProperty ReadProperty(JsonReader r, JsonLoadSettings settings, IJsonLineInfo lineInfo, JContainer parent)
		{
			DuplicatePropertyNameHandling duplicatePropertyNameHandling = (settings != null) ? settings.DuplicatePropertyNameHandling : DuplicatePropertyNameHandling.Replace;
			JObject jobject = (JObject)parent;
			string text = r.Value.ToString();
			JProperty jproperty = jobject.Property(text);
			if (jproperty != null)
			{
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Ignore)
				{
					return null;
				}
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Error)
				{
					throw JsonReaderException.Create(r, JContainer.getString_1(107294098).FormatWith(CultureInfo.InvariantCulture, text));
				}
			}
			JProperty jproperty2 = new JProperty(text);
			jproperty2.SetLineInfo(lineInfo, settings);
			if (jproperty == null)
			{
				parent.Add(jproperty2);
			}
			else
			{
				jproperty.Replace(jproperty2);
			}
			return jproperty2;
		}

		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor customTypeDescriptor = this.First as ICustomTypeDescriptor;
			if (customTypeDescriptor == null)
			{
				return null;
			}
			return customTypeDescriptor.GetProperties();
		}

		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		JToken IList<JToken>.this[int index]
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

		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken result;
			if ((result = (value as JToken)) == null)
			{
				throw new ArgumentException(JContainer.getString_1(107293489));
			}
			return result;
		}

		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		void IList.Clear()
		{
			this.ClearItems();
		}

		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false);
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new JsonException(JContainer.getString_1(107293452).FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			JToken jtoken;
			if ((jtoken = (addingNewEventArgs.NewObject as JToken)) == null)
			{
				throw new JsonException(JContainer.getString_1(107293355).FormatWith(CultureInfo.InvariantCulture, typeof(JToken)));
			}
			this.Add(jtoken);
			return jtoken;
		}

		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return null;
			}
		}

		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, JsonMergeSettings settings)
		{
			switch (settings.MergeArrayHandling)
			{
			case MergeArrayHandling.Concat:
				using (IEnumerator enumerator = content.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						JToken content2 = (JToken)obj;
						target.Add(content2);
					}
					return;
				}
				break;
			case MergeArrayHandling.Union:
				break;
			case MergeArrayHandling.Replace:
				goto IL_D7;
			case MergeArrayHandling.Merge:
				goto IL_11D;
			default:
				throw new ArgumentOutOfRangeException(JContainer.getString_1(107400692), JContainer.getString_1(107293302));
			}
			HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					JToken jtoken = (JToken)obj2;
					if (hashSet.Add(jtoken))
					{
						target.Add(jtoken);
					}
				}
				return;
			}
			IL_D7:
			target.ClearItems();
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					JToken content3 = (JToken)obj3;
					target.Add(content3);
				}
				return;
			}
			IL_11D:
			int num = 0;
			foreach (object obj4 in content)
			{
				if (num < target.Count)
				{
					JContainer jcontainer;
					if ((jcontainer = (target[num] as JContainer)) != null)
					{
						jcontainer.Merge(obj4, settings);
					}
					else if (obj4 != null)
					{
						JToken jtoken2 = JContainer.CreateFromContent(obj4);
						if (jtoken2.Type != JTokenType.Null)
						{
							target[num] = jtoken2;
						}
					}
				}
				else
				{
					target.Add(obj4);
				}
				num++;
			}
		}

		static JContainer()
		{
			Strings.CreateGetStringDelegate(typeof(JContainer));
		}

		internal ListChangedEventHandler _listChanged;

		internal AddingNewEventHandler _addingNew;

		internal NotifyCollectionChangedEventHandler _collectionChanged;

		private object _syncRoot;

		private bool _busy;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
