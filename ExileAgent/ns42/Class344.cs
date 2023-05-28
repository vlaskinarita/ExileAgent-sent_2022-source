using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.Win32;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns42
{
	internal static class Class344
	{
		public static List<string> smethod_0()
		{
			List<string> list = new List<string>();
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(Class344.getString_0(107272718));
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				PropertyData propertyData = managementBaseObject.Properties[Class344.getString_0(107272701)];
				PropertyData propertyData2 = managementBaseObject.Properties[Class344.getString_0(107408241)];
				if (propertyData != null && propertyData2 != null && propertyData.Value != null)
				{
					list.Add(propertyData2.Value.ToString());
				}
			}
			return list;
		}

		public static void smethod_1(string string_0, string string_1)
		{
			try
			{
				if (string_0.smethod_25())
				{
					Class181.smethod_3(Enum11.const_2, Class344.getString_0(107272672));
				}
				else
				{
					List<Class344.Class345> list = new List<Class344.Class345>();
					Class181.smethod_3(Enum11.const_0, Class344.getString_0(107272635));
					foreach (string name in Class344.list_0)
					{
						RegistryKey registryKey_ = Registry.LocalMachine.OpenSubKey(name);
						list.AddRange(Class344.smethod_2(registryKey_, list, string_0));
					}
					Class181.smethod_3(Enum11.const_0, Class344.getString_0(107272602));
					foreach (Class344.Class345 @class in list)
					{
						string text = @class.Value.Replace(string_0, string_1);
						Class181.smethod_2(Enum11.const_3, Class344.getString_0(107273041), new object[]
						{
							@class.KeyName,
							@class.Value,
							@class.Value,
							text
						});
						Registry.SetValue(@class.KeyName, @class.ValueName, text);
					}
				}
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, Class344.getString_0(107272984), new object[]
				{
					ex
				});
			}
		}

		private unsafe static List<Class344.Class345> smethod_2(RegistryKey registryKey_0, List<Class344.Class345> list_1, string string_0)
		{
			void* ptr = stackalloc byte[11];
			((byte*)ptr)[8] = ((registryKey_0 == null) ? 1 : 0);
			List<Class344.Class345> result;
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				result = list_1;
			}
			else
			{
				string[] subKeyNames = registryKey_0.GetSubKeyNames();
				string[] array = subKeyNames;
				*(int*)ptr = 0;
				while (*(int*)ptr < array.Length)
				{
					string name = array[*(int*)ptr];
					try
					{
						using (RegistryKey registryKey = registryKey_0.OpenSubKey(name))
						{
							((byte*)ptr)[9] = ((registryKey == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								return list_1;
							}
							string[] valueNames = registryKey.GetValueNames();
							*(int*)((byte*)ptr + 4) = 0;
							while (*(int*)((byte*)ptr + 4) < valueNames.Length)
							{
								string text = valueNames[*(int*)((byte*)ptr + 4)];
								string text2 = registryKey.GetValue(text).smethod_10();
								((byte*)ptr)[10] = (text2.Contains(string_0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									list_1.Add(new Class344.Class345(registryKey.Name, text, text2));
								}
								*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
							}
							Class344.smethod_2(registryKey, list_1, string_0);
						}
					}
					catch (SecurityException)
					{
					}
					catch (Exception ex)
					{
						Class181.smethod_2(Enum11.const_2, Class344.getString_0(107272955), new object[]
						{
							ex
						});
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				result = list_1;
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class344()
		{
			Strings.CreateGetStringDelegate(typeof(Class344));
			Class344.list_0 = new List<string>
			{
				Class344.getString_0(107272882),
				Class344.getString_0(107272861),
				Class344.getString_0(107272272),
				Class344.getString_0(107272251),
				Class344.getString_0(107272174)
			};
		}

		private static List<string> list_0;

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class Class345
		{
			public string KeyName { get; set; }

			public string ValueName { get; set; }

			public string Value { get; set; }

			public Class345(string string_3, string string_4, string string_5)
			{
				this.KeyName = string_3;
				this.ValueName = string_4;
				this.Value = string_5;
			}

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_2;
		}
	}
}
