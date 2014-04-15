using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that adds a layer to map
    /// </summary>
    public class LayerAddCommand : ICommand
    {
        #region Fields

        private List<Layer> layers;

        private Layer layer;

        #endregion

        #region Initialize

        public LayerAddCommand(List<Layer> layers, string layerName, int width, int height)
        {
            this.layers = layers;

            this.layer = new Layer(layerName, width, height);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create layer for map
        /// </summary>
        public void Execute()
        {
            layers.Add(layer);
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layers.Remove(layer);
        }

        #endregion

    }
}
