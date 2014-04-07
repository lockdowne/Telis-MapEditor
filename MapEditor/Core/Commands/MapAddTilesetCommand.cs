using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class MapAddTilesetCommand : ICommand
    {
        private List<Tileset> currentTilesets;

        private Tileset currentTileset;

        private string path;

        private int width;
        private int height;

        private GraphicsDevice graphics;

        public MapAddTilesetCommand(List<Tileset> tilesets, string texturePath, int tileWidth, int tileHeight, GraphicsDevice graphicsDevice, ClosableTabControl tabControl)
        {
            currentTilesets = tilesets;

            path = texturePath;

            width = tileWidth;
            height = tileHeight;

            graphics = graphicsDevice;
        }

        public void Execute()
        {
            Texture2D texture;

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                texture = Texture2D.FromStream(graphics, fileStream);
            }

            currentTileset = new Tileset()
            {
                Texture = texture,
                TexturePath = path,
                TileWidth = width,
                TileHeight = height,
            };

            currentTilesets.Add(currentTileset);
        }

        public void UnExecute()
        {
            currentTilesets.Remove(currentTileset);
        }
    }
}
