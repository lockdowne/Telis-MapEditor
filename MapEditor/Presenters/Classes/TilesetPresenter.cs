using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Delegates multiple tileset presenters
    /// </summary>
    public class TilesetPresenter : ITilesetPresenter
    {
        #region Fields

        private readonly ITilesetView view;       

        private Dictionary<string, ITilesetRenderPresenter> Presenters = new Dictionary<string, ITilesetRenderPresenter>();

        #endregion

        #region Properties
 
        public event Action<int[,]> SendTileBrushValues;

        public ITilesetRenderPresenter CurrentPresenter
        {
            get
            {
                if(Presenters.ContainsKey(view.GetCurrentView.KeyName))
                    return Presenters[view.GetCurrentView.KeyName];

                return null;
            }
        }

        #endregion

        #region Initialize

        public TilesetPresenter(ITilesetView view)
        {
            this.view = view;            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new tab to tileset view and tileset presenter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="renderView"></param>
        /// <param name="tilesetPath"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void AddPresenter(string name, IXnaRenderView renderView, string tilesetPath, int tileWidth, int tileHeight)
        {
            if (Presenters.ContainsKey(name))
                return;

            view.AddView(name, renderView);

            ITilesetRenderPresenter presenter = new TilesetRenderPresenter(renderView);
            presenter.LoadTexture(tilesetPath);
            presenter.SetTileDimesions(tileWidth, tileHeight);
            presenter.SendTileBrushValues += (values) => { if (SendTileBrushValues != null) SendTileBrushValues(values); };

            Presenters.Add(name, presenter);            
        }

        public void RemovePresenter(ITilesetRenderPresenter presenter)
        {

        }

        /// <summary>
        /// Show form
        /// </summary>
        /// <param name="parent"></param>
        public void LoadForm(IMainView parent)
        {
            view.ShowForm(parent);
        }

        #endregion
    }
}
