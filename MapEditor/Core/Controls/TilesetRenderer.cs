using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using MapEditor.Core.Helpers;
using MapEditor.Models;

namespace MapEditor.Core.Controls
{
    public class TilesetRenderer : GraphicsDeviceControl
    {
        private SpriteBatch spriteBatch = null;

        private Vector2 cameraPosition = Vector2.Zero;
        private Vector2? startVector = Vector2.Zero;
        private Vector2? endVector = Vector2.Zero;

        private Camera camera = null;

        //private DrawingTool drawingTool = null;

        public Texture2D Texture { get; set; }

        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        private bool isLeftMouseDown = false;

        public Rectangle SelectionBox
        {
            get
            {
                if (startVector == null || endVector == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(startVector.Value.X, endVector.Value.X),
                    (int)Math.Min(startVector.Value.Y, endVector.Value.Y),
                    (int)Math.Abs(startVector.Value.X - endVector.Value.X),
                    (int)Math.Abs(startVector.Value.Y - endVector.Value.Y));
            }
        }

        public int[,] SelectionBoxValues
        {
            get
            {
                if (Texture == null)
                    return null;

                int width = SelectionBox.Width / TileWidth;
                int height = SelectionBox.Height / TileHeight;
                int x = SelectionBox.Location.X / TileWidth;
                int y = SelectionBox.Location.Y / TileHeight;

                int index = (y * (Texture.Width / TileWidth)) + x;

                if (width == 0 || height == 0)
                    return null;

                int[,] box = new int[height, width];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        box[i, j] = index + j;
                    }

                    index += (Texture.Width / TileWidth);
                }

                return box;
            }
        }

        public int PixelWidth
        {
            get
            {
                if (Texture == null)
                    return 0;

                return Texture.Width;
            }
        }

        public int PixelHeight
        {
            get
            {
                if (Texture == null)
                    return 0;

                return Texture.Height;
            }
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera()
            {
                Position = Vector2.Zero,
                Rotation = 0f, 
                Zoom = 1f,
            };

            Texture2D pixel = null;

            System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                pixel = Texture2D.FromStream(GraphicsDevice, memoryStream);
            }

           // drawingTool = new DrawingTool(spriteBatch, pixel);

            MouseDown += new MouseEventHandler(TilesetRenderer_MouseDown);
            MouseMove += new MouseEventHandler(TilesetRenderer_MouseMove);
            MouseUp += new MouseEventHandler(TilesetRenderer_MouseUp);
            
            Application.Idle += delegate { Invalidate(); };
        }

        void TilesetRenderer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                endVector = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));

                isLeftMouseDown = false;
            }
        }

        void TilesetRenderer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftMouseDown)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    endVector = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));
            }
        }

        void TilesetRenderer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                endVector = null;

                startVector = SnapToGrid(new Vector2(MathHelper.Clamp(InvertCameraMatrix(e.Location).X, 0, PixelWidth), MathHelper.Clamp(InvertCameraMatrix(e.Location).Y, 0, PixelHeight)));

                isLeftMouseDown = true;
            }
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.White);

            if (Texture == null)
                return;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);

            spriteBatch.Draw(Texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

           // drawingTool.DrawRectangle(SelectionBox, Color.Red);

            spriteBatch.End();
        }

        public void LoadTexture(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                Texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }
        }

        private Vector2 SnapToGrid(Vector2 position)
        {
            int x = (int)position.X / TileWidth;
            int y = (int)position.Y / TileHeight;

            return new Vector2(x * TileWidth, y * TileHeight);
        }

        private Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }

    }
}
