using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.UI
{
    public interface IAddTilesetView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;
        event EventHandler OnBrowse;

        string TilesetPath { get; set; }

        void ShowForm();
        void CloseForm();

    }
}
