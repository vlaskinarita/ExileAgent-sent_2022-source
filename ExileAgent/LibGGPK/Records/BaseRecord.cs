using System;
using System.Collections.Generic;
using System.IO;

namespace LibGGPK.Records
{
	public abstract class BaseRecord
	{
		public uint Length { get; protected set; }

		public long RecordBegin { get; protected set; }

		public abstract void Read(BinaryReader br);

		public abstract void Write(BinaryWriter bw, Dictionary<long, long> changedOffsets);
	}
}
