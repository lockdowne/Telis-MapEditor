using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface IOffsetView
    {
        event OffsetEventHandler OnConfirm;
        event Action OnCancel;

        int OffsetX { set; }
        int OffsetY { set; }

        void ShowWindow();
        void CloseWindow();
    }
}
