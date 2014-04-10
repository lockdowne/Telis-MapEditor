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

        private bool isVisisble;

        #endregion

        #region Initialize

        public LayerVisibilityCommand(Layer layer, bool isVisible)
        {
            this.layer = layer;

            this.isVisisble = isVisible;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set visibility of layer
        /// </summary>
        public void Execute()
        {
            layer.IsVisible = isVisisble;
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layer.IsVisible = !isVisisble;
        }

        #endregion
    }
}
