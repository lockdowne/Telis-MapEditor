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

// TODO: Hook up selected tab changed event for main view
// TODO: Known bug => temp tile brushes are visible when attempting to draw brushes.. right clicking.. then reenabling the draw paint option
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
        private readonly IMinimapPresenter minimapPresenter;

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

        public MainPresenter(IMainView mainView, ILayerView layerView, IOffsetView offsetView,
            IMapResizeView mapResizeView, IFileNewView fileNewView, ITilesetView tilesetView,
            ITileResizeView tileResizeView, IAddTilesetView addTilesetView, IMinimapView minimapView)
        {
            this.mainView = mainView;
            this.layerView = layerView;
            this.offsetView = offsetView;
            this.mapResizeView = mapResizeView;
            this.fileNewView = fileNewView;
            this.tilesetPresenter = new TilesetPresenter(tilesetView);
            this.tileResizeView = tileResizeView;
            this.addTilesetView = addTilesetView;
            this.minimapPresenter = new MinimapPresenter(minimapView);

            SubscribeMainViewEvents();
            SubscribeLayerViewEvents();
            SubscribeOffsetViewEvents();
            SubscribeFileNewViewEvents();
            SubscribeTilesetPresenterEvents();
            SubscribeMapResizeViewEvents();
            SubscribeTileResizeViewEvents();
            SubscribeAddTilesetViewEvents();
            SubscribeMinimapPresenterEvents();

       
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
                if (CurrentMainPresenter != null)
                    CurrentMainPresenter.SetPaintTool(PaintTool.Fill);
            };

            mainView.EditUndo += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.Undo();
                    UpdateLayerView();
                }
            };

            mainView.EditRedo += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.Redo();
                    UpdateLayerView();
                }
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
                    {
                        CurrentMainPresenter.CloneLayer();

                        UpdateLayerView();
                    }
            };

            mainView.LayerAdd += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                        if (mapResizeView != null)
                        {
                            CurrentMainPresenter.AddLayer(mapResizeView.MapWidth, mapResizeView.MapHeight);

                            UpdateLayerView();
                        }
            };

            mainView.LayerLower += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                    {
                        CurrentMainPresenter.LowerLayer();

                        UpdateLayerView();
                    }
            };

            mainView.LayerRaise += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                    {
                        CurrentMainPresenter.RaiseLayer();

                        UpdateLayerView();
                    }
            };

            mainView.LayerRemove += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (layerView != null)
                    {
                        CurrentMainPresenter.RemoveLayer();

                        UpdateLayerView();
                    }
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


            mainView.SelectedTabChanged += (sender, e) =>
                {
                    if (CurrentMainPresenter == null)
                        return;
                    
                    UpdateLayerView();
                    minimapPresenter.GenerateMinimap(CurrentMainPresenter.Layers, CurrentMainPresenter.Tilesets);
                    UpdateTilesetView();
                };

           

                
           
        }

        private void SubscribeLayerViewEvents()
        {
            if (layerView == null)
                return;

            layerView.AddLayerItem += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                    if (mapResizeView != null)
                    {
                        CurrentMainPresenter.AddLayer(mapResizeView.MapWidth, mapResizeView.MapHeight);

                        UpdateLayerView();
                    }
            };

            layerView.MoveLayerDown += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.LowerLayer();

                    UpdateLayerView();
                }
            };

            layerView.MoveLayerUp += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.RaiseLayer();

                    UpdateLayerView();

                   
                }
            };

            layerView.RemoveLayerItem += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.RemoveLayer();

                    UpdateLayerView();
                }
            };

            layerView.DuplicateLayer += (sender, e) =>
            {
                if (CurrentMainPresenter != null)
                {
                    CurrentMainPresenter.CloneLayer();

                    UpdateLayerView();
                }
            };

            layerView.LayerIndexChanged += (index) =>
                {
                    if (CurrentMainPresenter != null)
                    {
                        CurrentMainPresenter.LayerIndex = index;
                    }
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
                if (string.IsNullOrWhiteSpace(fileNewView.FileName))
                {
                    fileNewView.DisplayMessage("Enter a name for project");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fileNewView.TilesetPath))
                {
                    fileNewView.DisplayMessage("Select an image for tileset");
                    return;
                }

                if (MainPresenters.ContainsKey(fileNewView.FileName))
                {
                    fileNewView.DisplayMessage("Project name is already open");
                    return;
                }
                
                fileNewView.CloseForm();

                // TODO: Change below code accordingly 
                
                tilesetPresenter.LoadForm(mainView);
                layerView.ShowForm(mainView);
                minimapPresenter.ShowForm(mainView);

                AddMainPresenter(fileNewView.FileName, fileNewView.TilesetPath, fileNewView.TileWidth, fileNewView.TileHeight, fileNewView.MapWidth, fileNewView.MapHeight);
                //tilesetPresenter.AddPresenter(fileNewView.TilesetPath, fileNewView.TileWidth, fileNewView.TileHeight);


                mapResizeView.MapWidth = fileNewView.MapWidth;
                mapResizeView.MapHeight = fileNewView.MapHeight;                



                minimapPresenter.GenerateMinimap(CurrentMainPresenter.Layers, CurrentMainPresenter.Tilesets);
                //minimapPresenter.SetFormSize(mapResizeView.MapWidth, mapResizeView.MapHeight);

                UpdateLayerView();

                UpdateTilesetView();
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

        private void SubscribeTilesetPresenterEvents()
        {
            if (tilesetPresenter == null)
                return;

            tilesetPresenter.OnSendTileBrushValues += (brush) =>
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
                    {
                        CurrentMainPresenter.ResizeMap(mapResizeView.MapWidth, mapResizeView.MapHeight, mapResizeView.MapWidthNumeric, mapResizeView.MapHeightNumeric);

                        if(minimapPresenter != null)
                            minimapPresenter.SetFormSize(mapResizeView.MapWidth, mapResizeView.MapHeight);
                    }
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

        private void SubscribeMinimapPresenterEvents()
        {
            if (minimapPresenter == null)
                return;

            minimapPresenter.MinimapChanged += (camera) =>
                {
                    if (CurrentMainPresenter != null)
                        CurrentMainPresenter.Camera.Position = camera.Position;
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
        public void AddMainPresenter(string name, string tilesetPath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            if (MainPresenters.ContainsKey(name))
                return;

            IXnaRenderView renderView = new XnaRenderView();

            mainView.AddView(name, renderView);

            IMainRenderPresenter presenter = new MainRenderPresenter(renderView);
            presenter.InitializeMap(tilesetPath, tileWidth, tileHeight, mapWidth, mapHeight);
            presenter.MapChanged += () =>
                {                    
                    minimapPresenter.GenerateMinimap(CurrentMainPresenter.Layers, CurrentMainPresenter.Tilesets);
                };
            presenter.CameraChanged += (camera) =>
                {
                    minimapPresenter.MinimapCamera = new Camera()
                    {
                        Position = camera.Position,
                        ViewportHeight = mainView.ControlHeight,
                        ViewportWidth = mainView.ControlWidth,
                        Zoom = camera.Zoom
                    };

                    minimapPresenter.IsScrolling = false;
                };

            MainPresenters.Add(name, presenter);
        }

        private void UpdateLayerView()
        {
            if (layerView == null)
                return;

            if (CurrentMainPresenter == null)
                return;

            layerView.CheckedListBox.Items.Clear();

            CurrentMainPresenter.Layers.ForEach(layer =>
                {
                    object item = layer.LayerName;

                    layerView.CheckedListBox.Items.Add(item, layer.IsVisible);                    
                });           

            
            if(layerView.CheckedListBox.Items.Count > 0)
                layerView.CheckedListBox.SelectedIndex = CurrentMainPresenter.LayerIndex;
        }

        private void UpdateTilesetView()
        {
            if (tilesetPresenter == null)
                return;

            if (CurrentMainPresenter == null)
                return;

            tilesetPresenter.Clear();

            CurrentMainPresenter.Tilesets.ForEach(tileset =>
                {
                    tilesetPresenter.AddPresenter(tileset.TexturePath, tileset.TileWidth, tileset.TileHeight);
                });
        }

        #endregion
    }
}
