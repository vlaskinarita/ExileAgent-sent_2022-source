using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibGGPK.Records
{
	public sealed class FileRecord : BaseRecord, IComparable
	{
		public FileRecord(long recordBegin, uint length, byte[] hash, string name, long dataBegin, long dataLength)
		{
			base.RecordBegin = recordBegin;
			base.Length = length;
			this.Hash = hash;
			this.Name = name;
			this.DataBegin = dataBegin;
			this.DataLength = dataLength;
		}

		public FileRecord(uint length, BinaryReader br)
		{
			base.RecordBegin = br.BaseStream.Position - 8L;
			base.Length = length;
			this.Read(br);
		}

		public override void Read(BinaryReader br)
		{
			int num = br.ReadInt32();
			this.Hash = br.ReadBytes(32);
			this.Name = Encoding.Unicode.GetString(br.ReadBytes(2 * (num - 1)));
			br.BaseStream.Seek(2L, SeekOrigin.Current);
			this.DataBegin = br.BaseStream.Position;
			this.DataLength = (long)((ulong)base.Length - (ulong)((long)(8 + num * 2 + 32 + 4)));
			br.BaseStream.Seek(this.DataLength, SeekOrigin.Current);
		}

		public override void Write(BinaryWriter bw, Dictionary<long, long> changedOffsets)
		{
			long position = bw.BaseStream.Position;
			if (position != base.RecordBegin)
			{
				changedOffsets[base.RecordBegin] = position;
			}
			bw.Write(base.Length);
			bw.Write(Encoding.ASCII.GetBytes(FileRecord.getString_0(107320498)));
			bw.Write(this.Name.Length + 1);
			bw.Write(this.Hash);
			bw.Write(Encoding.Unicode.GetBytes(this.Name));
			bw.Write(0);
		}

		public string ExtractTempFile(string ggpkPath)
		{
			string tempFileName = Path.GetTempFileName();
			string text = tempFileName + Path.GetExtension(this.Name);
			File.Move(tempFileName, text);
			this.ExtractFile(ggpkPath, text);
			return text;
		}

		public void ExtractFile(string ggpkPath, string outputPath)
		{
			byte[] bytes = this.ReadFileContent(ggpkPath);
			File.WriteAllBytes(outputPath, bytes);
		}

		public void ExtractFileWithDirectoryStructure(string ggpkPath, string outputDirectory)
		{
			byte[] bytes = this.ReadFileContent(ggpkPath);
			string text = outputDirectory + Path.DirectorySeparatorChar.ToString() + this.GetDirectoryPath();
			Directory.CreateDirectory(text);
			File.WriteAllBytes(text + Path.DirectorySeparatorChar.ToString() + this.Name, bytes);
		}

		public byte[] ReadFileContent(string ggpkPath)
		{
			byte[] array = new byte[this.DataLength];
			FileStream fileStream = File.Open(ggpkPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			fileStream.Seek(this.DataBegin, SeekOrigin.Begin);
			fileStream.Read(array, 0, array.Length);
			return array;
		}

		public byte[] ReadFileContent(BinaryReader fs)
		{
			byte[] array = new byte[this.DataLength];
			fs.BaseStream.Seek(this.DataBegin, SeekOrigin.Begin);
			fs.Read(array, 0, array.Length);
			return array;
		}

		public FileRecord.DataFormat FileFormat
		{
			get
			{
				FileRecord.DataFormat result;
				if (this.Name.Equals(FileRecord.getString_0(107352111)))
				{
					result = FileRecord.DataFormat.Unicode;
				}
				else
				{
					result = (FileRecord.KnownFileFormats.ContainsKey(Path.GetExtension(this.Name).ToLower()) ? FileRecord.KnownFileFormats[Path.GetExtension(this.Name).ToLower()] : FileRecord.DataFormat.Unknown);
				}
				return result;
			}
		}

		public string GetDirectoryPath()
		{
			return this.ContainingDirectory.GetDirectoryPath();
		}

		public uint GetNameHash()
		{
			return Murmur.Hash2(this.Name, 0U);
		}

		private void MarkAsFree(FileStream ggpkFileStream, LinkedList<FreeRecord> freeRecordRoot)
		{
			byte[] bytes = BitConverter.GetBytes(0L);
			byte[] bytes2 = Encoding.ASCII.GetBytes(FileRecord.getString_0(107320448));
			ggpkFileStream.Seek(base.RecordBegin + 4L, SeekOrigin.Begin);
			ggpkFileStream.Write(bytes2, 0, 4);
			ggpkFileStream.Write(bytes, 0, bytes.Length);
			ggpkFileStream.Seek(freeRecordRoot.Last.Value.RecordBegin + 8L, SeekOrigin.Begin);
			ggpkFileStream.Write(BitConverter.GetBytes(base.RecordBegin), 0, 8);
			FreeRecord value = new FreeRecord(base.Length, base.RecordBegin, 0L);
			freeRecordRoot.AddLast(value);
			if (this.ggpc != null)
			{
				this.ggpc.RecordOffsets[base.RecordBegin] = value;
			}
		}

		public void ReplaceContents(string ggpkPath, byte[] replacmentData, GrindingGearsPackageContainer ggpc)
		{
			this.ggpc = ggpc;
			this.ReplaceContents(ggpkPath, replacmentData, ggpc.FreeRoot);
		}

		public void ReplaceContents(string ggpkPath, byte[] replacmentData, LinkedList<FreeRecord> freeRecordRoot)
		{
			FileStream fileStream = File.Open(ggpkPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
			if ((long)replacmentData.Length != this.DataLength)
			{
				this.MarkAsFree(fileStream, freeRecordRoot);
				fileStream.Seek(0L, SeekOrigin.End);
				base.RecordBegin = fileStream.Position;
				base.Length = (uint)((ulong)base.Length - (ulong)this.DataLength + (ulong)((long)replacmentData.Length));
				if (this.ggpc != null)
				{
					this.ggpc.RecordOffsets[base.RecordBegin] = this;
				}
				this.ContainingDirectory.Record.UpdateOffset(ggpkPath, this.GetNameHash(), base.RecordBegin);
			}
			else
			{
				fileStream.Seek(base.RecordBegin, SeekOrigin.Begin);
			}
			SHA256 sha = SHA256.Create();
			this.Hash = sha.ComputeHash(replacmentData);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(base.Length);
			binaryWriter.Write(FileRecord.getString_0(107320498).ToCharArray(0, 4));
			binaryWriter.Write(this.Name.Length + 1);
			binaryWriter.Write(this.Hash);
			binaryWriter.Write(Encoding.Unicode.GetBytes(this.Name + FileRecord.getString_0(107222498)));
			this.DataBegin = binaryWriter.BaseStream.Position;
			this.DataLength = (long)replacmentData.Length;
			binaryWriter.Write(replacmentData);
			binaryWriter.Flush();
		}

		public void ReplaceContents(string ggpkPath, string replacmentPath, LinkedList<FreeRecord> freeRecordRoot)
		{
			this.ReplaceContents(ggpkPath, File.ReadAllBytes(replacmentPath), freeRecordRoot);
		}

		public override string ToString()
		{
			return this.Name;
		}

		public int CompareTo(object obj)
		{
			if (!(obj is FileRecord))
			{
				throw new NotImplementedException(FileRecord.getString_0(107352054));
			}
			return string.Compare(this.Name, (obj as FileRecord).Name, StringComparison.Ordinal);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static FileRecord()
		{
			Strings.CreateGetStringDelegate(typeof(FileRecord));
			FileRecord.KnownFileFormats = new Dictionary<string, FileRecord.DataFormat>
			{
				{
					FileRecord.getString_0(107400130),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352045),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352004),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351995),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352018),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352013),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351972),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351963),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351986),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351977),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351936),
					FileRecord.DataFormat.Sound
				},
				{
					FileRecord.getString_0(107351927),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351950),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107335009),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107351909),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351900),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107334550),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107334542),
					FileRecord.DataFormat.Dat
				},
				{
					FileRecord.getString_0(107351923),
					FileRecord.DataFormat.Dat
				},
				{
					FileRecord.getString_0(107351914),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351873),
					FileRecord.DataFormat.TextureDds
				},
				{
					FileRecord.getString_0(107351864),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351887),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351878),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352349),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352372),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352363),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352322),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352317),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107352336),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352327),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352286),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107352281),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352304),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352299),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352294),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107332452),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352253),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107332231),
					FileRecord.DataFormat.Image
				},
				{
					FileRecord.getString_0(107352276),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352267),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352262),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107352221),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352244),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107329723),
					FileRecord.DataFormat.Sound
				},
				{
					FileRecord.getString_0(107352235),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352230),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352189),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352212),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107230432),
					FileRecord.DataFormat.Image
				},
				{
					FileRecord.getString_0(107352203),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107352154),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352177),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107352168),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107326556),
					FileRecord.DataFormat.RichText
				},
				{
					FileRecord.getString_0(107352131),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107352122),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107325530),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107352149),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351588),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351579),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351602),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351593),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351552),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351543),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351566),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107324521),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107351525),
					FileRecord.DataFormat.Ascii
				},
				{
					FileRecord.getString_0(107395689),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107351516),
					FileRecord.DataFormat.Unicode
				},
				{
					FileRecord.getString_0(107321746),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107322093),
					FileRecord.DataFormat.Unknown
				},
				{
					FileRecord.getString_0(107321253),
					FileRecord.DataFormat.Unicode
				}
			};
		}

		private static readonly Dictionary<string, FileRecord.DataFormat> KnownFileFormats;

		public const string Tag = "FILE";

		public byte[] Hash;

		public string Name;

		public long DataBegin;

		public long DataLength;

		public DirectoryTreeNode ContainingDirectory;

		private GrindingGearsPackageContainer ggpc = null;

		[NonSerialized]
		internal static GetString getString_0;

		public enum DataFormat
		{
			Unknown,
			Image,
			Ascii,
			Unicode,
			RichText,
			Sound,
			Dat,
			TextureDds
		}
	}
}
