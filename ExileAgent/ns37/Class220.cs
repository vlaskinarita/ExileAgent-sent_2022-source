using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns37
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal sealed class Class220
	{
		internal Class220()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Class220.resourceManager_0 == null)
				{
					ResourceManager resourceManager = new ResourceManager(Class220.getString_0(107448396), typeof(Class220).Assembly);
					Class220.resourceManager_0 = resourceManager;
				}
				return Class220.resourceManager_0;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Class220.cultureInfo_0;
			}
			set
			{
				Class220.cultureInfo_0 = value;
			}
		}

		internal static byte[] Constantine
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107448387), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] DFPT_B5_POE
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447858), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Fontin_Bold
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447873), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Fontin_Italic
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447824), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Fontin_Regular
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447835), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Fontin_SmallCaps
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447782), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] FrizQuadrataITC
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447757), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] hkyt
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447768), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] OptimusPrincepsSemiBold
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447727), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Quikhand
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447694), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Thasadith_Bold
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447713), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Thasadith_Italic
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447660), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] Thasadith_Regular
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447667), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		internal static byte[] YDSapphIIM
		{
			get
			{
				object @object = Class220.ResourceManager.GetObject(Class220.getString_0(107447642), Class220.cultureInfo_0);
				return (byte[])@object;
			}
		}

		static Class220()
		{
			Strings.CreateGetStringDelegate(typeof(Class220));
		}

		private static ResourceManager resourceManager_0;

		private static CultureInfo cultureInfo_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
