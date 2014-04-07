using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class TileResizeCommand : ICommand
    {
        private List<Tileset> currentTilesets;

        private int currentWidth;
        private int currentHeight;
        private int previousWidth;
        private int previousHeight;

        public TileResizeCommand(List<Tileset> tilesets, int tileWidth, int tileHeight)
        {
            currentTilesets = tilesets;

            currentWidth = tileWidth;
            currentHeight = tileHeight;
        }

        public void Execute()
        {
            previousWidth = currentTilesets.FirstOrDefault().TileWidth;
            previousHeight = currentTilesets.FirstOrDefault().TileHeight;

            currentTilesets.ForEach(tile => { tile.TileWidth = currentWidth; tile.TileHeight = currentHeight; });
        }

        public void UnExecute()
        {
            currentTilesets.ForEach(tile => { tile.TileWidth = previousWidth; tile.TileHeight = previousHeight; });
        }
    }
}
