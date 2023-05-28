using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class EditorRegistry
	{
		public EditorRegistry()
		{
			this.InitializeStandardTypes();
		}

		private void InitializeStandardTypes()
		{
			this.Register(typeof(bool), typeof(BooleanCellEditor));
			this.Register(typeof(short), typeof(IntUpDown));
			this.Register(typeof(int), typeof(IntUpDown));
			this.Register(typeof(long), typeof(IntUpDown));
			this.Register(typeof(ushort), typeof(UintUpDown));
			this.Register(typeof(uint), typeof(UintUpDown));
			this.Register(typeof(ulong), typeof(UintUpDown));
			this.Register(typeof(float), typeof(FloatCellEditor));
			this.Register(typeof(double), typeof(FloatCellEditor));
			this.Register(typeof(DateTime), (object model, OLVColumn column, object value) => new DateTimePicker
			{
				Format = DateTimePickerFormat.Short
			});
			this.Register(typeof(bool), (object model, OLVColumn column, object value) => new BooleanCellEditor2
			{
				ThreeState = column.TriStateCheckBoxes
			});
		}

		public void Register(Type type, Type controlType)
		{
			this.Register(type, (object model, OLVColumn column, object value) => controlType.InvokeMember(EditorRegistry.<>c__DisplayClass5.getString_0(107403130), BindingFlags.CreateInstance, null, null, null) as Control);
		}

		public void Register(Type type, EditorCreatorDelegate creator)
		{
			this.creatorMap[type] = creator;
		}

		public void RegisterDefault(EditorCreatorDelegate creator)
		{
			this.defaultCreator = creator;
		}

		public void RegisterFirstChance(EditorCreatorDelegate creator)
		{
			this.firstChanceCreator = creator;
		}

		public Control GetEditor(object model, OLVColumn column, object value)
		{
			if (this.firstChanceCreator != null)
			{
				Control control = this.firstChanceCreator(model, column, value);
				if (control != null)
				{
					return control;
				}
			}
			Type type = (value == null) ? column.DataType : value.GetType();
			if (type != null && this.creatorMap.ContainsKey(type))
			{
				Control control = this.creatorMap[type](model, column, value);
				if (control != null)
				{
					return control;
				}
			}
			if (value != null && value.GetType().IsEnum)
			{
				return this.CreateEnumEditor(value.GetType());
			}
			if (this.defaultCreator != null)
			{
				return this.defaultCreator(model, column, value);
			}
			return null;
		}

		protected Control CreateEnumEditor(Type type)
		{
			return new EnumCellEditor(type);
		}

		private EditorCreatorDelegate firstChanceCreator;

		private EditorCreatorDelegate defaultCreator;

		private Dictionary<Type, EditorCreatorDelegate> creatorMap = new Dictionary<Type, EditorCreatorDelegate>();
	}
}
