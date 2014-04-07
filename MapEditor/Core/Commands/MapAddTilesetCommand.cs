using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Controls;
using MapEditor.Models;
using MapEditor.Presenters;
using MapEditor.UI;

namespace MapEditor.Core.Commands
{
    public class MapAddTilesetCommand : ICommand
    {
        private IMainRenderPresenter mapPresenter;
        private ITilesetPresenter tilesetPresenter;

        private string texturePath;

        private int tileWidth;
        private int tileHeight;

        public MapAddTilesetCommand(string texturePath, int tileWidth, int tileHeight, IMainRenderPresenter mapPresenter, ITilesetPresenter tilesetPresenter)
        {
            this.mapPresenter = mapPresenter;
            this.tilesetPresenter = tilesetPresenter;

            this.texturePath = texturePath;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void Execute()
        {
            string name = Path.GetFileName(texturePath);

            IXnaRenderView view = new XnaRenderView(); 
            
            ITilesetView tilesetView = new TilesetView();
            tilesetView.AddView(name, view);      

            mapPresenter.AddTilesetImage(texturePath, tileWidth, tileHeight);                 

            tilesetPresenter.AddPresenter(name.Split('.').FirstOrDefault(), view, texturePath, tileWidth, tileHeight);
        }

        public void UnExecute()
        {
           
        }
    }
}
