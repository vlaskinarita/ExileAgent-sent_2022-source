using System;
using System.Globalization;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	[Serializable]
	public sealed class Cookie
	{
		static Cookie()
		{
			Strings.CreateGetStringDelegate(typeof(Cookie));
			Cookie._emptyPorts = new int[0];
			Cookie._reservedCharsForValue = new char[]
			{
				';',
				','
			};
		}

		internal Cookie()
		{
			this.init(string.Empty, string.Empty, string.Empty, string.Empty);
		}

		public Cookie(string name, string value) : this(name, value, string.Empty, string.Empty)
		{
		}

		public Cookie(string name, string value, string path) : this(name, value, path, string.Empty)
		{
		}

		public Cookie(string name, string value, string path, string domain)
		{
			if (name == null)
			{
				throw new ArgumentNullException(Cookie.getString_0(107377925));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Cookie.getString_0(107139999), Cookie.getString_0(107377925));
			}
			if (name[0] == '$')
			{
				string message = Cookie.getString_0(107132471);
				throw new ArgumentException(message, Cookie.getString_0(107377925));
			}
			if (!name.IsToken())
			{
				string message2 = Cookie.getString_0(107135268);
				throw new ArgumentException(message2, Cookie.getString_0(107377925));
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (value.Contains(Cookie._reservedCharsForValue) && !value.IsEnclosedIn('"'))
			{
				string message3 = Cookie.getString_0(107132430);
				throw new ArgumentException(message3, Cookie.getString_0(107457590));
			}
			this.init(name, value, path ?? string.Empty, domain ?? string.Empty);
		}

		internal bool ExactDomain
		{
			get
			{
				return this._domain.Length == 0 || this._domain[0] != '.';
			}
		}

		internal int MaxAge
		{
			get
			{
				int result;
				if (this._expires == DateTime.MinValue)
				{
					result = 0;
				}
				else
				{
					DateTime d = (this._expires.Kind != DateTimeKind.Local) ? this._expires.ToLocalTime() : this._expires;
					TimeSpan t = d - DateTime.Now;
					result = ((t > TimeSpan.Zero) ? ((int)t.TotalSeconds) : 0);
				}
				return result;
			}
			set
			{
				this._expires = ((value > 0) ? DateTime.Now.AddSeconds((double)value) : DateTime.Now);
			}
		}

		internal int[] Ports
		{
			get
			{
				return this._ports ?? Cookie._emptyPorts;
			}
		}

		internal string SameSite
		{
			get
			{
				return this._sameSite;
			}
			set
			{
				this._sameSite = value;
			}
		}

		public string Comment
		{
			get
			{
				return this._comment;
			}
			internal set
			{
				this._comment = value;
			}
		}

		public Uri CommentUri
		{
			get
			{
				return this._commentUri;
			}
			internal set
			{
				this._commentUri = value;
			}
		}

		public bool Discard
		{
			get
			{
				return this._discard;
			}
			internal set
			{
				this._discard = value;
			}
		}

		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = (value ?? string.Empty);
			}
		}

		public bool Expired
		{
			get
			{
				return this._expires != DateTime.MinValue && this._expires <= DateTime.Now;
			}
			set
			{
				this._expires = (value ? DateTime.Now : DateTime.MinValue);
			}
		}

		public DateTime Expires
		{
			get
			{
				return this._expires;
			}
			set
			{
				this._expires = value;
			}
		}

		public bool HttpOnly
		{
			get
			{
				return this._httpOnly;
			}
			set
			{
				this._httpOnly = value;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(Cookie.getString_0(107457590));
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(Cookie.getString_0(107139999), Cookie.getString_0(107457590));
				}
				if (value[0] == '$')
				{
					string message = Cookie.getString_0(107132471);
					throw new ArgumentException(message, Cookie.getString_0(107457590));
				}
				if (!value.IsToken())
				{
					string message2 = Cookie.getString_0(107135268);
					throw new ArgumentException(message2, Cookie.getString_0(107457590));
				}
				this._name = value;
			}
		}

		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = (value ?? string.Empty);
			}
		}

		public string Port
		{
			get
			{
				return this._port;
			}
			internal set
			{
				int[] ports;
				if (Cookie.tryCreatePorts(value, out ports))
				{
					this._port = value;
					this._ports = ports;
				}
			}
		}

		public bool Secure
		{
			get
			{
				return this._secure;
			}
			set
			{
				this._secure = value;
			}
		}

		public DateTime TimeStamp
		{
			get
			{
				return this._timeStamp;
			}
		}

		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Contains(Cookie._reservedCharsForValue) && !value.IsEnclosedIn('"'))
				{
					string message = Cookie.getString_0(107132430);
					throw new ArgumentException(message, Cookie.getString_0(107457590));
				}
				this._value = value;
			}
		}

		public int Version
		{
			get
			{
				return this._version;
			}
			internal set
			{
				if (value >= 0 && value <= 1)
				{
					this._version = value;
				}
			}
		}

		private static int hash(int i, int j, int k, int l, int m)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25) ^ (m << 20 | m >> 12);
		}

		private void init(string name, string value, string path, string domain)
		{
			this._name = name;
			this._value = value;
			this._path = path;
			this._domain = domain;
			this._expires = DateTime.MinValue;
			this._timeStamp = DateTime.Now;
		}

		private string toResponseStringVersion0()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat(Cookie.getString_0(107132345), this._name, this._value);
			if (this._expires != DateTime.MinValue)
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132364), this._expires.ToUniversalTime().ToString(Cookie.getString_0(107132343), CultureInfo.CreateSpecificCulture(Cookie.getString_0(107132258))));
			}
			if (!this._path.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132249), this._path);
			}
			if (!this._domain.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132232), this._domain);
			}
			if (!this._sameSite.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132247), this._sameSite);
			}
			if (this._secure)
			{
				stringBuilder.Append(Cookie.getString_0(107132706));
			}
			if (this._httpOnly)
			{
				stringBuilder.Append(Cookie.getString_0(107132725));
			}
			return stringBuilder.ToString();
		}

		private string toResponseStringVersion1()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat(Cookie.getString_0(107132676), this._name, this._value, this._version);
			if (this._expires != DateTime.MinValue)
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132647), this.MaxAge);
			}
			if (!this._path.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132249), this._path);
			}
			if (!this._domain.IsNullOrEmpty())
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132232), this._domain);
			}
			if (this._port != null)
			{
				if (this._port != Cookie.getString_0(107314089))
				{
					stringBuilder.AppendFormat(Cookie.getString_0(107132658), this._port);
				}
				else
				{
					stringBuilder.Append(Cookie.getString_0(107132609));
				}
			}
			if (this._comment != null)
			{
				stringBuilder.AppendFormat(Cookie.getString_0(107132632), HttpUtility.UrlEncode(this._comment));
			}
			if (this._commentUri != null)
			{
				string originalString = this._commentUri.OriginalString;
				stringBuilder.AppendFormat(Cookie.getString_0(107132579), (!originalString.IsToken()) ? originalString.Quote() : originalString);
			}
			if (this._discard)
			{
				stringBuilder.Append(Cookie.getString_0(107132586));
			}
			if (this._secure)
			{
				stringBuilder.Append(Cookie.getString_0(107132706));
			}
			return stringBuilder.ToString();
		}

		private static bool tryCreatePorts(string value, out int[] result)
		{
			result = null;
			string[] array = value.Trim(new char[]
			{
				'"'
			}).Split(new char[]
			{
				','
			});
			int num = array.Length;
			int[] array2 = new int[num];
			for (int i = 0; i < num; i++)
			{
				string text = array[i].Trim();
				if (text.Length == 0)
				{
					array2[i] = int.MinValue;
				}
				else if (!int.TryParse(text, out array2[i]))
				{
					return false;
				}
			}
			result = array2;
			return true;
		}

		internal bool EqualsWithoutValue(Cookie cookie)
		{
			StringComparison comparisonType = StringComparison.InvariantCulture;
			StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
			return this._name.Equals(cookie._name, StringComparison.InvariantCultureIgnoreCase) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2) && this._version == cookie._version;
		}

		internal bool EqualsWithoutValueAndVersion(Cookie cookie)
		{
			StringComparison comparisonType = StringComparison.InvariantCulture;
			StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
			return this._name.Equals(cookie._name, StringComparison.InvariantCultureIgnoreCase) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2);
		}

		internal string ToRequestString(Uri uri)
		{
			string result;
			if (this._name.Length == 0)
			{
				result = string.Empty;
			}
			else if (this._version == 0)
			{
				result = string.Format(Cookie.getString_0(107132345), this._name, this._value);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.AppendFormat(Cookie.getString_0(107132541), this._version, this._name, this._value);
				if (!this._path.IsNullOrEmpty())
				{
					stringBuilder.AppendFormat(Cookie.getString_0(107132512), this._path);
				}
				else if (uri != null)
				{
					stringBuilder.AppendFormat(Cookie.getString_0(107132512), uri.GetAbsolutePath());
				}
				else
				{
					stringBuilder.Append(Cookie.getString_0(107132527));
				}
				if (!this._domain.IsNullOrEmpty() && (uri == null || uri.Host != this._domain))
				{
					stringBuilder.AppendFormat(Cookie.getString_0(107132482), this._domain);
				}
				if (this._port != null)
				{
					if (this._port != Cookie.getString_0(107314089))
					{
						stringBuilder.AppendFormat(Cookie.getString_0(107132493), this._port);
					}
					else
					{
						stringBuilder.Append(Cookie.getString_0(107131932));
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		internal string ToResponseString()
		{
			return (this._name.Length == 0) ? string.Empty : ((this._version == 0) ? this.toResponseStringVersion0() : this.toResponseStringVersion1());
		}

		internal static bool TryCreate(string name, string value, out Cookie result)
		{
			result = null;
			try
			{
				result = new Cookie(name, value);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public override bool Equals(object comparand)
		{
			Cookie cookie = comparand as Cookie;
			bool result;
			if (cookie == null)
			{
				result = false;
			}
			else
			{
				StringComparison comparisonType = StringComparison.InvariantCulture;
				StringComparison comparisonType2 = StringComparison.InvariantCultureIgnoreCase;
				result = (this._name.Equals(cookie._name, StringComparison.InvariantCultureIgnoreCase) && this._value.Equals(cookie._value, comparisonType) && this._path.Equals(cookie._path, comparisonType) && this._domain.Equals(cookie._domain, comparisonType2) && this._version == cookie._version);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return Cookie.hash(StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._name), this._value.GetHashCode(), this._path.GetHashCode(), StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._domain), this._version);
		}

		public override string ToString()
		{
			return this.ToRequestString(null);
		}

		private string _comment;

		private Uri _commentUri;

		private bool _discard;

		private string _domain;

		private static readonly int[] _emptyPorts;

		private DateTime _expires;

		private bool _httpOnly;

		private string _name;

		private string _path;

		private string _port;

		private int[] _ports;

		private static readonly char[] _reservedCharsForValue;

		private string _sameSite;

		private bool _secure;

		private DateTime _timeStamp;

		private string _value;

		private int _version;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
