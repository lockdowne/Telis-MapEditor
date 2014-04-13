using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Helpers;

namespace MapEditor.UI
{
    /// <summary>
    /// Base for main UI
    /// </summary>
    public interface IMainView
    {
        event EventHandler FileNew;
        event EventHandler FileOpen;
        event EventHandler FileSave;
        event EventHandler FileSaveAs;
        event EventHandler FileClose;
        event EventHandler FileCloseAll;
        event EventHandler FileExit;

        event EventHandler EditUndo;
        event EventHandler EditRedo;
        event EventHandler EditCopy;
        event EventHandler EditCut;
        event EventHandler EditDraw;
        event EventHandler EditErase;
        event EventHandler EditSelect;
        event EventHandler EditFill;

        event EventHandler ViewShowGrid;
        event EventHandler ViewZoomIn;
        event EventHandler ViewZoomOut;
        event EventHandler ViewShowLayerForm;
        event EventHandler ViewShowTilesetForm;
        event EventHandler ViewShowMinimapForm;

        event EventHandler LayerAdd;
        event EventHandler LayerRemove;
        event EventHandler LayerClone;
        event EventHandler LayerRaise;
        event EventHandler LayerLower;

        event EventHandler MapOffset;
        event EventHandler MapResize;
        event EventHandler TileResize;
        event EventHandler AddTileset;
        event EventHandler RemoveTileset;

        event FormClosingEventHandler ViewClosing;

        IXnaRenderView GetCurrentView { get; }

        void AddView(string fileName, IXnaRenderView renderer);        
    }
}
