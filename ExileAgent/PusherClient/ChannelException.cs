using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public class ChannelException : PusherException, IChannelException
	{
		public ChannelException(string message, ErrorCodes code, string channelName, string socketId) : base(message, code)
		{
			this.ChannelName = channelName;
			this.SocketID = socketId;
		}

		public ChannelException(ErrorCodes code, string channelName, string socketId, Exception innerException) : base(string.Concat(new string[]
		{
			ChannelException.getString_0(107312294),
			channelName,
			ChannelException.getString_0(107404107),
			Environment.NewLine,
			innerException.Message
		}), code, innerException)
		{
			this.ChannelName = channelName;
			this.SocketID = socketId;
		}

		public ChannelException(string message, ErrorCodes code, string channelName, string socketId, Exception innerException) : base(message, code, innerException)
		{
			this.ChannelName = channelName;
			this.SocketID = socketId;
		}

		public string ChannelName { get; set; }

		public string EventName { get; set; }

		public string MessageData { get; set; }

		public string SocketID { get; set; }

		public Channel Channel { get; set; }

		static ChannelException()
		{
			Strings.CreateGetStringDelegate(typeof(ChannelException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
