using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.UI;

namespace MapEditor.UI
{
    /// <summary>
    /// Base for new view
    /// </summary>
    public interface IFileNewView
    {
        event EventHandler Browse;
        event EventHandler Confirm;
        event EventHandler Cancel;
        event EventHandler ValueChanged;

        string FileName { get; }
        string TilesetPath { get; }
        string Display { set; }

        int MapWidth { get; }
        int MapHeight { get; }
        int TileWidth { get; }
        int TileHeight { get; }

        void ShowForm();

        void CloseForm();
        void DisplayMessage(string message);
        void SetTilesetPath(string path);
    }
}
