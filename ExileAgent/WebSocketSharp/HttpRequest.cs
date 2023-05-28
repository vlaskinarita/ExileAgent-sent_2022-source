using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal sealed class HttpRequest : HttpBase
	{
		private HttpRequest(string method, string uri, Version version, NameValueCollection headers) : base(version, headers)
		{
			this._method = method;
			this._uri = uri;
		}

		internal HttpRequest(string method, string uri) : this(method, uri, HttpVersion.Version11, new NameValueCollection())
		{
			base.Headers[HttpRequest.getString_1(107325979)] = HttpRequest.getString_1(107132990);
		}

		public AuthenticationResponse AuthenticationResponse
		{
			get
			{
				string text = base.Headers[HttpRequest.getString_1(107137805)];
				return (text == null || text.Length <= 0) ? null : AuthenticationResponse.Parse(text);
			}
		}

		public CookieCollection Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = base.Headers.GetCookies(false);
				}
				return this._cookies;
			}
		}

		public string HttpMethod
		{
			get
			{
				return this._method;
			}
		}

		public bool IsWebSocketRequest
		{
			get
			{
				return this._method == HttpRequest.getString_1(107457704) && base.ProtocolVersion > HttpVersion.Version10 && base.Headers.Upgrades(HttpRequest.getString_1(107144079));
			}
		}

		public string RequestUri
		{
			get
			{
				return this._uri;
			}
		}

		internal static HttpRequest CreateConnectRequest(Uri uri)
		{
			string dnsSafeHost = uri.DnsSafeHost;
			int port = uri.Port;
			string text = string.Format(HttpRequest.getString_1(107135337), dnsSafeHost, port);
			HttpRequest httpRequest = new HttpRequest(HttpRequest.getString_1(107142139), text);
			httpRequest.Headers[HttpRequest.getString_1(107132961)] = ((port == 80) ? dnsSafeHost : text);
			return httpRequest;
		}

		internal static HttpRequest CreateWebSocketRequest(Uri uri)
		{
			HttpRequest httpRequest = new HttpRequest(HttpRequest.getString_1(107457704), uri.PathAndQuery);
			NameValueCollection headers = httpRequest.Headers;
			int port = uri.Port;
			string scheme = uri.Scheme;
			headers[HttpRequest.getString_1(107132961)] = (((port != 80 || !(scheme == HttpRequest.getString_1(107141969))) && (port != 443 || !(scheme == HttpRequest.getString_1(107363573)))) ? uri.Authority : uri.DnsSafeHost);
			headers[HttpRequest.getString_1(107141292)] = HttpRequest.getString_1(107144079);
			headers[HttpRequest.getString_1(107141570)] = HttpRequest.getString_1(107141292);
			return httpRequest;
		}

		internal HttpResponse GetResponse(Stream stream, int millisecondsTimeout)
		{
			byte[] array = base.ToByteArray();
			stream.Write(array, 0, array.Length);
			return HttpBase.Read<HttpResponse>(stream, new Func<string[], HttpResponse>(HttpResponse.Parse), millisecondsTimeout);
		}

		internal static HttpRequest Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[]
			{
				' '
			}, 3);
			if (array.Length != 3)
			{
				throw new ArgumentException(HttpRequest.getString_1(107132952) + headerParts[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], false);
			}
			return new HttpRequest(array[0], array[1], new Version(array[2].Substring(5)), webHeaderCollection);
		}

		internal static HttpRequest Read(Stream stream, int millisecondsTimeout)
		{
			return HttpBase.Read<HttpRequest>(stream, new Func<string[], HttpRequest>(HttpRequest.Parse), millisecondsTimeout);
		}

		public void SetCookies(CookieCollection cookies)
		{
			if (cookies != null && cookies.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				foreach (Cookie cookie in cookies.Sorted)
				{
					if (!cookie.Expired)
					{
						stringBuilder.AppendFormat(HttpRequest.getString_1(107132919), cookie.ToString());
					}
				}
				int length = stringBuilder.Length;
				if (length > 2)
				{
					stringBuilder.Length = length - 2;
					base.Headers[HttpRequest.getString_1(107142104)] = stringBuilder.ToString();
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat(HttpRequest.getString_1(107132942), new object[]
			{
				this._method,
				this._uri,
				base.ProtocolVersion,
				HttpRequest.getString_1(107248519)
			});
			NameValueCollection headers = base.Headers;
			foreach (string text in headers.AllKeys)
			{
				stringBuilder.AppendFormat(HttpRequest.getString_1(107132913), text, headers[text], HttpRequest.getString_1(107248519));
			}
			stringBuilder.Append(HttpRequest.getString_1(107248519));
			string entityBody = base.EntityBody;
			if (entityBody.Length > 0)
			{
				stringBuilder.Append(entityBody);
			}
			return stringBuilder.ToString();
		}

		static HttpRequest()
		{
			Strings.CreateGetStringDelegate(typeof(HttpRequest));
		}

		private CookieCollection _cookies;

		private string _method;

		private string _uri;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
