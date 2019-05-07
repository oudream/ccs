namespace Hong.Control.IPAddressBox
{
    using System;
    using System.Windows.Forms;

    internal class SpecialKeyEventArgs : EventArgs
    {
        private int _fieldId;
        private Keys _keyCode;

        public int FieldId
        {
            get
            {
                return this._fieldId;
            }
            set
            {
                this._fieldId = value;
            }
        }

        public Keys KeyCode
        {
            get
            {
                return this._keyCode;
            }
            set
            {
                this._keyCode = value;
            }
        }
    }
}

