using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Core;
using MapEditor.Core.Commands;
using MapEditor.Core.PaintTools;
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


        public Texture2D Pixel { get; set; }       

        public List<Tileset> Tilesets = new List<Tileset>();
        public List<Layer> Layers = new List<Layer>();

        private CommandManager commandManager;

        public int[,] TileBrushValues { get; set; }

        public int LayerIndex {  get; set; }
        public int TilesetIndex { get; set; }

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }


        public Vector2? BeginSelectionBox { get; set; }
        public Vector2? EndSelectionBox { get; set; }        

        private IPaintTool[] paintTools;

        private PaintTool currentPaintTool;



        public Rectangle SelectionBox
        {
            get
            {
                if (BeginSelectionBox == null || EndSelectionBox == null)
                    return Rectangle.Empty;

                return new Rectangle((int)Math.Min(BeginSelectionBox.Value.X, EndSelectionBox.Value.X),
                   (int)Math.Min(BeginSelectionBox.Value.Y, EndSelectionBox.Value.Y),
                   (int)Math.Abs(BeginSelectionBox.Value.X - EndSelectionBox.Value.X),
                   (int)Math.Abs(BeginSelectionBox.Value.Y - EndSelectionBox.Value.Y));
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

                Pixel = Texture2D.FromStream(view.GetGraphicsDevice, memoryStream);
            }

            commandManager = new CommandManager();

            paintTools = new IPaintTool[] { new DrawPaintTool(this), new ErasePaintTool(this), new SelectPaintTool(this) };
        }
        
        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseMove(sender, e);
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseUp(sender, e);
        }
       
        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    SetPaintTool(PaintTool.Select);
                    break;
                    
            }

            paintTools[(int)currentPaintTool].OnMouseDown(sender, e);
        }

        void view_OnInitialize()
        {
            // TODO: FIX
        }

        void view_OnDraw(SpriteBatch spriteBatch)
        {
            view.GetGraphicsDevice.Clear(Color.DimGray);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.CameraTransformation);


            // Draw only viewport
            int left = (int)Math.Floor(camera.Position.X / TileWidth);
            int right = TileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / TileWidth;
            right = Math.Min(right, MapWidth);

            int top = (int)Math.Floor(camera.Position.Y / TileHeight);
            int bottom = TileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / TileHeight;
            bottom = Math.Min(bottom, MapHeight);

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

            if (paintTools[(int)currentPaintTool] != null)
                paintTools[(int)currentPaintTool].Draw(spriteBatch);

            spriteBatch.End();
        }

        public void SetPaintTool(PaintTool paintTool)
        {
            this.currentPaintTool = paintTool;
        }

        public void InitializeMap(string texturePath, int tileWidth, int tileHeight, int mapWidth, int mapHeight, CheckedListBox checkedListBox)
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

            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;

            checkedListBox.Items.Add("Layer" + checkedListBox.Items.Count, true);
            //checkedListBox.SelectedIndex++;
        }

        public void RemoveTileset()
        {

        }

        public void AddTileset(string texturePath, int tileWidth, int tileHeight)
        {
            
        }

       

        public void AddLayer(CheckedListBox checkedListBox)
        {
            commandManager.ExecuteLayerAddCommand(Layers, MapWidth, MapHeight, checkedListBox);
        }

        public void RemoveLayer(CheckedListBox checkedListBox)
        {
            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex, checkedListBox);
        }

        public void CloneLayer()
        {
            commandManager.ExecuteLayerClone(Layers, LayerIndex);
        }

        public void RaiseLayer(CheckedListBox checkedListBox)
        {
            if (LayerIndex <= 0)
                return;

            commandManager.ExecuteLayerRaise(Layers, LayerIndex, checkedListBox);
        }

        public void LowerLayer(CheckedListBox checkedListBox)
        {
            if (LayerIndex >= Layers.Count - 1)
                return;

            commandManager.ExecuteLayerLower(Layers, LayerIndex, checkedListBox);
        }

        public void SetLayerVisibility(bool isVisible)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerVisibility(Layers[LayerIndex], isVisible);
        }

        public void DrawTileBrushes(TileBrushCollection tileBrushes)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteEditDrawCommand(Layers[LayerIndex], tileBrushes.TileBrushes); 
        } 
        
        public void RemoveTiles(TileBrushCollection tileBrushes)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteEditRemoveCommand(Layers[LayerIndex], tileBrushes.TileBrushes);
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

        public Vector2 SnapToGrid(Vector2 position)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            int x = (int)position.X / tileWidth;
            int y = (int)position.Y / tileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        public Vector2 CoordinateToPixels(int x, int y)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            return new Vector2(x * tileWidth, y * tileHeight);
        }

        public Vector2 PixelsToCoordinate(Vector2 position)
        {
            int tileWidth = Tilesets.FirstOrDefault().TileWidth;
            int tileHeight = Tilesets.FirstOrDefault().TileHeight;

            return new Vector2((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        public Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }
    }
}
