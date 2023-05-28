using System;
using System.Security.Principal;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class HttpBasicIdentity : GenericIdentity
	{
		internal HttpBasicIdentity(string username, string password) : base(username, HttpBasicIdentity.getString_0(107245412))
		{
			this._password = password;
		}

		public string Password
		{
			get
			{
				return this._password;
			}
		}

		static HttpBasicIdentity()
		{
			Strings.CreateGetStringDelegate(typeof(HttpBasicIdentity));
		}

		private string _password;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
