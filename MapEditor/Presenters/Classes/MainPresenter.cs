using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;
using MapEditor.Models;
using MapEditor.UI;

namespace MapEditor.Presenters
{
    /// <summary>
    /// Parent to all forms
    /// </summary>
    public class MainPresenter
    {
        #region Fields

        private readonly IMainView view;
        private readonly ILayerView layerView;

        private Dictionary<string, IMainRenderPresenter> MainPresenters = new Dictionary<string, IMainRenderPresenter>();

        private FileNewPresenter fileNewPresenter;
        private TilesetPresenter tilesetPresenter;
        

        #endregion

        #region Properties

        public IMainRenderPresenter CurrentMainPresenter
        {
            get { return MainPresenters[view.GetCurrentView.KeyName]; }
        }

        #endregion

        #region Initialize

        public MainPresenter(IMainView view, ILayerView layerView)
        {
            this.view = view;

            view.FileNew += new EventHandler(FileNew);
            view.LayerAdd += new EventHandler(view_LayerAdd);
            view.EditCopy += new EventHandler(view_EditCopy);
            view.EditCut += new EventHandler(view_EditCut);
            view.EditDraw += new EventHandler(view_EditDraw);
            view.EditPaste += new EventHandler(view_EditPaste);
            view.EditRemove += new EventHandler(view_EditRemove);
            view.EditSelect += new EventHandler(view_EditSelect);
            view.LayerClone += new EventHandler(view_LayerClone);
            view.LayerLower += new EventHandler(view_LayerLower);
            view.LayerRaise += new EventHandler(view_LayerRaise);
            view.LayerRemove += new EventHandler(view_LayerRemove);
            view.LayerVisibility += new EventHandler(view_LayerVisibility);
            view.MapOffset += new EventHandler(view_MapOffset);
            view.MapResize += new EventHandler(view_MapResize);

            layerView.AddLayerItem += new EventHandler(layerView_AddLayerItem);
            layerView.MoveLayerDown += new EventHandler(layerView_MoveLayerDown);
            layerView.MoveLayerUp += new EventHandler(layerView_MoveLayerUp);
            layerView.RemoveLayerItem += new EventHandler(layerView_RemoveLayerItem);

            fileNewPresenter = new FileNewPresenter(new FileNewView());
            tilesetPresenter = new TilesetPresenter(new TilesetView());

            fileNewPresenter.Confirmed += fileNewPresenter_Confirmed;            

            tilesetPresenter.SendTileBrushValues += (brush) => CurrentMainPresenter.TileBrushValues = brush;

         


            // TEST
           // tilesetPresenter.LoadForm();
            
           // tilesetPresenter.AddPresenter("boosh", new XnaRenderView(), @"C:\Users\Imperator\Pictures\aa.png", 32, 32);

           /* ITilesetRenderView v = new TilesetRenderView();
           // v.Dock = System.Windows.Forms.DockStyle.Fill;
            v.OnInitialize += () => System.Windows.Forms.MessageBox.Show("WORKS");
            ((MainView)view).Controls.Add((System.Windows.Forms.Control)v); */
        }

        void layerView_RemoveLayerItem(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void layerView_MoveLayerUp(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void layerView_MoveLayerDown(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void layerView_AddLayerItem(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Events

        void view_MapResize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_MapOffset(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_LayerVisibility(object sender, EventArgs e)
        {
            CurrentMainPresenter.SetLayerVisibility();
        }

        void view_LayerRemove(object sender, EventArgs e)
        {
            CurrentMainPresenter.RemoveLayer();
        }

        void view_LayerRaise(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_LayerLower(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_LayerClone(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditSelect(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditRemove(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditPaste(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditDraw(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditCut(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_EditCopy(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void view_LayerAdd(object sender, EventArgs e)
        {
            CurrentMainPresenter.AddLayer();
        }

        void fileNewPresenter_Confirmed()
        {
           // map = fileNewPresenter.GetNewMap(); 
            tilesetPresenter.LoadForm(view);
            AddMainPresenter(fileNewPresenter.MapName, new XnaRenderView(), fileNewPresenter.TilesetPath, fileNewPresenter.TileWidth, fileNewPresenter.TileHeight, fileNewPresenter.MapWidth, fileNewPresenter.MapHeight);
            tilesetPresenter.AddPresenter(fileNewPresenter.MapName, new XnaRenderView(), fileNewPresenter.TilesetPath, fileNewPresenter.TileWidth, fileNewPresenter.TileHeight);
        }

        
        void FileNew(object sender, EventArgs e)
        {
            fileNewPresenter.Load();            
        }

        #endregion

        #region Public Methods

        public void AddMainPresenter(string name, IXnaRenderView renderView, string tilesetPath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            if (MainPresenters.ContainsKey(name))
                return;

            view.AddView(name, renderView);

            IMainRenderPresenter presenter = new MainRenderPresenter(renderView);
            presenter.InitializeMap(tilesetPath, tileWidth, tileHeight, mapWidth, mapHeight);
           // presenter.AddLayer(mapWidth, mapHeight);
            //presenter.SetMapDimesions(mapWidth, mapHeight);
            //presenter.SetTileDimensions(tileWidth, tileHeight);

            MainPresenters.Add(name, presenter);
        }

        #endregion
    }
}
