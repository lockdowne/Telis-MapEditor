using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle tileset events
    /// </summary>
    /// <param name="e"></param>
    public delegate void TilesetEventHandler(TilesetEventArgs e);

    /// <summary>
    /// Provides data for NewMap event
    /// </summary>
    public class TilesetEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets tile brush value
        /// </summary>
        public int[,] TileBrushValue { get; private set; }

        #endregion

        #region Initialize

        public TilesetEventArgs(int[,] tileBrushValue)
        {
            TileBrushValue = tileBrushValue;
        }

        #endregion
    }
}
