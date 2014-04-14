using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    public interface IMinimapPresenter
    {
        void GenerateMinimap(List<Layer> layers, List<Tileset> tilesets);

        void ShowForm(IMainView parent);

        void SetFormSize(int width, int height);
    }
}
