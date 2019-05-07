using System;
using System.Collections.Generic;
using System.Text;
using Hong.Common.Systemer;
using Hong.Common.Stringer;
using Hong.Profile.Base;
using System.Reflection;
using System.Windows.Forms;

namespace Hong.Channel.Base
{
	public abstract class ChannelBase
	{
		public abstract OutBufferType BufferTypeReceived();

		public abstract OutBufferType BufferTypeSent();

		protected abstract Type ChannelConfigViewType();

		protected abstract ChannelConfig ChannelConfig();

        public abstract bool Connected
        {
            get;
            set;
        }

		//1）读线程来关闭时触发
		public event ConnectChangedDelegate ConnectChanged;
		protected void ConnectChangedEvents(bool oldConnected, bool newConnected)
		{
			_receivedByteCount = 0;
			_sentByteCount = 0;
			if (ConnectChanged != null)
			{
				ConnectChanged(oldConnected, newConnected);
			}
		}

		//1）读线程触发
		public event ChannelReadDelegate ReceivedData;
		protected int ReceivedDataEvents(byte[] buf, int index, int count)
		{
			if (buf == null)
			{
				return -1;
			}
			_receivedByteCount += count;
			//显示数据
			SystemMessager.OutBuffer(BufferTypeReceived(), buf, index, count);
			//发布数据
			if (ReceivedData != null)
			{
				return ReceivedData(buf, index, count);
			}
			else
			{
				return -1;
			}
		}

        public int WriteData(byte[] buf, int index, int count)
        {
			if (StringHexer.IsValidBuffer(buf, index, count) && Connected)
			{
				int sents = WriteDataImpl(buf, index, count);
				if (sents > 0)
				{
					_sentByteCount += sents;
					//显示数据
					SystemMessager.OutBuffer(BufferTypeSent(), buf, index, count);
				}
				return sents;
			}
			else
			{
				return -1;
			}
        }

		protected abstract int WriteDataImpl(byte[] buf, int index, int count);

		public int WriteData(byte[] buf)
		{
			if (buf != null)
			{
				return WriteData(buf, 0, buf.Length);
			}
			else
			{
				return -1;
			}
		}

		public int WriteText(string text)
		{
			if (text != null)
			{
				byte[] buf = Encoding.Default.GetBytes(text);
				return WriteData(buf);
			}
			else
			{
				return -1;
			}
		}

		public int WriteHexString(string hexString)
		{
			byte[] buf = StringHexer.DecodeHexString(hexString);
			if (buf != null && buf.Length > 0)
			{
				return WriteData(buf, 0, buf.Length);
			}
			else
			{
				return -1;
			}
 		}

		public bool ShowConfig()
		{
			Type type = this.ChannelConfigViewType();
			if (! type.IsSubclassOf(typeof(Form)))
			{
				return false;
			}
			Assembly asm = type.Assembly;
			Form form = (Form)asm.CreateInstance(type.FullName);
			IChannelConfigForm iform = form as IChannelConfigForm;
			if (iform == null)
			{
				return false;
			}
			try
			{
				ChannelConfig().Load();
				iform.ViewOut(ChannelConfig());
				DialogResult dr = form.ShowDialog();
				if (dr == DialogResult.OK)
				{
					if (Connected)
					{
						Connected = false;
					}
					if (!Connected)
					{
						iform.ViewIn(ChannelConfig());
						ChannelConfig().Save();
					}
				}
				else
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		private int _receivedByteCount;
		public int ReceivedByteCount
		{
			get
			{
				return _receivedByteCount;
			}
		}

		private int _sentByteCount;
		public int SentByteCount
		{
			get
			{
				return _sentByteCount;
			}
		}
}
}
