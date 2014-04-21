using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface INewMapView
    {
        event Action OnBrowse;
        event NewMapEventHandler OnConfirm;
        event Action OnCancel;
        event NewMapEventHandler OnNumericValueChanged;
        event NewMapEventHandler OnWindowLoaded;

        string PixelDisplayer { set; }
        string TilesetPath { set; }

        void ShowWindow();
        void CloseWindow();
        void DisplayMessage(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
        void Reset();
    }
}
