using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ImageDiff;

namespace ns2
{
	internal sealed class Class2
	{
		public AnalyzerTypes AnalyzerType { get; set; }

		public LabelerTypes Labeler { get; set; }

		public double JustNoticeableDifference { get; set; }

		public int DetectionPadding { get; set; }

		public int BoundingBoxPadding { get; set; }

		public Color BoundingBoxColor { get; set; }

		public BoundingBoxModes BoundingBoxMode { get; set; }

		public Class2()
		{
			this.Labeler = LabelerTypes.Basic;
			this.JustNoticeableDifference = 2.3;
			this.DetectionPadding = 2;
			this.BoundingBoxPadding = 2;
			this.BoundingBoxColor = Color.Red;
			this.BoundingBoxMode = BoundingBoxModes.Single;
			this.AnalyzerType = AnalyzerTypes.ExactMatch;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AnalyzerTypes analyzerTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LabelerTypes labelerTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Color color_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BoundingBoxModes boundingBoxModes_0;
	}
}
