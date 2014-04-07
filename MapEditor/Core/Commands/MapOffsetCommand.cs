using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class MapOffsetCommand : ICommand 
    {
        private List<Layer> currentLayers;
        private List<Layer> previousLayers;

        private int offsetX;
        private int offsetY;

        public MapOffsetCommand(List<Layer> layers, int offsetX, int offsetY)
        {
            currentLayers = layers;

            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public void Execute()
        {
            previousLayers = currentLayers;

            List<Layer> newLayers = new List<Layer>();

            currentLayers.ForEach(layer =>
                {
                    Layer newLayer = new Layer(layer.MapWidth, layer.MapHeight);

                    for (int y = 0; y < layer.MapHeight; y++)
                    {
                        for (int x = 0; x < layer.MapWidth; x++)
                        {
                            if((offsetY + y) < newLayer.MapHeight && (offsetX + x) < newLayer.MapWidth)
                                newLayer.Rows[offsetY + y].Columns[offsetX + x].TileID = layer.Rows[y].Columns[x].TileID;
                        }
                    }

                    newLayers.Add(newLayer);

                });

            currentLayers = newLayers;
        }

        public void UnExecute()
        {
            currentLayers = previousLayers;
        }
    }
}
