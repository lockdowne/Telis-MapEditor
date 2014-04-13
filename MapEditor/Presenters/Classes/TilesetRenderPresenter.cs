﻿using System;
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
    /// <summary>
    /// Holds logic for tileset model and UI
    /// </summary>
    public class TilesetRenderPresenter : ITilesetRenderPresenter
    {
        #region Fields

        public event Action<int[,]> SendTileBrushValues;

        private readonly IXnaRenderView view;        

        private Camera camera;

        private Vector2 cameraPosition;
        private Vector2? beginSelectionBox;
        private Vector2? endSelectionBox;

        private bool isMouseLeftPressed;

        private Texture2D pixel;
        private Texture2D tileset;

        private int tileWidth;
        private int tileHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selection box created on top of the tileset image
        /// </summary>
        public Rectangle SelectionBox
        {
            get
            {
                if (beginSelectionBox == null || endSelectionBox == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(beginSelectionBox.Value.X, endSelectionBox.Value.X),
                   (int)Math.Min(beginSelectionBox.Value.Y, endSelectionBox.Value.Y),
                   (int)Math.Abs(beginSelectionBox.Value.X - endSelectionBox.Value.X),
                   (int)Math.Abs(beginSelectionBox.Value.Y - endSelectionBox.Value.Y));
            }
        }

        /// <summary>
        /// Gets the width of the tileset image in pixels
        /// </summary>
        public int PixelWidth
        {
            get
            {
                if (tileset == null)
                    return 0;

                return tileset.Width;
            }
            
        }

        /// <summary>
        /// Gets the height of the tileset image in pixels
        /// </summary>
        public int PixelHeight
        {
            get
            {
                if (tileset == null)
                    return 0;

                return tileset.Height;
            }
        }

        #endregion

        #region Initialize

        public TilesetRenderPresenter(IXnaRenderView view)
        {
            this.view = view; 

            // Init is not working like its suppose to
            view.OnInitialize += new Action(view_OnInitialize);
            view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            view.OnXnaDown += new MouseEventHandler(view_OnXnaDown);
            view.OnXnaUp += new MouseEventHandler(view_OnXnaUp);
            view.OnXnaMove += new MouseEventHandler(view_OnXnaMove);
            
            camera = new Camera()
            {
                Position = Vector2.Zero,
                Rotation = 0f,
                Zoom = 1f,
            };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                pixel = Texture2D.FromStream(view.GetGraphicsDevice, memoryStream);  
            }       
                      
        }

        #endregion

        #region Events

        void view_OnInitialize()
        {
            
        }
        
        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GetGraphicsDevice.Clear(Color.White);

            if (tileset == null || pixel == null)
                return;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);

            spriteBatch.Draw(tileset, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            DrawingTool.DrawRectangle(spriteBatch, pixel, SelectionBox, Color.Red, 2);

            spriteBatch.End();

        }

        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                endSelectionBox = null;

                beginSelectionBox = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));

                isMouseLeftPressed = true;
            }
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                endSelectionBox = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));

                isMouseLeftPressed = false;

                if (SendTileBrushValues != null)
                    SendTileBrushValues(GetTileBrush());
                //TEST.BOX = GetTileBrush();
            }
        }

        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                if (e.Button == MouseButtons.Left)
                {
                    endSelectionBox = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the tile ids of the selected tiles in the tileset selection box
        /// </summary>
        /// <returns></returns>
        public int[,] GetTileBrush()
        {
            if (tileset == null)
                return null;

            if (tileWidth == 0 || tileHeight == 0)
                return null;
                //throw new 

            int width = SelectionBox.Width / tileWidth;
            int height = SelectionBox.Height / tileHeight;
            int x = SelectionBox.Location.X / tileWidth;
            int y = SelectionBox.Location.Y / tileHeight;

            int index = (y * (tileset.Width / tileWidth)) + x;

            if (width == 0 || height == 0)
                return null;

            int[,] box = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    box[i, j] = index + j;
                }

                index += (tileset.Width / tileWidth);
            }

            return box;
        }

        /// <summary>
        /// Load texture from path
        /// </summary>
        /// <param name="path"></param>
        public void LoadTexture(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                tileset = Texture2D.FromStream(view.GetGraphicsDevice, fileStream);
            }
        }

        /// <summary>
        /// Set the tile width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetTileDimesions(int width, int height)
        {
            tileWidth = width;
            tileHeight = height;
        }

        /// <summary>
        /// Clear selection box vectors
        /// </summary>
        public void ClearSelectionBox()
        {
            beginSelectionBox = null;
            endSelectionBox = null;
        }

        /// <summary>
        /// Rounds off position to fit with the map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 SnapToGrid(Vector2 position)
        {
            int x = (int)position.X / tileWidth;
            int y = (int)position.Y / tileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        /// <summary>
        /// Transforms position to adjust itself to scrollable maps
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }

        #endregion
    }
}