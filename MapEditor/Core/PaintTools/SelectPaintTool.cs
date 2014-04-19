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

            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                isMouseRightPressed = true;

                beginSelectionBox = null;
                endSelectionBox = null;

                presenter.BeginSelectionBox = null;
                presenter.EndSelectionBox = null;

                beginSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                presenter.BeginSelectionBox = beginSelectionBox;
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (isMouseRightPressed)
            {
                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                presenter.EndSelectionBox = endSelectionBox;
            }
        }


        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (isMouseRightPressed)
            {
                isMouseRightPressed = false;               

                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                //presenter.BeginSelectionBox = beginSelectionBox;
                presenter.EndSelectionBox = endSelectionBox;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        #endregion
    }
}
