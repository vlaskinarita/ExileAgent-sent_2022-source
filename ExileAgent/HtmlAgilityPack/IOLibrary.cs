using System;
using System.IO;

namespace HtmlAgilityPack
{
	internal struct IOLibrary
	{
		internal static void CopyAlways(string source, string target)
		{
			if (!File.Exists(source))
			{
				return;
			}
			Directory.CreateDirectory(Path.GetDirectoryName(target));
			IOLibrary.MakeWritable(target);
			File.Copy(source, target, true);
		}

		internal static void MakeWritable(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
		}
	}
}
