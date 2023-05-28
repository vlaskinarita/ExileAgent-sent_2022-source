using System;

namespace WebSocket4Net.Protocol.FramePartReader
{
	internal abstract class DataFramePartReader : IDataFramePartReader
	{
		public abstract int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader);

		public static IDataFramePartReader NewReader
		{
			get
			{
				return DataFramePartReader.FixPartReader;
			}
		}

		private protected static IDataFramePartReader FixPartReader { protected get; private set; } = new FixPartReader();

		private protected static IDataFramePartReader ExtendedLenghtReader { protected get; private set; } = new ExtendedLenghtReader();

		private protected static IDataFramePartReader MaskKeyReader { protected get; private set; } = new MaskKeyReader();

		private protected static IDataFramePartReader PayloadDataReader { protected get; private set; } = new PayloadDataReader();
	}
}
