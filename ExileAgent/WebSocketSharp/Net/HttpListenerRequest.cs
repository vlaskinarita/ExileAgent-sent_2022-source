using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerRequest
	{
		static HttpListenerRequest()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerRequest));
			HttpListenerRequest._100continue = Encoding.ASCII.GetBytes(HttpListenerRequest.getString_0(107131368));
		}

		internal HttpListenerRequest(HttpListenerContext context)
		{
			this._context = context;
			this._connection = context.Connection;
			this._contentLength = -1L;
			this._headers = new WebHeaderCollection();
			this._requestTraceIdentifier = Guid.NewGuid();
		}

		public string[] AcceptTypes
		{
			get
			{
				string text = this._headers[HttpListenerRequest.getString_0(107131331)];
				string[] result;
				if (text == null)
				{
					result = null;
				}
				else
				{
					if (this._acceptTypes == null)
					{
						this._acceptTypes = text.SplitHeaderValue(new char[]
						{
							','
						}).TrimEach().ToList<string>().ToArray();
					}
					result = this._acceptTypes;
				}
				return result;
			}
		}

		public int ClientCertificateError
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public Encoding ContentEncoding
		{
			get
			{
				if (this._contentEncoding == null)
				{
					this._contentEncoding = this.getContentEncoding();
				}
				return this._contentEncoding;
			}
		}

		public long ContentLength64
		{
			get
			{
				return this._contentLength;
			}
		}

		public string ContentType
		{
			get
			{
				return this._headers[HttpListenerRequest.getString_0(107472703)];
			}
		}

		public CookieCollection Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = this._headers.GetCookies(false);
				}
				return this._cookies;
			}
		}

		public bool HasEntityBody
		{
			get
			{
				return this._contentLength > 0L || this._chunked;
			}
		}

		public NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		public string HttpMethod
		{
			get
			{
				return this._httpMethod;
			}
		}

		public Stream InputStream
		{
			get
			{
				if (this._inputStream == null)
				{
					this._inputStream = ((this._contentLength > 0L || this._chunked) ? this._connection.GetRequestStream(this._contentLength, this._chunked) : Stream.Null);
				}
				return this._inputStream;
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				return this._context.User != null;
			}
		}

		public bool IsLocal
		{
			get
			{
				return this._connection.IsLocal;
			}
		}

		public bool IsSecureConnection
		{
			get
			{
				return this._connection.IsSecure;
			}
		}

		public bool IsWebSocketRequest
		{
			get
			{
				return this._httpMethod == HttpListenerRequest.getString_0(107457815) && this._headers.Upgrades(HttpListenerRequest.getString_0(107144190));
			}
		}

		public bool KeepAlive
		{
			get
			{
				return this._headers.KeepsAlive(this._protocolVersion);
			}
		}

		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this._connection.LocalEndPoint;
			}
		}

		public Version ProtocolVersion
		{
			get
			{
				return this._protocolVersion;
			}
		}

		public NameValueCollection QueryString
		{
			get
			{
				if (this._queryString == null)
				{
					Uri url = this.Url;
					this._queryString = QueryStringCollection.Parse((url != null) ? url.Query : null, Encoding.UTF8);
				}
				return this._queryString;
			}
		}

		public string RawUrl
		{
			get
			{
				return this._rawUrl;
			}
		}

		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this._connection.RemoteEndPoint;
			}
		}

		public Guid RequestTraceIdentifier
		{
			get
			{
				return this._requestTraceIdentifier;
			}
		}

		public Uri Url
		{
			get
			{
				if (!this._urlSet)
				{
					this._url = HttpUtility.CreateRequestUrl(this._rawUrl, this._userHostName ?? this.UserHostAddress, this.IsWebSocketRequest, this.IsSecureConnection);
					this._urlSet = true;
				}
				return this._url;
			}
		}

		public Uri UrlReferrer
		{
			get
			{
				string text = this._headers[HttpListenerRequest.getString_0(107131354)];
				Uri result;
				if (text == null)
				{
					result = null;
				}
				else
				{
					if (this._urlReferrer == null)
					{
						this._urlReferrer = text.ToUri();
					}
					result = this._urlReferrer;
				}
				return result;
			}
		}

		public string UserAgent
		{
			get
			{
				return this._headers[HttpListenerRequest.getString_0(107326090)];
			}
		}

		public string UserHostAddress
		{
			get
			{
				return this._connection.LocalEndPoint.ToString();
			}
		}

		public string UserHostName
		{
			get
			{
				return this._userHostName;
			}
		}

		public string[] UserLanguages
		{
			get
			{
				string text = this._headers[HttpListenerRequest.getString_0(107131309)];
				string[] result;
				if (text == null)
				{
					result = null;
				}
				else
				{
					if (this._userLanguages == null)
					{
						this._userLanguages = text.Split(new char[]
						{
							','
						}).TrimEach().ToList<string>().ToArray();
					}
					result = this._userLanguages;
				}
				return result;
			}
		}

		private Encoding getContentEncoding()
		{
			string text = this._headers[HttpListenerRequest.getString_0(107472703)];
			Encoding result;
			if (text == null)
			{
				result = Encoding.UTF8;
			}
			else
			{
				Encoding encoding;
				result = (HttpUtility.TryGetEncoding(text, out encoding) ? encoding : Encoding.UTF8);
			}
			return result;
		}

		internal void AddHeader(string headerField)
		{
			char c = headerField[0];
			if (c == ' ' || c == '\t')
			{
				this._context.ErrorMessage = HttpListenerRequest.getString_0(107131320);
			}
			else
			{
				int num = headerField.IndexOf(':');
				if (num < 1)
				{
					this._context.ErrorMessage = HttpListenerRequest.getString_0(107131320);
				}
				else
				{
					string text = headerField.Substring(0, num).Trim();
					if (text.Length == 0 || !text.IsToken())
					{
						this._context.ErrorMessage = HttpListenerRequest.getString_0(107131291);
					}
					else
					{
						string text2 = (num < headerField.Length - 1) ? headerField.Substring(num + 1).Trim() : string.Empty;
						this._headers.InternalSet(text, text2, false);
						string a = text.ToLower(CultureInfo.InvariantCulture);
						if (a == HttpListenerRequest.getString_0(107131262))
						{
							if (this._userHostName != null)
							{
								this._context.ErrorMessage = HttpListenerRequest.getString_0(107131253);
							}
							else if (text2.Length == 0)
							{
								this._context.ErrorMessage = HttpListenerRequest.getString_0(107131253);
							}
							else
							{
								this._userHostName = text2;
							}
						}
						else if (a == HttpListenerRequest.getString_0(107131224))
						{
							long num2;
							if (this._contentLength > -1L)
							{
								this._context.ErrorMessage = HttpListenerRequest.getString_0(107131171);
							}
							else if (!long.TryParse(text2, out num2))
							{
								this._context.ErrorMessage = HttpListenerRequest.getString_0(107131171);
							}
							else if (num2 < 0L)
							{
								this._context.ErrorMessage = HttpListenerRequest.getString_0(107131171);
							}
							else
							{
								this._contentLength = num2;
							}
						}
					}
				}
			}
		}

		internal void FinishInitialization()
		{
			if (this._userHostName == null)
			{
				this._context.ErrorMessage = HttpListenerRequest.getString_0(107130650);
			}
			else
			{
				string text = this._headers[HttpListenerRequest.getString_0(107130621)];
				if (text != null)
				{
					if (!text.Equals(HttpListenerRequest.getString_0(107130564), StringComparison.OrdinalIgnoreCase))
					{
						this._context.ErrorMessage = HttpListenerRequest.getString_0(107130583);
						this._context.ErrorStatusCode = 501;
						return;
					}
					this._chunked = true;
				}
				if ((this._httpMethod == HttpListenerRequest.getString_0(107380089) || this._httpMethod == HttpListenerRequest.getString_0(107142296)) && (this._contentLength <= 0L && !this._chunked))
				{
					this._context.ErrorMessage = string.Empty;
					this._context.ErrorStatusCode = 411;
				}
				else
				{
					string text2 = this._headers[HttpListenerRequest.getString_0(107130506)];
					if (text2 != null)
					{
						if (!text2.Equals(HttpListenerRequest.getString_0(107130529), StringComparison.OrdinalIgnoreCase))
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130480);
						}
						else
						{
							ResponseStream responseStream = this._connection.GetResponseStream();
							responseStream.InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
						}
					}
				}
			}
		}

		internal bool FlushInput()
		{
			Stream inputStream = this.InputStream;
			bool result;
			if (inputStream == Stream.Null)
			{
				result = true;
			}
			else
			{
				int num = 2048;
				if (this._contentLength > 0L && this._contentLength < (long)num)
				{
					num = (int)this._contentLength;
				}
				byte[] buffer = new byte[num];
				for (;;)
				{
					try
					{
						IAsyncResult asyncResult = inputStream.BeginRead(buffer, 0, num, null, null);
						if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(100))
						{
							result = false;
							break;
						}
						if (inputStream.EndRead(asyncResult) <= 0)
						{
							result = true;
							break;
						}
					}
					catch
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		internal bool IsUpgradeRequest(string protocol)
		{
			return this._headers.Upgrades(protocol);
		}

		internal void SetRequestLine(string requestLine)
		{
			string[] array = requestLine.Split(new char[]
			{
				' '
			}, 3);
			if (array.Length < 3)
			{
				this._context.ErrorMessage = HttpListenerRequest.getString_0(107130483);
			}
			else
			{
				string text = array[0];
				if (text.Length == 0)
				{
					this._context.ErrorMessage = HttpListenerRequest.getString_0(107130410);
				}
				else
				{
					string text2 = array[1];
					if (text2.Length == 0)
					{
						this._context.ErrorMessage = HttpListenerRequest.getString_0(107130913);
					}
					else
					{
						string text3 = array[2];
						Version version;
						if (text3.Length != 8)
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130872);
						}
						else if (!text3.StartsWith(HttpListenerRequest.getString_0(107143148), StringComparison.Ordinal))
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130872);
						}
						else if (!text3.Substring(5).TryCreateVersion(out version))
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130872);
						}
						else if (version != HttpVersion.Version11)
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130872);
							this._context.ErrorStatusCode = 505;
						}
						else if (!text.IsHttpMethod(version))
						{
							this._context.ErrorMessage = HttpListenerRequest.getString_0(107130410);
							this._context.ErrorStatusCode = 501;
						}
						else
						{
							this._httpMethod = text;
							this._rawUrl = text2;
							this._protocolVersion = version;
						}
					}
				}
			}
		}

		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			throw new NotSupportedException();
		}

		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		public X509Certificate2 GetClientCertificate()
		{
			throw new NotSupportedException();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat(HttpListenerRequest.getString_0(107130799), this._httpMethod, this._rawUrl, this._protocolVersion).Append(this._headers.ToString());
			return stringBuilder.ToString();
		}

		private static readonly byte[] _100continue;

		private string[] _acceptTypes;

		private bool _chunked;

		private HttpConnection _connection;

		private Encoding _contentEncoding;

		private long _contentLength;

		private HttpListenerContext _context;

		private CookieCollection _cookies;

		private WebHeaderCollection _headers;

		private string _httpMethod;

		private Stream _inputStream;

		private Version _protocolVersion;

		private NameValueCollection _queryString;

		private string _rawUrl;

		private Guid _requestTraceIdentifier;

		private Uri _url;

		private Uri _urlReferrer;

		private bool _urlSet;

		private string _userHostName;

		private string[] _userLanguages;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
