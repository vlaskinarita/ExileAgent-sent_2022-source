using System;
using System.Collections.Generic;
using System.IO;

namespace LibBundle.Records
{
	public sealed class DirectoryRecord
	{
		public DirectoryRecord(BinaryReader br)
		{
			this.indexOffset = br.BaseStream.Position;
			this.Hash = br.ReadUInt64();
			this.Offset = br.ReadInt32();
			this.Size = br.ReadInt32();
			this.RecursiveSize = br.ReadInt32();
			this.paths = new List<string>();
		}

		public long indexOffset;

		public ulong Hash;

		public int Offset;

		public int Size;

		public int RecursiveSize;

		public List<string> paths;
	}
}
