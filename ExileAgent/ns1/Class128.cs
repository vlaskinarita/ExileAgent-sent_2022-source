using System;
using ns29;
using ns35;
using PusherClient;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns1
{
	internal sealed class Class128 : ITraceLogger
	{
		public void TraceError(string message)
		{
			Class181.smethod_2(Enum11.const_2, Class128.getString_0(107371201), new object[]
			{
				message
			});
		}

		public void TraceInformation(string message)
		{
			Class181.smethod_2(Enum11.const_3, Class128.getString_0(107371201), new object[]
			{
				message
			});
		}

		public void TraceWarning(string message)
		{
			Class181.smethod_2(Enum11.const_1, Class128.getString_0(107371201), new object[]
			{
				message
			});
		}

		static Class128()
		{
			Strings.CreateGetStringDelegate(typeof(Class128));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
