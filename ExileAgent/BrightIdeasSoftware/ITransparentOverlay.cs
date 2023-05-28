using System;

namespace BrightIdeasSoftware
{
	public interface ITransparentOverlay : IOverlay
	{
		int Transparency { get; set; }
	}
}
