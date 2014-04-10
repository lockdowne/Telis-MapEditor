﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    public class CommandManager
    {
        private Stack<ICommand> UndoCommands = new Stack<ICommand>();
        private Stack<ICommand> RedoCommands = new Stack<ICommand>();

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

        public void ExecuteEditCopyCommand(Layer layer, Rectangle selectionBox, int tileWidth, int tileHeight, int[,] tileBrush)
        {
            ICommand command = new EditCopyCommand(layer, selectionBox, tileWidth, tileHeight, tileBrush);
            command.Execute();

            // No undo/redo
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

        public void ExecuteLayerClone(List<Layer> layers, int index)
        {
            ICommand command = new LayerCloneCommand(layers, index);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteLayerRaise(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerRaiseCommand(layers, index, checkedListBox);
            command.Execute();

            /*UndoCommands.Push(command);
            RedoCommands.Clear();*/
        }

        public void ExecuteLayerLower(List<Layer> layers, int index, CheckedListBox checkedListBox)
        {
            ICommand command = new LayerLowerCommand(layers, index, checkedListBox);
            command.Execute();

            /*UndoCommands.Push(command);
             RedoCommands.Clear();*/
        }


        public void ExecuteMapOffset(List<Layer> layers, int offsetX, int offsetY)
        {
            ICommand command = new MapOffsetCommand(layers, offsetX, offsetY);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteMapResize(List<Layer> layers, int width, int height)
        {
            ICommand command = new MapResizeCommand(layers, width, height);
            command.Execute();

            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        public void ExecuteInitializeMap(List<Layer> layers, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
           // ICommand addLayer = new LayerAddCommand(layers, mapWidth, mapHeight);
            //ICommand addTileset
        }
    }
}
