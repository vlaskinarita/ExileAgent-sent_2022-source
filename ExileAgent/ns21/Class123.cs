using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Timers;
using Newtonsoft.Json;
using ns0;
using ns10;
using ns22;
using ns23;
using ns24;
using ns25;
using ns26;
using ns27;
using ns29;
using ns35;
using ns4;
using ns6;
using ns8;
using PoEv2;
using PoEv2.Models;
using PoEv2.PublicModels;
using PusherClient;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns21
{
	internal static class Class123
	{
		private static void smethod_0()
		{
			Class123.dictionary_0 = new Dictionary<Enum3, Action<string, string>>();
			Class123.channel_0.Bind(Class123.getString_0(107371694), new Action<PusherEvent>(new Class138(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371681), new Action<PusherEvent>(new Class137(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371700), new Action<PusherEvent>(new Class136(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371655), new Action<PusherEvent>(new Class134(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371118), new Action<PusherEvent>(new Class135(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371125), new Action<PusherEvent>(new Class133(Class123.mainForm_0).method_0));
			Class123.channel_0.Bind(Class123.getString_0(107371104), new Action<PusherEvent>(new Class132(Class123.mainForm_0).method_0));
			Class123.dictionary_0.Add(Enum3.const_1, new Action<string, string>(new Class131(Class123.mainForm_0).method_1));
			Class123.dictionary_0.Add(Enum3.const_2, new Action<string, string>(new Class130(Class123.mainForm_0).method_1));
		}

		private static void smethod_1(string string_0, string string_1)
		{
			string_0 = string_0.Replace(Class139.smethod_0(Enum3.const_0), string.Empty);
			Enum3 key = Class142.smethod_1(string_0);
			if (Class123.dictionary_0.ContainsKey(key))
			{
				Class123.dictionary_0[key](string_0, string_1);
			}
		}

		public static bool IsDisconnected
		{
			get
			{
				return Class123.pusher_0 != null && Class123.pusher_0.State == ConnectionState.Disconnected;
			}
		}

		public static bool IsConnected
		{
			get
			{
				return Class123.pusher_0 != null && Class123.pusher_0.State == ConnectionState.Connected;
			}
		}

		[DebuggerStepThrough]
		public static void smethod_2(MainForm mainForm_1)
		{
			Class123.Class124 @class = new Class123.Class124();
			@class.mainForm_0 = mainForm_1;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Class123.Class124>(ref @class);
		}

		private static void smethod_3()
		{
			if (!Class123.bool_4)
			{
				Class123.timer_0 = new Timer();
				Class123.timer_0.Interval = 20000.0;
				Class123.timer_0.Elapsed += Class123.smethod_17;
				Class123.timer_0.SynchronizingObject = null;
				Class123.timer_1 = new Timer();
				Class123.timer_1.Interval = 40000.0;
				Class123.timer_1.Elapsed += Class123.smethod_18;
				Class123.timer_1.SynchronizingObject = null;
				Class123.bool_4 = true;
			}
		}

		[DebuggerStepThrough]
		private static void smethod_4()
		{
			Class123.Class125 @class = new Class123.Class125();
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Class123.Class125>(ref @class);
		}

		public static void smethod_5(Enum3 enum3_0, string string_0 = null, DataMethods dataMethods_0 = DataMethods.POST)
		{
			if (Class123.pusher_0 != null && Class123.pusher_0.State == ConnectionState.Connected)
			{
				string text = Class139.smethod_0(enum3_0);
				Uri requestUri = new Uri(text);
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
				httpWebRequest.Method = dataMethods_0.ToString();
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.ContentType = Class123.getString_0(107371745);
				httpWebRequest.Timeout = 60000;
				HttpWebRequest httpWebRequest2 = httpWebRequest;
				httpWebRequest2.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(httpWebRequest2.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Class103.smethod_5));
				httpWebRequest.Headers = new WebHeaderCollection
				{
					{
						Class123.getString_0(107371720),
						Class139.AuthKey
					}
				};
				try
				{
					if (dataMethods_0 == DataMethods.POST && string_0 != null)
					{
						byte[] bytes = Encoding.UTF8.GetBytes(string_0);
						httpWebRequest.ContentLength = (long)bytes.Length;
						Stream requestStream = httpWebRequest.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
					}
					Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						Class123.smethod_1(text, streamReader.ReadToEnd());
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (WebException ex)
				{
					if (ex.Message.Contains(Class123.getString_0(107371047)) && (text == Class139.smethod_0(Enum3.const_1) || text == Class139.smethod_0(Enum3.const_2)))
					{
						Class123.smethod_1(text, ex.Message);
					}
					else
					{
						Class123.smethod_1(text, string.Empty);
					}
				}
				catch (Exception ex2)
				{
					Class181.smethod_2(Enum11.const_2, Class123.getString_0(107371042), new object[]
					{
						dataMethods_0,
						ex2
					});
				}
			}
		}

		[DebuggerStepThrough]
		public static void smethod_6(Enum3 enum3_0, Bitmap bitmap_0)
		{
			Class123.Class126 @class = new Class123.Class126();
			@class.enum3_0 = enum3_0;
			@class.bitmap_0 = bitmap_0;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Class123.Class126>(ref @class);
		}

		private unsafe static void smethod_7(object object_1)
		{
			void* ptr = stackalloc byte[2];
			try
			{
				*(byte*)ptr = ((!Class123.bool_3) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					Class181.smethod_3(Enum11.const_0, Class123.getString_0(107371021));
				}
				else
				{
					Class123.bool_3 = false;
				}
				((byte*)ptr)[1] = (Class123.bool_1 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					Class181.smethod_3(Enum11.const_0, Class123.getString_0(107370992));
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, Class123.getString_0(107370975), new object[]
				{
					ex
				});
			}
		}

		private static void smethod_8(object object_1)
		{
			Class181.smethod_3(Enum11.const_0, Class123.getString_0(107370938));
			Class123.smethod_11();
		}

		private static void smethod_9(object object_1, ConnectionState connectionState_0)
		{
			if (connectionState_0 != ConnectionState.Connecting)
			{
				Class123.bool_2 = false;
			}
			Class181.smethod_2(Enum11.const_3, Class123.getString_0(107370881), new object[]
			{
				connectionState_0
			});
		}

		private unsafe static void smethod_10(object object_1, PusherException pusherException_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (Class123.bool_1 ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				Class181.smethod_2(Enum11.const_3, Class123.getString_0(107371388), new object[]
				{
					pusherException_0.Message
				});
				if (pusherException_0 is ChannelException || pusherException_0 is OperationException)
				{
					Class181.smethod_3(Enum11.const_2, Class123.getString_0(107371331));
				}
				else
				{
					((byte*)ptr)[1] = (pusherException_0.Message.Contains(Class123.getString_0(107371274)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Class181.smethod_3(Enum11.const_2, Class123.getString_0(107371253));
					}
					else
					{
						Class181.smethod_2(Enum11.const_2, Class123.getString_0(107371192), new object[]
						{
							pusherException_0.Message
						});
					}
				}
			}
		}

		private static void smethod_11()
		{
			if (Class123.bool_0)
			{
				Class123.smethod_5(Enum3.const_1, null, DataMethods.GET);
				Class123.bool_0 = false;
			}
		}

		[DebuggerStepThrough]
		public static void smethod_12(bool bool_6 = true, bool bool_7 = false)
		{
			Class123.Class127 @class = new Class123.Class127();
			@class.bool_0 = bool_6;
			@class.bool_1 = bool_7;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Class123.Class127>(ref @class);
		}

		public static void smethod_13()
		{
			Class123.timer_0.Enabled = true;
			Class123.timer_1.Enabled = true;
			Class123.timer_0.Stop();
			Class123.timer_0.Start();
			Class123.timer_1.Stop();
			Class123.timer_1.Start();
		}

		public static void smethod_14()
		{
			Class123.timer_1.Stop();
			Class123.timer_1.Start();
		}

		public static void smethod_15()
		{
			Class123.smethod_17(null, null);
		}

		public static void smethod_16()
		{
			Class123.smethod_18(null, null);
		}

		private static void smethod_17(object sender, ElapsedEventArgs e)
		{
			if (Class123.pusher_0 != null && Class123.pusher_0.State == ConnectionState.Connected)
			{
				Class140 value = new Class140
				{
					Connected = true,
					BotStarted = (Class123.mainForm_0.genum0_0 == MainForm.GEnum0.const_2 || UI.bool_2),
					BotStarting = (Class123.mainForm_0.genum0_0 == MainForm.GEnum0.const_1),
					BotPaused = MainForm.IsPaused,
					BotStartedTime = Class123.mainForm_0.method_135(),
					TradesRequested = Class123.mainForm_0.method_132(),
					TradesFailed = Class123.mainForm_0.method_134(),
					TradesCompleted = Class123.mainForm_0.method_133(),
					AccountName = Class255.class105_0.method_3(ConfigOptions.AccountName)
				};
				string string_ = JsonConvert.SerializeObject(value);
				Class123.smethod_5(Enum3.const_3, string_, DataMethods.POST);
			}
		}

		private static void smethod_18(object sender, ElapsedEventArgs e)
		{
			if (Class123.pusher_0.State == ConnectionState.Connected)
			{
				Class123.smethod_5(Enum3.const_2, null, DataMethods.POST);
			}
		}

		public unsafe static bool smethod_19()
		{
			void* ptr = stackalloc byte[3];
			object obj = Class123.object_0;
			*(byte*)ptr = 0;
			try
			{
				Monitor.Enter(obj, ref *(bool*)ptr);
				if (Class123.IsConnected || Class123.bool_2)
				{
					((byte*)ptr)[1] = 1;
				}
				else
				{
					Class123.bool_0 = true;
					Class123.smethod_4();
					DateTime t = DateTime.Now.AddSeconds(5.0);
					for (;;)
					{
						((byte*)ptr)[2] = ((!Class123.bool_5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) == 0)
						{
							break;
						}
						Thread.Sleep(300);
						if (t < DateTime.Now || Class123.IsDisconnected)
						{
							goto IL_8B;
						}
					}
					((byte*)ptr)[1] = 1;
					goto IL_9D;
					IL_8B:
					((byte*)ptr)[1] = 0;
				}
			}
			finally
			{
				if (*(sbyte*)ptr != 0)
				{
					Monitor.Exit(obj);
				}
			}
			IL_9D:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static void smethod_20()
		{
			Class123.smethod_5(Enum3.const_7, null, DataMethods.GET);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class123()
		{
			Strings.CreateGetStringDelegate(typeof(Class123));
			Class123.bool_0 = true;
			Class123.bool_1 = false;
			Class123.bool_2 = false;
			Class123.bool_3 = false;
			Class123.bool_4 = false;
			Class123.object_0 = new object();
			Class123.bool_5 = false;
		}

		private static Dictionary<Enum3, Action<string, string>> dictionary_0;

		private static MainForm mainForm_0;

		private static Pusher pusher_0;

		private static Channel channel_0;

		private static Timer timer_0;

		private static Timer timer_1;

		private static bool bool_0;

		private static bool bool_1;

		private static bool bool_2;

		private static bool bool_3;

		private static bool bool_4;

		private static object object_0;

		public static bool bool_5;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
