using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class CellEditKeyEngine
	{
		public void SetKeyBehaviour(Keys key, CellEditCharacterBehaviour normalBehaviour, CellEditAtEdgeBehaviour atEdgeBehaviour)
		{
			this.CellEditKeyMap[key] = normalBehaviour;
			this.CellEditKeyAtEdgeBehaviourMap[key] = atEdgeBehaviour;
		}

		public bool HandleKey(ObjectListView olv, Keys keyData)
		{
			if (olv == null)
			{
				throw new ArgumentNullException(CellEditKeyEngine.getString_0(107315624));
			}
			CellEditCharacterBehaviour behaviour;
			if (!this.CellEditKeyMap.TryGetValue(keyData, out behaviour))
			{
				return false;
			}
			this.ListView = olv;
			switch (behaviour)
			{
			case CellEditCharacterBehaviour.Ignore:
				break;
			case CellEditCharacterBehaviour.ChangeColumnLeft:
			case CellEditCharacterBehaviour.ChangeColumnRight:
				this.HandleColumnChange(keyData, behaviour);
				break;
			case CellEditCharacterBehaviour.ChangeRowUp:
			case CellEditCharacterBehaviour.ChangeRowDown:
				this.HandleRowChange(keyData, behaviour);
				break;
			case CellEditCharacterBehaviour.CancelEdit:
				this.HandleCancelEdit();
				break;
			case CellEditCharacterBehaviour.EndEdit:
				this.HandleEndEdit();
				break;
			default:
				return this.HandleCustomVerb(keyData, behaviour);
			}
			return true;
		}

		protected ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				this.listView = value;
			}
		}

		protected OLVListItem ItemBeingEdited
		{
			get
			{
				return this.ListView.cellEditEventArgs.ListViewItem;
			}
		}

		protected int SubItemIndexBeingEdited
		{
			get
			{
				return this.ListView.cellEditEventArgs.SubItemIndex;
			}
		}

		protected IDictionary<Keys, CellEditCharacterBehaviour> CellEditKeyMap
		{
			get
			{
				if (this.cellEditKeyMap == null)
				{
					this.InitializeCellEditKeyMaps();
				}
				return this.cellEditKeyMap;
			}
			set
			{
				this.cellEditKeyMap = value;
			}
		}

		protected IDictionary<Keys, CellEditAtEdgeBehaviour> CellEditKeyAtEdgeBehaviourMap
		{
			get
			{
				if (this.cellEditKeyAtEdgeBehaviourMap == null)
				{
					this.InitializeCellEditKeyMaps();
				}
				return this.cellEditKeyAtEdgeBehaviourMap;
			}
			set
			{
				this.cellEditKeyAtEdgeBehaviourMap = value;
			}
		}

		protected void InitializeCellEditKeyMaps()
		{
			this.cellEditKeyMap = new Dictionary<Keys, CellEditCharacterBehaviour>();
			this.cellEditKeyMap[Keys.Escape] = CellEditCharacterBehaviour.CancelEdit;
			this.cellEditKeyMap[Keys.Return] = CellEditCharacterBehaviour.EndEdit;
			this.cellEditKeyMap[Keys.Return] = CellEditCharacterBehaviour.EndEdit;
			this.cellEditKeyMap[Keys.Tab] = CellEditCharacterBehaviour.ChangeColumnRight;
			this.cellEditKeyMap[Keys.LButton | Keys.Back | Keys.Shift] = CellEditCharacterBehaviour.ChangeColumnLeft;
			this.cellEditKeyMap[Keys.LButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditCharacterBehaviour.ChangeColumnLeft;
			this.cellEditKeyMap[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditCharacterBehaviour.ChangeColumnRight;
			this.cellEditKeyMap[Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditCharacterBehaviour.ChangeRowUp;
			this.cellEditKeyMap[Keys.Back | Keys.Space | Keys.Alt] = CellEditCharacterBehaviour.ChangeRowDown;
			this.cellEditKeyAtEdgeBehaviourMap = new Dictionary<Keys, CellEditAtEdgeBehaviour>();
			this.cellEditKeyAtEdgeBehaviourMap[Keys.Tab] = CellEditAtEdgeBehaviour.Wrap;
			this.cellEditKeyAtEdgeBehaviourMap[Keys.LButton | Keys.Back | Keys.Shift] = CellEditAtEdgeBehaviour.Wrap;
			this.cellEditKeyAtEdgeBehaviourMap[Keys.LButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditAtEdgeBehaviour.Wrap;
			this.cellEditKeyAtEdgeBehaviourMap[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditAtEdgeBehaviour.Wrap;
			this.cellEditKeyAtEdgeBehaviourMap[Keys.RButton | Keys.MButton | Keys.Space | Keys.Alt] = CellEditAtEdgeBehaviour.ChangeColumn;
			this.cellEditKeyAtEdgeBehaviourMap[Keys.Back | Keys.Space | Keys.Alt] = CellEditAtEdgeBehaviour.ChangeColumn;
		}

		protected void HandleEndEdit()
		{
			this.ListView.PossibleFinishCellEditing();
		}

		protected void HandleCancelEdit()
		{
			this.ListView.CancelCellEdit();
		}

		protected bool HandleCustomVerb(Keys keyData, CellEditCharacterBehaviour behaviour)
		{
			return false;
		}

		protected void HandleRowChange(Keys keyData, CellEditCharacterBehaviour behaviour)
		{
			if (!this.ListView.PossibleFinishCellEditing())
			{
				return;
			}
			OLVListItem itemBeingEdited = this.ItemBeingEdited;
			int num = this.SubItemIndexBeingEdited;
			bool flag = behaviour == CellEditCharacterBehaviour.ChangeRowUp;
			OLVListItem adjacentItemOrNull = this.GetAdjacentItemOrNull(itemBeingEdited, flag);
			if (adjacentItemOrNull != null)
			{
				this.StartCellEditIfDifferent(adjacentItemOrNull, num);
				return;
			}
			CellEditAtEdgeBehaviour cellEditAtEdgeBehaviour = CellEditAtEdgeBehaviour.Wrap;
			this.CellEditKeyAtEdgeBehaviourMap.TryGetValue(keyData, out cellEditAtEdgeBehaviour);
			switch (cellEditAtEdgeBehaviour)
			{
			case CellEditAtEdgeBehaviour.Ignore:
			case CellEditAtEdgeBehaviour.ChangeRow:
				break;
			case CellEditAtEdgeBehaviour.Wrap:
				adjacentItemOrNull = this.GetAdjacentItemOrNull(null, flag);
				this.StartCellEditIfDifferent(adjacentItemOrNull, num);
				return;
			case CellEditAtEdgeBehaviour.ChangeColumn:
			{
				List<OLVColumn> editableColumnsInDisplayOrder = this.EditableColumnsInDisplayOrder;
				int num2 = Math.Max(0, editableColumnsInDisplayOrder.IndexOf(this.ListView.GetColumn(num)));
				if (flag)
				{
					num2 = (editableColumnsInDisplayOrder.Count + num2 - 1) % editableColumnsInDisplayOrder.Count;
				}
				else
				{
					num2 = (num2 + 1) % editableColumnsInDisplayOrder.Count;
				}
				num = editableColumnsInDisplayOrder[num2].Index;
				adjacentItemOrNull = this.GetAdjacentItemOrNull(null, flag);
				this.StartCellEditIfDifferent(adjacentItemOrNull, num);
				break;
			}
			case CellEditAtEdgeBehaviour.EndEdit:
				this.ListView.PossibleFinishCellEditing();
				return;
			default:
				return;
			}
		}

		protected void HandleColumnChange(Keys keyData, CellEditCharacterBehaviour behaviour)
		{
			if (!this.ListView.PossibleFinishCellEditing())
			{
				return;
			}
			if (this.ListView.View != View.Details)
			{
				return;
			}
			List<OLVColumn> editableColumnsInDisplayOrder = this.EditableColumnsInDisplayOrder;
			OLVListItem olvi = this.ItemBeingEdited;
			int num = Math.Max(0, editableColumnsInDisplayOrder.IndexOf(this.ListView.GetColumn(this.SubItemIndexBeingEdited)));
			bool flag;
			if (((flag = (behaviour == CellEditCharacterBehaviour.ChangeColumnLeft)) && num == 0) || (!flag && num == editableColumnsInDisplayOrder.Count - 1))
			{
				CellEditAtEdgeBehaviour cellEditAtEdgeBehaviour = CellEditAtEdgeBehaviour.Wrap;
				this.CellEditKeyAtEdgeBehaviourMap.TryGetValue(keyData, out cellEditAtEdgeBehaviour);
				switch (cellEditAtEdgeBehaviour)
				{
				case CellEditAtEdgeBehaviour.Ignore:
					return;
				case CellEditAtEdgeBehaviour.Wrap:
				case CellEditAtEdgeBehaviour.ChangeRow:
					if (cellEditAtEdgeBehaviour == CellEditAtEdgeBehaviour.ChangeRow)
					{
						olvi = this.GetAdjacentItem(olvi, flag && num == 0);
					}
					if (flag)
					{
						num = editableColumnsInDisplayOrder.Count - 1;
					}
					else
					{
						num = 0;
					}
					break;
				case CellEditAtEdgeBehaviour.EndEdit:
					this.HandleEndEdit();
					return;
				}
			}
			else if (flag)
			{
				num--;
			}
			else
			{
				num++;
			}
			int index = editableColumnsInDisplayOrder[num].Index;
			this.StartCellEditIfDifferent(olvi, index);
		}

		protected void StartCellEditIfDifferent(OLVListItem olvi, int subItemIndex)
		{
			if (this.ItemBeingEdited == olvi && this.SubItemIndexBeingEdited == subItemIndex)
			{
				return;
			}
			this.ListView.EnsureVisible(olvi.Index);
			this.ListView.StartCellEdit(olvi, subItemIndex);
		}

		protected OLVListItem GetAdjacentItemOrNull(OLVListItem olvi, bool up)
		{
			if (!up)
			{
				return this.ListView.GetNextItem(olvi);
			}
			return this.ListView.GetPreviousItem(olvi);
		}

		protected OLVListItem GetAdjacentItem(OLVListItem olvi, bool up)
		{
			return this.GetAdjacentItemOrNull(olvi, up) ?? this.GetAdjacentItemOrNull(null, up);
		}

		protected List<OLVColumn> EditableColumnsInDisplayOrder
		{
			get
			{
				List<OLVColumn> list = new List<OLVColumn>();
				foreach (OLVColumn olvcolumn in this.ListView.ColumnsInDisplayOrder)
				{
					if (olvcolumn.IsEditable)
					{
						list.Add(olvcolumn);
					}
				}
				return list;
			}
		}

		static CellEditKeyEngine()
		{
			Strings.CreateGetStringDelegate(typeof(CellEditKeyEngine));
		}

		private ObjectListView listView;

		private IDictionary<Keys, CellEditCharacterBehaviour> cellEditKeyMap;

		private IDictionary<Keys, CellEditAtEdgeBehaviour> cellEditKeyAtEdgeBehaviourMap;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
