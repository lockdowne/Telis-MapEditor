using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace MapEditor.Models
{
    /// <summary>
    /// Created for serialization purposes
    /// </summary>
    public class TileBrush
    {
        public int[,] Brush { get; set; }

        public Vector2 Position { get; set; }
    }
}