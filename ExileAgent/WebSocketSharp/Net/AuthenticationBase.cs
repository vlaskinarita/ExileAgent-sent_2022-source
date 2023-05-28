using System;
using System.Collections.Specialized;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal abstract class AuthenticationBase
	{
		protected AuthenticationBase(AuthenticationSchemes scheme, NameValueCollection parameters)
		{
			this._scheme = scheme;
			this.Parameters = parameters;
		}

		public string Algorithm
		{
			get
			{
				return this.Parameters[AuthenticationBase.getString_0(107160870)];
			}
		}

		public string Nonce
		{
			get
			{
				return this.Parameters[AuthenticationBase.getString_0(107160811)];
			}
		}

		public string Opaque
		{
			get
			{
				return this.Parameters[AuthenticationBase.getString_0(107160834)];
			}
		}

		public string Qop
		{
			get
			{
				return this.Parameters[AuthenticationBase.getString_0(107160825)];
			}
		}

		public string Realm
		{
			get
			{
				return this.Parameters[AuthenticationBase.getString_0(107160788)];
			}
		}

		public AuthenticationSchemes Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		internal static string CreateNonceValue()
		{
			byte[] array = new byte[16];
			Random random = new Random();
			random.NextBytes(array);
			StringBuilder stringBuilder = new StringBuilder(32);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString(AuthenticationBase.getString_0(107251625)));
			}
			return stringBuilder.ToString();
		}

		internal static NameValueCollection ParseParameters(string value)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string text in value.SplitHeaderValue(new char[]
			{
				','
			}))
			{
				int num = text.IndexOf('=');
				string name = (num > 0) ? text.Substring(0, num).Trim() : null;
				string value2 = (num < 0) ? text.Trim().Trim(new char[]
				{
					'"'
				}) : ((num < text.Length - 1) ? text.Substring(num + 1).Trim().Trim(new char[]
				{
					'"'
				}) : string.Empty);
				nameValueCollection.Add(name, value2);
			}
			return nameValueCollection;
		}

		internal abstract string ToBasicString();

		internal abstract string ToDigestString();

		public override string ToString()
		{
			return (this._scheme == AuthenticationSchemes.Basic) ? this.ToBasicString() : ((this._scheme == AuthenticationSchemes.Digest) ? this.ToDigestString() : string.Empty);
		}

		static AuthenticationBase()
		{
			Strings.CreateGetStringDelegate(typeof(AuthenticationBase));
		}

		private AuthenticationSchemes _scheme;

		internal NameValueCollection Parameters;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
