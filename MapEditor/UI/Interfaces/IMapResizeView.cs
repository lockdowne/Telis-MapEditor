using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.UI
{
    public interface IMapResizeView
    {
        event EventHandler OnConfirm;
        event EventHandler OnCancel;

        int MapWidth { get; }
        int MapHeight { get; }

        void ShowForm();
        void CloseForm();
    }
}
