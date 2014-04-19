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
        Camera MinimapCamera { get; set; }

        event Action<Camera> MinimapChanged;

        bool IsScrolling { get; set; }

        void GenerateMinimap(List<Layer> layers, List<Tileset> tilesets);

        void ShowForm(IMainView parent);

        void SetFormSize(int width, int height);
    }
}
