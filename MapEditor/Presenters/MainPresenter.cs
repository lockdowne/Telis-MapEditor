using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.PaintTools;
using MapEditor.Core.EventsArgs;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public class MainPresenter
    {
        #region Fields

        private readonly IMainView mainView;
        private readonly ILayerView layerView;        
        private readonly INewMapView newMapView;
        private readonly IOffsetView offsetView;
        private readonly IResizeMapView resizeMapView;
        private readonly IResizeTileView resizeTileView;        
        private readonly ITilesetPresenter tilesetPresenter;
        private readonly IMinimapPresenter minimapPresenter;
        private readonly IRenameView renameView;

        private Dictionary<string, Map> Maps = new Dictionary<string, Map>();

        #endregion

        #region Initialize

        public MainPresenter(IMainView mainView,
            ILayerView layerView,
            INewMapView newMapView,
            IOffsetView offsetView,
            IResizeMapView resizeMapView,
            IResizeTileView resizerTileView,
            ITilesetView tilesetView,
            IMinimapView minimapView,
            IRenameView renameView)
        {
            this.mainView = mainView;
            this.layerView = layerView;
            this.newMapView = newMapView;
            this.offsetView = offsetView;
            this.resizeMapView = resizeMapView;
            this.resizeTileView = resizerTileView;
            this.tilesetPresenter = new TilesetPresenter(tilesetView);
            this.minimapPresenter = new MinimapPresenter(minimapView);
            this.renameView = renameView;

            SubscribeLayerEvents();
            SubscribeMainEvents();
            SubscribeMinimapEvents();            
            SubscribeOffsetViewEvents();
            SubscribeResizeMapEvents();
            SubscribeResizeTileEvents();
            SubscribeTilesetEvents();
            SubscribeNewMapEvents();
            SubscribeRenameEvents();
        }

        private void SubscribeMainEvents()
        {
            if (mainView == null)
                return;

            mainView.OnEditCopy += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].CopySelection();

                };

            mainView.OnEditCut += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].CutSelection();
                };

            mainView.OnEditDraw += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetPaintTool(PaintTool.Draw);
                };

            mainView.OnEditErase += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetPaintTool(PaintTool.Erase);
                };

            mainView.OnEditFill += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetPaintTool(PaintTool.Fill);
                };

            mainView.OnEditRedo += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].Redo();
                };

            mainView.OnEditSelect += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetPaintTool(PaintTool.Select);
                };

            mainView.OnEditUndo += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].Undo();
                };

            mainView.OnFileClose += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    // Check for save
                    Maps.Remove(mainView.SelectedTabName);
                    mainView.RemoveTab(mainView.SelectedTabName);                    
                };

            mainView.OnFileCloseAll += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    // Check for saves
                    Maps.Clear();
                    mainView.RemoveAllTabs();
                };

            mainView.OnFileExit += () =>
                {
                    // Check for save
                    Application.Exit();
                };

            mainView.OnFileNew += () =>
                {
                    newMapView.ShowWindow();
                };

            mainView.OnFileOpen += () =>
                {
                    // open file
                };

            mainView.OnFileSave += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    // save file
                };

            mainView.OnFileSaveAs += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    // save as file
                };

            mainView.OnLayerAdd += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;
                    
                    Maps[mainView.SelectedTabName].AddLayer(resizeMapView.MapWidth, resizeMapView.MapHeight);
                };

            mainView.OnLayerDuplicate += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].CloneLayer();
                };

            mainView.OnLayerLower += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].LowerLayer();
                };

            mainView.OnLayerRaise += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].RaiseLayer();
                };

            mainView.OnLayerRemove += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].RemoveLayer();
                };

            mainView.OnLayerRename += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (renameView == null)
                        return;

                    renameView.ShowWindow((Form)mainView);
                };

            mainView.OnLayerVisibility += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetLayerVisibility();
                };

            mainView.OnMapOffset += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (offsetView == null)
                        return;

                    offsetView.ShowWindow();
                };

            mainView.OnMapResizeMap += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (resizeMapView == null)
                        return;

                    resizeMapView.ShowWindow();
                };

            mainView.OnMapResizeTile += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (resizeTileView == null)
                        return;

                    resizeTileView.ShowWindow();
                };

            mainView.OnViewGrid += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    // Show grid
                };

            mainView.OnViewLayers += (isChecked) =>
                {
                    if (layerView == null)
                        return;

                    if (isChecked)
                        layerView.ShowWindow(mainView);
                    else
                        layerView.HideWindow();
                };

            mainView.OnViewMinimap += (isChecked) =>
                {
                    if (isChecked)
                        minimapPresenter.ShowWindow(mainView);
                    else
                        minimapPresenter.HideWindow();
                };

            mainView.OnViewTileset += (isChecked) =>
                {
                    if (isChecked)
                        tilesetPresenter.ShowWindow(mainView);
                    else
                        tilesetPresenter.HideWindow();
                };

            mainView.OnZoomIn += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].ZoomIn();
                };

            mainView.OnZoomOut += () =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].ZoomOut();
                };

            mainView.OnSelectedTabChanged += (name) =>
                {
                    if (!Maps.ContainsKey(name))
                        return;

                    // Update layers
                    layerView.ClearAllListItems();

                    Maps[mainView.SelectedTabName].Layers.ForEach(layer =>
                        {
                            layerView.AddListItem(layer.LayerName);
                        });

                    minimapPresenter.GenerateMinimap(Maps[mainView.SelectedTabName].Layers, Maps[mainView.SelectedTabName].Tileset);

                    tilesetPresenter.SetTileset(Maps[mainView.SelectedTabName].Tileset);

                    Maps[mainView.SelectedTabName].ClearSelectionBox();
                };
        }

        private void SubscribeLayerEvents()
        {
            if (layerView == null)
                return;

            layerView.OnAddLayer += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if(resizeMapView == null)
                        return;

                    Maps[mainView.SelectedTabName].AddLayer(resizeMapView.MapWidth, resizeMapView.MapHeight);
                };

            layerView.OnDuplicateLayer += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].CloneLayer();
                };

            layerView.OnLayerIndexChanged += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].LayerIndex = layer.LayerIndex;
                };

            layerView.OnMoveLayerDown += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].LowerLayer();
                };

            layerView.OnMoveLayerUp += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].RaiseLayer();
                };

            layerView.OnRemoveLayer += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].RemoveLayer();
                };

            layerView.OnRenameLayer += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (renameView == null)
                        return;

                    renameView.ShowWindow((Form)mainView);
                    
                };

            layerView.OnLayerVisibilityChanged += (layer) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].SetLayerVisibility();
                };
         
            
        }       

        private void SubscribeOffsetViewEvents()
        {
            if (offsetView == null)
                return;

            offsetView.OnCancel += () =>
                {
                    offsetView.CloseWindow();
                };

            offsetView.OnConfirm += (offset) =>
                {
                    try
                    {

                        if (mainView.SelectedTabName == string.Empty)
                            throw new Exception("No map exists to offset");

                        Maps[mainView.SelectedTabName].OffsetMap(offset.X, offset.Y);

                        offsetView.CloseWindow();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };
        }

        private void SubscribeResizeMapEvents()
        {
            if (resizeMapView == null)
                return;

            resizeMapView.OnCancel += () =>
                {
                    resizeMapView.CloseWindow();
                };

            resizeMapView.OnConfirm += (resize) =>
                {
                    try
                    {
                        if (mainView.SelectedTabName == string.Empty)
                            throw new Exception("No map exists to resize");

                        Maps[mainView.SelectedTabName].ResizeMap(resize.MapWidth, resize.MapHeight);

                        resizeMapView.CloseWindow();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };
        }

        private void SubscribeResizeTileEvents()
        {
            if (resizeTileView == null)
                return;

            resizeTileView.OnCancel += () =>
                {
                    resizeTileView.CloseWindow();
                };

            resizeTileView.OnConfirm += (resize) =>
                {
                    try
                    {
                        if (mainView.SelectedTabName == string.Empty)
                            throw new Exception("No map exists to resize");

                        Maps[mainView.SelectedTabName].ResizeTiles(resize.TileWidth, resize.TileHeight);

                        resizeTileView.CloseWindow();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };
        }
      
        private void SubscribeTilesetEvents()
        {
            tilesetPresenter.OnTileBrushChanged += (tiles) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    if (mainView.SelectedTabName == null)
                        return;

                    Maps[mainView.SelectedTabName].TileBrushValues = tiles.TileBrushValue;
                };
        }

        private void SubscribeMinimapEvents()
        {
            minimapPresenter.OnCameraChanged += (camera) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].Camera.Position = camera.Camera.Position;
                };
        }
        
        private void SubscribeNewMapEvents()
        {
            if (newMapView == null)
                return;

            newMapView.OnBrowse += () =>
                {
                    try
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();

                        openFileDialog.Filter = "Image Files (.bmp, .jpeg, .jpg, .png)|*.bmp;*.jpeg;*.jpg*;*.png;";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.Multiselect = false;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            newMapView.TilesetPath = openFileDialog.FileName;
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };

            newMapView.OnCancel += () =>
                {
                    newMapView.CloseWindow();
                    newMapView.Reset();
                };

            newMapView.OnConfirm += (newMap) =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(newMap.MapName))
                        {
                            newMapView.DisplayMessage("Enter a name for the map", "Map name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(newMap.TilesetPath))
                        {
                            newMapView.DisplayMessage("Browse for a tileset image", "Tileset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (Maps.ContainsKey(newMap.MapName))
                        {
                            newMapView.DisplayMessage("Map name is already open", "Map name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (!AddMap(newMap.MapName, newMap.TilesetPath, newMap.TileWidth, newMap.TileHeight, newMap.MapWidth, newMap.MapHeight))
                        {
                            throw new Exception("Unable to create map");
                        }
                        else
                        {
                            // Good news everyone!
                            newMapView.CloseWindow();
                            newMapView.Reset();
                            tilesetPresenter.ShowWindow(mainView);
                            minimapPresenter.ShowWindow(mainView);
                            layerView.ShowWindow(mainView);
                            minimapPresenter.GenerateMinimap(Maps[mainView.SelectedTabName].Layers, Maps[mainView.SelectedTabName].Tileset);
                            Maps[mainView.SelectedTabName].ClearUndoRedo();
                        }
                    }
                    catch(Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };

            newMapView.OnNumericValueChanged += (newMap) =>
                {
                    newMapView.PixelDisplayer = newMap.MapWidth * newMap.TileWidth + " x " + newMap.MapHeight * newMap.TileHeight + " pixels";
                };

            newMapView.OnWindowLoaded += (newMap) =>
                {
                    newMapView.PixelDisplayer = newMap.MapWidth * newMap.TileWidth + " x " + newMap.MapHeight * newMap.TileHeight + " pixels";
                };

        }

        private void SubscribeRenameEvents()
        {
            if (renameView == null)
                return;

            renameView.OnCancel += () =>
                {
                    renameView.CloseWindow();
                };

            renameView.OnConfirm += (name) =>
                {
                    if (mainView.SelectedTabName == string.Empty)
                        return;

                    Maps[mainView.SelectedTabName].RenameLayer(name.Name);

                    renameView.ClearTextBox();

                    renameView.CloseWindow();
                };
        }

        #endregion

        #region Methods

        public bool AddMap(string name, string tilesetPath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            if (Maps.ContainsKey(name))
                return false;

            try
            { 
                mainView.AddTab(name);

                Map map = new Map(mainView.SelectedXnaTab.GraphicsDevice, tilesetPath, tileWidth, tileHeight);
                map.ResizeTiles(tileWidth, tileHeight);
                map.AddLayer(mapWidth, mapHeight);
                tilesetPresenter.SetTileset(map.Tileset);
                layerView.AddListItem(map.Layers.First().LayerName);

                map.OnCameraChanged += (camera) =>
                    {
                        minimapPresenter.Camera = camera.Camera;
                        minimapPresenter.IsScrolling = false;
                    };

                map.OnLayerChanged += (index) =>
                    {
                        if (mainView.SelectedTabName == string.Empty)
                            return;

                        if (layerView == null)
                            return;

                        layerView.ClearAllListItems();

                        Maps[mainView.SelectedTabName].Layers.ForEach(layer =>
                            {
                                layerView.AddListItem(layer.LayerName);
                            });

                        layerView.SelectedIndex = index.LayerIndex;
                    };

                map.OnMapChanged += () =>
                    {
                        minimapPresenter.GenerateMinimap(map.Layers, map.Tileset);
                        minimapPresenter.MapViewport = new Vector2(mainView.ViewportWidth, mainView.ViewportHeight);
                    };
                

                mainView.SelectedXnaTab.OnDraw += (spriteBatch) =>
                    {
                        map.Draw(spriteBatch);
                    };

                mainView.SelectedXnaTab.OnInitialize += () =>
                    {

                    };

                mainView.SelectedXnaTab.OnXnaDown += (sender, e) =>
                    {
                        map.MouseDown(sender, e);
                    };

                mainView.SelectedXnaTab.OnXnaUp += (sender, e) =>
                    {
                        map.MouseUp(sender, e);
                    };

                mainView.SelectedXnaTab.OnXnaMove += (sender, e) =>
                    {
                        map.MouseMove(sender, e);
                    };

                mainView.SelectedXnaTab.OnXnaWheel += (sender, e) =>
                    {
                        map.MouseWheel(sender, e);
                    };

                Maps.Add(name, map);

                if (resizeMapView == null)
                    return false;

                resizeMapView.MapWidth = mapWidth;
                resizeMapView.MapHeight = mapHeight;

                if (resizeTileView == null)
                    return false;

                resizeTileView.TileWidth = tileWidth;
                resizeTileView.TileHeight = tileHeight;
            }
            catch
            {
                Maps.Remove(name);
                mainView.RemoveTab(name);
                layerView.ClearAllListItems();

                return false;
            }

            return true;
        }

        #endregion
    }
}
