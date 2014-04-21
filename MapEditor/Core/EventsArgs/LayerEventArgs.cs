using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle LayerView events
    /// </summary>
    /// <param name="e"></param>
    public delegate void LayerEventHandler(LayerEventArgs e);

    /// <summary>
    /// Provides data for Layer events
    /// </summary>
    public class LayerEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets layer index
        /// </summary>
        public int LayerIndex { get; private set; }

        #endregion

        #region Initialize

        public LayerEventArgs(int layerIndex)
        {
            LayerIndex = layerIndex;
        }

        #endregion
    }
}
