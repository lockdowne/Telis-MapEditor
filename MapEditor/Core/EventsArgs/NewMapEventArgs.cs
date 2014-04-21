using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{

    /// <summary>
    /// Represents the method that will handle NewMap events
    /// </summary>
    /// <param name="e"></param>
    public delegate void NewMapEventHandler(NewMapEventArgs e);

    /// <summary>
    /// Provides data for NewMap event
    /// </summary>
    public class NewMapEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets map name
        /// </summary>
        public string MapName { get; private set; }

        /// <summary>
        /// Gets tileset path to image
        /// </summary>
        public string TilesetPath { get; private set; }

        /// <summary>
        /// Gets map width in tiles
        /// </summary>
        public int MapWidth { get; private set; }

        /// <summary>
        /// Gets map height in tiles
        /// </summary>
        public int MapHeight { get; private set; }

        /// <summary>
        /// Gets tile width in pixels
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// Gets tile height in pixel
        /// </summary>
        public int TileHeight { get; private set; }

        #endregion

        #region Initialize

        public NewMapEventArgs(string mapName, string tilesetPath, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            MapName = mapName;
            TilesetPath = tilesetPath;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            TileWidth = tileWidth;
            TileHeight = tileHeight;            
        }

        #endregion
    }
}
