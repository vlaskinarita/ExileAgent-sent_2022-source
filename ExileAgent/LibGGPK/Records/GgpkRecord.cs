using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibGGPK.Records
{
	public sealed class GgpkRecord : BaseRecord
	{
		public GgpkRecord(uint length)
		{
			base.Length = length;
			this.RecordOffsets = new long[2];
		}

		public GgpkRecord(uint length, BinaryReader br)
		{
			base.RecordBegin = br.BaseStream.Position - 8L;
			base.Length = length;
			this.Read(br);
		}

		public GgpkRecord(long recordBegin, uint length, long[] recordOffsets)
		{
			base.RecordBegin = recordBegin;
			base.Length = length;
			this.RecordOffsets = recordOffsets;
		}

		public override void Read(BinaryReader br)
		{
			this.Version = br.ReadUInt32();
			this.RecordOffsets = new long[2];
			for (int i = 0; i < 2; i++)
			{
				this.RecordOffsets[i] = br.ReadInt64();
			}
		}

		public override void Write(BinaryWriter bw, Dictionary<long, long> changedOffsets)
		{
			bw.Write(base.Length);
			bw.Write(Encoding.ASCII.GetBytes(GgpkRecord.getString_0(107320505)));
			bw.Write(this.Version);
			long num = this.RecordOffsets[0];
			bw.Write(changedOffsets.ContainsKey(num) ? changedOffsets[num] : num);
			num = this.RecordOffsets[1];
			bw.Write(changedOffsets.ContainsKey(num) ? changedOffsets[num] : num);
		}

		public override string ToString()
		{
			return GgpkRecord.getString_0(107320505);
		}

		static GgpkRecord()
		{
			Strings.CreateGetStringDelegate(typeof(GgpkRecord));
		}

		public const string Tag = "GGPK";

		public long[] RecordOffsets;

		public uint Version;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
