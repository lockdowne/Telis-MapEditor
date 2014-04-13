using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core;
using MapEditor.Core.Controls;
using MapEditor.Core.PaintTools;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    /// <summary>
    /// Main holder of logic between UIs and Models
    /// </summary>
    public class MainPresenter
    {
        #region Fields

        private readonly IMainView mainView;
        private readonly ILayerView layerView;
        private readonly IOffsetView offsetView;
        private readonly IMapResizeView mapResizeView;
        private readonly IFileNewView fileNewView;
        private readonly ITilesetPresenter tilesetPresenter;
        private readonly ITileResizeView tileResizeView;
        private readonly IAddTilesetView addTilesetView;

        private Dictionary<string, IMainRenderPresenter> MainPresenters = new Dictionary<string, IMainRenderPresenter>();


        #endregion

        #region Properties

        /// <summary>
        /// Get current map presenter
        /// </summary>
        public IMainRenderPresenter CurrentMainPresenter
        {
            get 
            {
                if(mainView.GetCurrentView != null)
                    if (MainPresenters.ContainsKey(mainView.GetCurrentView.KeyName))
                        return MainPresenters[mainView.GetCurrentView.KeyName];

                return null;
            }
        }

        #endregion

        #region Initialize

        public MainPresenter(IMainView mainView, ILayerView layerView, IOffsetView offsetView, IMapResizeView mapResizeView, IFileNewView fileNewView, ITilesetView tilesetView, ITileResizeView tileResizeView, IAddTilesetView addTilesetView)
        {
            this.mainView = mainView;
            this.layerView = layerView;
            this.offsetView = offsetView;
            this.mapResizeView = mapResizeView;
            this.fileNewView = fileNewView;
            this.tilesetPresenter = new TilesetPresenter(tilesetView);
            this.tileResizeView = tileResizeView;
            this.addTilesetView = addTilesetView;

            SubscribeMainViewEvents();
            SubscribeLayerViewEvents();
            SubscribeOffsetViewEvents();
            SubscribeFileNewViewEvents();
            SubscribeTilesetViewEvents();
            SubscribeMapResizeViewEvents();
            SubscribeTileResizeViewEvents();
            SubscribeAddTilesetViewEvents();
        }


        private void SubscribeMainViewEvents()
        {
            if (mainView == null)
                return;

            mainView.FileNew += (sender, e) =>
            {
                if (fileNewView != null)
                    fileNewView.ShowForm();
            };

            mainView.FileOpen += (sender, e) =>
            {
                // TODO: open file
            };

            mainView.FileSave += (sender, e) =>
            {
                // TODO: save file
            };

            mainView.FileSaveAs += (sender, e) =>
            {
                // TODO: save as
            };

            mainView.FileClose += (sender, e) =>
            {
                // TODO: close file
            };

            mainView.FileCloseAll += (sender, e) =>
            {
                // TODO: close all files
            };

            mainView.FileExit += (sender, e) =>
            {
                // TODO: exit system
            };

            mainView.EditCopy += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.CopySelection();
            };

            mainView.EditCut += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.CutSelection();
            };

            mainView.EditDraw += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.SetPaintTool(PaintTool.Draw);
            };

            mainView.EditErase += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.SetPaintTool(PaintTool.Erase);
            };

            mainView.EditSelect += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.SetPaintTool(PaintTool.Select);
            };

            mainView.EditFill += (sender, e) =>
            {
                // TODO: flood fill algorithm
            };

            mainView.EditUndo += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.Undo();
            };

            mainView.EditRedo += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.Redo();
            };

            mainView.ViewShowGrid += (sender, e) =>
            {
                // TODO: show grid
            };

            mainView.ViewZoomIn += (sender, e) =>
            {
                // TODO: zoom in
            };

            mainView.ViewZoomOut += (sender, e) =>
            {
                // TODO: zoom out
            };
            
            mainView.ViewShowLayerForm += (sender, e) =>
            {
                if (layerView != null)
                    layerView.ShowForm(mainView);
            };

            mainView.ViewShowTilesetForm += (sender, e) =>
            {
                if (tilesetPresenter != null)
                    tilesetPresenter.LoadForm(mainView);
            };

            mainView.ViewShowMinimapForm += (sender, e) =>
            {
                // TODO: show minimap
            };

            mainView.LayerClone += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                        CurrentMainPresenter.CloneLayer(layerView.CheckedListBox);
            };

            mainView.LayerAdd += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                       if(mapResizeView != null)
                            CurrentMainPresenter.AddLayer(layerView.CheckedListBox, mapResizeView.MapWidth, mapResizeView.MapHeight);
            };

            mainView.LayerLower += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                        CurrentMainPresenter.LowerLayer(layerView.CheckedListBox);
            };

            mainView.LayerRaise += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                        CurrentMainPresenter.RaiseLayer(layerView.CheckedListBox);
            };

            mainView.LayerRemove += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                        CurrentMainPresenter.RemoveLayer(layerView.CheckedListBox);
            };

            mainView.MapOffset += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (offsetView != null)
                        offsetView.ShowForm();
            };

            mainView.MapResize += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (mapResizeView != null)
                        mapResizeView.ShowForm();
            };            

            mainView.TileResize += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (tileResizeView != null)
                        tileResizeView.ShowForm();
            };

            mainView.AddTileset += (sender, e) =>
                {
                    if (CurrentMainPresenter != null)
                        if (addTilesetView != null)
                            addTilesetView.ShowForm();
                };

            mainView.RemoveTileset += (sender, e) =>
                {
                    // TODO: remove tileset
                };

            mainView.ViewClosing += (sender, e) =>
                {
                    e.Cancel = false;
                };

            
        }

        private void SubscribeLayerViewEvents()
        {
            if (layerView == null)
                return;

            layerView.AddLayerItem += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if(mapResizeView != null)
                        CurrentMainPresenter.AddLayer(layerView.CheckedListBox, mapResizeView.MapWidth, mapResizeView.MapHeight);
            };

            layerView.MoveLayerDown += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.LowerLayer(layerView.CheckedListBox);
            };

            layerView.MoveLayerUp += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.RaiseLayer(layerView.CheckedListBox);
            };

            layerView.RemoveLayerItem += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.RemoveLayer(layerView.CheckedListBox);
            };

            layerView.LayerIndexChanged += (sender, e) =>
            {
                // TODO: Need work around for finding check state of checkedBoxList item and to invoke layer visibility event               
            };

            layerView.DuplicateLayer += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.CloneLayer(layerView.CheckedListBox);
            };
        }

        private void SubscribeOffsetViewEvents()
        {
            if (offsetView == null)
                return;

            offsetView.OnCancel += (sender, e) =>
            {
                offsetView.CloseForm();
            };

            offsetView.OnConfirm += (sender, e) =>
            {
                offsetView.CloseForm();

                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.OffsetMap(offsetView.OffsetX, offsetView.OffsetY);
            };
        }

        private void SubscribeFileNewViewEvents()
        {
            if (fileNewView == null)
                return;

            fileNewView.Browse += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Image Files (.bmp, .jpeg, .jpg, .png)|*.bmp;*.jpeg;*.jpg*;*.png;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileNewView.TilesetPath = openFileDialog.FileName;
                }
            };

            fileNewView.Confirm += (sender, e) =>
            {
                fileNewView.CloseForm();

                // TODO: Change below code accordingly 

                tilesetPresenter.LoadForm(mainView);
                layerView.ShowForm(mainView);

                AddMainPresenter(fileNewView.FileName, new XnaRenderView(), fileNewView.TilesetPath, fileNewView.TileWidth, fileNewView.TileHeight, fileNewView.MapWidth, fileNewView.MapHeight);
                tilesetPresenter.AddPresenter(new XnaRenderView(), fileNewView.TilesetPath, fileNewView.TileWidth, fileNewView.TileHeight);
                
                

                layerView.CheckedListBox.SelectedIndex = 0;
                mapResizeView.MapWidth = fileNewView.MapWidth;
                mapResizeView.MapHeight = fileNewView.MapHeight;
            };

            fileNewView.Cancel += (sender, e) =>
            {
                fileNewView.CloseForm();
            };

            fileNewView.ValueChanged += (sender, e) =>
            {
                fileNewView.Display = fileNewView.MapWidth * fileNewView.TileWidth + " x " + fileNewView.MapHeight * fileNewView.TileHeight + " pixels";
            };
        }

        private void SubscribeTilesetViewEvents()
        {
            if (tilesetPresenter == null)
                return;

            tilesetPresenter.SendTileBrushValues += (brush) =>
                {
                    if (CurrentMainPresenter != null)
                        CurrentMainPresenter.TileBrushValues = brush;
                };
        }

        private void SubscribeMapResizeViewEvents()
        {
            if (mapResizeView == null)
                return;

            mapResizeView.OnConfirm += (sender, e) =>
                {
                    mapResizeView.CloseForm();

                    if (CurrentMainPresenter != null)
                        CurrentMainPresenter.ResizeMap(mapResizeView.MapWidth, mapResizeView.MapHeight, mapResizeView.MapWidthNumeric, mapResizeView.MapHeightNumeric);
                };

            mapResizeView.OnCancel += (sender, e) =>
                {
                    mapResizeView.CloseForm();
                };
        }

        private void SubscribeTileResizeViewEvents()
        {
            if (tileResizeView == null)
                return;

            tileResizeView.OnConfirm += (sender, e) =>
                {
                    tileResizeView.CloseForm();

                    if (CurrentMainPresenter != null)
                    {                        
                        CurrentMainPresenter.ResizeTiles(tileResizeView.TileWidth, tileResizeView.TileHeight, tileResizeView.TileWidthNumeric,
                            tileResizeView.TileHeightNumeric, tilesetPresenter.AllPresenters.Select<ITilesetRenderPresenter, Action<int,int>>(x => x.SetTileDimesions).ToList());
                    }
                };

            tileResizeView.OnCancel += (sender, e) =>
                {
                    tileResizeView.CloseForm();
                };
        }

        private void SubscribeAddTilesetViewEvents()
        {
            if (addTilesetView == null)
                return;

            addTilesetView.OnBrowse += (sender, e) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();

                    openFileDialog.Filter = "Image Files (.bmp, .jpeg, .jpg, .png)|*.bmp;*.jpeg;*.jpg*;*.png;";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        addTilesetView.TilesetPath = openFileDialog.FileName;
                    }
                };

            addTilesetView.OnCancel += (sender, e) =>
                {
                    addTilesetView.CloseForm();
                };

            addTilesetView.OnConfirm += (sender, e) =>
                {
                    addTilesetView.CloseForm();

                    if (CurrentMainPresenter != null)
                        if(tileResizeView != null)
                            if(tilesetPresenter != null)
                                CurrentMainPresenter.AddTileset(addTilesetView.TilesetPath, tileResizeView.TileWidth, tileResizeView.TileHeight, tilesetPresenter.AddPresenter, tilesetPresenter.RemovePresenter);
                };
        }

        #endregion             

        #region Methods

        /// <summary>
        /// Add new tab with presenter to main view
        /// </summary>
        /// <param name="name"></param>
        /// <param name="renderView"></param>
        /// <param name="tilesetPath"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public void AddMainPresenter(string name, IXnaRenderView renderView, string tilesetPath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            if (MainPresenters.ContainsKey(name))
                return;

            mainView.AddView(name, renderView);

            IMainRenderPresenter presenter = new MainRenderPresenter(renderView);
            presenter.InitializeMap(tilesetPath, tileWidth, tileHeight, mapWidth, mapHeight, layerView.CheckedListBox);
           // presenter.AddLayer(mapWidth, mapHeight);
            //presenter.SetMapDimesions(mapWidth, mapHeight);
            //presenter.SetTileDimensions(tileWidth, tileHeight);

            MainPresenters.Add(name, presenter);
        }

        #endregion
    }
}
