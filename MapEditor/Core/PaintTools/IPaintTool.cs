using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Presenters;
using MapEditor.Models;

namespace MapEditor.Core.PaintTools
{
    [Flags]
    public enum PaintTool
    {
        Draw = 0,
        Erase = 1,
        Select = 2,
        Fill = 3,
    }

    public interface IPaintTool
    {
        void MouseDown(object sender, MouseEventArgs e);
        void MouseMove(object sender, MouseEventArgs e);
        void MouseUp(object sender, MouseEventArgs e);

        void Draw(SpriteBatch spriteBatch);
    }
}
