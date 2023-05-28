using System;

namespace PusherClient
{
	public sealed class ChannelDecryptionException : ChannelException
	{
		public ChannelDecryptionException(string message) : base(message, ErrorCodes.ChannelDecryptionFailure, null, null)
		{
		}
	}
}
