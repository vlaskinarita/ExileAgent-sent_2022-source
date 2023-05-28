using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerResponse : IDisposable
	{
		internal HttpListenerResponse(HttpListenerContext context)
		{
			this._context = context;
			this._keepAlive = true;
			this._statusCode = 200;
			this._statusDescription = HttpListenerResponse.getString_0(107141348);
			this._version = HttpVersion.Version11;
		}

		internal bool CloseConnection
		{
			get
			{
				return this._closeConnection;
			}
			set
			{
				this._closeConnection = value;
			}
		}

		internal WebHeaderCollection FullHeaders
		{
			get
			{
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(HttpHeaderType.Response, true);
				if (this._headers != null)
				{
					webHeaderCollection.Add(this._headers);
				}
				if (this._contentType != null)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107472720), HttpListenerResponse.createContentTypeHeaderText(this._contentType, this._contentEncoding), true);
				}
				if (webHeaderCollection[HttpListenerResponse.getString_0(107132992)] == null)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107132992), HttpListenerResponse.getString_0(107133118), true);
				}
				if (webHeaderCollection[HttpListenerResponse.getString_0(107350143)] == null)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107350143), DateTime.UtcNow.ToString(HttpListenerResponse.getString_0(107297688), CultureInfo.InvariantCulture), true);
				}
				if (this._sendChunked)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107130638), HttpListenerResponse.getString_0(107130581), true);
				}
				else
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107133825), this._contentLength.ToString(CultureInfo.InvariantCulture), true);
				}
				bool flag = !this._context.Request.KeepAlive || !this._keepAlive || this._statusCode == 400 || this._statusCode == 408 || this._statusCode == 411 || this._statusCode == 413 || this._statusCode == 414 || this._statusCode == 500 || this._statusCode == 503;
				int reuses = this._context.Connection.Reuses;
				if (flag || reuses >= 100)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107141698), HttpListenerResponse.getString_0(107141713), true);
				}
				else
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107130823), string.Format(HttpListenerResponse.getString_0(107130774), 100 - reuses), true);
					if (this._context.Request.ProtocolVersion < HttpVersion.Version11)
					{
						webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107141698), HttpListenerResponse.getString_0(107141704), true);
					}
				}
				if (this._redirectLocation != null)
				{
					webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107245237), this._redirectLocation.AbsoluteUri, true);
				}
				if (this._cookies != null)
				{
					foreach (Cookie cookie in this._cookies)
					{
						webHeaderCollection.InternalSet(HttpListenerResponse.getString_0(107142255), cookie.ToResponseString(), true);
					}
				}
				return webHeaderCollection;
			}
		}

		internal bool HeadersSent
		{
			get
			{
				return this._headersSent;
			}
			set
			{
				this._headersSent = value;
			}
		}

		internal string StatusLine
		{
			get
			{
				return string.Format(HttpListenerResponse.getString_0(107130749), this._version, this._statusCode, this._statusDescription);
			}
		}

		public Encoding ContentEncoding
		{
			get
			{
				return this._contentEncoding;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				this._contentEncoding = value;
			}
		}

		public long ContentLength64
		{
			get
			{
				return this._contentLength;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				if (value < 0L)
				{
					string paramName = HttpListenerResponse.getString_0(107133960);
					throw new ArgumentOutOfRangeException(paramName, HttpListenerResponse.getString_0(107457841));
				}
				this._contentLength = value;
			}
		}

		public string ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				if (value == null)
				{
					this._contentType = null;
				}
				else
				{
					if (value.Length == 0)
					{
						string message2 = HttpListenerResponse.getString_0(107140250);
						throw new ArgumentException(message2, HttpListenerResponse.getString_0(107457841));
					}
					if (!HttpListenerResponse.isValidForContentType(value))
					{
						string message3 = HttpListenerResponse.getString_0(107135519);
						throw new ArgumentException(message3, HttpListenerResponse.getString_0(107457841));
					}
					this._contentType = value;
				}
			}
		}

		public CookieCollection Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = new CookieCollection();
				}
				return this._cookies;
			}
			set
			{
				this._cookies = value;
			}
		}

		public WebHeaderCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new WebHeaderCollection(HttpHeaderType.Response, false);
				}
				return this._headers;
			}
			set
			{
				if (value == null)
				{
					this._headers = null;
				}
				else
				{
					if (value.State != HttpHeaderType.Response)
					{
						string message = HttpListenerResponse.getString_0(107130707);
						throw new InvalidOperationException(message);
					}
					this._headers = value;
				}
			}
		}

		public bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				this._keepAlive = value;
			}
		}

		public Stream OutputStream
		{
			get
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._outputStream == null)
				{
					this._outputStream = this._context.Connection.GetResponseStream();
				}
				return this._outputStream;
			}
		}

		public Version ProtocolVersion
		{
			get
			{
				return this._version;
			}
		}

		public string RedirectLocation
		{
			get
			{
				return (this._redirectLocation != null) ? this._redirectLocation.OriginalString : null;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				if (value == null)
				{
					this._redirectLocation = null;
				}
				else
				{
					if (value.Length == 0)
					{
						string message2 = HttpListenerResponse.getString_0(107140250);
						throw new ArgumentException(message2, HttpListenerResponse.getString_0(107457841));
					}
					Uri redirectLocation;
					if (!Uri.TryCreate(value, UriKind.Absolute, out redirectLocation))
					{
						string message3 = HttpListenerResponse.getString_0(107130110);
						throw new ArgumentException(message3, HttpListenerResponse.getString_0(107457841));
					}
					this._redirectLocation = redirectLocation;
				}
			}
		}

		public bool SendChunked
		{
			get
			{
				return this._sendChunked;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				this._sendChunked = value;
			}
		}

		public int StatusCode
		{
			get
			{
				return this._statusCode;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				if (value < 100 || value > 999)
				{
					string message2 = HttpListenerResponse.getString_0(107130081);
					throw new ProtocolViolationException(message2);
				}
				this._statusCode = value;
				this._statusDescription = value.GetStatusDescription();
			}
		}

		public string StatusDescription
		{
			get
			{
				return this._statusDescription;
			}
			set
			{
				if (this._disposed)
				{
					string objectName = base.GetType().ToString();
					throw new ObjectDisposedException(objectName);
				}
				if (this._headersSent)
				{
					string message = HttpListenerResponse.getString_0(107130756);
					throw new InvalidOperationException(message);
				}
				if (value == null)
				{
					throw new ArgumentNullException(HttpListenerResponse.getString_0(107457841));
				}
				if (value.Length == 0)
				{
					this._statusDescription = this._statusCode.GetStatusDescription();
				}
				else
				{
					if (!HttpListenerResponse.isValidForStatusDescription(value))
					{
						string message2 = HttpListenerResponse.getString_0(107135519);
						throw new ArgumentException(message2, HttpListenerResponse.getString_0(107457841));
					}
					this._statusDescription = value;
				}
			}
		}

		private bool canSetCookie(Cookie cookie)
		{
			List<Cookie> list = this.findCookie(cookie).ToList<Cookie>();
			bool result;
			if (list.Count == 0)
			{
				result = true;
			}
			else
			{
				int version = cookie.Version;
				foreach (Cookie cookie2 in list)
				{
					if (cookie2.Version == version)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		private void close(bool force)
		{
			this._disposed = true;
			this._context.Connection.Close(force);
		}

		private void close(byte[] responseEntity, int bufferLength, bool willBlock)
		{
			Stream outputStream = this.OutputStream;
			if (willBlock)
			{
				outputStream.WriteBytes(responseEntity, bufferLength);
				this.close(false);
			}
			else
			{
				outputStream.WriteBytesAsync(responseEntity, bufferLength, delegate
				{
					this.close(false);
				}, null);
			}
		}

		private static string createContentTypeHeaderText(string value, Encoding encoding)
		{
			string result;
			if (value.IndexOf(HttpListenerResponse.getString_0(107130052), StringComparison.Ordinal) > -1)
			{
				result = value;
			}
			else if (encoding == null)
			{
				result = value;
			}
			else
			{
				result = string.Format(HttpListenerResponse.getString_0(107130007), value, encoding.WebName);
			}
			return result;
		}

		private IEnumerable<Cookie> findCookie(Cookie cookie)
		{
			HttpListenerResponse.<findCookie>d__65 <findCookie>d__ = new HttpListenerResponse.<findCookie>d__65(-2);
			<findCookie>d__.<>4__this = this;
			<findCookie>d__.<>3__cookie = cookie;
			return <findCookie>d__;
		}

		private static bool isValidForContentType(string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool result;
				if (c >= ' ')
				{
					if (c <= '~')
					{
						if (HttpListenerResponse.getString_0(107129982).IndexOf(c) <= -1)
						{
							i++;
							continue;
						}
						result = false;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			return true;
		}

		private static bool isValidForStatusDescription(string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool result;
				if (c >= ' ')
				{
					if (c <= '~')
					{
						i++;
						continue;
					}
					result = false;
				}
				else
				{
					result = false;
				}
				return result;
			}
			return true;
		}

		public void Abort()
		{
			if (!this._disposed)
			{
				this.close(true);
			}
		}

		public void AppendCookie(Cookie cookie)
		{
			this.Cookies.Add(cookie);
		}

		public void AppendHeader(string name, string value)
		{
			this.Headers.Add(name, value);
		}

		public void Close()
		{
			if (!this._disposed)
			{
				this.close(false);
			}
		}

		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (this._disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			if (responseEntity == null)
			{
				throw new ArgumentNullException(HttpListenerResponse.getString_0(107129997));
			}
			long num = (long)responseEntity.Length;
			if (num > 2147483647L)
			{
				this.close(responseEntity, 1024, willBlock);
			}
			else
			{
				Stream stream = this.OutputStream;
				if (willBlock)
				{
					stream.Write(responseEntity, 0, (int)num);
					this.close(false);
				}
				else
				{
					stream.BeginWrite(responseEntity, 0, (int)num, delegate(IAsyncResult ar)
					{
						stream.EndWrite(ar);
						this.close(false);
					}, null);
				}
			}
		}

		public void CopyFrom(HttpListenerResponse templateResponse)
		{
			if (templateResponse == null)
			{
				throw new ArgumentNullException(HttpListenerResponse.getString_0(107129944));
			}
			WebHeaderCollection headers = templateResponse._headers;
			if (headers != null)
			{
				if (this._headers != null)
				{
					this._headers.Clear();
				}
				this.Headers.Add(headers);
			}
			else
			{
				this._headers = null;
			}
			this._contentLength = templateResponse._contentLength;
			this._statusCode = templateResponse._statusCode;
			this._statusDescription = templateResponse._statusDescription;
			this._keepAlive = templateResponse._keepAlive;
			this._version = templateResponse._version;
		}

		public void Redirect(string url)
		{
			if (this._disposed)
			{
				string objectName = base.GetType().ToString();
				throw new ObjectDisposedException(objectName);
			}
			if (this._headersSent)
			{
				string message = HttpListenerResponse.getString_0(107130756);
				throw new InvalidOperationException(message);
			}
			if (url == null)
			{
				throw new ArgumentNullException(HttpListenerResponse.getString_0(107403981));
			}
			if (url.Length == 0)
			{
				string message2 = HttpListenerResponse.getString_0(107140250);
				throw new ArgumentException(message2, HttpListenerResponse.getString_0(107403981));
			}
			Uri redirectLocation;
			if (!Uri.TryCreate(url, UriKind.Absolute, out redirectLocation))
			{
				string message3 = HttpListenerResponse.getString_0(107130110);
				throw new ArgumentException(message3, HttpListenerResponse.getString_0(107403981));
			}
			this._redirectLocation = redirectLocation;
			this._statusCode = 302;
			this._statusDescription = HttpListenerResponse.getString_0(107141630);
		}

		public void SetCookie(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException(HttpListenerResponse.getString_0(107135048));
			}
			if (!this.canSetCookie(cookie))
			{
				string message = HttpListenerResponse.getString_0(107129919);
				throw new ArgumentException(message, HttpListenerResponse.getString_0(107135048));
			}
			this.Cookies.Add(cookie);
		}

		public void SetHeader(string name, string value)
		{
			this.Headers.Set(name, value);
		}

		void IDisposable.Dispose()
		{
			if (!this._disposed)
			{
				this.close(true);
			}
		}

		static HttpListenerResponse()
		{
			Strings.CreateGetStringDelegate(typeof(HttpListenerResponse));
		}

		private bool _closeConnection;

		private Encoding _contentEncoding;

		private long _contentLength;

		private string _contentType;

		private HttpListenerContext _context;

		private CookieCollection _cookies;

		private bool _disposed;

		private WebHeaderCollection _headers;

		private bool _headersSent;

		private bool _keepAlive;

		private ResponseStream _outputStream;

		private Uri _redirectLocation;

		private bool _sendChunked;

		private int _statusCode;

		private string _statusDescription;

		private Version _version;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
