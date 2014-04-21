using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface IResizeMapView
    {
        event ResizeMapEventHandler OnConfirm;
        event Action OnCancel;

        int MapWidth { get;  set; }
        int MapHeight { get; set; }

        void ShowWindow();
        void CloseWindow();
    }
}
