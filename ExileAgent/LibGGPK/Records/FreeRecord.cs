using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibGGPK.Records
{
	public sealed class FreeRecord : BaseRecord
	{
		public FreeRecord(uint length, long recordBegin, long nextFreeOffset)
		{
			base.Length = length;
			base.RecordBegin = recordBegin;
			this.NextFreeOffset = nextFreeOffset;
		}

		public FreeRecord(uint length, BinaryReader br)
		{
			base.RecordBegin = br.BaseStream.Position - 8L;
			base.Length = length;
			this.Read(br);
		}

		public override void Read(BinaryReader br)
		{
			this.NextFreeOffset = br.ReadInt64();
			br.BaseStream.Seek((long)((ulong)(base.Length - 16U)), SeekOrigin.Current);
		}

		public override void Write(BinaryWriter bw, Dictionary<long, long> changedOffsets)
		{
			bw.Write(base.Length);
			bw.Write(Encoding.ASCII.GetBytes(FreeRecord.getString_0(107320460)));
			bw.Write(this.NextFreeOffset);
		}

		public override string ToString()
		{
			return FreeRecord.getString_0(107320460);
		}

		static FreeRecord()
		{
			Strings.CreateGetStringDelegate(typeof(FreeRecord));
		}

		public const string Tag = "FREE";

		public long NextFreeOffset;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
