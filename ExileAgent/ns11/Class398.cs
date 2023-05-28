using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ns29;

namespace ns11
{
	internal sealed class Class398
	{
		[DllImport("kernel32")]
		private static extern bool MoveFileEx(string string_1, string string_2, int int_1);

		internal static bool IsWebApplication
		{
			get
			{
				try
				{
					string a = Process.GetCurrentProcess().MainModule.ModuleName.ToLower();
					if (a == "w3wp.exe")
					{
						return true;
					}
					if (a == "aspnet_wp.exe")
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
		}

		internal static void smethod_0()
		{
			try
			{
				AppDomain.CurrentDomain.AssemblyResolve += Class398.smethod_1;
			}
			catch
			{
			}
		}

		internal unsafe static Assembly smethod_1(object object_0, ResolveEventArgs resolveEventArgs_0)
		{
			void* ptr = stackalloc byte[16];
			Class398.Struct27 @struct = new Class398.Struct27(resolveEventArgs_0.Name);
			string s = @struct.method_0(false);
			string b = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
			string[] array = "U3lzdGVtLlZhbHVlVHVwbGUsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Y2M3YjEzZmZjZDJkZGQ1MQ==,[z]{271d5f2d-aeeb-4aec-8746-b1507bedbba3},U3lzdGVtLlZhbHVlVHVwbGU=,[z]{271d5f2d-aeeb-4aec-8746-b1507bedbba3}".Split(new char[]
			{
				','
			});
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			*(int*)ptr = 0;
			while (*(int*)ptr < array.Length - 1)
			{
				if (array[*(int*)ptr] == b)
				{
					text = array[*(int*)ptr + 1];
					IL_7E:
					if (text.Length == 0 && @struct.string_2.Length == 0)
					{
						b = Convert.ToBase64String(Encoding.UTF8.GetBytes(@struct.string_0));
						*(int*)((byte*)ptr + 4) = 0;
						while (*(int*)((byte*)ptr + 4) < array.Length - 1)
						{
							if (array[*(int*)((byte*)ptr + 4)] == b)
							{
								text = array[*(int*)((byte*)ptr + 4) + 1];
								break;
							}
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 2;
						}
					}
					if (text.Length > 0)
					{
						if (text[0] == '[')
						{
							*(int*)((byte*)ptr + 8) = text.IndexOf(']');
							string text2 = text.Substring(1, *(int*)((byte*)ptr + 8) - 1);
							flag = (text2.IndexOf('z') >= 0);
							flag2 = (text2.IndexOf('t') >= 0);
							text = text.Substring(*(int*)((byte*)ptr + 8) + 1);
						}
						Dictionary<string, Assembly> obj = Class398.dictionary_0;
						Assembly result;
						lock (obj)
						{
							if (Class398.dictionary_0.ContainsKey(text))
							{
								result = Class398.dictionary_0[text];
							}
							else
							{
								Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text);
								if (manifestResourceStream == null)
								{
									goto IL_275;
								}
								*(int*)((byte*)ptr + 12) = (int)manifestResourceStream.Length;
								byte[] array2 = new byte[*(int*)((byte*)ptr + 12)];
								manifestResourceStream.Read(array2, 0, *(int*)((byte*)ptr + 12));
								if (flag)
								{
									array2 = Class402.smethod_2(array2);
								}
								Assembly assembly = null;
								if (!flag2)
								{
									try
									{
										assembly = Assembly.Load(array2);
									}
									catch (FileLoadException)
									{
										flag2 = true;
									}
									catch (BadImageFormatException)
									{
										flag2 = true;
									}
								}
								if (flag2)
								{
									try
									{
										string text3 = string.Format("{0}{1}\\", Path.GetTempPath(), text);
										Directory.CreateDirectory(text3);
										string text4 = text3 + @struct.string_0 + ".dll";
										if (!File.Exists(text4))
										{
											FileStream fileStream = File.OpenWrite(text4);
											fileStream.Write(array2, 0, array2.Length);
											fileStream.Close();
											Class398.MoveFileEx(text4, null, 4);
											Class398.MoveFileEx(text3, null, 4);
										}
										assembly = Assembly.LoadFile(text4);
									}
									catch
									{
									}
								}
								Class398.dictionary_0[text] = assembly;
								result = assembly;
							}
						}
						return result;
					}
					IL_275:
					return null;
				}
				*(int*)ptr = *(int*)ptr + 2;
			}
			goto IL_7E;
		}

		internal const string string_0 = "{71461f04-2faa-4bb9-a0dd-28a79101b599}";

		private const int int_0 = 4;

		private static Dictionary<string, Assembly> dictionary_0 = new Dictionary<string, Assembly>();

		internal struct Struct27
		{
			public string method_0(bool bool_0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.string_0);
				if (bool_0 && this.version_0 != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.version_0);
				}
				stringBuilder.Append(", Culture=");
				stringBuilder.Append((this.string_1.Length == 0) ? "neutral" : this.string_1);
				stringBuilder.Append(", PublicKeyToken=");
				stringBuilder.Append((this.string_2.Length == 0) ? "null" : this.string_2);
				return stringBuilder.ToString();
			}

			public Struct27(string string_3)
			{
				this.version_0 = null;
				this.string_1 = string.Empty;
				this.string_2 = string.Empty;
				this.string_0 = string.Empty;
				string[] array = string_3.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (text.StartsWith("Version="))
					{
						this.version_0 = new Version(text.Substring(8));
					}
					else if (text.StartsWith("Culture="))
					{
						this.string_1 = text.Substring(8);
						if (this.string_1 == "neutral")
						{
							this.string_1 = string.Empty;
						}
					}
					else if (text.StartsWith("PublicKeyToken="))
					{
						this.string_2 = text.Substring(15);
						if (this.string_2 == "null")
						{
							this.string_2 = string.Empty;
						}
					}
					else
					{
						this.string_0 = text;
					}
				}
			}

			public string string_0;

			public Version version_0;

			public string string_1;

			public string string_2;
		}
	}
}
