using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerCloneCommand : ICommand
    {
        #region Fields

        private  Dictionary<string, Layer> layers;

        private Layer layer;

        private string key;

        #endregion 

        #region Initialize

        public LayerCloneCommand(Dictionary<string, Layer> layers, string key)
        {
            this.layers = layers;

            this.layer = layers[key];

            this.key = key;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add clone of layer to map and view
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
