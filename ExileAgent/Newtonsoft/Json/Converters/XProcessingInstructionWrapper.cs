using System;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XProcessingInstructionWrapper : XObjectWrapper
	{
		private XProcessingInstruction ProcessingInstruction
		{
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction) : base(processingInstruction)
		{
		}

		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
