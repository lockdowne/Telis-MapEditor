using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.UI
{
    public interface IMinimapView
    {
        event Action<SpriteBatch> OnXnaDraw;
        event Action OnXnaInitialize;
        event MouseEventHandler OnXnaDown;
        event MouseEventHandler OnXnaUp;
        event MouseEventHandler OnXnaMove;
        event MouseEventHandler OnXnaWheel;

        GraphicsDevice GraphicsDevice { get; }

        int MinimapWidth { get; }
        int MinimapHeight { get; }

        void ShowWindow(IMainView parent);
        void HideWindow();
    }
}
