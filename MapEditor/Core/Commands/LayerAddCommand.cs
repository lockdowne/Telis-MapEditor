using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerAddCommand : ICommand
    {
        private List<Layer> currentLayers;

        private Layer currentLayer;

        private CheckedListBox checkedListBox;

        private object currentCheckedListBoxItem;

        public LayerAddCommand(List<Layer> layers, int width, int height, CheckedListBox box)
        {
            currentLayers = layers;

            currentLayer = new Layer(width, height);

            checkedListBox = box;
        }

        public void Execute()
        {
            currentLayers.Add(currentLayer);

            currentCheckedListBoxItem = "Layer" + checkedListBox.Items.Count;

            checkedListBox.Items.Add(currentCheckedListBoxItem, true);
        }

        public void UnExecute()
        {
            currentLayers.Remove(currentLayer);

            checkedListBox.Items.Remove(currentCheckedListBoxItem);
        }


    }
}
