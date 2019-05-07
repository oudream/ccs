using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Xml;
using System.Reflection;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
    {
		class MyClass
		{
			public List<string> sl;

			void show()
			{ 

			}

			public MyClass()
			{
				sl = new List<string>();
				sl.Add("o");
				sl.Add("u");
			}
		}

		struct MyStruct
		{
			public int i;
			public string s;
			public byte[] buf;
			public MyClass mc;

			public MyStruct(int j)
			{
				mc = new MyClass();
				i = j;
				s = "oudream";
				buf = new byte[2];
			}
		}

		struct MyStruct1
		{
			public int i;
		}

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			//byte[] buf = new byte[3];
			//buf[0] = 1;
			//buf[1] = 3;
			//buf[2] = 7;

			//BufMothod(buf);

			//label1.Text = String.Format("{0:X2} {1:X2} {2:X2}", buf[0], buf[1], buf[2]);


			if (!(false || true))
			{
				MessageBox.Show("hello");
			}
        }

        private void BufMothod(ref byte[] buf)
        {
            buf[0] = 2;
            buf[1] = 4;
            buf[2] = 6;
        }

		private void button2_Click(object sender, EventArgs e)
		{

			//byte[] buf = new byte[20];
			//gets(buf);
			//label1.Text = Encoding.Default.GetString(buf);
			object o = new object();

			MyStruct st = new MyStruct(1);

			MyStruct1 str = new MyStruct1();

			label1.Text = System.Runtime.InteropServices.Marshal.SizeOf(st).ToString();
			//label1.Text = System.Runtime.InteropServices.Marshal.SizeOf(st.mc).ToString();
			label1.Text = System.Runtime.InteropServices.Marshal.SizeOf(str).ToString();
			gets(st);
			//st.s = "l";
			List<string> sl = new List<string>();
			sl.Add("lasf");
			st.mc.sl = sl;
			label1.Text = st.mc.sl[0];
		}

		private void gets(MyStruct st)
		{
			
		}

		[DllImport(@"D:\Project\TesterCSharp\DllTestDelphi\DllTestProject.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		static extern int DllMothod(ref KeyEventRecord lpDCB);
 
		private void button3_Click(object sender, EventArgs e)
		{
			DCB dcb = new DCB();
			dcb.DCBlength = 1;
			dcb.BaudRate = 2;
			dcb.Flags = 3;
			dcb.wReserved = 4;
			dcb.XonLim = 5;
			dcb.XoffLim = 6;
			dcb.ByteSize = 7;
			dcb.Parity = 8;
			dcb.StopBits = 9;
			dcb.XonChar = 10;
			dcb.XoffChar = 10;
			//dcb.ErrorChar = 11;
			dcb.EofChar = 12;
			dcb.EvtChar = 13;
			dcb.wReserved1 = 0x0f;

			KeyEventRecord key = new KeyEventRecord();
			key.keyDown = true;
			key.repeatCount = 0x1;
			key.virtualKeyCode = 0x2;
			key.virtualScanCode = 0x4;
			key.uChar = 'o';
			key.controlKeyState = 0x8;

			label1.Text = DllMothod(ref key).ToString();

			//this.Text = String.Format("{0:D} , {1:D} , {2:D}", dcb.XonLim, dcb.XoffLim, dcb.Parity);
		}

		public event Predicate<char> PredicateEvent;
		private void button4_Click(object sender, EventArgs e)
		{
			char[] hexByte = new char[('9' - '0') + ('f' - 'a') + ('F' - 'A') + 3];
			int index = 0;
			for (int i = '0'; i <= '9'; i++)
			{
				hexByte[index++] = (char)i;
			}
			for (int i = 'a'; i <= 'f'; i++)
			{
				hexByte[index++] = (char)i;
			}
			for (int i = 'A'; i <= 'F'; i++)
			{
				hexByte[index++] = (char)i;
			}

			label1.Text = new string(hexByte);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			IPAddress ip1 = IPAddress.Parse("192.168.0.3");
			IPAddress ip2 = IPAddress.Parse("192.168.000.003");
			if (IPAddress.Equals(ip1, ip2))
			{
				label1.Text = "y";
			}
			else
			{
				label1.Text = "n";
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			IPEndPoint ip1 = new IPEndPoint(IPAddress.Parse("192.168.1.30"), 5566);
			IPEndPoint ip2 = new IPEndPoint(IPAddress.Parse("192.168.001.030"), 5566);
			if (IPEndPoint.Equals(ip1, ip2))
			{
				label1.Text = "y";
			}
			else
			{
				label1.Text = "n";
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			label1.Text = ipAddressBox1.Text;
			this.Text = ipAddressBox1.ToString();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
						"<title>Pride And Prejudice</title>" +
						"</book>");

			//Create an attribute.
			XmlAttribute attr = doc.CreateAttribute("publisher");
			attr.Value = "WorldWide Publishing";

			//Add the new node to the document. 
			doc.DocumentElement.SetAttributeNode(attr);

			Console.WriteLine("Display the modified XML...");
			doc.Save(@"c:\a.xml");
		}

		private void button9_Click(object sender, EventArgs e)
		{
			Type type = typeof(Form1);
			if (type.IsSubclassOf(typeof(Control)))
			{
				Assembly asm = Assembly.GetExecutingAssembly();
				Form frm = (Form)asm.CreateInstance(type.FullName);
				frm.ShowDialog();
			}
		}

        private void ipAddressBox1_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {
            int right = ((Button)sender).Width;
            int bottom = ((Button)sender).Height;
            if (e.X > (right - 10) && e.Y > (bottom - 10))
            {
                ((Button)sender).Cursor = Cursors.SizeNWSE;
            }
            else if (e.X > (right - 10))
            {
                ((Button)sender).Cursor = Cursors.VSplit;
            }
            else if (e.Y > (bottom - 10))
            {
                ((Button)sender).Cursor = Cursors.HSplit;
            }
            else
            {
                ((Button)sender).Cursor = Cursors.Default;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (((Button)sender).Cursor == Cursors.VSplit)
                {
                    ((Button)sender).Width = e.X;
                }
                else if (((Button)sender).Cursor == Cursors.HSplit)
                {
                    ((Button)sender).Height = e.Y;
                }
                else
                {
                    ((Button)sender).Width = e.X;
                    ((Button)sender).Height = e.Y;
                }
            }
        }
    }

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct KeyEventRecord
	{
		internal bool keyDown;
		internal short repeatCount;
		internal short virtualKeyCode;
		internal short virtualScanCode;
		internal char uChar;
		internal int controlKeyState;
	}

	[StructLayout(LayoutKind.Sequential, Size = 27)]
	internal struct DCB
	{
		public uint DCBlength;
		public uint BaudRate;
		public uint Flags;
		public ushort wReserved;
		public ushort XonLim;
		public ushort XoffLim;
		public byte ByteSize;
		public byte Parity;
		public byte StopBits;
		public byte XonChar;
		public byte XoffChar;
		//public byte ErrorChar;
		public byte EofChar;
		public byte EvtChar;
		public ushort wReserved1;


	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct TOKEN_SOURCE
	{
		private const int TOKEN_SOURCE_LENGTH = 8;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal char[] Name;
		internal LUID SourceIdentifier;
	}

	internal struct LUID
	{
		internal uint LowPart;
		internal uint HighPart;
	}

	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	struct MyStruct2
	{
		public uint DCBlength;
		public uint BaudRate;
		public uint Flags;
		public ushort wReserved;
		public ushort XonLim;
		public ushort XoffLim;
		public byte ByteSize;
		public byte Parity;
		//public byte StopBits;
		public byte XonChar;
		public byte XoffChar;
		public byte ErrorChar;
		public byte EofChar;
		public byte EvtChar;
		public ushort wReserved1;
	}

}
