using System;

namespace WebSocket4Net.Protocol
{
	public sealed class CloseStatusCodeHybi10 : ICloseStatusCode
	{
		public CloseStatusCodeHybi10()
		{
			this.NormalClosure = 1000;
			this.GoingAway = 1001;
			this.ProtocolError = 1002;
			this.NotAcceptableData = 1003;
			this.TooLargeFrame = 1004;
			this.InvalidUTF8 = 1007;
			this.ViolatePolicy = 1000;
			this.ExtensionNotMatch = 1000;
			this.UnexpectedCondition = 1000;
			this.TLSHandshakeFailure = 1000;
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
