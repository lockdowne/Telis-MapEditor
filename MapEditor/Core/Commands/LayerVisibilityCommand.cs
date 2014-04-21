using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that sets the selected layers visibility
    /// </summary>
    public class LayerVisibilityCommand : ICommand
    {
        #region Fields

        private Layer layer;

        #endregion

        #region Initialize

        public LayerVisibilityCommand(Layer layer)
        {
            this.layer = layer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set visibility of layer
        /// </summary>
        public void Execute()
        {
            layer.IsVisible = !layer.IsVisible;
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layer.IsVisible = !layer.IsVisible;
        }

        #endregion
    }
}
