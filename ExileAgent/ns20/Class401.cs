using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ns29;

namespace ns20
{
	internal sealed class Class401
	{
		public static string smethod_0(int int_1)
		{
			int_1 ^= 107396847;
			int_1 -= Class401.int_0;
			if (!Class401.bool_0)
			{
				return Class401.smethod_2(int_1);
			}
			return Class401.smethod_1(int_1);
		}

		public static string smethod_1(int int_1)
		{
			object obj = Class401.object_0;
			lock (obj)
			{
				string text;
				Class401.dictionary_0.TryGetValue(int_1, out text);
				if (text != null)
				{
					return text;
				}
			}
			return Class401.smethod_2(int_1);
		}

		public static string smethod_2(int int_1)
		{
			byte[] array = Class401.byte_0;
			int index = int_1 + 1;
			int num = array[int_1];
			int num2;
			if ((num & 128) == 0)
			{
				num2 = num;
				if (num2 == 0)
				{
					return string.Empty;
				}
			}
			else if ((num & 64) == 0)
			{
				num2 = ((num & 63) << 8) + (int)Class401.byte_0[index++];
			}
			else
			{
				num2 = ((num & 31) << 24) + ((int)Class401.byte_0[index++] << 16) + ((int)Class401.byte_0[index++] << 8) + (int)Class401.byte_0[index++];
			}
			string result;
			try
			{
				byte[] array2 = Convert.FromBase64String(Encoding.UTF8.GetString(Class401.byte_0, index, num2));
				string text = string.Intern(Encoding.UTF8.GetString(array2, 0, array2.Length));
				if (Class401.bool_0)
				{
					Class401.smethod_3(int_1, text);
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static void smethod_3(int int_1, string string_2)
		{
			try
			{
				object obj = Class401.object_0;
				lock (obj)
				{
					Class401.dictionary_0.Add(int_1, string_2);
				}
			}
			catch
			{
			}
		}

		static Class401()
		{
			if (Class401.string_0 == "1")
			{
				Class401.bool_0 = true;
				Class401.dictionary_0 = new Dictionary<int, string>();
			}
			Class401.int_0 = Convert.ToInt32(Class401.string_1);
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("{75f0fe8c-cdec-4253-986c-5a5173a18a34}"))
			{
				int num = Convert.ToInt32(manifestResourceStream.Length);
				byte[] buffer = new byte[num];
				manifestResourceStream.Read(buffer, 0, num);
				Class401.byte_0 = Class402.smethod_2(buffer);
			}
		}

		private static readonly string string_0 = "1";

		private static readonly string string_1 = "132";

		private static readonly byte[] byte_0 = null;

		private static readonly Dictionary<int, string> dictionary_0;

		private static readonly object object_0 = new object();

		private static readonly bool bool_0 = false;

		private static readonly int int_0 = 0;
	}
}
