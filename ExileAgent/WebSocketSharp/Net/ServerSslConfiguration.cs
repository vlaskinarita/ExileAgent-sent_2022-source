using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class ServerSslConfiguration
	{
		public ServerSslConfiguration()
		{
			this._enabledSslProtocols = SslProtocols.None;
		}

		public ServerSslConfiguration(ServerSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(ServerSslConfiguration.getString_0(107160012));
			}
			this._checkCertRevocation = configuration._checkCertRevocation;
			this._clientCertRequired = configuration._clientCertRequired;
			this._clientCertValidationCallback = configuration._clientCertValidationCallback;
			this._enabledSslProtocols = configuration._enabledSslProtocols;
			this._serverCert = configuration._serverCert;
		}

		public bool CheckCertificateRevocation
		{
			get
			{
				return this._checkCertRevocation;
			}
			set
			{
				this._checkCertRevocation = value;
			}
		}

		public bool ClientCertificateRequired
		{
			get
			{
				return this._clientCertRequired;
			}
			set
			{
				this._clientCertRequired = value;
			}
		}

		public RemoteCertificateValidationCallback ClientCertificateValidationCallback
		{
			get
			{
				if (this._clientCertValidationCallback == null)
				{
					this._clientCertValidationCallback = new RemoteCertificateValidationCallback(ServerSslConfiguration.defaultValidateClientCertificate);
				}
				return this._clientCertValidationCallback;
			}
			set
			{
				this._clientCertValidationCallback = value;
			}
		}

		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		public X509Certificate2 ServerCertificate
		{
			get
			{
				return this._serverCert;
			}
			set
			{
				this._serverCert = value;
			}
		}

		private static bool defaultValidateClientCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		static ServerSslConfiguration()
		{
			Strings.CreateGetStringDelegate(typeof(ServerSslConfiguration));
		}

		private bool _checkCertRevocation;

		private bool _clientCertRequired;

		private RemoteCertificateValidationCallback _clientCertValidationCallback;

		private SslProtocols _enabledSslProtocols;

		private X509Certificate2 _serverCert;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
