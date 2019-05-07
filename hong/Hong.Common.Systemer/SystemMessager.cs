using System;
using System.Collections.Generic;
using System.Text;
using Hong.Common.Stringer;

namespace Hong.Common.Systemer
{
	public enum OutInfoType
	{
		General,
		Error,
		Exception,
		Prompt
	}

	public enum OutBufferType
	{
		SerialReceived,
		SerialSent,
		TcpServerReceived,
		TcpServerSent,
		TcpClientReceived,
		TcpClientSent,
		UdpReceived,
		UdpSent
	}

	public delegate void OutInfoDelegate(OutInfoType infoType, string info);

	public delegate void OutBufferDelegate(OutBufferType bufferType, string bufferStr);

	public static class SystemMessager
	{
		static SystemMessager()
		{
		}

		public static event OutInfoDelegate OutInfoed;
		private static void OutInfoedEvents(OutInfoType infoType, string info)
		{
			if (OutInfoed != null)
			{
				OutInfoed(infoType, info);
			}
		}

		public static void OutInfoPrompt(string info)
		{
			OutInfoedEvents(OutInfoType.Prompt, "提示："+info);
		}

		public static void OutInfoError(string info)
		{
			OutInfoedEvents(OutInfoType.Error, "错误："+info);
		}

		public static void OutInfoException(string info)
		{
			OutInfoedEvents(OutInfoType.Exception, "异常："+info);
		}

		public static void OutInfoGeneral(string info)
		{
			OutInfoedEvents(OutInfoType.General, "系统信息："+info);
		}

		public static event OutBufferDelegate OutBuffered;
		private static void OutBufferedEvents(OutBufferType bufferType, string bufferStr)
		{
			if (OutBuffered != null)
			{
				OutBuffered(bufferType, bufferStr);
			}
		}

		public static void OutBuffer(OutBufferType bufferType, byte[] buf, int index, int count)
		{
			if (OutBuffered != null)
			{
				//string bufferStr = StringHexer.EncodeHexString(buf, index, count);
				string bufferStr = Encoding.Default.GetString(buf, index, count);
				OutBuffered(bufferType, bufferStr);
			}
		}

		public static void OutBuffer(OutBufferType bufferType, byte[] buf)
		{
			if (buf != null)
			{
				OutBuffer(bufferType, buf, 0, buf.Length);
			}
		}
	}
}
