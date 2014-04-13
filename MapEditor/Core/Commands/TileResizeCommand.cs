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

        private List<Tileset> tilesets;

        private int currentTileWidth;
        private int previousTileWidth;
        private int currentTileHeight;
        private int previousTileHeight;

        private NumericUpDownEx tileWidthNumeric;
        private NumericUpDownEx tileHeightNumeric;

        private List<Action<int,int>> setTileDimensions;

        #endregion

        #region Initialize

        public TileResizeCommand(List<Tileset> tilesets, int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric, List<Action<int, int>> setTileDimensions)
        {
            if (tilesets == null)
                return;

            this.tilesets = tilesets;

            this.currentTileWidth = tileWidth;
            this.currentTileHeight = tileHeight;

            // TODO: Check bounds
            this.previousTileWidth = tilesets.FirstOrDefault().TileWidth;
            this.previousTileHeight = tilesets.FirstOrDefault().TileHeight;

            this.tileWidthNumeric = tileWidthNumeric;
            this.tileHeightNumeric = tileHeightNumeric;

            this.setTileDimensions = setTileDimensions;

        }
            

        #endregion

        #region Methods

        /// <summary>
        /// Resize tiles
        /// </summary>
        public void Execute()
        {           
            tilesets.ForEach(tile => { tile.TileWidth = currentTileWidth; tile.TileHeight = currentTileHeight; });

            tileWidthNumeric.Value = currentTileWidth;
            tileHeightNumeric.Value = currentTileHeight;

            setTileDimensions.ForEach(tile => tile(currentTileWidth, currentTileHeight));
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            tilesets.ForEach(tile => { tile.TileWidth = previousTileWidth; tile.TileHeight = previousTileHeight; });

            tileWidthNumeric.Value = previousTileWidth;
            tileHeightNumeric.Value = previousTileHeight;

            setTileDimensions.ForEach(tile => tile(previousTileWidth, previousTileHeight));
        }

        #endregion
    }
}
