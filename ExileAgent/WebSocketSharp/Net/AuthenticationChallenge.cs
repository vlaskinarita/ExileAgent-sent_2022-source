using System;
using System.Collections.Specialized;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class AuthenticationChallenge : AuthenticationBase
	{
		private AuthenticationChallenge(AuthenticationSchemes scheme, NameValueCollection parameters) : base(scheme, parameters)
		{
		}

		internal AuthenticationChallenge(AuthenticationSchemes scheme, string realm) : base(scheme, new NameValueCollection())
		{
			this.Parameters[AuthenticationChallenge.getString_1(107160785)] = realm;
			if (scheme == AuthenticationSchemes.Digest)
			{
				this.Parameters[AuthenticationChallenge.getString_1(107160808)] = AuthenticationBase.CreateNonceValue();
				this.Parameters[AuthenticationChallenge.getString_1(107160867)] = AuthenticationChallenge.getString_1(107161188);
				this.Parameters[AuthenticationChallenge.getString_1(107160822)] = AuthenticationChallenge.getString_1(107312403);
			}
		}

		public string Domain
		{
			get
			{
				return this.Parameters[AuthenticationChallenge.getString_1(107132200)];
			}
		}

		public string Stale
		{
			get
			{
				return this.Parameters[AuthenticationChallenge.getString_1(107161215)];
			}
		}

		internal static AuthenticationChallenge CreateBasicChallenge(string realm)
		{
			return new AuthenticationChallenge(AuthenticationSchemes.Basic, realm);
		}

		internal static AuthenticationChallenge CreateDigestChallenge(string realm)
		{
			return new AuthenticationChallenge(AuthenticationSchemes.Digest, realm);
		}

		internal static AuthenticationChallenge Parse(string value)
		{
			string[] array = value.Split(new char[]
			{
				' '
			}, 2);
			AuthenticationChallenge result;
			if (array.Length != 2)
			{
				result = null;
			}
			else
			{
				string a = array[0].ToLower();
				result = ((a == AuthenticationChallenge.getString_1(107161206)) ? new AuthenticationChallenge(AuthenticationSchemes.Basic, AuthenticationBase.ParseParameters(array[1])) : ((a == AuthenticationChallenge.getString_1(107161165)) ? new AuthenticationChallenge(AuthenticationSchemes.Digest, AuthenticationBase.ParseParameters(array[1])) : null));
			}
			return result;
		}

		internal override string ToBasicString()
		{
			return string.Format(AuthenticationChallenge.getString_1(107161156), this.Parameters[AuthenticationChallenge.getString_1(107160785)]);
		}

		internal override string ToDigestString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			string text = this.Parameters[AuthenticationChallenge.getString_1(107132200)];
			if (text != null)
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107161131), this.Parameters[AuthenticationChallenge.getString_1(107160785)], text, this.Parameters[AuthenticationChallenge.getString_1(107160808)]);
			}
			else
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107161070), this.Parameters[AuthenticationChallenge.getString_1(107160785)], this.Parameters[AuthenticationChallenge.getString_1(107160808)]);
			}
			string text2 = this.Parameters[AuthenticationChallenge.getString_1(107160831)];
			if (text2 != null)
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107161057), text2);
			}
			string text3 = this.Parameters[AuthenticationChallenge.getString_1(107161215)];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107160492), text3);
			}
			string text4 = this.Parameters[AuthenticationChallenge.getString_1(107160867)];
			if (text4 != null)
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107160507), text4);
			}
			string text5 = this.Parameters[AuthenticationChallenge.getString_1(107160822)];
			if (text5 != null)
			{
				stringBuilder.AppendFormat(AuthenticationChallenge.getString_1(107160454), text5);
			}
			return stringBuilder.ToString();
		}

		static AuthenticationChallenge()
		{
			Strings.CreateGetStringDelegate(typeof(AuthenticationChallenge));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
