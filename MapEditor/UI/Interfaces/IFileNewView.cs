using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.UI;

namespace MapEditor.UI
{
    public interface IFileNewView
    {
        event EventHandler Browse;
        event EventHandler Confirm;
        event EventHandler Cancel;
        event EventHandler ValueChanged;

        string GetFileName { get; }
        string GetTilesetPath { get; }
        string SetDisplay { set; }

        int GetMapWidth { get; }
        int GetMapHeight { get; }
        int GetTileWidth { get; }
        int GetTileHeight { get; }

        DialogResult ShowForm();

        void CloseForm();
        void DisplayMessage(string message);
        void SetTilesetPath(string path);
    }
}
