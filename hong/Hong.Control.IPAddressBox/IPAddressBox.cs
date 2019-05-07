namespace Hong.Control.IPAddressBox
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    [Designer(typeof(IPAddressControlDesigner))]
    public class IPAddressBox : UserControl
    {
        private bool _backColorChanged;
        private System.Windows.Forms.BorderStyle _borderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        private DotControl[] _dotControls = new DotControl[3];
        private FieldControl[] _fieldControls = new FieldControl[4];
        private bool _readOnly;
        private TextBox _referenceTextBox = new TextBox();
        private IContainer components = null;
        private Size Fixed3DOffset = new Size(3, 3);
        private Size FixedSingleOffset = new Size(2, 2);
        public const int NumberOfFields = 4;

        public event EventHandler<FieldChangedEventArgs> FieldChangedEvent;

        public IPAddressBox()
        {
            this.BackColor = SystemColors.Window;
            this.ResetBackColorChanged();
            for (int i = 0; i < this._fieldControls.Length; i++)
            {
                this._fieldControls[i] = new FieldControl();
                this._fieldControls[i].CedeFocusEvent += new EventHandler<CedeFocusEventArgs>(this.OnCedeFocus);
                this._fieldControls[i].FieldId = i;
                this._fieldControls[i].Name = "FieldControl" + i.ToString(CultureInfo.InvariantCulture);
                this._fieldControls[i].Parent = this;
                this._fieldControls[i].SpecialKeyEvent += new EventHandler<SpecialKeyEventArgs>(this.OnSpecialKey);
                this._fieldControls[i].TextChangedEvent += new EventHandler<TextChangedEventArgs>(this.OnFieldTextChanged);
                base.Controls.Add(this._fieldControls[i]);
                if (i < 3)
                {
                    this._dotControls[i] = new DotControl();
                    this._dotControls[i].Name = "DotControl" + i.ToString(CultureInfo.InvariantCulture);
                    this._dotControls[i].Parent = this;
                    base.Controls.Add(this._dotControls[i]);
                }
            }
            this.InitializeComponent();
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            this._referenceTextBox.AutoSize = true;
            base.Size = this.MinimumSize;
            this.AutoSize = true;
            base.DragEnter += new DragEventHandler(this.IPAddressControl_DragEnter);
            base.DragDrop += new DragEventHandler(this.IPAddressControl_DragDrop);
        }

        private void AdjustSize()
        {
            Size minimumSize = this.MinimumSize;
            if (base.Size.Width > minimumSize.Width)
            {
                minimumSize.Width = base.Size.Width;
            }
            if (base.Size.Height > minimumSize.Height)
            {
                minimumSize.Height = base.Size.Height;
            }
            if (this.AutoSize)
            {
                base.Size = new Size(this.MinimumSize.Width, this.MinimumSize.Height);
            }
            else
            {
                base.Size = minimumSize;
            }
            this.LayoutControls();
        }

        private Size CalculateMinimumSize()
        {
            Size size = new Size(0, 0);
            foreach (FieldControl control in this._fieldControls)
            {
                size.Width += control.Size.Width;
                size.Height = Math.Max(size.Height, control.Size.Height);
            }
            foreach (DotControl control2 in this._dotControls)
            {
                size.Width += control2.Size.Width;
                size.Height = Math.Max(size.Height, control2.Size.Height);
            }
            switch (this.BorderStyle)
            {
                case System.Windows.Forms.BorderStyle.FixedSingle:
                    size.Width += 4;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;

                case System.Windows.Forms.BorderStyle.Fixed3D:
                    size.Width += 6;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;
            }
            return size;
        }

        public void Clear()
        {
            foreach (FieldControl control in this._fieldControls)
            {
                control.Clear();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public byte[] GetAddressBytes()
        {
            byte[] buffer = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                buffer[i] = this._fieldControls[i].Value;
            }
            return buffer;
        }

        private int GetSuggestedHeight()
        {
            this._referenceTextBox.BorderStyle = this.BorderStyle;
            this._referenceTextBox.Font = this.Font;
            return this._referenceTextBox.Height;
        }

		private static Hong.Control.IPAddressBox.NativeMethods.TEXTMETRIC GetTextMetrics(IntPtr hwnd, Font font)
        {
			Hong.Control.IPAddressBox.NativeMethods.TEXTMETRIC textmetric;
			IntPtr windowDC = Hong.Control.IPAddressBox.NativeMethods.GetWindowDC(hwnd);
            IntPtr hgdiobj = font.ToHfont();
            try
            {
				IntPtr ptr3 = Hong.Control.IPAddressBox.NativeMethods.SelectObject(windowDC, hgdiobj);
				Hong.Control.IPAddressBox.NativeMethods.GetTextMetrics(windowDC, out textmetric);
				Hong.Control.IPAddressBox.NativeMethods.SelectObject(windowDC, ptr3);
            }
            finally
            {
				Hong.Control.IPAddressBox.NativeMethods.ReleaseDC(hwnd, windowDC);
				Hong.Control.IPAddressBox.NativeMethods.DeleteObject(hgdiobj);
            }
            return textmetric;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
        }

        private void IPAddressControl_DragDrop(object sender, DragEventArgs e)
        {
            this.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void IPAddressControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LayoutControls()
        {
            base.SuspendLayout();
            int num = base.Size.Width - this.MinimumSize.Width;
            Debug.Assert(num >= 0);
            int num2 = (this._fieldControls.Length + this._dotControls.Length) + 1;
            int num3 = num / num2;
            int num4 = num % num2;
            int[] numArray = new int[num2];
            for (int i = 0; i < num2; i++)
            {
                numArray[i] = num3;
                if (i < num4)
                {
                    numArray[i]++;
                }
            }
            int x = 0;
            int y = 0;
            switch (this.BorderStyle)
            {
                case System.Windows.Forms.BorderStyle.FixedSingle:
                    x = this.FixedSingleOffset.Width;
                    y = this.FixedSingleOffset.Height;
                    break;

                case System.Windows.Forms.BorderStyle.Fixed3D:
                    x = this.Fixed3DOffset.Width;
                    y = this.Fixed3DOffset.Height;
                    break;
            }
            int num8 = 0;
            x += numArray[num8++];
            for (int j = 0; j < this._fieldControls.Length; j++)
            {
                this._fieldControls[j].Location = new Point(x, y);
                x += this._fieldControls[j].Size.Width;
                if (j < this._dotControls.Length)
                {
                    x += numArray[num8++];
                    this._dotControls[j].Location = new Point(x, y);
                    x += this._dotControls[j].Size.Width;
                    x += numArray[num8++];
                }
            }
            base.ResumeLayout(false);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            this._backColorChanged = true;
        }

        private void OnCedeFocus(object sender, CedeFocusEventArgs e)
        {
            if (((e.Direction != Direction.Reverse) || (e.FieldId != 0)) && ((e.Direction != Direction.Forward) || (e.FieldId != 3)))
            {
                int fieldId = e.FieldId;
                if (e.Direction == Direction.Forward)
                {
                    fieldId++;
                }
                else
                {
                    fieldId--;
                }
                this._fieldControls[fieldId].TakeFocus(e.Direction, e.Selection);
            }
        }

        private void OnFieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (null != this.FieldChangedEvent)
            {
                FieldChangedEventArgs args = new FieldChangedEventArgs();
                args.FieldId = e.FieldId;
                args.Text = e.Text;
                this.FieldChangedEvent(this, args);
            }
            this.OnTextChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.AdjustSize();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this._fieldControls[0].TakeFocus(Direction.Forward, Selection.All);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.IBeam;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color backColor = this.BackColor;
            if (!this._backColorChanged && !(base.Enabled && !this.ReadOnly))
            {
                backColor = SystemColors.Control;
            }
            e.Graphics.FillRectangle(new SolidBrush(backColor), base.ClientRectangle);
            Rectangle bounds = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
            switch (this.BorderStyle)
            {
                case System.Windows.Forms.BorderStyle.FixedSingle:
                    ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
                    break;

                case System.Windows.Forms.BorderStyle.Fixed3D:
                    if (!Application.RenderWithVisualStyles)
                    {
                        ControlPaint.DrawBorder3D(e.Graphics, base.ClientRectangle, Border3DStyle.Sunken);
                        break;
                    }
                    ControlPaint.DrawVisualStyleBorder(e.Graphics, bounds);
                    break;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.AdjustSize();
        }

        private void OnSpecialKey(object sender, SpecialKeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.End:
                    this._fieldControls[3].TakeFocus(Direction.Reverse, Selection.None);
                    break;

                case Keys.Home:
                    this._fieldControls[0].TakeFocus(Direction.Forward, Selection.None);
                    break;

                case Keys.Back:
                    if (e.FieldId > 0)
                    {
                        this._fieldControls[e.FieldId - 1].HandleSpecialKey(Keys.Back);
                    }
                    break;
            }
        }

        private void Parse(string text)
        {
            this.Clear();
            if (null != text)
            {
                int startIndex = 0;
                int index = 0;
                index = 0;
                while (index < this._dotControls.Length)
                {
                    int num3 = text.IndexOf(this._dotControls[index].Text, startIndex);
                    if (num3 >= 0)
                    {
                        this._fieldControls[index].Text = text.Substring(startIndex, num3 - startIndex);
                        startIndex = num3 + this._dotControls[index].Text.Length;
                    }
                    else
                    {
                        break;
                    }
                    index++;
                }
                this._fieldControls[index].Text = text.Substring(startIndex);
            }
        }

        private void ResetBackColorChanged()
        {
            this._backColorChanged = false;
        }

        public void SetAddressBytes(byte[] bytes)
        {
            this.Clear();
            int num = Math.Min(4, bytes.Length);
            for (int i = 0; i < num; i++)
            {
                this._fieldControls[i].Text = bytes[i].ToString(CultureInfo.InvariantCulture);
            }
        }

        public void SetFieldFocus(int fieldId)
        {
            if ((fieldId >= 0) && (fieldId < 4))
            {
                this._fieldControls[fieldId].TakeFocus(Direction.Forward, Selection.All);
            }
        }

        public void SetFieldRange(int fieldId, byte rangeLower, byte rangeUpper)
        {
            if ((fieldId >= 0) && (fieldId < 4))
            {
                this._fieldControls[fieldId].RangeLower = rangeLower;
                this._fieldControls[fieldId].RangeUpper = rangeUpper;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                builder.Append(this._fieldControls[i].ToString());
                if (i < this._dotControls.Length)
                {
                    builder.Append(this._dotControls[i].ToString());
                }
            }
            return builder.ToString();
        }

        [DefaultValue(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                if (this.AutoSize)
                {
                    this.AdjustSize();
                }
            }
        }

        public int Baseline
        {
            get
            {
                int num = GetTextMetrics(base.Handle, this.Font).tmAscent + 1;
                switch (this.BorderStyle)
                {
                    case System.Windows.Forms.BorderStyle.FixedSingle:
                        return (num + this.FixedSingleOffset.Height);

                    case System.Windows.Forms.BorderStyle.Fixed3D:
                        return (num + this.Fixed3DOffset.Height);
                }
                return num;
            }
        }

        public bool Blank
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (!control.Blank)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [DefaultValue(2)]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return this._borderStyle;
            }
            set
            {
                this._borderStyle = value;
                this.AdjustSize();
                base.Invalidate();
            }
        }

        public override bool Focused
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (control.Focused)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return this.CalculateMinimumSize();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                foreach (FieldControl control in this._fieldControls)
                {
                    control.ReadOnly = this._readOnly;
                }
                foreach (DotControl control2 in this._dotControls)
                {
                    control2.ReadOnly = this._readOnly;
                }
                base.Invalidate();
            }
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this._fieldControls.Length; i++)
                {
                    builder.Append(this._fieldControls[i].Text);
                    if (i < this._dotControls.Length)
                    {
                        builder.Append(this._dotControls[i].Text);
                    }
                }
                return builder.ToString();
            }
            set
            {
                this.Parse(value);
            }
        }
    }
}

