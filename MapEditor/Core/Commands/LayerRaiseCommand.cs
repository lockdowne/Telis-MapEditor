using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerRaiseCommand : ICommand
    {
        #region Fields

        private List<Layer> layers;
        
        private int layerIndex;

        private CheckedListBox checkedListBox;

        #endregion 

        #region Initialize

        public LayerRaiseCommand(List<Layer> layers, int layerIndex, CheckedListBox checkedListBox)
        {
            this.layers = layers;

            this.layerIndex = layerIndex;

            this.checkedListBox = checkedListBox;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            Layer layerSwap = layers[layerIndex - 1];

            layers[layerIndex - 1] = layers[layerIndex];
            layers[layerIndex] = layerSwap;

            object boxSwap = checkedListBox.Items[layerIndex - 1];

            CheckState newCheckState = checkedListBox.GetItemCheckState(layerIndex);
            CheckState previousCheckState = checkedListBox.GetItemCheckState(layerIndex - 1);

            checkedListBox.Items[layerIndex - 1] = checkedListBox.Items[layerIndex];
            checkedListBox.Items[layerIndex] = boxSwap;
            checkedListBox.SelectedIndex = layerIndex - 1;
            checkedListBox.SetItemCheckState(layerIndex - 1, newCheckState);
            checkedListBox.SetItemCheckState(layerIndex, previousCheckState);
        }

        public void UnExecute()
        {
            Layer swap = layers[layerIndex];

            layers[layerIndex] = layers[layerIndex - 1];
            layers[layerIndex - 1] = swap;
        }

        #endregion
    }
}
