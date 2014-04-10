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

        private List<TileBrush> currentTileBrushes;
        private List<TileBrush> previousTileBrushes;

        #endregion

        #region Initialize

        public EditRemoveCommand(Layer layer, List<TileBrush> brushes)
        {
            this.layer = layer;

            this.currentTileBrushes = new List<TileBrush>(brushes.AsEnumerable());

            this.previousTileBrushes = new List<TileBrush>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Store previous tiles and replace them with new tilebrushes
        /// </summary>
        public void Execute()
        {
            currentTileBrushes.ForEach(brush =>
            {
                int[,] previousBrush = new int[brush.Brush.GetLength(0), brush.Brush.GetLength(1)];

                // Get previous tiles
                for (int y = 0; y < brush.Brush.GetLength(0); y++)
                {
                    for (int x = 0; x < brush.Brush.GetLength(1); x++)
                    {
                        previousBrush[y, x] = layer.Rows[(int)(y + brush.Position.Y)].Columns[(int)(x + brush.Position.X)].TileID;
                    }
                }

                // Add previous tiles
                previousTileBrushes.Add(new TileBrush()
                {
                    Brush = previousBrush,
                    Position = brush.Position,
                });

            });

            currentTileBrushes.ForEach(brush =>
            {
                // Draw new tiles
                for (int y = 0; y < brush.Brush.GetLength(0); y++)
                {
                    for (int x = 0; x < brush.Brush.GetLength(1); x++)
                    {
                        layer.Rows[(int)(y + brush.Position.Y)].Columns[(int)(x + brush.Position.X)].TileID = brush.Brush[y, x];
                    }
                }
            });
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            previousTileBrushes.ForEach(brush =>
            {
                for (int y = 0; y < brush.Brush.GetLength(0); y++)
                {
                    for (int x = 0; x < brush.Brush.GetLength(1); x++)
                    {
                        layer.Rows[(int)(y + brush.Position.Y)].Columns[(int)(x + brush.Position.X)].TileID = brush.Brush[y, x];
                    }
                }
            });
        }

        #endregion
    }
}
