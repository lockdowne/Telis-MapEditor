using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Core.Commands
{
    public class CommandManager
    {
        #region Fields

        private Stack<ICommand> UndoCommands = new Stack<ICommand>();
        private Stack<ICommand> RedoCommands = new Stack<ICommand>();

        #endregion

        #region Methods

        public void Undo()
        {
            if (UndoCommands.Count <= 0)
                return;

            ICommand command = UndoCommands.Pop();
            command.UnExecute();

            RedoCommands.Push(command);
        }

        public void Redo()
        {
            if (RedoCommands.Count <= 0)
                return;

            ICommand command = RedoCommands.Pop();
            command.Execute();

            UndoCommands.Push(command);
        }

        public void ClearAll()
        {
            UndoCommands.Clear();
            RedoCommands.Clear();
        }

        public void ExecuteEditDrawCommand(Layer layer, List<TileBrush> tileBrushes)
        {
            ICommand command = new EditDrawCommand(layer, tileBrushes);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteEditRemoveCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight)
        {
            ICommand command = new EditRemoveCommand(layer, selectionBox, tileWidth, tileHeight);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteEditCopyCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, List<int[,]> clipboard)
        {
            ICommand command = new EditCopyCommand(layer, selectionBox, tileWidth, tileHeight, clipboard);
            command.Execute();

            // No undo/redo
        }

        public void ExecuteEditCutCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, List<int[,]> clipboard)
        {
            ICommand command = new EditCutCommand(layer, selectionBox, tileWidth, tileHeight, clipboard);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteEditFillCommand(TileBrush tileBrush, Layer layer, int target)
        {
            ICommand command = new EditFillCommand(tileBrush, layer, target);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteAddTilesetCommand()
        {
            //ICommand command = new MapAddTilesetCommand(
        }

        public void ExecuteLayerAddCommand(List<Layer> layers, string layerName, int width, int height)
        {
            ICommand command = new LayerAddCommand(layers, layerName, width, height);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerRemoveCommand(List<Layer> layers, int layerIndex)
        {
            ICommand command = new LayerRemoveCommand(layers, layerIndex);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerVisibility(Layer layer)
        {
            ICommand command = new LayerVisibilityCommand(layer);
            command.Execute();

            // This command does not have the need to be undoed
            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerClone(List<Layer> layers, int layerIndex)
        {
            ICommand command = new LayerCloneCommand(layers, layerIndex);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerRaise(List<Layer> layers, int layerIndex)
        {
            ICommand command = new LayerRaiseCommand(layers, layerIndex);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerLower(List<Layer> layers, int layerIndex)
        {
            ICommand command = new LayerLowerCommand(layers, layerIndex);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }


        public void ExecuteMapOffset(List<Layer> layers, int offsetX, int offsetY)
        {
            ICommand command = new MapOffsetCommand(layers, offsetX, offsetY);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteMapResize(List<Layer> layers, int mapWidth, int mapHeight)
        {
            ICommand command = new MapResizeCommand(layers, mapWidth, mapHeight);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteTileResize(Tileset tileset, int tileWidth, int tileHeight)
        {
            ICommand command = new TileResizeCommand(tileset, tileWidth, tileHeight);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteMapAddTileset(List<Tileset> tilesets, string texturePath, int tileWidth, int tileHeight, GraphicsDevice graphicsDevice, Action<string, int, int> createTileset, Action<string> removeTileset)
        {
            ICommand command = new MapAddTilesetCommand(tilesets, texturePath, tileWidth, tileHeight, graphicsDevice, createTileset, removeTileset);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }


        public void ExecuteInitializeMap(List<Layer> layers, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            // ICommand addLayer = new LayerAddCommand(layers, mapWidth, mapHeight);
            //ICommand addTileset
        }

        public void ExecuteRenameLayer(Layer layer, string name)
        {
            ICommand command = new LayerRenameCommand(layer, name);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        #endregion
    }
}
