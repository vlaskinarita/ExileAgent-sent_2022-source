using System;

namespace WebSocket4Net.Protocol.FramePartReader
{
	internal sealed class PayloadDataReader : DataFramePartReader
	{
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			long num = (long)lastLength + frame.ActualPayloadLength;
			if ((long)frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			nextPartReader = null;
			return (int)((long)frame.Length - num);
		}
	}
}
