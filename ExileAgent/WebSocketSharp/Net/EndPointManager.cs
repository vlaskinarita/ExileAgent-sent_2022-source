using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Net
{
	internal sealed class EndPointManager
	{
		static EndPointManager()
		{
			Strings.CreateGetStringDelegate(typeof(EndPointManager));
			EndPointManager._endpoints = new Dictionary<IPEndPoint, EndPointListener>();
		}

		private EndPointManager()
		{
		}

		private static void addPrefix(string uriPrefix, HttpListener listener)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(uriPrefix, listener);
			IPAddress ipaddress = EndPointManager.convertToIPAddress(httpListenerPrefix.Host);
			if (ipaddress == null)
			{
				string message = EndPointManager.getString_0(107131587);
				throw new HttpListenerException(87, message);
			}
			if (!ipaddress.IsLocal())
			{
				string message2 = EndPointManager.getString_0(107131587);
				throw new HttpListenerException(87, message2);
			}
			int num;
			if (!int.TryParse(httpListenerPrefix.Port, out num))
			{
				string message3 = EndPointManager.getString_0(107131562);
				throw new HttpListenerException(87, message3);
			}
			if (!num.IsPortNumber())
			{
				string message4 = EndPointManager.getString_0(107131562);
				throw new HttpListenerException(87, message4);
			}
			string path = httpListenerPrefix.Path;
			if (path.IndexOf('%') != -1)
			{
				string message5 = EndPointManager.getString_0(107131505);
				throw new HttpListenerException(87, message5);
			}
			if (path.IndexOf(EndPointManager.getString_0(107235501), StringComparison.Ordinal) != -1)
			{
				string message6 = EndPointManager.getString_0(107131505);
				throw new HttpListenerException(87, message6);
			}
			IPEndPoint ipendPoint = new IPEndPoint(ipaddress, num);
			EndPointListener endPointListener;
			if (EndPointManager._endpoints.TryGetValue(ipendPoint, out endPointListener))
			{
				if (endPointListener.IsSecure ^ httpListenerPrefix.IsSecure)
				{
					string message7 = EndPointManager.getString_0(107131416);
					throw new HttpListenerException(87, message7);
				}
			}
			else
			{
				endPointListener = new EndPointListener(ipendPoint, httpListenerPrefix.IsSecure, listener.CertificateFolderPath, listener.SslConfiguration, listener.ReuseAddress);
				EndPointManager._endpoints.Add(ipendPoint, endPointListener);
			}
			endPointListener.AddPrefix(httpListenerPrefix);
		}

		private static IPAddress convertToIPAddress(string hostname)
		{
			IPAddress result;
			if (hostname == EndPointManager.getString_0(107449758))
			{
				result = IPAddress.Any;
			}
			else if (hostname == EndPointManager.getString_0(107398595))
			{
				result = IPAddress.Any;
			}
			else
			{
				result = hostname.ToIPAddress();
			}
			return result;
		}

		private static void removePrefix(string uriPrefix, HttpListener listener)
		{
			HttpListenerPrefix httpListenerPrefix = new HttpListenerPrefix(uriPrefix, listener);
			IPAddress ipaddress = EndPointManager.convertToIPAddress(httpListenerPrefix.Host);
			int num;
			if (ipaddress != null && ipaddress.IsLocal() && int.TryParse(httpListenerPrefix.Port, out num) && num.IsPortNumber())
			{
				string path = httpListenerPrefix.Path;
				if (path.IndexOf('%') == -1 && path.IndexOf(EndPointManager.getString_0(107235501), StringComparison.Ordinal) == -1)
				{
					IPEndPoint key = new IPEndPoint(ipaddress, num);
					EndPointListener endPointListener;
					if (EndPointManager._endpoints.TryGetValue(key, out endPointListener) && !(endPointListener.IsSecure ^ httpListenerPrefix.IsSecure))
					{
						endPointListener.RemovePrefix(httpListenerPrefix);
					}
				}
			}
		}

		internal static bool RemoveEndPoint(IPEndPoint endpoint)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			bool result;
			lock (syncRoot)
			{
				result = EndPointManager._endpoints.Remove(endpoint);
			}
			return result;
		}

		public static void AddListener(HttpListener listener)
		{
			List<string> list = new List<string>();
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				try
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.addPrefix(text, listener);
						list.Add(text);
					}
				}
				catch
				{
					foreach (string uriPrefix in list)
					{
						EndPointManager.removePrefix(uriPrefix, listener);
					}
					throw;
				}
			}
		}

		public static void AddPrefix(string uriPrefix, HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				EndPointManager.addPrefix(uriPrefix, listener);
			}
		}

		public static void RemoveListener(HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				foreach (string uriPrefix in listener.Prefixes)
				{
					EndPointManager.removePrefix(uriPrefix, listener);
				}
			}
		}

		public static void RemovePrefix(string uriPrefix, HttpListener listener)
		{
			object syncRoot = ((ICollection)EndPointManager._endpoints).SyncRoot;
			lock (syncRoot)
			{
				EndPointManager.removePrefix(uriPrefix, listener);
			}
		}

		private static readonly Dictionary<IPEndPoint, EndPointListener> _endpoints;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
