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
    public class ErasePaintTool : IPaintTool
    {
        private readonly MainRenderPresenter presenter;

        private bool isMouseLeftPressed;

        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private TileBrushCollection tileBrushes;

        public ErasePaintTool(MainRenderPresenter parent)
        {
            presenter = parent;

            tileBrushes = new TileBrushCollection();
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseLeftPressed = true;

                currentMousePosition = presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                tileBrushes.AddTileBrush(new TileBrush()
                {
                    Brush = new int[,] {{-1}},
                    Position = currentMousePosition,
                });

                //presenter.Layers[presenter.LayerIndex].Rows[(int)MathHelper.Clamp(mousePosition.Y, 0, presenter.Layers.FirstOrDefault().MapHeight)].Columns[(int)MathHelper.Clamp(mousePosition.X, 0, presenter.Layers.FirstOrDefault().MapHeight)].TileID = -1;

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
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = new int[,] {{-1}},
                        Position = currentMousePosition,
                    });
                }
                //presenter.Layers[presenter.LayerIndex].Rows[(int)MathHelper.Clamp(mousePosition.Y, 0, presenter.Layers.FirstOrDefault().MapHeight)].Columns[(int)MathHelper.Clamp(mousePosition.X, 0, presenter.Layers.FirstOrDefault().MapHeight)].TileID = -1;
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                presenter.RemoveTiles(tileBrushes);

                tileBrushes.ClearBrushes();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //tileBrushes.Draw(spriteBatch, presenter.Tilesets[presenter.TilesetIndex]);
        }
    }
}
