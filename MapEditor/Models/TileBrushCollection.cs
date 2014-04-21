using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Models
{
    /// <summary>
    /// Represents a collection of tile brushes
    /// </summary>
    public class TileBrushCollection
    {
        #region Fields
        
        private List<TileBrush> tileBrushes = new List<TileBrush>();

        #endregion

        public List<TileBrush> TileBrushes
        {
            get { return tileBrushes; }
        }

        #region Methods
        
        /// <summary>
        /// Draws tilebrushes to screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="tileset"></param>
        public void Draw(SpriteBatch spriteBatch, Tileset tileset)
        {
            if (spriteBatch == null)
                return;

            if (tileset == null)
                return;

            tileBrushes.ForEach(brush =>
                {
                    for (int y = 0; y < brush.Brush.GetLength(0); y++)
                    {
                        for (int x = 0; x < brush.Brush.GetLength(1); x++)
                        {
                            int tileID = brush.Brush[y, x];

                            if (tileID > -1)
                            {
                                spriteBatch.Draw(tileset.Texture,
                                                        new Rectangle(x * tileset.TileWidth + (tileset.TileWidth * (int)brush.Position.X),
                                                            y * tileset.TileHeight + (tileset.TileHeight * (int)brush.Position.Y),
                                                            tileset.TileWidth,
                                                            tileset.TileHeight),
                                                            tileset.GetSourceRectangle(tileID),
                                                            Color.White * 0.5f);
                            }
                        }
                    }
                });
        }

        /// <summary>
        /// Adds new tilebrush to collection
        /// </summary>
        /// <param name="brush"></param>
        public void AddTileBrush(TileBrush brush)
        {
            if (brush == null)
                return;

            tileBrushes.ForEach(tileBrush =>
                {
                    if (tileBrush.Position == brush.Position)
                        tileBrushes.Remove(tileBrush);                    
                });

            tileBrushes.Add(brush);
        }

        /// <summary>
        /// Removes all tilebrushes from collection
        /// </summary>
        public void ClearBrushes()
        {
            tileBrushes.Clear();
        }

        #endregion
    }
}
