using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;


namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that removes selected tiles and copies removed tiles as a new tilebrush
    /// </summary>
    public class EditCutCommand : ICommand
    {
        #region Fields

        private Layer layer;

        private Rectangle selectionBox;

        private int tileWidth;
        private int tileHeight;

        private int[,] previousBrush;

        private List<int[,]> clipboard;

        #endregion

        #region Initialize

        public EditCutCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, List<int[,]> clipboard)
        {
            this.layer = layer;

            this.selectionBox = selectionBox;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;          

            this.clipboard = clipboard;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Copies selected tiles to a new tilebrush and removes the selected tiles
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

            clipboard.Add(brush);
        }

        /// <summary>
        /// Inverts the execute method
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

            clipboard.Clear();
        }

        #endregion
    }
}
