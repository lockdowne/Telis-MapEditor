using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MapEditor.Models
{
    /// <summary>
    /// Individual cell to be used inside matrix
    /// </summary>
    public class Tile
    {
        #region Properties

        /// <summary>
        /// Gets or sets tile id
        /// </summary>
        public int TileID { get; set; }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public string Property { get; set; }

        #endregion
    }
}
