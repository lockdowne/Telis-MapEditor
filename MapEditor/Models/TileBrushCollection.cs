using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Models
{
    public class TileBrushCollection
    {
        private List<TileBrush> tileBrushes = new List<TileBrush>();

        public List<TileBrush> TileBrushes
        {
            get { return tileBrushes; }
        }

        public void Draw(SpriteBatch spriteBatch, Tileset tileset)
        {
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

        public void AddTileBrush(TileBrush brush)
        {
            tileBrushes.ForEach(tileBrush =>
                {
                    if (tileBrush.Position == brush.Position)
                        tileBrushes.Remove(tileBrush);                    
                });

            tileBrushes.Add(brush);
        }

        public void ClearBrushes()
        {
            tileBrushes.Clear();
        }
    }
}
