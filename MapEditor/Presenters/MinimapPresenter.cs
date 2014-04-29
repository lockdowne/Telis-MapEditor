using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.EventsArgs;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    /// <summary>
    /// Represents logic for minimap
    /// </summary>
    public class MinimapPresenter : IMinimapPresenter
    {
        #region Fields

        private readonly IMinimapView view;

        private Minimap minimap;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when minimap camera has changed
        /// </summary>
        public event CameraEventHandler OnCameraChanged;

        /// <summary>
        /// Occurs when hide is called
        /// </summary>
        public event Action OnHiding;

        /// <summary>
        /// Occurs when show is called
        /// </summary>
        public event Action OnShowing;

        /// <summary>
        /// Gets or sets the minimap camera
        /// </summary>
        public Camera Camera
        {
            get { return minimap.Camera; }
            set { minimap.Camera = value; }
        }

        /// <summary>
        /// Gets or sets whether minimap is scrolling
        /// </summary>
        public bool IsScrolling
        {
            get { return minimap.IsScrolling; }
            set { minimap.IsScrolling = value; }
        }

        /// <summary>
        /// Sets the minimap width and height in pixels
        /// </summary>
        public Vector2 MinimapDimensions
        {
            set { minimap.MinimapDimensions = value; }
        }

        /// <summary>
        /// Sets the parent map width and height in pixels
        /// </summary>
        public Vector2 MapDimensions
        {
            set { minimap.MapDimensions = value; }
        }

        /// <summary>
        /// Sets the parent map viewport width and height in pixels
        /// </summary>
        public Vector2 MapViewport
        {
            set { minimap.MapViewport = value; }
        }

        #endregion

        #region Initialize

        public MinimapPresenter(IMinimapView view)
        {
            this.view = view;            
            
           SubscribeEvents();
        }

        private void SubscribeEvents()
        {           
 
            view.OnXnaInitialize += () =>
                {
                    minimap = new Minimap(view.GraphicsDevice);
                    minimap.MinimapDimensions = new Vector2(view.MinimapWidth, view.MinimapHeight);                    

                    minimap.OnCameraChanged += (e) =>
                    {
                        if (OnCameraChanged != null)
                            OnCameraChanged(e);
                    };
                };

            view.OnXnaDown += (sender, e) =>
                {
                    minimap.MouseDown(sender, e);
                };

            view.OnXnaUp += (sender, e) =>
                {
                    minimap.MouseUp(sender, e);
                };

            view.OnXnaMove += (sender, e) =>
                {
                    minimap.MouseMove(sender, e);
                };

            view.OnXnaDraw += (spriteBatch) =>
                {
                    view.GraphicsDevice.Clear(Color.DimGray);

                    spriteBatch.Begin();

                    minimap.Draw(spriteBatch);

                    spriteBatch.End();
                };

            view.OnHiding += () =>
                {
                    if (OnHiding != null)
                        OnHiding();
                };

            view.OnShowing += () =>
                {
                    if (OnShowing != null)
                        OnShowing();
                };
            

            
        }

        #endregion

        #region Methods

        public void GenerateMinimap(List<Layer> layers, Tileset tileset)
        {
            minimap.GenerateMinimap(layers, tileset);
        }

        public void ShowWindow(IMainView parent)
        {
            view.ShowWindow(parent);
        }

        public void HideWindow()
        {
            view.HideWindow();
        }

        public void ClearMinimap()
        {
            if (minimap == null)
                return;

            minimap.ClearMinimap();
        }

        #endregion
    }
}
