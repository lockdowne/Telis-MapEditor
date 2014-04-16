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

        private Vector2? beginSelectionBox;
        private Vector2? endSelectionBox;

        private bool isMouseRightPressed;

      

        public ErasePaintTool(MainRenderPresenter parent)
        {
            presenter = parent;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (presenter == null)
                return;

            if (e.Button == MouseButtons.Right)
            {

                isMouseRightPressed = true;

                beginSelectionBox = null;
                endSelectionBox = null;

                presenter.BeginSelectionBox = null;
                presenter.EndSelectionBox = null;

                beginSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));
                //presenter.Layers[presenter.LayerIndex].Rows[(int)MathHelper.Clamp(mousePosition.Y, 0, presenter.Layers.FirstOrDefault().MapHeight)].Columns[(int)MathHelper.Clamp(mousePosition.X, 0, presenter.Layers.FirstOrDefault().MapHeight)].TileID = -1;
                presenter.BeginSelectionBox = beginSelectionBox;
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseRightPressed)
            {
                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                presenter.EndSelectionBox = endSelectionBox;
            }
                //presenter.Layers[presenter.LayerIndex].Rows[(int)MathHelper.Clamp(mousePosition.Y, 0, presenter.Layers.FirstOrDefault().MapHeight)].Columns[(int)MathHelper.Clamp(mousePosition.X, 0, presenter.Layers.FirstOrDefault().MapHeight)].TileID = -1;
            
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseRightPressed)
            {
                isMouseRightPressed = false;

                endSelectionBox = presenter.SnapToGrid(new Vector2(MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).X, 0, presenter.MapWidth * presenter.TileWidth),
                    MathHelper.Clamp(presenter.InvertCameraMatrix(e.Location).Y, 0, presenter.MapHeight * presenter.TileHeight)));

                presenter.EndSelectionBox = endSelectionBox;

                presenter.RemoveTiles();
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
        }
    }
}
