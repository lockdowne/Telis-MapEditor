using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core.Commands;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public class MainRenderPresenter : IMainRenderPresenter
    {
        private readonly IXnaRenderView view;

        private Camera camera;

        private Vector2 cameraPosition;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;
        private Vector2? beginSelectionBox;
        private Vector2? endSelectionBox;

        private bool isMouseLeftPressed;
        private bool isMouseRightPressed;

        private Texture2D pixel;

        private TileBrushCollection tileBrushes;
        

        public List<Tileset> Tilesets = new List<Tileset>();
        public List<Layer> Layers = new List<Layer>();

        private CommandManager commandManager;

        public int[,] TileBrushValues { get; set; }

        public int LayerIndex { get; set; }
        public int TilesetIndex { get; set; }

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

        public MainRenderPresenter(IXnaRenderView view)
        {
            this.view = view;

            view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            view.OnInitialize += new Action(view_OnInitialize);
            view.OnXnaDown += new MouseEventHandler(view_OnXnaDown);
            view.OnXnaUp += new MouseEventHandler(view_OnXnaUp);
            view.OnXnaMove += new MouseEventHandler(view_OnXnaMove);

            // Move to initialize method
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

            tileBrushes = new TileBrushCollection();

            commandManager = new CommandManager();


            
        }
        
        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                previousMousePosition = currentMousePosition;
                currentMousePosition = PixelsToCoordinate(InvertCameraMatrix(e.Location));

                if (currentMousePosition != previousMousePosition)
                {
                    if (TileBrushValues != null)
                    {
                        tileBrushes.AddTileBrush(new TileBrush()
                        {
                            Brush = TileBrushValues,
                            Position = currentMousePosition,
                        });
                    }
                }
            }
            else
            {
                currentMousePosition = PixelsToCoordinate(InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();
                if (TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = TileBrushValues,
                        Position = currentMousePosition,
                    });
                }
            }

        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                commandManager.ExecuteEditDrawCommand(Layers[LayerIndex], tileBrushes.TileBrushes);

                tileBrushes.ClearBrushes();
            }
        }
       
        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                commandManager.Undo();
            }
            if (e.Button == MouseButtons.Left)
            {
                isMouseLeftPressed = true;

                // TODO: Be sure tilebrush brush data is never null

                currentMousePosition = PixelsToCoordinate(InvertCameraMatrix(e.Location));

                tileBrushes.ClearBrushes();

                if (TileBrushValues != null)
                {
                    tileBrushes.AddTileBrush(new TileBrush()
                    {
                        Brush = TileBrushValues,
                        Position = currentMousePosition,
                    });
                }
            }
        }

        void view_OnInitialize()
        {
            // TODO: FIX
        }

        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GetGraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);



            int tileWidth = (Tilesets.FirstOrDefault() == null) ? 0 : Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = (Tilesets.FirstOrDefault() == null) ? 0 : Tilesets.FirstOrDefault().TileHeight;
            int mapWidth = (Layers.FirstOrDefault() == null) ? 0 : Layers.FirstOrDefault().MapWidth;
            int mapHeight = (Layers.FirstOrDefault() == null) ? 0 : Layers.FirstOrDefault().MapHeight;

            // Draw only viewport
            int left = (int)Math.Floor(camera.Position.X / tileWidth);
            int right = tileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / tileWidth;
            right = Math.Min(right, mapWidth);

            int top = (int)Math.Floor(camera.Position.Y / tileHeight);
            int bottom = tileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / tileHeight;
            bottom = Math.Min(bottom, mapHeight);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Tilesets.ForEach(tileset =>
                        {
                            Layers.ForEach(layer =>
                                {
                                    if (layer.IsVisible)
                                    {
                                        if (tileset.Texture != null)
                                        {
                                            int tileID = layer.Rows[y].Columns[x].TileID;

                                            if (tileID > -1)
                                            {
                                                spriteBatch.Draw(tileset.Texture,
                                                    new Rectangle((x * tileset.TileWidth),
                                                        (y * tileset.TileHeight),
                                                        tileset.TileWidth,
                                                        tileset.TileHeight),
                                                        tileset.GetSourceRectangle(tileID),
                                                        Color.White);
                                            }
                                        }
                                    }
                                });
                        });

                    /* Draw grid
                    DrawingTool.DrawRectangle(spriteBatch, pixel, new Rectangle((x * 32),
                                                        (y * 32),
                                                        32,
                                                        32), Color.White, 1);*/
                    
                }
            }

            if (tileBrushes != null)
                tileBrushes.Draw(spriteBatch, Tilesets[TilesetIndex]);

            spriteBatch.End();
        }

        public void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            Texture2D texture;

            using (FileStream fileStream = new FileStream(texturePath, FileMode.Open))
            {
                texture = Texture2D.FromStream(view.GetGraphicsDevice, fileStream);
            }

            // TODO: Ensure only one instance of texture is instantiated
            Tilesets.Add(new Tileset()
                {
                    Texture = texture,
                    TexturePath = texturePath,
                    TileWidth = tileWidth,
                    TileHeight = tileHeight,
                });

            Layers.Add(new Layer(mapWidth, mapHeight));
        }

        public void RemoveTileset()
        {

        }

        public void AddTileset(string texturePath, int tileWidth, int tileHeight)
        {
            
        }


        public void AddLayer()
        {
            int mapWidth = Layers.FirstOrDefault().MapWidth;
            int mapHeight = Layers.FirstOrDefault().MapHeight;

            commandManager.ExecuteLayerAddCommand(Layers, mapWidth, mapHeight);
        }

        public void AddLayer(int width, int height)
        {
            commandManager.ExecuteLayerAddCommand(Layers, width, height);
        }

        public void RemoveLayer()
        {
            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex);
        }

        public void CloneLayer()
        {
            commandManager.ExecuteLayerClone(Layers, LayerIndex);
        }

        public void SetLayerVisibility()
        {
            commandManager.ExecuteLayerVisibility(Layers[LayerIndex]);
        }

        public void OffsetMap(int offsetX, int offsetY)
        {
            commandManager.ExecuteMapOffset(Layers, offsetX, offsetY);
        }

        public void ResizeMap(int mapWidth, int mapHeight)
        {
            commandManager.ExecuteMapResize(Layers, mapWidth, mapHeight);
        }

        public void Undo()
        {
            commandManager.Undo();
        }

        public void Redo()
        {
            commandManager.Redo();
        }

        public void InitializeMap()
        {

        }

        private Vector2 SnapToGrid(Vector2 position)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            int x = (int)position.X / tileWidth;
            int y = (int)position.Y / tileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        private Vector2 CoordinateToPixels(int x, int y)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        private Vector2 PixelsToCoordinate(Vector2 position)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            return new Vector2((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        private Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }
    }
}
