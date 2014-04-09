using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class EditRemoveCommand : ICommand
    {
        private Layer currentLayer;
        private Layer previousLayer;

        private List<TileBrush> tileBrushes;

        public EditRemoveCommand(Layer layer, List<TileBrush> brushes)
        {
            currentLayer = layer;

            tileBrushes = brushes;
        }

        public void Execute()
        {
            previousLayer = currentLayer;

            tileBrushes.ForEach(brush =>
            {
                for (int y = 0; y < brush.Brush.GetLength(0); y++)
                {
                    for (int x = 0; x < brush.Brush.GetLength(1); x++)
                    {
                        currentLayer.Rows[(int)(y + brush.Position.Y)].Columns[(int)(x + brush.Position.X)].TileID = brush.Brush[y, x];
                    }
                }
            });
        }

        public void UnExecute()
        {

        }
    }
}
