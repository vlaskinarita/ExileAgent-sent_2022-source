using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	public sealed class ClientSslConfiguration
	{
		public ClientSslConfiguration(string targetHost)
		{
			if (targetHost == null)
			{
				throw new ArgumentNullException(ClientSslConfiguration.getString_0(107160567));
			}
			if (targetHost.Length == 0)
			{
				throw new ArgumentException(ClientSslConfiguration.getString_0(107140417), ClientSslConfiguration.getString_0(107160567));
			}
			this._targetHost = targetHost;
			this._enabledSslProtocols = SslProtocols.None;
		}

		public ClientSslConfiguration(ClientSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(ClientSslConfiguration.getString_0(107160006));
			}
			this._checkCertRevocation = configuration._checkCertRevocation;
			this._clientCertSelectionCallback = configuration._clientCertSelectionCallback;
			this._clientCerts = configuration._clientCerts;
			this._enabledSslProtocols = configuration._enabledSslProtocols;
			this._serverCertValidationCallback = configuration._serverCertValidationCallback;
			this._targetHost = configuration._targetHost;
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

		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this._clientCerts;
			}
			set
			{
				this._clientCerts = value;
			}
		}

		public LocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			get
			{
				if (this._clientCertSelectionCallback == null)
				{
					this._clientCertSelectionCallback = new LocalCertificateSelectionCallback(ClientSslConfiguration.defaultSelectClientCertificate);
				}
				return this._clientCertSelectionCallback;
			}
			set
			{
				this._clientCertSelectionCallback = value;
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

		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (this._serverCertValidationCallback == null)
				{
					this._serverCertValidationCallback = new RemoteCertificateValidationCallback(ClientSslConfiguration.defaultValidateServerCertificate);
				}
				return this._serverCertValidationCallback;
			}
			set
			{
				this._serverCertValidationCallback = value;
			}
		}

		public string TargetHost
		{
			get
			{
				return this._targetHost;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(ClientSslConfiguration.getString_0(107458008));
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(ClientSslConfiguration.getString_0(107140417), ClientSslConfiguration.getString_0(107458008));
				}
				this._targetHost = value;
			}
		}

		private static X509Certificate defaultSelectClientCertificate(object sender, string targetHost, X509CertificateCollection clientCertificates, X509Certificate serverCertificate, string[] acceptableIssuers)
		{
			return null;
		}

		private static bool defaultValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		static ClientSslConfiguration()
		{
			Strings.CreateGetStringDelegate(typeof(ClientSslConfiguration));
		}

		private bool _checkCertRevocation;

		private LocalCertificateSelectionCallback _clientCertSelectionCallback;

		private X509CertificateCollection _clientCerts;

		private SslProtocols _enabledSslProtocols;

		private RemoteCertificateValidationCallback _serverCertValidationCallback;

		private string _targetHost;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
