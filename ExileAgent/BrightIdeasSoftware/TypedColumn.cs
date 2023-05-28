using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using ns20;

namespace BrightIdeasSoftware
{
	public sealed class TypedColumn<T> where T : class
	{
		public TypedColumn(OLVColumn column)
		{
			this.column = column;
		}

		public TypedColumn<T>.TypedAspectGetterDelegate AspectGetter
		{
			get
			{
				return this.aspectGetter;
			}
			set
			{
				this.aspectGetter = value;
				if (value == null)
				{
					this.column.AspectGetter = null;
					return;
				}
				this.column.AspectGetter = delegate(object x)
				{
					if (x != null)
					{
						return this.aspectGetter((T)((object)x));
					}
					return null;
				};
			}
		}

		public TypedColumn<T>.TypedAspectPutterDelegate AspectPutter
		{
			get
			{
				return this.aspectPutter;
			}
			set
			{
				this.aspectPutter = value;
				if (value == null)
				{
					this.column.AspectPutter = null;
					return;
				}
				this.column.AspectPutter = delegate(object x, object newValue)
				{
					this.aspectPutter((T)((object)x), newValue);
				};
			}
		}

		public TypedColumn<T>.TypedImageGetterDelegate ImageGetter
		{
			get
			{
				return this.imageGetter;
			}
			set
			{
				this.imageGetter = value;
				if (value == null)
				{
					this.column.ImageGetter = null;
					return;
				}
				this.column.ImageGetter = ((object x) => this.imageGetter((T)((object)x)));
			}
		}

		public TypedColumn<T>.TypedGroupKeyGetterDelegate GroupKeyGetter
		{
			get
			{
				return this.groupKeyGetter;
			}
			set
			{
				this.groupKeyGetter = value;
				if (value == null)
				{
					this.column.GroupKeyGetter = null;
					return;
				}
				this.column.GroupKeyGetter = ((object x) => this.groupKeyGetter((T)((object)x)));
			}
		}

		public void GenerateAspectGetter()
		{
			if (!string.IsNullOrEmpty(this.column.AspectName))
			{
				this.AspectGetter = this.GenerateAspectGetter(typeof(T), this.column.AspectName);
			}
		}

		private TypedColumn<T>.TypedAspectGetterDelegate GenerateAspectGetter(Type type, string path)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[]
			{
				type
			}, type, true);
			this.GenerateIL(type, path, dynamicMethod.GetILGenerator());
			return (TypedColumn<T>.TypedAspectGetterDelegate)dynamicMethod.CreateDelegate(typeof(TypedColumn<T>.TypedAspectGetterDelegate));
		}

		private void GenerateIL(Type type, string path, ILGenerator il)
		{
			il.Emit(OpCodes.Ldarg_0);
			string[] array = path.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < array.Length; i++)
			{
				type = this.GeneratePart(il, type, array[i], i == array.Length - 1);
				if (type == null)
				{
					break;
				}
			}
			if (type != null && type.IsValueType && !typeof(T).IsValueType)
			{
				il.Emit(OpCodes.Box, type);
			}
			il.Emit(OpCodes.Ret);
		}

		private Type GeneratePart(ILGenerator il, Type type, string pathPart, bool isLastPart)
		{
			List<MemberInfo> list = new List<MemberInfo>(type.GetMember(pathPart));
			MemberInfo memberInfo = list.Find(delegate(MemberInfo x)
			{
				if (x.MemberType != MemberTypes.Field)
				{
					if (x.MemberType != MemberTypes.Property)
					{
						return x.MemberType == MemberTypes.Method && ((MethodInfo)x).GetParameters().Length == 0;
					}
				}
				return true;
			});
			if (memberInfo == null)
			{
				il.Emit(OpCodes.Pop);
				if (Munger.IgnoreMissingAspects)
				{
					il.Emit(OpCodes.Ldnull);
				}
				else
				{
					il.Emit(OpCodes.Ldstr, string.Format(Class401.smethod_0(107306300), pathPart, type.FullName));
				}
				return null;
			}
			Type type2 = null;
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType != MemberTypes.Method)
				{
					if (memberType == MemberTypes.Property)
					{
						PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
						il.Emit(OpCodes.Call, propertyInfo.GetGetMethod());
						type2 = propertyInfo.PropertyType;
					}
				}
				else
				{
					MethodInfo methodInfo = (MethodInfo)memberInfo;
					if (methodInfo.IsVirtual)
					{
						il.Emit(OpCodes.Callvirt, methodInfo);
					}
					else
					{
						il.Emit(OpCodes.Call, methodInfo);
					}
					type2 = methodInfo.ReturnType;
				}
			}
			else
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				il.Emit(OpCodes.Ldfld, fieldInfo);
				type2 = fieldInfo.FieldType;
			}
			if (type2.IsValueType && !isLastPart)
			{
				LocalBuilder local = il.DeclareLocal(type2);
				il.Emit(OpCodes.Stloc, local);
				il.Emit(OpCodes.Ldloca, local);
			}
			return type2;
		}

		private OLVColumn column;

		private TypedColumn<T>.TypedAspectGetterDelegate aspectGetter;

		private TypedColumn<T>.TypedAspectPutterDelegate aspectPutter;

		private TypedColumn<T>.TypedImageGetterDelegate imageGetter;

		private TypedColumn<T>.TypedGroupKeyGetterDelegate groupKeyGetter;

		public delegate object TypedAspectGetterDelegate(T rowObject);

		public delegate void TypedAspectPutterDelegate(T rowObject, object newValue);

		public delegate object TypedGroupKeyGetterDelegate(T rowObject);

		public delegate object TypedImageGetterDelegate(T rowObject);
	}
}
