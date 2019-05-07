using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Hong.Common.SystemWin;

namespace Hong.Xpo.WinModule
{
    public class WinImageBox : WinControlEasy<Image>
    {
        public WinImageBox()
        {
            _pictureBox = new PictureBox();
            _pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;

            _button = new Button();
            _button.Dock = System.Windows.Forms.DockStyle.Top;
            _button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            _button.Text = "选择图片……";
            _button.Click += new System.EventHandler(ButtonClick);

            _groupBox = new GroupBox();
            _groupBox.Controls.Add(this._pictureBox);
            _groupBox.Controls.Add(this._button);
        }

        private PictureBox _pictureBox;

        private GroupBox _groupBox;

        private Button _button;

        private void ButtonClick(object sender, EventArgs e)
        {
            string fileName = WinSystemHelper.Singleton.ShowOpenFileDialog();
            if (fileName != "")
            {
                Image image = Image.FromFile(fileName);
                _pictureBox.Image = image;
            }
        }

        protected override bool ComponentToValueImpl(out Image value)
        {
            value = _pictureBox.Image;
            return true;
        }

        protected override bool ValueToComponentImpl(Image value)
        {
            _pictureBox.Image = value;
            return true;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_groupBox);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_groupBox);
        }

        protected override void TitleValueChanged(string value)
        {
            _groupBox.Text = value;
        }

        protected override void LayoutChanged(WinLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _groupBox.Location = new System.Drawing.Point(layout.LocationX.Value, layout.LocationY.Value);
            _groupBox.Width = layout.Width.Value;
            _groupBox.Height = layout.Height.Value;
            _groupBox.Dock = layout.Dock.Value;
        }
    }
}
