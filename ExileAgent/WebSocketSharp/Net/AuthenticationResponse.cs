using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class AuthenticationResponse : AuthenticationBase
	{
		private AuthenticationResponse(AuthenticationSchemes scheme, NameValueCollection parameters) : base(scheme, parameters)
		{
		}

		internal AuthenticationResponse(NetworkCredential credentials) : this(AuthenticationSchemes.Basic, new NameValueCollection(), credentials, 0U)
		{
		}

		internal AuthenticationResponse(AuthenticationChallenge challenge, NetworkCredential credentials, uint nonceCount) : this(challenge.Scheme, challenge.Parameters, credentials, nonceCount)
		{
		}

		internal AuthenticationResponse(AuthenticationSchemes scheme, NameValueCollection parameters, NetworkCredential credentials, uint nonceCount) : base(scheme, parameters)
		{
			this.Parameters[AuthenticationResponse.getString_1(107472291)] = credentials.Username;
			this.Parameters[AuthenticationResponse.getString_1(107309735)] = credentials.Password;
			this.Parameters[AuthenticationResponse.getString_1(107245499)] = credentials.Domain;
			this._nonceCount = nonceCount;
			if (scheme == AuthenticationSchemes.Digest)
			{
				this.initAsDigest();
			}
		}

		internal uint NonceCount
		{
			get
			{
				return (this._nonceCount < uint.MaxValue) ? this._nonceCount : 0U;
			}
		}

		public string Cnonce
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107160859)];
			}
		}

		public string Nc
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107160818)];
			}
		}

		public string Password
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107309735)];
			}
		}

		public string Response
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107140444)];
			}
		}

		public string Uri
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107245499)];
			}
		}

		public string UserName
		{
			get
			{
				return this.Parameters[AuthenticationResponse.getString_1(107472291)];
			}
		}

		private static string createA1(string username, string password, string realm)
		{
			return string.Format(AuthenticationResponse.getString_1(107160474), username, realm, password);
		}

		private static string createA1(string username, string password, string realm, string nonce, string cnonce)
		{
			return string.Format(AuthenticationResponse.getString_1(107160474), AuthenticationResponse.hash(AuthenticationResponse.createA1(username, password, realm)), nonce, cnonce);
		}

		private static string createA2(string method, string uri)
		{
			return string.Format(AuthenticationResponse.getString_1(107135614), method, uri);
		}

		private static string createA2(string method, string uri, string entity)
		{
			return string.Format(AuthenticationResponse.getString_1(107160474), method, uri, AuthenticationResponse.hash(entity));
		}

		private static string hash(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(64);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString(AuthenticationResponse.getString_1(107251627)));
			}
			return stringBuilder.ToString();
		}

		private void initAsDigest()
		{
			string text = this.Parameters[AuthenticationResponse.getString_1(107160827)];
			if (text != null)
			{
				if (text.Split(new char[]
				{
					','
				}).Contains((string qop) => qop.Trim().ToLower() == AuthenticationResponse.<>c.getString_0(107312411)))
				{
					this.Parameters[AuthenticationResponse.getString_1(107160827)] = AuthenticationResponse.getString_1(107312408);
					this.Parameters[AuthenticationResponse.getString_1(107160859)] = AuthenticationBase.CreateNonceValue();
					NameValueCollection parameters = this.Parameters;
					string name = AuthenticationResponse.getString_1(107160818);
					string format = AuthenticationResponse.getString_1(107160425);
					uint num = this._nonceCount + 1U;
					this._nonceCount = num;
					parameters[name] = string.Format(format, num);
				}
				else
				{
					this.Parameters[AuthenticationResponse.getString_1(107160827)] = null;
				}
			}
			this.Parameters[AuthenticationResponse.getString_1(107348531)] = AuthenticationResponse.getString_1(107457981);
			this.Parameters[AuthenticationResponse.getString_1(107140444)] = AuthenticationResponse.CreateRequestDigest(this.Parameters);
		}

		internal static string CreateRequestDigest(NameValueCollection parameters)
		{
			string username = parameters[AuthenticationResponse.getString_1(107472291)];
			string password = parameters[AuthenticationResponse.getString_1(107309735)];
			string realm = parameters[AuthenticationResponse.getString_1(107160790)];
			string text = parameters[AuthenticationResponse.getString_1(107160813)];
			string uri = parameters[AuthenticationResponse.getString_1(107245499)];
			string text2 = parameters[AuthenticationResponse.getString_1(107160872)];
			string text3 = parameters[AuthenticationResponse.getString_1(107160827)];
			string text4 = parameters[AuthenticationResponse.getString_1(107160859)];
			string text5 = parameters[AuthenticationResponse.getString_1(107160818)];
			string method = parameters[AuthenticationResponse.getString_1(107348531)];
			string value = (text2 == null || !(text2.ToLower() == AuthenticationResponse.getString_1(107160448))) ? AuthenticationResponse.createA1(username, password, realm) : AuthenticationResponse.createA1(username, password, realm, text, text4);
			string value2 = (text3 == null || !(text3.ToLower() == AuthenticationResponse.getString_1(107160403))) ? AuthenticationResponse.createA2(method, uri) : AuthenticationResponse.createA2(method, uri, parameters[AuthenticationResponse.getString_1(107160781)]);
			string arg = AuthenticationResponse.hash(value);
			string arg2 = (text3 != null) ? string.Format(AuthenticationResponse.getString_1(107160422), new object[]
			{
				text,
				text5,
				text4,
				text3,
				AuthenticationResponse.hash(value2)
			}) : string.Format(AuthenticationResponse.getString_1(107135614), text, AuthenticationResponse.hash(value2));
			return AuthenticationResponse.hash(string.Format(AuthenticationResponse.getString_1(107135614), arg, arg2));
		}

		internal static AuthenticationResponse Parse(string value)
		{
			try
			{
				string[] array = value.Split(new char[]
				{
					' '
				}, 2);
				if (array.Length != 2)
				{
					return null;
				}
				string a = array[0].ToLower();
				return (a == AuthenticationResponse.getString_1(107161211)) ? new AuthenticationResponse(AuthenticationSchemes.Basic, AuthenticationResponse.ParseBasicCredentials(array[1])) : ((a == AuthenticationResponse.getString_1(107161170)) ? new AuthenticationResponse(AuthenticationSchemes.Digest, AuthenticationBase.ParseParameters(array[1])) : null);
			}
			catch
			{
			}
			return null;
		}

		internal static NameValueCollection ParseBasicCredentials(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(value));
			int num = @string.IndexOf(':');
			string text = @string.Substring(0, num);
			string value2 = (num < @string.Length - 1) ? @string.Substring(num + 1) : string.Empty;
			num = text.IndexOf('\\');
			if (num > -1)
			{
				text = text.Substring(num + 1);
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection[AuthenticationResponse.getString_1(107472291)] = text;
			nameValueCollection[AuthenticationResponse.getString_1(107309735)] = value2;
			return nameValueCollection;
		}

		internal override string ToBasicString()
		{
			string s = string.Format(AuthenticationResponse.getString_1(107135614), this.Parameters[AuthenticationResponse.getString_1(107472291)], this.Parameters[AuthenticationResponse.getString_1(107309735)]);
			string str = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
			return AuthenticationResponse.getString_1(107160361) + str;
		}

		internal override string ToDigestString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat(AuthenticationResponse.getString_1(107160384), new object[]
			{
				this.Parameters[AuthenticationResponse.getString_1(107472291)],
				this.Parameters[AuthenticationResponse.getString_1(107160790)],
				this.Parameters[AuthenticationResponse.getString_1(107160813)],
				this.Parameters[AuthenticationResponse.getString_1(107245499)],
				this.Parameters[AuthenticationResponse.getString_1(107140444)]
			});
			string text = this.Parameters[AuthenticationResponse.getString_1(107160836)];
			if (text != null)
			{
				stringBuilder.AppendFormat(AuthenticationResponse.getString_1(107161062), text);
			}
			string text2 = this.Parameters[AuthenticationResponse.getString_1(107160872)];
			if (text2 != null)
			{
				stringBuilder.AppendFormat(AuthenticationResponse.getString_1(107160512), text2);
			}
			string text3 = this.Parameters[AuthenticationResponse.getString_1(107160827)];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(AuthenticationResponse.getString_1(107160283), text3, this.Parameters[AuthenticationResponse.getString_1(107160859)], this.Parameters[AuthenticationResponse.getString_1(107160818)]);
			}
			return stringBuilder.ToString();
		}

		public IIdentity ToIdentity()
		{
			AuthenticationSchemes scheme = base.Scheme;
			IIdentity result;
			if (scheme != AuthenticationSchemes.Basic)
			{
				IIdentity identity = (scheme == AuthenticationSchemes.Digest) ? new HttpDigestIdentity(this.Parameters) : null;
				result = identity;
			}
			else
			{
				IIdentity identity = new HttpBasicIdentity(this.Parameters[AuthenticationResponse.getString_1(107472291)], this.Parameters[AuthenticationResponse.getString_1(107309735)]);
				result = identity;
			}
			return result;
		}

		static AuthenticationResponse()
		{
			Strings.CreateGetStringDelegate(typeof(AuthenticationResponse));
		}

		private uint _nonceCount;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
