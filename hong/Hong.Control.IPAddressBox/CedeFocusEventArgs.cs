namespace Hong.Control.IPAddressBox
{
    using System;

    internal class CedeFocusEventArgs : EventArgs
    {
		private Hong.Control.IPAddressBox.Direction _direction;
        private int _fieldId;
		private Hong.Control.IPAddressBox.Selection _selection;

		public Hong.Control.IPAddressBox.Direction Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }

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

		public Hong.Control.IPAddressBox.Selection Selection
        {
            get
            {
                return this._selection;
            }
            set
            {
                this._selection = value;
            }
        }
    }
}

