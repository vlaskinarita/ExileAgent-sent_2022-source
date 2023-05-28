using System;
using System.Linq;

namespace WebSocket4Net.Protocol
{
	internal sealed class ProtocolProcessorFactory
	{
		public ProtocolProcessorFactory(params IProtocolProcessor[] processors)
		{
			this.m_OrderedProcessors = (from p in processors
			orderby (int)p.Version descending
			select p).ToArray<IProtocolProcessor>();
		}

		public IProtocolProcessor GetProcessorByVersion(WebSocketVersion version)
		{
			return this.m_OrderedProcessors.FirstOrDefault((IProtocolProcessor p) => p.Version == version);
		}

		public IProtocolProcessor GetPreferedProcessorFromAvialable(int[] versions)
		{
			foreach (int num in from i in versions
			orderby i descending
			select i)
			{
				foreach (IProtocolProcessor protocolProcessor in this.m_OrderedProcessors)
				{
					int version = (int)protocolProcessor.Version;
					if (version < num)
					{
						break;
					}
					if (version <= num)
					{
						return protocolProcessor;
					}
				}
			}
			return null;
		}

		private IProtocolProcessor[] m_OrderedProcessors;
	}
}
