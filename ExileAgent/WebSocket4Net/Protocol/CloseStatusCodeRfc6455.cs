using System;

namespace WebSocket4Net.Protocol
{
	public sealed class CloseStatusCodeRfc6455 : ICloseStatusCode
	{
		public CloseStatusCodeRfc6455()
		{
			this.NormalClosure = 1000;
			this.GoingAway = 1001;
			this.ProtocolError = 1002;
			this.NotAcceptableData = 1003;
			this.TooLargeFrame = 1009;
			this.InvalidUTF8 = 1007;
			this.ViolatePolicy = 1008;
			this.ExtensionNotMatch = 1010;
			this.UnexpectedCondition = 1011;
			this.NoStatusCode = 1005;
		}

		public short NormalClosure { get; private set; }

		public short GoingAway { get; private set; }

		public short ProtocolError { get; private set; }

		public short NotAcceptableData { get; private set; }

		public short TooLargeFrame { get; private set; }

		public short InvalidUTF8 { get; private set; }

		public short ViolatePolicy { get; private set; }

		public short ExtensionNotMatch { get; private set; }

		public short UnexpectedCondition { get; private set; }

		public short TLSHandshakeFailure { get; private set; }

		public short NoStatusCode { get; private set; }
	}
}
