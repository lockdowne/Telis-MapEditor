using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle Offset events
    /// </summary>
    /// <param name="e"></param>
    public delegate void OffsetEventHandler(OffsetEventArgs e);

    /// <summary>
    /// Provides data for Offset event
    /// </summary>
    public class OffsetEventArgs
    {
        #region Properties

        /// <summary>
        /// Gets x axis offset
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets y axis offset
        /// </summary>
        public int Y { get; private set; }

        #endregion

        #region Initialize

        public OffsetEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
