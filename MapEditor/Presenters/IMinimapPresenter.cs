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
    public interface IMinimapPresenter
    {
        event CameraEventHandler OnCameraChanged;
        event Action OnHiding;
        event Action OnShowing;

        Camera Camera { get; set; }
        bool IsScrolling { get; set; }
        Vector2 MinimapDimensions { set; }
        Vector2 MapDimensions { set; }
        Vector2 MapViewport { set; }

        void GenerateMinimap(List<Layer> layers, Tileset tileset);
        void ShowWindow(IMainView parent);
        void HideWindow();
        void ClearMinimap();
    }
}
