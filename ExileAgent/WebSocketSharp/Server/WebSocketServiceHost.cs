using System;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public abstract class WebSocketServiceHost
	{
		protected WebSocketServiceHost(string path, Logger log)
		{
			this._path = path;
			this._log = log;
			this._sessions = new WebSocketSessionManager(log);
		}

		internal ServerState State
		{
			get
			{
				return this._sessions.State;
			}
		}

		protected Logger Log
		{
			get
			{
				return this._log;
			}
		}

		public bool KeepClean
		{
			get
			{
				return this._sessions.KeepClean;
			}
			set
			{
				this._sessions.KeepClean = value;
			}
		}

		public string Path
		{
			get
			{
				return this._path;
			}
		}

		public WebSocketSessionManager Sessions
		{
			get
			{
				return this._sessions;
			}
		}

		public abstract Type BehaviorType { get; }

		public TimeSpan WaitTime
		{
			get
			{
				return this._sessions.WaitTime;
			}
			set
			{
				this._sessions.WaitTime = value;
			}
		}

		internal void Start()
		{
			this._sessions.Start();
		}

		internal void StartSession(WebSocketContext context)
		{
			this.CreateSession().Start(context, this._sessions);
		}

		internal void Stop(ushort code, string reason)
		{
			this._sessions.Stop(code, reason);
		}

		protected abstract WebSocketBehavior CreateSession();

		private Logger _log;

		private string _path;

		private WebSocketSessionManager _sessions;
	}
}
