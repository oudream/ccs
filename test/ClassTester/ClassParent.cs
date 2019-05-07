using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClassTester
{
    public class ClassParent
    {
        private Form _form;
        public ClassParent(string text, int h, int w)
        {
            _form = new Form();
            _form.Text = text;
            _form.Height = h;
            _form.Width = w;
        }

        public Form Form()
        {
            return _form;
        }
    }
}
