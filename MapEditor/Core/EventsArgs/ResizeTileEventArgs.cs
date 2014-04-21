using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle ResizeTile events
    /// </summary>
    /// <param name="e"></param>
    public delegate void ResizeTileEventHandler(ResizeTileEventArgs e);

    /// <summary>
    /// Provides data for ResizeTile event
    /// </summary>
    public class ResizeTileEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the tile width in pixels
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// Gets the tile height in pixels
        /// </summary>
        public int TileHeight { get; private set; }

        #endregion

        #region Initialize

        public ResizeTileEventArgs(int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        #endregion
    }
}
