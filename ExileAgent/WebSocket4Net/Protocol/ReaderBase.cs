﻿using System;
using WebSocket4Net.Common;

namespace WebSocket4Net.Protocol
{
	public abstract class ReaderBase : IClientCommandReader<WebSocketCommandInfo>
	{
		private protected WebSocket WebSocket { protected get; private set; }

		public ReaderBase(WebSocket websocket)
		{
			this.WebSocket = websocket;
			this.m_BufferSegments = new ArraySegmentList();
		}

		protected ArraySegmentList BufferSegments
		{
			get
			{
				return this.m_BufferSegments;
			}
		}

		public ReaderBase(ReaderBase previousCommandReader)
		{
			this.m_BufferSegments = previousCommandReader.BufferSegments;
		}

		public abstract WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left);

		public IClientCommandReader<WebSocketCommandInfo> NextCommandReader { get; internal set; }

		protected void AddArraySegment(byte[] buffer, int offset, int length)
		{
			this.BufferSegments.AddSegment(buffer, offset, length, true);
		}

		protected void ClearBufferSegments()
		{
			this.BufferSegments.ClearSegements();
		}

		private readonly ArraySegmentList m_BufferSegments;
	}
}
