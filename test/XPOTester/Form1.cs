using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using System.Resources;
using DevExpress.Data.Filtering;
using System.Collections;
using System.Reflection;
using ClassTester;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public partial class Form1 : Form
	{
		private Session session;
		private XPCollection<User> UserCollection;
		private XPCollection<Department> DepartmentCollection;
		private XPCollection<Product> ProductCollection;
		private XPCollection<OrderMain> OrderMainCollection;
        private XPCollection<InSchool> inschools;
		public Form1()
		{
			InitializeComponent();

			this.sqlConnection1.Open();
			session = new Session();
			session.Connection = this.sqlConnection1;

			UserCollection = new XPCollection<User>(session);
			DepartmentCollection = new XPCollection<Department>(session);
			OrderMainCollection = new XPCollection<OrderMain>(session);
			ProductCollection = new XPCollection<Product>(session);
            inschools = new XPCollection<InSchool>(session);

			this.comboBox1.Items.Clear();
			this.comboBox2.Items.Clear();
			string[] views = Enum.GetNames(typeof(View));
			foreach (string item in views)
			{
				this.comboBox1.Items.Add(item);
				this.comboBox2.Items.Add(item);
				this.InSchoolViewTypeEd.Items.Add(item);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			User user = new User(session);

			Role role = new Role(session);
			role.Name = "Administrator";
			user.Roles.Add(role);

			role = new Role(session);
			role.Name = "Tester";
			user.Roles.Add(role);

			user.FingerPrintData = new byte[380];

			user.LastName = this.textBox1.Text;
			user.FirstName = this.textBox2.Text;
			user.Name = this.textBox2.Text;

			user.UserName = this.textBox1.Text;
			user.PassWordA = this.textBox2.Text;
			user.PassWordB = this.textBox2.Text;
			user.Photo = this.pictureBox1.Image;

			user.Department = GetDepartment("A");

			StringBuilder s = new StringBuilder(8192);
			user.TempletA = s.ToString();

			user.Save();
		}

		private Department GetDepartment(string p)
		{
			foreach (Department item in DepartmentCollection)
			{
				if (item.DepartmentName.Equals(p))
				{
					return item;
				}
			}
			return null;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.listBox1.Items.Clear();
			foreach (User item in UserCollection)
			{
				this.listBox1.Items.Add(item.Oid.ToString());
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			UserCollection.Reload();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string fileName = "";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				fileName = this.openFileDialog1.FileName;
				this.pictureBox1.Image = Image.FromFile(fileName);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int oid = Convert.ToInt32(this.listBox1.SelectedItem);
			foreach (User item in UserCollection)
			{
				if (item.Oid == oid)
				{
					this.label1.Text = String.Format("Name : {0}, Gender : {1}, Department : {2}", item.Name, item.Gender.ToString(), item.Department.DepartmentName);
					this.pictureBox1.Image = item.Photo;
				}
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			Department department = new Department(session);
			department.DepartmentName = this.textBox3.Text;
			department.Save();

			DepartmentCollection.Add(department);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			ListView lv = this.listView1;
			lv.Items.Clear();
			lv.LargeImageList = this.imageList1;
			lv.SmallImageList = this.imageList2;
			ListViewItem li = null;
			for (int i = 1; i < 6; i++)
			{
				li = new ListViewItem();
				li.Text = "Name" + i.ToString();
				li.SubItems.Add(i.ToString() + i.ToString());
				li.SubItems.Add(i.ToString() + i.ToString() + i.ToString());
				li.SubItems.Add(i.ToString() + i.ToString() + i.ToString() + i.ToString());
				li.ImageKey = i.ToString();
				lv.Items.Add(li);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.listView1.View = (View)Enum.Parse(typeof(View), this.comboBox1.Text);
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void listView1_Click(object sender, EventArgs e)
		{
			ListView lv = this.listView1;
			System.Windows.Forms.ListView.SelectedListViewItemCollection lis = lv.SelectedItems;
			foreach (ListViewItem item in lis)
			{
				item.Checked = !item.Checked;
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
		}

		private OrderMain _orderMain;
		private void button8_Click(object sender, EventArgs e)
		{
			_orderMain = new OrderMain(session);
			_orderMain.ClientName = this.textBox4.Text;
		}

		private void button9_Click(object sender, EventArgs e)
		{
			OrderDetail od = new OrderDetail(session);
			od.Product = _product;
			od.Price = Convert.ToInt32(this.numericUpDown1.Value);
			od.Count = Convert.ToInt32(this.numericUpDown2.Value);
			_orderMain.OrderDetails.Add(od);
		}

		private void button10_Click(object sender, EventArgs e)
		{
			_orderMain.Save();

			ViewOutOrderDetails(_orderMain);
		}

		private void ViewOutOrderDetails(OrderMain orderMain)
		{
			ListView lv = this.listView2;
			lv.Items.Clear();

			ListViewItem li;
			foreach (OrderDetail item in orderMain.OrderDetails)
			{
				li = new ListViewItem();
				li.Text = item.Price.ToString();
				if (item.Product != null)
				{
					li.SubItems.Add(item.Product.Name);
				}
				li.SubItems.Add(item.Count.ToString());
				li.SubItems.Add((item.Price * item.Count).ToString());
				lv.Items.Add(li);
			}
		}

		private void ViewOutOrderDetails()
		{
		}

		private void button13_Click(object sender, EventArgs e)
		{
			OrderMainCollection.Reload();

			this.listBox2.Items.Clear();
			foreach (OrderMain item in OrderMainCollection)
			{
				this.listBox2.Items.Add(item.ClientName);
			}
		}

		private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			string clientName = this.listBox2.SelectedItem.ToString();
			OrderMain om = null;
			foreach (OrderMain item in OrderMainCollection)
			{
				if (item.ClientName.Equals(clientName))
				{
					om = item;
					break;
				}
			}
			if (om != null)
			{
				ViewOutOrderDetails(om);
			}
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.listView2.View = (View)Enum.Parse(typeof(View), this.comboBox2.Text);
		}

		Product _product;
		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (Product item in ProductCollection)
			{
				if (item.Name.Equals(this.comboBox3.Text))
				{
					_product = item;
					MessageBox.Show(_product.Name);
					break;
				}
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			//_product = new Product(session);
			//_product.Name = "pa";
			//_product.Save();
			//_product = new Product(session);
			//_product.Name = "pb";
			//_product.Save();
			//_product = new Product(session);
			//_product.Name = "pc";
			//_product.Save();
		}

		private void InSchoolSaveBn_Click(object sender, EventArgs e)
		{
			InSchool inSchool = new InSchool(session);
			inSchool.Student = this.InSchoolNameEd.Text;
			inSchool.InSchoolTime = this.InSchoolTimeEd.Value;
			inSchool.Save();
		}

		private void InSchoolRefreshBn_Click(object sender, EventArgs e)
		{
			ListView lv = this.InSchoolView;
			lv.Items.Clear();

			ListViewItem li;
			foreach (InSchool item in inschools)
			{
				li = new ListViewItem();
				li.Tag = item;
				li.Text = item.Student;
				li.SubItems.Add(item.InSchoolTime.ToString());
				lv.Items.Add(li);
			}
		}

		private void InSchoolViewTypeEd_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.InSchoolView.View = (View)Enum.Parse(typeof(View), this.InSchoolViewTypeEd.Text);
		}

		private void button17_Click(object sender, EventArgs e)
		{
			GroupOperator ops = new GroupOperator();
			BinaryOperator op;
			op = new BinaryOperator("InSchoolTime", this.dateTimePicker1.Value, BinaryOperatorType.Greater);
			ops.Operands.Add(op);
			op = new BinaryOperator("InSchoolTime", this.dateTimePicker2.Value, BinaryOperatorType.Less);
			ops.Operands.Add(op);

			ListView lv = this.InSchoolView;
			lv.Items.Clear();

			XPCollection<InSchool> inschools = new XPCollection<InSchool>(session, ops);

			ListViewItem li;
			foreach (InSchool item in inschools)
			{
				li = new ListViewItem();
				li.Tag = item;
				li.Text = item.Student;
				li.SubItems.Add(item.InSchoolTime.ToString());
				lv.Items.Add(li);
			}
		}

		private void button14_Click(object sender, EventArgs e)
		{
			XPCollection<InSchool> inschools = new XPCollection<InSchool>(session);
			ICollection ic = inschools as ICollection;
			InSchool[] iss = new InSchool[ic.Count];
			ic.CopyTo(iss, 0);

			ListView lv = this.InSchoolView;
			lv.Items.Clear();
			ListViewItem li;
			foreach (InSchool item in iss)
			{
				li = new ListViewItem();
				li.Tag = item;
				li.Text = item.Student;
				li.SubItems.Add(item.InSchoolTime.ToString());
				lv.Items.Add(li);
			}
		}

		private void InSchoolDeleteBn_Click(object sender, EventArgs e)
		{
            XPCollection<InSchool> inschoolList = new XPCollection<InSchool>(session);

			ListView lv = this.InSchoolView;

			foreach (ListViewItem item in lv.SelectedItems)
			{
				InSchool isc = (InSchool)item.Tag;
				//DeleteInSchool(isc.Oid);
				isc.Delete();
				item.Remove();
			}

            this.Text = Convert.ToString(inschoolList.Count);
		}

		private void DeleteInSchool(int p)
		{
            //InSchool isc = new InSchool(session);
            //isc.Oid = p;
            //isc.Delete();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{

		}

        private void button15_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void InSchoolEditBn_Click(object sender, EventArgs e)
        {
            XPCollection<InSchool> inschoolList = new XPCollection<InSchool>(session);
            InSchool inschool = inschoolList[0];

            ListView lv = this.InSchoolView;

            foreach (ListViewItem item in lv.SelectedItems)
            {
                InSchool isc = (InSchool)item.Tag;
                isc.InSchoolTime = DateTime.Now;
                item.SubItems[1] = new ListViewItem.ListViewSubItem(item, isc.InSchoolTime.ToString());
            }

            this.Text = Convert.ToString(inschool.InSchoolTime.ToString());
        }

        private void InSchoolAddBn_Click(object sender, EventArgs e)
        {
            XPCollection<InSchool> inschoolList = new XPCollection<InSchool>(session);
            
            InSchool inschool = new InSchool(session);
            inschool.InSchoolTime = DateTime.Now;
            inschool.Save();

            this.Text = Convert.ToString(inschoolList[inschoolList.Count-1].InSchoolTime.ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            XPCollection inschoolList = new XPCollection(session, typeof(InSchool));

            foreach (XPMemberInfo item in inschoolList.ObjectClassInfo.PersistentProperties)
            {
                MessageBox.Show(item.Name + " - " +  item.MemberType.FullName);
            }

            return;

            MessageBox.Show(inschoolList.ObjectType.FullName);
            MessageBox.Show(inschoolList.Count.ToString());

            return;

            object[] paramValues = {"hello class testor", 600, 800};
            Assembly asm = Assembly.GetAssembly(typeof(ClassTester.ClassTestClass));
            ClassParent frm = (ClassParent)asm.CreateInstance("ClassTester.ClassParent", true, BindingFlags.Default, null, paramValues, null, null);
            frm.Form().ShowDialog();
        }
	}
}
