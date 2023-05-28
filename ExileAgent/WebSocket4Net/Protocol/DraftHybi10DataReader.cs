using System;
using System.Collections.Generic;
using WebSocket4Net.Common;
using WebSocket4Net.Protocol.FramePartReader;

namespace WebSocket4Net.Protocol
{
	internal sealed class DraftHybi10DataReader : IClientCommandReader<WebSocketCommandInfo>
	{
		public DraftHybi10DataReader()
		{
			this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
			this.m_PartReader = DataFramePartReader.NewReader;
		}

		public int LeftBufferSize
		{
			get
			{
				return this.m_Frame.InnerData.Count;
			}
		}

		public IClientCommandReader<WebSocketCommandInfo> NextCommandReader
		{
			get
			{
				return this;
			}
		}

		protected void AddArraySegment(ArraySegmentList segments, byte[] buffer, int offset, int length, bool isReusableBuffer)
		{
			segments.AddSegment(buffer, offset, length, isReusableBuffer);
		}

		public WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			this.AddArraySegment(this.m_Frame.InnerData, readBuffer, offset, length, true);
			IDataFramePartReader dataFramePartReader;
			int num = this.m_PartReader.Process(this.m_LastPartLength, this.m_Frame, out dataFramePartReader);
			if (num < 0)
			{
				left = 0;
				return null;
			}
			left = num;
			if (left > 0)
			{
				this.m_Frame.InnerData.TrimEnd(left);
			}
			if (dataFramePartReader == null)
			{
				WebSocketCommandInfo result;
				if (this.m_Frame.IsControlFrame)
				{
					result = new WebSocketCommandInfo(this.m_Frame);
					this.m_Frame.Clear();
				}
				else if (this.m_Frame.FIN)
				{
					if (this.m_PreviousFrames != null && this.m_PreviousFrames.Count > 0)
					{
						this.m_PreviousFrames.Add(this.m_Frame);
						this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
						result = new WebSocketCommandInfo(this.m_PreviousFrames);
						this.m_PreviousFrames = null;
					}
					else
					{
						result = new WebSocketCommandInfo(this.m_Frame);
						this.m_Frame.Clear();
					}
				}
				else
				{
					if (this.m_PreviousFrames == null)
					{
						this.m_PreviousFrames = new List<WebSocketDataFrame>();
					}
					this.m_PreviousFrames.Add(this.m_Frame);
					this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
					result = null;
				}
				this.m_LastPartLength = 0;
				this.m_PartReader = DataFramePartReader.NewReader;
				return result;
			}
			this.m_LastPartLength = this.m_Frame.InnerData.Count - num;
			this.m_PartReader = dataFramePartReader;
			return null;
		}

		private List<WebSocketDataFrame> m_PreviousFrames;

		private WebSocketDataFrame m_Frame;

		private IDataFramePartReader m_PartReader;

		private int m_LastPartLength;
	}
}
