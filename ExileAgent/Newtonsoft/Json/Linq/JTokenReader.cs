using System;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JTokenReader : JsonReader, IJsonLineInfo
	{
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, JTokenReader.getString_1(107346918));
			this._root = token;
		}

		internal JTokenReader(JToken token, string initialPath) : this(token)
		{
			this._initialPath = initialPath;
		}

		public override bool Read()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				this._current = this._root;
				this.SetToken(this._current);
				return true;
			}
			if (this._current == null)
			{
				return false;
			}
			JContainer jcontainer;
			if ((jcontainer = (this._current as JContainer)) != null && this._parent != jcontainer)
			{
				return this.ReadInto(jcontainer);
			}
			return this.ReadOver(this._current);
		}

		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t)
			{
				if (t != t.Parent.Last)
				{
					this._current = next;
					this.SetToken(this._current);
					return true;
				}
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		private bool ReadToEnd()
		{
			this._current = null;
			base.SetToken(JsonToken.None);
			return false;
		}

		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return null;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JTokenReader.getString_1(107254463), c.Type, JTokenReader.getString_1(107291661));
			}
		}

		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.GetValueOrDefault());
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				return;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				return;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor, ((JConstructor)token).Name);
				return;
			case JTokenType.Property:
				base.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				return;
			case JTokenType.Comment:
				base.SetToken(JsonToken.Comment, ((JValue)token).Value);
				return;
			case JTokenType.Integer:
				base.SetToken(JsonToken.Integer, ((JValue)token).Value);
				return;
			case JTokenType.Float:
				base.SetToken(JsonToken.Float, ((JValue)token).Value);
				return;
			case JTokenType.String:
				base.SetToken(JsonToken.String, ((JValue)token).Value);
				return;
			case JTokenType.Boolean:
				base.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				return;
			case JTokenType.Null:
				base.SetToken(JsonToken.Null, ((JValue)token).Value);
				return;
			case JTokenType.Undefined:
				base.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				return;
			case JTokenType.Date:
			{
				object obj = ((JValue)token).Value;
				object obj2;
				if ((obj2 = obj) is DateTime)
				{
					DateTime value = (DateTime)obj2;
					obj = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
				}
				base.SetToken(JsonToken.Date, obj);
				return;
			}
			case JTokenType.Raw:
				base.SetToken(JsonToken.Raw, ((JValue)token).Value);
				return;
			case JTokenType.Bytes:
				base.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				return;
			case JTokenType.Guid:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.Uri:
			{
				object value2 = ((JValue)token).Value;
				Uri uri;
				base.SetToken(JsonToken.String, ((uri = (value2 as Uri)) != null) ? uri.OriginalString : this.SafeToString(value2));
				return;
			}
			case JTokenType.TimeSpan:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(JTokenReader.getString_1(107254463), token.Type, JTokenReader.getString_1(107291112));
			}
		}

		private string SafeToString(object value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo current = this._current;
			return current != null && current.HasLineInfo();
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LineNumber;
				}
				return 0;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LinePosition;
				}
				return 0;
			}
		}

		public override string Path
		{
			get
			{
				string text = base.Path;
				if (this._initialPath == null)
				{
					this._initialPath = this._root.Path;
				}
				if (!string.IsNullOrEmpty(this._initialPath))
				{
					if (string.IsNullOrEmpty(text))
					{
						return this._initialPath;
					}
					if (text.StartsWith('['))
					{
						text = this._initialPath + text;
					}
					else
					{
						text = this._initialPath + JTokenReader.getString_1(107374165) + text;
					}
				}
				return text;
			}
		}

		static JTokenReader()
		{
			Strings.CreateGetStringDelegate(typeof(JTokenReader));
		}

		private readonly JToken _root;

		private string _initialPath;

		private JToken _parent;

		private JToken _current;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
