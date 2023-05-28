using System;
using System.ComponentModel;
using System.Globalization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware.Design
{
	internal sealed class OverlayConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				ImageOverlay imageOverlay = value as ImageOverlay;
				if (imageOverlay != null)
				{
					if (imageOverlay.Image != null)
					{
						return OverlayConverter.getString_0(107313439);
					}
					return OverlayConverter.getString_0(107313430);
				}
				else
				{
					TextOverlay textOverlay = value as TextOverlay;
					if (textOverlay != null)
					{
						if (!string.IsNullOrEmpty(textOverlay.Text))
						{
							return OverlayConverter.getString_0(107313439);
						}
						return OverlayConverter.getString_0(107313430);
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		static OverlayConverter()
		{
			Strings.CreateGetStringDelegate(typeof(OverlayConverter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
