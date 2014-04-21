using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.EventsArgs;
using MapEditor.UI;


namespace MapEditor.Models
{
    /// <summary>
    /// Represents a minimap created from the collection of layers from a map
    /// </summary>
    public class Minimap
    {
        #region Fields

        private List<Texture2D> textures = new List<Texture2D>();

        private Texture2D pixel;

        private Vector2 drawScale;

        private GraphicsDevice graphicsDevice;

        private bool isMouseLeftPressed;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when minimap camera is changed
        /// </summary>
        public event CameraEventHandler OnCameraChanged;

        /// <summary>
        /// Gets or sets camera
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Gets and sets whether minimap is scrolling
        /// </summary>
        public bool IsScrolling { get; set; }

        /// <summary>
        /// Gets the rectangle scale between minimap and parent map
        /// </summary>
        public Vector2 RectangleScale
        {
            get
            {
                if (MapDimensions == Vector2.Zero)
                    return Vector2.Zero;

                return new Vector2(MinimapDimensions.X, MinimapDimensions.Y) / MapDimensions;
            }
        }

        /// <summary>
        /// Minimap width and height in pixels
        /// </summary>
        public Vector2 MinimapDimensions { get; set; }

        /// <summary>
        /// Parent map width and height in pixels
        /// </summary>
        public Vector2 MapDimensions { get; set; }

        /// <summary>
        /// Parent map viewport width and height in pixels
        /// </summary>
        public Vector2 MapViewport { get; set; }

        #endregion

        #region Initialize

        public Minimap(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
                throw new ArgumentNullException();

            this.graphicsDevice = graphicsDevice;

            Initialize();           
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            // Load pixel
            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                pixel = Texture2D.FromStream(graphicsDevice, memoryStream);
            }

            Camera = new Camera()
            {
                Zoom = 1f,
            };
        }

        /// <summary>
        /// Draw minimap
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="pixel"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw minimap
            textures.ForEach(texture =>
            {
                spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, drawScale, SpriteEffects.None, 1f);
            });

            if (Camera == null)
                return;

            if (Camera.Zoom <= 0)
                return;

            // Draw view rectangle
            if (!IsScrolling)
                DrawingTool.DrawRectangle(spriteBatch, pixel,
                    new Rectangle((int)MathHelper.Clamp(Camera.Position.X * RectangleScale.X, 0, MinimapDimensions.X),
                        (int)MathHelper.Clamp(Camera.Position.Y * RectangleScale.Y, 0, MinimapDimensions.Y),
                        (int)(RectangleScale.X * MapViewport.X / Camera.Zoom),
                        (int)(RectangleScale.Y * MapViewport.Y / Camera.Zoom)),
                        Color.White,
                        1);
            else
                DrawingTool.DrawRectangle(spriteBatch, pixel,
                    new Rectangle((int)MathHelper.Clamp(Camera.Position.X, 0, MinimapDimensions.X),
                        (int)MathHelper.Clamp(Camera.Position.Y, 0, MinimapDimensions.Y),
                        (int)(RectangleScale.X * MapViewport.X / Camera.Zoom),
                        (int)(RectangleScale.Y * MapViewport.Y / Camera.Zoom)),
                        Color.White,
                        1);

        }

        /// <summary>
        /// Mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                isMouseLeftPressed = true;
        }

        /// <summary>
        /// Mouse Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;
            }
        }

        /// <summary>
        /// Mouse move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                Camera.Position = new Vector2(MathHelper.Clamp(e.Location.X, 0, MinimapDimensions.X),
                    MathHelper.Clamp(e.Location.Y, 0, MinimapDimensions.Y));

                IsScrolling = true;

                if (OnCameraChanged != null)
                    OnCameraChanged(new CameraEventArgs(new Camera() { Position = Camera.Position / RectangleScale }));
            }
        }

        /// <summary>
        /// Create minimap
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="layers"></param>
        /// <param name="tileset"></param>
        public void GenerateMinimap(List<Layer> layers, Tileset tileset)
        {
            if (layers.Count <= 0)
                return;

            textures.Clear();

            if (tileset == null)
                throw new ArgumentNullException();

            layers.ForEach(layer =>
                {
                    Texture2D texture;

                    Color[] colors = new Color[layer.LayerWidth * layer.LayerHeight];

                    int x = 0;
                    int y = -1;

                    for (int i = 0; i < layer.LayerWidth * layer.LayerHeight; i++)
                    {
                        if (i % layer.LayerWidth == 0)
                            y++;

                        x = i % layer.LayerWidth;

                        colors[i] = tileset.GetCenterPixelColor(layer.Rows[y].Columns[x].TileID);
                    }

                    texture = new Texture2D(graphicsDevice, layer.LayerWidth, layer.LayerHeight);
                    texture.SetData<Color>(colors);

                    textures.Add(texture);
                });

            MapDimensions = new Vector2(layers.First().LayerWidth * tileset.TileWidth, layers.First().LayerHeight * tileset.TileHeight);

            drawScale = new Vector2(MinimapDimensions.X, MinimapDimensions.Y) / new Vector2(textures.First().Width, textures.First().Height);
        }

      

        #endregion
    }
}
