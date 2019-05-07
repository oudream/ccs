using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;
using System.Windows.Forms;
using Hong.Xpo.UiModule;
using System.Drawing;

namespace Hong.Xpo.WinModule
{
    public class WinLayout : UiLayout
    {
        private const DockStyle DefaultDock = DockStyle.None;
        private const int DefaultHeight = -1;
        private const int DefaultLocationX = -1;
        private const int DefaultLocationY = -1;
        private const int DefaultWidth = -1; 

        public WinLayout(CellerBase celler)
            : base(celler)
        {
            _dock = AddVariable<DockStyle>("Dock", DefaultDock);
            _height = AddVariable<int>("Height", DefaultHeight);
            _locationX = AddVariable<int>("LocationX", DefaultLocationX);
            _locationY = AddVariable<int>("LocationY", DefaultLocationY);
            _width = AddVariable<int>("Width", DefaultWidth);
        }

        protected override void VariablesChanged(object sender, VariableListChangedArgs e)
        {
            if (Variables.IndexOf(e.Variable) > -1)
            {
                if ((_height.Value != DefaultHeight && _locationX.Value != DefaultLocationX && _locationY.Value != DefaultLocationY && _width.Value != DefaultWidth) || _dock.Value != DefaultDock)
                {
                    RaiseChanged(e);
                }
            }
        }

        private VariableItem<DockStyle> _dock;
        public VariableItem<DockStyle> Dock
        {
            get
            {
                return _dock;
            }
        }

        private VariableItem<int> _locationX;
        public VariableItem<int> LocationX
        {
            get
            {
                return _locationX;
            }
        }

        private VariableItem<int> _locationY;
        public VariableItem<int> LocationY
        {
            get
            {
                return _locationY;
            }
        }

        private VariableItem<int> _width;
        public VariableItem<int> Width
        {
            get
            {
                return _width;
            }
        }

        private VariableItem<int> _height;
        public VariableItem<int> Height
        {
            get
            {
                return _height;
            }
        }
    }
}
