using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class EditCopyCommand : ICommand
    {
        private Layer layer;

        private Rectangle selectionBox;

        private int[,] tileBrush;

        private int tileWidth;
        private int tileHeight;

        public EditCopyCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, int[,] tileBrush)
        {
            this.layer = layer;

            this.selectionBox = selectionBox;

            this.tileBrush = tileBrush;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

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

            tileBrush = brush;

        }


        public void UnExecute()
        {

        }
    }
}
