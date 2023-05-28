using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware.Design
{
	public sealed class ObjectListViewDesigner : ControlDesigner
	{
		public override void Initialize(IComponent component)
		{
			Type type = Type.GetType(ObjectListViewDesigner.getString_0(107314230)) ?? Type.GetType(ObjectListViewDesigner.getString_0(107314181));
			if (type == null)
			{
				throw new ArgumentException(ObjectListViewDesigner.getString_0(107314491));
			}
			this.listViewDesigner = (ControlDesigner)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public, null, null, null);
			this.designerFilter = this.listViewDesigner;
			this.listViewDesignGetHitTest = type.GetMethod(ObjectListViewDesigner.getString_0(107314446), BindingFlags.Instance | BindingFlags.NonPublic);
			this.listViewDesignWndProc = type.GetMethod(ObjectListViewDesigner.getString_0(107314461), BindingFlags.Instance | BindingFlags.NonPublic);
			TypeDescriptor.CreateAssociation(component, this.listViewDesigner);
			IServiceContainer serviceContainer = (IServiceContainer)component.Site;
			if (serviceContainer != null && this.GetService(typeof(DesignerCommandSet)) == null)
			{
				serviceContainer.AddService(typeof(DesignerCommandSet), new ObjectListViewDesigner.CDDesignerCommandSet(this));
			}
			this.listViewDesigner.Initialize(component);
			base.Initialize(component);
			this.RemoveDuplicateDockingActionList();
		}

		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			this.listViewDesigner.InitializeNewComponent(defaultValues);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listViewDesigner != null)
			{
				this.listViewDesigner.Dispose();
			}
			base.Dispose(disposing);
		}

		private void RemoveDuplicateDockingActionList()
		{
			FieldInfo field = typeof(ControlDesigner).GetField(ObjectListViewDesigner.getString_0(107314416), BindingFlags.Instance | BindingFlags.NonPublic);
			if (field != null)
			{
				DesignerActionList designerActionList = (DesignerActionList)field.GetValue(this);
				if (designerActionList != null)
				{
					DesignerActionService designerActionService = (DesignerActionService)this.GetService(typeof(DesignerActionService));
					if (designerActionService != null)
					{
						designerActionService.Remove(this.Control, designerActionList);
					}
				}
			}
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			this.designerFilter.PreFilterProperties(properties);
			List<string> list = new List<string>(new string[]
			{
				ObjectListViewDesigner.getString_0(107314395),
				ObjectListViewDesigner.getString_0(107314406),
				ObjectListViewDesigner.getString_0(107314377),
				ObjectListViewDesigner.getString_0(107314328),
				ObjectListViewDesigner.getString_0(107314339),
				ObjectListViewDesigner.getString_0(107313782),
				ObjectListViewDesigner.getString_0(107313793)
			});
			foreach (object obj in properties.Keys)
			{
				string text = (string)obj;
				if (text.StartsWith(ObjectListViewDesigner.getString_0(107313744)))
				{
					list.Add(text);
				}
			}
			if (this.Control is TreeListView)
			{
				list.AddRange(new string[]
				{
					ObjectListViewDesigner.getString_0(107313763),
					ObjectListViewDesigner.getString_0(107313710),
					ObjectListViewDesigner.getString_0(107313677),
					ObjectListViewDesigner.getString_0(107313664),
					ObjectListViewDesigner.getString_0(107313635),
					ObjectListViewDesigner.getString_0(107313610),
					ObjectListViewDesigner.getString_0(107313561),
					ObjectListViewDesigner.getString_0(107314032)
				});
			}
			foreach (string key in list)
			{
				PropertyDescriptor value = TypeDescriptor.CreateProperty(typeof(ObjectListView), (PropertyDescriptor)properties[key], new Attribute[]
				{
					new BrowsableAttribute(false)
				});
				properties[key] = value;
			}
		}

		protected override void PreFilterEvents(IDictionary events)
		{
			base.PreFilterEvents(events);
			this.designerFilter.PreFilterEvents(events);
			List<string> list = new List<string>(new string[]
			{
				ObjectListViewDesigner.getString_0(107314003),
				ObjectListViewDesigner.getString_0(107314014),
				ObjectListViewDesigner.getString_0(107313993),
				ObjectListViewDesigner.getString_0(107313936),
				ObjectListViewDesigner.getString_0(107313955),
				ObjectListViewDesigner.getString_0(107313906),
				ObjectListViewDesigner.getString_0(107313877),
				ObjectListViewDesigner.getString_0(107313848)
			});
			if (this.Control is TreeListView)
			{
				list.AddRange(new string[]
				{
					ObjectListViewDesigner.getString_0(107313835),
					ObjectListViewDesigner.getString_0(107313262),
					ObjectListViewDesigner.getString_0(107313233),
					ObjectListViewDesigner.getString_0(107313204),
					ObjectListViewDesigner.getString_0(107313179),
					ObjectListViewDesigner.getString_0(107313146)
				});
			}
			foreach (string key in list)
			{
				EventDescriptor value = TypeDescriptor.CreateEvent(typeof(ObjectListView), (EventDescriptor)events[key], new Attribute[]
				{
					new BrowsableAttribute(false)
				});
				events[key] = value;
			}
		}

		protected override void PostFilterAttributes(IDictionary attributes)
		{
			this.designerFilter.PostFilterAttributes(attributes);
			base.PostFilterAttributes(attributes);
		}

		protected override void PostFilterEvents(IDictionary events)
		{
			this.designerFilter.PostFilterEvents(events);
			base.PostFilterEvents(events);
		}

		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actionLists = this.listViewDesigner.ActionLists;
				if (actionLists.Count > 0 && !(actionLists[0] is ObjectListViewDesigner.ListViewActionListAdapter))
				{
					actionLists[0] = new ObjectListViewDesigner.ListViewActionListAdapter(this, actionLists[0]);
				}
				return actionLists;
			}
		}

		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = new ArrayList(base.AssociatedComponents);
				arrayList.AddRange(this.listViewDesigner.AssociatedComponents);
				return arrayList;
			}
		}

		protected override bool GetHitTest(Point point)
		{
			return (bool)this.listViewDesignGetHitTest.Invoke(this.listViewDesigner, new object[]
			{
				point
			});
		}

		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 78)
			{
				if (msg != 8270)
				{
					base.WndProc(ref m);
					return;
				}
			}
			this.listViewDesignWndProc.Invoke(this.listViewDesigner, new object[]
			{
				m
			});
		}

		static ObjectListViewDesigner()
		{
			Strings.CreateGetStringDelegate(typeof(ObjectListViewDesigner));
		}

		protected ControlDesigner listViewDesigner;

		protected IDesignerFilter designerFilter;

		protected MethodInfo listViewDesignGetHitTest;

		protected MethodInfo listViewDesignWndProc;

		[NonSerialized]
		internal static GetString getString_0;

		private sealed class ListViewActionListAdapter : DesignerActionList
		{
			public ListViewActionListAdapter(ObjectListViewDesigner designer, DesignerActionList wrappedList) : base(wrappedList.Component)
			{
				this.designer = designer;
				this.wrappedList = wrappedList;
			}

			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection sortedActionItems = this.wrappedList.GetSortedActionItems();
				sortedActionItems.RemoveAt(2);
				sortedActionItems.RemoveAt(0);
				return sortedActionItems;
			}

			private void EditValue(ComponentDesigner componentDesigner, IComponent iComponent, string propertyName)
			{
				Type type = Type.GetType(ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107313156));
				type.InvokeMember(ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107313039), BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[]
				{
					componentDesigner,
					iComponent,
					propertyName
				});
			}

			private void SetValue(object target, string propertyName, object value)
			{
				TypeDescriptor.GetProperties(target)[propertyName].SetValue(target, value);
			}

			public void InvokeColumnsDialog()
			{
				this.EditValue(this.designer, base.Component, ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107316771));
			}

			public ImageList LargeImageList
			{
				get
				{
					return ((ListView)base.Component).LargeImageList;
				}
				set
				{
					this.SetValue(base.Component, ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107313058), value);
				}
			}

			public ImageList SmallImageList
			{
				get
				{
					return ((ListView)base.Component).SmallImageList;
				}
				set
				{
					this.SetValue(base.Component, ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107313549), value);
				}
			}

			public View View
			{
				get
				{
					return ((ListView)base.Component).View;
				}
				set
				{
					this.SetValue(base.Component, ObjectListViewDesigner.ListViewActionListAdapter.getString_0(107313496), value);
				}
			}

			static ListViewActionListAdapter()
			{
				Strings.CreateGetStringDelegate(typeof(ObjectListViewDesigner.ListViewActionListAdapter));
			}

			private ObjectListViewDesigner designer;

			private DesignerActionList wrappedList;

			[NonSerialized]
			internal static GetString getString_0;
		}

		private sealed class CDDesignerCommandSet : DesignerCommandSet
		{
			public CDDesignerCommandSet(ComponentDesigner componentDesigner)
			{
				this.componentDesigner = componentDesigner;
			}

			public override ICollection GetCommands(string name)
			{
				if (this.componentDesigner != null)
				{
					if (name.Equals(ObjectListViewDesigner.CDDesignerCommandSet.getString_0(107313489)))
					{
						return this.componentDesigner.Verbs;
					}
					if (name.Equals(ObjectListViewDesigner.CDDesignerCommandSet.getString_0(107313512)))
					{
						return this.componentDesigner.ActionLists;
					}
				}
				return base.GetCommands(name);
			}

			static CDDesignerCommandSet()
			{
				Strings.CreateGetStringDelegate(typeof(ObjectListViewDesigner.CDDesignerCommandSet));
			}

			private readonly ComponentDesigner componentDesigner;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
