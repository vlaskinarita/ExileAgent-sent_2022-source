using System;

namespace WebSocketSharp
{
	internal enum Opcode : byte
	{
		Cont,
		Text,
		Binary,
		Close = 8,
		Ping,
		Pong
	}
}
