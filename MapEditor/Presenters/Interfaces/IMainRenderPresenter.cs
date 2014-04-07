using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Presenters
{
    public interface IMainRenderPresenter
    {
        int[,] TileBrushValues { get; set; }

        int LayerIndex {  get; set; }
        int TilesetIndex { get; set; }

        void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight, CheckedListBox checkedListBox);
        void AddTileset(string path, int tileWidth, int tileHeight);
        void RemoveTileset();
        void AddLayer(CheckedListBox checkedListBox);
        void RemoveLayer(CheckedListBox checkedListBox);
        void SetLayerVisibility();        
    }
}
