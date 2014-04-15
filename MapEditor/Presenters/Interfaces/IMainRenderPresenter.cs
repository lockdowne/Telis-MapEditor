using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;
using MapEditor.Core.PaintTools;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public interface IMainRenderPresenter
    {
        List<Tileset> Tilesets { get; set; }
        List<Layer> Layers { get; set; }

        event Action MapChanged;

        int[,] TileBrushValues { get; set; }

        int LayerIndex {  get; set; }
        int TilesetIndex { get; set; }

        void SetPaintTool(PaintTool paintTool);
        void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight, CheckedListBox checkedListBox);
        void AddTileset(string texturePath, int tileWidth, int tileHeight, Action<string, int, int> createTileset, Action<string> removeTileset);
        void RemoveTileset();
        void AddLayer(CheckedListBox checkedListBox, int mapWidth, int mapHeight);
        void RemoveLayer(CheckedListBox checkedListBox);
        void CloneLayer(CheckedListBox checkedListBox);
        void SetLayerVisibility(bool isVisible);
        void RaiseLayer(CheckedListBox checkedListBox);
        void LowerLayer(CheckedListBox checkedListBox);
        void CopySelection();
        void CutSelection();
        void OffsetMap(int offsetX, int offsetY);
        void ResizeMap(int mapWidth, int mapHeight, NumericUpDownEx mapWidthNumeric, NumericUpDownEx mapHeightNumeric);
        void ResizeTiles(int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric, List<Action<int,int>> setTileDimensions);
        void Undo();
        void Redo();    
    }
}
