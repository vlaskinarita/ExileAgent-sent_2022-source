using System;
using System.Resources;
using FxResources.System.Buffers;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace System
{
	internal static class SR
	{
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.SR.s_resourceManager) == null)
				{
					result = (System.SR.s_resourceManager = new ResourceManager(System.SR.ResourceType));
				}
				return result;
			}
		}

		private static bool UsingResourceKeys()
		{
			return false;
		}

		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(System.SR.getString_0(107404667), args);
			}
			return string.Format(resourceFormat, args);
		}

		internal static string Format(string resourceFormat, object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(System.SR.getString_0(107404667), new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(System.SR.getString_0(107404667), new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, p1, p2);
		}

		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(System.SR.getString_0(107404667), new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		internal static string ArgumentException_BufferNotFromPool
		{
			get
			{
				return System.SR.GetResourceString(System.SR.getString_0(107144266), null);
			}
		}

		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.Buffers.SR);
			}
		}

		static SR()
		{
			Strings.CreateGetStringDelegate(typeof(System.SR));
		}

		private static ResourceManager s_resourceManager;

		private const string s_resourcesName = "FxResources.System.Buffers.SR";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
