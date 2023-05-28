using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibGGPK.Records;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibGGPK
{
	public sealed class GrindingGearsPackageContainer
	{
		public Dictionary<long, BaseRecord> RecordOffsets { get; private set; }

		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		public GrindingGearsPackageContainer()
		{
			this.RecordOffsets = new Dictionary<long, BaseRecord>(700000);
			this._files = new List<FileRecord>();
			this._directories = new List<DirectoryRecord>();
			this._freeRecords = new List<FreeRecord>();
		}

		public void Read(string pathToGgpk, Action<string> output)
		{
			this._pathToGppk = pathToGgpk;
			if (output != null)
			{
				output(GrindingGearsPackageContainer.getString_0(107320181) + Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320128) + Environment.NewLine);
			}
			this.ReadRecordOffsets(pathToGgpk, output);
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320091) + Environment.NewLine);
			}
			this.CreateDirectoryTree(output);
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320598) + Environment.NewLine);
			}
		}

		public void Read(string pathToGgpk, string pathToBin, Action<string> output)
		{
			this._pathToGppk = pathToGgpk;
			if (output != null)
			{
				output(GrindingGearsPackageContainer.getString_0(107320181) + Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320585) + Environment.NewLine);
			}
			this.DeserializeRecords(pathToBin, output);
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320091) + Environment.NewLine);
			}
			this.CreateDirectoryTree(output);
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320598) + Environment.NewLine);
			}
		}

		public void SerializeRecords(string pathToBin, Action<string> output)
		{
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320516));
			}
			FileStream fileStream = File.Create(pathToBin);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			foreach (KeyValuePair<long, BaseRecord> keyValuePair in this.RecordOffsets)
			{
				binaryWriter.Write(keyValuePair.Key);
				BaseRecord value = keyValuePair.Value;
				if (value is FileRecord)
				{
					binaryWriter.Write(1);
					FileRecord fileRecord = (FileRecord)value;
					binaryWriter.Write(fileRecord.RecordBegin);
					binaryWriter.Write(fileRecord.Length);
					binaryWriter.Write(fileRecord.Hash);
					binaryWriter.Write(fileRecord.Name);
					binaryWriter.Write(fileRecord.DataBegin);
					binaryWriter.Write(fileRecord.DataLength);
				}
				else if (value is GgpkRecord)
				{
					binaryWriter.Write(2);
					GgpkRecord ggpkRecord = (GgpkRecord)value;
					binaryWriter.Write(ggpkRecord.RecordBegin);
					binaryWriter.Write(ggpkRecord.Length);
					binaryWriter.Write(ggpkRecord.RecordOffsets.Length);
					foreach (long value2 in ggpkRecord.RecordOffsets)
					{
						binaryWriter.Write(value2);
					}
				}
				else if (value is FreeRecord)
				{
					binaryWriter.Write(3);
					FreeRecord freeRecord = (FreeRecord)value;
					binaryWriter.Write(freeRecord.RecordBegin);
					binaryWriter.Write(freeRecord.Length);
					binaryWriter.Write(freeRecord.NextFreeOffset);
				}
				else if (value is DirectoryRecord)
				{
					binaryWriter.Write(4);
					DirectoryRecord directoryRecord = (DirectoryRecord)value;
					binaryWriter.Write(directoryRecord.RecordBegin);
					binaryWriter.Write(directoryRecord.Length);
					binaryWriter.Write(directoryRecord.Hash);
					binaryWriter.Write(directoryRecord.Name);
					binaryWriter.Write(directoryRecord.EntriesBegin);
					binaryWriter.Write(directoryRecord.Entries.Count);
					foreach (DirectoryRecord.DirectoryEntry directoryEntry in directoryRecord.Entries)
					{
						binaryWriter.Write(directoryEntry.EntryNameHash);
						binaryWriter.Write(directoryEntry.Offset);
					}
				}
			}
			fileStream.Flush();
			fileStream.Close();
			if (output != null)
			{
				output(GrindingGearsPackageContainer.getString_0(107320523) + Environment.NewLine);
			}
		}

		public void DeserializeRecords(string pathToBin, Action<string> output)
		{
			if (output != null)
			{
				output(Environment.NewLine);
				output(GrindingGearsPackageContainer.getString_0(107320482));
			}
			FileStream fileStream = File.OpenRead(pathToBin);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			while (fileStream.Length - fileStream.Position > 1L)
			{
				long key = binaryReader.ReadInt64();
				switch (binaryReader.ReadByte())
				{
				case 1:
					this.RecordOffsets.Add(key, new FileRecord(binaryReader.ReadInt64(), binaryReader.ReadUInt32(), binaryReader.ReadBytes(32), binaryReader.ReadString(), binaryReader.ReadInt64(), binaryReader.ReadInt64()));
					break;
				case 2:
				{
					long recordBegin = binaryReader.ReadInt64();
					uint length = binaryReader.ReadUInt32();
					long[] array = new long[binaryReader.ReadInt32()];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = binaryReader.ReadInt64();
					}
					this.RecordOffsets.Add(key, new GgpkRecord(recordBegin, length, array));
					break;
				}
				case 3:
				{
					long recordBegin2 = binaryReader.ReadInt64();
					this.RecordOffsets.Add(key, new FreeRecord(binaryReader.ReadUInt32(), recordBegin2, binaryReader.ReadInt64()));
					break;
				}
				case 4:
				{
					long recordBegin3 = binaryReader.ReadInt64();
					uint length2 = binaryReader.ReadUInt32();
					byte[] hash = binaryReader.ReadBytes(32);
					string name = binaryReader.ReadString();
					long entriesBegin = binaryReader.ReadInt64();
					int num = binaryReader.ReadInt32();
					List<DirectoryRecord.DirectoryEntry> list = new List<DirectoryRecord.DirectoryEntry>(num);
					for (int j = 0; j < num; j++)
					{
						list.Add(new DirectoryRecord.DirectoryEntry
						{
							EntryNameHash = binaryReader.ReadUInt32(),
							Offset = binaryReader.ReadInt64()
						});
					}
					this.RecordOffsets.Add(key, new DirectoryRecord(recordBegin3, length2, hash, name, entriesBegin, list));
					break;
				}
				}
			}
			fileStream.Flush();
			fileStream.Close();
			if (output != null)
			{
				output(GrindingGearsPackageContainer.getString_0(107320523) + Environment.NewLine);
			}
		}

		private void ReadRecordOffsets(string pathToGgpk, Action<string> output)
		{
			float num = 0f;
			using (FileStream fileStream = GrindingGearsPackageContainer.OpenFile(pathToGgpk, out this._isReadOnly))
			{
				BinaryReader binaryReader = new BinaryReader(fileStream);
				long length = binaryReader.BaseStream.Length;
				while (binaryReader.BaseStream.Position < length)
				{
					long position = binaryReader.BaseStream.Position;
					BaseRecord value = this.ReadRecord(binaryReader);
					this.RecordOffsets.Add(position, value);
					float num2 = (float)position / (float)length;
					if (num2 - num >= 0.1f)
					{
						if (output != null)
						{
							output(string.Format(GrindingGearsPackageContainer.getString_0(107320489), 100.0 * (double)num2, Environment.NewLine));
						}
						num = num2;
					}
				}
				if (output != null)
				{
					float num3 = 100f * (float)binaryReader.BaseStream.Position / (float)binaryReader.BaseStream.Length;
					output(string.Format(GrindingGearsPackageContainer.getString_0(107320489), num3, Environment.NewLine));
				}
			}
		}

		private static FileStream OpenFile(string path, out bool isReadOnly)
		{
			isReadOnly = true;
			FileStream result;
			try
			{
				FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				isReadOnly = false;
				result = fileStream;
			}
			catch (IOException)
			{
				result = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			}
			return result;
		}

		private BaseRecord ReadRecord(BinaryReader br)
		{
			uint length = br.ReadUInt32();
			string @string = Encoding.ASCII.GetString(br.ReadBytes(4));
			string text = @string;
			string a = text;
			BaseRecord result;
			if (!(a == GrindingGearsPackageContainer.getString_0(107320468)))
			{
				if (!(a == GrindingGearsPackageContainer.getString_0(107320459)))
				{
					if (!(a == GrindingGearsPackageContainer.getString_0(107320418)))
					{
						if (!(a == GrindingGearsPackageContainer.getString_0(107320409)))
						{
							throw new Exception(GrindingGearsPackageContainer.getString_0(107320432) + @string);
						}
						result = new DirectoryRecord(length, br);
					}
					else
					{
						result = new FreeRecord(length, br);
					}
				}
				else
				{
					result = new GgpkRecord(length, br);
				}
			}
			else
			{
				result = new FileRecord(length, br);
			}
			return result;
		}

		private void CreateDirectoryTree(Action<string> output)
		{
			this.DirectoryRoot = this.BuildDirectoryTree();
			output(string.Format(GrindingGearsPackageContainer.getString_0(107320379), this._directories.Count, this._files.Count) + Environment.NewLine);
			this.FreeRoot = this.BuildFreeList();
			output(string.Format(GrindingGearsPackageContainer.getString_0(107320362), this._freeRecords.Count) + Environment.NewLine);
		}

		private DirectoryTreeNode BuildDirectoryTree()
		{
			GgpkRecord ggpkRecord = this.RecordOffsets[0L] as GgpkRecord;
			if (ggpkRecord == null)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352585));
			}
			long num = ggpkRecord.RecordOffsets.Where(new Func<long, bool>(this.RecordOffsets.ContainsKey)).FirstOrDefault((long o) => this.RecordOffsets[o] is DirectoryRecord);
			if (num == 0L)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352512));
			}
			DirectoryRecord directoryRecord = this.RecordOffsets[num] as DirectoryRecord;
			if (directoryRecord == null)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352495));
			}
			DirectoryTreeNode directoryTreeNode = new DirectoryTreeNode
			{
				Children = new List<DirectoryTreeNode>(),
				Files = new List<FileRecord>(),
				Name = GrindingGearsPackageContainer.getString_0(107271933),
				Parent = null,
				Record = directoryRecord
			};
			foreach (DirectoryRecord.DirectoryEntry directoryEntry in directoryRecord.Entries)
			{
				this.BuildDirectoryTree(directoryEntry, directoryTreeNode);
			}
			return directoryTreeNode;
		}

		private void BuildDirectoryTree(DirectoryRecord.DirectoryEntry directoryEntry, DirectoryTreeNode root)
		{
			if (this.RecordOffsets.ContainsKey(directoryEntry.Offset))
			{
				if (this.RecordOffsets[directoryEntry.Offset] is DirectoryRecord)
				{
					DirectoryRecord directoryRecord = this.RecordOffsets[directoryEntry.Offset] as DirectoryRecord;
					this._directories.Add(directoryRecord);
					DirectoryTreeNode directoryTreeNode = new DirectoryTreeNode
					{
						Name = directoryRecord.Name,
						Parent = root,
						Children = new List<DirectoryTreeNode>(),
						Files = new List<FileRecord>(),
						Record = directoryRecord
					};
					root.Children.Add(directoryTreeNode);
					using (List<DirectoryRecord.DirectoryEntry>.Enumerator enumerator = directoryRecord.Entries.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DirectoryRecord.DirectoryEntry directoryEntry2 = enumerator.Current;
							this.BuildDirectoryTree(directoryEntry2, directoryTreeNode);
						}
						return;
					}
				}
				if (this.RecordOffsets[directoryEntry.Offset] is FileRecord)
				{
					FileRecord fileRecord = this.RecordOffsets[directoryEntry.Offset] as FileRecord;
					this._files.Add(fileRecord);
					fileRecord.ContainingDirectory = root;
					root.Files.Add(fileRecord);
				}
			}
		}

		private LinkedList<FreeRecord> BuildFreeList()
		{
			GgpkRecord ggpkRecord = this.RecordOffsets[0L] as GgpkRecord;
			if (ggpkRecord == null)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352585));
			}
			long num = ggpkRecord.RecordOffsets.Where(new Func<long, bool>(this.RecordOffsets.ContainsKey)).FirstOrDefault((long o) => this.RecordOffsets[o] is FreeRecord);
			if (num == 0L)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352414));
			}
			FreeRecord freeRecord = this.RecordOffsets[num] as FreeRecord;
			if (freeRecord == null)
			{
				throw new Exception(GrindingGearsPackageContainer.getString_0(107352393));
			}
			LinkedList<FreeRecord> linkedList = new LinkedList<FreeRecord>();
			for (;;)
			{
				linkedList.AddLast(freeRecord);
				this._freeRecords.Add(freeRecord);
				long nextFreeOffset = freeRecord.NextFreeOffset;
				if (nextFreeOffset == 0L)
				{
					break;
				}
				if (!this.RecordOffsets.ContainsKey(nextFreeOffset))
				{
					goto IL_118;
				}
				freeRecord = (this.RecordOffsets[freeRecord.NextFreeOffset] as FreeRecord);
				if (freeRecord == null)
				{
					goto IL_12D;
				}
			}
			return linkedList;
			IL_118:
			throw new Exception(GrindingGearsPackageContainer.getString_0(107352828));
			IL_12D:
			throw new Exception(GrindingGearsPackageContainer.getString_0(107352783));
		}

		public void Save(string pathToGgpkNew, Action<string> output)
		{
			if (output != null)
			{
				output(GrindingGearsPackageContainer.getString_0(107352682) + Environment.NewLine);
			}
			FileStream fileStream;
			FileStream input = fileStream = File.OpenRead(this._pathToGppk);
			try
			{
				FileStream fileStream2;
				FileStream output2 = fileStream2 = File.Open(pathToGgpkNew, FileMode.Truncate, FileAccess.ReadWrite);
				try
				{
					BinaryReader reader = new BinaryReader(input);
					BinaryWriter writer = new BinaryWriter(output2);
					GgpkRecord ggpkRecord = this.RecordOffsets[0L] as GgpkRecord;
					if (ggpkRecord == null)
					{
						throw new Exception(GrindingGearsPackageContainer.getString_0(107352585));
					}
					writer.Seek((int)ggpkRecord.Length, SeekOrigin.Begin);
					Dictionary<long, long> changedOffsets = new Dictionary<long, long>();
					double previousPercentComplete = 0.0;
					double fileCopied = 0.0;
					DirectoryTreeNode.TraverseTreePostorder(this.DirectoryRoot, delegate(DirectoryTreeNode dir)
					{
						dir.Record.Write(writer, changedOffsets);
					}, delegate(FileRecord file)
					{
						byte[] buffer = file.ReadFileContent(reader);
						file.Write(writer, changedOffsets);
						writer.Write(buffer);
						double fileCopied = fileCopied;
						fileCopied += 1.0;
						double num = fileCopied / (double)this._files.Count;
						if (num - previousPercentComplete >= 0.05000000074505806)
						{
							if (output != null)
							{
								output(string.Format(GrindingGearsPackageContainer.<>c__DisplayClass26_1.getString_0(107352612), 100.0 * num));
							}
							previousPercentComplete = num;
						}
					});
					if (output != null)
					{
						output(GrindingGearsPackageContainer.getString_0(107352661));
					}
					long position = writer.BaseStream.Position;
					this.DirectoryRoot.Record.Write(writer, changedOffsets);
					long position2 = writer.BaseStream.Position;
					FreeRecord freeRecord = new FreeRecord(16U, position2, 0L);
					freeRecord.Write(writer, null);
					writer.Seek(0, SeekOrigin.Begin);
					GgpkRecord ggpkRecord2 = new GgpkRecord(ggpkRecord.Length);
					ggpkRecord2.RecordOffsets[0] = position;
					ggpkRecord2.RecordOffsets[1] = position2;
					ggpkRecord2.Write(writer, changedOffsets);
					if (output != null)
					{
						output(GrindingGearsPackageContainer.getString_0(107352652));
					}
				}
				finally
				{
					if (fileStream2 != null)
					{
						((IDisposable)fileStream2).Dispose();
					}
				}
			}
			finally
			{
				if (fileStream != null)
				{
					((IDisposable)fileStream).Dispose();
				}
			}
		}

		public void DeleteFileRecord(FileRecord file)
		{
			DirectoryTreeNode containingDirectory = file.ContainingDirectory;
			containingDirectory.RemoveFile(file);
		}

		public void DeleteDirectoryRecord(DirectoryTreeNode dir)
		{
			DirectoryTreeNode parent = dir.Parent;
			if (parent != null)
			{
				parent.RemoveDirectory(dir);
			}
		}

		static GrindingGearsPackageContainer()
		{
			Strings.CreateGetStringDelegate(typeof(GrindingGearsPackageContainer));
		}

		public DirectoryTreeNode DirectoryRoot;

		public LinkedList<FreeRecord> FreeRoot;

		private const int EstimatedFileCount = 700000;

		private bool _isReadOnly;

		private string _pathToGppk;

		private readonly List<FileRecord> _files;

		private readonly List<DirectoryRecord> _directories;

		private readonly List<FreeRecord> _freeRecords;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
