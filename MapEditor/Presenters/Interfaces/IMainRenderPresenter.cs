using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.PaintTools;

namespace MapEditor.Presenters
{
    public interface IMainRenderPresenter
    {
        int[,] TileBrushValues { get; set; }

        int LayerIndex {  get; set; }
        int TilesetIndex { get; set; }

        void SetPaintTool(PaintTool paintTool);
        void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight, CheckedListBox checkedListBox);
        void AddTileset(string path, int tileWidth, int tileHeight);
        void RemoveTileset();
        void AddLayer(CheckedListBox checkedListBox);
        void RemoveLayer(CheckedListBox checkedListBox);
        void CloneLayer(CheckedListBox checkedListBox);
        void SetLayerVisibility(bool isVisible);
        void RaiseLayer(CheckedListBox checkedListBox);
        void LowerLayer(CheckedListBox checkedListBox);
        void CopySelection();
        void CutSelection();
        void Undo();
        void Redo();

    
    }
}
