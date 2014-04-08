using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerVisibilityCommand : ICommand
    {
        private Layer layer;

        private bool isVisisble;

        public LayerVisibilityCommand(Layer layer, bool isVisible)
        {
            this.layer = layer;

            this.isVisisble = isVisible;
        }

        public void Execute()
        {
            layer.IsVisible = isVisisble;
        }

        public void UnExecute()
        {
            layer.IsVisible = !isVisisble;
        }

    }
}
