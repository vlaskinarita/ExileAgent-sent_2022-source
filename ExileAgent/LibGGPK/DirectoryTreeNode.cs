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
	public sealed class DirectoryTreeNode : IComparable
	{
		public static void TraverseTreePreorder(DirectoryTreeNode root, Action<DirectoryTreeNode> directoryAction, Action<FileRecord> fileAction)
		{
			foreach (DirectoryTreeNode directoryTreeNode in root.Children)
			{
				if (directoryAction != null)
				{
					directoryAction(directoryTreeNode);
				}
				DirectoryTreeNode.TraverseTreePreorder(directoryTreeNode, directoryAction, fileAction);
			}
			if (fileAction != null)
			{
				foreach (FileRecord obj in root.Files)
				{
					fileAction(obj);
				}
			}
		}

		public static void TraverseTreePostorder(DirectoryTreeNode root, Action<DirectoryTreeNode> directoryAction, Action<FileRecord> fileAction)
		{
			foreach (DirectoryTreeNode directoryTreeNode in root.Children)
			{
				DirectoryTreeNode.TraverseTreePostorder(directoryTreeNode, directoryAction, fileAction);
				if (directoryAction != null)
				{
					directoryAction(directoryTreeNode);
				}
			}
			if (fileAction != null)
			{
				foreach (FileRecord obj in root.Files)
				{
					fileAction(obj);
				}
			}
		}

		public string GetDirectoryPath()
		{
			string directoryPath;
			if (this._directoryPath != null)
			{
				directoryPath = this._directoryPath;
			}
			else
			{
				Stack<string> stack = new Stack<string>();
				StringBuilder stringBuilder = new StringBuilder();
				DirectoryTreeNode directoryTreeNode = this;
				while (directoryTreeNode != null && directoryTreeNode.Name.Length > 0)
				{
					stack.Push(directoryTreeNode.Name);
					directoryTreeNode = directoryTreeNode.Parent;
				}
				foreach (string str in stack)
				{
					stringBuilder.Append(str + Path.DirectorySeparatorChar.ToString());
				}
				this._directoryPath = stringBuilder.ToString();
				directoryPath = this._directoryPath;
			}
			return directoryPath;
		}

		public override string ToString()
		{
			return this.Name;
		}

		public int CompareTo(object obj)
		{
			if (!(obj is DirectoryTreeNode))
			{
				throw new Exception(DirectoryTreeNode.getString_0(107320892));
			}
			return string.Compare(this.Name, (obj as DirectoryTreeNode).Name, StringComparison.Ordinal);
		}

		public void RemoveFile(FileRecord file)
		{
			if (!this.Files.Contains(file))
			{
				throw new Exception(DirectoryTreeNode.getString_0(107320331) + this.Name);
			}
			this.Files.Remove(file);
			DirectoryRecord.DirectoryEntry item = this.Record.Entries.FirstOrDefault((DirectoryRecord.DirectoryEntry n) => n.Offset == file.RecordBegin);
			this.Record.Entries.Remove(item);
		}

		public void RemoveDirectory(DirectoryTreeNode dir)
		{
			if (!this.Children.Contains(dir))
			{
				throw new Exception(DirectoryTreeNode.getString_0(107320254) + this.Name);
			}
			this.Children.Remove(dir);
			DirectoryRecord.DirectoryEntry item = this.Record.Entries.FirstOrDefault((DirectoryRecord.DirectoryEntry n) => n.Offset == dir.Record.RecordBegin);
			this.Record.Entries.Remove(item);
		}

		static DirectoryTreeNode()
		{
			Strings.CreateGetStringDelegate(typeof(DirectoryTreeNode));
		}

		public DirectoryTreeNode Parent;

		public List<DirectoryTreeNode> Children;

		public List<FileRecord> Files;

		public string Name;

		public DirectoryRecord Record;

		private string _directoryPath;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
