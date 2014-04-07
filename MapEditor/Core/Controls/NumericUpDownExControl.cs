using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Core.Controls
{
    public class NumericUpDownEx : NumericUpDown
    {
        public string Measure { get; set; }

        public NumericUpDownEx()
        {

        }

        protected override void UpdateEditText()
        {
            this.Text = this.Value.ToString() + " " + Measure;
        }
    }
}
