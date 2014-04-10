using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.Helpers;

namespace MapEditor.UI
{
    /// <summary>
    /// Base for main UI
    /// </summary>
    public interface IMainView
    {
        event EventHandler FileNew;
        event EventHandler FileExit;

        event EventHandler EditCopy;
        event EventHandler EditCut;
        event EventHandler EditDraw;
        event EventHandler EditPaste;
        event EventHandler EditRemove;
        event EventHandler EditSelect;

        event EventHandler LayerAdd;
        event EventHandler LayerRemove;
        event EventHandler LayerClone;
        event EventHandler LayerRaise;
        event EventHandler LayerLower;
        event EventHandler LayerVisibility;

        event EventHandler MapOffset;
        event EventHandler MapResize;

        event EventHandler TileResize;

        event EventHandler Undo;
        event EventHandler Redo;
        

        IXnaRenderView GetCurrentView { get; }

        void AddView(string fileName, IXnaRenderView renderer);        
    }
}
