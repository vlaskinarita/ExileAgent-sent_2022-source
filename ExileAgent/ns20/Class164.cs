using System;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;
using ns0;
using PoEv2;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns20
{
	internal sealed class Class164
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			Class164.mainForm_0 = mainForm_1;
		}

		public static void smethod_1(string string_0, string string_1, string string_2)
		{
			try
			{
				Class0<string, string> value = new Class0<string, string>(string_2, Class164.getString_0(107380918));
				WebClient webClient = new WebClient();
				webClient.Headers[Class164.getString_0(107464476)] = string_0;
				webClient.Headers[Class164.getString_0(107464491)] = Class164.getString_0(107371887);
				string data = JsonConvert.SerializeObject(value);
				webClient.UploadString(Class164.getString_0(107463930), data);
				webClient.Dispose();
			}
			catch
			{
			}
		}

		public unsafe static void smethod_2(string string_0, string string_1)
		{
			void* ptr = stackalloc byte[3];
			try
			{
				WebClient webClient = new WebClient();
				NameValueCollection nameValueCollection = new NameValueCollection();
				*(byte*)ptr = (Class255.class105_0.method_4(ConfigOptions.DiscordUsernameCheck) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					nameValueCollection.Add(Class164.getString_0(107463913), Class164.mainForm_0.CharacterName);
				}
				else
				{
					((byte*)ptr)[1] = ((Class255.class105_0.method_3(ConfigOptions.DiscordUsername) != string.Empty) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						nameValueCollection.Add(Class164.getString_0(107463913), Class255.class105_0.method_3(ConfigOptions.DiscordUsername));
					}
					else
					{
						nameValueCollection.Add(Class164.getString_0(107463913), Class164.getString_0(107351725));
					}
				}
				((byte*)ptr)[2] = ((Class255.class105_0.method_3(ConfigOptions.DiscordAvatarUrl) != string.Empty) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					nameValueCollection.Add(Class164.getString_0(107463868), Class255.class105_0.method_3(ConfigOptions.DiscordAvatarUrl));
				}
				else
				{
					nameValueCollection.Add(Class164.getString_0(107463868), Class164.getString_0(107463883));
				}
				nameValueCollection.Add(Class164.getString_0(107463753), string_1);
				webClient.UploadValues(string_0, nameValueCollection);
			}
			catch
			{
			}
		}

		static Class164()
		{
			Strings.CreateGetStringDelegate(typeof(Class164));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
