using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Hong.ChildSafeSystem.Module
{
	/// <summary>
	/// 接收指纹机消息的窗体
	/// </summary>
	class MessageForm : Form
	{
		public event EventHandler ReceiveMessage;

		public MessageForm()
		{
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			switch (m.Msg)
			{
				case (int)Message.WM_FPSMSG:
					{
						int value = m.WParam.ToInt32();
						if (Enum.IsDefined(typeof(Message), value))
						{
							Message message = (Message)Enum.Parse(typeof(Message), value.ToString());
							int data = m.LParam.ToInt32();

							MessageEventArgs args = new MessageEventArgs(message, data);
							ReceiveMessage(this, args);

						}
						else
						{
							base.WndProc(ref m);
						}

						break;
					}
				default:
					{
						base.WndProc(ref m);
						break;
					}
			}
		}
	}

	/// <summary>
	/// 动态库返回的消息
	/// </summary>
	/// <remarks>通过SetMainWnd(接收动态库消息的窗口句柄)函数设置,窗口自定义消息默认为WM_FPSMSG = 1024 + 800</remarks>
	internal enum Message
	{
		WM_FPSMSG = 1024 + 800,                //自定义消息
		FM_SHOW_STATUS = 0x00,                 //状态消息（无效）
		FM_SHOW_NEWIMAGE = 0x01,               //有新的指纹图像
		FM_CAPTURE_NEWIMAGE = 0x02,            //采集图像返回消息
		FM_ENROLL_COMPLETE = 0x03,             //登记返回消息
		FM_VERIFY_COMPLETE = 0x04,             //1:1识别返回消息
		FM_IDENTIFY_COMPLETE = 0x05,           //1:N识别返回消息

		FE_WAITING_FOR_FINGER_ON = 0x11,       //请求放置手指
		FE_FOUND_FINGER_ON = 0x12,             //手指已经放上
		FE_WAITING_FOR_FINGER_OFF = 0x13,      //请求手指拿开
		FE_FOUND_FINGER_OFF = 0x14,            //手指已经拿开
		FE_WAITING_FOR_FINGER_KEEP = 0x15,     //登记时，放同一手指
		FE_IMAGE_CAPTURED = 0x17,              //采集到了手指的图像
		FE_SENSOR_TIMEOUT = 0x1F,              //超时  
	}

	public class FingerprintWrapper
	{
		#region 成员

		private static FingerprintWrapper _default = null;

		private static MessageForm _form = null;
		private static bool _usable = false;

		//private static int MaxFingerprintSize = 7500;
		private static int MaxFingerprintTemplateSize = 16384;

		private static int _matSize;
		private static byte[] _matBuf;

		private FingerprintMode mode = FingerprintMode.Idle;

		#endregion 成员

		#region 函数
		protected FingerprintWrapper() { }

		static FingerprintWrapper()
		{
			_matSize = 0;
			_matBuf = new byte[MaxFingerprintTemplateSize];

			_form = new MessageForm();
			_form.Size = new Size(0, 0);
			_form.Location = new Point(-100, -100);
			_form.ReceiveMessage += new EventHandler(ReceiveMessage);
			_form.Visible = false;

			//设置接收指纹机消息的窗体,并初始指纹机动态库
			//DllWrapper.CloseTransaction();
			//DllWrapper.SetMainWnd(_form.Handle);
			//DllWrapper.InitAndReady();
			//_usable = (DllWrapper.InitTransaction() == 1);
			//{
			//    label1.Text = "打开设备失败";
			//}
		}

		/// <summary>
		/// 接收到指纹机发出的消息
		/// </summary>
		/// <param name="sender">转发指纹机消息的窗体</param>
		/// <param name="e">指纹机消息</param>
		static void ReceiveMessage(object sender, EventArgs e)
		{
			MessageEventArgs args = e as MessageEventArgs;
			Message message = args.Message;

			switch (message)
			{
				case Message.FM_SHOW_STATUS:
					break;
				case Message.FM_SHOW_NEWIMAGE:
					{
						Bitmap bmp = new Bitmap(256, 256);
						Graphics g = Graphics.FromImage(bmp);
						DllWrapper.DrawImage(g.GetHdc(), 0, 0);
						g.Dispose();

						Default().DoShowImage(bmp);

						break;
					}
				case Message.FM_CAPTURE_NEWIMAGE:
					{
						//string print = string.Empty;
						bool success = false;
						//StringBuilder sb = new StringBuilder(MaxFingerprintSize);
						//long size = 0;
						//DllWrapper.Base64_GetMatchTemplate(sb, ref size);
						//success = sb.Length > 0;
						DllWrapper.GetMatchTemplate(ref _matBuf[0], ref _matSize);
						success = (_matSize > 0 && _matBuf != null);
						//if (success)
						//{
						//print = sb.ToString();
						//bool result = false;
						//StringBuilder tmp = null;
						//DateTime start = DateTime.Now;
						//fingers.ForEach(delegate(StringBuilder item)
						//{
						//    if (DllWrapper.Base64_VerifyTemplateOneToOne(item, sb, 1) == 1)
						//    {
						//        result = true;
						//        tmp = item;
						//        return;
						//    }
						//});
						//DateTime stop = DateTime.Now;

						//if (DllWrapper.Base64_VerifyTemplateOneToOne(template, sb, 1) == 1)
						//if (result)
						//{
						//    label1.Text = string.Format("{0}  位置:{1}  用时:{2}毫秒", new object[] { "识别成功", fingers.IndexOf(tmp), stop.Subtract(start).Milliseconds });
						//}
						//else
						//{
						//    label1.Text = "识别失败";
						//}
						//}
						Default().DoCaptured(success, _matBuf, _matSize);
						Default().DoTip(FingerprintTip.Captured);
						break;
					}
				case Message.FM_ENROLL_COMPLETE:
					{
						bool success = (args.Data == 1);
						//string print = string.Empty;
						if (success)
						{
							//StringBuilder sb = new StringBuilder(MaxFingerprintTemplateSize);
							//long size = 0;
							//DllWrapper.Base64_GetReferenceTemplate(sb, ref size);
							//print = sb.ToString();
							DllWrapper.GetReferenceTemplate(ref _matBuf[0], ref _matSize);
						}

						Default().DoEnrolled(success, _matBuf, _matSize);

						Default().DoTip(FingerprintTip.Enrolled);

						break;
					}
				case Message.FM_VERIFY_COMPLETE:
					break;
				case Message.FM_IDENTIFY_COMPLETE:
					break;
				case Message.FE_WAITING_FOR_FINGER_ON:
				case Message.FE_FOUND_FINGER_ON:
				case Message.FE_WAITING_FOR_FINGER_OFF:
				case Message.FE_FOUND_FINGER_OFF:
				case Message.FE_WAITING_FOR_FINGER_KEEP:
				case Message.FE_IMAGE_CAPTURED:
					{
						int value = (int)message;
						if (Enum.IsDefined(typeof(FingerprintTip), value))
						{
							FingerprintTip tip = (FingerprintTip)Enum.Parse(typeof(FingerprintTip), value.ToString(), true);
							Default().DoTip(tip);
						}
						break;
					}
				case Message.FE_SENSOR_TIMEOUT:
					{
						Default().DoTimeout();
						break;
					}
				default:
					break;
			}
		}

		/// <summary>
		/// 默认的指纹机
		/// </summary>
		/// <returns></returns>
		public static FingerprintWrapper Default()
		{
			if (_default == null)
			{
				_default = new FingerprintWrapper();
			}
			return _default;
		}

		public bool VerifyTemplateOneToOne(ref byte RefVal, int RefSize, ref byte MatVal, int MatSize)
		{
			return (DllWrapper.VerifyTemplateOneToOne(ref RefVal, RefSize, ref MatVal, MatSize, 1) == 1);
		}

		public bool Base64_VerifyTemplateOneToOne(StringBuilder RefVal, StringBuilder MatVal)
		{
			int result = DllWrapper.Base64_VerifyTemplateOneToOne(RefVal, MatVal, 1);
			return (result == 1);
		}

		public void Capture()
		{
			SetMode(FingerprintMode.Capture);
			//DllWrapper.CaptureImages();
		}

		public void Enroll()
		{
			SetMode(FingerprintMode.Enroll);
		}

		public void Idle()
		{
			SetMode(FingerprintMode.Idle);
		}

		public void Start()
		{
			//设置接收指纹机消息的窗体,并初始指纹机动态库
			DllWrapper.SetMainWnd(_form.Handle);
			DllWrapper.InitAndReady();
			_usable = (DllWrapper.InitTransaction() == 1);
			//_usable = false;
			//DllWrapper.CloseTransaction();
			//_usable = (DllWrapper.InitTransaction() == 1);
		}

		public void Stop()
		{
			//if (DllWrapper.CloseTransaction() == 1)
			//{
			//    _usable = false;
			//}
		}

		private void SetMode(FingerprintMode fingerprintMode)
		{
			mode = fingerprintMode;
			switch (mode)
			{
				case FingerprintMode.Idle:
					break;
				case FingerprintMode.Capture:
					DllWrapper.CaptureImages();
					break;
				case FingerprintMode.Enroll:
					DllWrapper.EnrollTemplates();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 调用ShowImage事件
		/// </summary>
		/// <param name="bitmap">指纹图像</param>
		private void DoShowImage(System.Drawing.Bitmap bitmap)
		{
			if (ShowImage != null)
			{
				ShowImageEventArgs args = new ShowImageEventArgs(bitmap);
				ShowImage(this, args);
			}
		}

		/// <summary>
		/// 调用Captured事件
		/// </summary>
		/// <param name="fingerprint">指纹数据</param>
		private void DoCaptured(bool success, byte[] matBuf, int matSize)
		{
			if (Captured != null)
			{
				CapturedEventArgs args = new CapturedEventArgs(success, matBuf, matSize);
				Captured(this, args);
			}
		}

		/// <summary>
		/// 调用Tip事件
		/// </summary>
		/// <param name="tip">指纹机提示信息类型</param>
		private void DoTip(FingerprintTip tip)
		{
			if (Tip != null)
			{
				TipEventArgs args = new TipEventArgs(tip);
				Tip(this, args);
			}
		}

		/// <summary>
		/// 调用EnrollComplete事件
		/// </summary>
		/// <param name="success">登记指纹模板是否成功</param>
		/// <param name="fingerprint">指纹模板数据</param>
		private void DoEnrolled(bool success, byte[] matBuf, int matSize)
		{
			if (Enrolled != null)
			{
				EnrolledEventArgs args = new EnrolledEventArgs(success, matBuf, matSize);
				Enrolled(this, args);
			}
		}


		private void DoTimeout()
		{
			if (Timeout != null)
			{
				Timeout(this, new EventArgs());
			}
		}

		#endregion 函数

		#region 属性

		/// <summary>
		/// 指纹机是否可用
		/// </summary>
		public bool Usable { get { return _usable; } }

		public FingerprintMode Mode { get { return mode; } }

		#endregion 属性

		#region 事件

		/// <summary>
		/// 显示指纹机捕获图像的事件
		/// </summary>
		public event EventHandler ShowImage;

		/// <summary>
		/// 采集指纹的事件
		/// </summary>
		public event EventHandler Captured;

		/// <summary>
		/// 捕获指纹机提示信息的事件
		/// </summary>
		public event EventHandler Tip;

		/// <summary>
		/// 登记指纹模板成功的事件
		/// </summary>
		public event EventHandler Enrolled;

		public event EventHandler Timeout;

		#endregion 事件
	}

	/// <summary>
	/// 指纹机提示信息类型
	/// </summary>
	public enum FingerprintTip
	{
		/// <summary>
		/// 请求放置手指
		/// </summary>
		WaitingForFingerOn = 0x11,
		/// <summary>
		/// 手指已经放上
		/// </summary>
		FingerOn = 0x12,
		/// <summary>
		/// 请求手指拿开
		/// </summary>
		WaitingForFingerOff = 0x13,
		/// <summary>
		/// 手指已经拿开
		/// </summary>
		FingerOff = 0x14,
		/// <summary>
		/// 登记时，放同一手指
		/// </summary>
		WaitingForFingerKeep = 0x15,
		/// <summary>
		/// 采集到了手指的图像
		/// </summary>
		ImageCaptured = 0x17,
		/// <summary>
		/// 超时 
		/// </summary>
		SensorTimeout = 0x1f,
		/// <summary>
		/// 采集指纹图像结束
		/// </summary>
		Captured,
		/// <summary>
		/// 指纹认证结束
		/// </summary>
		Enrolled,
	}

	public enum FingerprintMode
	{
		/// <summary>
		/// 空闲状态
		/// </summary>
		Idle,
		/// <summary>
		/// 指纹获取状态
		/// </summary>
		Capture,
		/// <summary>
		/// 指纹认证状态
		/// </summary>
		Enroll,
	}

	/// <summary>
	/// 捕获指纹机提示信息的事件参数
	/// </summary>
	public class TipEventArgs : EventArgs
	{
		/// <summary>
		/// 指纹机提示信息类型
		/// </summary>
		public FingerprintTip Tip { get; set; }

		public TipEventArgs(FingerprintTip tip)
		{
			this.Tip = tip;
		}

		public override string ToString()
		{
			string result = string.Empty;
			switch (Tip)
			{
				case FingerprintTip.WaitingForFingerOn:
					result = "请求放置手指";
					break;
				case FingerprintTip.FingerOn:
					result = "手指已经放上";
					break;
				case FingerprintTip.WaitingForFingerOff:
					result = "请求手指拿开";
					break;
				case FingerprintTip.FingerOff:
					result = "手指已经拿开";
					break;
				case FingerprintTip.WaitingForFingerKeep:
					result = "登记时，放同一手指";
					break;
				case FingerprintTip.ImageCaptured:
					result = "采集到了手指的图像";
					break;
				case FingerprintTip.SensorTimeout:
					result = "超时";
					break;
				case FingerprintTip.Captured:
					result = "采集指纹图像结束";
					break;
				case FingerprintTip.Enrolled:
					result = "指纹认证结束";
					break;
				default:
					break;
			}
			return result;
		}
	}

	/// <summary>
	/// 登记指纹模板成功的事件参数
	/// </summary>
	public class EnrolledEventArgs : EventArgs
	{
		/// <summary>
		/// 登记指纹模板是否成功
		/// </summary>
		public bool Success { get; set; }
		/// <summary>
		/// 指纹模板
		/// </summary>
		/// <remarks>登记不成功,该值为string.Empty;否则为指纹模板数据</remarks>
		public byte[] MatBuf { get; set; }
		public int MatSize { get; set; }

		public EnrolledEventArgs(bool success, byte[] matBuf, int matSize)
		{
			this.Success = success;
			this.MatBuf = matBuf;
			this.MatSize = matSize;
		}
	}

	/// <summary>
	/// 显示指纹机捕获图像的事件参数
	/// </summary>
	public class ShowImageEventArgs : EventArgs
	{
		/// <summary>
		/// 指纹机捕获的指纹图像
		/// </summary>
		public Bitmap Image { get; set; }

		public ShowImageEventArgs(Bitmap bitmap)
		{
			this.Image = bitmap;
		}
	}

	/// <summary>
	/// 采集指纹的事件参数
	/// </summary>
	public class CapturedEventArgs : EventArgs
	{
		/// <summary>
		/// 采集指纹是否成功
		/// </summary>
		public bool Success { get; set; }
		/// <summary>
		/// 指纹数据
		/// </summary>
		public byte[] MatBuf { get; set; }
		public int MatSize { get; set; }

		public CapturedEventArgs(bool success, byte[] matBuf, int matSize)
		{
			this.Success = success;
			this.MatBuf = matBuf;
			this.MatSize = matSize;
		}
	}

	/// <summary>
	/// 指纹机消息的事件参数
	/// </summary>
	class MessageEventArgs : EventArgs
	{
		private Message _messsage;
		private int _data;

		/// <summary>
		/// 消息类型
		/// </summary>
		public Message Message { get { return _messsage; } }
		/// <summary>
		/// 消息数据
		/// </summary>
		public int Data { get { return _data; } }

		public MessageEventArgs(Message message, int data)
		{
			_messsage = message;
			_data = data;
		}
	}
}
