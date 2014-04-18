using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.UI
{
    public interface IMinimapView
    {
        event Action<SpriteBatch> OnDraw;
        event Action OnInitialize;
        event MouseEventHandler OnXnaDown;
        event MouseEventHandler OnXnaUp;
        event MouseEventHandler OnXnaMove;

        GraphicsDevice GraphicsDevice { get; }

        void SetFormSize(int width, int height);
        void ShowForm(IMainView view);
        void CloseForm();
    }
}
