using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command removes selected tiles
    /// </summary>
    public class EditRemoveCommand : ICommand
    {
        #region Fields

        private Layer layer;

        private Rectangle selectionBox;

        private int tileWidth;
        private int tileHeight;

        private int[,] previousBrush;

        #endregion

        #region Initialize

        public EditRemoveCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight)
        {
            this.layer = layer;

            this.selectionBox = selectionBox;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove tiles inside selection box
        /// </summary>
        public void Execute()
        {
            int[,] brush = new int[selectionBox.Height / tileHeight, selectionBox.Width / tileWidth];

            for (int y = 0; y < selectionBox.Height / tileHeight; y++)
            {
                for (int x = 0; x < selectionBox.Width / tileWidth; x++)
                {
                    brush[y, x] = layer.Rows[y + (selectionBox.Y / tileHeight)].Columns[x + (selectionBox.X / tileWidth)].TileID;
                    layer.Rows[y + (selectionBox.Y / tileHeight)].Columns[x + (selectionBox.X / tileWidth)].TileID = -1;
                }
            }

            previousBrush = brush;
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            for (int y = 0; y < selectionBox.Height / tileHeight; y++)
            {
                for (int x = 0; x < selectionBox.Width / tileWidth; x++)
                {
                    layer.Rows[y + (selectionBox.Y / tileHeight)].Columns[x + (selectionBox.X / tileWidth)].TileID = previousBrush[y, x];
                }
            }
        }

        #endregion
    }
}
