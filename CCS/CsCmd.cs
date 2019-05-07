using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CCS
{
    /// <summary>
    /// 命令行类
    /// 
    /// <para>使用方法：</para>
    ///  <para>1.创建类实例</para>
    /// <para>2.调用 Initialize() 接口初始化</para>
    /// <para>3.增加输出响应接口，例如：</para>
    /// <para>  (1).增加输出事件响应：MyCmd.OutputDataReceived += new DataReceivedEventHandler(MyCmd_OutputDataReceived);</para>
    /// <para>  (2).定义事件处理接口：private void MyCmd_OutputDataReceived(object sender, DataReceivedEventArgs e)</para>
    /// <para>4.调用 Input() 接口执行命令行语句</para>
    /// </summary>
    public class CsCmd
    {
        private Process _v_p = null;
        public delegate void OnOutputDataReceived(object sender, DataReceivedEventArgs e);
        public event OnOutputDataReceived OutputDataReceived = null;

        /// <summary>
        /// 初始化实例
        /// </summary>
        public void Initialize()
        {
            _v_p = new Process();
            _v_p.StartInfo.FileName = "cmd.exe";
            _v_p.StartInfo.UseShellExecute = false;
            _v_p.StartInfo.RedirectStandardError = true;
            _v_p.StartInfo.RedirectStandardInput = true;
            _v_p.StartInfo.RedirectStandardOutput = true;
            _v_p.StartInfo.CreateNoWindow = true;
            _v_p.OutputDataReceived += new DataReceivedEventHandler(_v_p_OutputDataReceived);
            _v_p.Start();
            _v_p.BeginOutputReadLine();
        }
        /// <summary>
        /// 执行命令行语句
        /// </summary>
        /// <param name="input">命令行语句</param>
        public void Input(string input)
        {
            if (_v_p != null) _v_p.StandardInput.WriteLine(input); 
        }
        /// <summary>
        /// 命令行输出回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _v_p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e == null) return;
            if(string.IsNullOrEmpty(e.Data)) return;
            if (OutputDataReceived != null) OutputDataReceived(sender, e);
        }
    }
}
