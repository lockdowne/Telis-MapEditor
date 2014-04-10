using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Presenters
{
   
    public interface IFileNewPresenter
    {
        event Action Confirmed;

        string MapName { get; }
        string TilesetPath { get; }

        int MapWidth { get; }
        int MapHeight { get; }
        int TileWidth { get; }
        int TileHeight { get; }

        void Load();
    }
}