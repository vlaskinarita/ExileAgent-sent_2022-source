using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware.Properties
{
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal sealed class Resources
	{
		internal Resources()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager(Resources.getString_0(107314523), typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap ClearFiltering
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject(Resources.getString_0(107314498), Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap ColumnFilterIndicator
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject(Resources.getString_0(107314477), Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Filtering
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject(Resources.getString_0(107314448), Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap SortAscending
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject(Resources.getString_0(107314435), Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap SortDescending
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject(Resources.getString_0(107314414), Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		static Resources()
		{
			Strings.CreateGetStringDelegate(typeof(Resources));
		}

		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
