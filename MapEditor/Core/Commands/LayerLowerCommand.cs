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
        private List<Layer> currentLayers;

        private int currentIndex;

        private CheckedListBox checkedListBox;

        public LayerLowerCommand(List<Layer> layers, int layerIndex, CheckedListBox box)
        {
            currentLayers = layers;

            currentIndex = layerIndex;

            checkedListBox = box;
        }

        public void Execute()
        {
            Layer swap = currentLayers[currentIndex + 1];

            currentLayers[currentIndex + 1] = currentLayers[currentIndex];
            currentLayers[currentIndex] = swap;       
     
            object boxSwap = checkedListBox.Items[currentIndex + 1];

            CheckState newCheckState = checkedListBox.GetItemCheckState(currentIndex);
            CheckState previousCheckState = checkedListBox.GetItemCheckState(currentIndex + 1);

            checkedListBox.Items[currentIndex + 1] = checkedListBox.Items[currentIndex];
            checkedListBox.Items[currentIndex] = boxSwap;
            checkedListBox.SelectedIndex = currentIndex + 1;
            checkedListBox.SetItemCheckState(currentIndex + 1, newCheckState);
            checkedListBox.SetItemCheckState(currentIndex, previousCheckState);
        }
        

        public void UnExecute()
        {
            Layer swap = currentLayers[currentIndex];

            currentLayers[currentIndex] = currentLayers[currentIndex + 1];
            currentLayers[currentIndex + 1] = swap;
        }
    }
}
