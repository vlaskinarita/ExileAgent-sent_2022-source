using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	[ComVisible(true)]
	[Serializable]
	public sealed class WebHeaderCollection : NameValueCollection, ISerializable
	{
		static WebHeaderCollection()
		{
			Strings.CreateGetStringDelegate(typeof(WebHeaderCollection));
			WebHeaderCollection._headers = new Dictionary<string, HttpHeaderInfo>(StringComparer.InvariantCultureIgnoreCase)
			{
				{
					WebHeaderCollection.getString_0(107131395),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107131395), HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107130302),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107130249), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107130260),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107130239), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129674),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107131373), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129685),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129636), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129615),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129615), HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129610),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129610), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107137980),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107137980), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129633),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129584), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107141745),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107141745), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129595),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129542), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129517),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129528), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129503),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107133872), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129450),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129461), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129948),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129899), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129914),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129865), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129876),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107472767), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107142279),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107142279), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107129827),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129827), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107350190),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107350190), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107130570),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107130570), HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129846),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129846), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129801),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129801), HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129824),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129824), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107133136),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107133136), HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129815),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129770), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129789),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129736), HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129711),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129726), HttpHeaderType.Request | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129161),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129180), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107129135),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129142), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107129113),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107130870), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129068),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129083), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107245284),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107245284), HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129030),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129045), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107128996),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107128996), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129019),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107137031), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107128994),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107136311), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107128937),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107128948), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129435),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129435), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129394),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129394), HttpHeaderType.Request | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107131418),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107131418), HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129385),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129400), HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129351),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107142815), HttpHeaderType.Response | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129326),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107140114), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValueInRequest)
				},
				{
					WebHeaderCollection.getString_0(107129293),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107140139), HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107129304),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107142853), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValueInRequest)
				},
				{
					WebHeaderCollection.getString_0(107129275),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107143226), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValueInResponse)
				},
				{
					WebHeaderCollection.getString_0(107133039),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107133039), HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107129246),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107142302), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107129201),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107129216), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107161423),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107161418), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107161413),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107161413), HttpHeaderType.Request | HttpHeaderType.Response)
				},
				{
					WebHeaderCollection.getString_0(107161432),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107130685), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107161407),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107161407), HttpHeaderType.Request)
				},
				{
					WebHeaderCollection.getString_0(107141467),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107141467), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107161362),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107326154), HttpHeaderType.Request | HttpHeaderType.Restricted)
				},
				{
					WebHeaderCollection.getString_0(107161349),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107161349), HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107161372),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107161372), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107450049),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107450049), HttpHeaderType.Request | HttpHeaderType.Response | HttpHeaderType.MultiValue)
				},
				{
					WebHeaderCollection.getString_0(107161367),
					new HttpHeaderInfo(WebHeaderCollection.getString_0(107137533), HttpHeaderType.Response | HttpHeaderType.Restricted | HttpHeaderType.MultiValue)
				}
			};
		}

		internal WebHeaderCollection(HttpHeaderType state, bool internallyUsed)
		{
			this._state = state;
			this._internallyUsed = internallyUsed;
		}

		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException(WebHeaderCollection.getString_0(107161346));
			}
			try
			{
				this._internallyUsed = serializationInfo.GetBoolean(WebHeaderCollection.getString_0(107161289));
				this._state = (HttpHeaderType)serializationInfo.GetInt32(WebHeaderCollection.getString_0(107161300));
				int @int = serializationInfo.GetInt32(WebHeaderCollection.getString_0(107161259));
				for (int i = 0; i < @int; i++)
				{
					base.Add(serializationInfo.GetString(i.ToString()), serializationInfo.GetString((@int + i).ToString()));
				}
			}
			catch (SerializationException ex)
			{
				throw new ArgumentException(ex.Message, WebHeaderCollection.getString_0(107161346), ex);
			}
		}

		public WebHeaderCollection()
		{
		}

		internal HttpHeaderType State
		{
			get
			{
				return this._state;
			}
		}

		public override string[] AllKeys
		{
			get
			{
				return base.AllKeys;
			}
		}

		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		public string this[HttpRequestHeader header]
		{
			get
			{
				string key = header.ToString();
				string headerName = WebHeaderCollection.getHeaderName(key);
				return this.Get(headerName);
			}
			set
			{
				this.Add(header, value);
			}
		}

		public string this[HttpResponseHeader header]
		{
			get
			{
				string key = header.ToString();
				string headerName = WebHeaderCollection.getHeaderName(key);
				return this.Get(headerName);
			}
			set
			{
				this.Add(header, value);
			}
		}

		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		private void add(string name, string value, HttpHeaderType headerType)
		{
			base.Add(name, value);
			if (this._state <= HttpHeaderType.Unspecified && headerType != HttpHeaderType.Unspecified)
			{
				this._state = headerType;
			}
		}

		private void checkAllowed(HttpHeaderType headerType)
		{
			if (this._state != HttpHeaderType.Unspecified && headerType != HttpHeaderType.Unspecified && headerType != this._state)
			{
				string message = WebHeaderCollection.getString_0(107161282);
				throw new InvalidOperationException(message);
			}
		}

		private static string checkName(string name, string paramName)
		{
			if (name == null)
			{
				string message = WebHeaderCollection.getString_0(107161193);
				throw new ArgumentNullException(paramName, message);
			}
			if (name.Length == 0)
			{
				string message2 = WebHeaderCollection.getString_0(107161680);
				throw new ArgumentException(message2, paramName);
			}
			name = name.Trim();
			if (name.Length == 0)
			{
				string message3 = WebHeaderCollection.getString_0(107161639);
				throw new ArgumentException(message3, paramName);
			}
			if (!name.IsToken())
			{
				string message4 = WebHeaderCollection.getString_0(107161626);
				throw new ArgumentException(message4, paramName);
			}
			return name;
		}

		private void checkRestricted(string name, HttpHeaderType headerType)
		{
			if (!this._internallyUsed)
			{
				bool response = headerType == HttpHeaderType.Response;
				if (WebHeaderCollection.isRestricted(name, response))
				{
					string message = WebHeaderCollection.getString_0(107161541);
					throw new ArgumentException(message);
				}
			}
		}

		private static string checkValue(string value, string paramName)
		{
			string result;
			if (value == null)
			{
				result = string.Empty;
			}
			else
			{
				value = value.Trim();
				int length = value.Length;
				if (length == 0)
				{
					result = value;
				}
				else
				{
					if (length > 65535)
					{
						string message = WebHeaderCollection.getString_0(107161524);
						throw new ArgumentOutOfRangeException(paramName, message);
					}
					if (!value.IsText())
					{
						string message2 = WebHeaderCollection.getString_0(107160899);
						throw new ArgumentException(message2, paramName);
					}
					result = value;
				}
			}
			return result;
		}

		private static HttpHeaderInfo getHeaderInfo(string name)
		{
			StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
			foreach (HttpHeaderInfo httpHeaderInfo in WebHeaderCollection._headers.Values)
			{
				if (httpHeaderInfo.HeaderName.Equals(name, comparisonType))
				{
					return httpHeaderInfo;
				}
			}
			return null;
		}

		private static string getHeaderName(string key)
		{
			HttpHeaderInfo httpHeaderInfo;
			return WebHeaderCollection._headers.TryGetValue(key, out httpHeaderInfo) ? httpHeaderInfo.HeaderName : null;
		}

		private static HttpHeaderType getHeaderType(string name)
		{
			HttpHeaderInfo headerInfo = WebHeaderCollection.getHeaderInfo(name);
			HttpHeaderType result;
			if (headerInfo == null)
			{
				result = HttpHeaderType.Unspecified;
			}
			else if (headerInfo.IsRequest)
			{
				result = ((!headerInfo.IsResponse) ? HttpHeaderType.Request : HttpHeaderType.Unspecified);
			}
			else
			{
				result = (headerInfo.IsResponse ? HttpHeaderType.Response : HttpHeaderType.Unspecified);
			}
			return result;
		}

		private static bool isMultiValue(string name, bool response)
		{
			HttpHeaderInfo headerInfo = WebHeaderCollection.getHeaderInfo(name);
			return headerInfo != null && headerInfo.IsMultiValue(response);
		}

		private static bool isRestricted(string name, bool response)
		{
			HttpHeaderInfo headerInfo = WebHeaderCollection.getHeaderInfo(name);
			return headerInfo != null && headerInfo.IsRestricted(response);
		}

		private void set(string name, string value, HttpHeaderType headerType)
		{
			base.Set(name, value);
			if (this._state <= HttpHeaderType.Unspecified && headerType != HttpHeaderType.Unspecified)
			{
				this._state = headerType;
			}
		}

		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		internal void InternalSet(string header, bool response)
		{
			int num = header.IndexOf(':');
			if (num == -1)
			{
				string message = WebHeaderCollection.getString_0(107160842);
				throw new ArgumentException(message, WebHeaderCollection.getString_0(107251041));
			}
			string name = header.Substring(0, num);
			string value = (num < header.Length - 1) ? header.Substring(num + 1) : string.Empty;
			name = WebHeaderCollection.checkName(name, WebHeaderCollection.getString_0(107251041));
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107251041));
			if (WebHeaderCollection.isMultiValue(name, response))
			{
				base.Add(name, value);
			}
			else
			{
				base.Set(name, value);
			}
		}

		internal void InternalSet(string name, string value, bool response)
		{
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			if (WebHeaderCollection.isMultiValue(name, response))
			{
				base.Add(name, value);
			}
			else
			{
				base.Set(name, value);
			}
		}

		internal string ToStringMultiValue(bool response)
		{
			int count = this.Count;
			string result;
			if (count == 0)
			{
				result = WebHeaderCollection.getString_0(107248694);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string key = this.GetKey(i);
					if (WebHeaderCollection.isMultiValue(key, response))
					{
						foreach (string arg in this.GetValues(i))
						{
							stringBuilder.AppendFormat(WebHeaderCollection.getString_0(107160821), key, arg);
						}
					}
					else
					{
						stringBuilder.AppendFormat(WebHeaderCollection.getString_0(107160821), key, this.Get(i));
					}
				}
				stringBuilder.Append(WebHeaderCollection.getString_0(107248694));
				result = stringBuilder.ToString();
			}
			return result;
		}

		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = WebHeaderCollection.checkName(headerName, WebHeaderCollection.getString_0(107160772));
			headerValue = WebHeaderCollection.checkValue(headerValue, WebHeaderCollection.getString_0(107160787));
			HttpHeaderType headerType = WebHeaderCollection.getHeaderType(headerName);
			this.checkAllowed(headerType);
			this.add(headerName, headerValue, headerType);
		}

		public void Add(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException(WebHeaderCollection.getString_0(107251041));
			}
			int length = header.Length;
			if (length == 0)
			{
				string message = WebHeaderCollection.getString_0(107140297);
				throw new ArgumentException(message, WebHeaderCollection.getString_0(107251041));
			}
			int num = header.IndexOf(':');
			if (num == -1)
			{
				string message2 = WebHeaderCollection.getString_0(107160842);
				throw new ArgumentException(message2, WebHeaderCollection.getString_0(107251041));
			}
			string name = header.Substring(0, num);
			string value = (num < length - 1) ? header.Substring(num + 1) : string.Empty;
			name = WebHeaderCollection.checkName(name, WebHeaderCollection.getString_0(107251041));
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107251041));
			HttpHeaderType headerType = WebHeaderCollection.getHeaderType(name);
			this.checkRestricted(name, headerType);
			this.checkAllowed(headerType);
			this.add(name, value, headerType);
		}

		public void Add(HttpRequestHeader header, string value)
		{
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Request);
			this.checkAllowed(HttpHeaderType.Request);
			this.add(headerName, value, HttpHeaderType.Request);
		}

		public void Add(HttpResponseHeader header, string value)
		{
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Response);
			this.checkAllowed(HttpHeaderType.Response);
			this.add(headerName, value, HttpHeaderType.Response);
		}

		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.checkName(name, WebHeaderCollection.getString_0(107378223));
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			HttpHeaderType headerType = WebHeaderCollection.getHeaderType(name);
			this.checkRestricted(name, headerType);
			this.checkAllowed(headerType);
			this.add(name, value, headerType);
		}

		public override void Clear()
		{
			base.Clear();
			this._state = HttpHeaderType.Unspecified;
		}

		public override string Get(int index)
		{
			return base.Get(index);
		}

		public override string Get(string name)
		{
			return base.Get(name);
		}

		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		public override string GetKey(int index)
		{
			return base.GetKey(index);
		}

		public override string[] GetValues(int index)
		{
			string[] values = base.GetValues(index);
			return (values == null || values.Length == 0) ? null : values;
		}

		public override string[] GetValues(string name)
		{
			string[] values = base.GetValues(name);
			return (values == null || values.Length == 0) ? null : values;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException(WebHeaderCollection.getString_0(107161346));
			}
			serializationInfo.AddValue(WebHeaderCollection.getString_0(107161289), this._internallyUsed);
			serializationInfo.AddValue(WebHeaderCollection.getString_0(107161300), (int)this._state);
			int count = this.Count;
			serializationInfo.AddValue(WebHeaderCollection.getString_0(107161259), count);
			for (int i = 0; i < count; i++)
			{
				serializationInfo.AddValue(i.ToString(), this.GetKey(i));
				serializationInfo.AddValue((count + i).ToString(), this.Get(i));
			}
		}

		public static bool IsRestricted(string headerName)
		{
			return WebHeaderCollection.IsRestricted(headerName, false);
		}

		public static bool IsRestricted(string headerName, bool response)
		{
			headerName = WebHeaderCollection.checkName(headerName, WebHeaderCollection.getString_0(107160772));
			return WebHeaderCollection.isRestricted(headerName, response);
		}

		public override void OnDeserialization(object sender)
		{
		}

		public void Remove(HttpRequestHeader header)
		{
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Request);
			this.checkAllowed(HttpHeaderType.Request);
			base.Remove(headerName);
		}

		public void Remove(HttpResponseHeader header)
		{
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Response);
			this.checkAllowed(HttpHeaderType.Response);
			base.Remove(headerName);
		}

		public override void Remove(string name)
		{
			name = WebHeaderCollection.checkName(name, WebHeaderCollection.getString_0(107378223));
			HttpHeaderType headerType = WebHeaderCollection.getHeaderType(name);
			this.checkRestricted(name, headerType);
			this.checkAllowed(headerType);
			base.Remove(name);
		}

		public void Set(HttpRequestHeader header, string value)
		{
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Request);
			this.checkAllowed(HttpHeaderType.Request);
			this.set(headerName, value, HttpHeaderType.Request);
		}

		public void Set(HttpResponseHeader header, string value)
		{
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			string key = header.ToString();
			string headerName = WebHeaderCollection.getHeaderName(key);
			this.checkRestricted(headerName, HttpHeaderType.Response);
			this.checkAllowed(HttpHeaderType.Response);
			this.set(headerName, value, HttpHeaderType.Response);
		}

		public override void Set(string name, string value)
		{
			name = WebHeaderCollection.checkName(name, WebHeaderCollection.getString_0(107378223));
			value = WebHeaderCollection.checkValue(value, WebHeaderCollection.getString_0(107457888));
			HttpHeaderType headerType = WebHeaderCollection.getHeaderType(name);
			this.checkRestricted(name, headerType);
			this.checkAllowed(headerType);
			this.set(name, value, headerType);
		}

		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		public override string ToString()
		{
			int count = this.Count;
			string result;
			if (count == 0)
			{
				result = WebHeaderCollection.getString_0(107248694);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					stringBuilder.AppendFormat(WebHeaderCollection.getString_0(107160821), this.GetKey(i), this.Get(i));
				}
				stringBuilder.Append(WebHeaderCollection.getString_0(107248694));
				result = stringBuilder.ToString();
			}
			return result;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		private static readonly Dictionary<string, HttpHeaderInfo> _headers;

		private bool _internallyUsed;

		private HttpHeaderType _state;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
