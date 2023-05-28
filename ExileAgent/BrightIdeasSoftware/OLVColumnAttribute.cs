using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class OLVColumnAttribute : Attribute
	{
		public OLVColumnAttribute()
		{
		}

		public OLVColumnAttribute(string title)
		{
			this.Title = title;
		}

		public string AspectToStringFormat
		{
			get
			{
				return this.aspectToStringFormat;
			}
			set
			{
				this.aspectToStringFormat = value;
			}
		}

		public bool CheckBoxes
		{
			get
			{
				return this.checkBoxes;
			}
			set
			{
				this.checkBoxes = value;
				this.IsCheckBoxesSet = true;
			}
		}

		public int DisplayIndex
		{
			get
			{
				return this.displayIndex;
			}
			set
			{
				this.displayIndex = value;
			}
		}

		public bool FillsFreeSpace
		{
			get
			{
				return this.fillsFreeSpace;
			}
			set
			{
				this.fillsFreeSpace = value;
			}
		}

		public int FreeSpaceProportion
		{
			get
			{
				return this.freeSpaceProportion;
			}
			set
			{
				this.freeSpaceProportion = value;
				this.IsFreeSpaceProportionSet = true;
			}
		}

		public object[] GroupCutoffs
		{
			get
			{
				return this.groupCutoffs;
			}
			set
			{
				this.groupCutoffs = value;
			}
		}

		public string[] GroupDescriptions
		{
			get
			{
				return this.groupDescriptions;
			}
			set
			{
				this.groupDescriptions = value;
			}
		}

		public string GroupWithItemCountFormat
		{
			get
			{
				return this.groupWithItemCountFormat;
			}
			set
			{
				this.groupWithItemCountFormat = value;
			}
		}

		public string GroupWithItemCountSingularFormat
		{
			get
			{
				return this.groupWithItemCountSingularFormat;
			}
			set
			{
				this.groupWithItemCountSingularFormat = value;
			}
		}

		public bool Hyperlink
		{
			get
			{
				return this.hyperlink;
			}
			set
			{
				this.hyperlink = value;
			}
		}

		public string ImageAspectName
		{
			get
			{
				return this.imageAspectName;
			}
			set
			{
				this.imageAspectName = value;
			}
		}

		public bool IsEditable
		{
			get
			{
				return this.isEditable;
			}
			set
			{
				this.isEditable = value;
				this.IsEditableSet = true;
			}
		}

		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		public bool IsTileViewColumn
		{
			get
			{
				return this.isTileViewColumn;
			}
			set
			{
				this.isTileViewColumn = value;
			}
		}

		public int MaximumWidth
		{
			get
			{
				return this.maximumWidth;
			}
			set
			{
				this.maximumWidth = value;
			}
		}

		public int MinimumWidth
		{
			get
			{
				return this.minimumWidth;
			}
			set
			{
				this.minimumWidth = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				this.textAlign = value;
				this.IsTextAlignSet = true;
			}
		}

		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		public bool TriStateCheckBoxes
		{
			get
			{
				return this.triStateCheckBoxes;
			}
			set
			{
				this.triStateCheckBoxes = value;
				this.IsTriStateCheckBoxesSet = true;
			}
		}

		public bool UseInitialLetterForGroup
		{
			get
			{
				return this.useInitialLetterForGroup;
			}
			set
			{
				this.useInitialLetterForGroup = value;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		private string aspectToStringFormat;

		private bool checkBoxes;

		internal bool IsCheckBoxesSet;

		private int displayIndex = -1;

		private bool fillsFreeSpace;

		private int freeSpaceProportion;

		internal bool IsFreeSpaceProportionSet;

		private object[] groupCutoffs;

		private string[] groupDescriptions;

		private string groupWithItemCountFormat;

		private string groupWithItemCountSingularFormat;

		private bool hyperlink;

		private string imageAspectName;

		private bool isEditable = true;

		internal bool IsEditableSet;

		private bool isVisible = true;

		private bool isTileViewColumn;

		private int maximumWidth = -1;

		private int minimumWidth = -1;

		private string name;

		private HorizontalAlignment textAlign;

		internal bool IsTextAlignSet;

		private string tag;

		private string title;

		private string toolTipText;

		private bool triStateCheckBoxes;

		internal bool IsTriStateCheckBoxesSet;

		private bool useInitialLetterForGroup;

		private int width = 150;
	}
}
