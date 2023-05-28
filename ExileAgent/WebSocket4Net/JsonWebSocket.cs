using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Authentication;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;

namespace WebSocket4Net
{
	public sealed class JsonWebSocket : IDisposable
	{
		public bool EnableAutoSendPing
		{
			get
			{
				return this.m_WebSocket.EnableAutoSendPing;
			}
			set
			{
				this.m_WebSocket.EnableAutoSendPing = value;
			}
		}

		public int AutoSendPingInterval
		{
			get
			{
				return this.m_WebSocket.AutoSendPingInterval;
			}
			set
			{
				this.m_WebSocket.AutoSendPingInterval = value;
			}
		}

		public WebSocketState State
		{
			get
			{
				return this.m_WebSocket.State;
			}
		}

		public JsonWebSocket(string uri) : this(uri, string.Empty)
		{
		}

		public JsonWebSocket(string uri, WebSocketVersion version) : this(uri, string.Empty, null, version)
		{
		}

		public JsonWebSocket(string uri, string subProtocol) : this(uri, subProtocol, null, WebSocketVersion.None)
		{
		}

		public JsonWebSocket(string uri, List<KeyValuePair<string, string>> cookies) : this(uri, string.Empty, cookies, WebSocketVersion.None)
		{
		}

		public JsonWebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies) : this(uri, subProtocol, cookies, WebSocketVersion.None)
		{
		}

		public JsonWebSocket(string uri, string subProtocol, WebSocketVersion version) : this(uri, subProtocol, null, version)
		{
		}

		public JsonWebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, WebSocketVersion version) : this(uri, subProtocol, cookies, null, string.Empty, string.Empty, version)
		{
		}

		public JsonWebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, WebSocketVersion version) : this(uri, subProtocol, cookies, customHeaderItems, userAgent, string.Empty, version)
		{
		}

		public JsonWebSocket(string uri, string subProtocol, List<KeyValuePair<string, string>> cookies, List<KeyValuePair<string, string>> customHeaderItems, string userAgent, string origin, WebSocketVersion version)
		{
			this.m_ExecutorDict = new Dictionary<string, IJsonExecutor>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			this.m_WebSocket = new WebSocket(uri, subProtocol, cookies, customHeaderItems, userAgent, origin, version, null, SslProtocols.None, 0);
			this.m_WebSocket.EnableAutoSendPing = true;
			this.SubscribeEvents();
		}

		public JsonWebSocket(WebSocket websocket)
		{
			this.m_ExecutorDict = new Dictionary<string, IJsonExecutor>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			if (websocket == null)
			{
				throw new ArgumentNullException(JsonWebSocket.getString_0(107143608));
			}
			if (websocket.State != WebSocketState.None)
			{
				throw new ArgumentException(JsonWebSocket.getString_0(107143563), JsonWebSocket.getString_0(107143608));
			}
			this.m_WebSocket = websocket;
			this.SubscribeEvents();
		}

		private void SubscribeEvents()
		{
			this.m_WebSocket.Closed += this.m_WebSocket_Closed;
			this.m_WebSocket.MessageReceived += this.m_WebSocket_MessageReceived;
			this.m_WebSocket.Opened += this.m_WebSocket_Opened;
			this.m_WebSocket.Error += this.m_WebSocket_Error;
		}

		public int ReceiveBufferSize
		{
			get
			{
				return this.m_WebSocket.ReceiveBufferSize;
			}
			set
			{
				this.m_WebSocket.ReceiveBufferSize = value;
			}
		}

		public void Open()
		{
			if (this.m_WebSocket.StateCode == -1 || this.m_WebSocket.StateCode == 3)
			{
				this.m_WebSocket.Open();
			}
		}

		public void Close()
		{
			if (this.m_WebSocket == null)
			{
				return;
			}
			if (this.m_WebSocket.StateCode == 1 || this.m_WebSocket.StateCode == 0)
			{
				this.m_WebSocket.Close();
			}
		}

		public event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(this.m_Error, value);
			}
			remove
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Remove(this.m_Error, value);
			}
		}

		private void m_WebSocket_Error(object sender, ErrorEventArgs e)
		{
			if (this.m_Error == null)
			{
				return;
			}
			this.m_Error(this, e);
		}

		public event EventHandler Opened
		{
			add
			{
				this.m_Opened = (EventHandler)Delegate.Combine(this.m_Opened, value);
			}
			remove
			{
				this.m_Opened = (EventHandler)Delegate.Remove(this.m_Opened, value);
			}
		}

		private void m_WebSocket_Opened(object sender, EventArgs e)
		{
			if (this.m_Opened == null)
			{
				return;
			}
			this.m_Opened(this, e);
		}

		private void m_WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Message))
			{
				return;
			}
			int num = e.Message.IndexOf(' ');
			string token = string.Empty;
			string text;
			string text2;
			if (num > 0)
			{
				text = e.Message.Substring(0, num);
				text2 = e.Message.Substring(num + 1);
				num = text.IndexOf('-');
				if (num > 0)
				{
					token = text.Substring(num + 1);
					text = text.Substring(0, num);
				}
			}
			else
			{
				text = e.Message;
				text2 = string.Empty;
			}
			IJsonExecutor executor = this.GetExecutor(text, token);
			if (executor == null)
			{
				return;
			}
			object param;
			try
			{
				if (!executor.Type.IsSimpleType())
				{
					param = this.DeserializeObject(text2, executor.Type);
				}
				else if (text2.GetType() == executor.Type)
				{
					param = text2;
				}
				else
				{
					param = Convert.ChangeType(text2, executor.Type, null);
				}
			}
			catch (Exception innerException)
			{
				this.m_WebSocket_Error(this, new ErrorEventArgs(new Exception(JsonWebSocket.getString_0(107143534), innerException)));
				return;
			}
			try
			{
				executor.Execute(this, token, param);
			}
			catch (Exception innerException2)
			{
				this.m_WebSocket_Error(this, new ErrorEventArgs(new Exception(JsonWebSocket.getString_0(107143465), innerException2)));
			}
		}

		public event EventHandler Closed
		{
			add
			{
				this.m_Closed = (EventHandler)Delegate.Combine(this.m_Closed, value);
			}
			remove
			{
				this.m_Closed = (EventHandler)Delegate.Remove(this.m_Closed, value);
			}
		}

		private void m_WebSocket_Closed(object sender, EventArgs e)
		{
			if (this.m_Closed == null)
			{
				return;
			}
			this.m_Closed(this, e);
		}

		public void On<T>(string name, Action<T> executor)
		{
			this.RegisterExecutor<T>(name, string.Empty, new JsonExecutor<T>(executor));
		}

		public void On<T>(string name, Action<JsonWebSocket, T> executor)
		{
			this.RegisterExecutor<T>(name, string.Empty, new JsonExecutorWithSender<T>(executor));
		}

		public void Send(string name, object content)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(JsonWebSocket.getString_0(107377577));
			}
			if (content == null)
			{
				this.m_WebSocket.Send(name);
				return;
			}
			if (!content.GetType().IsSimpleType())
			{
				this.m_WebSocket.Send(string.Format(JsonWebSocket.getString_0(107403319), name, this.SerializeObject(content)));
				return;
			}
			this.m_WebSocket.Send(string.Format(JsonWebSocket.getString_0(107403319), name, content));
		}

		public string Query<T>(string name, object content, Action<T> executor)
		{
			return this.Query<T>(name, content, new JsonExecutor<T>(executor));
		}

		public string Query<T>(string name, object content, Action<string, T> executor)
		{
			return this.Query<T>(name, content, new JsonExecutorWithToken<T>(executor));
		}

		public string Query<T>(string name, object content, Action<JsonWebSocket, T> executor)
		{
			return this.Query<T>(name, content, new JsonExecutorWithSender<T>(executor));
		}

		public string Query<T>(string name, object content, Action<JsonWebSocket, string, T> executor)
		{
			return this.Query<T>(name, content, new JsonExecutorFull<T>(executor));
		}

		public string Query<T>(string name, object content, Action<JsonWebSocket, T, object> executor, object state)
		{
			return this.Query<T>(name, content, new JsonExecutorWithSenderAndState<T>(executor, state));
		}

		private string Query<T>(string name, object content, IJsonExecutor executor)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(JsonWebSocket.getString_0(107377577));
			}
			int num = JsonWebSocket.m_Random.Next(1000, 9999);
			this.RegisterExecutor<T>(name, num.ToString(), executor);
			if (content != null)
			{
				if (!content.GetType().IsSimpleType())
				{
					this.m_WebSocket.Send(string.Format(JsonWebSocket.getString_0(107143428), name, num, this.SerializeObject(content)));
				}
				else
				{
					this.m_WebSocket.Send(string.Format(JsonWebSocket.getString_0(107143428), name, num, content));
				}
			}
			else
			{
				this.m_WebSocket.Send(string.Format(JsonWebSocket.getString_0(107291358), name, num));
			}
			return num.ToString();
		}

		private void RegisterExecutor<T>(string name, string token, IJsonExecutor executor)
		{
			Dictionary<string, IJsonExecutor> executorDict = this.m_ExecutorDict;
			lock (executorDict)
			{
				if (string.IsNullOrEmpty(token))
				{
					this.m_ExecutorDict.Add(name, executor);
				}
				else
				{
					this.m_ExecutorDict.Add(string.Format(JsonWebSocket.getString_0(107291358), name, token), executor);
				}
			}
		}

		private IJsonExecutor GetExecutor(string name, string token)
		{
			string key = name;
			bool flag = false;
			if (!string.IsNullOrEmpty(token))
			{
				key = string.Format(JsonWebSocket.getString_0(107291358), name, token);
				flag = true;
			}
			Dictionary<string, IJsonExecutor> executorDict = this.m_ExecutorDict;
			IJsonExecutor result;
			lock (executorDict)
			{
				IJsonExecutor jsonExecutor;
				if (!this.m_ExecutorDict.TryGetValue(key, out jsonExecutor))
				{
					result = null;
				}
				else
				{
					if (flag)
					{
						this.m_ExecutorDict.Remove(key);
					}
					result = jsonExecutor;
				}
			}
			return result;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (disposing && this.m_WebSocket != null)
			{
				this.m_WebSocket.Dispose();
			}
			this.m_disposed = true;
		}

		~JsonWebSocket()
		{
			this.Dispose(false);
		}

		public SecurityOption Security
		{
			get
			{
				return this.m_WebSocket.Security;
			}
		}

		protected string SerializeObject(object target)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(target.GetType());
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, target);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		protected object DeserializeObject(string json, Type type)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(type);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				result = dataContractJsonSerializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JsonWebSocket()
		{
			Strings.CreateGetStringDelegate(typeof(JsonWebSocket));
			JsonWebSocket.m_Random = new Random();
		}

		private WebSocket m_WebSocket;

		private bool m_disposed;

		private EventHandler<ErrorEventArgs> m_Error;

		private EventHandler m_Opened;

		private EventHandler m_Closed;

		private static Random m_Random;

		private const string m_QueryTemplateA = "{0}-{1} {2}";

		private const string m_QueryTemplateB = "{0}-{1}";

		private const string m_QueryTemplateC = "{0} {1}";

		private const string m_QueryKeyTokenTemplate = "{0}-{1}";

		private Dictionary<string, IJsonExecutor> m_ExecutorDict;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
