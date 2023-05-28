using System;

namespace System
{
	internal static class NotImplemented
	{
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
