using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _layout = new TableLayout(2);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private TableLayout _layout;
        private void button1_Click(object sender, EventArgs e)
        {
            int columnIndex = -2, rowIndex=-2;
            _layout.RequestTableSite(Convert.ToInt32(this.numericUpDown1.Value), Convert.ToInt32(this.numericUpDown2.Value), out columnIndex, out rowIndex);
            this.label1.Text = columnIndex.ToString() + " - " + rowIndex.ToString();
        }

    }
}
