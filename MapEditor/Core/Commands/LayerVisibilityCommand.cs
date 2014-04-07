using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerVisibilityCommand : ICommand
    {
        private Layer currentLayer;

        public LayerVisibilityCommand(Layer layer)
        {
            currentLayer = layer;
        }

        public void Execute()
        {
            currentLayer.IsVisible = !currentLayer.IsVisible;
        }

        public void UnExecute()
        {
            currentLayer.IsVisible = !currentLayer.IsVisible;
        }

    }
}
