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

        private List<Layer> layers;

        private Layer layer;

        private int layerIndex;

        private object checkedListBoxItem;

        private CheckedListBox checkedListBox;

        #endregion 

        #region Initialize

        public LayerCloneCommand(List<Layer> layers, int layerIndex, CheckedListBox checkedListBox)
        {
            this.layers = layers;

            this.layer = layers[layerIndex];

            this.layerIndex = layerIndex;

            int number = checkedListBox.Items.Count + 1;

            this.checkedListBoxItem = "Layer" + number;

            this.checkedListBox = checkedListBox;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add clone of layer to map and view
        /// </summary>
        public void Execute()
        {
            layers.Add(layer);

            checkedListBox.Items.Add(checkedListBoxItem);
        }


        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            layers.Remove(layer);

            checkedListBox.Items.Remove(checkedListBoxItem);
        }

        #endregion
    }
}
