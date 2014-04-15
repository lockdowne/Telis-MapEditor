using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Core.Commands
{
    public class MapAddTilesetCommand : ICommand
    {
        private List<Tileset> tilesets;

        private Tileset tileset;

        private string texturePath;

        private int tileWidth;
        private int tileHeight;

        private GraphicsDevice graphicsDevice;

        private Action<string, int, int> createTileset;
        private Action<string> removeTileset;

        public MapAddTilesetCommand(List<Tileset> tilesets, string texturePath, int tileWidth, int tileHeight, GraphicsDevice graphicsDevice, Action<string, int, int> createTileset, Action<string> removeTileset)
        {
            this.tilesets = tilesets;

            this.texturePath = texturePath;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            this.graphicsDevice = graphicsDevice;

            this.createTileset = createTileset;
            this.removeTileset = removeTileset;
        }

        public void Execute()
        {
            Texture2D texture;

            using (FileStream fileStream = new FileStream(texturePath, FileMode.Open))
            {
                texture = Texture2D.FromStream(graphicsDevice, fileStream);
            }

            tileset = new Tileset()
            {
                Texture = texture,
                TexturePath = texturePath,
                TileWidth = tileWidth,
                TileHeight = tileHeight,
            };

            tilesets.Add(tileset);

            createTileset(texturePath, tileWidth, tileHeight);
        }

        public void UnExecute()
        {
            tilesets.Remove(tileset);

            string key = Path.GetFileName(texturePath).Split('.')[0];

            removeTileset(key);
        }
    }
}
