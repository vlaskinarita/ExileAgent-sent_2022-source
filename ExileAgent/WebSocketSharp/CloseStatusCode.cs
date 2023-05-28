using System;

namespace WebSocketSharp
{
	public enum CloseStatusCode : ushort
	{
		Normal = 1000,
		Away,
		ProtocolError,
		UnsupportedData,
		Undefined,
		NoStatus,
		Abnormal,
		InvalidData,
		PolicyViolation,
		TooBig,
		MandatoryExtension,
		ServerError,
		TlsHandshakeFailure = 1015
	}
}
