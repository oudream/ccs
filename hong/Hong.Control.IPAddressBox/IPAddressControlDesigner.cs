namespace Hong.Control.IPAddressBox
{
    using System.Collections;
    using System.Windows.Forms.Design;
    using System.Windows.Forms.Design.Behavior;

    internal class IPAddressControlDesigner : ControlDesigner
    {
        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                if (this.Control.AutoSize)
                {
                    return (System.Windows.Forms.Design.SelectionRules.Visible | System.Windows.Forms.Design.SelectionRules.Moveable);
                }
                return (System.Windows.Forms.Design.SelectionRules.Visible | System.Windows.Forms.Design.SelectionRules.Moveable | System.Windows.Forms.Design.SelectionRules.AllSizeable);
            }
        }

        public override IList SnapLines
        {
            get
            {
                IPAddressBox control = (IPAddressBox) this.Control;
                IList snapLines = base.SnapLines;
                snapLines.Add(new SnapLine(SnapLineType.Baseline, control.Baseline));
                return snapLines;
            }
        }
    }
}

