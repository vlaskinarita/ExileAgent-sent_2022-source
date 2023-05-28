using System;

namespace WebSocket4Net.Protocol
{
	internal sealed class DraftHybi00HandshakeReader : HandshakeReader
	{
		public DraftHybi00HandshakeReader(WebSocket websocket) : base(websocket)
		{
		}

		private void SetDataReader()
		{
			base.NextCommandReader = new DraftHybi00DataReader(this);
		}

		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			if (this.m_ReceivedChallengeLength < 0)
			{
				WebSocketCommandInfo commandInfo = base.GetCommandInfo(readBuffer, offset, length, out left);
				if (commandInfo == null)
				{
					return null;
				}
				if (HandshakeReader.BadRequestCode.Equals(commandInfo.Key))
				{
					return commandInfo;
				}
				this.m_ReceivedChallengeLength = 0;
				this.m_HandshakeCommand = commandInfo;
				int srcOffset = offset + length - left;
				if (left < this.m_ExpectedChallengeLength)
				{
					if (left > 0)
					{
						Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, left);
						this.m_ReceivedChallengeLength = left;
						left = 0;
					}
					return null;
				}
				if (left == this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, left);
					this.SetDataReader();
					this.m_HandshakeCommand.Data = this.m_Challenges;
					left = 0;
					return this.m_HandshakeCommand;
				}
				Buffer.BlockCopy(readBuffer, srcOffset, this.m_Challenges, 0, this.m_ExpectedChallengeLength);
				left -= this.m_ExpectedChallengeLength;
				this.SetDataReader();
				this.m_HandshakeCommand.Data = this.m_Challenges;
				return this.m_HandshakeCommand;
			}
			else
			{
				int num = this.m_ReceivedChallengeLength + length;
				if (num < this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, length);
					left = 0;
					this.m_ReceivedChallengeLength = num;
					return null;
				}
				if (num == this.m_ExpectedChallengeLength)
				{
					Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, length);
					left = 0;
					this.SetDataReader();
					this.m_HandshakeCommand.Data = this.m_Challenges;
					return this.m_HandshakeCommand;
				}
				int num2 = this.m_ExpectedChallengeLength - this.m_ReceivedChallengeLength;
				Buffer.BlockCopy(readBuffer, offset, this.m_Challenges, this.m_ReceivedChallengeLength, num2);
				left = length - num2;
				this.SetDataReader();
				this.m_HandshakeCommand.Data = this.m_Challenges;
				return this.m_HandshakeCommand;
			}
		}

		private int m_ReceivedChallengeLength = -1;

		private int m_ExpectedChallengeLength = 16;

		private WebSocketCommandInfo m_HandshakeCommand;

		private byte[] m_Challenges = new byte[16];
	}
}
