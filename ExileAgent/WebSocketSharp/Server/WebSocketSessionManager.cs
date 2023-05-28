using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocketSharp.Server
{
	public sealed class WebSocketSessionManager
	{
		static WebSocketSessionManager()
		{
			Strings.CreateGetStringDelegate(typeof(WebSocketSessionManager));
			WebSocketSessionManager._emptyPingFrameAsBytes = WebSocketFrame.CreatePingFrame(false).ToArray();
		}

		internal WebSocketSessionManager(Logger log)
		{
			this._log = log;
			this._forSweep = new object();
			this._keepClean = true;
			this._sessions = new Dictionary<string, IWebSocketSession>();
			this._state = ServerState.Ready;
			this._sync = ((ICollection)this._sessions).SyncRoot;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.setSweepTimer(60000.0);
		}

		internal ServerState State
		{
			get
			{
				return this._state;
			}
		}

		public IEnumerable<string> ActiveIDs
		{
			get
			{
				WebSocketSessionManager.<get_ActiveIDs>d__15 <get_ActiveIDs>d__ = new WebSocketSessionManager.<get_ActiveIDs>d__15(-2);
				<get_ActiveIDs>d__.<>4__this = this;
				return <get_ActiveIDs>d__;
			}
		}

		public int Count
		{
			get
			{
				object sync = this._sync;
				int count;
				lock (sync)
				{
					count = this._sessions.Count;
				}
				return count;
			}
		}

		public IEnumerable<string> IDs
		{
			get
			{
				IEnumerable<string> result;
				if (this._state != ServerState.Start)
				{
					result = Enumerable.Empty<string>();
				}
				else
				{
					object sync = this._sync;
					lock (sync)
					{
						if (this._state != ServerState.Start)
						{
							result = Enumerable.Empty<string>();
						}
						else
						{
							result = this._sessions.Keys.ToList<string>();
						}
					}
				}
				return result;
			}
		}

		public IEnumerable<string> InactiveIDs
		{
			get
			{
				WebSocketSessionManager.<get_InactiveIDs>d__21 <get_InactiveIDs>d__ = new WebSocketSessionManager.<get_InactiveIDs>d__21(-2);
				<get_InactiveIDs>d__.<>4__this = this;
				return <get_InactiveIDs>d__;
			}
		}

		public IWebSocketSession this[string id]
		{
			get
			{
				if (id == null)
				{
					throw new ArgumentNullException(WebSocketSessionManager.getString_0(107459975));
				}
				if (id.Length == 0)
				{
					throw new ArgumentException(WebSocketSessionManager.getString_0(107140619), WebSocketSessionManager.getString_0(107459975));
				}
				IWebSocketSession result;
				this.tryGetSession(id, out result);
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
					if (this.canSet())
					{
						this._keepClean = value;
					}
				}
			}
		}

		public IEnumerable<IWebSocketSession> Sessions
		{
			get
			{
				IEnumerable<IWebSocketSession> result;
				if (this._state != ServerState.Start)
				{
					result = Enumerable.Empty<IWebSocketSession>();
				}
				else
				{
					object sync = this._sync;
					lock (sync)
					{
						if (this._state != ServerState.Start)
						{
							result = Enumerable.Empty<IWebSocketSession>();
						}
						else
						{
							result = this._sessions.Values.ToList<IWebSocketSession>();
						}
					}
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
					string message = WebSocketSessionManager.getString_0(107159160);
					throw new ArgumentOutOfRangeException(WebSocketSessionManager.getString_0(107458210), message);
				}
				object sync = this._sync;
				lock (sync)
				{
					if (this.canSet())
					{
						this._waitTime = value;
					}
				}
			}
		}

		private void broadcast(Opcode opcode, byte[] data, Action completed)
		{
			Dictionary<CompressionMethod, byte[]> dictionary = new Dictionary<CompressionMethod, byte[]>();
			try
			{
				foreach (IWebSocketSession webSocketSession in this.Sessions)
				{
					if (this._state != ServerState.Start)
					{
						this._log.Error(WebSocketSessionManager.getString_0(107159131));
						break;
					}
					webSocketSession.Context.WebSocket.Send(opcode, data, dictionary);
				}
				if (completed != null)
				{
					completed();
				}
			}
			catch (Exception ex)
			{
				this._log.Error(ex.Message);
				this._log.Debug(ex.ToString());
			}
			finally
			{
				dictionary.Clear();
			}
		}

		private void broadcast(Opcode opcode, Stream stream, Action completed)
		{
			Dictionary<CompressionMethod, Stream> dictionary = new Dictionary<CompressionMethod, Stream>();
			try
			{
				foreach (IWebSocketSession webSocketSession in this.Sessions)
				{
					if (this._state != ServerState.Start)
					{
						this._log.Error(WebSocketSessionManager.getString_0(107159131));
						break;
					}
					webSocketSession.Context.WebSocket.Send(opcode, stream, dictionary);
				}
				if (completed != null)
				{
					completed();
				}
			}
			catch (Exception ex)
			{
				this._log.Error(ex.Message);
				this._log.Debug(ex.ToString());
			}
			finally
			{
				foreach (Stream stream2 in dictionary.Values)
				{
					stream2.Dispose();
				}
				dictionary.Clear();
			}
		}

		private void broadcastAsync(Opcode opcode, byte[] data, Action completed)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.broadcast(opcode, data, completed);
			});
		}

		private void broadcastAsync(Opcode opcode, Stream stream, Action completed)
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.broadcast(opcode, stream, completed);
			});
		}

		private Dictionary<string, bool> broadping(byte[] frameAsBytes)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (IWebSocketSession webSocketSession in this.Sessions)
			{
				if (this._state != ServerState.Start)
				{
					this._log.Error(WebSocketSessionManager.getString_0(107159131));
					break;
				}
				bool value = webSocketSession.Context.WebSocket.Ping(frameAsBytes, this._waitTime);
				dictionary.Add(webSocketSession.ID, value);
			}
			return dictionary;
		}

		private bool canSet()
		{
			return this._state == ServerState.Ready || this._state == ServerState.Stop;
		}

		private static string createID()
		{
			return Guid.NewGuid().ToString(WebSocketSessionManager.getString_0(107301250));
		}

		private void setSweepTimer(double interval)
		{
			this._sweepTimer = new Timer(interval);
			this._sweepTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				this.Sweep();
			};
		}

		private void stop(PayloadData payloadData, bool send)
		{
			byte[] frameAsBytes = send ? WebSocketFrame.CreateCloseFrame(payloadData, false).ToArray() : null;
			object sync = this._sync;
			lock (sync)
			{
				this._state = ServerState.ShuttingDown;
				this._sweepTimer.Enabled = false;
				foreach (IWebSocketSession webSocketSession in this._sessions.Values.ToList<IWebSocketSession>())
				{
					webSocketSession.Context.WebSocket.Close(payloadData, frameAsBytes);
				}
				this._state = ServerState.Stop;
			}
		}

		private bool tryGetSession(string id, out IWebSocketSession session)
		{
			session = null;
			bool result;
			if (this._state != ServerState.Start)
			{
				result = false;
			}
			else
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._state != ServerState.Start)
					{
						result = false;
					}
					else
					{
						result = this._sessions.TryGetValue(id, out session);
					}
				}
			}
			return result;
		}

		internal string Add(IWebSocketSession session)
		{
			object sync = this._sync;
			string result;
			lock (sync)
			{
				if (this._state != ServerState.Start)
				{
					result = null;
				}
				else
				{
					string text = WebSocketSessionManager.createID();
					this._sessions.Add(text, session);
					result = text;
				}
			}
			return result;
		}

		internal bool Remove(string id)
		{
			object sync = this._sync;
			bool result;
			lock (sync)
			{
				result = this._sessions.Remove(id);
			}
			return result;
		}

		internal void Start()
		{
			object sync = this._sync;
			lock (sync)
			{
				this._sweepTimer.Enabled = this._keepClean;
				this._state = ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			if (code == 1005)
			{
				this.stop(PayloadData.Empty, true);
			}
			else
			{
				PayloadData payloadData = new PayloadData(code, reason);
				bool send = !code.IsReserved();
				this.stop(payloadData, send);
			}
		}

		public void Broadcast(byte[] data)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107404582));
			}
			if ((long)data.Length <= (long)WebSocket.FragmentLength)
			{
				this.broadcast(Opcode.Binary, data, null);
			}
			else
			{
				this.broadcast(Opcode.Binary, new MemoryStream(data), null);
			}
		}

		public void Broadcast(string data)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107404582));
			}
			byte[] array;
			if (!data.TryGetUTF8EncodedBytes(out array))
			{
				string message2 = WebSocketSessionManager.getString_0(107135959);
				throw new ArgumentException(message2, WebSocketSessionManager.getString_0(107404582));
			}
			if ((long)array.Length <= (long)WebSocket.FragmentLength)
			{
				this.broadcast(Opcode.Text, array, null);
			}
			else
			{
				this.broadcast(Opcode.Text, new MemoryStream(array), null);
			}
		}

		public void Broadcast(Stream stream, int length)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107251701));
			}
			if (!stream.CanRead)
			{
				string message2 = WebSocketSessionManager.getString_0(107135577);
				throw new ArgumentException(message2, WebSocketSessionManager.getString_0(107251701));
			}
			if (length < 1)
			{
				string message3 = WebSocketSessionManager.getString_0(107158993);
				throw new ArgumentException(message3, WebSocketSessionManager.getString_0(107344872));
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = WebSocketSessionManager.getString_0(107135503);
				throw new ArgumentException(message4, WebSocketSessionManager.getString_0(107251701));
			}
			if (num < length)
			{
				string format = WebSocketSessionManager.getString_0(107135462);
				string message5 = string.Format(format, num);
				this._log.Warn(message5);
			}
			if (num <= WebSocket.FragmentLength)
			{
				this.broadcast(Opcode.Binary, array, null);
			}
			else
			{
				this.broadcast(Opcode.Binary, new MemoryStream(array), null);
			}
		}

		public void BroadcastAsync(byte[] data, Action completed)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107404582));
			}
			if ((long)data.Length <= (long)WebSocket.FragmentLength)
			{
				this.broadcastAsync(Opcode.Binary, data, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Binary, new MemoryStream(data), completed);
			}
		}

		public void BroadcastAsync(string data, Action completed)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107404582));
			}
			byte[] array;
			if (!data.TryGetUTF8EncodedBytes(out array))
			{
				string message2 = WebSocketSessionManager.getString_0(107135959);
				throw new ArgumentException(message2, WebSocketSessionManager.getString_0(107404582));
			}
			if ((long)array.Length <= (long)WebSocket.FragmentLength)
			{
				this.broadcastAsync(Opcode.Text, array, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Text, new MemoryStream(array), completed);
			}
		}

		public void BroadcastAsync(Stream stream, int length, Action completed)
		{
			if (this._state != ServerState.Start)
			{
				string message = WebSocketSessionManager.getString_0(107159058);
				throw new InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107251701));
			}
			if (!stream.CanRead)
			{
				string message2 = WebSocketSessionManager.getString_0(107135577);
				throw new ArgumentException(message2, WebSocketSessionManager.getString_0(107251701));
			}
			if (length < 1)
			{
				string message3 = WebSocketSessionManager.getString_0(107158993);
				throw new ArgumentException(message3, WebSocketSessionManager.getString_0(107344872));
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = WebSocketSessionManager.getString_0(107135503);
				throw new ArgumentException(message4, WebSocketSessionManager.getString_0(107251701));
			}
			if (num < length)
			{
				string format = WebSocketSessionManager.getString_0(107135462);
				string message5 = string.Format(format, num);
				this._log.Warn(message5);
			}
			if (num <= WebSocket.FragmentLength)
			{
				this.broadcastAsync(Opcode.Binary, array, completed);
			}
			else
			{
				this.broadcastAsync(Opcode.Binary, new MemoryStream(array), completed);
			}
		}

		public void CloseSession(string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close();
		}

		public void CloseSession(string id, ushort code, string reason)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close(code, reason);
		}

		public void CloseSession(string id, CloseStatusCode code, string reason)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Close(code, reason);
		}

		public bool PingTo(string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			return webSocketSession.Context.WebSocket.Ping();
		}

		public bool PingTo(string message, string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message2 = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message2);
			}
			return webSocketSession.Context.WebSocket.Ping(message);
		}

		public void SendTo(byte[] data, string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(data);
		}

		public void SendTo(string data, string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(data);
		}

		public void SendTo(Stream stream, int length, string id)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.Send(stream, length);
		}

		public void SendToAsync(byte[] data, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(string data, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(Stream stream, int length, string id, Action<bool> completed)
		{
			IWebSocketSession webSocketSession;
			if (!this.TryGetSession(id, out webSocketSession))
			{
				string message = WebSocketSessionManager.getString_0(107159000);
				throw new InvalidOperationException(message);
			}
			webSocketSession.Context.WebSocket.SendAsync(stream, length, completed);
		}

		public void Sweep()
		{
			if (this._sweeping)
			{
				this._log.Info(WebSocketSessionManager.getString_0(107159435));
			}
			else
			{
				object forSweep = this._forSweep;
				lock (forSweep)
				{
					if (this._sweeping)
					{
						this._log.Info(WebSocketSessionManager.getString_0(107159435));
						return;
					}
					this._sweeping = true;
				}
				foreach (string key in this.InactiveIDs)
				{
					if (this._state != ServerState.Start)
					{
						break;
					}
					object sync = this._sync;
					lock (sync)
					{
						if (this._state != ServerState.Start)
						{
							break;
						}
						IWebSocketSession webSocketSession;
						if (this._sessions.TryGetValue(key, out webSocketSession))
						{
							WebSocketState connectionState = webSocketSession.ConnectionState;
							if (connectionState == WebSocketState.Open)
							{
								webSocketSession.Context.WebSocket.Close(CloseStatusCode.Abnormal);
							}
							else if (connectionState != WebSocketState.Closing)
							{
								this._sessions.Remove(key);
							}
						}
					}
				}
				this._sweeping = false;
			}
		}

		public bool TryGetSession(string id, out IWebSocketSession session)
		{
			if (id == null)
			{
				throw new ArgumentNullException(WebSocketSessionManager.getString_0(107459975));
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(WebSocketSessionManager.getString_0(107140619), WebSocketSessionManager.getString_0(107459975));
			}
			return this.tryGetSession(id, out session);
		}

		private static readonly byte[] _emptyPingFrameAsBytes;

		private object _forSweep;

		private volatile bool _keepClean;

		private Logger _log;

		private Dictionary<string, IWebSocketSession> _sessions;

		private volatile ServerState _state;

		private volatile bool _sweeping;

		private Timer _sweepTimer;

		private object _sync;

		private TimeSpan _waitTime;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
