using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerCloneCommand : ICommand
    {
        private List<Layer> currentLayers;

        private Layer currentLayer;

        private int index;

        public LayerCloneCommand(List<Layer> layers, int layerIndex)
        {
            currentLayers = layers;

            index = layerIndex;
        }

        public void Execute()
        {
            currentLayer = currentLayers[index];

            currentLayers.Add(currentLayer);
        }

        public void UnExecute()
        {
            currentLayers.Remove(currentLayer);
        }
    }
}
