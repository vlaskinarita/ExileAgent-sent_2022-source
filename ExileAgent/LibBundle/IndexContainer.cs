using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LibBundle.Records;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibBundle
{
	public sealed class IndexContainer : BundleContainer
	{
		public IndexContainer(string path) : base(path)
		{
			MemoryStream memoryStream = base.Read(null);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			this.Bundles = new BundleRecord[num];
			for (int i = 0; i < num; i++)
			{
				this.Bundles[i] = new BundleRecord(binaryReader)
				{
					bundleIndex = i
				};
			}
			int num2 = binaryReader.ReadInt32();
			this.Files = new FileRecord[num2];
			for (int j = 0; j < num2; j++)
			{
				FileRecord fileRecord = new FileRecord(binaryReader);
				this.Files[j] = fileRecord;
				this.FindFiles[fileRecord.Hash] = fileRecord;
				fileRecord.bundleRecord = this.Bundles[fileRecord.BundleIndex];
				this.Bundles[fileRecord.BundleIndex].Files.Add(fileRecord);
			}
			int num3 = binaryReader.ReadInt32();
			this.Directorys = new DirectoryRecord[num3];
			for (int k = 0; k < num3; k++)
			{
				this.Directorys[k] = new DirectoryRecord(binaryReader);
			}
			long position = binaryReader.BaseStream.Position;
			this.directoryBundleData = binaryReader.ReadBytes((int)(binaryReader.BaseStream.Length - position));
			binaryReader.BaseStream.Seek(position, SeekOrigin.Begin);
			BundleContainer bundleContainer = new BundleContainer(binaryReader);
			BinaryReader binaryReader2 = new BinaryReader(bundleContainer.Read(binaryReader));
			this.Hashes = new Dictionary<ulong, string>(this.Files.Length);
			foreach (DirectoryRecord directoryRecord in this.Directorys)
			{
				List<string> list = new List<string>();
				bool flag = false;
				binaryReader2.BaseStream.Seek((long)directoryRecord.Offset, SeekOrigin.Begin);
				while (binaryReader2.BaseStream.Position - (long)directoryRecord.Offset <= (long)(directoryRecord.Size - 4))
				{
					int num4 = binaryReader2.ReadInt32();
					if (num4 == 0)
					{
						if (flag = !flag)
						{
							list = new List<string>();
						}
					}
					else
					{
						num4--;
						StringBuilder stringBuilder = new StringBuilder();
						char value;
						while ((value = binaryReader2.ReadChar()) > '\0')
						{
							stringBuilder.Append(value);
						}
						string text = stringBuilder.ToString();
						if (num4 < list.Count)
						{
							text = list[num4] + text;
						}
						if (flag)
						{
							list.Add(text);
						}
						else
						{
							directoryRecord.paths.Add(text);
							this.Hashes[IndexContainer.FNV1a64Hash(text)] = text;
						}
					}
				}
			}
			binaryReader2.Close();
		}

		public static ulong FNV1a64Hash(string str)
		{
			if (str.EndsWith(IndexContainer.getString_1(107375332)))
			{
				str.TrimEnd(new char[]
				{
					'/'
				});
				str += IndexContainer.getString_1(107320854);
			}
			else
			{
				str = str.ToLower() + IndexContainer.getString_1(107320854);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			ulong num = 14695981039346656037UL;
			foreach (byte b in bytes)
			{
				num = (num ^ (ulong)b) * 1099511628211UL;
			}
			return num;
		}

		public override void Save(string path)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			binaryWriter.Write(this.Bundles.Length);
			foreach (BundleRecord bundleRecord in this.Bundles)
			{
				binaryWriter.Write(bundleRecord.nameLength);
				binaryWriter.Write(Encoding.UTF8.GetBytes(bundleRecord.Name), 0, bundleRecord.nameLength);
				binaryWriter.Write(bundleRecord.UncompressedSize);
			}
			binaryWriter.Write(this.Files.Length);
			foreach (FileRecord fileRecord in this.Files)
			{
				binaryWriter.Write(fileRecord.Hash);
				binaryWriter.Write(fileRecord.BundleIndex);
				binaryWriter.Write(fileRecord.Offset);
				binaryWriter.Write(fileRecord.Size);
			}
			binaryWriter.Write(this.Directorys.Length);
			foreach (DirectoryRecord directoryRecord in this.Directorys)
			{
				binaryWriter.Write(directoryRecord.Hash);
				binaryWriter.Write(directoryRecord.Offset);
				binaryWriter.Write(directoryRecord.Size);
				binaryWriter.Write(directoryRecord.RecursiveSize);
			}
			binaryWriter.Write(this.directoryBundleData);
			binaryWriter.Flush();
			this.dataToSave = ((MemoryStream)binaryWriter.BaseStream).ToArray();
			binaryWriter.Close();
			base.Save(path);
		}

		static IndexContainer()
		{
			Strings.CreateGetStringDelegate(typeof(IndexContainer));
		}

		public BundleRecord[] Bundles;

		public FileRecord[] Files;

		public DirectoryRecord[] Directorys;

		public Dictionary<ulong, FileRecord> FindFiles = new Dictionary<ulong, FileRecord>();

		public Dictionary<ulong, string> Hashes;

		public byte[] directoryBundleData;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
