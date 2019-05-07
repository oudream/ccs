using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using InfoQuick.SinoVoice.Tts;
using Microsoft.Win32;

namespace Hong.Audio.Speech
{
    /// <summary>
    /// 捷通华声多语种语音合成平台（jTTS 5.0）
    /// </summary>
    public class SpeechWrapper
    {
        private static SpeechWrapper _default = null;
        private static bool _initialized = false;

        static SpeechWrapper()
        {
            //写入注册信息
            string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\SinoVoice\jTTS4_Professional";
            string path = Application.StartupPath + @"\Speech\XiaoKun";
            Registry.SetValue(key, "LibPath4", path);
            Registry.SetValue(key, "SerialNo", "56CCB31A7362F2DF");

            //初始化
            int iErr = Jtts.jTTS_Init(null, null);
            if (Jtts.ERR_NONE == iErr || Jtts.ERR_ALREADYINIT == iErr)
            {
                _initialized = true;
            }
            //配置
            Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
            iErr = Jtts.jTTS_Get(out config);
            config.nCodePage = (ushort)Encoding.Default.CodePage;
            Jtts.jTTS_Set(ref config);
        }

        public SpeechWrapper()
        {
        }

        ~SpeechWrapper()
        {
            Jtts.jTTS_End();
        }

        public static SpeechWrapper Default()
        {
            if (_default == null)
            {
                _default = new SpeechWrapper();
            }
            return _default;
        }

        public void Speak(string text)
        {
            if (!_initialized)
            {
                return;
            }
            int iErr = Jtts.jTTS_Play(text, 0);
            //if (Jtts.ERR_NONE != iErr)
            //{
            //    JttsErrMsg(iErr);
            //}
        }

        public void Setting()
        {
            int iErr = 0;

            Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
            iErr = Jtts.jTTS_Get(out config);
            DlgSetup dlg = new DlgSetup();
            //Set data
            dlg.SetJttsConfig(config);
            //dlg.FileFormat = iFileFormat;
            //dlg.FileHead = iFileHead;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                dlg.GetJttsConfig(ref config);
                Jtts.jTTS_Set(ref config);
                //iFileFormat = dlg.FileFormat;
                //iFileHead = dlg.FileHead;
            }
            dlg.Dispose();
        }
    }
}
