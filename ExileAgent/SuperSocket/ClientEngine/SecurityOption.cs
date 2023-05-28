using System;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace SuperSocket.ClientEngine
{
	public sealed class SecurityOption
	{
		public SslProtocols EnabledSslProtocols { get; set; }

		public X509CertificateCollection Certificates { get; set; }

		public bool AllowUnstrustedCertificate { get; set; }

		public bool AllowNameMismatchCertificate { get; set; }

		public bool AllowCertificateChainErrors { get; set; }

		public NetworkCredential Credential { get; set; }

		public SecurityOption() : this(SecurityOption.GetDefaultProtocol(), new X509CertificateCollection())
		{
		}

		public SecurityOption(SslProtocols enabledSslProtocols) : this(enabledSslProtocols, new X509CertificateCollection())
		{
		}

		public SecurityOption(SslProtocols enabledSslProtocols, X509Certificate certificate) : this(enabledSslProtocols, new X509CertificateCollection(new X509Certificate[]
		{
			certificate
		}))
		{
		}

		public SecurityOption(SslProtocols enabledSslProtocols, X509CertificateCollection certificates)
		{
			this.EnabledSslProtocols = enabledSslProtocols;
			this.Certificates = certificates;
		}

		public SecurityOption(NetworkCredential credential)
		{
			this.Credential = credential;
		}

		private static SslProtocols GetDefaultProtocol()
		{
			return SslProtocols.Default;
		}
	}
}
