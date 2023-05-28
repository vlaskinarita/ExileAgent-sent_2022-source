using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class NetworkCredential
	{
		static NetworkCredential()
		{
			Strings.CreateGetStringDelegate(typeof(NetworkCredential));
			NetworkCredential._noRoles = new string[0];
		}

		public NetworkCredential(string username, string password) : this(username, password, null, null)
		{
		}

		public NetworkCredential(string username, string password, string domain, params string[] roles)
		{
			if (username == null)
			{
				throw new ArgumentNullException(NetworkCredential.getString_0(107472258));
			}
			if (username.Length == 0)
			{
				throw new ArgumentException(NetworkCredential.getString_0(107140366), NetworkCredential.getString_0(107472258));
			}
			this._username = username;
			this._password = password;
			this._domain = domain;
			this._roles = roles;
		}

		public string Domain
		{
			get
			{
				return this._domain ?? string.Empty;
			}
			internal set
			{
				this._domain = value;
			}
		}

		public string Password
		{
			get
			{
				return this._password ?? string.Empty;
			}
			internal set
			{
				this._password = value;
			}
		}

		public string[] Roles
		{
			get
			{
				return this._roles ?? NetworkCredential._noRoles;
			}
			internal set
			{
				this._roles = value;
			}
		}

		public string Username
		{
			get
			{
				return this._username;
			}
			internal set
			{
				this._username = value;
			}
		}

		private string _domain;

		private static readonly string[] _noRoles;

		private string _password;

		private string[] _roles;

		private string _username;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
