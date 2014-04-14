using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.UI;

namespace MapEditor.Models
{
    public class Minimap
    {
        private List<Texture2D> textures = new List<Texture2D>();    
 
        public void Draw(SpriteBatch spriteBatch)
        {
            textures.ForEach(texture =>
                {
                    spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                });
        }

        public void GenerateMinimap(GraphicsDevice graphicsDevice, List<Layer> layers, List<Tileset> tilesets)
        {
            textures.Clear();

            tilesets.ForEach(tileset =>
                {
                    layers.ForEach(layer =>
                        {
                            Texture2D texture; 

                            Color[] colors = new Color[layer.MapWidth * layer.MapHeight];

                            int x = 0;
                            int y = -1;

                            for (int i = 0; i < layer.MapWidth * layer.MapHeight; i++)
                            {
                                if (i % layer.MapWidth == 0)
                                    y++;

                                x = i % layer.MapWidth;

                                colors[i] = tileset.GetCenterPixelColor(layer.Rows[y].Columns[x].TileID);
                            }

                            texture = new Texture2D(graphicsDevice, layer.MapWidth, layer.MapHeight);
                            texture.SetData<Color>(colors);

                            textures.Add(texture);
                        });
                });
        }
    }
}
