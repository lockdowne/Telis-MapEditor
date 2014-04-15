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

        private Dictionary<string, Layer> layers;

        private Layer layer;

        private string key;

        #endregion

        #region Initialize

        public LayerAddCommand(Dictionary<string, Layer> layers, string key,  int width, int height)
        {
            this.layers = layers;

            this.layer = new Layer(width, height);

            this.key = key;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create layer for map
        /// </summary>
        public void Execute()
        {
            layers.Add(key, layer);
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layers.Remove(key);
        }

        #endregion

    }
}
