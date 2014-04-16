﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;


namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command draws tilebrushes to map
    /// </summary>
    public class EditDrawCommand : ICommand
    {
        #region Fields

        private Layer layer;

        private List<TileBrush> currentTileBrushes;
        private List<TileBrush> previousTileBrushes;

        #endregion

        #region Initialize

        public EditDrawCommand(Layer layer, List<TileBrush> brushes)
        {
            this.layer = layer;

            this.currentTileBrushes = new List<TileBrush>(brushes.AsEnumerable());

            this.previousTileBrushes = new List<TileBrush>();
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Change current tile ids to new tile ids
        /// </summary>
        public void Execute()
        {
            int mapWidth = layer.MapWidth;
            int mapHeight = layer.MapHeight;

            currentTileBrushes.ForEach(brush =>
                {
                    int[,] previousBrush = new int[brush.Brush.GetLength(0), brush.Brush.GetLength(1)];

                    // Get previous tiles
                    for (int y = 0; y < brush.Brush.GetLength(0); y++)
                    {
                        for (int x = 0; x < brush.Brush.GetLength(1); x++)
                        {
                            previousBrush[y, x] = layer.Rows[(int)(MathHelper.Clamp(y + brush.Position.Y, 0, mapHeight - 1))].Columns[(int)(MathHelper.Clamp(x + brush.Position.X, 0, mapWidth - 1))].TileID;
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
                            layer.Rows[(int)(MathHelper.Clamp(y + brush.Position.Y, 0, mapHeight - 1))].Columns[(int)(MathHelper.Clamp(x + brush.Position.X, 0, mapWidth - 1))].TileID = brush.Brush[y, x];
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
