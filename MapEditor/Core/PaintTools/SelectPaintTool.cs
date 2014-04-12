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
        #region Fields

        private readonly MainRenderPresenter presenter;

        private Vector2? beginSelectionBox;
        private Vector2? endSelectionBox; 

        private bool isMouseRightPressed;

        #endregion

        #region Properties

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

        #endregion

        #region Initialize

        public SelectPaintTool(MainRenderPresenter parent)
        {
            presenter = parent;
        }

        #endregion

        #region Methods

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (e.Button == MouseButtons.Right)
            {
                isMouseRightPressed = true;

                endSelectionBox = null;

                int tileWidth = 0;
                int tileHeight = 0;
                int mapWidth = 0;
                int mapHeight = 0;

                if (presenter.Tilesets.Count > 0)
                {
                    tileWidth = presenter.Tilesets.FirstOrDefault().TileWidth;
                    tileHeight = presenter.Tilesets.FirstOrDefault().TileHeight;
                }

                if (presenter.Layers.Count > 0)
                {
                    mapWidth = presenter.Layers.FirstOrDefault().MapWidth;
                    mapHeight = presenter.Layers.FirstOrDefault().MapHeight;
                }                

                beginSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, mapWidth * tileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, mapHeight * tileHeight)));
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (isMouseRightPressed)
            {
                int tileWidth = 0;
                int tileHeight = 0;
                int mapWidth = 0;
                int mapHeight = 0;

                if (presenter.Tilesets.Count > 0)
                {
                    tileWidth = presenter.Tilesets.FirstOrDefault().TileWidth;
                    tileHeight = presenter.Tilesets.FirstOrDefault().TileHeight;
                }

                if (presenter.Layers.Count > 0)
                {
                    mapWidth = presenter.Layers.FirstOrDefault().MapWidth;
                    mapHeight = presenter.Layers.FirstOrDefault().MapHeight;
                }    

                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, mapWidth * tileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, mapHeight * tileHeight)));
            }
        }


        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (isMouseRightPressed)
            {
                isMouseRightPressed = false;

                int tileWidth = 0;
                int tileHeight = 0;
                int mapWidth = 0;
                int mapHeight = 0;

                if (presenter.Tilesets.Count > 0)
                {
                    tileWidth = presenter.Tilesets.FirstOrDefault().TileWidth;
                    tileHeight = presenter.Tilesets.FirstOrDefault().TileHeight;
                }

                if (presenter.Layers.Count > 0)
                {
                    mapWidth = presenter.Layers.FirstOrDefault().MapWidth;
                    mapHeight = presenter.Layers.FirstOrDefault().MapHeight;
                }    

                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, mapWidth * tileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, mapHeight * tileHeight)));

                presenter.BeginSelectionBox = beginSelectionBox;
                presenter.EndSelectionBox = endSelectionBox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (SelectionBox == Rectangle.Empty)
                return;

            if (spriteBatch == null)
                return;

            DrawingTool.DrawRectangle(spriteBatch, presenter.Pixel, SelectionBox, Color.White, 2);
        }

        #endregion
    }
}
