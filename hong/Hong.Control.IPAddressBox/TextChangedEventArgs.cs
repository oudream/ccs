namespace Hong.Control.IPAddressBox
{
    using System;

    internal class TextChangedEventArgs : EventArgs
    {
        private int _fieldId;
        private string _text;

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

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }
}

