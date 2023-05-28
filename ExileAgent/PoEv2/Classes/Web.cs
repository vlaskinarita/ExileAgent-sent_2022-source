using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns0;
using ns1;
using ns15;
using ns22;
using ns29;
using ns33;
using ns35;
using ns36;
using ns39;
using ns4;
using ns41;
using ns8;
using ns9;
using PoEv2.Models;
using PoEv2.Models.Flipping;
using PoEv2.Models.Query;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Classes
{
	public static class Web
	{
		public static string smethod_0(string string_1)
		{
			Uri requestUri = new Uri(string_1);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			HttpWebRequest httpWebRequest2 = httpWebRequest;
			httpWebRequest2.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(httpWebRequest2.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Class103.smethod_5));
			string result;
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
				string text = streamReader.ReadToEnd();
				result = text;
			}
			catch (WebException ex)
			{
				result = Web.getString_0(107450867) + ex.Message + Web.getString_0(107450858);
			}
			return result;
		}

		public unsafe static Dictionary<string, string> smethod_1(string string_1, IDictionary<string, string> idictionary_0)
		{
			void* ptr = stackalloc byte[10];
			Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			string html = Web.smethod_6(Class103.string_14 + string_1, idictionary_0);
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			string text = null;
			HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(Web.getString_0(107450849));
			((byte*)ptr)[4] = ((htmlNodeCollection == null) ? 1 : 0);
			Dictionary<string, string> result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = dictionary2;
			}
			else
			{
				foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					bool flag = htmlNode.Descendants(Web.getString_0(107450256)).Any(new Func<HtmlNode, bool>(Web.<>c.<>9.method_0));
					((byte*)ptr)[5] = (flag ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						text = htmlNode.InnerText.Replace(Web.getString_0(107396260), Web.getString_0(107397466)).Replace(Web.getString_0(107461836), Web.getString_0(107397466)).Trim();
						((byte*)ptr)[6] = ((!text.Contains(Web.getString_0(107450283))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							((byte*)ptr)[7] = ((text != Web.getString_0(107397466)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								List<int> list = new List<int>();
								foreach (HtmlNode htmlNode2 in htmlNode.Descendants(Web.getString_0(107450256)).Where(new Func<HtmlNode, bool>(Web.<>c.<>9.method_1)).ToList<HtmlNode>())
								{
									Match match = Web.regex_0.Match(htmlNode2.Id);
									((byte*)ptr)[8] = ((match.Groups.Count > 1) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) != 0)
									{
										list.Add(int.Parse(match.Groups[1].Value));
									}
								}
								((byte*)ptr)[9] = ((!dictionary.ContainsKey(text)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									dictionary.Add(text, list);
								}
								else
								{
									dictionary[text].AddRange(list);
								}
							}
						}
					}
				}
				Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
				string innerText = htmlDocument.DocumentNode.SelectSingleNode(Web.getString_0(107450270)).InnerText;
				MatchCollection matchCollection = Web.regex_1.Matches(innerText);
				foreach (object obj in matchCollection)
				{
					Match match2 = (Match)obj;
					dictionary3.Add(int.Parse(match2.Groups[1].Value), match2.Groups[2].Value);
				}
				foreach (KeyValuePair<string, List<int>> keyValuePair in dictionary)
				{
					foreach (int num in keyValuePair.Value)
					{
						*(int*)ptr = num;
						dictionary2.Add(dictionary3[*(int*)ptr], keyValuePair.Key);
					}
				}
				result = dictionary2;
			}
			return result;
		}

		public unsafe static string smethod_2(string string_1, Encoding encoding_0, IDictionary<string, string> idictionary_0)
		{
			void* ptr = stackalloc byte[3];
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Web.<>c.<>9.method_2));
			Uri uri = new Uri(string_1);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.UserAgent = Class255.class105_0.method_3(ConfigOptions.UserAgent);
			httpWebRequest.Timeout = 20000;
			httpWebRequest.KeepAlive = false;
			*(byte*)ptr = ((idictionary_0 != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair in idictionary_0)
				{
					((byte*)ptr)[1] = ((keyValuePair.Key != Web.getString_0(107397466)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						httpWebRequest.smethod_3(new Cookie(keyValuePair.Key, keyValuePair.Value, Web.getString_0(107372738), uri.Host));
					}
				}
			}
			string result;
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, encoding_0);
				string text = streamReader.ReadToEnd();
				((byte*)ptr)[2] = (text.Contains(Web.getString_0(107450189)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, Web.getString_0(107450128));
					result = Web.getString_0(107450067);
				}
				else
				{
					result = text;
				}
			}
			catch (WebException ex)
			{
				result = Web.getString_0(107450867) + ex.Message + Web.getString_0(107450858);
			}
			return result;
		}

		public unsafe static bool smethod_3(this WebRequest webRequest_0, Cookie cookie_0)
		{
			void* ptr = stackalloc byte[3];
			HttpWebRequest httpWebRequest = webRequest_0 as HttpWebRequest;
			*(byte*)ptr = ((httpWebRequest == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((httpWebRequest.CookieContainer == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					httpWebRequest.CookieContainer = new CookieContainer();
				}
				httpWebRequest.CookieContainer.Add(cookie_0);
				((byte*)ptr)[1] = 1;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_4(string string_1, string string_2, IDictionary<string, string> idictionary_0)
		{
			void* ptr = stackalloc byte[2];
			try
			{
				Tuple<string, string> tuple = Web.smethod_5(string.Format(Web.string_0, string_1), idictionary_0);
				*(byte*)ptr = (string.IsNullOrEmpty(tuple.Item2) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					Class181.smethod_3(Enum11.const_2, Web.getString_0(107450478));
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Web.getString_0(107450445) + Uri.EscapeDataString(tuple.Item1));
				stringBuilder.Append(Web.getString_0(107450436) + Uri.EscapeDataString(string_2));
				stringBuilder.Append(Web.getString_0(107450391) + tuple.Item2);
				Web.smethod_7(Web.getString_0(107372068), string.Format(Web.string_0, string_1), stringBuilder.ToString(), idictionary_0);
				((byte*)ptr)[1] = 1;
			}
			catch (Exception ex)
			{
				Class181.smethod_3(Enum11.const_2, Web.getString_0(107450382) + ex.ToString());
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private static Tuple<string, string> smethod_5(string string_1, IDictionary<string, string> idictionary_0)
		{
			string html = Web.smethod_6(string_1, idictionary_0);
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			string item = null;
			HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode(Web.getString_0(107450373));
			Match match = Web.regex_2.Match(htmlNode.InnerText);
			if (match.Groups.Count > 1)
			{
				item = match.Groups[1].Value;
			}
			HtmlNode htmlNode2 = htmlDocument.DocumentNode.SelectSingleNode(Web.getString_0(107450328));
			string value = htmlNode2.Attributes[Web.getString_0(107449803)].Value;
			return Tuple.Create<string, string>(item, value);
		}

		public static string smethod_6(string string_1, IDictionary<string, string> idictionary_0)
		{
			Stream stream = Web.smethod_7(Web.getString_0(107449794), string_1, null, idictionary_0);
			string result;
			if (stream == null)
			{
				result = Web.getString_0(107397466);
			}
			else
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		public static Stream smethod_7(string string_1, string string_2, string string_3, IDictionary<string, string> idictionary_0)
		{
			Uri uri = new Uri(string_2);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.UserAgent = Class255.class105_0.method_3(ConfigOptions.UserAgent);
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = string_1;
			httpWebRequest.ContentType = Web.getString_0(107372123);
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.Timeout = 20000;
			foreach (KeyValuePair<string, string> keyValuePair in idictionary_0)
			{
				if (keyValuePair.Key != Web.getString_0(107397466))
				{
					httpWebRequest.smethod_3(new Cookie(keyValuePair.Key, keyValuePair.Value, Web.getString_0(107372738), uri.Host));
				}
			}
			if (string_1 == Web.getString_0(107372068) && string_3 != null)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string_3);
				httpWebRequest.ContentLength = (long)bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
			}
			Stream result;
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				result = response.GetResponseStream();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public unsafe static Class247 smethod_8(string string_1, string string_2, Enum5 enum5_0 = Enum5.const_1)
		{
			void* ptr = stackalloc byte[4];
			Uri uri = new Uri(string_1);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.UserAgent = Class255.class105_0.method_3(ConfigOptions.UserAgent);
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = Web.getString_0(107372068);
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.Timeout = 20000;
			if (enum5_0 != Enum5.const_0)
			{
				if (enum5_0 == Enum5.const_1)
				{
					httpWebRequest.ContentType = Web.getString_0(107372078);
				}
			}
			else
			{
				httpWebRequest.ContentType = Web.getString_0(107372123);
			}
			foreach (KeyValuePair<string, string> keyValuePair in Class255.Cookies)
			{
				*(byte*)ptr = ((keyValuePair.Key != Web.getString_0(107397466)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					httpWebRequest.smethod_3(new Cookie(keyValuePair.Key, keyValuePair.Value, Web.getString_0(107372738), uri.Host));
				}
			}
			Class247 result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string_2);
				httpWebRequest.ContentLength = (long)bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				WebResponse response = httpWebRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				HttpWebResponse httpWebResponse = (HttpWebResponse)response;
				((byte*)ptr)[1] = ((httpWebResponse.StatusCode != HttpStatusCode.OK) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text = streamReader.ReadToEnd();
						((byte*)ptr)[2] = (text.Contains(Web.getString_0(107449757)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							return new Class247(Enum7.const_2);
						}
						return new Class247(Enum7.const_0, text);
					}
				}
				result = new Class247(Enum7.const_1);
			}
			catch (Exception ex)
			{
				((byte*)ptr)[3] = (ex.Message.Contains(Web.getString_0(107449760)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					result = new Class247(Enum7.const_2);
				}
				else
				{
					result = new Class247(Enum7.const_1, ex.Message);
				}
			}
			return result;
		}

		public static Stream smethod_9(IEnumerable<string> ienumerable_0, string string_1, bool bool_0 = false)
		{
			Uri requestUri = new Uri(string.Format(Web.getString_0(107449735), new object[]
			{
				Class103.string_11,
				string.Join(Web.getString_0(107393090), ienumerable_0),
				string_1,
				bool_0 ? Web.getString_0(107449706) : string.Empty
			}));
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.UserAgent = Class255.class105_0.method_3(ConfigOptions.UserAgent);
			Stream result;
			try
			{
				WebResponse response = httpWebRequest.GetResponse();
				result = response.GetResponseStream();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Class250 smethod_10(JsonItem jsonItem_0)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string text = Class255.class105_0.method_3(ConfigOptions.League);
					Class181.smethod_2(Enum11.const_3, Web.getString_0(107449657), new object[]
					{
						text,
						jsonItem_0.Item64
					});
					string value = webClient.DownloadString(Class103.smethod_2(text, jsonItem_0.Item64));
					Class250 @class = JsonConvert.DeserializeObject<Class250>(value);
					Class181.smethod_2(Enum11.const_3, Web.getString_0(107449628), new object[]
					{
						@class.Min,
						@class.Max,
						@class.Currency
					});
					return @class;
				}
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, Web.getString_0(107449587), new object[]
				{
					ex.Message
				});
			}
			return null;
		}

		public static List<FlippingListJsonItem> smethod_11(IEnumerable<FlippingListItem> ienumerable_0)
		{
			Web.Class200 @class = new Web.Class200();
			@class.ienumerable_0 = ienumerable_0;
			@class.byte_0 = null;
			Thread thread = new Thread(new ThreadStart(@class.method_0));
			thread.Start();
			thread.Join();
			List<FlippingListJsonItem> result;
			if (@class.byte_0 == null || @class.byte_0.Length == 0)
			{
				result = null;
			}
			else
			{
				result = FlippingListJsonItem.smethod_1(Encoding.UTF8.GetString(@class.byte_0));
			}
			return result;
		}

		public unsafe static string smethod_12(string string_1)
		{
			void* ptr = stackalloc byte[5];
			Regex regex = new Regex(Web.getString_0(107449570));
			string html = Web.smethod_6(string_1, Class255.Cookies);
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode(Web.getString_0(107450061));
			*(byte*)ptr = ((htmlNode == null) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = Web.getString_0(107397466);
			}
			else
			{
				string innerHtml = htmlNode.InnerHtml;
				Match match = regex.Match(innerHtml);
				string text = null;
				((byte*)ptr)[1] = ((match.Groups.Count > 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					text = match.Groups[1].Value;
					((byte*)ptr)[2] = (text.EndsWith(Web.getString_0(107449968)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						text = text.Substring(0, text.Length - 3);
					}
				}
				((byte*)ptr)[3] = (text.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					result = null;
				}
				else
				{
					text = Web.getString_0(107449995) + text;
					Class268 @class = JsonConvert.DeserializeObject<Class268>(text, Util.smethod_12());
					((byte*)ptr)[4] = ((@class == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						result = null;
					}
					else
					{
						@class.Query.stats = new List<Class293>();
						@class.Query.filters = new Class288();
						@class.Query.status = new Class287(Web.getString_0(107452544));
						JObject jobject = JsonConvert.DeserializeObject<JObject>(text, Util.smethod_12());
						JToken jtoken = jobject[Web.getString_0(107369727)][Web.getString_0(107369686)];
						JToken jtoken2 = jobject[Web.getString_0(107369727)][Web.getString_0(107369709)];
						string text2 = JsonConvert.SerializeObject(@class, Util.smethod_11()).Replace(Web.getString_0(107369727), Web.getString_0(107369736));
						string newValue = (jtoken != null) ? string.Format(Web.getString_0(107449990), jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>())) : string.Empty;
						string newValue2 = (jtoken2 != null) ? string.Format(Web.getString_0(107449936), jtoken2.ToString(Formatting.None, Array.Empty<JsonConverter>())) : Web.getString_0(107449941);
						text2 = text2.Replace(Web.getString_0(107449911), newValue);
						text2 = text2.Replace(Web.getString_0(107449926), newValue2);
						text2 = text2.Replace(Web.getString_0(107449873), Web.getString_0(107449900));
						result = text2;
					}
				}
			}
			return result;
		}

		public static Class271 smethod_13(string string_1)
		{
			Class247 @class = Web.smethod_8(Class103.TradeApiUrl, string_1, Enum5.const_1);
			Class271 result;
			if (@class.Response > Enum7.const_0)
			{
				Class181.smethod_2(Enum11.const_2, Web.getString_0(107449895), new object[]
				{
					@class.Reason
				});
				Class181.smethod_2(Enum11.const_3, Web.getString_0(107449826), new object[]
				{
					string_1
				});
				result = null;
			}
			else
			{
				Class271 class2 = JsonConvert.DeserializeObject<Class271>(@class.WebData);
				result = class2;
			}
			return result;
		}

		public static Class266 smethod_14(string string_1)
		{
			Class247 @class = Web.smethod_8(Class103.ExchangeApiUrl, string_1, Enum5.const_1);
			Class266 result;
			if (@class.Response > Enum7.const_0)
			{
				Class181.smethod_2(Enum11.const_2, Web.getString_0(107449895), new object[]
				{
					@class.Reason
				});
				Class181.smethod_2(Enum11.const_3, Web.getString_0(107449269), new object[]
				{
					string_1
				});
				result = null;
			}
			else
			{
				Class266 class2 = JsonConvert.DeserializeObject<Class266>(@class.WebData, new JsonConverter[]
				{
					new FetchTradeResult.GClass20()
				});
				result = class2;
			}
			return result;
		}

		public unsafe static bool smethod_15(JsonTab jsonTab_0)
		{
			void* ptr = stackalloc byte[9];
			*(int*)ptr = jsonTab_0.i;
			((byte*)ptr)[4] = ((!Stashes.Items.ContainsKey(*(int*)ptr)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Class181.smethod_2(Enum11.const_2, Web.getString_0(107449280), new object[]
				{
					jsonTab_0.n
				});
				((byte*)ptr)[5] = 0;
			}
			else
			{
				((byte*)ptr)[6] = ((!Stashes.Items[*(int*)ptr].Any<JsonItem>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Web.getString_0(107449179), new object[]
					{
						jsonTab_0.n
					});
					((byte*)ptr)[5] = 0;
				}
				else
				{
					JsonItem jsonItem = Stashes.Items[*(int*)ptr].FirstOrDefault(new Func<JsonItem, bool>(Web.<>c.<>9.method_3));
					string string_ = Util.smethod_20(true, true, true, false, 6, 10);
					((byte*)ptr)[7] = ((jsonItem == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						Class181.smethod_2(Enum11.const_2, Web.getString_0(107449114), new object[]
						{
							jsonTab_0.n
						});
						((byte*)ptr)[5] = 0;
					}
					else
					{
						Class181.smethod_2(Enum11.const_3, Web.getString_0(107449053), new object[]
						{
							jsonItem.Name,
							jsonItem.id
						});
						Stream stream = Web.smethod_9(new List<string>
						{
							jsonItem.id
						}, string_, false);
						((byte*)ptr)[8] = ((stream == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							((byte*)ptr)[5] = 0;
						}
						else
						{
							using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
							{
								Class276 @class = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
								if (@class == null || @class.result == null || !@class.result.Any<FetchTradeResult>())
								{
									((byte*)ptr)[5] = 0;
									goto IL_1F1;
								}
								FetchTradeResult fetchTradeResult = @class.result.First<FetchTradeResult>();
								if (fetchTradeResult == null || fetchTradeResult.gone)
								{
									((byte*)ptr)[5] = 0;
									goto IL_1F1;
								}
							}
							((byte*)ptr)[5] = 1;
						}
					}
				}
			}
			IL_1F1:
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public static void smethod_16()
		{
			try
			{
				string value = File.ReadAllText(Web.getString_0(107372701));
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
				nameValueCollection.Add(Web.getString_0(107449532), Class255.class105_0.method_3(ConfigOptions.AuthKey));
				nameValueCollection.Add(Web.getString_0(107449527), value);
				Web.smethod_8(Web.getString_0(107354560), nameValueCollection.ToString(), Enum5.const_0);
			}
			catch
			{
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Web()
		{
			Strings.CreateGetStringDelegate(typeof(Web));
			Web.string_0 = Web.getString_0(107449518);
			Web.regex_0 = new Regex(Web.getString_0(107449481));
			Web.regex_1 = new Regex(Web.getString_0(107449472));
			Web.regex_2 = new Regex(Web.getString_0(107449399));
		}

		private static string string_0;

		public static MainForm mainForm_0;

		private static Regex regex_0;

		private static Regex regex_1;

		private static Regex regex_2;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class200
		{
			internal void method_0()
			{
				try
				{
					using (WebClient webClient = new WebClient())
					{
						this.byte_0 = webClient.UploadValues(Class103.string_2, Web.Class200.getString_0(107372077), FlippingListJsonItem.smethod_0(this.ienumerable_0));
					}
				}
				catch (Exception ex)
				{
					Class181.smethod_2(Enum11.const_2, Web.Class200.getString_0(107249087), new object[]
					{
						ex
					});
				}
			}

			static Class200()
			{
				Strings.CreateGetStringDelegate(typeof(Web.Class200));
			}

			public byte[] byte_0;

			public IEnumerable<FlippingListItem> ienumerable_0;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
