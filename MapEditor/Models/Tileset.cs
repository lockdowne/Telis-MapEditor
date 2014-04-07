using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Models
{
    public class Tileset
    {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        // Tileset texture directory
        public string TexturePath { get; set; }

        // Tileset texture
        public Texture2D Texture { get; set; }

        // Finds the exact tile in tileset to draw
        public Rectangle GetSourceRectangle(int tileIndex)
        {
            if (Texture == null)
                throw new Exception("Texture null no rectangle can be made");

            int tileY = tileIndex / (Texture.Width / TileWidth);
            int tileX = tileIndex % (Texture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
