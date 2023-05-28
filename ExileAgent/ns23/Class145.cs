using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns1;
using ns10;
using ns17;
using ns29;
using ns34;
using ns35;
using ns4;
using ns8;
using ns9;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns23
{
	internal static class Class145
	{
		public unsafe static string smethod_0(string string_0, Class249 class249_0)
		{
			void* ptr = stackalloc byte[17];
			string text = Web.smethod_12(string_0);
			Thread.Sleep(5500);
			*(byte*)ptr = (text.smethod_25() ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_2(Enum11.const_2, Class145.getString_0(107369504), new object[]
				{
					string_0
				});
				result = null;
			}
			else
			{
				Class268 @class = JsonConvert.DeserializeObject<Class268>(text.Replace(Class145.getString_0(107369495), Class145.getString_0(107369486)));
				((byte*)ptr)[1] = ((@class == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = null;
				}
				else
				{
					Class267 query = @class.Query;
					query.stats = new List<Class293>();
					((byte*)ptr)[2] = ((query.filters == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						query.filters = new Class288();
					}
					((byte*)ptr)[3] = ((query.filters.TradeFilters == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						query.filters.TradeFilters = new Class295();
					}
					((byte*)ptr)[4] = ((query.filters.TradeFilters.filters.price == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						query.filters.TradeFilters.filters.price = new Class289();
					}
					query.filters.TradeFilters.filters.price.max = new double?((double)class249_0.Amount);
					((byte*)ptr)[5] = ((class249_0.Currency != Class145.getString_0(107406751)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						query.filters.TradeFilters.filters.price.option = API.smethod_4(class249_0.Currency);
					}
					JObject jobject = JsonConvert.DeserializeObject<JObject>(text, Util.smethod_12());
					JToken jtoken = jobject[Class145.getString_0(107369495)][Class145.getString_0(107369445)];
					JToken jtoken2 = jobject[Class145.getString_0(107369495)][Class145.getString_0(107369468)];
					string value = JsonConvert.SerializeObject(@class, Util.smethod_11()).Replace(Class145.getString_0(107369486), Class145.getString_0(107369495));
					JObject jobject2 = JsonConvert.DeserializeObject<JObject>(value, Util.smethod_12());
					foreach (JToken jtoken3 in jobject2[Class145.getString_0(107369495)].Children().ToList<JToken>())
					{
						JProperty jproperty = (JProperty)jtoken3;
						((byte*)ptr)[6] = ((jproperty.Name == Class145.getString_0(107369455)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							jproperty.Remove();
						}
						if (query.name == Class145.getString_0(107369926) && query.disc != null)
						{
							((byte*)ptr)[7] = ((jproperty.Name == Class145.getString_0(107369897)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								jproperty.Remove();
							}
							((byte*)ptr)[8] = ((jproperty.Name == Class145.getString_0(107369888)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								jproperty.Remove();
							}
						}
						((byte*)ptr)[9] = ((jtoken != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							((byte*)ptr)[10] = ((jproperty.Name == Class145.getString_0(107369445)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								jproperty.Remove();
							}
						}
						((byte*)ptr)[11] = ((jtoken2 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							((byte*)ptr)[12] = ((jproperty.Name == Class145.getString_0(107369468)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								jproperty.Remove();
							}
						}
					}
					if (query.name == Class145.getString_0(107369926) && query.disc != null)
					{
						Class269 o = new Class269(query.disc, Class145.getString_0(107369926));
						Class269 o2 = new Class269(query.disc, Class145.getString_0(107352454));
						((JObject)jobject2[Class145.getString_0(107369495)]).Add(Class145.getString_0(107369897), JToken.FromObject(o));
						((JObject)jobject2[Class145.getString_0(107369495)]).Add(Class145.getString_0(107369888), JToken.FromObject(o2));
					}
					((byte*)ptr)[13] = ((jtoken != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						((JObject)jobject2[Class145.getString_0(107369495)]).Add(Class145.getString_0(107369445), jtoken);
					}
					((byte*)ptr)[14] = ((jtoken2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						((JObject)jobject2[Class145.getString_0(107369495)]).Add(Class145.getString_0(107369468), jtoken2);
						foreach (JToken jtoken4 in jobject2[Class145.getString_0(107369495)][Class145.getString_0(107369468)].Children().ToList<JToken>())
						{
							JProperty jproperty2 = (JProperty)jtoken4;
							((byte*)ptr)[15] = ((jproperty2.Name == Class145.getString_0(107369911)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 15) != 0)
							{
								jproperty2.Remove();
							}
						}
					}
					((byte*)ptr)[16] = ((jtoken2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						((JObject)jobject2[Class145.getString_0(107369495)][Class145.getString_0(107369468)]).Add(Class145.getString_0(107369911), JToken.FromObject(query.filters.TradeFilters, JsonSerializer.Create(Util.smethod_11())));
					}
					string text2 = JsonConvert.SerializeObject(jobject2, Util.smethod_11());
					result = text2;
				}
			}
			return result;
		}

		public unsafe static string smethod_1(string string_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((string_0 == null) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				Class271 @class = Web.smethod_13(string_0);
				Thread.Sleep(5500);
				((byte*)ptr)[1] = ((@class == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = null;
				}
				else
				{
					result = @class.id;
				}
			}
			return result;
		}

		public static string smethod_2(string string_0, Class249 class249_0)
		{
			string string_ = Class145.smethod_0(Class103.TradeWebsiteUrl + Class145.getString_0(107372497) + string_0, class249_0);
			string text = Class145.smethod_1(string_);
			Class181.smethod_2(Enum11.const_3, Class145.getString_0(107369858), new object[]
			{
				string_0,
				text
			});
			return text;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class145()
		{
			Strings.CreateGetStringDelegate(typeof(Class145));
			Class145.regex_0 = new Regex(Class145.getString_0(107369829));
		}

		public static Regex regex_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
