using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibBundle.Records
{
	public sealed class BundleRecord
	{
		public BundleContainer Bundle
		{
			get
			{
				if (this._bundle == null)
				{
					this.Read();
				}
				return this._bundle;
			}
		}

		public BundleRecord(BinaryReader br)
		{
			this.indexOffset = br.BaseStream.Position;
			this.nameLength = br.ReadInt32();
			this.Name = Encoding.UTF8.GetString(br.ReadBytes(this.nameLength)) + BundleRecord.getString_0(107320858);
			this.UncompressedSize = br.ReadInt32();
			this.Files = new List<FileRecord>();
		}

		public void Read()
		{
			this._bundle = new BundleContainer(this.Name);
		}

		public void Save(string path)
		{
			MemoryStream memoryStream = this.Bundle.Read(null);
			MemoryStream memoryStream2 = new MemoryStream();
			foreach (FileRecord fileRecord in this.Files)
			{
				if (this.dataToAdd.ContainsKey(fileRecord))
				{
					fileRecord.Offset = (int)memoryStream2.Position;
					memoryStream2.Write(this.dataToAdd[fileRecord], 0, fileRecord.Size);
				}
				else
				{
					byte[] buffer = new byte[fileRecord.Size];
					memoryStream.Seek((long)fileRecord.Offset, SeekOrigin.Begin);
					memoryStream.Read(buffer, 0, fileRecord.Size);
					fileRecord.Offset = (int)memoryStream2.Position;
					memoryStream2.Write(buffer, 0, fileRecord.Size);
				}
			}
			this.Bundle.dataToSave = memoryStream2.ToArray();
			this.UncompressedSize = this.Bundle.dataToSave.Length;
			this.Bundle.Save(path);
			this.dataToAdd = new Dictionary<FileRecord, byte[]>();
			memoryStream.Close();
			memoryStream2.Close();
		}

		static BundleRecord()
		{
			Strings.CreateGetStringDelegate(typeof(BundleRecord));
		}

		public long indexOffset;

		public int bundleIndex;

		public int nameLength;

		public string Name;

		public int UncompressedSize;

		public List<FileRecord> Files;

		internal Dictionary<FileRecord, byte[]> dataToAdd = new Dictionary<FileRecord, byte[]>();

		private BundleContainer _bundle;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
