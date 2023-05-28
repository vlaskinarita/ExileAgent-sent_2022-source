using System;

namespace WebSocketSharp
{
	public sealed class CloseEventArgs : EventArgs
	{
		internal CloseEventArgs(PayloadData payloadData, bool clean)
		{
			this._payloadData = payloadData;
			this._clean = clean;
		}

		internal CloseEventArgs(ushort code, string reason, bool clean)
		{
			this._payloadData = new PayloadData(code, reason);
			this._clean = clean;
		}

		public ushort Code
		{
			get
			{
				return this._payloadData.Code;
			}
		}

		public string Reason
		{
			get
			{
				return this._payloadData.Reason;
			}
		}

		public bool WasClean
		{
			get
			{
				return this._clean;
			}
		}

		private bool _clean;

		private PayloadData _payloadData;
	}
}
