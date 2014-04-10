using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that sets the dimensions of the tiles
    /// </summary>
    public class TileResizeCommand : ICommand
    {
        #region Fields

        private List<Tileset> tilesets;

        private int currentWidth;
        private int currentHeight;
        private int previousWidth;
        private int previousHeight;

        #endregion

        #region Initialize

        public TileResizeCommand(List<Tileset> tilesets, int tileWidth, int tileHeight)
        {
            this.tilesets = tilesets;

            currentWidth = tileWidth;
            currentHeight = tileHeight;
            
            previousWidth = tilesets.FirstOrDefault().TileWidth;
            previousHeight = tilesets.FirstOrDefault().TileHeight;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resize tiles
        /// </summary>
        public void Execute()
        {           
            tilesets.ForEach(tile => { tile.TileWidth = currentWidth; tile.TileHeight = currentHeight; });
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            tilesets.ForEach(tile => { tile.TileWidth = previousWidth; tile.TileHeight = previousHeight; });
        }

        #endregion
    }
}
