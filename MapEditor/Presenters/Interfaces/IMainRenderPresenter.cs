using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Presenters
{
    public interface IMainRenderPresenter
    {
        int[,] TileBrushValues { get; set; }

        void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight);
        void AddTileset(string path, int tileWidth, int tileHeight);
        void RemoveTileset();
        void AddLayer();
        void AddLayer(int width, int height);
        void RemoveLayer();
        void SetLayerVisibility();        
    }
}
