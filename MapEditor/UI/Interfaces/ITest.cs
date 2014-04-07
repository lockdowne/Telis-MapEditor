using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.UI
{
    public interface ITest
    {
        event EventHandler OnXnaMouseDown;
        event Action<SpriteBatch> OnDraw;

        void CreateTab(string name);
        void DisplayMessage(string message);
        void ShowForm();
    }
}
