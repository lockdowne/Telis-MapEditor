using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command copies selected tiles to new tilebrush
    /// </summary>
    public class EditCopyCommand : ICommand
    {
        #region Fields

        private Layer layer;

        private Rectangle selectionBox;

        private int tileWidth;
        private int tileHeight;

        private List<int[,]> clipboard;

        #endregion

        #region Initialize

        public EditCopyCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, List<int[,]> clipboard)
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
        /// Copies selected tiles to new tilebrush
        /// </summary>
        public void Execute()
        {
            int[,] brush = new int[selectionBox.Height / tileHeight, selectionBox.Width / tileWidth];

            for (int y = 0; y < selectionBox.Height / tileHeight; y++)
            {
                for (int x = 0; x < selectionBox.Width / tileWidth; x++)
                {
                    brush[y,x] = layer.Rows[y + (selectionBox.Y / tileHeight)].Columns[x + (selectionBox.X / tileWidth)].TileID;
                }
            }

            clipboard.Add(brush);

        }

        public void UnExecute() { }

        #endregion
    }
}
