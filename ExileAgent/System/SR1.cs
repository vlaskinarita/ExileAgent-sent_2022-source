using System;
using System.Resources;
using FxResources.System.Memory;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace System
{
	internal static class SR1
	{
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = SR1.s_resourceManager) == null)
				{
					result = (SR1.s_resourceManager = new ResourceManager(SR1.ResourceType));
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
				text = SR1.ResourceManager.GetString(resourceKey);
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
			if (SR1.UsingResourceKeys())
			{
				return resourceFormat + string.Join(SR1.getString_0(107404802), args);
			}
			return string.Format(resourceFormat, args);
		}

		internal static string Format(string resourceFormat, object p1)
		{
			if (SR1.UsingResourceKeys())
			{
				return string.Join(SR1.getString_0(107404802), new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (SR1.UsingResourceKeys())
			{
				return string.Join(SR1.getString_0(107404802), new object[]
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
			if (SR1.UsingResourceKeys())
			{
				return string.Join(SR1.getString_0(107404802), new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		internal static Type ResourceType { get; }

		internal static string NotSupported_CannotCallEqualsOnSpan
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143542), null);
			}
		}

		internal static string NotSupported_CannotCallGetHashCodeOnSpan
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143461), null);
			}
		}

		internal static string Argument_InvalidTypeWithPointersNotSupported
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143916), null);
			}
		}

		internal static string Argument_DestinationTooShort
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143855), null);
			}
		}

		internal static string MemoryDisposed
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143814), null);
			}
		}

		internal static string OutstandingReferences
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143793), null);
			}
		}

		internal static string Argument_BadFormatSpecifier
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143796), null);
			}
		}

		internal static string Argument_GWithPrecisionNotSupported
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143727), null);
			}
		}

		internal static string Argument_CannotParsePrecision
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143198), null);
			}
		}

		internal static string Argument_PrecisionTooLarge
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143157), null);
			}
		}

		internal static string Argument_OverlapAlignmentMismatch
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143088), null);
			}
		}

		internal static string EndPositionNotReached
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143043), null);
			}
		}

		internal static string UnexpectedSegmentType
		{
			get
			{
				return SR1.GetResourceString(SR1.getString_0(107143014), null);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static SR1()
		{
			Strings.CreateGetStringDelegate(typeof(SR1));
			SR1.ResourceType = typeof(FxResources.System.Memory.SR);
		}

		private static ResourceManager s_resourceManager;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
