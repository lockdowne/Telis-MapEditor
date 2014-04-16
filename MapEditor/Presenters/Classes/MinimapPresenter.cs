using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public class MinimapPresenter : IMinimapPresenter
    {
        private readonly IMinimapView view;

        private Minimap minimap;

        public Rectangle ViewRectangle { get; set; }

        public MinimapPresenter(IMinimapView view)
        {
            this.view = view;

            this.view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            this.view.OnInitialize += new Action(view_OnInitialize);

            this.minimap = new Minimap();

            view.OnXnaDown += new EventHandler(view_OnXnaDown);
            view.OnXnaUp += new EventHandler(view_OnXnaUp);
            view.OnXnaMove += new EventHandler(view_OnXnaMove);
        }

        void view_OnXnaMove(object sender, EventArgs e)
        {
           
        }

        void view_OnXnaUp(object sender, EventArgs e)
        {
            
        }

        void view_OnXnaDown(object sender, EventArgs e)
        {
            
        }

        void view_OnInitialize()
        {
            
        }

        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin();

            minimap.Draw(spriteBatch);

            spriteBatch.End();            
        }

        public void GenerateMinimap(List<Layer> layers, List<Tileset> tilesets)
        {            
            minimap.GenerateMinimap(view.GraphicsDevice, layers, tilesets);
        }

        public void ShowForm(IMainView parent)
        {
            view.ShowForm(parent);
        }

        public void SetFormSize(int width, int height)
        {
            view.SetFormSize(width, height);
        }
    }
}
