using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Core.Controls;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that sets the dimensions of the tiles
    /// </summary>
    public class TileResizeCommand : ICommand
    {
        #region Fields

        private Tileset tileset;

        private int currentTileWidth;
        private int previousTileWidth;
        private int currentTileHeight;
        private int previousTileHeight;

        #endregion

        #region Initialize

        public TileResizeCommand(Tileset tileset, int tileWidth, int tileHeight)
        {
            this.tileset = tileset;

            this.currentTileWidth = tileWidth;
            this.currentTileHeight = tileHeight;

            // TODO: Check bounds
            this.previousTileWidth = tileset.TileWidth;
            this.previousTileHeight = tileset.TileHeight;

        }
            

        #endregion

        #region Methods

        /// <summary>
        /// Resize tiles
        /// </summary>
        public void Execute()
        {
            tileset.TileWidth = currentTileWidth;
            tileset.TileHeight = currentTileHeight;
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            tileset.TileWidth = previousTileWidth;
            tileset.TileHeight = previousTileHeight;
        }

        #endregion
    }
}
