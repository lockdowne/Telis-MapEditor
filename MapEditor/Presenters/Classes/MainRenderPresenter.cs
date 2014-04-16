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
using MapEditor.Core.Controls;
using MapEditor.Core.PaintTools;
using MapEditor.UI;
using MapEditor.Models;

namespace MapEditor.Presenters
{
    public class MainRenderPresenter : IMainRenderPresenter
    {
        #region Fields

        private readonly IXnaRenderView view;

        private Camera camera;

        private Vector2 cameraPosition;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;

        public Texture2D Pixel { get; set; }     

        private CommandManager commandManager;

        private int layerCounter;

        private bool isMouseLeftPressed;
        private bool isMouseMiddlePressed;
     
        private IPaintTool[] paintTools;

        private PaintTool currentPaintTool;
        #endregion

        #region Properties

        public event Action MapChanged;

        public int[,] TileBrushValues { get; set; }

        public List<int[,]> Clipboard;

        public int LayerIndex { get; set; }

        public int TilesetIndex { get; set; }
        
        public List<Tileset> Tilesets { get; set; }
        public List<Layer> Layers { get; set; }

        public Vector2? BeginSelectionBox { get; set; }
        public Vector2? EndSelectionBox { get; set; }        

  

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

        public int MapWidth
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;

                return Layers.First().MapWidth;
            }
        }

        public int MapHeight
        {
            get
            {
                if (Layers.Count <= 0)
                    return 0;

                return Layers.First().MapHeight;
            }
        }

        public int TileWidth
        {
            get
            {
                if (Tilesets.Count <= 0)
                    return 0;

                return Tilesets.First().TileWidth;
            }
        }

        public int TileHeight
        {
            get
            {
                if (Tilesets.Count <= 0)
                    return 0;

                return Tilesets.First().TileHeight;
            }
        }

        #endregion

        #region Initialize

        public MainRenderPresenter(IXnaRenderView view)
        {
            this.view = view;

            view.OnDraw += new Action<SpriteBatch>(view_OnDraw);
            view.OnInitialize += new Action(view_OnInitialize);
            view.OnXnaDown += new MouseEventHandler(view_OnXnaDown);
            view.OnXnaUp += new MouseEventHandler(view_OnXnaUp);
            view.OnXnaMove += new MouseEventHandler(view_OnXnaMove);
            view.OnXnaWheel += new MouseEventHandler(view_OnXnaWheel);

            Tilesets = new List<Tileset>();
            Layers = new List<Layer>();

            // Move to initialize method
            camera = new Camera()
            {
                Position = Vector2.Zero,
                Zoom = 1f,
            };

            Clipboard = new List<int[,]>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                System.Drawing.Bitmap bitmap = MapEditor.Properties.Resources.Pixel;

                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                memoryStream.Seek(0, SeekOrigin.Begin);

                Pixel = Texture2D.FromStream(view.GetGraphicsDevice, memoryStream);
            }

            commandManager = new CommandManager();

            paintTools = new IPaintTool[] { new DrawPaintTool(this), new ErasePaintTool(this), new SelectPaintTool(this) };

            cameraPosition = Vector2.Zero;
        }

      
       

        #endregion
        
        #region Events

        void view_OnXnaWheel(object sender, MouseEventArgs e)
        {
            // Position => wheel up => zoom in
            // Negative => wheel down => zoom out
            float zoom = camera.Zoom;

            if (e.Delta > 0)
                zoom += 0.1f;
            else if (e.Delta < 0)
                zoom -= 0.1f;

            camera.Zoom = MathHelper.Clamp(zoom, 0.5f, 2f);
        }

        void view_OnXnaMove(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseMove(sender, e);

            Console.WriteLine(e.Location.ToString());

            if (isMouseMiddlePressed)
            {
                currentMousePosition = InvertCameraMatrix(e.Location);

                Vector2 difference = currentMousePosition - previousMousePosition;
                
                cameraPosition += -difference;

                if (cameraPosition.X < 0)
                    cameraPosition.X = 0;
                if (cameraPosition.Y < 0)
                    cameraPosition.Y = 0;
                if (cameraPosition.X >= MapWidth * TileWidth)
                    cameraPosition.X = MapWidth * TileWidth;
                if (cameraPosition.Y >= MapHeight * TileHeight)
                    cameraPosition.Y = MapHeight * TileHeight;

                //cameraPosition.Normalize();

                //Console.WriteLine(cameraPosition.ToString());

                ScrollCamera(cameraPosition);
            }
        }

        void view_OnXnaUp(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseUp(sender, e);

            if (isMouseLeftPressed)
            {
                isMouseLeftPressed = false;

                if (MapChanged != null)
                    MapChanged();
            }

            if (isMouseMiddlePressed)
            {
                isMouseMiddlePressed = false;
            }
        }
       
        void view_OnXnaDown(object sender, MouseEventArgs e)
        {
            paintTools[(int)currentPaintTool].OnMouseDown(sender, e);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    isMouseLeftPressed = true;
                    break;
                case MouseButtons.Right:
                    if(currentPaintTool != PaintTool.Erase)
                        SetPaintTool(PaintTool.Select);
                    break;             
                case MouseButtons.Middle:
                    isMouseMiddlePressed = true;
                    previousMousePosition = InvertCameraMatrix(e.Location);
                    break;
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

            if (TileWidth <= 0 || TileHeight <= 0)
                return;

            // Draw only viewport
            /*
            int left = (int)Math.Floor(camera.Position.X / TileWidth);
            int right = TileWidth + left + spriteBatch.GraphicsDevice.Viewport.Width / TileWidth;
            right = Math.Min(right, MapWidth);

            int top = (int)Math.Floor(camera.Position.Y / TileHeight);
            int bottom = TileHeight + top + spriteBatch.GraphicsDevice.Viewport.Height / TileHeight;
            bottom = Math.Min(bottom, MapHeight);*/

            int top = 0;
            int left = 0;

            int bottom = MapHeight;
            int right = MapWidth;

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

            if (SelectionBox != Rectangle.Empty)
                DrawingTool.DrawRectangle(spriteBatch, Pixel, SelectionBox, Color.White, 2);

            // This should only be called reactively 
            /*if (MapChanged != null)
                MapChanged();*/

            spriteBatch.End();
        }

        #endregion

        #region Methods 
     

        private void ScrollCamera(Vector2 position)
        {
            /*camera.Position = new Vector2(MathHelper.Clamp((int)(camera.Position.X + position.X), 0, tileWidth * mapWidth - view.GetGraphicsDevice.Viewport.Width),
              (MathHelper.Clamp((int)(camera.Position.Y + position.Y), 0, tileHeight * mapHeight - view.GetGraphicsDevice.Viewport.Height)));
             * */

            /*camera.Position = new Vector2(MathHelper.Clamp((int)(position.X), 0, (MapWidth * TileWidth) - view.GetGraphicsDevice.Viewport.Width),
              (MathHelper.Clamp((int)(position.Y), 0, (MapHeight * TileHeight) - view.GetGraphicsDevice.Viewport.Height)));*/

            camera.Position = new Vector2(MathHelper.Clamp((int)(position.X), 0, (MapWidth * TileWidth)),
             (MathHelper.Clamp((int)(position.Y), 0, (MapHeight * TileHeight))));

        }


        /// <summary>
        /// Sets current paint tool
        /// </summary>
        /// <param name="paintTool"></param>
        public void SetPaintTool(PaintTool paintTool)
        {
            if (paintTool == PaintTool.Erase)
                RemoveTiles();

            if (paintTool == PaintTool.Draw)
                ClearSelectionBox();

            this.currentPaintTool = paintTool;
        }

        /// <summary>
        /// Sets up map with data to initialize
        /// </summary>
        /// <param name="texturePath"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="checkedListBox"></param>
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

            layerCounter = 1;

            Layers.Add(new Layer("Layer " + layerCounter.ToString(), mapWidth, mapHeight));
        }

        public void RemoveTileset()
        {

        }

        /// <summary>
        /// Sets the selection box to empty
        /// </summary>
        private void ClearSelectionBox()
        {
            BeginSelectionBox = null;
            EndSelectionBox = null;
        }

        /// <summary>
        /// Copy selected tiles
        /// </summary>
        public void CopySelection()
        {
            if (currentPaintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    commandManager.ExecuteEditCopyCommand(Layers[LayerIndex], SelectionBox, TileWidth, TileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();
                }
            }
        }


        /// <summary>
        /// Cut selected tiles
        /// </summary>
        public void CutSelection()
        {
            if (currentPaintTool == PaintTool.Select)
            {
                if (!SelectionBox.IsEmpty)
                {
                    commandManager.ExecuteEditCutCommand(Layers[LayerIndex], SelectionBox, TileWidth, TileHeight, Clipboard);

                    TileBrushValues = Clipboard.FirstOrDefault();

                    Clipboard.Clear();

                    SetPaintTool(PaintTool.Draw);

                    ClearSelectionBox();                                       
                }
            }
        }

        /// <summary>
        /// Add layer to map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void AddLayer(int mapWidth, int mapHeight)
        {
            layerCounter++;

            commandManager.ExecuteLayerAddCommand(Layers, "Layer " + layerCounter.ToString(), mapWidth, mapHeight);

            LayerIndex = -1;

        }

        /// <summary>
        /// Remove layer from map
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RemoveLayer()
        {
            if (LayerIndex < 0)
                return;

            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerRemoveCommand(Layers, LayerIndex);

            LayerIndex = -1;
        }

        /// <summary>
        /// Duplicate layer 
        /// </summary>
        public void CloneLayer()
        {
            if (LayerIndex < 0)
                return;

            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerClone(Layers, LayerIndex);

            LayerIndex = -1;
        }

        /// <summary>
        /// Move layer one index up
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void RaiseLayer()
        {
            if (LayerIndex <= 0)
                return;

            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerRaise(Layers, LayerIndex);

            LayerIndex = -1;
        }

        /// <summary>
        /// Move layer one index down
        /// </summary>
        /// <param name="checkedListBox"></param>
        public void LowerLayer()
        {
            if (LayerIndex < 0)
                return;

            if (LayerIndex >= Layers.Count - 1)
                return;

            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerLower(Layers, LayerIndex);

            LayerIndex = -1;
        }

        /// <summary>
        /// Set layers visibility
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetLayerVisibility(bool isVisible)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteLayerVisibility(Layers[LayerIndex], isVisible);
        }

        /// <summary>
        /// Draw temporary tilebrushes into map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void DrawTileBrushes(TileBrushCollection tileBrushes)
        {
            if (Layers.Count <= 0)
                return;

            if (LayerIndex < 0)
                return;

            commandManager.ExecuteEditDrawCommand(Layers[LayerIndex], tileBrushes.TileBrushes); 
        } 
        
        /// <summary>
        /// Remove selected tiles from map
        /// </summary>
        /// <param name="tileBrushes"></param>
        public void RemoveTiles()
        {
            if (Layers.Count <= 0)
                return;

            if (LayerIndex < 0)
                return;

            commandManager.ExecuteEditRemoveCommand(Layers[LayerIndex], SelectionBox, TileWidth, TileHeight);
        }

        /// <summary>
        /// Offset map in tile measurement
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void OffsetMap(int offsetX, int offsetY)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteMapOffset(Layers, offsetX, offsetY);
        }

        /// <summary>
        /// Resize map in tile measurement
        /// </summary>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public void ResizeMap(int mapWidth, int mapHeight, NumericUpDownEx mapWidthNumeric, NumericUpDownEx mapHeightNumeric)
        {
            if (Layers.Count <= 0)
                return;

            commandManager.ExecuteMapResize(Layers, mapWidth, mapHeight, mapWidthNumeric, mapHeightNumeric);
        }

        /// <summary>
        /// Resize tiles in map and tilesets
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void ResizeTiles(int tileWidth, int tileHeight, NumericUpDownEx tileWidthNumeric, NumericUpDownEx tileHeightNumeric, List<Action<int, int>> setTileDimensions)
        {
            if (Tilesets.Count <= 0)
                return;

            commandManager.ExecuteTileResize(Tilesets, tileWidth, tileHeight, tileWidthNumeric, tileHeightNumeric, setTileDimensions);
        }

        /// <summary>
        /// Undo previous action
        /// </summary>
        public void Undo()
        {
            LayerIndex = -1;

            commandManager.Undo();
        }

        /// <summary>
        /// Redo previous action
        /// </summary>
        public void Redo()
        {
            LayerIndex = -1;

            commandManager.Redo();
        }

        public void AddTileset(string texturePath, int tileWidth, int tileHeight, Action<string, int, int> createTileset, Action<string> removeTileset)
        {
            commandManager.ExecuteMapAddTileset(Tilesets, texturePath, tileWidth, tileHeight, view.GetGraphicsDevice, createTileset, removeTileset);
        }

        /// <summary>
        /// Rounds off position to fit with the map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 SnapToGrid(Vector2 position)
        {
            if (TileWidth <= 0)
                return Vector2.Zero;
            if (TileHeight <= 0)
                return Vector2.Zero;

            int x = (int)position.X / TileWidth;
            int y = (int)position.Y / TileHeight;

            return new Vector2(x * TileWidth, y * TileHeight);
        }

        /// <summary>
        /// Changes set of coordinates on map to pixel length
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 CoordinateToPixels(int x, int y)
        {
            return new Vector2(x * TileWidth, y * TileHeight);
        }

        /// <summary>
        /// Changes position to set of coordnates on map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 PixelsToCoordinate(Vector2 position)
        {
            if (TileWidth <= 0)
                return Vector2.Zero;
            if (TileHeight <= 0)
                return Vector2.Zero;

            return new Vector2((int)position.X / TileWidth, (int)position.Y / TileHeight);
        }

        /// <summary>
        /// Transforms position to adjust itself to scrollable maps
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 InvertCameraMatrix(System.Drawing.Point point)
        {
            return Vector2.Transform(new Vector2(point.X, point.Y), Matrix.Invert(camera.CameraTransformation));
        }

        #endregion
    }
}
