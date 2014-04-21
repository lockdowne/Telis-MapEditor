using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Models
{
    /// <summary>
    /// Represents a tileset
    /// </summary>
    public class Tileset
    {
        #region Properties

        /// <summary>
        /// Gets or sets tile width
        /// </summary>
        public int TileWidth { get; set; }

        /// <summary>
        /// Gets or sets tile height
        /// </summary>
        public int TileHeight { get; set; }

       /// <summary>
       /// Gets or sets texture directory path
       /// </summary>
        public string TexturePath { get; set; }

        /// <summary>
        /// Gets or sets texture2D
        /// </summary>
        public Texture2D Texture { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Return a rectangle with the size of the tile dimensions
        /// Represents the location on the texture to draw
        /// </summary>
        /// <param name="tileIndex"></param>
        /// <returns></returns>
        public Rectangle GetSourceRectangle(int tileIndex)
        {
            if (Texture == null)
                throw new ArgumentNullException();
            
            int tileY = tileIndex / (Texture.Width / TileWidth);
            int tileX = tileIndex % (Texture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }

        /// <summary>
        /// Get the color of the pixel in the center of selected tile
        /// </summary>
        /// <param name="tileIndex"></param>
        /// <returns></returns>
        public Color GetCenterPixelColor(int tileIndex)
        {
            if (Texture == null)
                throw new ArgumentNullException();

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
