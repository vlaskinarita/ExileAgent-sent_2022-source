using System;
using System.Reflection;
using System.Reflection.Emit;
using ns20;
using ns43;
using SmartAssembly.Delegates;

namespace SmartAssembly.HouseOfCards
{
	public static class Strings
	{
		[Attribute1]
		public unsafe static void CreateGetStringDelegate(Type ownerType)
		{
			void* ptr = stackalloc byte[8];
			FieldInfo[] fields = ownerType.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
			*(int*)ptr = 0;
			while (*(int*)ptr < fields.Length)
			{
				FieldInfo fieldInfo = fields[*(int*)ptr];
				try
				{
					if (fieldInfo.FieldType == typeof(GetString))
					{
						DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(string), new Type[]
						{
							typeof(int)
						}, ownerType.Module, true);
						ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
						ilgenerator.Emit(OpCodes.Ldarg_0);
						MethodInfo[] methods = typeof(Class401).GetMethods(BindingFlags.Static | BindingFlags.Public);
						*(int*)((byte*)ptr + 4) = 0;
						while (*(int*)((byte*)ptr + 4) < methods.Length)
						{
							MethodInfo methodInfo = methods[*(int*)((byte*)ptr + 4)];
							if (methodInfo.ReturnType == typeof(string))
							{
								ilgenerator.Emit(OpCodes.Ldc_I4, fieldInfo.MetadataToken & 16777215);
								ilgenerator.Emit(OpCodes.Sub);
								ilgenerator.Emit(OpCodes.Call, methodInfo);
								IL_FB:
								ilgenerator.Emit(OpCodes.Ret);
								fieldInfo.SetValue(null, dynamicMethod.CreateDelegate(typeof(GetString)));
								return;
							}
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
						goto IL_FB;
					}
				}
				catch
				{
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
		}
	}
}
