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
    public class FillPaintTool : IPaintTool
    {
        #region Fields

        private readonly MainRenderPresenter presenter;

        private Layer tempLayer;

        private bool isMiddleMousePressed;

        private int previousTarget;

        #endregion

        public FillPaintTool(MainRenderPresenter presenter)
        {
            this.presenter = presenter;

        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int x = (int)MathHelper.Clamp(presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location)).X, 0, presenter.MapWidth - 1);
                int y = (int)MathHelper.Clamp(presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location)).Y, 0, presenter.MapHeight - 1);

                TileBrush tileBrush = new TileBrush() { Brush = presenter.TileBrushValues, Position = new Vector2(x, y), };
                presenter.Fill(tileBrush, presenter.Layers[presenter.LayerIndex].Rows[y].Columns[x].TileID);                
            }

            if (e.Button == MouseButtons.Middle)
                isMiddleMousePressed = true;
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isMiddleMousePressed)
            {
                int x = (int)MathHelper.Clamp(presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location)).X, 0, presenter.MapWidth - 1);
                int y = (int)MathHelper.Clamp(presenter.PixelsToCoordinate(presenter.InvertCameraMatrix(e.Location)).Y, 0, presenter.MapHeight - 1);

                TileBrush tileBrush = new TileBrush() { Brush = presenter.TileBrushValues, Position = new Vector2(x, y), };

                if(presenter.Layers[presenter.LayerIndex].Rows[y].Columns[x].TileID != previousTarget)
                    FloodFill(tileBrush, presenter.Layers[presenter.LayerIndex].Rows[y].Columns[x].TileID);
            }
        }


        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                isMiddleMousePressed = false;
        }

        /*private void FloodFill(TileBrush tileBrush, int target)
        {
            Layer layer = presenter.Layers[presenter.LayerIndex];

            Queue<TileBrush> queue = new Queue<TileBrush>();
            queue.Enqueue(tileBrush);

            if (layer.Rows[(int)tileBrush.Position.Y].Columns[(int)tileBrush.Position.X].TileID == tileBrush.Brush[0,0])
                return;

            while (queue.Count > 0)
            {
                TileBrush brush = queue.Dequeue();

                if (layer.Rows[(int)brush.Position.Y].Columns[(int)brush.Position.X].TileID == target)
                {
                    int left = (int)brush.Position.X;
                    int right = (int)brush.Position.X + 1;
                    int y = (int)brush.Position.Y;

                    
                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });


                    while (layer.Rows[y].Columns[left].TileID == target)
                    {
                        layer.Rows[y].Columns[left].TileID = brush.Brush[0,0];

                        left = (int)MathHelper.Clamp(left - 1, 0, layer.MapWidth - 1);

                        if(layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1))});

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });
                    }

                    while (layer.Rows[y].Columns[right].TileID == target)
                    {
                        layer.Rows[y].Columns[right].TileID = brush.Brush[0, 0];

                        right = (int)MathHelper.Clamp(right + 1, 0, layer.MapWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });
                    }
                }
            }          
        }*/

        private void FloodFill(TileBrush tileBrush, int target)
        {
            //Layer layer = presenter.Layers[presenter.LayerIndex];

            previousTarget = target;

            if (tileBrush == null)
                return;

            Layer layer = new Layer(presenter.Layers[presenter.LayerIndex].LayerName, presenter.Layers[presenter.LayerIndex].MapWidth, presenter.Layers[presenter.LayerIndex].MapHeight);

            for (int layerY = 0; layerY < layer.MapHeight; layerY++)
            {
                for (int layerX = 0; layerX < layer.MapWidth; layerX++)
                {
                    layer.Rows[layerY].Columns[layerX].TileID = presenter.Layers[presenter.LayerIndex].Rows[layerY].Columns[layerX].TileID;
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
                    int right = (int)MathHelper.Clamp(brush.Position.X + 1, 0, layer.MapWidth - 1);
                    int y = (int)brush.Position.Y;

                    int i = tileBrush.Brush.GetLength(0);
                    int j = tileBrush.Brush.GetLength(1);


                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                    if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                        queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });


                    while (layer.Rows[y].Columns[left].TileID == target)
                    {
                        layer.Rows[y].Columns[left].TileID = brush.Brush[y % i, left % j];

                        left = (int)MathHelper.Clamp(left - 1, 0, layer.MapWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });
                    }

                    while (layer.Rows[y].Columns[right].TileID == target)
                    {
                        layer.Rows[y].Columns[right].TileID = brush.Brush[y % i, right % j];

                        right = (int)MathHelper.Clamp(right + 1, 0, layer.MapWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

                        if (layer.Rows[(int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)].Columns[right].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(right, (int)MathHelper.Clamp(y + 1, 0, layer.MapHeight - 1)) });
                    }
                }
            }

            tempLayer = layer;
        }  
          

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tempLayer == null)
                return;

            int left = (int)Math.Floor(presenter.Camera.Position.X / presenter.Tilesets.First().TileWidth);
            int right = presenter.Tilesets.First().TileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / presenter.Tilesets.First().TileWidth;
            right = Math.Min(right, tempLayer.MapWidth);

            int top = (int)Math.Floor(presenter.Camera.Position.Y / presenter.Tilesets.First().TileHeight);
            int bottom = presenter.Tilesets.First().TileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / presenter.Tilesets.First().TileHeight;
            bottom = Math.Min(bottom, tempLayer.MapHeight);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int tileID = tempLayer.Rows[y].Columns[x].TileID;

                    if (tileID > -1)
                    {
                        spriteBatch.Draw(presenter.Tilesets.First().Texture,
                            new Rectangle((x * presenter.Tilesets.First().TileWidth),
                                (y * presenter.Tilesets.First().TileHeight),
                                presenter.Tilesets.First().TileWidth,
                                presenter.Tilesets.First().TileHeight),
                                presenter.Tilesets.First().GetSourceRectangle(tileID),
                                Color.White * 0.5f);
                    }
                }
            }
        }             
    }
}
