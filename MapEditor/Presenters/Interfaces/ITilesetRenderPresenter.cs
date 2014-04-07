using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public interface ITilesetRenderPresenter
    {
        event Action<int[,]> SendTileBrushValues;

        int[,] GetTileBrush();

        void LoadTexture(string path);
        void SetTileDimesions(int width, int height);
    }
}
