using System;
using System.IO;
using System.Runtime.InteropServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace LibBundle
{
	public class BundleContainer
	{
		[DllImport("oo2core_8_win64.dll")]
		public static extern int OodleLZ_Decompress(byte[] buffer, int bufferSize, byte[] result, long outputBufferSize, int a, int b, int c, IntPtr d, long e, IntPtr f, IntPtr g, IntPtr h, long i, int ThreadModule);

		[DllImport("oo2core_8_win64.dll")]
		public static extern int OodleLZ_Compress(BundleContainer.ENCODE_TYPES format, byte[] buffer, long bufferSize, byte[] outputBuffer, BundleContainer.COMPRESSTION_LEVEL level, IntPtr opts, long offs, long unused, IntPtr scratch, long scratch_size);

		public BundleContainer(string path)
		{
			this.path = path;
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(path));
			this.Initialize(binaryReader);
			binaryReader.Close();
		}

		public BundleContainer(BinaryReader br)
		{
			this.Initialize(br);
		}

		public BundleContainer(byte[] data)
		{
			this.offset = 0L;
			this.size_decompressed = (long)(this.uncompressed_size = data.Length);
			this.encoder = BundleContainer.ENCODE_TYPES.LEVIATHAN;
			this.entry_count = ((data.Length % 262144 == 0) ? (data.Length / 262144) : (data.Length / 262144 + 1));
			this.head_size = this.entry_count * 4 + 48;
			this.chunk_size = 262144;
			this.unknown = 1;
			this.unknown6 = 0;
			this.unknown5 = 0;
			this.unknown4 = 0;
			this.unknown3 = 0;
			this.dataToSave = data;
		}

		private void Initialize(BinaryReader br)
		{
			this.offset = br.BaseStream.Position;
			this.uncompressed_size = br.ReadInt32();
			this.data_size = br.ReadInt32();
			this.head_size = br.ReadInt32();
			this.encoder = (BundleContainer.ENCODE_TYPES)br.ReadInt32();
			this.unknown = br.ReadInt32();
			this.size_decompressed = (long)((int)br.ReadInt64());
			this.size_compressed = br.ReadInt64();
			this.entry_count = br.ReadInt32();
			this.chunk_size = br.ReadInt32();
			this.unknown3 = br.ReadInt32();
			this.unknown4 = br.ReadInt32();
			this.unknown5 = br.ReadInt32();
			this.unknown6 = br.ReadInt32();
		}

		public MemoryStream Read(BinaryReader br = null)
		{
			if (br == null)
			{
				if (this.path == null)
				{
					throw new ArgumentException(BundleContainer.getString_0(107320645), BundleContainer.getString_0(107242730));
				}
				br = new BinaryReader(File.OpenRead(this.path));
			}
			br.BaseStream.Seek(this.offset + 60L, SeekOrigin.Begin);
			int[] array = new int[this.entry_count];
			for (int i = 0; i < this.entry_count; i++)
			{
				array[i] = br.ReadInt32();
			}
			MemoryStream memoryStream = new MemoryStream(this.uncompressed_size);
			for (int j = 0; j < this.entry_count; j++)
			{
				byte[] array2 = br.ReadBytes(array[j]);
				int num = (j + 1 == this.entry_count) ? (this.uncompressed_size - this.chunk_size * (this.entry_count - 1)) : this.chunk_size;
				byte[] array3 = new byte[num + 64];
				BundleContainer.OodleLZ_Decompress(array2, array2.Length, array3, (long)num, 0, 0, 0, IntPtr.Zero, 0L, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0L, 3);
				memoryStream.Write(array3, 0, num);
			}
			br.Close();
			return memoryStream;
		}

		public virtual void Save(string path)
		{
			if (this.dataToSave == null)
			{
				throw new NotSupportedException(BundleContainer.getString_0(107320955));
			}
			this.path = path;
			this.size_decompressed = (long)(this.uncompressed_size = this.dataToSave.Length);
			this.entry_count = this.uncompressed_size / this.chunk_size;
			if (this.uncompressed_size % this.chunk_size != 0)
			{
				this.entry_count++;
			}
			this.head_size = this.entry_count * 4 + 48;
			BinaryWriter binaryWriter = new BinaryWriter(File.Create(path));
			binaryWriter.BaseStream.Seek(this.offset + 60L + (long)(this.entry_count * 4), SeekOrigin.Begin);
			this.data_size = 0;
			MemoryStream memoryStream = new MemoryStream(this.dataToSave);
			int[] array = new int[this.entry_count];
			for (int i = 0; i < this.entry_count - 1; i++)
			{
				byte[] array2 = new byte[this.chunk_size];
				memoryStream.Read(array2, 0, this.chunk_size);
				byte[] array3 = new byte[array2.Length + 548];
				int num = BundleContainer.OodleLZ_Compress(BundleContainer.ENCODE_TYPES.LEVIATHAN, array2, (long)array2.Length, array3, BundleContainer.COMPRESSTION_LEVEL.Normal, IntPtr.Zero, 0L, 0L, IntPtr.Zero, 0L);
				this.data_size += (array[i] = num);
				binaryWriter.Write(array3, 0, num);
			}
			byte[] array4 = new byte[this.dataToSave.Length - (this.entry_count - 1) * this.chunk_size];
			memoryStream.Read(array4, 0, array4.Length);
			byte[] array5 = new byte[array4.Length + 548];
			int num2 = BundleContainer.OodleLZ_Compress(BundleContainer.ENCODE_TYPES.LEVIATHAN, array4, (long)array4.Length, array5, BundleContainer.COMPRESSTION_LEVEL.Normal, IntPtr.Zero, 0L, 0L, IntPtr.Zero, 0L);
			this.data_size += (array[this.entry_count - 1] = num2);
			binaryWriter.Write(array5, 0, num2);
			binaryWriter.BaseStream.Seek(this.offset + 60L, SeekOrigin.Begin);
			for (int j = 0; j < this.entry_count; j++)
			{
				binaryWriter.Write(array[j]);
			}
			this.size_compressed = (long)this.data_size;
			binaryWriter.BaseStream.Seek(this.offset, SeekOrigin.Begin);
			binaryWriter.Write(this.uncompressed_size);
			binaryWriter.Write(this.data_size);
			binaryWriter.Write(this.head_size);
			binaryWriter.Write((uint)this.encoder);
			binaryWriter.Write(this.unknown);
			binaryWriter.Write(this.size_decompressed);
			binaryWriter.Write(this.size_compressed);
			binaryWriter.Write(this.entry_count);
			binaryWriter.Write(this.chunk_size);
			binaryWriter.Write(this.unknown3);
			binaryWriter.Write(this.unknown4);
			binaryWriter.Write(this.unknown5);
			binaryWriter.Write(this.unknown6);
			binaryWriter.Flush();
			binaryWriter.Close();
			memoryStream.Close();
		}

		static BundleContainer()
		{
			Strings.CreateGetStringDelegate(typeof(BundleContainer));
		}

		public string path;

		public long offset = 0L;

		public int uncompressed_size;

		public int data_size;

		public int head_size;

		public BundleContainer.ENCODE_TYPES encoder;

		public int unknown;

		public long size_decompressed;

		public long size_compressed;

		public int entry_count;

		public int chunk_size;

		public int unknown3;

		public int unknown4;

		public int unknown5;

		public int unknown6;

		internal byte[] dataToSave;

		[NonSerialized]
		internal static GetString getString_0;

		public enum ENCODE_TYPES : uint
		{
			LZH,
			LZHLW,
			LZNIB,
			NONE,
			LZB16,
			LZBLW,
			LZA,
			LZNA,
			KRAKEN,
			MERMAID,
			BITKNIT,
			SELKIE,
			HYDRA,
			LEVIATHAN
		}

		public enum COMPRESSTION_LEVEL : uint
		{
			None,
			SuperFast,
			VeryFast,
			Fast,
			Normal,
			Optimal1,
			Optimal2,
			Optimal3,
			Optimal4,
			Optimal5
		}
	}
}
