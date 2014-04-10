using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Core.Controls
{
    //This class is just in case we decided to modify the CheckedListBox class
    public partial class CheckedListBoxEx : CheckedListBox
    {
        protected override void OnDoubleClick(EventArgs e)
        {
            //base.OnDoubleClick(e);
        }

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //base.OnMouseDown(e);
        }

        
    }
}
