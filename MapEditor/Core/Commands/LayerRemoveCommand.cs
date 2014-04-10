using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Remove layer from map and view
    /// </summary>
    public class LayerRemoveCommand : ICommand
    {
        #region Fields

        private List<Layer> layers;

        private Layer layer;

        private int layerIndex;

        private CheckedListBox checkedListBox;

        private object checkedListBoxItem;

        #endregion

        #region Initialize

        public LayerRemoveCommand(List<Layer> layers, int layerIndex, CheckedListBox checkedListBox)
        {
            this.layers = layers;

            this.layer = layers[layerIndex];

            this.checkedListBox = checkedListBox;

            this.checkedListBoxItem = checkedListBox.Items[layerIndex];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove selected layer from and view
        /// </summary>
        public void Execute()
        {
            layers.RemoveAt(layerIndex);

            checkedListBox.Items.RemoveAt(layerIndex);
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layers.Insert(layerIndex, layer);

            checkedListBox.Items.Insert(layerIndex, checkedListBoxItem);
        }

        #endregion
    }
}
