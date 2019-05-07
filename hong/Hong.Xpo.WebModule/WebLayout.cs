using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Web.UI.WebControls;
using Hong.Profile.Base;

namespace Hong.Xpo.WebModule
{
    public class WebLayout : UiLayout
    {
        public const string DefaultUnit = "";
        private const int DefaultRowIndex = -1;
        private const int DefaultColumnIndex = -1;
        private const int DefaultRowSpan = -1;
        private const int DefaultColumnSpan = -1;

        public WebLayout(CellerBase celler)
            : base(celler)
        {
            _componentHeight = AddVariable<string>("ComponentHeight", DefaultUnit);
            _componentWidth = AddVariable<string>("ComponentWidth", DefaultUnit);
            _cellHeight = AddVariable<string>("CellHeight", DefaultUnit);
            _cellWidth = AddVariable<string>("CellWidth", DefaultUnit);
            _rowIndex = AddVariable<int>("RowIndex", DefaultRowIndex);
            _columnIndex = AddVariable<int>("ColumnIndex", DefaultColumnIndex);
            _rowSpan = AddVariable<int>("RowSpan", DefaultRowSpan);
            _columnSpan = AddVariable<int>("ColumnSpan", DefaultColumnSpan);

            _tableCell = new TableCell();
            _tableCell.HorizontalAlign = HorizontalAlign.Left;
            _tableCell.VerticalAlign = VerticalAlign.Middle;
            _tableCell.BorderStyle = BorderStyle.Dashed;
            _tableCell.BorderWidth = 1;
        }

        private Unit GetUnit(string value)
        {
            if (value != DefaultUnit)
            {
                if (value.LastIndexOf('%') > 0)
                {
                    return Unit.Percentage(Convert.ToInt32(value.Remove(value.Length-1)));
                }
                else
                {
                    return new Unit(Convert.ToInt32(value));
                }
            }
            else
            {
                return Unit.Empty;
            }
        }

        protected override void VariablesChanged(object sender, VariableListChangedArgs e)
        {
            if (e.Variable == _componentHeight || e.Variable == _componentWidth)
            {
                RaiseChanged(e);
            }
            else if (e.Variable == _rowSpan && _rowSpan.Value != DefaultRowSpan)
            {
                _tableCell.RowSpan = RowSpan.Value;
            }
            else if (e.Variable == _columnSpan && _columnSpan.Value != DefaultColumnSpan)
            {
                _tableCell.ColumnSpan = ColumnSpan.Value;
            }
            else if (e.Variable == _cellHeight && _cellHeight.Value != DefaultUnit)
            {
                _tableCell.Height = CellHeight;
            }
            else if (e.Variable == _cellWidth && _cellWidth.Value != DefaultUnit)
            {
                _tableCell.Width = CellWidth;
            }
        }

        #region 容器里面的控件Layout定义

        private VariableItem<string> _componentHeight;

        private VariableItem<string> _componentWidth;

        public Unit ComponentHeight
        {
            get
            {
                return GetUnit(_componentHeight.Value);
            }
        }

        public Unit ComponentWidth
        {
            get
            {
                return GetUnit(_componentWidth.Value);
            }
        }

        #endregion

        #region 容器方面的Layout定义

        private TableCell _tableCell;
        public TableCell TableCell
        {
            get
            {
                return _tableCell;
            }
        }

        private VariableItem<int> _rowIndex;
        public VariableItem<int> RowIndex
        {
            get
            {
                return _rowIndex;
            }
        }

        private VariableItem<int> _columnIndex;
        public VariableItem<int> ColumnIndex
        {
            get
            {
                return _columnIndex;
            }
        }

        private VariableItem<string> _cellWidth;

        private VariableItem<string> _cellHeight;

        private VariableItem<int> _rowSpan;
        public VariableItem<int> RowSpan
        {
            get
            {
                return _rowSpan;
            }
        }

        private VariableItem<int> _columnSpan;
        public VariableItem<int> ColumnSpan
        {
            get
            {
                return _columnSpan;
            }
        }

        public Unit CellHeight
        {
            get
            {
                return GetUnit(_cellHeight.Value);
            }
        }

        public Unit CellWidth
        {
            get
            {
                return GetUnit(_cellWidth.Value);
            }
        }

        internal bool IsInside(int rowIndex, int columnIndex)
        {
            if (((rowIndex >= RowIndex.Value) && (rowIndex <= RowIndex.Value + RowSpan.Value - 1))
                && ((columnIndex >= ColumnIndex.Value) && (columnIndex <= ColumnIndex.Value + ColumnSpan.Value - 1)))
            {
                return true;
            }
            return false;
        }

        internal int ColumnEndIndex()
        {
            return ColumnIndex.Value + ColumnSpan.Value - 1;
        }

        internal int RowEndIndex()
        {
            return RowIndex.Value + RowSpan.Value - 1;
        }

        internal void SetCellPoint(string cellWidth, string cellHeight)
        {
            _cellWidth.Value = cellWidth;
            _cellHeight.Value = cellHeight;
        }

        internal void SetComponentPoint(string componentWidth, string componentHeight)
        {
            _componentWidth.Value = componentWidth;
            _componentHeight.Value = componentHeight;
        }

        #endregion
    }
}
