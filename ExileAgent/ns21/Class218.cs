using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns21
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal sealed class Class218
	{
		internal Class218()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Class218.resourceManager_0 == null)
				{
					ResourceManager resourceManager = new ResourceManager(Class218.getString_0(107430172), typeof(Class218).Assembly);
					Class218.resourceManager_0 = resourceManager;
				}
				return Class218.resourceManager_0;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Class218.cultureInfo_0;
			}
			set
			{
				Class218.cultureInfo_0 = value;
			}
		}

		internal static string EnvironmentFiles
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430167), Class218.cultureInfo_0);
			}
		}

		internal static string EnvironmentReplacement
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430110), Class218.cultureInfo_0);
			}
		}

		internal static string MaterialFiles
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430077), Class218.cultureInfo_0);
			}
		}

		internal static string MaterialReplacement
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430056), Class218.cultureInfo_0);
			}
		}

		internal static string ParticleFiles
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430027), Class218.cultureInfo_0);
			}
		}

		internal static string ParticleReplacement
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430038), Class218.cultureInfo_0);
			}
		}

		internal static string RendererFiles
		{
			get
			{
				return Class218.ResourceManager.GetString(Class218.getString_0(107430009), Class218.cultureInfo_0);
			}
		}

		static Class218()
		{
			Strings.CreateGetStringDelegate(typeof(Class218));
		}

		private static ResourceManager resourceManager_0;

		private static CultureInfo cultureInfo_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
