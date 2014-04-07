using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class MapResizeCommand : ICommand
    {
        private List<Layer> currentLayers;
        private List<Layer> previousLayers;

        private int width;
        private int height;

        public MapResizeCommand(List<Layer> layers, int width, int height)
        {
            currentLayers = layers;

            this.width = width;
            this.height = height;
        }

        public void Execute()
        {
            previousLayers = currentLayers;

            currentLayers.ForEach(layer => layer.Resize(width, height));
        }

        public void UnExecute()
        {
            currentLayers = previousLayers;
        }
    }
}
