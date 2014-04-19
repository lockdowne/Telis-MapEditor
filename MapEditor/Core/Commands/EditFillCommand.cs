using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class EditFillCommand : ICommand
    {
        private TileBrush tileBrush;

        private Layer currentLayer;
        private Layer previousLayer;

        private int target;

        public EditFillCommand(TileBrush tileBrush, Layer layer, int target)
        {
            this.tileBrush = tileBrush;

            this.currentLayer = layer;
            
            this.target = target;

            this.previousLayer = new Layer(layer.LayerName, layer.MapWidth, layer.MapHeight);

            for (int y = 0; y < layer.MapHeight; y++)
            {
                for (int x = 0; x < layer.MapWidth; x++)
                {
                    previousLayer.Rows[y].Columns[x].TileID = layer.Rows[y].Columns[x].TileID;
                }
            }
        }

        public void Execute()
        {            
            Layer layer = currentLayer;

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
                    int right = (int)brush.Position.X + 1;
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
        }

        public void UnExecute()
        {
            for (int y = 0; y < previousLayer.MapHeight; y++)
            {
                for (int x = 0; x < previousLayer.MapWidth; x++)
                {
                    currentLayer.Rows[y].Columns[x].TileID = previousLayer.Rows[y].Columns[x].TileID;
                }
            }
        }
    }
}
