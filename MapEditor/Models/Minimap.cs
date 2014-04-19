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
        public const int MINIMAP_WIDTH = 284;
        public const int MINIMAP_HEIGHT = 261;

        public bool IsScrolling { get; set; }

        private List<Texture2D> textures = new List<Texture2D>();

        private Vector2 scale;
        private Vector2 mapPixelsDimensions;

        public Vector2 CameraScale
        {
            get { return new Vector2(MINIMAP_WIDTH, MINIMAP_HEIGHT) / mapPixelsDimensions; }
        }

        public Camera Camera { get; set; }
       
        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {            
            textures.ForEach(texture =>
            {
                spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            });

            // Draw view rectangle
            if (Camera != null)
            {
                if (!IsScrolling)
                    DrawingTool.DrawRectangle(spriteBatch, pixel,
                        new Rectangle((int)MathHelper.Clamp(Camera.Position.X * CameraScale.X, 0, MINIMAP_WIDTH),
                            (int)MathHelper.Clamp(Camera.Position.Y * CameraScale.Y, 0, MINIMAP_HEIGHT),
                            (int)(CameraScale.X * Camera.ViewportWidth / Camera.Zoom),
                            (int)(CameraScale.Y * Camera.ViewportHeight / Camera.Zoom)),
                            Color.White,
                            1);
                else
                    DrawingTool.DrawRectangle(spriteBatch, pixel,
                       new Rectangle((int)MathHelper.Clamp(Camera.Position.X, 0, MINIMAP_WIDTH),
                           (int)MathHelper.Clamp(Camera.Position.Y, 0, MINIMAP_HEIGHT),
                           (int)(CameraScale.X * Camera.ViewportWidth / Camera.Zoom),
                           (int)(CameraScale.Y * Camera.ViewportHeight / Camera.Zoom)),
                           Color.White,
                           1);
            }
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

            mapPixelsDimensions = new Vector2(layers.First().MapWidth * tilesets.First().TileWidth, layers.First().MapHeight * tilesets.First().TileHeight);

            scale = new Vector2(MINIMAP_WIDTH, MINIMAP_HEIGHT) / new Vector2(textures.First().Width, textures.First().Height);
        }
    }
}
