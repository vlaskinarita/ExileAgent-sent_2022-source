using System;
using System.Net;
using PusherClient;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns20
{
	internal sealed class Class122 : IAuthorizer
	{
		public Class122(string string_1, string string_2)
		{
			this.string_0 = string_2;
			this.uri_0 = new Uri(string_1);
		}

		public string Authorize(string channelName, string socketId)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string data = string.Format(Class122.getString_0(107371849), channelName, socketId);
					webClient.Headers[HttpRequestHeader.ContentType] = Class122.getString_0(107371776);
					webClient.Headers[HttpRequestHeader.Accept] = Class122.getString_0(107371731);
					webClient.Headers[Class122.getString_0(107371706)] = this.string_0;
					result = webClient.UploadString(this.uri_0, Class122.getString_0(107371721), data);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		static Class122()
		{
			Strings.CreateGetStringDelegate(typeof(Class122));
		}

		private readonly Uri uri_0;

		private readonly string string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
