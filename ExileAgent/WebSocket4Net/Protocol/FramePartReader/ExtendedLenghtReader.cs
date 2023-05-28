using System;

namespace WebSocket4Net.Protocol.FramePartReader
{
	internal sealed class ExtendedLenghtReader : DataFramePartReader
	{
		public override int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader)
		{
			int num = 2;
			if (frame.PayloadLenght == 126)
			{
				num += 2;
			}
			else
			{
				num += 8;
			}
			if (frame.Length < num)
			{
				nextPartReader = this;
				return -1;
			}
			if (frame.HasMask)
			{
				nextPartReader = DataFramePartReader.MaskKeyReader;
			}
			else
			{
				if (frame.ActualPayloadLength == 0L)
				{
					nextPartReader = null;
					return (int)((long)frame.Length - (long)num);
				}
				nextPartReader = DataFramePartReader.PayloadDataReader;
			}
			if (frame.Length > num)
			{
				return nextPartReader.Process(num, frame, out nextPartReader);
			}
			return 0;
		}
	}
}
