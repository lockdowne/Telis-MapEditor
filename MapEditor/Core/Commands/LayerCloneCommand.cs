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

        private  List<Layer> layers;

        private Layer layer;

        #endregion 

        #region Initialize

        public LayerCloneCommand(List<Layer> layers, int layerIndex)
        {
            this.layers = layers;

            this.layer = layers[layerIndex];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add clone of layer to map and view
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
