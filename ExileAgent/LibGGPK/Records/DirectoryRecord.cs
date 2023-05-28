using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibGGPK.Records
{
	public sealed class DirectoryRecord : BaseRecord
	{
		public DirectoryRecord(uint length, BinaryReader br)
		{
			base.RecordBegin = br.BaseStream.Position - 8L;
			base.Length = length;
			this.Entries = new List<DirectoryRecord.DirectoryEntry>();
			this.Read(br);
		}

		public DirectoryRecord(long recordBegin, uint length, byte[] hash, string name, long entriesBegin, List<DirectoryRecord.DirectoryEntry> entries)
		{
			base.RecordBegin = recordBegin;
			base.Length = length;
			this.Hash = hash;
			this.Name = name;
			this.EntriesBegin = entriesBegin;
			this.Entries = entries;
		}

		public override void Read(BinaryReader br)
		{
			int num = br.ReadInt32();
			int num2 = br.ReadInt32();
			this.Hash = br.ReadBytes(32);
			this.Name = Encoding.Unicode.GetString(br.ReadBytes(2 * (num - 1)));
			br.BaseStream.Seek(2L, SeekOrigin.Current);
			this.EntriesBegin = br.BaseStream.Position;
			for (int i = 0; i < num2; i++)
			{
				this.Entries.Add(new DirectoryRecord.DirectoryEntry
				{
					EntryNameHash = br.ReadUInt32(),
					Offset = br.ReadInt64()
				});
			}
		}

		public override void Write(BinaryWriter bw, Dictionary<long, long> changedOffsets)
		{
			long position = bw.BaseStream.Position;
			if (position != base.RecordBegin)
			{
				changedOffsets[base.RecordBegin] = position;
			}
			bw.Write(base.Length);
			bw.Write(Encoding.ASCII.GetBytes(DirectoryRecord.getString_0(107320427)));
			bw.Write(this.Name.Length + 1);
			bw.Write(this.Entries.Count);
			bw.Write(this.Hash);
			bw.Write(Encoding.Unicode.GetBytes(this.Name));
			bw.Write(0);
			foreach (DirectoryRecord.DirectoryEntry directoryEntry in this.Entries)
			{
				bw.Write(directoryEntry.EntryNameHash);
				long offset = directoryEntry.Offset;
				bw.Write(changedOffsets.ContainsKey(offset) ? changedOffsets[offset] : offset);
			}
		}

		public void UpdateOffset(string ggpkPath, uint nameHash, long newEntryOffset)
		{
			DirectoryRecord.DirectoryEntry directoryEntry = this.Entries.FirstOrDefault((DirectoryRecord.DirectoryEntry e) => e.EntryNameHash == nameHash);
			if (directoryEntry.Offset == 0L)
			{
				throw new ApplicationException(DirectoryRecord.getString_0(107352636));
			}
			using (FileStream fileStream = File.Open(ggpkPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
			{
				fileStream.Seek(this.EntriesBegin + (long)(12 * this.Entries.IndexOf(directoryEntry)) + 4L, SeekOrigin.Begin);
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(newEntryOffset);
				directoryEntry.Offset = newEntryOffset;
			}
		}

		public uint GetNameHash()
		{
			return Murmur.Hash2(this.Name, 0U);
		}

		public override string ToString()
		{
			return this.Name;
		}

		static DirectoryRecord()
		{
			Strings.CreateGetStringDelegate(typeof(DirectoryRecord));
		}

		public const string Tag = "PDIR";

		public byte[] Hash;

		public string Name;

		public List<DirectoryRecord.DirectoryEntry> Entries;

		public long EntriesBegin;

		[NonSerialized]
		internal static GetString getString_0;

		public struct DirectoryEntry
		{
			public uint EntryNameHash;

			public long Offset;
		}
	}
}
