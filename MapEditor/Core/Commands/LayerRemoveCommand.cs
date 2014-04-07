using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class LayerRemoveCommand : ICommand
    {
        private List<Layer> currentLayers;

        private Layer currentLayer;

        private int index;

        private CheckedListBox checkedListBox;

        private object currentCheckedListBoxItem;

        public LayerRemoveCommand(List<Layer> layers, int removeIndex, CheckedListBox box)
        {
            currentLayers = layers;

            currentLayer = layers[removeIndex];

            checkedListBox = box;

            currentCheckedListBoxItem = box.Items[removeIndex];
        }

        public void Execute()
        {
            currentLayers.RemoveAt(index);

            checkedListBox.Items.RemoveAt(index);
        }

        public void UnExecute()
        {
            currentLayers.Insert(index, currentLayer);

            checkedListBox.Items.Insert(index, currentCheckedListBoxItem);
        }

        
    }
}
