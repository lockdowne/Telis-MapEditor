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
        List<ITilesetRenderPresenter> AllPresenters { get; }

        void AddPresenter(string path, int tileWidth, int tileHeight);
        void RemovePresenter(string key);
        void LoadForm(IMainView view);
        
        
    }
}
