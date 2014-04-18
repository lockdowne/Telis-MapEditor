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
        private const int MINIMAP_WIDTH = 284;
        private const int MINIMAP_HEIGHT = 261;

        private List<Texture2D> textures = new List<Texture2D>();

        private Vector2 scale;

        public Camera Camera { get; set; }
       
        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {            
            textures.ForEach(texture =>
            {
                spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            });

            // Draw view rectangle
            if (Camera != null)
                DrawingTool.DrawRectangle(spriteBatch, pixel,
                    new Rectangle((int)MathHelper.Clamp(Camera.Position.X / MINIMAP_WIDTH * scale.X, 0, MINIMAP_WIDTH),
                        (int)MathHelper.Clamp(Camera.Position.Y  / MINIMAP_HEIGHT * scale.Y, 0, MINIMAP_HEIGHT),
                        (int)(MINIMAP_WIDTH / scale.X),
                        (int)(MINIMAP_HEIGHT / scale.Y)),
                        Color.White,
                        1);
        }

        public void GenerateMinimap(GraphicsDevice graphicsDevice, List<Layer> layers, List<Tileset> tilesets)
        {
            if (layers.Count <= 0)
                return;

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

            scale = new Vector2(MINIMAP_WIDTH, MINIMAP_HEIGHT) / new Vector2(textures.First().Width, textures.First().Height);
        }
    }
}
