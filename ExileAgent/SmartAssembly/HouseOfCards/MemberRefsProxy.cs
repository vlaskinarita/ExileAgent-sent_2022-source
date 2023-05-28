using System;
using System.Reflection;
using System.Reflection.Emit;
using ns43;

namespace SmartAssembly.HouseOfCards
{
	public static class MemberRefsProxy
	{
		[Attribute1]
		public unsafe static void CreateMemberRefsDelegates(int typeID)
		{
			void* ptr = stackalloc byte[29];
			Type typeFromHandle;
			try
			{
				typeFromHandle = Type.GetTypeFromHandle(MemberRefsProxy.moduleHandle_0.ResolveTypeHandle(33554433 + typeID));
			}
			catch
			{
				return;
			}
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
			*(int*)ptr = 0;
			IL_34:
			while (*(int*)ptr < fields.Length)
			{
				FieldInfo fieldInfo = fields[*(int*)ptr];
				string name = fieldInfo.Name;
				((byte*)ptr)[28] = 0;
				*(int*)((byte*)ptr + 4) = 0;
				*(int*)((byte*)ptr + 8) = name.Length - 1;
				IL_C5:
				while (*(int*)((byte*)ptr + 8) >= 0)
				{
					char c = name[*(int*)((byte*)ptr + 8)];
					if (c == '~')
					{
						((byte*)ptr)[28] = 1;
						IL_D4:
						MethodInfo methodInfo;
						try
						{
							methodInfo = (MethodInfo)MethodBase.GetMethodFromHandle(MemberRefsProxy.moduleHandle_0.ResolveMethodHandle(*(int*)((byte*)ptr + 4) + 167772161));
						}
						catch
						{
							goto IL_278;
						}
						goto IL_FD;
						IL_278:
						*(int*)ptr = *(int*)ptr + 1;
						goto IL_34;
						IL_FD:
						Delegate value;
						if (methodInfo.IsStatic)
						{
							try
							{
								value = Delegate.CreateDelegate(fieldInfo.FieldType, methodInfo);
								goto IL_269;
							}
							catch (Exception)
							{
								goto IL_278;
							}
						}
						ParameterInfo[] parameters = methodInfo.GetParameters();
						*(int*)((byte*)ptr + 16) = parameters.Length + 1;
						Type[] array = new Type[*(int*)((byte*)ptr + 16)];
						array[0] = typeof(object);
						*(int*)((byte*)ptr + 20) = 1;
						while (*(int*)((byte*)ptr + 20) < *(int*)((byte*)ptr + 16))
						{
							array[*(int*)((byte*)ptr + 20)] = parameters[*(int*)((byte*)ptr + 20) - 1].ParameterType;
							*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
						}
						DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, methodInfo.ReturnType, array, typeFromHandle, true);
						ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
						ilgenerator.Emit(OpCodes.Ldarg_0);
						if (*(int*)((byte*)ptr + 16) > 1)
						{
							ilgenerator.Emit(OpCodes.Ldarg_1);
						}
						if (*(int*)((byte*)ptr + 16) > 2)
						{
							ilgenerator.Emit(OpCodes.Ldarg_2);
						}
						if (*(int*)((byte*)ptr + 16) > 3)
						{
							ilgenerator.Emit(OpCodes.Ldarg_3);
						}
						if (*(int*)((byte*)ptr + 16) > 4)
						{
							*(int*)((byte*)ptr + 24) = 4;
							while (*(int*)((byte*)ptr + 24) < *(int*)((byte*)ptr + 16))
							{
								ilgenerator.Emit(OpCodes.Ldarg_S, *(int*)((byte*)ptr + 24));
								*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + 1;
							}
						}
						ilgenerator.Emit(OpCodes.Tailcall);
						ilgenerator.Emit((*(sbyte*)((byte*)ptr + 28) != 0) ? OpCodes.Callvirt : OpCodes.Call, methodInfo);
						ilgenerator.Emit(OpCodes.Ret);
						try
						{
							value = dynamicMethod.CreateDelegate(typeFromHandle);
						}
						catch
						{
							goto IL_278;
						}
						IL_269:
						try
						{
							fieldInfo.SetValue(null, value);
						}
						catch
						{
						}
						goto IL_278;
					}
					*(int*)((byte*)ptr + 12) = 0;
					while (*(int*)((byte*)ptr + 12) < 58)
					{
						if (MemberRefsProxy.char_0[*(int*)((byte*)ptr + 12)] == c)
						{
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) * 58 + *(int*)((byte*)ptr + 12);
							IL_BB:
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) - 1;
							goto IL_C5;
						}
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
					}
					goto IL_BB;
				}
				goto IL_D4;
			}
		}

		static MemberRefsProxy()
		{
			if (typeof(MulticastDelegate) != null)
			{
				MemberRefsProxy.moduleHandle_0 = Assembly.GetExecutingAssembly().GetModules()[0].ModuleHandle;
			}
		}

		private static ModuleHandle moduleHandle_0;

		private static char[] char_0 = new char[]
		{
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			'\u007f',
			'\u0080',
			'\u0081',
			'\u0082',
			'\u0083',
			'\u0084',
			'\u0086',
			'\u0087',
			'\u0088',
			'\u0089',
			'\u008a',
			'\u008b',
			'\u008c',
			'\u008d',
			'\u008e',
			'\u008f',
			'\u0090',
			'\u0091',
			'\u0092',
			'\u0093',
			'\u0094',
			'\u0095',
			'\u0096',
			'\u0097',
			'\u0098',
			'\u0099',
			'\u009a',
			'\u009b',
			'\u009c',
			'\u009d',
			'\u009e',
			'\u009f'
		};
	}
}
