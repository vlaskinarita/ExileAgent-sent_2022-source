using System;

namespace WebSocketSharp.Net
{
	public enum AuthenticationSchemes
	{
		None,
		Digest,
		Basic = 8,
		Anonymous = 32768
	}
}
