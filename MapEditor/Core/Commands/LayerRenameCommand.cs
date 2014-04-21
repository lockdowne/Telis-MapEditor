using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerRenameCommand : ICommand
    {
        #region Fields

        private Layer layer;

        private string currentName;
        private string previousName;

        #endregion

        #region Initialize

        public LayerRenameCommand(Layer layer, string name)
        {
            this.layer = layer;

            this.currentName = name;

            this.previousName = layer.LayerName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renames layer
        /// </summary>
        public void Execute()
        {
            layer.LayerName = currentName;
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layer.LayerName = previousName;
        }

        #endregion

    }
}
