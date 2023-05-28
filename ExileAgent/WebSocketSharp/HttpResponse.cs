using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal sealed class HttpResponse : HttpBase
	{
		private HttpResponse(string code, string reason, Version version, NameValueCollection headers) : base(version, headers)
		{
			this._code = code;
			this._reason = reason;
		}

		internal HttpResponse(HttpStatusCode code) : this(code, code.GetDescription())
		{
		}

		internal HttpResponse(HttpStatusCode code, string reason)
		{
			int num = (int)code;
			this..ctor(num.ToString(), reason, HttpVersion.Version11, new NameValueCollection());
			base.Headers[HttpResponse.getString_1(107132867)] = HttpResponse.getString_1(107132993);
		}

		public CookieCollection Cookies
		{
			get
			{
				return base.Headers.GetCookies(true);
			}
		}

		public bool HasConnectionClose
		{
			get
			{
				return base.Headers.Contains(HttpResponse.getString_1(107141573), HttpResponse.getString_1(107141588), StringComparison.OrdinalIgnoreCase);
			}
		}

		public bool IsProxyAuthenticationRequired
		{
			get
			{
				return this._code == HttpResponse.getString_1(107132858);
			}
		}

		public bool IsRedirect
		{
			get
			{
				return this._code == HttpResponse.getString_1(107132885) || this._code == HttpResponse.getString_1(107132880);
			}
		}

		public bool IsUnauthorized
		{
			get
			{
				return this._code == HttpResponse.getString_1(107379293);
			}
		}

		public bool IsWebSocketResponse
		{
			get
			{
				return base.ProtocolVersion > HttpVersion.Version10 && this._code == HttpResponse.getString_1(107132875) && base.Headers.Upgrades(HttpResponse.getString_1(107144082));
			}
		}

		public string Reason
		{
			get
			{
				return this._reason;
			}
		}

		public string StatusCode
		{
			get
			{
				return this._code;
			}
		}

		internal static HttpResponse CreateCloseResponse(HttpStatusCode code)
		{
			HttpResponse httpResponse = new HttpResponse(code);
			httpResponse.Headers[HttpResponse.getString_1(107141573)] = HttpResponse.getString_1(107141588);
			return httpResponse;
		}

		internal static HttpResponse CreateUnauthorizedResponse(string challenge)
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.Unauthorized);
			httpResponse.Headers[HttpResponse.getString_1(107137361)] = challenge;
			return httpResponse;
		}

		internal static HttpResponse CreateWebSocketResponse()
		{
			HttpResponse httpResponse = new HttpResponse(HttpStatusCode.SwitchingProtocols);
			NameValueCollection headers = httpResponse.Headers;
			headers[HttpResponse.getString_1(107141295)] = HttpResponse.getString_1(107144082);
			headers[HttpResponse.getString_1(107141573)] = HttpResponse.getString_1(107141295);
			return httpResponse;
		}

		internal static HttpResponse Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[]
			{
				' '
			}, 3);
			if (array.Length != 3)
			{
				throw new ArgumentException(HttpResponse.getString_1(107133350) + headerParts[0]);
			}
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], true);
			}
			return new HttpResponse(array[1], array[2], new Version(array[0].Substring(5)), webHeaderCollection);
		}

		internal static HttpResponse Read(Stream stream, int millisecondsTimeout)
		{
			return HttpBase.Read<HttpResponse>(stream, new Func<string[], HttpResponse>(HttpResponse.Parse), millisecondsTimeout);
		}

		public void SetCookies(CookieCollection cookies)
		{
			if (cookies != null && cookies.Count != 0)
			{
				NameValueCollection headers = base.Headers;
				foreach (Cookie cookie in cookies.Sorted)
				{
					headers.Add(HttpResponse.getString_1(107142130), cookie.ToResponseString());
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat(HttpResponse.getString_1(107133353), new object[]
			{
				base.ProtocolVersion,
				this._code,
				this._reason,
				HttpResponse.getString_1(107248522)
			});
			NameValueCollection headers = base.Headers;
			foreach (string text in headers.AllKeys)
			{
				stringBuilder.AppendFormat(HttpResponse.getString_1(107132916), text, headers[text], HttpResponse.getString_1(107248522));
			}
			stringBuilder.Append(HttpResponse.getString_1(107248522));
			string entityBody = base.EntityBody;
			if (entityBody.Length > 0)
			{
				stringBuilder.Append(entityBody);
			}
			return stringBuilder.ToString();
		}

		static HttpResponse()
		{
			Strings.CreateGetStringDelegate(typeof(HttpResponse));
		}

		private string _code;

		private string _reason;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
