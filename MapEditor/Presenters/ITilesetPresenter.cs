using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Core.EventsArgs;
using MapEditor.Models;
using MapEditor.UI;


namespace MapEditor.Presenters
{
    public interface ITilesetPresenter
    {
        event TilesetEventHandler OnTileBrushChanged;
        event Action OnHiding;
        event Action OnShowing;

        Rectangle SelectionBox { get; }

        void SetTileset(Tileset tileset);
        void SetTileSize(int tileWidth, int tileHeight);
        void ShowWindow(IMainView parent);
        void HideWindow();
    }
}
