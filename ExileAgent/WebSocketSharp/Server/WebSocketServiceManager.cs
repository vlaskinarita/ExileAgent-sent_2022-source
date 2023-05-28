using System;
using System.Collections;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Server
{
	public sealed class WebSocketServiceManager
	{
		internal WebSocketServiceManager(Logger log)
		{
			this._log = log;
			this._hosts = new Dictionary<string, WebSocketServiceHost>();
			this._keepClean = true;
			this._state = ServerState.Ready;
			this._sync = ((ICollection)this._hosts).SyncRoot;
			this._waitTime = TimeSpan.FromSeconds(1.0);
		}

		public int Count
		{
			get
			{
				object sync = this._sync;
				int count;
				lock (sync)
				{
					count = this._hosts.Count;
				}
				return count;
			}
		}

		public IEnumerable<WebSocketServiceHost> Hosts
		{
			get
			{
				object sync = this._sync;
				IEnumerable<WebSocketServiceHost> result;
				lock (sync)
				{
					result = this._hosts.Values.ToList<WebSocketServiceHost>();
				}
				return result;
			}
		}

		public WebSocketServiceHost this[string path]
		{
			get
			{
				if (path == null)
				{
					throw new ArgumentNullException(WebSocketServiceManager.getString_0(107251164));
				}
				if (path.Length == 0)
				{
					throw new ArgumentException(WebSocketServiceManager.getString_0(107140651), WebSocketServiceManager.getString_0(107251164));
				}
				if (path[0] != '/')
				{
					string message = WebSocketServiceManager.getString_0(107159450);
					throw new ArgumentException(message, WebSocketServiceManager.getString_0(107251164));
				}
				if (path.IndexOfAny(new char[]
				{
					'?',
					'#'
				}) > -1)
				{
					string message2 = WebSocketServiceManager.getString_0(107159413);
					throw new ArgumentException(message2, WebSocketServiceManager.getString_0(107251164));
				}
				WebSocketServiceHost result;
				this.InternalTryGetServiceHost(path, out result);
				return result;
			}
		}

		public bool KeepClean
		{
			get
			{
				return this._keepClean;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					string message;
					if (!this.canSet(out message))
					{
						this._log.Warn(message);
					}
					else
					{
						foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
						{
							webSocketServiceHost.KeepClean = value;
						}
						this._keepClean = value;
					}
				}
			}
		}

		public IEnumerable<string> Paths
		{
			get
			{
				object sync = this._sync;
				IEnumerable<string> result;
				lock (sync)
				{
					result = this._hosts.Keys.ToList<string>();
				}
				return result;
			}
		}

		public TimeSpan WaitTime
		{
			get
			{
				return this._waitTime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					string message = WebSocketServiceManager.getString_0(107159192);
					throw new ArgumentOutOfRangeException(WebSocketServiceManager.getString_0(107458242), message);
				}
				object sync = this._sync;
				lock (sync)
				{
					string message2;
					if (!this.canSet(out message2))
					{
						this._log.Warn(message2);
					}
					else
					{
						foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
						{
							webSocketServiceHost.WaitTime = value;
						}
						this._waitTime = value;
					}
				}
			}
		}

		private bool canSet(out string message)
		{
			message = null;
			bool result;
			if (this._state == ServerState.Start)
			{
				message = WebSocketServiceManager.getString_0(107160422);
				result = false;
			}
			else if (this._state == ServerState.ShuttingDown)
			{
				message = WebSocketServiceManager.getString_0(107160409);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		internal bool InternalTryGetServiceHost(string path, out WebSocketServiceHost host)
		{
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			bool result;
			lock (sync)
			{
				result = this._hosts.TryGetValue(path, out host);
			}
			return result;
		}

		internal void Start()
		{
			object sync = this._sync;
			lock (sync)
			{
				foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
				{
					webSocketServiceHost.Start();
				}
				this._state = ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			object sync = this._sync;
			lock (sync)
			{
				this._state = ServerState.ShuttingDown;
				foreach (WebSocketServiceHost webSocketServiceHost in this._hosts.Values)
				{
					webSocketServiceHost.Stop(code, reason);
				}
				this._state = ServerState.Stop;
			}
		}

		public void AddService<TBehavior>(string path, Action<TBehavior> initializer) where TBehavior : WebSocketBehavior, new()
		{
			if (path == null)
			{
				throw new ArgumentNullException(WebSocketServiceManager.getString_0(107251164));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(WebSocketServiceManager.getString_0(107140651), WebSocketServiceManager.getString_0(107251164));
			}
			if (path[0] != '/')
			{
				string message = WebSocketServiceManager.getString_0(107159450);
				throw new ArgumentException(message, WebSocketServiceManager.getString_0(107251164));
			}
			if (path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1)
			{
				string message2 = WebSocketServiceManager.getString_0(107159413);
				throw new ArgumentException(message2, WebSocketServiceManager.getString_0(107251164));
			}
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			lock (sync)
			{
				WebSocketServiceHost webSocketServiceHost;
				if (this._hosts.TryGetValue(path, out webSocketServiceHost))
				{
					string message3 = WebSocketServiceManager.getString_0(107159304);
					throw new ArgumentException(message3, WebSocketServiceManager.getString_0(107251164));
				}
				webSocketServiceHost = new WebSocketServiceHost<TBehavior>(path, initializer, this._log);
				if (!this._keepClean)
				{
					webSocketServiceHost.KeepClean = false;
				}
				if (this._waitTime != webSocketServiceHost.WaitTime)
				{
					webSocketServiceHost.WaitTime = this._waitTime;
				}
				if (this._state == ServerState.Start)
				{
					webSocketServiceHost.Start();
				}
				this._hosts.Add(path, webSocketServiceHost);
			}
		}

		public void Clear()
		{
			List<WebSocketServiceHost> list = null;
			object sync = this._sync;
			lock (sync)
			{
				list = this._hosts.Values.ToList<WebSocketServiceHost>();
				this._hosts.Clear();
			}
			foreach (WebSocketServiceHost webSocketServiceHost in list)
			{
				if (webSocketServiceHost.State == ServerState.Start)
				{
					webSocketServiceHost.Stop(1001, string.Empty);
				}
			}
		}

		public bool RemoveService(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(WebSocketServiceManager.getString_0(107251164));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(WebSocketServiceManager.getString_0(107140651), WebSocketServiceManager.getString_0(107251164));
			}
			if (path[0] != '/')
			{
				string message = WebSocketServiceManager.getString_0(107159450);
				throw new ArgumentException(message, WebSocketServiceManager.getString_0(107251164));
			}
			if (path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1)
			{
				string message2 = WebSocketServiceManager.getString_0(107159413);
				throw new ArgumentException(message2, WebSocketServiceManager.getString_0(107251164));
			}
			path = path.TrimSlashFromEnd();
			object sync = this._sync;
			WebSocketServiceHost webSocketServiceHost;
			lock (sync)
			{
				if (!this._hosts.TryGetValue(path, out webSocketServiceHost))
				{
					return false;
				}
				this._hosts.Remove(path);
			}
			if (webSocketServiceHost.State == ServerState.Start)
			{
				webSocketServiceHost.Stop(1001, string.Empty);
			}
			return true;
		}

		public bool TryGetServiceHost(string path, out WebSocketServiceHost host)
		{
			if (path == null)
			{
				throw new ArgumentNullException(WebSocketServiceManager.getString_0(107251164));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(WebSocketServiceManager.getString_0(107140651), WebSocketServiceManager.getString_0(107251164));
			}
			if (path[0] != '/')
			{
				string message = WebSocketServiceManager.getString_0(107159450);
				throw new ArgumentException(message, WebSocketServiceManager.getString_0(107251164));
			}
			if (path.IndexOfAny(new char[]
			{
				'?',
				'#'
			}) > -1)
			{
				string message2 = WebSocketServiceManager.getString_0(107159413);
				throw new ArgumentException(message2, WebSocketServiceManager.getString_0(107251164));
			}
			return this.InternalTryGetServiceHost(path, out host);
		}

		static WebSocketServiceManager()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocketServiceManager));
		}

		private Dictionary<string, WebSocketServiceHost> _hosts;

		private volatile bool _keepClean;

		private Logger _log;

		private volatile ServerState _state;

		private object _sync;

		private TimeSpan _waitTime;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
