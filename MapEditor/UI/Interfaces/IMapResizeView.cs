using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public interface IMapResizeView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;

        int MapWidth { get; set; }
        int MapHeight { get; set; }

        NumericUpDownEx MapWidthNumeric { get;  set; }
        NumericUpDownEx MapHeightNumeric { get;  set; }

        void ShowForm();
        void CloseForm();
    }
}
