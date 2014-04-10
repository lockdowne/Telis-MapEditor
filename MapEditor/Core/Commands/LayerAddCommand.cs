using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that adds a layer to map and layer view
    /// </summary>
    public class LayerAddCommand : ICommand
    {
        #region Fields

        private List<Layer> currentLayers;

        private Layer currentLayer;

        private CheckedListBox checkedListBox;

        private object currentCheckedListBoxItem;

        #endregion

        #region Initialize

        public LayerAddCommand(List<Layer> layers, int width, int height, CheckedListBox box)
        {
            currentLayers = layers;

            currentLayer = new Layer(width, height);

            checkedListBox = box;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create layer for map and object for layer view
        /// </summary>
        public void Execute()
        {
            currentLayers.Add(currentLayer);

            currentCheckedListBoxItem = "Layer" + checkedListBox.Items.Count;

            checkedListBox.Items.Add(currentCheckedListBoxItem, true);
        }

        /// <summary>
        /// Invert execute method
        /// </summary>
        public void UnExecute()
        {
            currentLayers.Remove(currentLayer);

            checkedListBox.Items.Remove(currentCheckedListBoxItem);
        }

        #endregion

    }
}
