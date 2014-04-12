using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public interface ITileResizeView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;

        int TileWidth { get; set; }
        int TileHeight { get; set; }

        NumericUpDownEx TileWidthNumeric { get; set; }
        NumericUpDownEx TileHeightNumeric { get; set; }

        void ShowForm();
        void CloseForm();
    }
}
