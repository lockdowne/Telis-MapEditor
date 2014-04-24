using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Core.Controls;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class MapResizeCommand : ICommand
    {
        #region Fields

        private List<Layer> currentLayers;
        private List<Layer> previousLayers;

        private int mapWidth;
        private int mapHeight;

        #endregion

        #region Initialize

        public MapResizeCommand(List<Layer> layers, int mapWidth, int mapHeight)
        {
            currentLayers = layers;

            previousLayers = new List<Layer>(currentLayers.AsEnumerable());

            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
        }

        #endregion

        #region Methods

        public void Execute()
        {
            List<Layer> newLayers = new List<Layer>();

            currentLayers.ForEach(layer =>
                {
                    Layer tempLayer = new Layer();
                    tempLayer.Initialize(layer.LayerName, mapWidth, mapHeight);

                    for (int y = 0; y < mapHeight; y++)
                    {
                        for (int x = 0; x < mapWidth; x++)
                        {
                            tempLayer.Rows[y].Columns[x].TileID = layer.Rows[y].Columns[x].TileID;
                        }
                    }

                    newLayers.Add(tempLayer);
                });

            currentLayers.Clear();

            currentLayers.AddRange(newLayers.AsEnumerable());
        }

        public void UnExecute()
        {
            currentLayers.Clear();

            currentLayers.AddRange(previousLayers.AsEnumerable());
        }

        #endregion
    }
}
