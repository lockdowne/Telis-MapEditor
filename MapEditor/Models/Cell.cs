using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Models
{
    /// <summary>
    /// Individual cell to be used inside matrix
    /// </summary>
    public class Cell
    {
        // Number to draw on map referenced from given tileset
        public int TileID { get; set; }

        // Cell property
        public string Property { get; set; }

    }
}
