using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerLowerCommand : ICommand
    {
        #region Fields

        private List<Layer> layers;

        private int layerIndex;

        #endregion

        #region Initialize

        public LayerLowerCommand(List<Layer> layers, int layerIndex)
        {
            this.layers = layers;

            this.layerIndex = layerIndex;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            Layer swap = layers[layerIndex + 1];

            layers[layerIndex + 1] = layers[layerIndex];
            layers[layerIndex] = swap;
        }        

        public void UnExecute()
        {
            Layer swap = layers[layerIndex];

            layers[layerIndex] = layers[layerIndex + 1];
            layers[layerIndex + 1] = swap;
        }

        #endregion
    }
}
