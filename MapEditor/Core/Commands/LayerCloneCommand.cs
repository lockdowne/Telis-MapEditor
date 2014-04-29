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

        private int layerIndex;

        #endregion 

        #region Initialize

        public LayerCloneCommand(List<Layer> layers, int layerIndex)
        {
            this.layers = layers;

            this.layerIndex = layerIndex;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add clone of layer to map and view
        /// </summary>
        public void Execute()
        {            
            Layer newLayer = new Layer();
            newLayer.Initialize(layers[layerIndex].LayerName, layers[layerIndex].LayerWidth, layers[layerIndex].LayerHeight);

            for (int y = 0; y < layers[layerIndex].LayerHeight; y++)
            {
                for (int x = 0; x < layers[layerIndex].LayerWidth; x++)
                {
                    newLayer.Rows[y].Columns[x].TileID = layers[layerIndex].Rows[y].Columns[x].TileID;
                }
            }

            this.layer = newLayer;

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
