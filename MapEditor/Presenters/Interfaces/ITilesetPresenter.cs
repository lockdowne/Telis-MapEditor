using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public interface ITilesetPresenter
    {
        event Action<int[,]> SendTileBrushValues;

        ITilesetRenderPresenter CurrentPresenter { get; }

        void AddPresenter(string name, IXnaRenderView renderView, string path, int tileWidth, int tileHeight);
        void LoadForm(IMainView parent);
        
        
    }
}
