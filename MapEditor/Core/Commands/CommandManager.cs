using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Manages all commands. Keeps track of commands in stack to execute, undo, and redo
    /// </summary>
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

        public void ExecuteEditRemoveCommand(Layer layer, List<TileBrush> tileBrushes)
        {
            ICommand command = new EditRemoveCommand(layer, tileBrushes);
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

        public void ExecuteAddTilesetCommand()
        {
            //ICommand command = new MapAddTilesetCommand(
        }

        public void ExecuteLayerAddCommand(List<Layer> layers, int width, int height, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerAddCommand(layers, width, height, checkedListBox);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerRemoveCommand(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerRemoveCommand(layers, index, checkedListBox);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerVisibility(Layer layer, bool isVisible)
        {
            ICommand command = new LayerVisibilityCommand(layer, isVisible);
            command.Execute();

            // This command does not have the need to be undoed
            //UndoCommands.Push(command);
            //RedoCommands.Clear();
        }

        public void ExecuteLayerClone(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerCloneCommand(layers, index, checkedListBox);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerRaise(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerRaiseCommand(layers, index, checkedListBox);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerLower(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerLowerCommand(layers, index, checkedListBox);
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

        public void ExecuteMapResize(List<Layer> layers, int mapWidth, int mapHeight, NumericUpDownEx mapWidthNumeric, NumericUpDownEx mapHeightNumeric)
        {
            ICommand command = new MapResizeCommand(layers, mapWidth, mapHeight, mapWidthNumeric, mapHeightNumeric);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteTileResize(List<Tileset> tilesets, int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric)
        {
            ICommand command = new TileResizeCommand(tilesets, tileWidth, tileHeight, tileWidthNumeric, tileHeightNumeric);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }


        public void ExecuteInitializeMap(List<Layer> layers, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
           // ICommand addLayer = new LayerAddCommand(layers, mapWidth, mapHeight);
            //ICommand addTileset
        }

        #endregion
    }
}
