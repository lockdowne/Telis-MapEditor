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
    public class SelectPaintTool : IPaintTool
    {
        private readonly MainRenderPresenter presenter;

        private Vector2? beginSelectionBox;
        private Vector2? endSelectionBox;       


        private bool isMouseRightPressed;

        public Rectangle SelectionBox
        {
            get
            {
                if (beginSelectionBox == null || endSelectionBox == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(beginSelectionBox.Value.X, endSelectionBox.Value.X),
                   (int)Math.Min(beginSelectionBox.Value.Y, endSelectionBox.Value.Y),
                   (int)Math.Abs(beginSelectionBox.Value.X - endSelectionBox.Value.X),
                   (int)Math.Abs(beginSelectionBox.Value.Y - endSelectionBox.Value.Y));
            }
        }

        public SelectPaintTool(MainRenderPresenter parent)
        {
            presenter = parent;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isMouseRightPressed = true;

                endSelectionBox = null;

                beginSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth), MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseRightPressed)
            {
                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth), MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));
            }
        }


        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseRightPressed)
            {
                isMouseRightPressed = false;

                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth), MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                presenter.BeginSelectionBox = beginSelectionBox;
                presenter.EndSelectionBox = endSelectionBox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (SelectionBox == null)
                return;

            DrawingTool.DrawRectangle(spriteBatch, presenter.Pixel, SelectionBox, Color.White, 2);
        }
    }
}
