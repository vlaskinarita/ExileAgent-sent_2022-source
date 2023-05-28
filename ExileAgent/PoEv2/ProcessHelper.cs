using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using ns0;
using ns12;
using ns29;
using ns35;
using ns36;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public static class ProcessHelper
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_1, string string_2);

		public static void smethod_0(MainForm mainForm_1)
		{
			ProcessHelper.mainForm_0 = mainForm_1;
		}

		public unsafe static bool smethod_1()
		{
			void* ptr = stackalloc byte[6];
			string[] array = ProcessHelper.string_0;
			*(int*)ptr = 0;
			while (*(int*)ptr < array.Length)
			{
				string text = array[*(int*)ptr];
				ProcessHelper.smethod_3(text);
				((byte*)ptr)[4] = ((!ProcessHelper.smethod_2(text)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Class181.smethod_2(Enum11.const_2, ProcessHelper.getString_0(107378119), new object[]
					{
						text
					});
					((byte*)ptr)[5] = 0;
					IL_66:
					return *(sbyte*)((byte*)ptr + 5) != 0;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			((byte*)ptr)[5] = 1;
			goto IL_66;
		}

		private unsafe static bool smethod_2(string string_1)
		{
			void* ptr = stackalloc byte[2];
			ServiceController serviceController = new ServiceController(string_1);
			try
			{
				TimeSpan timeSpan = TimeSpan.FromMilliseconds(5000.0);
				*(byte*)ptr = ((serviceController.Status != 4) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					ServiceControllerStatus status = serviceController.Status;
					ServiceControllerStatus serviceControllerStatus = status;
					if (serviceControllerStatus != 1)
					{
						if (serviceControllerStatus != 3)
						{
							if (serviceControllerStatus == 7)
							{
								serviceController.Continue();
								serviceController.WaitForStatus(4, timeSpan);
							}
						}
						else
						{
							serviceController.Start();
							serviceController.WaitForStatus(4, timeSpan);
						}
					}
					else
					{
						serviceController.Start();
						serviceController.WaitForStatus(4, timeSpan);
					}
					Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107378110), string_1));
					((byte*)ptr)[1] = 1;
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107378049), string_1));
					((byte*)ptr)[1] = 1;
				}
			}
			catch (Exception arg)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107378044), string_1, arg));
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private static void smethod_3(string string_1)
		{
			try
			{
				using (ManagementObject managementObject = new ManagementObject(string.Format(ProcessHelper.getString_0(107378003), string_1)))
				{
					managementObject.InvokeMethod(ProcessHelper.getString_0(107377970), new object[]
					{
						ProcessHelper.getString_0(107377949)
					});
				}
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_3, ProcessHelper.getString_0(107378416), new object[]
				{
					string_1,
					ex
				});
			}
		}

		public unsafe static void smethod_4()
		{
			void* ptr = stackalloc byte[12];
			try
			{
				ProcessHelper.mainForm_0.thread_3 = Thread.CurrentThread;
				((byte*)ptr)[4] = (Class255.class105_0.method_3(ConfigOptions.GamePath).smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Class181.smethod_3(Enum11.const_2, ProcessHelper.getString_0(107378375));
					ProcessHelper.mainForm_0.method_64(true);
				}
				else if (ProcessHelper.smethod_8() && !ProcessHelper.smethod_7(Class255.GameClient.BaseProcessId))
				{
					Class181.smethod_3(Enum11.const_2, Class255.GameClient.WrongBaseUserMessage);
					ProcessHelper.mainForm_0.method_64(true);
				}
				else if (UI.smethod_26() && !ProcessHelper.smethod_7(ProcessHelper.int_0))
				{
					Class181.smethod_3(Enum11.const_2, Class255.GameClient.WrongGameUserMessage);
					ProcessHelper.mainForm_0.method_64(true);
				}
				else
				{
					((byte*)ptr)[5] = (UI.smethod_26() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						Class181.smethod_3(Enum11.const_0, ProcessHelper.getString_0(107378334));
						ProcessHelper.mainForm_0.method_64(true);
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, ProcessHelper.getString_0(107378277));
						ProcessHelper.smethod_9();
						((byte*)ptr)[6] = ((UI.intptr_0 != IntPtr.Zero) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) == 0)
						{
							try
							{
								using (new StreamWriter(Class120.PoELogFile))
								{
								}
							}
							catch
							{
							}
							((byte*)ptr)[7] = ((!ProcessHelper.mainForm_0.bool_11) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								Class181.smethod_3(Enum11.const_2, ProcessHelper.getString_0(107378244));
							}
							else
							{
								try
								{
									Class181.smethod_3(Enum11.const_3, ProcessHelper.getString_0(107378239));
									((byte*)ptr)[8] = ((Class255.class105_0.method_3(ConfigOptions.GameClient) == ProcessHelper.getString_0(107395301)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) != 0)
									{
										Process[] processesByName = Process.GetProcessesByName(Class255.GameClient.BaseProcessName);
										*(int*)ptr = 0;
										while (*(int*)ptr < processesByName.Length)
										{
											Process process = processesByName[*(int*)ptr];
											process.Kill();
											*(int*)ptr = *(int*)ptr + 1;
										}
										ProcessHelper.smethod_15();
									}
									Process process2 = new Process();
									process2.StartInfo.CreateNoWindow = true;
									process2.StartInfo.UseShellExecute = false;
									process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
									process2.StartInfo.LoadUserProfile = true;
									process2.StartInfo.WorkingDirectory = Class120.PoEDirectory;
									((byte*)ptr)[9] = (Class255.class105_0.method_4(ConfigOptions.LimitedUser) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 9) != 0)
									{
										process2.StartInfo.UserName = Class255.class105_0.method_3(ConfigOptions.RunAsUser);
										process2.StartInfo.Password = new NetworkCredential(ProcessHelper.getString_0(107397005), Class255.class105_0.method_3(ConfigOptions.RunAsPassword)).SecurePassword;
									}
									process2.StartInfo.FileName = Class255.GameClient.BaseExePath + Class255.GameClient.ExeName;
									((byte*)ptr)[10] = ((!Class255.GameClient.BaseArguments.smethod_25()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 10) != 0)
									{
										process2.StartInfo.Arguments = Class255.GameClient.BaseArguments + ProcessHelper.getString_0(107397042);
									}
									ProcessStartInfo startInfo = process2.StartInfo;
									startInfo.Arguments += Class255.class105_0.method_3(ConfigOptions.LaunchArguments).Trim();
									process2.StartInfo.RedirectStandardError = true;
									process2.StartInfo.RedirectStandardOutput = true;
									ProcessHelper.smethod_5(process2);
									process2.Start();
									process2.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
									process2.StandardError.ReadToEndAsync().ConfigureAwait(false);
									Thread.Sleep((int)Class255.class105_0.method_6(ConfigOptions.LaunchGameTiming) * 1000);
									while (UI.smethod_28() && ProcessHelper.mainForm_0.gameProcessState_0 != GameProcessState.FixingConfig)
									{
										UI.smethod_1();
										Thread.Sleep(100);
									}
									((byte*)ptr)[11] = (UI.smethod_27() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 11) != 0)
									{
										UI.smethod_25();
									}
									ProcessHelper.mainForm_0.Invoke(new Action(ProcessHelper.<>c.<>9.method_0));
								}
								catch (Win32Exception)
								{
								}
								catch (ThreadAbortException)
								{
								}
								catch (Exception arg)
								{
									Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107378206), arg));
								}
							}
						}
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception arg2)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107377657), arg2));
			}
		}

		private static void smethod_5(Process process_1)
		{
			Class181.smethod_2(Enum11.const_3, ProcessHelper.getString_0(107377620), new object[]
			{
				Class255.class105_0.method_4(ConfigOptions.LimitedUser),
				process_1.StartInfo.UserName.smethod_10(),
				process_1.StartInfo.WorkingDirectory,
				process_1.StartInfo.FileName,
				process_1.StartInfo.Arguments
			});
		}

		private unsafe static string smethod_6(int int_1)
		{
			void* ptr = stackalloc byte[5];
			string empty;
			try
			{
				string queryString = ProcessHelper.getString_0(107377563) + int_1.ToString();
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString);
				ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					string[] array = new string[]
					{
						string.Empty,
						string.Empty
					};
					ref int ptr2 = ref *(int*)ptr;
					ManagementObject managementObject2 = managementObject;
					string methodName = ProcessHelper.getString_0(107377498);
					object[] args = array;
					ptr2 = Convert.ToInt32(managementObject2.InvokeMethod(methodName, args));
					((byte*)ptr)[4] = ((*(int*)ptr == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						return array[0];
					}
				}
				empty = string.Empty;
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}

		public static bool smethod_7(int int_1)
		{
			return !Class255.class105_0.method_4(ConfigOptions.LimitedUser) || int_1 == -1 || ProcessHelper.smethod_6(int_1) == Class255.class105_0.method_3(ConfigOptions.RunAsUser);
		}

		public unsafe static bool smethod_8()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = (Class255.GameClient.BaseProcessName.smethod_25() ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 1;
			}
			else
			{
				Process process = Process.GetProcesses().FirstOrDefault(new Func<Process, bool>(ProcessHelper.<>c.<>9.method_1));
				((byte*)ptr)[2] = ((process == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					Class255.GameClient.BaseProcessId = process.Id;
					((byte*)ptr)[1] = 1;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_9()
		{
			void* ptr = stackalloc byte[10];
			((byte*)ptr)[4] = ((ProcessHelper.int_0 != 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				IEnumerable<Process> source = Process.GetProcesses().Where(new Func<Process, bool>(ProcessHelper.<>c.<>9.method_2));
				((byte*)ptr)[5] = (source.Any<Process>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					Process process = source.First<Process>();
					UI.intptr_0 = process.MainWindowHandle;
					((byte*)ptr)[6] = (string.IsNullOrEmpty(Class120.string_6) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						Class120.string_6 = ProcessHelper.smethod_6(process.Id);
					}
					((byte*)ptr)[7] = 1;
					goto IL_173;
				}
			}
			((byte*)ptr)[8] = (Class255.class105_0.method_4(ConfigOptions.LimitedUser) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Class120.string_6 = Class255.class105_0.method_3(ConfigOptions.RunAsUser);
			}
			else
			{
				Class120.string_6 = Environment.UserName;
			}
			UI.intptr_0 = IntPtr.Zero;
			Process[] processes = Process.GetProcesses();
			Process[] array = processes;
			*(int*)ptr = 0;
			while (*(int*)ptr < array.Length)
			{
				Process process2 = array[*(int*)ptr];
				((byte*)ptr)[9] = (process2.ProcessName.Contains(ProcessHelper.getString_0(107350100)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					UI.intptr_0 = process2.MainWindowHandle;
					ProcessHelper.process_0 = process2;
					ProcessHelper.int_0 = process2.Id;
					Class120.string_6 = ProcessHelper.smethod_6(process2.Id);
					Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107377453), Class120.string_6));
					((byte*)ptr)[7] = 1;
					goto IL_173;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			((byte*)ptr)[7] = 0;
			IL_173:
			return *(sbyte*)((byte*)ptr + 7) != 0;
		}

		public static IEnumerable<Process> smethod_10()
		{
			return Process.GetProcesses().Where(new Func<Process, bool>(ProcessHelper.<>c.<>9.method_3));
		}

		public static void smethod_11(int int_1)
		{
			Process processById = Process.GetProcessById(int_1);
			if (processById == null)
			{
				Class181.smethod_3(Enum11.const_2, ProcessHelper.getString_0(107377436) + int_1.ToString());
			}
			else
			{
				ProcessHelper.process_0 = processById;
				UI.intptr_0 = processById.MainWindowHandle;
				ProcessHelper.int_0 = processById.Id;
			}
		}

		public static void smethod_12()
		{
			Process processById = Process.GetProcessById(ProcessHelper.int_0);
			if (processById.ProcessName != null)
			{
				processById.Kill();
			}
			else
			{
				Class181.smethod_3(Enum11.const_0, string.Format(ProcessHelper.getString_0(107377915), processById.ProcessName));
			}
		}

		public unsafe static bool smethod_13(bool bool_0 = false)
		{
			void* ptr = stackalloc byte[71];
			try
			{
				string text = ProcessHelper.getString_0(107397005);
				((byte*)ptr)[28] = ((!File.Exists(Class120.PoeIniPath)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 28) != 0)
				{
					((byte*)ptr)[29] = 0;
				}
				else
				{
					List<string> list = File.ReadLines(Class120.PoeIniPath).ToList<string>();
					foreach (string text2 in list)
					{
						((byte*)ptr)[30] = (text2.Contains(ProcessHelper.getString_0(107377802)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 30) != 0)
						{
							foreach (string text3 in list)
							{
								((byte*)ptr)[31] = (text3.Contains(ProcessHelper.getString_0(107377813)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 31) != 0)
								{
									break;
								}
								if (text3.Contains(ProcessHelper.getString_0(107377768)) && !text3.Contains(ProcessHelper.getString_0(107377763)))
								{
									text = text + ProcessHelper.getString_0(107377734) + text3;
								}
							}
						}
						Match match = ProcessHelper.regex_0.Match(text2);
						((byte*)ptr)[32] = ((match.Groups.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 32) != 0)
						{
							*(int*)((byte*)ptr + 8) = int.Parse(match.Groups[1].Value);
							ProcessHelper.mainForm_0.string_0 = ((char)(*(int*)((byte*)ptr + 8))).ToString().ToLower();
						}
						match = ProcessHelper.regex_1.Match(text2);
						((byte*)ptr)[33] = ((match.Groups.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 33) != 0)
						{
							*(int*)((byte*)ptr + 12) = int.Parse(match.Groups[1].Value);
							ProcessHelper.mainForm_0.string_1 = ((char)(*(int*)((byte*)ptr + 12))).ToString().ToLower();
						}
						match = ProcessHelper.regex_2.Match(text2);
						((byte*)ptr)[34] = ((match.Groups.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 34) != 0)
						{
							*(int*)((byte*)ptr + 16) = int.Parse(match.Groups[1].Value);
							ProcessHelper.mainForm_0.string_2 = ((char)(*(int*)((byte*)ptr + 16))).ToString().ToLower();
						}
						match = ProcessHelper.regex_17.Match(text2);
						((byte*)ptr)[35] = ((match.Groups.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 35) != 0)
						{
							*(int*)((byte*)ptr + 20) = int.Parse(match.Groups[1].Value);
							ProcessHelper.mainForm_0.string_3 = ((char)(*(int*)((byte*)ptr + 20))).ToString().ToLower();
						}
						match = ProcessHelper.regex_18.Match(text2);
						((byte*)ptr)[36] = ((match.Groups.Count > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 36) != 0)
						{
							*(int*)((byte*)ptr + 24) = int.Parse(match.Groups[1].Value);
							ProcessHelper.mainForm_0.string_4 = ((char)(*(int*)((byte*)ptr + 24))).ToString().ToLower();
						}
						int.TryParse(ProcessHelper.regex_3.Match(text2).Groups[1].Value, out *(int*)ptr);
						int.TryParse(ProcessHelper.regex_4.Match(text2).Groups[1].Value, out *(int*)((byte*)ptr + 4));
						if (text2.Contains(ProcessHelper.getString_0(107377753)) && *(int*)ptr != Class255.Resolution.Width)
						{
							text += ProcessHelper.getString_0(107377728);
						}
						if (text2.Contains(ProcessHelper.getString_0(107377671)) && *(int*)((byte*)ptr + 4) != Class255.Resolution.Height)
						{
							text += ProcessHelper.getString_0(107377134);
						}
						if (text2.Contains(ProcessHelper.getString_0(107377141)) && Class255.class105_0.method_5(ConfigOptions.WindowMode) != 1)
						{
							text += ProcessHelper.getString_0(107377060);
						}
						if (text2.Contains(ProcessHelper.getString_0(107377047)) && Class255.class105_0.method_5(ConfigOptions.WindowMode) != 0)
						{
							text += ProcessHelper.getString_0(107377060);
						}
						((byte*)ptr)[37] = ((text2 == ProcessHelper.getString_0(107376966)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 37) != 0)
						{
							text += ProcessHelper.getString_0(107376977);
						}
						((byte*)ptr)[38] = (text2.Contains(ProcessHelper.getString_0(107376960)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 38) != 0)
						{
							UI.int_3 = int.Parse(ProcessHelper.regex_6.Match(text2).Groups[1].Value);
							((byte*)ptr)[39] = ((UI.int_3 > 10) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 39) != 0)
							{
								text += ProcessHelper.getString_0(107376903);
							}
						}
						((byte*)ptr)[40] = (text2.Contains(ProcessHelper.getString_0(107377390)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 40) != 0)
						{
							UI.int_2 = int.Parse(ProcessHelper.regex_5.Match(text2).Groups[1].Value);
							((byte*)ptr)[41] = ((UI.int_2 > 10) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 41) != 0)
							{
								text += ProcessHelper.getString_0(107377401);
							}
						}
						((byte*)ptr)[42] = ((text2 == ProcessHelper.getString_0(107377348)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 42) != 0)
						{
							text += ProcessHelper.getString_0(107377339);
						}
						((byte*)ptr)[43] = ((text2 == ProcessHelper.getString_0(107377302)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 43) != 0)
						{
							text += ProcessHelper.getString_0(107377229);
						}
						((byte*)ptr)[44] = ((text2 == ProcessHelper.getString_0(107377192)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 44) != 0)
						{
							text += ProcessHelper.getString_0(107377159);
						}
						((byte*)ptr)[45] = ((text2 == ProcessHelper.getString_0(107376622)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 45) != 0)
						{
							text += ProcessHelper.getString_0(107376589);
						}
						((byte*)ptr)[46] = ((text2 == ProcessHelper.getString_0(107376596)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 46) != 0)
						{
							text += ProcessHelper.getString_0(107376567);
						}
						((byte*)ptr)[47] = ((text2 == ProcessHelper.getString_0(107376514)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 47) != 0)
						{
							text += ProcessHelper.getString_0(107376505);
						}
						((byte*)ptr)[48] = ((text2 == ProcessHelper.getString_0(107376472)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 48) != 0)
						{
							text += ProcessHelper.getString_0(107376419);
						}
						((byte*)ptr)[49] = ((text2 == ProcessHelper.getString_0(107376434)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 49) != 0)
						{
							text += ProcessHelper.getString_0(107376865);
						}
						if (!text2.Contains(ProcessHelper.getString_0(107376856)) && text2.Contains(ProcessHelper.getString_0(107376803)) && text2 != ProcessHelper.getString_0(107376822))
						{
							text += ProcessHelper.getString_0(107376773);
						}
						((byte*)ptr)[50] = (text2.Contains(ProcessHelper.getString_0(107376792)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 50) != 0)
						{
							text += ProcessHelper.getString_0(107376763);
						}
						((byte*)ptr)[51] = (text2.Contains(ProcessHelper.getString_0(107376706)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 51) != 0)
						{
							text += ProcessHelper.getString_0(107376677);
						}
						((byte*)ptr)[52] = (text2.Contains(ProcessHelper.getString_0(107376652)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 52) != 0)
						{
							text += ProcessHelper.getString_0(107376111);
						}
						((byte*)ptr)[53] = (text2.Contains(ProcessHelper.getString_0(107376118)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 53) != 0)
						{
							text += ProcessHelper.getString_0(107376089);
						}
						((byte*)ptr)[54] = (text2.Contains(ProcessHelper.getString_0(107376064)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 54) != 0)
						{
							text += ProcessHelper.getString_0(107376031);
						}
						((byte*)ptr)[55] = (text2.Contains(ProcessHelper.getString_0(107375974)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 55) != 0)
						{
							text += ProcessHelper.getString_0(107375945);
						}
						((byte*)ptr)[56] = (text2.Contains(ProcessHelper.getString_0(107375920)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 56) != 0)
						{
							text += ProcessHelper.getString_0(107375923);
						}
						((byte*)ptr)[57] = (text2.Contains(ProcessHelper.getString_0(107375898)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 57) != 0)
						{
							text += ProcessHelper.getString_0(107376373);
						}
						((byte*)ptr)[58] = (text2.Contains(ProcessHelper.getString_0(107376340)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 58) != 0)
						{
							text += ProcessHelper.getString_0(107376315);
						}
						if (text2.Contains(ProcessHelper.getString_0(107376266)) && text2 != ProcessHelper.getString_0(107376237))
						{
							text += ProcessHelper.getString_0(107376208);
						}
						((byte*)ptr)[59] = (text2.Contains(ProcessHelper.getString_0(107376211)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 59) != 0)
						{
							text += ProcessHelper.getString_0(107376182);
						}
						((byte*)ptr)[60] = (text2.Contains(ProcessHelper.getString_0(107376153)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 60) != 0)
						{
							text += ProcessHelper.getString_0(107375564);
						}
						((byte*)ptr)[61] = (text2.Contains(ProcessHelper.getString_0(107375551)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 61) != 0)
						{
							text += ProcessHelper.getString_0(107375510);
						}
						((byte*)ptr)[62] = (text2.Contains(ProcessHelper.getString_0(107375457)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 62) != 0)
						{
							text += ProcessHelper.getString_0(107375436);
						}
						((byte*)ptr)[63] = (text2.Contains(ProcessHelper.getString_0(107375447)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 63) != 0)
						{
							text += ProcessHelper.getString_0(107375418);
						}
						((byte*)ptr)[64] = (text2.Contains(ProcessHelper.getString_0(107375361)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 64) != 0)
						{
							text += ProcessHelper.getString_0(107375852);
						}
						((byte*)ptr)[65] = (text2.Contains(ProcessHelper.getString_0(107375863)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 65) != 0)
						{
							text += ProcessHelper.getString_0(107375838);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375781)) && text2 != ProcessHelper.getString_0(107375756))
						{
							text += ProcessHelper.getString_0(107375727);
						}
						((byte*)ptr)[66] = (text2.Contains(ProcessHelper.getString_0(107375730)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 66) != 0)
						{
							text += ProcessHelper.getString_0(107375657);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375624)) && text2 != ProcessHelper.getString_0(107375079))
						{
							text += ProcessHelper.getString_0(107375042);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375009)) && text2 != ProcessHelper.getString_0(107374980))
						{
							text += ProcessHelper.getString_0(107374947);
						}
						if (text2.Contains(ProcessHelper.getString_0(107374914)) && text2 != ProcessHelper.getString_0(107374885))
						{
							text += ProcessHelper.getString_0(107374880);
						}
						if (text2.StartsWith(ProcessHelper.getString_0(107375359)) && text2 != ProcessHelper.getString_0(107375322))
						{
							text += ProcessHelper.getString_0(107375281);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375212)) && text2 != ProcessHelper.getString_0(107375212) + Class255.NetworkingMode)
						{
							text += ProcessHelper.getString_0(107375219);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375194)) && text2 != ProcessHelper.getString_0(107375194) + Class255.class105_0.method_3(ConfigOptions.League))
						{
							text += ProcessHelper.getString_0(107375137);
						}
						if (text2.Contains(ProcessHelper.getString_0(107375112)) && text2 != ProcessHelper.getString_0(107374563))
						{
							text += ProcessHelper.getString_0(107374550);
						}
						((byte*)ptr)[67] = (text2.Contains(ProcessHelper.getString_0(107376434)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 67) != 0)
						{
							text += ProcessHelper.getString_0(107374477);
						}
						((byte*)ptr)[68] = (text2.Contains(ProcessHelper.getString_0(107374436)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 68) != 0)
						{
							text += ProcessHelper.getString_0(107374419);
						}
						((byte*)ptr)[69] = (text2.Contains(ProcessHelper.getString_0(107374342)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 69) != 0)
						{
							text += ProcessHelper.getString_0(107374837);
						}
					}
					((byte*)ptr)[70] = ((!string.IsNullOrEmpty(text)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 70) != 0)
					{
						Enum11 enum11_ = bool_0 ? Enum11.const_3 : Enum11.const_2;
						Class181.smethod_3(enum11_, ProcessHelper.getString_0(107374764) + text);
					}
					((byte*)ptr)[29] = ((text == ProcessHelper.getString_0(107397005)) ? 1 : 0);
				}
			}
			catch (Exception arg)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107374735), arg));
				((byte*)ptr)[29] = 0;
			}
			return *(sbyte*)((byte*)ptr + 29) != 0;
		}

		public unsafe static void smethod_14()
		{
			void* ptr = stackalloc byte[40];
			try
			{
				((byte*)ptr)[28] = (File.Exists(Class120.PoeIniPath) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 28) != 0)
				{
					string text = File.ReadAllText(Class120.PoeIniPath);
					text = text.Replace(ProcessHelper.getString_0(107376966), ProcessHelper.getString_0(107374702));
					List<string> list = File.ReadLines(Class120.PoeIniPath).ToList<string>();
					foreach (string text2 in list)
					{
						((byte*)ptr)[29] = (text2.Contains(ProcessHelper.getString_0(107377802)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 29) != 0)
						{
							foreach (string text3 in list)
							{
								((byte*)ptr)[30] = (text3.Contains(ProcessHelper.getString_0(107377813)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 30) != 0)
								{
									break;
								}
								if (text3.Contains(ProcessHelper.getString_0(107377768)) && !text3.Contains(ProcessHelper.getString_0(107377763)))
								{
									text = text.Replace(text3, text3.Replace(ProcessHelper.getString_0(107377768), ProcessHelper.getString_0(107374709)));
								}
							}
							break;
						}
					}
					*(int*)((byte*)ptr + 20) = Class255.class105_0.method_5(ConfigOptions.WindowMode);
					*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 20);
					if (*(int*)((byte*)ptr + 16) != 0)
					{
						if (*(int*)((byte*)ptr + 16) == 1)
						{
							text = text.Replace(ProcessHelper.getString_0(107377047), ProcessHelper.getString_0(107377141));
						}
					}
					else
					{
						text = text.Replace(ProcessHelper.getString_0(107377141), ProcessHelper.getString_0(107377047));
					}
					*(int*)ptr = int.Parse(ProcessHelper.regex_3.Match(text).Groups[1].Value);
					*(int*)((byte*)ptr + 4) = int.Parse(ProcessHelper.regex_4.Match(text).Groups[1].Value);
					UI.int_2 = int.Parse(ProcessHelper.regex_5.Match(text).Groups[1].Value);
					UI.int_3 = int.Parse(ProcessHelper.regex_6.Match(text).Groups[1].Value);
					string value = ProcessHelper.regex_7.Match(text).Groups[1].Value;
					((byte*)ptr)[31] = ((value != ProcessHelper.getString_0(107374672)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 31) != 0)
					{
						text = text.Replace(ProcessHelper.getString_0(107376803) + value, ProcessHelper.getString_0(107376822));
					}
					((byte*)ptr)[32] = ((*(int*)ptr != Class255.Resolution.Width) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 32) != 0)
					{
						string text4 = text;
						string oldValue = ProcessHelper.getString_0(107377753) + ((int*)ptr)->ToString();
						string str = ProcessHelper.getString_0(107377753);
						*(int*)((byte*)ptr + 24) = Class255.Resolution.Width;
						text = text4.Replace(oldValue, str + ((int*)((byte*)ptr + 24))->ToString());
					}
					((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 4) != Class255.Resolution.Height) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 33) != 0)
					{
						string text5 = text;
						string oldValue2 = ProcessHelper.getString_0(107377671) + ((int*)((byte*)ptr + 4))->ToString();
						string str2 = ProcessHelper.getString_0(107377671);
						*(int*)((byte*)ptr + 24) = Class255.Resolution.Height;
						text = text5.Replace(oldValue2, str2 + ((int*)((byte*)ptr + 24))->ToString());
					}
					((byte*)ptr)[34] = ((UI.int_2 != 10) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 34) != 0)
					{
						text = text.Replace(ProcessHelper.getString_0(107377390) + UI.int_2.ToString(), ProcessHelper.getString_0(107374667));
					}
					((byte*)ptr)[35] = ((UI.int_3 != 10) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 35) != 0)
					{
						text = text.Replace(ProcessHelper.getString_0(107376960) + UI.int_3.ToString(), ProcessHelper.getString_0(107374674));
					}
					string value2 = ProcessHelper.regex_8.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107376266) + value2, ProcessHelper.getString_0(107376237));
					text = text.Replace(ProcessHelper.getString_0(107376211), ProcessHelper.getString_0(107374649));
					string value3 = ProcessHelper.regex_9.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107374620) + value3, ProcessHelper.getString_0(107374031));
					text = text.Replace(ProcessHelper.getString_0(107375551), ProcessHelper.getString_0(107373986));
					string value4 = ProcessHelper.regex_10.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107373977) + value4, ProcessHelper.getString_0(107373924));
					text = text.Replace(ProcessHelper.getString_0(107375447), ProcessHelper.getString_0(107373903));
					text = text.Replace(ProcessHelper.getString_0(107375361), ProcessHelper.getString_0(107373906));
					text = text.Replace(ProcessHelper.getString_0(107375863), ProcessHelper.getString_0(107373885));
					string value5 = ProcessHelper.regex_11.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375781) + value5, ProcessHelper.getString_0(107375756));
					text = text.Replace(ProcessHelper.getString_0(107375730), ProcessHelper.getString_0(107373828));
					string value6 = ProcessHelper.regex_12.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375624) + value6, ProcessHelper.getString_0(107375079));
					string value7 = ProcessHelper.regex_13.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375009) + value7, ProcessHelper.getString_0(107374980));
					string value8 = ProcessHelper.regex_14.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375212) + value8, ProcessHelper.getString_0(107375212) + Class255.NetworkingMode);
					text = text.Replace(ProcessHelper.getString_0(107374335), ProcessHelper.getString_0(107374274));
					text = text.Replace(ProcessHelper.getString_0(107377348), ProcessHelper.getString_0(107374241));
					text = text.Replace(ProcessHelper.getString_0(107377302), ProcessHelper.getString_0(107374232));
					text = text.Replace(ProcessHelper.getString_0(107377192), ProcessHelper.getString_0(107374155));
					text = text.Replace(ProcessHelper.getString_0(107376622), ProcessHelper.getString_0(107374126));
					text = text.Replace(ProcessHelper.getString_0(107376596), ProcessHelper.getString_0(107374093));
					text = text.Replace(ProcessHelper.getString_0(107376514), ProcessHelper.getString_0(107373552));
					text = text.Replace(ProcessHelper.getString_0(107376472), ProcessHelper.getString_0(107373511));
					text = text.Replace(ProcessHelper.getString_0(107376792), ProcessHelper.getString_0(107373486));
					text = text.Replace(ProcessHelper.getString_0(107376706), ProcessHelper.getString_0(107373453));
					text = text.Replace(ProcessHelper.getString_0(107376652), ProcessHelper.getString_0(107373424));
					text = text.Replace(ProcessHelper.getString_0(107376118), ProcessHelper.getString_0(107373427));
					text = text.Replace(ProcessHelper.getString_0(107376064), ProcessHelper.getString_0(107373398));
					text = text.Replace(ProcessHelper.getString_0(107375974), ProcessHelper.getString_0(107373365));
					text = text.Replace(ProcessHelper.getString_0(107375920), ProcessHelper.getString_0(107373332));
					text = text.Replace(ProcessHelper.getString_0(107375898), ProcessHelper.getString_0(107373815));
					text = text.Replace(ProcessHelper.getString_0(107373742), ProcessHelper.getString_0(107374885));
					text = text.Replace(ProcessHelper.getString_0(107373705), ProcessHelper.getString_0(107375322));
					text = text.Replace(ProcessHelper.getString_0(107376340), ProcessHelper.getString_0(107373696));
					string value9 = ProcessHelper.regex_15.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375112) + value9, ProcessHelper.getString_0(107374563));
					string value10 = ProcessHelper.regex_16.Match(text).Groups[1].Value;
					text = text.Replace(ProcessHelper.getString_0(107375194) + value10, ProcessHelper.getString_0(107375194) + Class255.class105_0.method_3(ConfigOptions.League));
					text = text.Replace(ProcessHelper.getString_0(107374342), ProcessHelper.getString_0(107373643));
					*(int*)((byte*)ptr + 8) = text.IndexOf(ProcessHelper.getString_0(107373630));
					*(int*)((byte*)ptr + 12) = text.IndexOf(ProcessHelper.getString_0(107373577));
					string value11 = ProcessHelper.getString_0(107397005);
					((byte*)ptr)[36] = ((*(int*)((byte*)ptr + 8) == -1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 36) != 0)
					{
						*(int*)((byte*)ptr + 8) = text.IndexOf(ProcessHelper.getString_0(107373577));
						value11 = text.Substring(0, *(int*)((byte*)ptr + 8));
					}
					else
					{
						value11 = text.Substring(0, *(int*)((byte*)ptr + 8));
					}
					string value12 = text.Substring(*(int*)((byte*)ptr + 12), text.Length - *(int*)((byte*)ptr + 12));
					using (StreamWriter streamWriter = new StreamWriter(Class120.PoeIniPath))
					{
						streamWriter.Write(value11);
						streamWriter.Write(Class238.notifications);
						streamWriter.Write(value12);
						goto IL_C9B;
					}
				}
				try
				{
					ProcessHelper.smethod_4();
					for (;;)
					{
						((byte*)ptr)[37] = ((ProcessHelper.process_0 == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 37) == 0)
						{
							break;
						}
						ProcessHelper.smethod_9();
						Thread.Sleep(100);
					}
					Thread.Sleep(1000);
					Position position;
					((byte*)ptr)[38] = (UI.smethod_2(out position, Class238.Launch, ProcessHelper.getString_0(107373596), 0.8) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 38) != 0)
					{
						UI.smethod_25();
					}
					for (;;)
					{
						((byte*)ptr)[39] = ((UI.smethod_29() != GameProcessState.FixingConfig) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 39) == 0)
						{
							break;
						}
						Thread.Sleep(100);
					}
					UI.smethod_1();
					Win32.smethod_14(ProcessHelper.getString_0(107393999), false);
					Thread.Sleep(1000);
					Win32.smethod_14(ProcessHelper.getString_0(107393999), false);
					Thread.Sleep(1000);
					ProcessHelper.smethod_14();
					Thread.Sleep(1000);
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception arg)
				{
					Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107373039), arg));
				}
				IL_C9B:;
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception arg2)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(ProcessHelper.getString_0(107373039), arg2));
			}
		}

		public unsafe static bool UsingVulkan
		{
			get
			{
				void* ptr = stackalloc byte[2];
				*(byte*)ptr = ((!File.Exists(Class120.PoeIniPath)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					string text = File.ReadAllText(Class120.PoeIniPath);
					((byte*)ptr)[1] = (text.Contains(ProcessHelper.getString_0(107374335)) ? 1 : 0);
				}
				return *(sbyte*)((byte*)ptr + 1) != 0;
			}
		}

		public unsafe static void smethod_15()
		{
			void* ptr = stackalloc byte[3];
			string text = string.Format(ProcessHelper.getString_0(107373006), Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			*(byte*)ptr = (Class255.class105_0.method_4(ConfigOptions.LimitedUser) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				text = text.Replace(Environment.UserName, Class255.class105_0.method_3(ConfigOptions.RunAsUser));
			}
			((byte*)ptr)[1] = ((!File.Exists(text)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) == 0)
			{
				List<string> list = new List<string>();
				FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				for (;;)
				{
					((byte*)ptr)[2] = ((!streamReader.EndOfStream) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						break;
					}
					string text2 = streamReader.ReadLine();
					if (!text2.StartsWith(ProcessHelper.getString_0(107372953)) && !text2.StartsWith(ProcessHelper.getString_0(107372892)))
					{
						list.Add(text2);
					}
				}
				streamReader.Close();
				streamReader.Dispose();
				fileStream.Close();
				fileStream.Dispose();
				FileStream fileStream2 = new FileStream(text, FileMode.Create, FileAccess.Write);
				StreamWriter streamWriter = new StreamWriter(fileStream2);
				foreach (string value in list)
				{
					streamWriter.WriteLine(value);
				}
				streamWriter.WriteLine(ProcessHelper.getString_0(107372953));
				streamWriter.WriteLine(ProcessHelper.getString_0(107372823));
				streamWriter.WriteLine(ProcessHelper.getString_0(107373218) + Class255.class105_0.method_3(ConfigOptions.LaunchArguments));
				streamWriter.Close();
				streamWriter.Dispose();
				fileStream2.Close();
				fileStream2.Dispose();
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ProcessHelper()
		{
			Strings.CreateGetStringDelegate(typeof(ProcessHelper));
			ProcessHelper.regex_0 = new Regex(ProcessHelper.getString_0(107373177));
			ProcessHelper.regex_1 = new Regex(ProcessHelper.getString_0(107373140));
			ProcessHelper.regex_2 = new Regex(ProcessHelper.getString_0(107373063));
			ProcessHelper.regex_3 = new Regex(ProcessHelper.getString_0(107372538));
			ProcessHelper.regex_4 = new Regex(ProcessHelper.getString_0(107372505));
			ProcessHelper.regex_5 = new Regex(ProcessHelper.getString_0(107372472));
			ProcessHelper.regex_6 = new Regex(ProcessHelper.getString_0(107372443));
			ProcessHelper.regex_7 = new Regex(ProcessHelper.getString_0(107372414));
			ProcessHelper.regex_8 = new Regex(ProcessHelper.getString_0(107372361));
			ProcessHelper.regex_9 = new Regex(ProcessHelper.getString_0(107372328));
			ProcessHelper.regex_10 = new Regex(ProcessHelper.getString_0(107372311));
			ProcessHelper.regex_11 = new Regex(ProcessHelper.getString_0(107372798));
			ProcessHelper.regex_12 = new Regex(ProcessHelper.getString_0(107372765));
			ProcessHelper.regex_13 = new Regex(ProcessHelper.getString_0(107372728));
			ProcessHelper.regex_14 = new Regex(ProcessHelper.getString_0(107372691));
			ProcessHelper.regex_15 = new Regex(ProcessHelper.getString_0(107372662));
			ProcessHelper.regex_16 = new Regex(ProcessHelper.getString_0(107372585));
			ProcessHelper.regex_17 = new Regex(ProcessHelper.getString_0(107372556));
			ProcessHelper.regex_18 = new Regex(ProcessHelper.getString_0(107372011));
			ProcessHelper.string_0 = new string[]
			{
				ProcessHelper.getString_0(107371974),
				ProcessHelper.getString_0(107371993),
				ProcessHelper.getString_0(107371944),
				ProcessHelper.getString_0(107371919)
			};
		}

		private static Regex regex_0;

		private static Regex regex_1;

		private static Regex regex_2;

		private static Regex regex_3;

		private static Regex regex_4;

		private static Regex regex_5;

		private static Regex regex_6;

		private static Regex regex_7;

		private static Regex regex_8;

		private static Regex regex_9;

		private static Regex regex_10;

		private static Regex regex_11;

		private static Regex regex_12;

		private static Regex regex_13;

		private static Regex regex_14;

		private static Regex regex_15;

		private static Regex regex_16;

		private static Regex regex_17;

		private static Regex regex_18;

		private static MainForm mainForm_0;

		private static Process process_0;

		public static int int_0;

		private static string[] string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
