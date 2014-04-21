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
    /// Representers the logic for tileset interaction
    /// </summary>
    public class TilesetPresenter : ITilesetPresenter
    {
        #region Fields

        private readonly ITilesetView view;

        private Camera camera;

        private Texture2D pixel;

        private Tileset tileset;

        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;
        private Vector2 cameraPosition;
        private Vector2? selectionBoxA;
        private Vector2? selectionBoxB;

        private bool isMouseLeftPressed;
        private bool isMouseMiddlePressed;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when a new tile brush is selected
        /// </summary>
        public event TilesetEventHandler OnTileBrushChanged;

        /// <summary>
        /// Gets the selection box created on top of the tileset image
        /// </summary>
        public Rectangle SelectionBox
        {
            get
            {
                if (selectionBoxA == null || selectionBoxB == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(selectionBoxA.Value.X, selectionBoxB.Value.X),
                   (int)Math.Min(selectionBoxA.Value.Y, selectionBoxB.Value.Y),
                   (int)Math.Abs(selectionBoxA.Value.X - selectionBoxB.Value.X),
                   (int)Math.Abs(selectionBoxA.Value.Y - selectionBoxB.Value.Y));
            }
        }

        

        #endregion

        #region Initialize

        public TilesetPresenter(ITilesetView view)
        {
            this.view = view;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            view.OnXnaInitialize += () =>
                {
                    camera = new Camera()
                    {
                        Zoom = 1f,
                    };

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        pixel = Texture2D.FromStream(view.GraphicsDevice, memoryStream);
                    }    
                };

            view.OnXnaDraw += (spriteBatch) =>
                {
                    view.GraphicsDevice.Clear(Color.White);

                    if (tileset == null || pixel == null)
                        return;

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);

                    spriteBatch.Draw(tileset.Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

                    DrawingTool.DrawRectangle(spriteBatch, pixel, SelectionBox, Color.Red, 2);

                    spriteBatch.End();

                };

            view.OnXnaDown += (sender, e) =>
                {
                    if (tileset == null)
                        return;

                    if (e.Button == MouseButtons.Left)
                    {
                        ClearSelectionBox();

                        selectionBoxA = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCamera(e.Location).X, 0, tileset.Texture.Width),
                            MathHelper.Clamp(InvertCamera(e.Location).Y, 0, tileset.Texture.Height)));

                        isMouseLeftPressed = true;
                    }
                    else if (e.Button == MouseButtons.Middle)
                    {
                        isMouseMiddlePressed = true;

                        previousMousePosition = InvertCamera(e.Location);
                    }
                    
                };

            view.OnXnaUp += (sender, e) =>
                {
                    if (tileset == null)
                        return;

                    if (isMouseLeftPressed)
                    {
                        selectionBoxB = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCamera(e.Location).X, 0, tileset.Texture.Width),
                            MathHelper.Clamp(InvertCamera(e.Location).Y, 0, tileset.Texture.Height)));

                        isMouseLeftPressed = false;

                        if (OnTileBrushChanged != null)
                            OnTileBrushChanged(new TilesetEventArgs(GetTileBrush()));
                    }
                    else if (isMouseMiddlePressed)
                        isMouseMiddlePressed = false;
                };

            view.OnXnaMove += (sender, e) =>
                {
                    if (isMouseLeftPressed)
                    {
                        selectionBoxB = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCamera(e.Location).X, 0, tileset.Texture.Width),
                            MathHelper.Clamp(InvertCamera(e.Location).Y, 0, tileset.Texture.Height)));
                    }
                    else if (isMouseMiddlePressed)
                    {
                        currentMousePosition = InvertCamera(e.Location);

                        Vector2 difference = currentMousePosition - previousMousePosition;

                        cameraPosition += -difference;

                        if (cameraPosition.X < 0)
                            cameraPosition.X = 0;
                        if (cameraPosition.Y < 0)
                            cameraPosition.Y = 0;
                        if (cameraPosition.X >= tileset.Texture.Width)
                            cameraPosition.X = tileset.Texture.Width;
                        if (cameraPosition.Y >= tileset.Texture.Height)
                            cameraPosition.Y = tileset.Texture.Height;

                        ScrollCamera(cameraPosition);
                    }
                }; 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set tileset image and dimensions
        /// </summary>
        /// <param name="texturePath"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void SetTileset(Tileset tileset)
        {
            this.tileset = tileset;
        }

        public void SetTileSize(int tileWidth, int tileHeight)
        {
            tileset.TileWidth = tileWidth;
            tileset.TileHeight = tileHeight;
        }

        /// <summary>
        /// Show window
        /// </summary>
        public void ShowWindow(IMainView parent)
        {
            view.ShowWindow(parent);
        }

        /// <summary>
        /// Hide window
        /// </summary>
        public void HideWindow()
        {
            view.HideWindow();
        }

        /// <summary>
        /// Scroll camera
        /// </summary>
        /// <param name="position"></param>
        private void ScrollCamera(Vector2 position)
        {
            camera.Position = new Vector2(MathHelper.Clamp(position.X, 0, tileset.Texture.Width),
                MathHelper.Clamp(position.Y, 0, tileset.Texture.Height));
        }

        /// <summary>
        /// Reset selection box positions
        /// </summary>
        private void ClearSelectionBox()
        {
            selectionBoxA = null;
            selectionBoxB = null;
        }

        /// <summary>
        /// Gets the tile ids of the selected tiles in the tileset selection box
        /// </summary>
        /// <returns></returns>
        private int[,] GetTileBrush()
        {
            if (tileset == null)
                return null;

            if (tileset.TileWidth == 0 || tileset.TileHeight == 0)
                return null;

            int width = SelectionBox.Width / tileset.TileWidth;
            int height = SelectionBox.Height / tileset.TileHeight;
            int x = SelectionBox.Location.X / tileset.TileWidth;
            int y = SelectionBox.Location.Y / tileset.TileHeight;

            int index = (y * (tileset.Texture.Width / tileset.TileWidth)) + x;

            if (width == 0 || height == 0)
                return null;

            int[,] box = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    box[i, j] = index + j;
                }

                index += (tileset.Texture.Width / tileset.TileWidth);
            }

            return box;
        }

        /// <summary>
        /// Rounds off position to fit to tiled map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 SnapToGrid(Vector2 position)
        {
            int x = (int)position.X / tileset.TileWidth;
            int y = (int)position.Y / tileset.TileHeight;

            return new Vector2(x * tileset.TileWidth, y * tileset.TileHeight);
        }

        /// <summary>
        /// Inverts camera
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Vector2 InvertCamera(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }



        #endregion
    }
}
