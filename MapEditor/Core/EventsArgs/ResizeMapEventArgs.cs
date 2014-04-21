using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle ResizeMap events
    /// </summary>
    /// <param name="e"></param>
    public delegate void ResizeMapEventHandler(ResizeMapEventArgs e);

    /// <summary>
    /// Provides data for ResizeMap event
    /// </summary>
    public class ResizeMapEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets map width
        /// </summary>
        public int MapWidth { get; private set; }

        /// <summary>
        /// Gets map height
        /// </summary>
        public int MapHeight { get; private set; }

        #endregion

        #region Initialize

        public ResizeMapEventArgs(int mapWidth, int mapHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
        }

        #endregion
    }
}
