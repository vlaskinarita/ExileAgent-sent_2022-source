using System;
using System.IO;

namespace LibBundle.Records
{
	public sealed class FileRecord
	{
		public FileRecord(BinaryReader br)
		{
			this.indexOffset = br.BaseStream.Position;
			this.Hash = br.ReadUInt64();
			this.BundleIndex = br.ReadInt32();
			this.Offset = br.ReadInt32();
			this.Size = br.ReadInt32();
		}

		public byte[] Read(Stream stream = null)
		{
			byte[] array;
			if (!this.bundleRecord.dataToAdd.TryGetValue(this, out array))
			{
				array = new byte[this.Size];
				Stream stream2 = (stream == null) ? this.bundleRecord.Bundle.Read(null) : stream;
				stream2.Seek((long)this.Offset, SeekOrigin.Begin);
				stream2.Read(array, 0, this.Size);
			}
			return array;
		}

		public void Move(BundleRecord target)
		{
			byte[] value;
			if (this.bundleRecord.dataToAdd.TryGetValue(this, out value))
			{
				this.bundleRecord.dataToAdd.Remove(this);
			}
			else
			{
				value = this.Read(null);
			}
			this.bundleRecord.Files.Remove(this);
			target.Files.Add(this);
			target.dataToAdd[this] = value;
			this.bundleRecord = target;
			this.BundleIndex = target.bundleIndex;
		}

		public void Write(byte[] data)
		{
			this.Size = data.Length;
			this.bundleRecord.dataToAdd[this] = data;
		}

		public long indexOffset;

		public ulong Hash;

		public int BundleIndex;

		public int Offset;

		public int Size;

		public BundleRecord bundleRecord;
	}
}
