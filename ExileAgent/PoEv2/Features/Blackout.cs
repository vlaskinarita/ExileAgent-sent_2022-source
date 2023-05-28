using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LibBundle;
using LibBundle.Records;
using LibGGPK;
using LibGGPK.Records;
using Microsoft.VisualBasic.FileIO;
using ns0;
using ns12;
using ns21;
using ns22;
using ns29;
using ns34;
using ns35;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public sealed class Blackout
	{
		public int FileCount
		{
			get
			{
				return this.list_0.Count + this.list_1.Count + this.list_2.Count + this.list_3.Count;
			}
		}

		private bool UsingGGG
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.GameClient) == Blackout.getString_0(107397895);
			}
		}

		public Blackout(MainForm form)
		{
			this.method_0();
			this.method_1();
			this.method_10();
			this.grindingGearsPackageContainer_0 = new GrindingGearsPackageContainer();
			this.mainForm_0 = form;
			this.string_1 = this.method_14();
		}

		private void method_0()
		{
			this.list_0 = new List<string>(Class218.RendererFiles.smethod_18(Environment.NewLine));
			this.list_1 = new List<string>(Class218.EnvironmentFiles.smethod_18(Environment.NewLine));
			this.list_2 = new List<string>(Class218.ParticleFiles.smethod_18(Environment.NewLine));
			this.list_3 = new List<string>(Class218.MaterialFiles.smethod_18(Environment.NewLine));
		}

		private void method_1()
		{
			this.dictionary_0 = new Dictionary<Enum19, Class305>
			{
				{
					Enum19.const_0,
					new Class305(Blackout.getString_0(107271513))
				},
				{
					Enum19.const_1,
					new Class305(Blackout.getString_0(107271464))
				},
				{
					Enum19.const_2,
					new Class305(Blackout.getString_0(107271435))
				},
				{
					Enum19.const_3,
					new Class305(Blackout.getString_0(107271414))
				},
				{
					Enum19.const_4,
					new Class305(Blackout.getString_0(107271389))
				},
				{
					Enum19.const_5,
					new Class305(Blackout.getString_0(107270820))
				},
				{
					Enum19.const_6,
					new Class305(Blackout.getString_0(107270795))
				}
			};
		}

		public unsafe bool method_2()
		{
			void* ptr = stackalloc byte[3];
			Directory.CreateDirectory(Blackout.getString_0(107270766));
			*(byte*)ptr = ((!File.Exists(this.string_1)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, Blackout.getString_0(107270785));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				try
				{
					((byte*)ptr)[2] = (this.UsingGGG ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Action<string> output = new Action<string>(Blackout.<>c.<>9.method_0);
						this.grindingGearsPackageContainer_0.Read(this.string_1, output);
						this.method_8();
					}
					else
					{
						foreach (Class305 @class in this.Bundles)
						{
							File.Copy(@class.SteamFilePath, @class.FilePath);
						}
						this.indexContainer_0 = new IndexContainer(this.string_1);
					}
				}
				catch (Exception ex)
				{
					Class181.smethod_2(Enum11.const_2, Blackout.getString_0(107270704), new object[]
					{
						ex
					});
					((byte*)ptr)[1] = 0;
					goto IL_11A;
				}
				((byte*)ptr)[1] = 1;
			}
			IL_11A:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe void method_3()
		{
			void* ptr = stackalloc byte[10];
			try
			{
				Class181.smethod_3(Enum11.const_0, Blackout.getString_0(107270707));
				BundleRecord[] bundles = this.indexContainer_0.Bundles;
				*(int*)ptr = 0;
				while (*(int*)ptr < bundles.Length)
				{
					BundleRecord bundleRecord = bundles[*(int*)ptr];
					((byte*)ptr)[4] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_1].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						this.method_6(bundleRecord);
						this.method_4(bundleRecord);
					}
					((byte*)ptr)[5] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_6].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						this.method_4(bundleRecord);
					}
					((byte*)ptr)[6] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_2].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						this.method_6(bundleRecord);
						this.method_5(bundleRecord);
					}
					((byte*)ptr)[7] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_3].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						this.method_6(bundleRecord);
					}
					((byte*)ptr)[8] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_4].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						this.method_6(bundleRecord);
						this.method_7(bundleRecord);
					}
					((byte*)ptr)[9] = ((bundleRecord.Name == this.dictionary_0[Enum19.const_5].BundleName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						this.method_6(bundleRecord);
						this.method_7(bundleRecord);
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				this.method_19();
				Class181.smethod_3(Enum11.const_0, Blackout.getString_0(107270638));
				Class181.smethod_3(Enum11.const_0, Blackout.getString_0(107270601));
				this.method_9();
				this.method_18();
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, Blackout.getString_0(107270704), new object[]
				{
					ex
				});
			}
		}

		private unsafe void method_4(BundleRecord bundleRecord_0)
		{
			void* ptr = stackalloc byte[7];
			foreach (LibBundle.Records.FileRecord fileRecord in bundleRecord_0.Files)
			{
				((byte*)ptr)[4] = ((!this.list_0.Any<string>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					break;
				}
				string item = this.indexContainer_0.Hashes[fileRecord.Hash];
				((byte*)ptr)[5] = ((!this.list_0.Contains(item)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					this.list_0.Remove(item);
					this.mainForm_0.Invoke(new Action(this.method_20));
					string @string = Encoding.Unicode.GetString(fileRecord.Read(null));
					string text = Blackout.getString_0(107399247);
					string[] array = @string.smethod_18(Environment.NewLine);
					*(int*)ptr = 0;
					while (*(int*)ptr < array.Length)
					{
						string text2 = array[*(int*)ptr];
						((byte*)ptr)[6] = (string.IsNullOrEmpty(text2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) == 0)
						{
							text = text + Blackout.smethod_0(text2) + Environment.NewLine;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					fileRecord.Write(Encoding.Unicode.GetBytes(text));
				}
			}
		}

		private unsafe void method_5(BundleRecord bundleRecord_0)
		{
			void* ptr = stackalloc byte[2];
			foreach (LibBundle.Records.FileRecord fileRecord in bundleRecord_0.Files)
			{
				*(byte*)ptr = ((!this.list_1.Any<string>()) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					break;
				}
				string item = this.indexContainer_0.Hashes[fileRecord.Hash];
				((byte*)ptr)[1] = ((!this.list_1.Contains(item)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.mainForm_0.Invoke(new Action(this.method_21));
					this.list_1.Remove(item);
					fileRecord.Write(Encoding.Unicode.GetBytes(Class218.EnvironmentReplacement));
				}
			}
		}

		private unsafe void method_6(BundleRecord bundleRecord_0)
		{
			void* ptr = stackalloc byte[2];
			foreach (LibBundle.Records.FileRecord fileRecord in bundleRecord_0.Files)
			{
				*(byte*)ptr = ((!this.list_2.Any<string>()) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					break;
				}
				string item = this.indexContainer_0.Hashes[fileRecord.Hash];
				((byte*)ptr)[1] = ((!this.list_2.Contains(item)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.mainForm_0.Invoke(new Action(this.method_22));
					this.list_2.Remove(item);
					fileRecord.Write(Encoding.Unicode.GetBytes(Class218.ParticleReplacement));
				}
			}
		}

		private unsafe void method_7(BundleRecord bundleRecord_0)
		{
			void* ptr = stackalloc byte[2];
			foreach (LibBundle.Records.FileRecord fileRecord in bundleRecord_0.Files)
			{
				*(byte*)ptr = ((!this.list_3.Any<string>()) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					break;
				}
				string item = this.indexContainer_0.Hashes[fileRecord.Hash];
				((byte*)ptr)[1] = ((!this.list_3.Contains(item)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.mainForm_0.Invoke(new Action(this.method_23));
					this.list_3.Remove(item);
					fileRecord.Write(Encoding.Unicode.GetBytes(Class218.MaterialReplacement));
				}
			}
		}

		private void method_8()
		{
			foreach (Class305 class305_ in this.Bundles)
			{
				this.method_11(class305_);
			}
			this.indexContainer_0 = new IndexContainer(this.IndexPath);
		}

		private void method_9()
		{
			foreach (Class305 @class in this.Bundles)
			{
				if (this.UsingGGG)
				{
					this.method_13(@class);
				}
				else
				{
					File.Delete(@class.SteamFilePath);
					File.Move(@class.FilePath, @class.SteamFilePath);
				}
			}
		}

		public unsafe void method_10()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (Directory.Exists(Blackout.getString_0(107270766)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Directory.Delete(Blackout.getString_0(107270766), true);
			}
			foreach (Class305 @class in this.Bundles)
			{
				((byte*)ptr)[1] = (File.Exists(@class.FilePath) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					File.Delete(@class.FilePath);
				}
			}
			this.indexContainer_0 = null;
			this.grindingGearsPackageContainer_0 = null;
		}

		private unsafe void method_11(Class305 class305_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.grindingGearsPackageContainer_0.DirectoryRoot.Name != Blackout.getString_0(107271080)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				DirectoryTreeNode directoryTreeNode = this.method_12(class305_0.FilePath);
				((byte*)ptr)[1] = ((directoryTreeNode == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					foreach (LibGGPK.Records.FileRecord fileRecord in directoryTreeNode.Files)
					{
						((byte*)ptr)[2] = ((fileRecord.Name == class305_0.BinName) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							FileInfo fileInfo = new FileInfo(class305_0.FilePath);
							fileInfo.Directory.Create();
							((byte*)ptr)[3] = ((!File.Exists(class305_0.FilePath)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								fileRecord.ExtractFile(this.string_1, class305_0.FilePath);
							}
							break;
						}
					}
				}
			}
		}

		private unsafe DirectoryTreeNode method_12(string string_2)
		{
			void* ptr = stackalloc byte[2];
			Blackout.Class357 @class = new Blackout.Class357();
			@class.string_0 = string_2.Split(new char[]
			{
				'\\'
			});
			*(byte*)ptr = ((@class.string_0.Count<string>() == 1) ? 1 : 0);
			DirectoryTreeNode directoryTreeNode;
			if (*(sbyte*)ptr != 0)
			{
				directoryTreeNode = this.grindingGearsPackageContainer_0.DirectoryRoot.Children.FirstOrDefault(new Func<DirectoryTreeNode, bool>(Blackout.<>c.<>9.method_1));
			}
			else
			{
				directoryTreeNode = this.grindingGearsPackageContainer_0.DirectoryRoot.Children.FirstOrDefault(new Func<DirectoryTreeNode, bool>(Blackout.<>c.<>9.method_2));
				((byte*)ptr)[1] = ((directoryTreeNode == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return directoryTreeNode;
				}
				directoryTreeNode = directoryTreeNode.Children.FirstOrDefault(new Func<DirectoryTreeNode, bool>(@class.method_0));
			}
			return directoryTreeNode;
		}

		private static string smethod_0(string string_2)
		{
			Regex regex = new Regex(Blackout.getString_0(107271103));
			return regex.Replace(string_2, Blackout.getString_0(107271050));
		}

		private unsafe void method_13(Class305 class305_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.grindingGearsPackageContainer_0.DirectoryRoot.Name != Blackout.getString_0(107271080)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				DirectoryTreeNode directoryTreeNode = this.method_12(class305_0.FilePath);
				foreach (LibGGPK.Records.FileRecord fileRecord in directoryTreeNode.Files)
				{
					((byte*)ptr)[1] = ((fileRecord.Name == class305_0.BinName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						fileRecord.ReplaceContents(this.string_1, File.ReadAllBytes(class305_0.FilePath), this.grindingGearsPackageContainer_0);
					}
				}
			}
		}

		private string method_14()
		{
			string text = Class120.PoEDirectory;
			if (this.UsingGGG)
			{
				text += Blackout.getString_0(107271073);
			}
			else
			{
				text = text + Blackout.getString_0(107271024) + this.dictionary_0[Enum19.const_0].FilePath;
			}
			return text;
		}

		public unsafe bool method_15()
		{
			void* ptr = stackalloc byte[2];
			try
			{
				*(byte*)ptr = (this.UsingGGG ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					FileSystem.CopyFile(this.string_1, this.string_1 + Blackout.getString_0(107271011), UIOption.AllDialogs);
				}
				else
				{
					foreach (Class305 @class in this.Bundles)
					{
						FileSystem.CopyFile(@class.SteamFilePath, @class.SteamFilePath + Blackout.getString_0(107271011), UIOption.AllDialogs);
					}
				}
			}
			catch (Exception)
			{
				((byte*)ptr)[1] = 0;
				goto IL_9E;
			}
			((byte*)ptr)[1] = 1;
			IL_9E:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe bool method_16()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = (this.UsingGGG ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				string text = this.string_1;
				((byte*)ptr)[1] = ((!File.Exists(text + Blackout.getString_0(107271011))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = 0;
					goto IL_155;
				}
				File.Delete(text);
				File.Move(text + Blackout.getString_0(107271011), text);
			}
			else
			{
				foreach (Class305 @class in this.Bundles)
				{
					((byte*)ptr)[3] = ((!File.Exists(@class.SteamFilePath + Blackout.getString_0(107271011))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						((byte*)ptr)[2] = 0;
						goto IL_155;
					}
				}
				foreach (Class305 class2 in this.Bundles)
				{
					((byte*)ptr)[4] = (File.Exists(class2.SteamFilePath) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						File.Delete(class2.SteamFilePath);
						File.Move(class2.SteamFilePath + Blackout.getString_0(107271011), class2.SteamFilePath);
					}
				}
			}
			((byte*)ptr)[2] = 1;
			IL_155:
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public unsafe void method_17(string string_2)
		{
			void* ptr = stackalloc byte[5];
			Blackout.Class358 @class = new Blackout.Class358();
			@class.blackout_0 = this;
			Class181.smethod_3(Enum11.const_0, Blackout.getString_0(107271030));
			@class.string_0 = Blackout.getString_0(107271005);
			BundleRecord[] bundles = this.indexContainer_0.Bundles;
			@class.list_0 = string_2.Split(new char[]
			{
				'\\'
			}).ToList<string>();
			string value = string.Join(Blackout.getString_0(107394276), @class.list_0.Skip(@class.list_0.IndexOf(Blackout.getString_0(107270956)) + 1)).Replace('\\', '/');
			BundleRecord[] array = bundles;
			*(int*)ptr = 0;
			while (*(int*)ptr < array.Length)
			{
				Blackout.Class359 class2 = new Blackout.Class359();
				class2.class358_0 = @class;
				class2.bundleRecord_0 = array[*(int*)ptr];
				((byte*)ptr)[4] = ((!class2.bundleRecord_0.Name.Contains(value)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) == 0)
				{
					this.mainForm_0.Invoke(new Action(class2.method_0));
					foreach (LibBundle.Records.FileRecord fileRecord in class2.bundleRecord_0.Files)
					{
						Control control = this.mainForm_0;
						Action method;
						if ((method = class2.class358_0.action_0) == null)
						{
							method = (class2.class358_0.action_0 = new Action(class2.class358_0.method_1));
						}
						control.Invoke(method);
						string str = this.indexContainer_0.Hashes[fileRecord.Hash];
						string text = class2.class358_0.string_0 + str;
						new FileInfo(text).Directory.Create();
						fileRecord.bundleRecord.Name = string_2;
						File.WriteAllBytes(text, fileRecord.Read(null));
					}
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			Class181.smethod_3(Enum11.const_0, Blackout.getString_0(107270975));
		}

		private void method_18()
		{
			if (!this.UsingGGG)
			{
				foreach (Class305 @class in this.Bundles)
				{
					Class163.smethod_1(true, Blackout.getString_0(107368720), string.Format(Blackout.getString_0(107270918), @class.SteamFilePath, Class255.class105_0.method_3(ConfigOptions.RunAsUser)));
				}
			}
		}

		private void method_19()
		{
			using (IEnumerator<Class305> enumerator = this.BundlesWithoutIndex.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Blackout.Class360 @class = new Blackout.Class360();
					@class.class305_0 = enumerator.Current;
					this.indexContainer_0.Bundles.First(new Func<BundleRecord, bool>(@class.method_0)).Save(@class.class305_0.FilePath);
				}
			}
			this.indexContainer_0.Save(this.IndexPath);
		}

		private string IndexPath
		{
			get
			{
				return this.dictionary_0[Enum19.const_0].FilePath;
			}
		}

		private List<Class305> Bundles
		{
			get
			{
				return this.dictionary_0.Values.ToList<Class305>();
			}
		}

		private IEnumerable<Class305> BundlesWithoutIndex
		{
			get
			{
				return this.Bundles.Where(new Func<Class305, bool>(this.method_24));
			}
		}

		[CompilerGenerated]
		private void method_20()
		{
			ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
			int value = toolStripProgressBar_.Value;
			toolStripProgressBar_.Value = value + 1;
		}

		[CompilerGenerated]
		private void method_21()
		{
			ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
			int value = toolStripProgressBar_.Value;
			toolStripProgressBar_.Value = value + 1;
		}

		[CompilerGenerated]
		private void method_22()
		{
			ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
			int value = toolStripProgressBar_.Value;
			toolStripProgressBar_.Value = value + 1;
		}

		[CompilerGenerated]
		private void method_23()
		{
			ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
			int value = toolStripProgressBar_.Value;
			toolStripProgressBar_.Value = value + 1;
		}

		[CompilerGenerated]
		private bool method_24(Class305 class305_0)
		{
			return class305_0 != this.dictionary_0[Enum19.const_0];
		}

		static Blackout()
		{
			Strings.CreateGetStringDelegate(typeof(Blackout));
		}

		public const string string_0 = "Metadata";

		private GrindingGearsPackageContainer grindingGearsPackageContainer_0;

		private IndexContainer indexContainer_0;

		private string string_1;

		private MainForm mainForm_0;

		private List<string> list_0;

		private List<string> list_1;

		private List<string> list_2;

		private List<string> list_3;

		private Dictionary<Enum19, Class305> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class357
		{
			internal bool method_0(DirectoryTreeNode directoryTreeNode_0)
			{
				return directoryTreeNode_0.Name == this.string_0[0];
			}

			public string[] string_0;
		}

		[CompilerGenerated]
		private sealed class Class358
		{
			internal string method_0(LibBundle.Records.FileRecord fileRecord_0)
			{
				return this.blackout_0.indexContainer_0.Hashes[fileRecord_0.Hash];
			}

			internal void method_1()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.blackout_0.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			public Blackout blackout_0;

			public string string_0;

			public List<string> list_0;

			public Func<LibBundle.Records.FileRecord, string> func_0;

			public Action action_0;
		}

		[CompilerGenerated]
		private sealed class Class359
		{
			internal void method_0()
			{
				this.class358_0.blackout_0.mainForm_0.toolStripProgressBar_0.Maximum = this.bundleRecord_0.Files.Count;
				this.class358_0.blackout_0.mainForm_0.toolStripProgressBar_0.Value = 0;
				string path = this.class358_0.string_0 + this.class358_0.list_0.Last<string>() + Blackout.Class359.getString_0(107394820);
				string newLine = Environment.NewLine;
				IEnumerable<LibBundle.Records.FileRecord> files = this.bundleRecord_0.Files;
				Func<LibBundle.Records.FileRecord, string> selector;
				if ((selector = this.class358_0.func_0) == null)
				{
					selector = (this.class358_0.func_0 = new Func<LibBundle.Records.FileRecord, string>(this.class358_0.method_0));
				}
				File.WriteAllText(path, string.Join(newLine, files.Select(selector)));
			}

			static Class359()
			{
				Strings.CreateGetStringDelegate(typeof(Blackout.Class359));
			}

			public BundleRecord bundleRecord_0;

			public Blackout.Class358 class358_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class360
		{
			internal bool method_0(BundleRecord bundleRecord_0)
			{
				return bundleRecord_0.Name == this.class305_0.BundleName;
			}

			public Class305 class305_0;
		}
	}
}
