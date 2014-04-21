using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Models;

namespace MapEditor.Core.PaintTools
{
    public class DrawPaintTool : IPaintTool
    {
        #region Fields

        private Map map;

        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private TileBrushCollection tileBrushes;

        private bool isMouseLeftPressed;

        #endregion
        
        #region Initialize

        public DrawPaintTool(Map map)
        {
            this.map = map;

            tileBrushes = new TileBrushCollection();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create tilebrush at mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseLeftPressed = true;

                currentMousePosition = map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                if (map.TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = map.TileBrushValues,
                        Position = currentMousePosition,
                    });
                }
            }
        }

        /// <summary>
        /// Create tilebrush at mouse positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                previousMousePosition = currentMousePosition;
                currentMousePosition = map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location));

                if (currentMousePosition != previousMousePosition)
                {
                    if (map.TileBrushValues != null)
                    {
                        tileBrushes.AddTileBrush(new TileBrush()
                        {
                            Brush = map.TileBrushValues,
                            Position = currentMousePosition,
                        });
                    }
                }
            }
            else
            {
                currentMousePosition = map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                if (map.TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = map.TileBrushValues,
                        Position = currentMousePosition,
                    });
                }
            }
        }

        /// <summary>
        /// Apply tilebrushes to map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                map.DrawTileBrushes(tileBrushes);

                tileBrushes.ClearBrushes();
            }
        }

        /// <summary>
        /// Draw temporary tilebrushes before they are applied
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileBrushes.Draw(spriteBatch, map.Tileset);
        }

        #endregion
    }
}
