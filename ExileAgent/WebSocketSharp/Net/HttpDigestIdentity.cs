using System;
using System.Collections.Specialized;
using System.Security.Principal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpDigestIdentity : GenericIdentity
	{
		internal HttpDigestIdentity(NameValueCollection parameters) : base(parameters[HttpDigestIdentity.getString_0(107472252)], HttpDigestIdentity.getString_0(107245437))
		{
			this._parameters = parameters;
		}

		public string Algorithm
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160833)];
			}
		}

		public string Cnonce
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160820)];
			}
		}

		public string Nc
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160779)];
			}
		}

		public string Nonce
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160774)];
			}
		}

		public string Opaque
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160797)];
			}
		}

		public string Qop
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160788)];
			}
		}

		public string Realm
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107160751)];
			}
		}

		public string Response
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107140405)];
			}
		}

		public string Uri
		{
			get
			{
				return this._parameters[HttpDigestIdentity.getString_0(107245460)];
			}
		}

		internal bool IsValid(string password, string realm, string method, string entity)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(this._parameters);
			nameValueCollection[HttpDigestIdentity.getString_0(107309696)] = password;
			nameValueCollection[HttpDigestIdentity.getString_0(107160751)] = realm;
			nameValueCollection[HttpDigestIdentity.getString_0(107348492)] = method;
			nameValueCollection[HttpDigestIdentity.getString_0(107160742)] = entity;
			string b = AuthenticationResponse.CreateRequestDigest(nameValueCollection);
			return this._parameters[HttpDigestIdentity.getString_0(107140405)] == b;
		}

		static HttpDigestIdentity()
		{
			Strings.CreateGetStringDelegate(typeof(HttpDigestIdentity));
		}

		private NameValueCollection _parameters;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
