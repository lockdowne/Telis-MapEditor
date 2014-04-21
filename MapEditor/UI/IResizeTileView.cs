using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.EventsArgs;

namespace MapEditor.UI
{
    public interface IResizeTileView
    {
        event ResizeTileEventHandler OnConfirm;
        event Action OnCancel;

        int TileWidth { get; set; }
        int TileHeight { get; set; }

        void ShowWindow();
        void CloseWindow();
    }
}
