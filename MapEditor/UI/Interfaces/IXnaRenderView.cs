using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;

namespace MapEditor.UI
{
    public interface IXnaRenderView
    {
        event MouseEventHandler OnXnaDown;
        event MouseEventHandler OnXnaUp;
        event MouseEventHandler OnXnaMove;
        event MouseEventHandler OnXnaWheel;
        
        event Action OnInitialize;
        event Action<SpriteBatch> OnDraw;        

        GraphicsDevice GetGraphicsDevice { get; }

        string KeyName { get; }

    }
}
