using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Models;

namespace MapEditor.Core.PaintTools
{
    public class FillPaintTool : IPaintTool
    {
        #region Fields

        private Map map;

        private Layer tempLayer;

        private bool isMiddleMousePressed;

        private int previousTarget;

        #endregion

        #region Initialize

        public FillPaintTool(Map map)
        {
            this.map = map;
        }

        #endregion

        #region Methods

        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int x = (int)MathHelper.Clamp(map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location)).X, 0, map.MapWidth - 1);
                int y = (int)MathHelper.Clamp(map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location)).Y, 0, map.MapHeight - 1);

                TileBrush tileBrush = new TileBrush() { Brush = map.TileBrushValues, Position = new Vector2(x, y), };

                if (map.CurrentLayer != null)
                    map.Fill(tileBrush, map.CurrentLayer.Rows[y].Columns[x].TileID);                
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                isMiddleMousePressed = true;
        }

        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!isMiddleMousePressed)
            {
                int x = (int)MathHelper.Clamp(map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location)).X, 0, map.MapWidth - 1);
                int y = (int)MathHelper.Clamp(map.PixelsToCoordinate(map.InvertCameraMatrix(e.Location)).Y, 0, map.MapHeight - 1);

                TileBrush tileBrush = new TileBrush() { Brush = map.TileBrushValues, Position = new Vector2(x, y) };

                if (map.CurrentLayer != null)
                    if (map.CurrentLayer.Rows[y].Columns[x].TileID != previousTarget)
                        FloodFill(tileBrush, map.CurrentLayer.Rows[y].Columns[x].TileID);
            }
        }


        public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                isMiddleMousePressed = false;
        }

        private void FloodFill(TileBrush tileBrush, int target)
        {
            //Layer layer = presenter.Layers[presenter.LayerIndex];

            previousTarget = target;

            if (tileBrush == null)
                return;

            Layer layer = new Layer();
            layer.Initialize(map.CurrentLayer.LayerName, map.CurrentLayer.LayerWidth, map.CurrentLayer.LayerHeight);

            for (int layerY = 0; layerY < layer.LayerHeight; layerY++)
            {
                for (int layerX = 0; layerX < layer.LayerWidth; layerX++)
                {
                    layer.Rows[layerY].Columns[layerX].TileID = map.Layers[map.LayerIndex].Rows[layerY].Columns[layerX].TileID;
                }
            }

            Queue<TileBrush> queue = new Queue<TileBrush>();
            queue.Enqueue(tileBrush);


            for (int arrY = 0; arrY < tileBrush.Brush.GetLength(0); arrY++)
            {
                for (int arrX = 0; arrX < tileBrush.Brush.GetLength(1); arrX++)
                {
                    if (layer.Rows[(int)tileBrush.Position.Y].Columns[(int)tileBrush.Position.X].TileID == tileBrush.Brush[arrY, arrX])
                        return;
                }
            }
           

            while (queue.Count > 0)
            {
                TileBrush brush = queue.Dequeue();

                if (layer.Rows[(int)brush.Position.Y].Columns[(int)brush.Position.X].TileID == target)
                {
                    int left = (int)brush.Position.X;
                    int right = (int)MathHelper.Clamp(brush.Position.X + 1, 0, layer.LayerWidth - 1);
                    int y = (int)brush.Position.Y;

                    int i = tileBrush.Brush.GetLength(0);
                    int j = tileBrush.Brush.GetLength(1);


                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)) });


                    while (layer.Rows[y].Columns[left].TileID == target)
                    {
                        layer.Rows[y].Columns[left].TileID = brush.Brush[y % i, left % j];

                        left = (int)MathHelper.Clamp(left - 1, 0, layer.LayerWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)) });

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)) });
                    }

                    while (layer.Rows[y].Columns[right].TileID == target)
                    {
                        layer.Rows[y].Columns[right].TileID = brush.Brush[y % i, right % j];

                        right = (int)MathHelper.Clamp(right + 1, 0, layer.LayerWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.LayerHeight - 1)) });

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.LayerHeight - 1)) });
                    }
                }
            }

            tempLayer = layer;
        }  
          

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tempLayer == null)
                return;

            int left = (int)Math.Floor(map.Camera.Position.X / map.Tileset.TileWidth);
            int right = (int)(map.Tileset.TileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / map.Tileset.TileWidth / map.Camera.Zoom);
            right = Math.Min(right, tempLayer.LayerWidth);

            int top = (int)Math.Floor(map.Camera.Position.Y / map.Tileset.TileHeight);
            int bottom = (int)(map.Tileset.TileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / map.Tileset.TileHeight / map.Camera.Zoom);
            bottom = Math.Min(bottom, tempLayer.LayerHeight);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int tileID = tempLayer.Rows[y].Columns[x].TileID;

                    if (tileID > -1)
                    {
                        spriteBatch.Draw(map.Tileset.Texture,
                            new Rectangle((x * map.Tileset.TileWidth),
                                (y * map.Tileset.TileHeight),
                                map.Tileset.TileWidth,
                                map.Tileset.TileHeight),
                                map.Tileset.GetSourceRectangle(tileID),
                                Color.White * 0.5f);
                    }
                }
            }
        }

        #endregion
    }
}
