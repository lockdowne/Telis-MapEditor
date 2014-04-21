using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public interface IMainView
    {
        event Action OnFileNew;
        event Action OnFileOpen;
        event Action OnFileSave;
        event Action OnFileSaveAs;
        event Action OnFileClose;
        event Action OnFileCloseAll;
        event Action OnFileExit;
        event Action OnEditUndo;
        event Action OnEditRedo;
        event Action OnEditCopy;
        event Action OnEditCut;
        event Action OnEditDraw;
        event Action OnEditErase;
        event Action OnEditSelect;
        event Action OnEditFill;
        event Action OnViewGrid;
        event Action<bool> OnViewTileset;
        event Action<bool> OnViewLayers;
        event Action<bool> OnViewMinimap;
        event Action OnZoomIn;
        event Action OnZoomOut;
        event Action OnLayerAdd;
        event Action OnLayerRemove;
        event Action OnLayerDuplicate;
        event Action OnLayerRaise;
        event Action OnLayerLower;
        event Action OnLayerVisibility;
        event Action OnLayerRename;
        event Action OnMapOffset;
        event Action OnMapResizeTile;
        event Action OnMapResizeMap;
        event Action<string> OnSelectedTabChanged;
        event ControlEventHandler OnTabClosed;

        XnaRender SelectedXnaTab { get; }
        string SelectedTabName { get; }
        int ViewportWidth { get; }
        int ViewportHeight { get; }

        void AddTab(string name);
        void RemoveTab(string name);
        void RemoveAllTabs();

    }
}
