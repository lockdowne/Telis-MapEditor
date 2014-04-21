using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Command that offsets map by number of tiles
    /// </summary>
    public class MapOffsetCommand : ICommand
    {
        #region Fields

        private List<Layer> currentLayers;
        private List<Layer> previousLayers;

        private int offsetX;
        private int offsetY;

        #endregion

        #region Initialize

        public MapOffsetCommand(List<Layer> layers, int offsetX, int offsetY)
        {
            this.currentLayers = layers;
            this.previousLayers = new List<Layer>(layers.AsEnumerable());

            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Offset all layers
        /// </summary>
        public void Execute()
        {
            List<Layer> newLayers = new List<Layer>();

            currentLayers.ForEach(layer =>
                {
                    Layer newLayer = new Layer(layer.LayerName, layer.LayerWidth, layer.LayerHeight);

                    for (int y = 0; y < layer.LayerHeight; y++)
                    {
                        for (int x = 0; x < layer.LayerWidth; x++)
                        {
                            if((offsetY + y) < newLayer.LayerHeight && (offsetX + x) < newLayer.LayerWidth)
                                newLayer.Rows[offsetY + y].Columns[offsetX + x].TileID = layer.Rows[y].Columns[x].TileID;
                        }
                    }

                    newLayers.Add(newLayer);

                });

            currentLayers.Clear();
            currentLayers.AddRange(newLayers.AsEnumerable());
        }

        /// <summary>
        /// Inverse of execute method
        /// </summary>
        public void UnExecute()
        {
            currentLayers.Clear();
            currentLayers.AddRange(previousLayers.AsEnumerable());
        }

        #endregion
    }
}
