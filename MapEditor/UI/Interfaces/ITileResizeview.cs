using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.UI
{
    public interface ITileResizeView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;

        int TileWidth { get; set; }
        int TileHeight { get; set; }

        void ShowForm();
        void CloseForm();
    }
}
