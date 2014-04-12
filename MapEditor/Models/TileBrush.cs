using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace MapEditor.Models
{
    public class TileBrush
    {
        #region Properties

        public int[,] Brush { get; set; }

        public Vector2 Position { get; set; }

        #endregion
    }
}