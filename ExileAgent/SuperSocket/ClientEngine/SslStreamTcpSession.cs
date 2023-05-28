using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace SuperSocket.ClientEngine
{
	public sealed class SslStreamTcpSession : AuthenticatedStreamTcpSession
	{
		protected override void StartAuthenticatedStream(Socket client)
		{
			SecurityOption security = base.Security;
			if (security == null)
			{
				throw new Exception(SslStreamTcpSession.getString_2(107309791));
			}
			SslStream sslStream = new SslStream(new NetworkStream(client), false, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate));
			sslStream.BeginAuthenticateAsClient(base.HostName, security.Certificates, security.EnabledSslProtocols, false, new AsyncCallback(this.OnAuthenticated), sslStream);
		}

		private void OnAuthenticated(IAsyncResult result)
		{
			SslStream sslStream = result.AsyncState as SslStream;
			if (sslStream == null)
			{
				base.EnsureSocketClosed();
				this.OnError(new NullReferenceException(SslStreamTcpSession.getString_2(107309778)));
				return;
			}
			try
			{
				sslStream.EndAuthenticateAsClient(result);
			}
			catch (Exception e)
			{
				base.EnsureSocketClosed();
				this.OnError(e);
				return;
			}
			base.OnAuthenticatedStreamConnected(sslStream);
		}

		private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			RemoteCertificateValidationCallback serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
			if (serverCertificateValidationCallback != null)
			{
				return serverCertificateValidationCallback(sender, certificate, chain, sslPolicyErrors);
			}
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (base.Security.AllowNameMismatchCertificate)
			{
				sslPolicyErrors &= ~SslPolicyErrors.RemoteCertificateNameMismatch;
			}
			if (base.Security.AllowCertificateChainErrors)
			{
				sslPolicyErrors &= ~SslPolicyErrors.RemoteCertificateChainErrors;
			}
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (!base.Security.AllowUnstrustedCertificate)
			{
				this.OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (sslPolicyErrors != SslPolicyErrors.None && sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
			{
				this.OnError(new Exception(sslPolicyErrors.ToString()));
				return false;
			}
			if (chain != null && chain.ChainStatus != null)
			{
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if ((!(certificate.Subject == certificate.Issuer) || x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot) && x509ChainStatus.Status != X509ChainStatusFlags.NoError)
					{
						this.OnError(new Exception(sslPolicyErrors.ToString()));
						return false;
					}
				}
			}
			return true;
		}

		static SslStreamTcpSession()
		{
			Strings.CreateGetStringDelegate(typeof(SslStreamTcpSession));
		}

		[NonSerialized]
		internal static GetString getString_2;
	}
}
