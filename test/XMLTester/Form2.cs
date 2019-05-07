using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using GenericTester;
using DevExpress.Xpo;

namespace XMLTester
{
    public partial class Form2 : Form
    {
        class MyClassa
        {
            
        }

        class ClassA<T> : MyClassa
        {
            private T _value;
            public T Value
            {
                get
                {
                    return _value;
                }
            }
        }

        class ClassB : ClassA<int>
        {

        }

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = this.dateTimePicker1.Value;
            MessageBox.Show(dt.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }


        enum MyEnum
        {
            asdfjadsf,
            asdfasdf,
            asdfasd,
            sdfsadfg
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadEnumControl(typeof(MyEnum));
        }

        private void LoadEnumControl(Type type)
        {
            int defaultWidth = 80;
            int measure = 25;
            int x = 25;
            int y = 25;
            string[] names = Enum.GetNames(type);
            foreach (string name in names)
            {
                RadioButton radio = new RadioButton();
                radio.Text = name;
                radio.Location = new Point(x, y);
                x = x + defaultWidth + measure;
                if (x + defaultWidth > _groupBox.Width)
                {
                    x = measure;
                    y = y + measure;
                }
                _groupBox.Controls.Add(radio);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Class2 class2 = new Class2();
            
            class2.Value = MyEnum.asdfasdf;

            class2.Value = GetEnumValue(class2.Value.GetType()) as Enum;

            //Enum value = GetEnumValue(class2.Value.GetType()) as Enum;
            MessageBox.Show(class2.Value.ToString());
            MessageBox.Show(class2.Value.GetType().ToString());

            return;

            int k = 1;
            int l = 2;

            if (k.GetType() == l.GetType() || k.GetType().IsSubclassOf(l.GetType()))
            {
                MessageBox.Show("");
            }

            return;

            if (class2.Value.GetType().IsSubclassOf(typeof(Enum)))
            {
                MessageBox.Show("");
            }


        }

        private object GetEnumValue(Type type)
        {

            foreach (Control control in _groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        return Enum.Parse(type, radio.Text);
                    }
                }
            }
            return null;
        }

        private void GetEnumValue()
        {
        }

        class MyClass
        {
            public MyClass(Button button)
            {
                BN = button;
            }
            
            public Button BN;

            //public override string ToString()
            //{
            //    return BN.Text;
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new MyClass(button1));
            listBox1.Items.Add(new MyClass(button3));
            listBox1.Items.Add(new MyClass(button2));

            this.comboBox1.Items.Add(new MyClass(button1));
            this.comboBox1.Items.Add(new MyClass(button3));
            this.comboBox1.Items.Add(new MyClass(button2));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show((listBox1.SelectedItem as MyClass).BN.Name);
            MessageBox.Show((comboBox1.SelectedItem as MyClass).BN.Name);
        }

        class MyAttr : Attribute
        {
            public MyAttr(string propername)
            {
                _propername = propername;
            }

            private string _propername;
            public string Propername
            {
                get
                {
                    return _propername;
                }
            }
        }

        [MyAttr("DisplayName")]
        class TestAttr : XPObject
        {
            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public string FullName { get; set; }

            public string DisplayName { get; set; }

            public string Code { get; set; }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TestAttr testAttr = new TestAttr();
            testAttr.DisplayName = "sadfasdf";
            testAttr.FullName = "sa2dfasdafsdf";
            testAttr.MiddleName = "sadsadfwerwq34fasdf";
            testAttr.Code = "sadf3455437635asdf";
            TestAttrModth(testAttr);
        }

        private void TestAttrModth(XPObject obj)
        {
            Type type = obj.GetType();
            object[] attrs = type.GetCustomAttributes(false);
            foreach (object attr in attrs)
            {
                if (attr is MyAttr)
                {
                    MyAttr myattr = attr as MyAttr;
                    MessageBox.Show(myattr.Propername);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GroupBox g;

            g = new GroupBox();
            g.Dock = DockStyle.Left;
            g.Text = "1";
            g.Controls.Add(new Button());
            g.Controls.Add(new Button());
            g.Controls.Add(new Button());
            this.panel2.Controls.Add(g);
           
            g = new GroupBox();
            g.Dock = DockStyle.Fill;
            g.Text = "2";
            g.Controls.Add(new Button());
            g.Controls.Add(new Button());
            this.panel2.Controls.Add(g);

            this.panel2.Text = "213323";

            foreach (Control control in this.panel2.Controls)
            {
                control.TabIndex = 1;
            }
        }
    }
}
