using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCS;
using CCS.DB;

namespace SimpleDataBaseDemo
{
    public partial class Form1 : Form
    {
        private OutInfoDelegate OutInfoing;
        private OutBufferDelegate OutBuffering;

        public Form1()
        {
            InitializeComponent();

            OutInfoing = new OutInfoDelegate(OutInfoWin);
            CsInterinfo.OutInfoed += new OutInfoDelegate(OutInfoEvent);
        }

        private void OutInfoEvent(OutInfoType infoType, string info)
        {
            this.Invoke(OutInfoing, infoType, info);
        }

        private void OutInfoWin(OutInfoType infoType, string info)
        {
            this.InfoTextBox.AppendText(info + "\r\n\r\n");
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sDBType = "SQLSERVER";
            string sDBConn = this.richTextBox2.Text;
            CsDBManager.defaultDb(sDBType, sDBConn);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CsInterinfo.OutInfoed -= OutInfoEvent;
        }

        /// <summary>
        /// 运行SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int iResult = CsDBManager.ExcuteSQl(this.richTextBox3.Text);
            OutInfoWin(OutInfoType.Prompt, "ExcuteSQL Result: " + Convert.ToString(iResult));
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = CsDBManager.GetDataSetBySQl(this.richTextBox3.Text);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StringBuilder sRow = new StringBuilder();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    sRow.Append(dc.ColumnName);
                    sRow.Append(":");
                    sRow.Append(dr[dc.ColumnName].ToString());
                    sRow.Append(",");
                }
                OutInfoWin(OutInfoType.Prompt, sRow.ToString());
            }
        }

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            int iResult = CsDBManager.ExcuteSQl(this.richTextBox4.Text);
            OutInfoWin(OutInfoType.Prompt, "ExcuteSQL Result: " + Convert.ToString(iResult));
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            int iResult = CsDBManager.ExcuteSQl(this.richTextBox5.Text);
            OutInfoWin(OutInfoType.Prompt, "ExcuteSQL Result: " + Convert.ToString(iResult));
        }
    }
}
