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
    public class DrawPaintTool : IPaintTool
    {
        private readonly MainRenderPresenter presenter;

        private bool isMouseLeftPressed;

        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private TileBrushCollection tileBrushes;

        public DrawPaintTool(MainRenderPresenter parent)
        {
            this.presenter = parent;

            tileBrushes = new TileBrushCollection();
        }

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

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                presenter.DrawTileBrushes(tileBrushes);

                tileBrushes.ClearBrushes();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Tileset tileset)
        {
            tileBrushes.Draw(spriteBatch, tileset);
        }
    }
}
