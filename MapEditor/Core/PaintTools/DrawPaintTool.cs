using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Presenters;
using MapEditor.Models;


namespace MapEditor.Core.PaintTools
{
    /// <summary>
    ///Paint brush tool state. Changes the map state to paint brush.
    ///Map state to enable drawing tilebrushes to map
    /// </summary>
    public class DrawPaintTool : IPaintTool
    {
        #region Fields

        private readonly MainRenderPresenter presenter;

        private bool isMouseLeftPressed;

        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private TileBrushCollection tileBrushes;

        #endregion

        #region Initialize

        public DrawPaintTool(MainRenderPresenter parent)
        {
            this.presenter = parent;

            tileBrushes = new TileBrushCollection();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create tilebrush at mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseLeftPressed = true;

                currentMousePosition = presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                if (presenter.TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = presenter.TileBrushValues,
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
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                previousMousePosition = currentMousePosition;
                currentMousePosition = presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location));

                if (currentMousePosition != previousMousePosition)
                {
                    if (presenter.TileBrushValues != null)
                    {
                        tileBrushes.AddTileBrush(new TileBrush()
                        {
                            Brush = presenter.TileBrushValues,
                            Position = currentMousePosition,
                        });
                    }
                }
            }
            else
            {
                currentMousePosition = presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                if (presenter.TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = presenter.TileBrushValues,
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
        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                presenter.DrawTileBrushes(tileBrushes);

                tileBrushes.ClearBrushes();
            }
        }

        /// <summary>
        /// Draw temporary tilebrushes before they are applied
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileBrushes.Draw(spriteBatch, presenter.Tilesets[presenter.TilesetIndex]);
        }

        #endregion
    }
}
