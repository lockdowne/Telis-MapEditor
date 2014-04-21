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

        public List<Tile> Columns = new List<Tile>();

        #endregion
    }
   
    /// <summary>
    /// Represents a matrix of tiles
    /// </summary>
    public class Layer
    {
        #region Properties

        /// <summary>
        /// Gets or sets layer matrix
        /// </summary>
        public List<Row> Rows = new List<Row>();

        /// <summary>
        /// Gets or sets layer name
        /// </summary>
        public string LayerName { get; set; }
        
        /// <summary>
        /// Gets layer width in tiles
        /// </summary>
        public int LayerWidth { get; private set; }

        /// <summary>
        /// Gets layer height in tiles
        /// </summary>
        public int LayerHeight { get; private set; }

        /// <summary>
        /// Gets or sets layer visibility
        /// </summary>
        public bool IsVisible { get; set; }

        #endregion

        #region Initialize

        public Layer(string layerName, int layerWidth, int layerHeight)
        {
            LayerName = layerName;

            LayerWidth = layerWidth;
            LayerHeight = layerHeight;

            IsVisible = true;

            Initialize();
        }

        /// <summary>
        /// Initialize layer to empty cells
        /// </summary>
        private void Initialize()
        {
            for (int y = 0; y < LayerHeight; y++)
            {
                Row row = new Row();

                for (int x = 0; x < LayerWidth; x++)
                {
                    row.Columns.Add(new Tile()
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
