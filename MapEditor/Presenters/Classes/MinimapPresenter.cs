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
        #region Fields

        private const int MINIMAP_WIDTH = 284;
        private const int MINIMAP_HEIGHT = 261;

        private readonly IMinimapView view;

        private Minimap minimap;

        private Texture2D pixel;

        private bool isMouseLeftPressed;

        private Vector2 cameraPosition;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;
        
        #endregion

        #region Properties

        public event Action<Camera> MinimapChanged;

        public Rectangle ViewRectangle { get; set; }    

        public Camera MinimapCamera
        {
            get { return minimap.Camera; }
            set { minimap.Camera = value; }
        }

        public bool IsScrolling
        {
            get { return minimap.IsScrolling; }
            set { minimap.IsScrolling = value; }
        }

        #endregion

        public MinimapPresenter(IMinimapView view)
        {
            this.view = view;

            this.view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            this.view.OnInitialize += new Action(view_OnInitialize);

            this.minimap = new Minimap();

            view.OnXnaDown += new MouseEventHandler(view_OnXnaDown);
            view.OnXnaUp += new MouseEventHandler(view_OnXnaUp);
            view.OnXnaMove += new MouseEventHandler(view_OnXnaMove);

        }

        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                currentMousePosition = new Vector2(e.Location.X, e.Location.Y);

                if (minimap.Camera == null)
                {

                    minimap.Camera.Position = new Vector2(MathHelper.Clamp((int)(currentMousePosition.X), 0, MINIMAP_WIDTH),
                        (MathHelper.Clamp((int)(currentMousePosition.Y), 0, MINIMAP_HEIGHT)));

                    minimap.IsScrolling = true;

                    MinimapChanged(new Camera()
                    {
                        Position = minimap.Camera.Position / minimap.CameraScale,
                    });
                }
            }
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;
            }
        }

        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseLeftPressed = true;

                previousMousePosition = new Vector2(e.Location.X, e.Location.Y);
            }
        }

        void view_OnInitialize()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                pixel = Texture2D.FromStream(view.GraphicsDevice, memoryStream);
            }
        }

        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin();

            minimap.Draw(spriteBatch, pixel);

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
