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
    public class TilesetPresenter : ITilesetPresenter
    {
        public event Action<int[,]> SendTileBrushValues;

        private Dictionary<string, ITilesetRenderPresenter> Presenters = new Dictionary<string, ITilesetRenderPresenter>();

        public ITilesetRenderPresenter CurrentPresenter
        {
            get
            {
                return Presenters[view.GetCurrentView.KeyName];
            }
        }
        
        private readonly ITilesetView view;

        public TilesetPresenter(ITilesetView view)
        {
            this.view = view;            
        }

      
        public void AddPresenter(string name, IXnaRenderView renderView, string tilesetPath, int tileWidth, int tileHeight)
        {
            if (Presenters.ContainsKey(name))
                return;

           // view.AddView(name, renderView);

            ITilesetRenderPresenter presenter = new TilesetRenderPresenter(renderView);
            //presenter.LoadTexture(tilesetPath);
            presenter.SetTileDimesions(tileWidth, tileHeight);
            presenter.SendTileBrushValues += (values) => { if (SendTileBrushValues != null) SendTileBrushValues(values); };

            Presenters.Add(name, presenter);            
        }

        public void RemovePresenter()
        {

        }

        public void LoadForm()
        {
            view.ShowForm();
        }
    }
}
