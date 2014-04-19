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

        private bool isMouseLeftPressed;

        private Vector2 previousMousePosition;
        private Vector2 currentMousePosition;

        private TileBrushCollection tileBrushes;

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
                FloodFill(tileBrush, presenter.Layers[presenter.LayerIndex].Rows[y].Columns[x].TileID);
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {

        }


        public void OnMouseUp(object sender, MouseEventArgs e)
        {

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
            Layer layer = presenter.Layers[presenter.LayerIndex];

            Queue<TileBrush> queue = new Queue<TileBrush>();
            queue.Enqueue(tileBrush);

            if (layer.Rows[(int)tileBrush.Position.Y].Columns[(int)tileBrush.Position.X].TileID == tileBrush.Brush[0, 0])
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
                        layer.Rows[y].Columns[left].TileID = brush.Brush[0, 0];

                        left = (int)MathHelper.Clamp(left - 1, 0, layer.MapWidth - 1);

                        if (layer.Rows[(int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)].Columns[left].TileID == target)
                            queue.Enqueue(new TileBrush() { Brush = brush.Brush, Position = new Vector2(left, (int)MathHelper.Clamp(y - 1, 0, layer.MapHeight - 1)) });

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
        }  
          

        public void Draw(SpriteBatch spriteBatch) { }
             
    }
}
