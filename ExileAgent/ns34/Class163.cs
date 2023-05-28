using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using ns0;
using ns29;
using ns35;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns34
{
	internal sealed class Class163
	{
		public unsafe static string smethod_0()
		{
			void* ptr = stackalloc byte[2];
			string result;
			if (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.RunAsUser)) || !Class163.smethod_2(Class255.class105_0.method_3(ConfigOptions.RunAsUser)))
			{
				string text = Class163.list_0[Class163.random_0.Next(Class163.list_0.Count)];
				for (;;)
				{
					*(byte*)ptr = (Class163.smethod_2(text) ? 1 : 0);
					if (*(sbyte*)ptr == 0)
					{
						break;
					}
					Class163.list_0.Remove(text);
					text = Class163.list_0[Class163.random_0.Next(Class163.list_0.Count)];
				}
				string text2 = Class163.smethod_4();
				((byte*)ptr)[1] = ((!Class163.smethod_3(text, text2)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = string.Empty;
				}
				else
				{
					Class255.class105_0.method_9(ConfigOptions.RunAsUser, text, true);
					Class255.class105_0.method_9(ConfigOptions.RunAsPassword, text2, true);
					result = text;
				}
			}
			else
			{
				string text = Class255.class105_0.method_3(ConfigOptions.RunAsUser);
				DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
				DirectorySecurity accessControl = directoryInfo.GetAccessControl();
				SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				accessControl.RemoveAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow));
				directoryInfo.SetAccessControl(accessControl);
				result = text;
			}
			return result;
		}

		public static void smethod_1(bool bool_0, string string_0, string string_1)
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.FileName = string_0;
			processStartInfo.Arguments = string_1;
			if (bool_0)
			{
				processStartInfo.Verb = Class163.getString_0(107366863);
			}
			process.StartInfo = processStartInfo;
			process.Start();
		}

		public unsafe static bool smethod_2(string string_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (string.IsNullOrEmpty(string_0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				try
				{
					NTAccount ntaccount = new NTAccount(string_0);
					SecurityIdentifier securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
					((byte*)ptr)[1] = (securityIdentifier.IsAccountSid() ? 1 : 0);
				}
				catch (IdentityNotMappedException)
				{
					((byte*)ptr)[1] = 0;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static bool smethod_3(string string_0, string string_1)
		{
			PrincipalContext principalContext = null;
			UserPrincipal userPrincipal = null;
			GroupPrincipal groupPrincipal = null;
			DirectoryInfo directoryInfo = null;
			DirectorySecurity directorySecurity = null;
			bool result;
			try
			{
				Class181.smethod_3(Enum11.const_0, Class163.getString_0(107366886));
				principalContext = new PrincipalContext(ContextType.Machine);
				userPrincipal = new UserPrincipal(principalContext);
				userPrincipal.SetPassword(string_1);
				userPrincipal.DisplayName = string_0;
				userPrincipal.Name = string_0;
				userPrincipal.Description = Class163.getString_0(107397273);
				userPrincipal.UserCannotChangePassword = true;
				userPrincipal.PasswordNeverExpires = true;
				userPrincipal.Save();
				groupPrincipal = GroupPrincipal.FindByIdentity(principalContext, IdentityType.Sid, Class163.getString_0(107366845));
				groupPrincipal.Members.Add(userPrincipal);
				groupPrincipal.Save();
				directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
				directorySecurity = directoryInfo.GetAccessControl();
				SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				directorySecurity.RemoveAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow));
				directoryInfo.SetAccessControl(directorySecurity);
				Class163.smethod_1(true, Class163.getString_0(107366828), string.Format(Class163.getString_0(107366815), groupPrincipal.Name, string_0));
				Class163.smethod_1(true, Class163.getString_0(107366746), Environment.CurrentDirectory + Class163.getString_0(107366761) + string_0 + Class163.getString_0(107366716));
				Class163.smethod_1(true, Class163.getString_0(107366723), Class163.getString_0(107366674) + Environment.GetEnvironmentVariable(Class163.getString_0(107366669)) + Class163.getString_0(107366692));
				Class163.smethod_1(true, Class163.getString_0(107366746), Environment.GetEnvironmentVariable(Class163.getString_0(107366669)) + Class163.getString_0(107464447) + string_0 + Class163.getString_0(107464374));
				result = true;
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_3, Class163.getString_0(107464365), new object[]
				{
					principalContext == null,
					userPrincipal == null,
					groupPrincipal == null,
					directoryInfo == null,
					directorySecurity == null
				});
				Class181.smethod_3(Enum11.const_2, ex.ToString());
				result = false;
			}
			return result;
		}

		private unsafe static string smethod_4()
		{
			void* ptr = stackalloc byte[6];
			string text = Class163.getString_0(107464308);
			Random random = new Random();
			string text2 = string.Empty;
			for (;;)
			{
				((byte*)ptr)[5] = ((!Class163.smethod_5(text2)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					break;
				}
				text2 = string.Empty;
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[4] = ((*(int*)ptr < 20) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) == 0)
					{
						break;
					}
					text2 += text[random.Next(text.Length)].ToString();
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
			return text2;
		}

		private unsafe static bool smethod_5(string string_0)
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = (Regex.IsMatch(string_0, Class163.getString_0(107464215)) ? 1 : 0);
			((byte*)ptr)[1] = (Regex.IsMatch(string_0, Class163.getString_0(107464206)) ? 1 : 0);
			((byte*)ptr)[2] = (Regex.IsMatch(string_0, Class163.getString_0(107464229)) ? 1 : 0);
			((byte*)ptr)[3] = (Regex.IsMatch(string_0, Class163.getString_0(107464700)) ? 1 : 0);
			((byte*)ptr)[4] = (byte)(*(sbyte*)ptr & *(sbyte*)((byte*)ptr + 1) & *(sbyte*)((byte*)ptr + 2) & *(sbyte*)((byte*)ptr + 3));
			return *(sbyte*)((byte*)ptr + 4) != 0;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class163()
		{
			Strings.CreateGetStringDelegate(typeof(Class163));
			Class163.list_0 = new List<string>
			{
				Class163.getString_0(107464715),
				Class163.getString_0(107464706),
				Class163.getString_0(107464665),
				Class163.getString_0(107464656),
				Class163.getString_0(107464675),
				Class163.getString_0(107464630),
				Class163.getString_0(107464621),
				Class163.getString_0(107464640),
				Class163.getString_0(107464595),
				Class163.getString_0(107464618),
				Class163.getString_0(107464605),
				Class163.getString_0(107464560),
				Class163.getString_0(107464583),
				Class163.getString_0(107464574),
				Class163.getString_0(107464533),
				Class163.getString_0(107464556),
				Class163.getString_0(107464547),
				Class163.getString_0(107464506),
				Class163.getString_0(107464497),
				Class163.getString_0(107464524),
				Class163.getString_0(107464515)
			};
			Class163.random_0 = new Random();
		}

		private static List<string> list_0;

		private static Random random_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
