using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class EndPointListener
	{
		static EndPointListener()
		{
			Strings.CreateGetStringDelegate(typeof(EndPointListener));
			EndPointListener._defaultCertFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		}

		internal EndPointListener(IPEndPoint endpoint, bool secure, string certificateFolderPath, ServerSslConfiguration sslConfig, bool reuseAddress)
		{
			this._endpoint = endpoint;
			if (secure)
			{
				X509Certificate2 certificate = EndPointListener.getCertificate(endpoint.Port, certificateFolderPath, sslConfig.ServerCertificate);
				if (certificate == null)
				{
					string message = EndPointListener.getString_0(107132298);
					throw new ArgumentException(message);
				}
				this._secure = true;
				this._sslConfig = new ServerSslConfiguration(sslConfig);
				this._sslConfig.ServerCertificate = certificate;
			}
			this._prefixes = new List<HttpListenerPrefix>();
			this._connections = new Dictionary<HttpConnection, HttpConnection>();
			this._connectionsSync = ((ICollection)this._connections).SyncRoot;
			this._socket = new Socket(endpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (reuseAddress)
			{
				this._socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			this._socket.Bind(endpoint);
			this._socket.Listen(500);
			this._socket.BeginAccept(new AsyncCallback(EndPointListener.onAccept), this);
		}

		public IPAddress Address
		{
			get
			{
				return this._endpoint.Address;
			}
		}

		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		public int Port
		{
			get
			{
				return this._endpoint.Port;
			}
		}

		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				return this._sslConfig;
			}
		}

		private static void addSpecial(List<HttpListenerPrefix> prefixes, HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
			{
				if (httpListenerPrefix.Path == path)
				{
					string message = EndPointListener.getString_0(107132213);
					throw new HttpListenerException(87, message);
				}
			}
			prefixes.Add(prefix);
		}

		private void clearConnections()
		{
			HttpConnection[] array = null;
			object connectionsSync = this._connectionsSync;
			lock (connectionsSync)
			{
				int count = this._connections.Count;
				if (count == 0)
				{
					return;
				}
				array = new HttpConnection[count];
				Dictionary<HttpConnection, HttpConnection>.ValueCollection values = this._connections.Values;
				values.CopyTo(array, 0);
				this._connections.Clear();
			}
			foreach (HttpConnection httpConnection in array)
			{
				httpConnection.Close(true);
			}
		}

		private static RSACryptoServiceProvider createRSAFromFile(string path)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			byte[] keyBlob = File.ReadAllBytes(path);
			rsacryptoServiceProvider.ImportCspBlob(keyBlob);
			return rsacryptoServiceProvider;
		}

		private static X509Certificate2 getCertificate(int port, string folderPath, X509Certificate2 defaultCertificate)
		{
			if (folderPath == null || folderPath.Length == 0)
			{
				folderPath = EndPointListener._defaultCertFolderPath;
			}
			try
			{
				string text = Path.Combine(folderPath, string.Format(EndPointListener.getString_0(107132204), port));
				string path = Path.Combine(folderPath, string.Format(EndPointListener.getString_0(107132159), port));
				if (File.Exists(text) && File.Exists(path))
				{
					return new X509Certificate2(text)
					{
						PrivateKey = EndPointListener.createRSAFromFile(path)
					};
				}
			}
			catch
			{
			}
			return defaultCertificate;
		}

		private void leaveIfNoPrefix()
		{
			if (this._prefixes.Count <= 0)
			{
				List<HttpListenerPrefix> list = this._unhandled;
				if (list == null || list.Count <= 0)
				{
					list = this._all;
					if (list == null || list.Count <= 0)
					{
						this.Close();
					}
				}
			}
		}

		private static void onAccept(IAsyncResult asyncResult)
		{
			EndPointListener endPointListener = (EndPointListener)asyncResult.AsyncState;
			Socket socket = null;
			try
			{
				socket = endPointListener._socket.EndAccept(asyncResult);
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			catch (Exception)
			{
			}
			try
			{
				endPointListener._socket.BeginAccept(new AsyncCallback(EndPointListener.onAccept), endPointListener);
			}
			catch (Exception)
			{
				if (socket != null)
				{
					socket.Close();
				}
				return;
			}
			if (socket != null)
			{
				EndPointListener.processAccepted(socket, endPointListener);
			}
		}

		private static void processAccepted(Socket socket, EndPointListener listener)
		{
			HttpConnection httpConnection = null;
			try
			{
				httpConnection = new HttpConnection(socket, listener);
			}
			catch (Exception)
			{
				socket.Close();
				return;
			}
			object connectionsSync = listener._connectionsSync;
			lock (connectionsSync)
			{
				listener._connections.Add(httpConnection, httpConnection);
			}
			httpConnection.BeginReadRequest();
		}

		private static bool removeSpecial(List<HttpListenerPrefix> prefixes, HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			int count = prefixes.Count;
			for (int i = 0; i < count; i++)
			{
				if (prefixes[i].Path == path)
				{
					prefixes.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		private static HttpListener searchHttpListenerFromSpecial(string path, List<HttpListenerPrefix> prefixes)
		{
			HttpListener result;
			if (prefixes == null)
			{
				result = null;
			}
			else
			{
				HttpListener httpListener = null;
				int num = -1;
				foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
				{
					string path2 = httpListenerPrefix.Path;
					int length = path2.Length;
					if (length >= num && path.StartsWith(path2, StringComparison.Ordinal))
					{
						num = length;
						httpListener = httpListenerPrefix.Listener;
					}
				}
				result = httpListener;
			}
			return result;
		}

		internal static bool CertificateExists(int port, string folderPath)
		{
			if (folderPath == null || folderPath.Length == 0)
			{
				folderPath = EndPointListener._defaultCertFolderPath;
			}
			string path = Path.Combine(folderPath, string.Format(EndPointListener.getString_0(107132204), port));
			string path2 = Path.Combine(folderPath, string.Format(EndPointListener.getString_0(107132159), port));
			return File.Exists(path) && File.Exists(path2);
		}

		internal void RemoveConnection(HttpConnection connection)
		{
			object connectionsSync = this._connectionsSync;
			lock (connectionsSync)
			{
				this._connections.Remove(connection);
			}
		}

		internal bool TrySearchHttpListener(Uri uri, out HttpListener listener)
		{
			listener = null;
			bool result;
			if (uri == null)
			{
				result = false;
			}
			else
			{
				string host = uri.Host;
				bool flag = Uri.CheckHostName(host) == UriHostNameType.Dns;
				string b = uri.Port.ToString();
				string text = HttpUtility.UrlDecode(uri.AbsolutePath);
				if (text[text.Length - 1] != '/')
				{
					text += EndPointListener.getString_0(107380681);
				}
				if (host != null && host.Length > 0)
				{
					List<HttpListenerPrefix> prefixes = this._prefixes;
					int num = -1;
					foreach (HttpListenerPrefix httpListenerPrefix in prefixes)
					{
						if (flag)
						{
							string host2 = httpListenerPrefix.Host;
							if (Uri.CheckHostName(host2) == UriHostNameType.Dns && host2 != host)
							{
								continue;
							}
						}
						if (!(httpListenerPrefix.Port != b))
						{
							string path = httpListenerPrefix.Path;
							int length = path.Length;
							if (length >= num && text.StartsWith(path, StringComparison.Ordinal))
							{
								num = length;
								listener = httpListenerPrefix.Listener;
							}
						}
					}
					if (num != -1)
					{
						return true;
					}
				}
				listener = EndPointListener.searchHttpListenerFromSpecial(text, this._unhandled);
				if (listener != null)
				{
					result = true;
				}
				else
				{
					listener = EndPointListener.searchHttpListenerFromSpecial(text, this._all);
					result = (listener != null);
				}
			}
			return result;
		}

		public void AddPrefix(HttpListenerPrefix prefix)
		{
			if (prefix.Host == EndPointListener.getString_0(107449756))
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._unhandled;
					list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
					EndPointListener.addSpecial(list2, prefix);
				}
				while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._unhandled, list2, list) != list);
			}
			else if (prefix.Host == EndPointListener.getString_0(107398593))
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._all;
					list2 = ((list != null) ? new List<HttpListenerPrefix>(list) : new List<HttpListenerPrefix>());
					EndPointListener.addSpecial(list2, prefix);
				}
				while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._all, list2, list) != list);
			}
			else
			{
				List<HttpListenerPrefix> list;
				int num;
				for (;;)
				{
					list = this._prefixes;
					num = list.IndexOf(prefix);
					if (num > -1)
					{
						break;
					}
					if (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._prefixes, new List<HttpListenerPrefix>(list)
					{
						prefix
					}, list) == list)
					{
						return;
					}
				}
				if (list[num].Listener != prefix.Listener)
				{
					string message = string.Format(EndPointListener.getString_0(107132178), prefix);
					throw new HttpListenerException(87, message);
				}
			}
		}

		public void Close()
		{
			this._socket.Close();
			this.clearConnections();
			EndPointManager.RemoveEndPoint(this._endpoint);
		}

		public void RemovePrefix(HttpListenerPrefix prefix)
		{
			if (prefix.Host == EndPointListener.getString_0(107449756))
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._unhandled;
					if (list == null)
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
				}
				while (EndPointListener.removeSpecial(list2, prefix) && Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._unhandled, list2, list) != list);
				this.leaveIfNoPrefix();
			}
			else if (prefix.Host == EndPointListener.getString_0(107398593))
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._all;
					if (list == null)
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
				}
				while (EndPointListener.removeSpecial(list2, prefix) && Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._all, list2, list) != list);
				this.leaveIfNoPrefix();
			}
			else
			{
				List<HttpListenerPrefix> list;
				List<HttpListenerPrefix> list2;
				do
				{
					list = this._prefixes;
					if (!list.Contains(prefix))
					{
						break;
					}
					list2 = new List<HttpListenerPrefix>(list);
					list2.Remove(prefix);
				}
				while (Interlocked.CompareExchange<List<HttpListenerPrefix>>(ref this._prefixes, list2, list) != list);
				this.leaveIfNoPrefix();
			}
		}

		private List<HttpListenerPrefix> _all;

		private Dictionary<HttpConnection, HttpConnection> _connections;

		private object _connectionsSync;

		private static readonly string _defaultCertFolderPath;

		private IPEndPoint _endpoint;

		private List<HttpListenerPrefix> _prefixes;

		private bool _secure;

		private Socket _socket;

		private ServerSslConfiguration _sslConfig;

		private List<HttpListenerPrefix> _unhandled;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
