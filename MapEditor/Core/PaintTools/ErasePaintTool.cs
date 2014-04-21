using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Models;

namespace MapEditor.Core.PaintTools
{
    public class ErasePaintTool : IPaintTool
    {
        #region Fields

        private Map map;

        private Vector2? selectionBoxA;
        private Vector2? selectionBoxB;

        private bool isMousePressed;

        #endregion

        #region Initialize

        public ErasePaintTool(Map map)
        {
            this.map = map;
        }

        #endregion

        #region Methods

        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right || e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isMousePressed = true;

                ClearSelectionBox();

                map.ClearSelectionBox();

                selectionBoxA = map.SnapToGrid(new Vector2(MathHelper.Clamp(map.InvertCameraMatrix(e.Location).X, 0, map.MapWidth * map.TileWidth),
                    MathHelper.Clamp(map.InvertCameraMatrix(e.Location).Y, 0, map.MapHeight * map.TileHeight)));
               
                map.SelectionBoxA = selectionBoxA;
            }
        }

        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMousePressed)
            {
                selectionBoxB = map.SnapToGrid(new Vector2(MathHelper.Clamp(map.InvertCameraMatrix(e.Location).X, 0, map.MapWidth * map.TileWidth),
                    MathHelper.Clamp(map.InvertCameraMatrix(e.Location).Y, 0, map.MapHeight * map.TileHeight)));

                map.SelectionBoxB = selectionBoxB;
            }
        }

        public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMousePressed)
            {
                isMousePressed = false;

                selectionBoxB = map.SnapToGrid(new Vector2(MathHelper.Clamp(map.InvertCameraMatrix(e.Location).X, 0, map.MapWidth * map.TileWidth),
                    MathHelper.Clamp(map.InvertCameraMatrix(e.Location).Y, 0, map.MapHeight * map.TileHeight)));

                map.SelectionBoxB = selectionBoxB;

                map.RemoveTiles();
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        private void ClearSelectionBox()
        {
            selectionBoxA = null;
            selectionBoxB = null;
        }

        #endregion
    }
}
