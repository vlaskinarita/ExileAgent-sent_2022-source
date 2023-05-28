using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using ns0;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns8
{
	internal sealed class Class103
	{
		public static string TradeApiUrl
		{
			get
			{
				return Class103.getString_0(107355502) + Class255.class105_0.method_3(ConfigOptions.League);
			}
		}

		public static string TradeWebsiteUrl
		{
			get
			{
				return Class103.getString_0(107354897) + Class255.class105_0.method_3(ConfigOptions.League);
			}
		}

		public static string ExchangeUrl
		{
			get
			{
				return Class103.getString_0(107354840) + Class255.class105_0.method_3(ConfigOptions.League);
			}
		}

		public static string ExchangeApiUrl
		{
			get
			{
				return Class103.getString_0(107354779) + Class255.class105_0.method_3(ConfigOptions.League);
			}
		}

		public static string PoeNinjaCurrencyUrl
		{
			get
			{
				return string.Format(Class103.getString_0(107354714), Class255.class105_0.method_3(ConfigOptions.League));
			}
		}

		public static string PoeNinjaDivCardUrl
		{
			get
			{
				return string.Format(Class103.getString_0(107355133), Class255.class105_0.method_3(ConfigOptions.League));
			}
		}

		public static string smethod_0(string string_15, string string_16)
		{
			return string.Format(Class103.getString_0(107355052), Class103.string_12.Replace(Class103.getString_0(107355003), Class103.getString_0(107354994)), string_15, string_16);
		}

		public static string smethod_1(List<string> list_0, string string_15)
		{
			return string.Format(Class103.getString_0(107355021), Class103.string_11, string.Join(Class103.getString_0(107392421), list_0), string_15);
		}

		public static string smethod_2(string string_15, string string_16)
		{
			return string.Format(Class103.getString_0(107354964), string_15, string_16);
		}

		public static string smethod_3(string string_15)
		{
			return string.Format(Class103.getString_0(107354399), string_15);
		}

		public static string smethod_4()
		{
			return Class103.getString_0(107354382) + Class255.class105_0.method_3(ConfigOptions.AuthKey);
		}

		public static bool smethod_5(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
		{
			return x509Certificate_0.Subject.Contains(Class103.getString_0(107354297)) && x509Certificate_0.GetSerialNumberString() == Class103.getString_0(107354272) && x509Certificate_0.GetPublicKeyString() == Class103.getString_0(107354227);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class103()
		{
			Strings.CreateGetStringDelegate(typeof(Class103));
			Class103.string_0 = Class103.getString_0(107354593);
			Class103.string_1 = Class103.getString_0(107354524);
			Class103.string_2 = Class103.string_0 + Class103.getString_0(107354507);
			Class103.string_3 = Class103.string_0 + Class103.getString_0(107354462);
			Class103.string_4 = Class103.string_0 + Class103.getString_0(107354453);
			Class103.string_5 = Class103.getString_0(107354428);
			Class103.string_6 = Class103.getString_0(107353891);
			Class103.string_7 = Class103.string_0 + Class103.getString_0(107353814);
			Class103.string_8 = Class103.getString_0(107353833);
			Class103.string_9 = Class103.getString_0(107353732);
			Class103.string_10 = Class103.getString_0(107354111);
			Class103.string_11 = Class103.getString_0(107354030);
			Class103.string_12 = Class103.getString_0(107353937);
			Class103.string_13 = Class103.getString_0(107353368);
			Class103.string_14 = Class103.getString_0(107353331);
		}

		public static string string_0;

		public static string string_1;

		public static string string_2;

		public static string string_3;

		public static string string_4;

		public static string string_5;

		public static string string_6;

		public static string string_7;

		public static string string_8;

		public static string string_9;

		public static string string_10;

		public static string string_11;

		public static string string_12;

		public static string string_13;

		public static string string_14;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
