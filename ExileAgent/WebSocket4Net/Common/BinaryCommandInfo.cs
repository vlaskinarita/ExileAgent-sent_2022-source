using System;

namespace WebSocket4Net.Common
{
	public sealed class BinaryCommandInfo : CommandInfo<byte[]>
	{
		public BinaryCommandInfo(string key, byte[] data) : base(key, data)
		{
		}
	}
}
