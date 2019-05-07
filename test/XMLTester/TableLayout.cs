using System;
using System.Collections.Generic;
using System.Text;

namespace XMLTester
{
    public class TableLayout
    {
        public TableLayout(int columnCount)
        {
            _columnCount = columnCount;
            _rows = new List<bool[]>();
            AddRowCount(1);
        }

        private List<bool[]> _rows;

        private int _columnCount;

        public void RequestTableSite(int rowSpan, int columnSpan, out int rowIndex, out int columnIndex)
        {
            rowIndex = 0;
            columnIndex = 0;
            AddRowCount(rowSpan);
            int begini = -1;
            int beginj = 0;
            int endi = -1;
            int x = -1;
            for (int i = 0; i < _rows.Count; i++)
            {
                bool[] row = _rows[i];
                if (IsValidRow(row, columnSpan, beginj, out x))
                {
                    if (begini == -1)
                    {
                        begini = i;
                    }
                    beginj = x;
                }
                else
                {
                    if (begini != -1)
                    {
                        if (i - begini + 1 >= rowSpan)
                        {
                            endi = i;
                            break;
                        }
                        else
                        {
                            begini = -1;
                            endi = -1;
                        }
                    }
                    else
                    {
                        begini = -1;
                        endi = -1;
                    }
                }
            }
            rowIndex = begini;
            columnIndex = beginj;
            SetTableTrue(rowIndex, columnIndex, rowSpan, columnSpan);
        }

        private void SetTableTrue(int rowIndex, int columnIndex, int rowSpan, int columnSpan)
        {
            for (int i = 0; i < rowSpan; i++)
            {
                if (rowIndex + i > _rows.Count - 1)
                {
                    return;
                }
                bool[] row = _rows[rowIndex + i];
                for (int j = 0; j < columnSpan; j++)
                {
                    if (columnIndex + j > row.Length - 1)
                    {
                        break;
                    }
                    row[columnIndex + j] = true;
                }
            }
        }

        private bool IsValidRow(bool[] row, int columnSpan, int begin, out int x)
        {
            int begini = -1;
            int endi = -1;
            x = -1;
            for (int i = begin; i < row.Length; i++)
            {
                if (row[i] == false)
                {
                    if (begini == -1)
                    {
                        begini = i;
                        endi = -1;
                    }
                }
                else
                {
                    if (begini != -1)
                    {
                        if (i - begini + 1 >= columnSpan)
                        {
                            x = begini;
                            return true;
                        }
                        else
                        {
                            begini = -1;
                            endi = -1;
                        }
                    }
                    else
                    {
                        begini = -1;
                        endi = -1;
                    }
                }
            }
            if (begini == -1)
            {
                return false;
            }
            if (endi == -1)
            {
                if (row.Length - begini >= columnSpan)
                {
                    x = begini;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void AddRowCount(int rowSpan)
        {
            for (int i = 0; i < rowSpan; i++)
            {
                bool[] row = new bool[_columnCount];
                for (int j = 0; j < row.Length; j++)
                {
                    row[j] = false;
                }
                _rows.Add(row);
            }
        }
    }
}
