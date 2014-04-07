using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public interface IMainRenderPresenter
    {
        int[,] TileBrushValues { get; set; }

        void AddTileset(string texturePath, int tileWidth, int tileHeight, ITilesetPresenter tilesetPresenter);
        void AddTilesetImage(string path, int tileWidth, int tileHeight);
        void RemoveTileset();
        void AddLayer();
        void AddLayer(int width, int height);
        void RemoveLayer();
        void SetLayerVisibility();        
    }
}
