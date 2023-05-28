using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using ns0;
using ns8;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace ns39
{
	internal sealed class Class260
	{
		public event Class260.Delegate2 OnTradeFoundEvent
		{
			[CompilerGenerated]
			add
			{
				Class260.Delegate2 @delegate = this.delegate2_0;
				Class260.Delegate2 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate2 value2 = (Class260.Delegate2)Delegate.Combine(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate2>(ref this.delegate2_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
			[CompilerGenerated]
			remove
			{
				Class260.Delegate2 @delegate = this.delegate2_0;
				Class260.Delegate2 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate2 value2 = (Class260.Delegate2)Delegate.Remove(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate2>(ref this.delegate2_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
		}

		public event Class260.Delegate3 OnConnectedEvent
		{
			[CompilerGenerated]
			add
			{
				Class260.Delegate3 @delegate = this.delegate3_0;
				Class260.Delegate3 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate3 value2 = (Class260.Delegate3)Delegate.Combine(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate3>(ref this.delegate3_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
			[CompilerGenerated]
			remove
			{
				Class260.Delegate3 @delegate = this.delegate3_0;
				Class260.Delegate3 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate3 value2 = (Class260.Delegate3)Delegate.Remove(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate3>(ref this.delegate3_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
		}

		public event Class260.Delegate4 OnClosedEvent
		{
			[CompilerGenerated]
			add
			{
				Class260.Delegate4 @delegate = this.delegate4_0;
				Class260.Delegate4 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate4 value2 = (Class260.Delegate4)Delegate.Combine(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate4>(ref this.delegate4_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
			[CompilerGenerated]
			remove
			{
				Class260.Delegate4 @delegate = this.delegate4_0;
				Class260.Delegate4 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate4 value2 = (Class260.Delegate4)Delegate.Remove(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate4>(ref this.delegate4_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
		}

		public event Class260.Delegate5 OnErrorEvent
		{
			[CompilerGenerated]
			add
			{
				Class260.Delegate5 @delegate = this.delegate5_0;
				Class260.Delegate5 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate5 value2 = (Class260.Delegate5)Delegate.Combine(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate5>(ref this.delegate5_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
			[CompilerGenerated]
			remove
			{
				Class260.Delegate5 @delegate = this.delegate5_0;
				Class260.Delegate5 delegate2;
				do
				{
					delegate2 = @delegate;
					Class260.Delegate5 value2 = (Class260.Delegate5)Delegate.Remove(delegate2, value);
					@delegate = Interlocked.CompareExchange<Class260.Delegate5>(ref this.delegate5_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
		}

		public Class260(string string_1)
		{
			this.string_0 = string_1;
		}

		public void method_0(string string_1)
		{
			this.webSocket_0 = new WebSocket(Class103.smethod_0(Class255.class105_0.method_3(ConfigOptions.League), string_1), Array.Empty<string>())
			{
				Origin = Class260.getString_0(107354706),
				UserAgent = Class255.class105_0.method_3(ConfigOptions.UserAgent)
			};
			this.webSocket_0.OnMessage += this.method_4;
			this.webSocket_0.OnError += this.method_5;
			this.webSocket_0.OnOpen += this.webSocket_0_OnOpen;
			this.webSocket_0.OnClose += this.method_3;
		}

		private void webSocket_0_OnOpen(object sender, EventArgs e)
		{
			if (this.delegate3_0 != null)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_6));
			}
		}

		public void method_1()
		{
			foreach (KeyValuePair<string, string> keyValuePair in Class255.Cookies)
			{
				this.webSocket_0.SetCookie(new Cookie(keyValuePair.Key, keyValuePair.Value));
			}
			this.bool_0 = false;
			this.webSocket_0.ConnectAsync();
		}

		public void method_2(bool bool_1 = false)
		{
			if (this.webSocket_0 != null && this.webSocket_0.ReadyState != WebSocketState.Closed)
			{
				this.bool_0 = bool_1;
				this.webSocket_0.CloseAsync();
			}
		}

		private void method_3(object sender, CloseEventArgs e)
		{
			if (this.delegate4_0 != null && !this.bool_0)
			{
				this.closeEventArgs_0 = e;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_7));
			}
		}

		private void method_4(object sender, MessageEventArgs e)
		{
			Class260.Class262 @class = new Class260.Class262();
			@class.class260_0 = this;
			@class.class261_0 = JsonConvert.DeserializeObject<Class260.Class261>(e.Data);
			if (@class.class261_0.@new != null)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(@class.method_0));
			}
		}

		private void method_5(object sender, ErrorEventArgs e)
		{
			Class260.Class263 @class = new Class260.Class263();
			@class.class260_0 = this;
			@class.errorEventArgs_0 = e;
			if (this.delegate5_0 != null)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(@class.method_0));
			}
		}

		public string CloseError
		{
			get
			{
				string result;
				if (this.closeEventArgs_0 == null)
				{
					result = string.Empty;
				}
				else
				{
					result = string.Format(Class260.getString_0(107396549), this.closeEventArgs_0.Code, this.closeEventArgs_0.Reason);
				}
				return result;
			}
		}

		[CompilerGenerated]
		private void method_6(object object_0)
		{
			this.delegate3_0(this.string_0);
		}

		[CompilerGenerated]
		private void method_7(object object_0)
		{
			this.delegate4_0(this, this.string_0);
		}

		static Class260()
		{
			Strings.CreateGetStringDelegate(typeof(Class260));
		}

		private bool bool_0;

		private readonly string string_0;

		private WebSocket webSocket_0;

		private CloseEventArgs closeEventArgs_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class260.Delegate2 delegate2_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class260.Delegate3 delegate3_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class260.Delegate4 delegate4_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class260.Delegate5 delegate5_0;

		[NonSerialized]
		internal static GetString getString_0;

		public delegate void Delegate2(string id, List<string> fetchIds);

		public delegate void Delegate3(string id);

		public delegate void Delegate4(Class260 ws, string id);

		public delegate void Delegate5(string id, string msg);

		private sealed class Class261
		{
			public List<string> @new { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<string> list_0;
		}

		[CompilerGenerated]
		private sealed class Class262
		{
			internal void method_0(object object_0)
			{
				this.class260_0.delegate2_0(this.class260_0.string_0, this.class261_0.@new);
			}

			public Class260 class260_0;

			public Class260.Class261 class261_0;
		}

		[CompilerGenerated]
		private sealed class Class263
		{
			internal void method_0(object object_0)
			{
				this.class260_0.delegate5_0(this.class260_0.string_0, this.errorEventArgs_0.Message);
			}

			public Class260 class260_0;

			public ErrorEventArgs errorEventArgs_0;
		}
	}
}
