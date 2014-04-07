using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Models
{
    public class Row
    {
        public List<Cell> Columns = new List<Cell>();
    }
   
    public class Layer
    {
        // Matrix
        public List<Row> Rows = new List<Row>();
        
        // Layer width in tiles
        public int MapWidth { get; private set; }
        // Layer height in tiles
        public int MapHeight { get; private set; }

        // Visibility of layer
        public bool IsVisible { get; set; }

        public Layer(int width, int height)
        {
            MapWidth = width;
            MapHeight = height;

            IsVisible = true;

            Initialize();
        }

        /// <summary>
        /// Initialize layer to empty cells
        /// </summary>
        private void Initialize()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Row row = new Row();

                for (int x = 0; x < MapWidth; x++)
                {
                    row.Columns.Add(new Cell()
                    {
                        Property = string.Empty,
                        TileID = -1,
                    });
                }

                Rows.Add(row);
            }
        }

        public void Resize(int width, int height)
        {
            List<Row> resizedLayer = new List<Row>();

            for (int y = 0; y < height; y++)
            {
                Row row = new Row();

                for (int x = 0; x < width; x++)
                {
                    row.Columns.Add(new Cell()
                    {
                        Property = Rows[y].Columns[x].Property,
                        TileID = Rows[y].Columns[x].TileID,
                    });
                }

                resizedLayer.Add(row);
            }

            Rows = resizedLayer;

            MapWidth = width;
            MapHeight = height;
        }

    }
}
