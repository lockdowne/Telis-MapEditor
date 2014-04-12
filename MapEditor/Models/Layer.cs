using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor.Models
{
    public class Row
    {
        #region Properties

        public List<Cell> Columns = new List<Cell>();

        #endregion
    }
   
    public class Layer
    {
        #region Properties

        // Matrix
        public List<Row> Rows = new List<Row>();
        
        // Layer width in tiles
        public int MapWidth { get; private set; }
        // Layer height in tiles
        public int MapHeight { get; private set; }

        // Visibility of layer
        public bool IsVisible { get; set; }

        #endregion

        #region Initialize

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
                        TileID = -1,
                    });
                }

                Rows.Add(row);
            }
        }

        #endregion
    }
}
