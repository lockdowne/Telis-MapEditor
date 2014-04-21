using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.EventsArgs
{
    /// <summary>
    /// Represents the method that will handle minimap events
    /// </summary>
    /// <param name="e"></param>
    public delegate void CameraEventHandler(CameraEventArgs e);

    /// <summary>
    /// Provides data for minimap event
    /// </summary>
    public class CameraEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets camera
        /// </summary>
        public Camera Camera { get; private set; }

        #endregion

        #region Initialize

        public CameraEventArgs(Camera camera)
        {
            Camera = camera;
        }

        #endregion
    }
}
