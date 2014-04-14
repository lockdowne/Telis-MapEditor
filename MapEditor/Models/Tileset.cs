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
        #region Properties

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
                return Rectangle.Empty;

            if (TileWidth <= 0)
                return Rectangle.Empty;

            int tileY = tileIndex / (Texture.Width / TileWidth);
            int tileX = tileIndex % (Texture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }

        public Color GetCenterPixelColor(int tileIndex)
        {
            if (Texture == null)
                return Color.Transparent;

            if (tileIndex < 0)
                return Color.Transparent;

            Rectangle sourceRectangle = GetSourceRectangle(tileIndex);

            Color[] tileColors = new Color[sourceRectangle.Width * sourceRectangle.Height];

            Texture.GetData(0, sourceRectangle, tileColors, 0, tileColors.Length);

            return tileColors[(sourceRectangle.Width * sourceRectangle.Height) / 2];           
        }

        #endregion
    }
}
