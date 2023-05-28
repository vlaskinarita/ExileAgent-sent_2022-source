using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace WebSocket4Net.Protocol
{
	internal sealed class Rfc6455Processor : DraftHybi10Processor
	{
		public Rfc6455Processor() : base(WebSocketVersion.Rfc6455, new CloseStatusCodeRfc6455(), Rfc6455Processor.getString_2(107142709))
		{
		}

		static Rfc6455Processor()
		{
			Strings.CreateGetStringDelegate(typeof(Rfc6455Processor));
		}

		[NonSerialized]
		internal static GetString getString_2;
	}
}
