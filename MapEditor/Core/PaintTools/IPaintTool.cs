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
        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnMouseUp(object sender, MouseEventArgs e);

        void Draw(SpriteBatch spriteBatch);
    }
}
